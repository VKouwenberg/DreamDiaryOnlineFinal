using DataAccessDDO.ModelsDTO;
using DataAccessDDO.DatabaseSettings;
using Microsoft.EntityFrameworkCore.Metadata;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDDO.Repositories;

public class DreamerRepo
{
	private readonly DatabaseSettings.DatabaseSettings _databaseSettings;

	public DreamerRepo(DatabaseSettings.DatabaseSettings databaseSettings)
	{
		_databaseSettings = databaseSettings;
	}

	public void CreateDreamer(DreamerDTO dreamer)
	{
		using (MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString))
		{
			connection.Open();
			string query = "INSERT INTO Dreamer (DreamerName) VALUES (@DreamerName)";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@DreamerName", dreamer.DreamerName);
				command.ExecuteNonQuery();
			}
		}
	}

	public DreamerDTO ReadDreamer(int dreamerId)
	{
		using (MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString))
		{
			connection.Open();
			string query = "SELECT * FROM Dreamer WHERE DreamerId = @DreamerId";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@DreamerId", dreamerId);
				using (MySqlDataReader reader  = command.ExecuteReader())
				{
					if (reader.Read())
					{
						return new DreamerDTO
						{
							DreamerId = Convert.ToInt32(reader["DreamerId"]),
							DreamerName = reader["DreamerName"].ToString()
						};
					}
				}
			}
		}
		return null;
	}

	public void UpdateDreamer(DreamerDTO dto)
	{
		using (MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString))
		{
			connection.Open();
			string query = "UPDATE Dreamer SET DreamerName = @DreamerName WHERE DreamerId = @DreamerId";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@DreamerId", dto.DreamerId);
				command.Parameters.AddWithValue("@DreamerName", dto.DreamerName);
				command.ExecuteNonQuery();
			}
		}
	}

	public void DeleteDreamer(int dreamerId)
	{
		using (MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString))
		{
			connection.Open();
			string query = "DELETE FROM Dreamer WHERE DreamerId = @DreamerId = @DreamerId";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@DreamerId", dreamerId);
				command.ExecuteNonQuery ();
			}
		}
	}

	public List<DreamerDTO> GetAllDreamers()
	{
		List<DreamerDTO> dreamers = new List<DreamerDTO>();

		using (MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString))
		{
			connection.Open();
			string query = "SELECT * FROM Dreamer";

			using MySqlCommand command = new MySqlCommand(query, connection);
			using (MySqlDataReader reader = command.ExecuteReader())
			{
				while (reader.Read())
				{
					DreamerDTO dto = new DreamerDTO()
					{
						DreamerId = reader.GetInt32("DreamId"),
						DreamerName = reader.GetString("DreamName")
					};

					dreamers.Add(dto);
				}
			}
		}

		return dreamers;
	}
}