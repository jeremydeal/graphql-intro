using BabysFirstGraphQLService.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BabysFirstGraphQLService.Data.DAL
{
    public interface IUserData
    {
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetUsers();
    }

    public class UserData : IUserData
    {
        private const string connString = "Host=postgres:5432;Username=postgres;Password=password;Database=test";

        public Task<User> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            // Insert some data
            //await using (var cmd = new NpgsqlCommand("INSERT INTO user (some_field) VALUES (@p)", conn))
            //{
            //    cmd.Parameters.AddWithValue("p", "Hello world");
            //    await cmd.ExecuteNonQueryAsync();
            //}

            // Retrieve all rows
            await using (var cmd = new NpgsqlCommand("SELECT UserId, FirstName, LastName, Email, CreatedAt FROM user", conn))
            await using (var reader = await cmd.ExecuteReaderAsync())
                while (await reader.ReadAsync())
                    Console.WriteLine($"{reader["FirstName"].ToString()} {reader["LastName"].ToString()}");

            return new List<User>();
        }
    }
}
