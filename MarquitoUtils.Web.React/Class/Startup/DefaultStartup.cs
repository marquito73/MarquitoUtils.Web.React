using MarquitoUtils.Main.Class.Entities.File;
using MarquitoUtils.Main.Class.Entities.Sql;
using MarquitoUtils.Main.Class.Tools.Encryption;
using MarquitoUtils.Web.React.Class.Enums;
using MarquitoUtils.Web.React.Class.Tools;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using MarquitoUtils.Main.Class.Service.Sql;
using MarquitoUtils.Main.Class.Service.Files;
using MarquitoUtils.Main.Class.Tools;
using MarquitoUtils.Main.Class.Sql;
using MarquitoUtils.Web.React.Class.Communication;

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
        /// <param name="executeScripts">Execute sql scripts ?</param>
        public DefaultStartup(IConfiguration configuration, bool executeScripts = false)
        {
            this.Configuration = configuration;

            if (executeScripts)
            {
                SqlConnectionBuilder connectionBuilder = this.SetupSqlServerConnection();
                this.SqlScriptService = new SqlScriptService(connectionBuilder);
                // Init startup db context
                this.DbContext = DefaultDbContext.GetDbContext<T>(connectionBuilder);
                this.SqlScriptService.EntityService = new EntityService();
                this.SqlScriptService.EntityService.DbContext = this.DbContext;
                // If script_history table nout found, we need to create it
                if (!this.SqlScriptService.CheckIfTableExist("script_history"))
                {
                    // Get script for save all sql files executed
                    CustomFile sqlHistoryScript = WebFileHelper.GetSqlFile("001_ScriptHistory");
                    // Execute it
                    this.SqlScriptService.ExecuteSqlScript(sqlHistoryScript.FileName, sqlHistoryScript.Content, false);
                }
                // Execute alls sql scripts
                this.ExecuteSqlScripts(this.SqlScriptService);
                // Flush eventual data
                this.SqlScriptService.EntityService.FlushData();
            }
        }

        /// <summary>
        /// Setup sql server connection
        /// </summary>
        /// <returns>Sql server connection builder</returns>
        protected abstract SqlConnectionBuilder SetupSqlServerConnection();

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
            SqlConnectionBuilder sqlConnectionBuilder = new SqlConnectionBuilder(dbUser,
                dbPassword, dbSource, dbName);

            string encryptedDb = Encrypter.EncryptString(sqlConnectionBuilder.GetConnectionString());

            string decryptedDb = Encrypter.DecryptString(encryptedDb);
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
