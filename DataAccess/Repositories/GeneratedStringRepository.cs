using Dapper;
using Entities;
using Npgsql;

namespace Repositories
{
    class GeneratedStringRepository
    {
        private const string ConnectionString = "Host=localhost;Port=5432;Database=B1Task1;Username=postgres;password=postgres";

        public async Task<List<String>> GetGeneratedStrings()
        {
            var sql = "SELECT * FROM GeneratedStrings";
            using(var connection = new NpgsqlConnection(ConnectionString))
            {
                List<String> strings = 
                    (await connection.QueryAsync<GeneratedStringEntity>(sql)).Select(str => str.ToString()).ToList(); 
                return strings; 
            }
        }

        public async Task InsertGeneratedStrings(List<String> strings)
        {
            var sql = @"INSERT INTO GeneratedStrings (Date, Latin, Cyrillic, EvenNumber, Fractional) 
                        VALUES(@Date, @Latin, @Cyrillic, @EvenNumber, @Fractional)"; 
            using(var connection = new NpgsqlConnection(ConnectionString))
            {
                for(int i = 0; i < strings.Count; i++)
                {
                    try
                    {
                        var entity = new GeneratedStringEntity(strings[i]);
                        await connection.ExecuteAsync(sql, entity);
                    }
                    catch
                    {
                        continue;
                    }
                }   
            }
        }
    }
}