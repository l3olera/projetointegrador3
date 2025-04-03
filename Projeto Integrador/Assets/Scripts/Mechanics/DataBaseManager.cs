using System;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    private string dbPath;

    void Start()
    {
        // Caminho para o arquivo do banco de dados no StreamingAssets
        string filePath = Path.Combine(Application.streamingAssetsPath, "LakshmiBarkventuresDB.db");
        
        // String de conexão para o banco de dados
        dbPath = $"URI=file:{filePath}";
        Debug.Log($"Conectando ao banco de dados em: {dbPath}");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetComponent<DatabaseManager>().AddItem("Gate Key", "Key to open the gate");
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            GetComponent<DatabaseManager>().ShowItems();
            Debug.Log("Mostrando itens no banco de dados...");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            GetComponent<DatabaseManager>().ShowTableInfo("items");
        }
    }

    public void AddItem(string name, string description)
    {
        using var connection = new SqliteConnection(dbPath);
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO items (name, description) VALUES (@name, @description);";
            command.Parameters.Add(new SqliteParameter("@name", name));
            command.Parameters.Add(new SqliteParameter("@description", description));
            command.ExecuteNonQuery();
            Debug.Log($"Item '{name}' adicionado com sucesso!");
        }
        connection.Close();
    }

    public void ShowItems()
    {
        using var connection = new SqliteConnection(dbPath);
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Items;";
            using IDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string description = reader.GetString(2);
                Debug.Log($"ID: {id} - Nome: {name} - Descrição: {description}");
            }
        }
        connection.Close();
    }

    public void ShowTableInfo(string tableName)
    {
        using var connection = new SqliteConnection(dbPath);
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = $"PRAGMA table_info({tableName});";
            using IDataReader reader = command.ExecuteReader();
            Debug.Log($"Estrutura da tabela '{tableName}':");
            while (reader.Read())
            {
                string columnName = reader.GetString(1); // Nome da coluna
                string columnType = reader.GetString(2); // Tipo da coluna
                Debug.Log($"Coluna: {columnName}, Tipo: {columnType}");
            }
        }
        connection.Close();
    }
}
