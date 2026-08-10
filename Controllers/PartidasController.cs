using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiLibertadoresHAS.Data;
using CopaHAS.DTOs;
using ApiLibertadoresHAS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLibertadoresHAS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PartidasController : ControllerBase
    {
        private readonly DataContext _context;//using CopaHAS.Data

        public PartidasController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Add(Partida partida)
        {
            try
            {
                await _context.TB_PARTIDAS.AddAsync(partida);
                await _context.SaveChangesAsync();
                return Ok(partida);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }

        [HttpGet("ObterTabela")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var sql = @"
                    SELECT
                        P.Id AS IdPartida,
                        P.DataHora,
                        E.Nome AS Estadio,
                        E.Cidade,

                        TM.Nome AS TimeMandante,
                        PTM.Gols AS GolsMandante,
                        PTM.GolsDecisaoPenaltis AS GolsDecisaoPenaltisMandante,

                        TV.Nome AS TimeVisitante,
                        PTV.Gols AS GolsVisitante,
                        PTV.GolsDecisaoPenaltis AS GolsDecisaoPenaltisVisitante

                    FROM TB_PARTIDAS P
                    INNER JOIN TB_ESTADIOS E ON E.Id = P.EstadioId 
                    INNER JOIN TB_PARTIDAS_TIMES PTM ON PTM.PartidaId = P.Id AND PTM.Mandante = 1
                    INNER JOIN TB_TIMES TM ON TM.Id = PTM.TimeId
                    INNER JOIN TB_PARTIDAS_TIMES PTV ON PTV.PartidaId = P.Id AND PTV.Mandante = 0
                    INNER JOIN TB_TIMES TV ON TV.Id = PTV.TimeId
                    ORDER BY P.DataHora
                ";

                /*var sql = @"SELECT
                    J.id AS IdJogo,
                    J.DataHora,
                    E.Nome AS Estadio,
                    E.Cidade,
                                
                    S1.Nome AS SelecaoMandante,
                    JS1.Gols AS GolsMandante,   
                    JS1.GolsDecisaoPenaltis AS GolsDecisaoPenaltisMandante,             
                    T1.Nome AS TecnicoMandante,
                                
                    S2.Nome AS SelecaoVisitante,
                    JS2.Gols AS GolsVisitante,      
                    JS2.GolsDecisaoPenaltis AS GolsDecisaoPenaltisVisitante,          
                    T2.Nome AS TecnicoVisitante
                FROM TB_JOGOS J 
                INNER JOIN TB_ESTADIOS E ON E.Id = J.EstadioId
                INNER JOIN TB_JOGOS_SELECOES JS1 ON JS1.JogoId = J.Id
                INNER JOIN TB_SELECOES S1 ON S1.Id = JS1.SelecaoId
                LEFT JOIN TB_TECNICOS T1 ON T1.SelecaoId = S1.Id

                INNER JOIN TB_JOGOS_SELECOES JS2 ON JS2.JogoId = J.Id AND JS2.SelecaoId <> JS1.SelecaoId
                INNER JOIN TB_SELECOES S2 ON S2.Id = JS2.SelecaoId
                LEFT JOIN TB_TECNICOS T2 ON T2.SelecaoId = S2.Id
                WHERE S1.Id < S2.Id
                ORDER BY J.Id";*/

                var resultado = await _context.Database.SqlQueryRaw<JogoDTO>(sql)
                    .ToListAsync();

                return Ok(resultado);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message + " - " + ex.InnerException);
            }
        }




    }
}