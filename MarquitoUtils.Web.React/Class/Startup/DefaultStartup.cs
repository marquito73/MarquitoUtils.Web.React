using MarquitoUtils.Main.Class.Entities.File;
using MarquitoUtils.Main.Class.Entities.Sql;
using MarquitoUtils.Main.Class.Tools.Encryption;
using MarquitoUtils.Web.React.Class.Tools;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.Extensions.FileProviders;
using MarquitoUtils.Main.Class.Service.Sql;
using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Main.Class.Sql;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using JavaScriptEngineSwitcher.V8;
using React.AspNet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using MarquitoUtils.Main.Class.Entities.Translation;
using MarquitoUtils.Main.Class.Service.General;
using MarquitoUtils.Main.Class.Translations;
using MarquitoUtils.Main.Class.Service.Files;
using Microsoft.AspNetCore.Routing;

namespace MarquitoUtils.Web.React.Class.Startup
{
    /// <summary>
    /// Default startup class
    /// </summary>
    /// <typeparam name="T">Database context</typeparam>
    public abstract class DefaultStartup<T> where T : DefaultDbContext
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
        /// The database context
        /// </summary>
        protected T DbContext { get; private set; }

        /// <summary>
        /// Default startup class
        /// </summary>
        /// <param name="configuration">Configuration properties</param>
        /// <param name="notifyHubName">The notify hub name</param>
        /// <param name="executeScripts">Execute sql scripts ?</param>
        public DefaultStartup(IConfiguration configuration, bool executeScripts = false)
        {
            this.Configuration = configuration;

            if (executeScripts)
            {
                this.ManageSqlScripts();
            }

            this.ManageTranslations(Assembly.GetEntryAssembly());
        }

        /// <summary>
        /// Init translations from an XML translation file
        /// </summary>
        /// <param name="translationFilePath">The path to access translations</param>
        private void ManageTranslations(Assembly translationFilePath)
        {
            ITranslateService translateService = new TranslateService();

            List<Translation> translations = translateService.GetTranslations(
                    @Properties.Resources.translateFilePath, translationFilePath);

            Translate.SetTranslationService(translations);
        }

        /// <summary>
        /// Manage sql scripts
        /// </summary>
        private void ManageSqlScripts()
        {
            DatabaseConfiguration databaseConfiguration =
                this.FileService.GetDefaultDatabaseConfiguration();
            // Init startup db context
            this.DbContext = DefaultDbContext
                .GetDbContext<T>(databaseConfiguration);

            this.SqlScriptService = new SqlScriptService(databaseConfiguration);
            this.SqlScriptService.EntityService = new EntityService();
            this.SqlScriptService.EntityService.DbContext = this.DbContext;
            // If script_history table not found, we need to create it
            if (!this.SqlScriptService.CheckIfTableExist("script_history"))
            {
                // Get script for save all sql files executed
                CustomFile sqlHistoryScript = WebFileHelper.GetSqlFile("001_ScriptHistory");
                // Execute it
                this.SqlScriptService.ExecuteSqlScript(sqlHistoryScript.FileName, sqlHistoryScript.Content, false);
            }
            // If translation and translation_field tables not found, we need to create it
            if (!this.SqlScriptService.CheckIfTableExist("translation") 
                && !this.SqlScriptService.CheckIfTableExist("translation_field"))
            {
                // Get script for translations
                CustomFile sqlTranslations = WebFileHelper.GetSqlFile("002_Translations");
                // Execute it
                this.SqlScriptService.ExecuteSqlScript(sqlTranslations.FileName, sqlTranslations.Content, false);
            }
            // If user track history table not found, we need to create it
            if (!this.SqlScriptService.CheckIfTableExist("user_track_history"))
            {
                // Get script for user track history
                CustomFile sqlTranslations = WebFileHelper.GetSqlFile("003_UserTrackHistory");
                // Execute it
                this.SqlScriptService.ExecuteSqlScript(sqlTranslations.FileName, sqlTranslations.Content, false);
            }
            // Execute alls sql scripts
            this.ExecuteSqlScripts(this.SqlScriptService);
            // Flush eventual data
            this.SqlScriptService.EntityService.FlushData(out Exception exception);

            this.DbContext.Dispose();
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
            services.AddSignalR();
        }

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

        /// <summary>
        /// Get current library assembly
        /// </summary>
        /// <returns>Current library assembly</returns>
        public Assembly GetLibraryWebAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
        
        /// <summary>
        /// Init database creditentials
        /// </summary>
        /// <param name="dbUser">Database user</param>
        /// <param name="dbPassword">Database user's password</param>
        /// <param name="dbSource">Database source</param>
        /// <param name="dbName">Database's name</param>
        protected void InitDatabaseCreditentials(string dbUser, string dbPassword, string dbSource, 
            string dbName)
        {
            /*SqlConnectionBuilder sqlConnectionBuilder = new SqlConnectionBuilder(dbUser,
                dbPassword, dbSource, dbName);

            string encryptedDb = Encrypter.EncryptString(sqlConnectionBuilder.GetConnectionString());

            string decryptedDb = Encrypter.DecryptString(encryptedDb);*/
        }

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
