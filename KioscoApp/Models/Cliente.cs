using System;

namespace KioscoApp.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public DateTime? Nacimiento { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }
        
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}
