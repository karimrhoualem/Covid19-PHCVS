using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
		// ONLY ALTER THE FILE PATH BELOW
		public static string FILE_PATH = FilePath.GetFilePath(User.KARIM);

		// DO NOT ALTER THE CONST VALUES BELOW
		public const string sshServer = "login.encs.concordia.ca";
		public const string databaseServer = "njc353.encs.concordia.ca";
		public const string databaseUserName = "njc353_1";
		public const string databasePassword = "AGKM4424";

		public static async Task Main(string[] args)
        {
			string[] username_password = File.ReadAllLines(FILE_PATH);

			var sshUserName = username_password[0];
			var sshPassword = username_password[1];

			var (sshClient, localPort) = ConnectSsh(sshServer, sshUserName, sshPassword, databaseServer: databaseServer);
			using (sshClient)
			{
				MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder
				{
					Server = "127.0.0.1",
					Port = localPort,
					UserID = databaseUserName,
					Password = databasePassword,
				};

				var connectionString = $"Server=127.0.0.1;Port={localPort};Username={databaseUserName};Password={databasePassword};Connection Timeout=60000;DefaultCommandTimeout=60000;SslMode=None;";

				using var connection = new MySqlConnection(connectionString);
				connection.Open();

				using var command = new MySqlCommand("SELECT * FROM njc353_1.Person;", connection);
				using var reader = await command.ExecuteReaderAsync();
				while (await reader.ReadAsync())
				{
					var column0 = reader.GetValue(0);
					var column1 = reader.GetValue(1);
					var column2 = reader.GetValue(2);
					var column3 = reader.GetValue(3);
				}
			}

			CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

		public static (SshClient SshClient, uint Port) ConnectSsh(string sshHostName, string sshUserName, string sshPassword = null,
					string sshKeyFile = null, string sshPassPhrase = null, int sshPort = 22, string databaseServer = "localhost", int databasePort = 3306)
		{
			// check arguments
			if (string.IsNullOrEmpty(sshHostName))
				throw new ArgumentException($"{nameof(sshHostName)} must be specified.", nameof(sshHostName));
			if (string.IsNullOrEmpty(sshHostName))
				throw new ArgumentException($"{nameof(sshUserName)} must be specified.", nameof(sshUserName));
			if (string.IsNullOrEmpty(sshPassword) && string.IsNullOrEmpty(sshKeyFile))
				throw new ArgumentException($"One of {nameof(sshPassword)} and {nameof(sshKeyFile)} must be specified.");
			if (string.IsNullOrEmpty(databaseServer))
				throw new ArgumentException($"{nameof(databaseServer)} must be specified.", nameof(databaseServer));

			// define the authentication methods to use (in order)
			var authenticationMethods = new List<AuthenticationMethod>();
			if (!string.IsNullOrEmpty(sshKeyFile))
			{
				authenticationMethods.Add(new PrivateKeyAuthenticationMethod(sshUserName,
					new PrivateKeyFile(sshKeyFile, string.IsNullOrEmpty(sshPassPhrase) ? null : sshPassPhrase)));
			}
			if (!string.IsNullOrEmpty(sshPassword))
			{
				authenticationMethods.Add(new PasswordAuthenticationMethod(sshUserName, sshPassword));
			}

			// connect to the SSH server
			var sshClient = new SshClient(new ConnectionInfo(sshHostName, sshPort, sshUserName, authenticationMethods.ToArray()));
			sshClient.Connect();
			Console.WriteLine("SSH client connected.");

			// forward a local port to the database server and port, using the SSH server
			var forwardedPort = new ForwardedPortLocal("127.0.0.1", databaseServer, (uint)databasePort);
			sshClient.AddForwardedPort(forwardedPort);
			forwardedPort.Start();

			return (sshClient, forwardedPort.BoundPort);
		}
	}
}
