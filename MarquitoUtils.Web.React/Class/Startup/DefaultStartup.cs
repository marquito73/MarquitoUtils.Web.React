using MarquitoUtils.Main.Class.Entities.File;
using MarquitoUtils.Web.React.Class.Tools;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.FileProviders;
using MarquitoUtils.Main.Class.Service.Sql;
using MarquitoUtils.Main.Class.Sql;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using JavaScriptEngineSwitcher.V8;
using React.AspNet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MarquitoUtils.Main.Class.Service.General;
using MarquitoUtils.Main.Class.Service.Files;
using Microsoft.AspNetCore.Routing;
using MarquitoUtils.Web.React.Class.Entities.File;
using MarquitoUtils.Main.Class.Entities.Sql;
using MarquitoUtils.Main.Class.Entities.Sql.Translations;
using MarquitoUtils.Main.Class.Entities.Sql.UserTracking;
using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Web.React.Class.Config.WebFront;
using MarquitoUtils.Main.Class.Cache;

namespace MarquitoUtils.Web.React.Class.Startup
{
    /// <summary>
    /// Default startup class
    /// </summary>
    /// <typeparam name="DBContext">Database context</typeparam>
    public abstract class DefaultStartup<DBContext> where DBContext : DefaultDbContext
    {
        protected IConfiguration Configuration { get; }
        /// <summary>
        /// The file service
        /// </summary>
        private IFileService FileService { get; set; } = new FileService();
        /// <summary>
        /// The Sql script service
        /// </summary>
        private ISqlScriptService SqlScriptService { get; set; }
        /// <summary>
        /// Entity service
        /// </summary>
        private IEntityService EntityService { get; set; }
        /// <summary>
        /// The database context
        /// </summary>
        protected DBContext DbContext { get; private set; }
        /// <summary>
        /// Entities stored in cache
        /// </summary>
        private EntityCache EntityCache { get; } = new EntityCache();

        /// <summary>
        /// Default startup class
        /// </summary>
        /// <param name="configuration">Configuration properties</param>
        /// <param name="notifyHubName">The notify hub name</param>
        /// <param name="executeScripts">Execute sql scripts ?</param>
        public DefaultStartup(IConfiguration configuration, bool executeScripts = false)
        {
            this.Configuration = configuration;

            this.ManageDatabaseContext(executeScripts);
        }

        /// <summary>
        /// Manage database (migration scripts, entity cache, etc ..)
        /// </summary>
        /// <param name="executeScripts">Execute scripts ?</param>
        private void ManageDatabaseContext(bool executeScripts)
        {
            DatabaseConfiguration databaseConfiguration =
                this.FileService.GetDefaultDatabaseConfiguration();
            // Init startup db context
            this.DbContext = DefaultDbContext
                .GetDbContext<DBContext>(databaseConfiguration);
            // Init entity service
            this.EntityService = new EntityService()
            {
                DbContext = this.DbContext,
            };
            // Init sql script service
            this.SqlScriptService = new SqlScriptService(databaseConfiguration);
            this.SqlScriptService.EntityService = this.EntityService;
            // Execute scripts
            if (executeScripts)
            {
                this.ManageSqlScripts();
            }
            // Prepare entity cache
            if (this.StoreDatabaseEntitiesInCache())
            {
                this.ManageCache();
            }
        }

        /// <summary>
        /// Manage sql scripts
        /// </summary>
        private void ManageSqlScripts()
        {
            // If script_history table not found, we need to create it
            if (!this.SqlScriptService.CheckIfTableExist<ScriptHistory>())
            {
                // Get script for save all sql files executed and execute it
                this.SqlScriptService.ExecuteEntitySqlScript<ScriptHistory>(false);
            }
            // Get script for translations and execute it
            this.SqlScriptService.ExecuteEntitySqlScript<TranslationField>();
            this.SqlScriptService.ExecuteEntitySqlScript<Translation>();
            // Get script for user track history and execute it
            this.SqlScriptService.ExecuteEntitySqlScript<UserTrackHistory>();
            // Execute all entities scripts
            typeof(DBContext).GetProperties()
                .Where(prop => prop.PropertyType.IsGenericDbSetType())
                .Select(prop => prop.PropertyType.GenericTypeArguments[0])
                .Where(entityType =>
                {
                    return !entityType.Assembly.Equals(typeof(ScriptHistory).Assembly);
                })
                .ForEach(entityType => this.SqlScriptService.ExecuteEntitySqlScript(entityType));
            // Execute alls custom sql scripts
            this.ExecuteSqlScripts(this.SqlScriptService);
            // Flush eventual data
            this.SqlScriptService.EntityService.FlushData(out Exception exception);
        }

