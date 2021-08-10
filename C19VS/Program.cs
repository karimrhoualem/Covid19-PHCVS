using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace C19VS
{
    public class Program
    {
        public static DatabaseHelper databaseHelper = new DatabaseHelper(User.KARIM);

		public static void Main(string[] args)
        {
            databaseHelper.ConnectSshClient();

            CreateHostBuilder(args).Build().Run();

            // It's important to close the console using ctrl-c rather than closing the browser, otherwise 
            // the ssh connection won't close as it should.
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);
        }

        private static void OnProcessExit(object sender, EventArgs e)
        {
            databaseHelper.DisconnectSshClient();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(typeof(IDatabaseHelper), databaseHelper);
                });
	}
}
