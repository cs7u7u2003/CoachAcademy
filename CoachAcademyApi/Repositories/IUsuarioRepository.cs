using CoachAcademyApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoachAcademyApi.Repositories;

public interface IUsuarioRepository
{
    Task<UsuarioDto> GetUsuarioAsync(Credenciales credenciales);
    Task<bool> InsertarUsuarioAsync(UsuarioDto usuario);
}