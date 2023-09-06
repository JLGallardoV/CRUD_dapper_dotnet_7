using Dapper;
using UscastoresProject.Models;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;

namespace UscastoresProject.Data;
public class UscastoresRepository
{
    private readonly IDbConnection _dbConnection;

    public UscastoresRepository(IOptions<ConnectionString> connectionString)
    {
        _dbConnection = new MySqlConnection(connectionString.Value.ProjectConnection);
    }

    public async Task<List<Uscastores>> GetAll()
    {
        try
        {
            Console.WriteLine("INSIDE METHOD");
            _dbConnection?.Open();

            string query = @"
                SELECT * 
                FROM personal.uscastores
            ";
            
            var projects = await _dbConnection.QueryAsync<Uscastores>(query);
            Console.WriteLine($" RESPUESTA {projects}");
            return projects.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine($"CATCH ERROR {e}");
            return new List<Uscastores>();
        }
        finally
        {
            _dbConnection?.Close();
        }
    }


    public async Task<List<Uscastores>> GetById(int idpersonal)
    {
        try
        {
            Console.WriteLine("INSIDE METHOD");
            _dbConnection?.Open();

            string query = @"
                SELECT * 
                FROM personal.uscastores
                WHERE idpersonal = @Id
            ";
            
            var projects = await _dbConnection.QueryAsync<Uscastores>(query, new {Id = idpersonal});
            Console.WriteLine($" RESPUESTA {projects}");
            return projects.ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine($"CATCH ERROR {e}");
            return new List<Uscastores>();
        }
        finally
        {
            _dbConnection?.Close();
        }
    }

    public async Task<bool> Create(Uscastores colaborador)
    {
        try
        {
            _dbConnection?.Open();

            string query = @"INSERT INTO personal.uscastores(idpersonal, nombre, password, pregunta, respuesta, ultimopassword) 
                             VALUES(@IdPersonal, @Nombre, @Password, @Pregunta, @Respuesta, @UltimoPassword)";

            await _dbConnection.ExecuteAsync(query, colaborador);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            _dbConnection?.Close();
        }
    }

    public async Task<bool> Update(Uscastores colaborador)
    {
        try
        {
            _dbConnection?.Open();

            string query = @"UPDATE personal.uscastores
                             SET nombre = @Nombre
                             WHERE idpersonal = @IdPersonal";

            await _dbConnection.ExecuteAsync(query, colaborador);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            _dbConnection?.Close();
        }
    }

}