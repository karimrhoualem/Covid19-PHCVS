using C19VS;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

public class DatabaseHelper
{
	private static string FILE_PATH;

	private const string sshServer = "login.encs.concordia.ca";
	private const string databaseServer = "njc353.encs.concordia.ca";
	private const string databaseUserName = "njc353_1";
	private const string databasePassword = "AGKM4424";

	private string[] username_password = null;
	private string sshUserName = null;
	private string sshPassword = null;

	private SshClient sshClient;
	private uint localPort;

	private MySqlConnection connection;

	public DatabaseHelper(User user)
	{
		FILE_PATH = FilePath.GetFilePath(user);

		username_password = File.ReadAllLines(FILE_PATH);

		sshUserName = username_password[0];
		sshPassword = username_password[1];
	}

	public void Connect()
    {
        (sshClient, localPort) = ConnectSsh(sshServer, sshUserName, sshPassword, databaseServer: databaseServer);

        if (sshClient.IsConnected)
        {
			Console.WriteLine("SshClient connection open.");

            var connectionString = $"Server=127.0.0.1;Port={localPort};Username={databaseUserName};Password={databasePassword};Connection Timeout=60000;DefaultCommandTimeout=60000;SslMode=None;";

            connection = new MySqlConnection(connectionString);
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
				Console.WriteLine("MySql connection open.");
            }
        }
    }

	public void Disconnect()
    {
        if (sshClient.IsConnected)
        {
            if (connection.State == ConnectionState.Open)
            {
				connection.Close();
            }

			if (connection.State == ConnectionState.Closed)
			{
				Console.WriteLine("MySql connection closed.");
			}
		}

		sshClient.Disconnect();

        if (!sshClient.IsConnected)
        {
			Console.WriteLine("SshClient connection closed.");
		}
    }

	public async Task TestQueryAsync()
    {
        if (connection.State == ConnectionState.Open)
        {
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
    }

	private static (SshClient SshClient, uint Port) ConnectSsh(string sshHostName, string sshUserName, string sshPassword = null,
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
