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
    public class RodadasController : ControllerBase
    {
        private readonly DataContext _context;

        public RodadasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Rodada rodada = await _context.TB_RODADAS
                    .FirstOrDefaultAsync(eBusca => eBusca.Id == id);

                return Ok(rodada);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Rodada> lista = await _context.TB_RODADAS.ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }
    }
}