using C19VS;
using C19VS.Models;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IDatabaseHelper
{
	public void ConnectSshClient();
	public void DisconnectSshClient();
	public void ConnectDatabase();
	public void DisconnectDatabase();
}

public class DatabaseHelper : IDatabaseHelper
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

	public void ConnectSshClient()
    {
		(sshClient, localPort) = ConnectSsh(sshServer, sshUserName, sshPassword, databaseServer: databaseServer);
		
		if (sshClient.IsConnected)
		{
			Console.WriteLine("SshClient connection open.");
		}
	}

	public void DisconnectSshClient()
    {
		sshClient.Disconnect();

		if (!sshClient.IsConnected)
		{
			Console.WriteLine("SshClient connection closed.");
		}
	}

	public void ConnectDatabase()
    {
        if (sshClient.IsConnected)
        {
            var connectionString = $"Server=127.0.0.1;Port={localPort};Username={databaseUserName};Password={databasePassword};Connection Timeout=60000;DefaultCommandTimeout=60000;SslMode=None;";

            connection = new MySqlConnection(connectionString);
            connection.Open();
            if (connection.State == ConnectionState.Open)
            {
				Console.WriteLine("MySql connection open.");
            }
        }
    }

	public void DisconnectDatabase()
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
    }


	public async Task<List<object[]>> SelectAllRecords(string tableName)
	{
		if (connection.State == ConnectionState.Open)
		{
			using var command = new MySqlCommand($"SELECT * FROM njc353_1.{tableName};", connection);
			using var reader = await command.ExecuteReaderAsync();

			List<object[]> tableList = new List<object[]>();

			int headerCounter = 0;

			while (await reader.ReadAsync())
			{
				var count = reader.FieldCount;
				object[] columnNames = new object[count];

				if (headerCounter == 0)
				{
					for (int i = 0; i < count; i++)
					{
						columnNames[i] = reader.GetName(i);
					}

					tableList.Add(columnNames);

					headerCounter++;
				}

				object[] rowValues = new object[count];
				reader.GetValues(rowValues);

				for (int i = 0; i < rowValues.Length; i++)
				{
					if (rowValues[i].GetType() == typeof(DateTime))
					{
						rowValues[i] = ((DateTime)rowValues[i]).ToShortDateString();
					}
				}

				tableList.Add(rowValues);
			}

			return tableList;
		}
		else
		{
			return null;
		}
	}

	public async Task<object> SelectRecordAsync(Type type, Dictionary<string, string> dictionary)
	{
		string queryString = null;
		var dictionarylength = dictionary.Count;
		if (dictionarylength == 1)
		{
			var item = dictionary.First();
			queryString = $"SELECT * FROM njc353_1.{type.Name} WHERE {item.Key}='{item.Value}';";
		}
		else if (dictionarylength == 2)
		{
			List<(string, string)> list = new List<(string, string)>();
			foreach (var item in dictionary)
			{
				list.Add((item.Key, item.Value));
			}
			queryString = $"SELECT * FROM njc353_1.{type.Name} WHERE {list[0].Item1}='{list[0].Item2}' AND {list[1].Item1}='{list[1].Item2}';";
		}

		using var command = new MySqlCommand(queryString, connection);
		using var reader = await command.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			var count = reader.FieldCount;

			object[] rowValues = new object[count];
			reader.GetValues(rowValues);

			var objInstance = Activator.CreateInstance(type, new object[] { rowValues });

			return objInstance;
		}

		return null;
	}


	public bool UpdateRecord(Type type, object obj, Dictionary<string, string> dictionary)
    {
        System.Reflection.PropertyInfo[] props = null;

		var objInstance = Activator.CreateInstance(type, obj);

        StringBuilder sb = new StringBuilder();
        sb.Append($"UPDATE njc353_1.{type.Name} SET ");

		props = objInstance.GetType().GetProperties();

		foreach (var prop in props)
		{
			var name = prop.Name;
			var value = prop.GetValue(objInstance, null);

			// Exceptions for Person.
			if (name == "dob") value = ((DateTime)value).ToShortDateString();
			if (name == "infected") value = (int)(((bool)value) ? 1 : 0);

			// Exceptions for AgeGroup
			if (name == "Allowed") value = (int)(((bool)value) ? 1 : 0);

			//TODO: Add other exceptions below.
			//Exception for Dose
			if (name == "doseDate") value = ((DateTime)value).ToShortDateString();

			//Exception for Infection
			if (name == "infectionDate") value = ((DateTime)value).ToShortDateString();

			//Exception for Vaccine
			if (name == "approvalDate") value = ((DateTime)value).ToShortDateString();
			if (name == "suspensionDate") value = ((DateTime)value).ToShortDateString();

			sb.Append($"{name}='{value}',");
		}

        sb.Length--;

		var dictionarylength = dictionary.Count;
		if (dictionarylength == 1)
		{
			var item = dictionary.First();
			sb.Append($" WHERE {item.Key}='{item.Value}';");
		}
		else if (dictionarylength == 2)
		{
			List<(string, string)> list = new List<(string, string)>();
			foreach (var item in dictionary)
			{
				list.Add((item.Key, item.Value));
			}
			if (list[1].Item1 == "infectionDate") 
			{
				var dateArray = list[1].Item2.Split('-');
				var subDateArray = dateArray[2].Split(' ');
				var newDate = new DateTime(Int32.Parse(dateArray[0]), Int32.Parse(dateArray[1]), Int32.Parse(subDateArray[0])).ToShortDateString();

				sb.Append($" WHERE {list[0].Item1}='{list[0].Item2}' AND {list[1].Item1}='{newDate}';");
			}
            else
            {
				sb.Append($" WHERE {list[0].Item1}='{list[0].Item2}' AND {list[1].Item1}='{list[1].Item2}';");
            }
		}

		using var command = new MySqlCommand(sb.ToString(), connection);
        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected == 1)
            return true;

        return false;
    }

    public bool DeleteRecord(Type type, Dictionary<string, string> dictionary)
    {
		string queryString = null;
		var dictionarylength = dictionary.Count;
		if (dictionarylength == 1)
		{
			var item = dictionary.First();
			queryString = $"DELETE FROM njc353_1.{type.Name} WHERE {item.Key}='{item.Value}';";
		}
		else if (dictionarylength == 2)
		{
			List<(string, string)> list = new List<(string, string)>();
			foreach (var item in dictionary)
			{
				list.Add((item.Key, item.Value));
			}
			queryString = $"DELETE FROM njc353_1.{type.Name} WHERE {list[0].Item1}='{list[0].Item2}' AND {list[1].Item1}='{list[1].Item2}';";
		}

        using var command = new MySqlCommand(queryString, connection);
        int rowsAffected = command.ExecuteNonQuery();

        if (rowsAffected == 1)
        {
            return true;
        }

        return false;
    }

	public bool InsertRecord(Type type, object obj)
	{
		System.Reflection.PropertyInfo[] props = null;

		var objInstance = Activator.CreateInstance(type, obj);

		StringBuilder sb = new StringBuilder();
		sb.Append($"INSERT INTO njc353_1.{type.Name} VALUES (");

		props = objInstance.GetType().GetProperties();

		foreach (var prop in props)
		{
			var name = prop.Name;
			var value = prop.GetValue(objInstance, null);

			// Exceptions for Person.
			if (name == "dob") value = ((DateTime)value).ToShortDateString();
			if (name == "infected") value = (int)(((bool)value) ? 1 : 0);

			// Exceptions for AgeGroup
			if (name == "Allowed") value = (int)(((bool)value) ? 1 : 0);

			//TODO: Add other exceptions below.

			//Exception for Dose
			if (name == "doseDate") value = ((DateTime)value).ToShortDateString();

			//Exception for Infection
			if (name == "infectionDate") value = ((DateTime)value).ToShortDateString();

			//Exception for Vaccine
			if (name == "approvalDate") value = ((DateTime)value).ToShortDateString();
			if (name == "suspensionDate") value = ((DateTime)value).ToShortDateString();
			sb.Append($"'{value}',");
		}

		sb.Length--;
		sb.Append($");");

		using var command = new MySqlCommand(sb.ToString(), connection);
		int rowsAffected = command.ExecuteNonQuery();

		if (rowsAffected == 1)
		{
			return true;
		}

		return false;
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