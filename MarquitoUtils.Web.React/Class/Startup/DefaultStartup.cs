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

namespace MarquitoUtils.Web.React.Class.Startup
{
    public abstract class DefaultStartup
    {
        protected IConfiguration Configuration { get; }

        public DefaultStartup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public Assembly GetLibraryWebAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }

        /*SqlConnectionBuilder sqlConnectionBuilder = new SqlConnectionBuilder(Properties.Resources.dbUser,
                Properties.Resources.dbPassword, Properties.Resources.dbSource, Properties.Resources.dbCatalog);*/

        protected void InitDatabaseCreditentials(string dbUser, string dbPassword, string dbSource, 
            string dbCatalog)
        {
            SqlConnectionBuilder sqlConnectionBuilder = new SqlConnectionBuilder(dbUser,
                dbPassword, dbSource, dbCatalog);

            string encryptedDb = Encrypter.EncryptString(sqlConnectionBuilder.GetConnectionString());

            string decryptedDb = Encrypter.DecryptString(encryptedDb);

            string test = "";
        }

        protected string LoadImportFiles()
        {
            string executingPath = WebFileHelper.GetExecutingLocationPath(Assembly.GetEntryAssembly());

            List<CustomFile> staticFilesContents = WebFileHelper.GetStaticFiles();

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
