using ExemploApiCatalogoTurístico.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ExemploApiCatalogoTurístico.Repositories
{
    public class TurísticoSqlServerRepository : IJogoRepository
    {
        private readonly SqlConnection sqlConnection;

        public TurísticoSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Turístico>> Obter(int pagina, int quantidade)
        {
            var turístico = new List<Turístico>();

            var comando = $"select * from Turístico order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                turístico.Add(new Turístico
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Agencia"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return turístico;
        }

        public async Task<Turístico> Obter(Guid id)
        {
            Turístico turístico = null;

            var comando = $"select * from Turístico where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                turístico = new Turístico
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Agencia = (string)sqlDataReader["Agencia"],
                    Preco = (double)sqlDataReader["Preco"]
                };
            }

            await sqlConnection.CloseAsync();

            return turístico;
        }

        public async Task<List<Turístico>> Obter(string nome, string agencia)
        {
            var turístico = new List<Turístico>();

            var comando = $"select * from Turístico where Nome = '{nome}' and Agencia = '{agencia}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                turístico.Add(new Turístico
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Nome = (string)sqlDataReader["Nome"],
                    Agencia = (string)sqlDataReader["Agencia"],
                    Preco = (double)sqlDataReader["Preco"]
                });
            }

            await sqlConnection.CloseAsync();

            return turístico;
        }

        public async Task Inserir(Turístico turístico)
        {
            var comando = $"insert Turístico (Id, Nome, Agencia, Preco) values ('{turístico.Id}', '{turístico.Nome}', '{turístico.Agencia}', {turístico.Preco.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Atualizar(Turístico turístico)
        {
            var comando = $"update Turístico set Nome = '{turístico.Nome}', Agencia = '{turístico.Agencia}', Preco = {turístico.Preco.ToString().Replace(",", ".")} where Id = '{turístico.Id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Turístico where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }
    }
}
