using Dapper;
using Entities;
using Npgsql;

namespace Repositories
{
    public class GeneratedStringRepository
    {
        private const string ConnectionString = "Host=localhost;Port=5432;Database=B1Task1;Username=postgres;password=postgres";

        public async Task<List<string>> GetGeneratedStrings()
        {
            var sql = "SELECT * FROM generatedstrings";
            using(var connection = new NpgsqlConnection(ConnectionString))
            {
                List<string> strings = 
                    (await connection.QueryAsync<GeneratedStringEntity>(sql)).Select(str => str.ToString()).ToList(); 
                return strings; 
            }
        }

        public async Task InsertGeneratedStrings(List<string> strings)
        {
            var sql = @"INSERT INTO generatedstrings (date, latin, cyrillic, evennumber, fractional) 
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
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }   
            }
        }

        public async Task<int> GetEvenNumbersSum()
        {
            var sql = @"SELECT SUM(evennumber) FROM generatedstrings"; 
            using(var connection = new NpgsqlConnection(ConnectionString))
            {
                int result = await connection.ExecuteScalarAsync<int>(sql); 
                return result; 
            }
        }

        public async Task<double> GetFratcionalsMedian()
        {
            var sql = "SELECT PERCENTILE_CONT(0.5) WITHIN GROUP (ORDER BY fractional) FROM generatedstrings";
            using(var connection = new NpgsqlConnection(ConnectionString))
            {
                double result = await connection.ExecuteScalarAsync<double>(sql); 
                return result; 
            } 
        }
    }
}