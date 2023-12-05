using DataAccessDDO.DatabaseSettings;
using DataAccessDDO.ModelsDTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace DataAccessDDO.Repositories
{
	public class DreamRepo
	{
		private readonly DatabaseSettings.DatabaseSettings _databaseSettings;

		public DreamRepo(DatabaseSettings.DatabaseSettings databaseSettings)
		{
			_databaseSettings = databaseSettings;
		}

		public void CreateDream(DreamDTO dream)
		{
			using (MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString))
			{
				connection.Open();

				string query = "INSERT INTO Dream (DreamName, DreamText, DreamerId) VALUES (@DreamName, @DreamText, @ReadableBy, @DreamerId)";

				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@DreamName", dream.DreamName);
					command.Parameters.AddWithValue("@DreamText", dream.DreamText);
					command.Parameters.AddWithValue("@ReadableBy", dream.ReadableBy);
					command.Parameters.AddWithValue("@DreamerId", dream.DreamerId);

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
					command.Parameters.AddWithValue("@DreamId", dreamId);
					using (MySqlDataReader reader = command.ExecuteReader())
					{
						if (reader.Read())
						{
							return new DreamDTO
							{
								DreamId = Convert.ToInt32(reader["DreamId"]),
								DreamName = reader["DreamName"].ToString(),
								DreamText = reader["DreamText"].ToString(),
								ReadableBy = reader["ReadableBy"].ToString(),
								DreamerId = Convert.ToInt32(reader["DreamerId"])
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
				string query = "UPDATE Dream SET DreamName = @DreamName, DreamText = @DreamText WHERE DreamId = @DreamId";
				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@DreamId", dto.DreamId);
					command.Parameters.AddWithValue("@DreamName", dto.DreamName);
					command.Parameters.AddWithValue("@DreamText", dto.DreamText);
					command.Parameters.AddWithValue("@ReadableBy", dto.ReadableBy);
					command.ExecuteNonQuery();
				}
			}
		}

		public void DeleteDream(int dreamId)
		{
			using (MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString))
			{
				connection.Open();
				string query = "DELETE FROM Dream WHERE DreamId = @DreamId";
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
								DreamId = Convert.ToInt32(reader["DreamId"]),
								DreamName = reader["DreamName"].ToString(),
								DreamText = reader["DreamText"].ToString(),
								ReadableBy = reader["ReadableBy"].ToString(),
								DreamerId = Convert.ToInt32(reader["DreamerId"])
							};

							dreams.Add(dto);
						}
					}
				}
			}

			return dreams;
		}
		public void CreateDreamForDreamer(DreamDTO dream, DreamerDTO dreamer)
		{
			using (MySqlConnection connection = new MySqlConnection(_databaseSettings.ConnectionString))
			{
				connection.Open();

				string query = "INSERT INTO Dream (DreamName, DreamText, DreamerId) VALUES (@DreamName, @DreamText, @ReadableBy, @DreamerId)";

				using (MySqlCommand command = new MySqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@DreamName", dream.DreamName);
					command.Parameters.AddWithValue("@DreamText", dream.DreamText);
					command.Parameters.AddWithValue("@ReadableBy", dream.ReadableBy);
					command.Parameters.AddWithValue("@DreamerId", dream.DreamerId);

					command.ExecuteNonQuery();
				}
			}
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
								DreamText = reader["DreamText"].ToString(),
								ReadableBy = reader["ReadableBy"].ToString(),
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
					DreamId = Convert.ToInt32(reader["DreamId"]),
					DreamName = reader["DreamName"].ToString(),
					DreamText = reader["DreamText"].ToString(),
					ReadableBy = reader["ReadableBy"].ToString(),
					DreamerId = Convert.ToInt32(reader["DreamerId"]),
				};

				dreams.Add(dream);
			}
			connection.Close();

			return dreams;
		}
	}
}
