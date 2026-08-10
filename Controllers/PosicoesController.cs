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
    public class PosicoesController : ControllerBase
    {
        private readonly DataContext _context;

        public PosicoesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Posicao posicao = await _context.TB_POSICOES
                    .FirstOrDefaultAsync(eBusca => eBusca.Id == id);

                return Ok(posicao);
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
                List<Posicao> lista = await _context.TB_POSICOES.ToListAsync();

                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }
    }
}