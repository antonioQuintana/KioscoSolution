using System;

namespace KioscoApp.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; } // Map to 'Usuario' in DB
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public string Telefono { get; set; }
        public string Sexo { get; set; }
        public DateTime? Nacimiento { get; set; }
    }
}
