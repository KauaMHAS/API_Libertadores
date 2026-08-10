using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLibertadoresHAS.Data;
using ApiLibertadoresHAS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CopaHAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimesController : ControllerBase
    {
        private readonly DataContext _context;//using CopaHAS.Data

        public TimesController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")] //Buscar pelo id
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Time time = await _context.TB_TIMES
                    .FirstOrDefaultAsync(eBusca => eBusca.Id == id);//using CopaHAS.Models
                    //using Microsoft.EntityFrameworkCore

                return Ok(time);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        //Próximos métodos aqui.

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Time> lista = await _context.TB_TIMES.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

    }
}