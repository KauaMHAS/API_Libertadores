using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLibertadoresHAS.Data;
using ApiLibertadoresHAS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLibertadoresHAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstadiosController : ControllerBase
    {
        private readonly DataContext _context;

        public EstadiosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")] //Buscar pelo id
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Estadio estadio = await _context.TB_ESTADIOS
                    .FirstOrDefaultAsync(eBusca => eBusca.Id == id);

                return Ok(estadio);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Estadio> lista = await _context.TB_ESTADIOS.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Estadio estadio)
        {
            try
            {                
                await _context.TB_ESTADIOS.AddAsync(estadio);
                await _context.SaveChangesAsync();

                return Ok(estadio.Id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Estadio estadio)
        {
            try
            {                
                _context.TB_ESTADIOS.Update(estadio);
                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Estadio eRemover = await _context.TB_ESTADIOS
                    .FirstOrDefaultAsync(p => p.Id == id);

                _context.TB_ESTADIOS.Remove(eRemover);
                int linhaAfetadas = await _context.SaveChangesAsync();
                return Ok(linhaAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

    }
}