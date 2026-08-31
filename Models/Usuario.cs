using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiLibertadoresHAS.Models
{
    public class Usuario
    {
        public ICollection<Jogador> Jogadores {get;set;}
            = new List<Jogador>();
        public int Id { get; set; }

        public string Username { get; set; }

        public byte[]? PasswordHash { get; set; }

        public byte[]? PasswordSalt { get; set; }

        public byte[]? Foto { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public DateTime? DataAcesso { get; set; }

        [NotMapped]
        public string PasswordString { get; set; } = string.Empty;

        public string? Perfil { get; set; }

        public string? Email { get; set; }

        [NotMapped]
        public string Token { get; set; } = string.Empty;
    }
}