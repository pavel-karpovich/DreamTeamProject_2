using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Unosquare.Labs.EmbedIO;
using Unosquare.Labs.EmbedIO.Modules;

namespace DreamTeamProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:9696/";

            using (var server = new WebServer(url))
            {
                server.WithLocalSession();
                server.RegisterModule(new StaticFilesModule(StaticFilesRootPath));
                server.Module<StaticFilesModule>().UseRamCache = true;
                
                server.RunAsync();
#if DEBUG
                var browser = new System.Diagnostics.Process()
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true }
                };
                browser.Start();
#endif
                Console.ReadKey(true);


            }
        }

        public static string StaticFilesRootPath
        {
            get
            {
                var assemblyPath = Path.GetDirectoryName(typeof(Program).GetTypeInfo().Assembly.Location);

                // This lets you edit the files without restarting the server.
#if DEBUG
                return Path.Combine(Directory.GetParent(assemblyPath).Parent.Parent.FullName, "DreamTeamProject/static");
#else
                // This is when you have deployed the server.
                return Path.Combine(assemblyPath, "html");
#endif
            }
        }
    }
}
