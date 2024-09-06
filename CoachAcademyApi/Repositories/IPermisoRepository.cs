using CoachAcademyApi.Models;

namespace CoachAcademyApi.Repositories;

public interface IPermisoRepository
{
    Task<IEnumerable<PermisoFormularioDto>> GetPermisosByUsuarioAsync(int userId);
}