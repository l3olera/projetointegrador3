using System;
using System.Data;
using System.IO;
using Mono.Data.Sqlite;
using UnityEditor.Experimental;
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
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ShowInventory();
        }
    }

    //ITEMS
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
            }
        }
        connection.Close();
    }

    public void ShowItemsById(int id_item){
        using var connection = new SqliteConnection(dbPath);
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Items WHERE id_item = @id_item;";
            command.Parameters.Add(new SqliteParameter("@id_item", id_item));
            using IDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string description = reader.GetString(2);
            }
        }
        connection.Close();
    }

    //INVENTORY
    public void ShowInventory()
    {
        using var connection = new SqliteConnection(dbPath);
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "SELECT * FROM Inventory;";
            using IDataReader reader = command.ExecuteReader();
            while (reader.Read())
            { 
                int id_inventory = reader.GetInt32(0); 

                // Verifica o tipo da coluna id_item
                object id_itemValue = reader.IsDBNull(1) ? null : reader.GetValue(1);
                string id_item = id_itemValue != null ? id_itemValue.ToString() : "NULL";
            }
        }
        connection.Close();
    }

    public void AlterItemInventory(int id_inventory, int? id_item)
    {
        using var connection = new SqliteConnection(dbPath);
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "UPDATE Inventory SET id_item = @id_item WHERE id_inventory = @id_inventory;";
            
            command.Parameters.Add(new SqliteParameter("@id_inventory", id_inventory));
            command.Parameters.Add(new SqliteParameter("@id_item", id_item ?? (object)DBNull.Value));
            command.ExecuteNonQuery();
        }
        connection.Close();
    }

    public void JoinItemsAndInventory()
    {
        using var connection = new SqliteConnection(dbPath);
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            // Comando SQL para realizar a junção
            command.CommandText = @"
                SELECT Items.name 
                FROM Inventory 
                INNER JOIN Items ON Inventory.id_item = Items.id;
            ";

            using IDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string item_name = reader.GetString(1); // Nome do item
            }
        }
        connection.Close();
    }
}
