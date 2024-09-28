using System.Data;
using CoachAcademyApi.Models;
using Dapper;
using CoachAcademyApi.Utils;

namespace CoachAcademyApi.Repositories;

public class PermisoRepository : IPermisoRepository
{
    private readonly IDbConnection _dbConnection;
    private readonly ErrorLogger _errorLogger;

    public PermisoRepository(IDbConnection dbConnection, ErrorLogger errorLogger)
    {
        _dbConnection = dbConnection;
        _errorLogger = errorLogger;
    }

    public async Task<IEnumerable<PermisoFormularioDto>> GetPermisosByUsuarioAsync(int userId)
    {
        try
        {
            var parameters = new { UsuarioId = userId };
            var permisos = await _dbConnection.QueryAsync<PermisoFormularioDto>(
                "spGetPermisosByUsuario", parameters, commandType: CommandType.StoredProcedure);
            return permisos;
        }
        catch (Exception ex)
        {
            await _errorLogger.LogErrorAsync(ex.Message, ex.StackTrace, nameof(PermisoRepository), nameof(GetPermisosByUsuarioAsync));
            return Enumerable.Empty<PermisoFormularioDto>();

        }
    }
}