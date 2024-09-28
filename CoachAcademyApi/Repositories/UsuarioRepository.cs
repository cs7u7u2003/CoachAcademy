using System.Data;
using Dapper;
using CoachAcademyApi.Models;
using CoachAcademyApi.Utils;

namespace CoachAcademyApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _dbConnection;
        private readonly ErrorLogger _errorLogger;

        public UsuarioRepository(IDbConnection dbConnection, ErrorLogger errorLogger)
        {
            _dbConnection = dbConnection;
            _errorLogger = errorLogger;
        }

        public async Task<UsuarioDto> GetUsuarioAsync(Credenciales credenciales)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", credenciales.Usuario);

                return await _dbConnection.QueryFirstOrDefaultAsync<UsuarioDto>(
                    "[dbo].[sp_GetUsuarioByUserId]",
                    parameters,
                    commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex.Message, ex.StackTrace, nameof(UsuarioRepository), nameof(GetUsuarioAsync));
                return (UsuarioDto)Enumerable.Empty<UsuarioDto>();
            }
        }

        public async Task<bool> InsertarUsuarioAsync(UsuarioDto usuario)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Nombre", usuario.Nombre);
                parameters.Add("@Apellido", usuario.Apellido);
                parameters.Add("@UserId", usuario.UserId);
                parameters.Add("@PasswordHash", usuario.PasswordHash);
                parameters.Add("@PasswordSalt", usuario.PasswordSalt);
                parameters.Add("@Cedula", usuario.Cedula);
                parameters.Add("@PermissionId", usuario.PermissionId);
                parameters.Add("@Comment", usuario.Comment);

                var result = await _dbConnection.ExecuteAsync(
                    "[dbo].[sp_InsertarUsuario]",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                return result > 0;
            }
            catch (Exception ex)
            {
                await _errorLogger.LogErrorAsync(ex.Message, ex.StackTrace, nameof(UsuarioRepository), nameof(InsertarUsuarioAsync));
                return false;
            }
        }
    }
}
