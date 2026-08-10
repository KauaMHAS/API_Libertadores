using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibertadoresHAS.Models
{
    public class Estadio
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Cidade { get; set; } = string.Empty;

        public string Pais { get; set; } = string.Empty;

        public int Capacidade { get; set; }

        public ICollection<Partida> Partidas { get; set; } = new List<Partida>();
    }
}