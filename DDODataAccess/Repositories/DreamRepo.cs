using DataAccessDDO.DatabaseSettings;
using DataAccessDDO.ModelsDTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDDO.Repositories;

public class DreamRepo
{
	private readonly DatabaseSettings.DatabaseSettings _databaseSettings;

	public DreamRepo(DatabaseSettings.DatabaseSettings databaseSettings)
	{
		_databaseSettings = databaseSettings;
	}

	public void CreateDreamForDreamer(DreamDTO dream, DreamerDTO dreamer)
	{
		using (MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString))
		{
			connection.Open();

			string query = "INSERT INTO Dream (DreamName, DreamerId) VALUES (@DreamName, @DreamerId)";

			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@DreamName", dream.DreamName);
				command.Parameters.AddWithValue("@DreamerId", dreamer.DreamerId);

				command.ExecuteNonQuery();
			}
		}
	}

	public DreamDTO ReadDream(int dreamId)
	{
		using (MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString))
		{
			connection.Open();
			string query = "SELECT * FROM Dream WHERE DreamId = @DreamId";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@DreamerId", dreamId);
				using (MySqlDataReader reader = command.ExecuteReader())
				{
					if (reader.Read())
					{
						return new DreamDTO
						{
							DreamId = Convert.ToInt32(reader["DreamId"]),
							DreamName = reader["DreamName"].ToString()
						};
					}
				}
			}
		}
		return null;
	}

	public void UpdateDream(DreamDTO dto)
	{
		using (MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString))
		{
			connection.Open();
			string query = "UPDATE Dream SET DreamName = @DreamName WHERE DreamId = @DreamId";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@DreamId", dto.DreamId);
				command.Parameters.AddWithValue("@DreamName", dto.DreamName);
				command.ExecuteNonQuery();
			}
		}
	}
	public void DeleteDream(int dreamId)
	{
		using (MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString))
		{
			connection.Open();
			string query = "DELETE FROM Dream WHERE DreamId = @DreamId = @DreamId";
			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@DreamId", dreamId);
				command.ExecuteNonQuery();
			}
		}
	}


	public List<DreamDTO> GetAllDreams()
	{
		List<DreamDTO> dreams = new List<DreamDTO>();

		using (MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString))
		{
			connection.Open();
			string query = "SELECT * FROM Dream";

			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				using (MySqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						DreamDTO dto = new DreamDTO()
						{
							DreamId = reader.GetInt32("DreamId"),
							DreamName = reader.GetString("DreamName")
						};

						dreams.Add(dto);
					}
				}
			}
		}

		return dreams;
	}

	public List<DreamDTO> GetAllDreamsByDreamerId(int dreamerId)
	{
		List<DreamDTO> dreams = new List<DreamDTO>();

		using (MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString))
		{
			connection.Open();

			string query = "SELECT * FROM Dream WHERE DreamerId = @DreamerId";

			using (MySqlCommand command = new MySqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@DreamerId", dreamerId);

				using (MySqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						DreamDTO dto = new DreamDTO
						{
							DreamId = Convert.ToInt32(reader["DreamId"]),
							DreamName = reader["DreamName"].ToString(),
							DreamerId = Convert.ToInt32(reader["DreamerId"])
						};

						dreams.Add(dto);
					}
				}
			}
		}

		return dreams;
	}

	public List<DreamDTO> GetDreamsByDreamerId(int dreamerId)
	{
		List<DreamDTO> dreams = new List<DreamDTO>();

		using MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString);
		connection.Open();

		string query = "SELECT * FROM Dream WHERE DreamerId = @DreamerId";

		using MySqlCommand command = new MySqlCommand(query, connection);
		command.Parameters.AddWithValue("@DreamerId", dreamerId);

		using MySqlDataReader reader = command.ExecuteReader();

		while (reader.Read())
		{
			DreamDTO dream = new DreamDTO
			{
				DreamId = reader.GetInt32("DreamId"),
				DreamName = reader.GetString("DreamName"),
				DreamerId = reader.GetInt32("DreamerId"),
			};

			dreams.Add(dream);
		}
		connection.Close();

		return dreams;
	}
}
