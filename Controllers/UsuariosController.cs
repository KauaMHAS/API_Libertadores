using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLibertadoresHAS.Data;
using ApiLibertadoresHAS.Models;
using ApiLibertadoresHAS.Utils;

namespace ApiLibertadoresHAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly DataContext _context;

        public UsuariosController(DataContext context)
        {
            _context = context;
        }

        private async Task<bool> UsuarioExistente(string username)
        {
            if (await _context.TB_USUARIOS.AnyAsync(
                x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }

            return false;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUsuario(Usuario user)
        {
            try
            {
                if (await UsuarioExistente(user.Username))
                    throw new System.Exception("Nome de usuário já existe");

                Criptografia.CriarPasswordHah(
                    user.PasswordString,
                    out byte[] hash,
                    out byte[] salt);

                user.PasswordString = string.Empty;
                user.PasswordHash = hash;
                user.PasswordSalt = salt;

                await _context.TB_USUARIOS.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok(user.Id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPost("Autenticar")]
        public async Task<IActionResult> AutenticarUsuario(Usuario credenciais)
        {
            try
            {
                Usuario? usuario = await _context.TB_USUARIOS
                    .FirstOrDefaultAsync(
                        x => x.Username.ToLower()
                            .Equals(credenciais.Username.ToLower()));

                if (usuario == null)
                    throw new System.Exception("Usuário não encontrado.");

                if (!Criptografia.VerificarPasswordHash(
                    credenciais.PasswordString,
                    usuario.PasswordHash,
                    usuario.PasswordSalt))
                {
                    throw new System.Exception("Senha incorreta.");
                }

                usuario.PasswordString = string.Empty;
                usuario.PasswordHash = null;
                usuario.PasswordSalt = null;

                return Ok(usuario);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                List<Usuario> lista =
                    await _context.TB_USUARIOS.ToListAsync();

                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }
    }
}