        /// <summary>
        /// Manage the loading of entities inside the cache
        /// </summary>
        private void ManageCache()
        {
            typeof(DBContext).GetProperties()
                .Where(prop => prop.PropertyType.IsGenericDbSetType())
                .Select(prop => prop.PropertyType.GenericTypeArguments[0])
                .ForEach(entityType =>
                {
                    // Get generics type parameters, and methods type parameters
                    Type[] genericArgs = { entityType };
                    Type[] methodsArgs = { typeof(bool) };
                    // Then get the ExecuteEntitySqlScript method
                    MethodInfo method = this.SqlScriptService.EntityService
                        .GetType().GetMethod(nameof(IEntityService.GetEntityList), methodsArgs)
                        .MakeGenericMethod(genericArgs);
                    // Finally, execute it and store entities in cache
                    IEnumerable<Entity> entities = method.Invoke(this.SqlScriptService
                        .EntityService, new object[] { Type.Missing }) as IEnumerable<Entity>;
                    this.EntityCache.AddEntities(entityType, entities.ToList());
                });

            // Manage entity cache
            this.EntityService.EntityCache = this.EntityCache;

            this.EntityService.DbContext.UseCache = this.StoreDatabaseEntitiesInCache();
        }

        /// <summary>
        /// Add session and controller services here
        /// </summary>
        /// <param name="services">Services</param>
        protected abstract void AddSessionAndControllersServices(IServiceCollection services);

        /// <summary>
        /// Configure services added to the app
        /// This method gets called by the runtime
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            this.AddSessionAndControllersServices(services);
            services.AddMemoryCache();
            services.AddMvc();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddReact();
            // Make sure a JS engine is registered, or you will get an error!
            services.AddJsEngineSwitcher(options => options.DefaultEngineName = V8JsEngine.EngineName)
              .AddV8();
            // Add signalR to the website
            services.AddSignalR()
                // Disable camelCase naming policy
                .AddJsonProtocol(options => {
                    options.PayloadSerializerOptions.PropertyNamingPolicy = null;
                });

            // Init startup options
            StartupOptions options = new StartupOptions();

            // Translations
            options.Translations = this.GetTranslations(Assembly.GetEntryAssembly());

            // Web configuration
            if (this.UseWebFrontConfigFromConfigFile())
            {
                WebFrontConfiguration webFrontConfig = this.FileService
                    .GetDataFromXMLFile<WebFrontConfiguration>(@"Files\Configuration\WebFront.config");

                options.WebFrontConfiguration = webFrontConfig;
            }

            // Syncfusion
            if (this.RegisterSyncFusionLicenseKeyFromConfigFile())
            {
                SyncFusionConfiguration syncFusionConfig = this.FileService
                    .GetDataFromXMLFile<SyncFusionConfiguration>(@"Files\Configuration\SyncFusion.config");

                options.SyncFusionConfiguration = syncFusionConfig;
            }
            services.AddSingleton(options);

            services.AddSingleton(this.EntityService);
        }

        /// <summary>
        /// Get translations from XML translation file
        /// </summary>
        /// <param name="translationFilePath"></param>
        /// <returns></returns>
        private List<Main.Class.Entities.Translation.Translation> GetTranslations(Assembly translationFilePath)
        {
            ITranslateService translateService = new TranslateService();

            return translateService.GetTranslations(@Properties.Resources.translateFilePath, translationFilePath);
        }

        /// <summary>
        /// Use web config file for fonts, sizes and colors of the website ?
        /// </summary>
        /// <remarks>Config file need to be Files\Configuration\WebFront.config (as embedded resource)</remarks>
        /// <returns></returns>
        protected abstract bool UseWebFrontConfigFromConfigFile();

        /// <summary>
        /// Register a SyncFusion license key ?
        /// </summary>
        /// <remarks>Config file need to be Files\Configuration\SyncFusion.config (as embedded resource)</remarks>
        /// <returns></returns>
        protected abstract bool RegisterSyncFusionLicenseKeyFromConfigFile();

        /// <summary>
        /// Put database entities in cache ? (Improve performance)
        /// </summary>
        /// <returns></returns>
        protected abstract bool StoreDatabaseEntitiesInCache();

        /// <summary>
        /// Configure the HTTP request pipeline.
        /// This method gets called by the runtime
        /// <summary> 
        /// </summary>
        /// <param name="app">The app</param>
        /// <param name="env">Environnement</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                this.ConfigureEndpointController(endpoints);
                // Manage notify hubs
                this.ConfigureHubs(endpoints);
            });

            // Initialise ReactJS.NET. Must be before static files.
            app.UseReact(config =>
            {

            });
        }

        protected virtual void ConfigureEndpointController(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }

        protected virtual void ConfigureHubs(IEndpointRouteBuilder endpoints)
        {

        }

        /// <summary>
        /// Execute sql scripts
        /// </summary>
        /// <param name="sqlScriptService">Script service</param>
        protected abstract void ExecuteSqlScripts(ISqlScriptService sqlScriptService);

        protected string LoadImportFiles()
        {
            string executingPath = WebFileHelper.GetExecutingLocationPath(Assembly.GetEntryAssembly());

            List<CustomFile> staticFilesContents = WebFileHelper.GetWebStaticFiles();

            string tempDirPath = WebFileHelper.WriteWebTempFiles(staticFilesContents);

            return tempDirPath;
        }

        protected StaticFileOptions GetStaticFileOptions(string root)
        {
            StaticFileOptions fileOptions = new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    root
                    ),
                RequestPath = "/StaticWebComponents"
            };

            return fileOptions;
        }
    }
}
