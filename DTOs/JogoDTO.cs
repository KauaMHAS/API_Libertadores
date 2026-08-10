using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopaHAS.DTOs
{
    public class JogoDTO
    {
        public int IdJogo { get; set; }
        public DateTime DataHora { get; set; }
        public string Estadio { get; set; } = null!;
        public string Cidade { get; set; } = null!;
        public string TimeMandante { get; set; } = null!;
        public int GolsMandante { get; set; }
        public int GolsDecisaoPenaltisMandante { get; set; }        
        public string TimeVisitante { get; set; } = null!;
        public int GolsVisitante { get; set; }
        public int GolsDecisaoPenaltisVisitante { get; set; }
        
    }
}

