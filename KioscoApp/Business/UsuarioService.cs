using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using KioscoApp.Models;
using KioscoApp.Data;

namespace KioscoApp.Business
{
    public class UsuarioService
    {
        private UsuarioRepository _repository;

        public UsuarioService()
        {
            _repository = new UsuarioRepository();
        }

        public List<Usuario> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public Usuario Login(string usuario, string contrasena, out string mensajeError)
        {
            mensajeError = string.Empty;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                mensajeError = "Por favor, ingrese usuario y contraseña.";
                return null;
            }

            var user = _repository.ObtenerPorUsuario(usuario);
            if (user == null)
            {
                mensajeError = "Usuario no encontrado.";
                return null;
            }

            bool esValida = BCrypt.Net.BCrypt.Verify(contrasena, user.Contrasena);
            if (!esValida)
            {
                mensajeError = "Contraseña incorrecta.";
                return null;
            }

            return user;
        }

        public ValidationResult GuardarUsuario(Usuario u, bool isEdit, int idLoggedUser)
        {
            var result = ValidarUsuario(u, isEdit);
            if (!result.IsValid) return result;

            if (isEdit)
            {
                if (u.Id == idLoggedUser && u.Rol != "admin")
                {
                    result.AddError("Rol", "No puede quitarse el rol de administrador a sí mismo mientras está en sesión.");
                    return result;
                }

                if (!string.IsNullOrEmpty(u.Contrasena))
                {
                    u.Contrasena = BCrypt.Net.BCrypt.HashPassword(u.Contrasena);
                }
                _repository.Actualizar(u);
            }
            else
            {
                u.Contrasena = BCrypt.Net.BCrypt.HashPassword(u.Contrasena);
                _repository.Insertar(u);
            }

            return result;
        }

        public ValidationResult EliminarUsuario(int idToDelete, int idLoggedUser)
        {
            var result = new ValidationResult();
            if (idToDelete == idLoggedUser)
            {
                result.AddError("General", "No puede eliminar su propio usuario mientras está en sesión.");
                return result;
            }
            
            _repository.Eliminar(idToDelete);
            return result;
        }

        private ValidationResult ValidarUsuario(Usuario u, bool isEdit)
        {
            var result = new ValidationResult();
            int idExcluir = isEdit ? u.Id : 0;

            // Nombre
            if (string.IsNullOrWhiteSpace(u.Nombre)) result.AddError("Nombre", "Requerido");
            else if (u.Nombre.Trim().Length < 3) result.AddError("Nombre", "El nombre debe tener mínimo 3 caracteres");

            // Apellido
            if (string.IsNullOrWhiteSpace(u.Apellido)) result.AddError("Apellido", "Requerido");

            // Usuario
            if (string.IsNullOrWhiteSpace(u.NombreUsuario)) result.AddError("Usuario", "Requerido");
            else if (u.NombreUsuario.Trim().Length < 5) result.AddError("Usuario", "El usuario debe tener mínimo 5 caracteres");
            else if (_repository.ExisteDatoUnico("Usuario", u.NombreUsuario.Trim(), idExcluir)) result.AddError("Usuario", "El usuario ingresado ya existe en el sistema");

            // Contraseña
            if (!isEdit && string.IsNullOrWhiteSpace(u.Contrasena)) result.AddError("Contrasena", "Requerido para usuarios nuevos");

            // Rol
            if (string.IsNullOrWhiteSpace(u.Rol)) result.AddError("Rol", "Requerido");

            // Email
            if (string.IsNullOrWhiteSpace(u.Email)) result.AddError("Email", "Requerido");
            else if (!Regex.IsMatch(u.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) result.AddError("Email", "El formato del email es inválido (ej: correo@gmail.com)");
            else if (_repository.ExisteDatoUnico("Email", u.Email.Trim(), idExcluir)) result.AddError("Email", "El email ingresado ya está en uso por otro usuario");

            // Telefono
            if (string.IsNullOrWhiteSpace(u.Telefono)) result.AddError("Telefono", "Requerido");
            else if (!Regex.IsMatch(u.Telefono, @"^[0-9\+\-\s]+$")) result.AddError("Telefono", "El teléfono solo puede contener números y signos + o -");
            else if (u.Telefono.Replace(" ", "").Replace("-", "").Replace("+", "").Length < 8) result.AddError("Telefono", "El teléfono debe tener mínimo 8 números");
            else if (_repository.ExisteDatoUnico("Telefono", u.Telefono.Trim(), idExcluir)) result.AddError("Telefono", "El teléfono ingresado ya está registrado");

            // Dirección
            if (string.IsNullOrWhiteSpace(u.Calle)) result.AddError("Calle", "Requerido");
            else if (u.Calle.Trim().Length < 5) result.AddError("Calle", "La calle debe tener mínimo 5 caracteres");

            if (string.IsNullOrWhiteSpace(u.Numero)) result.AddError("Numero", "Requerido");

            // Localidad
            if (string.IsNullOrWhiteSpace(u.Provincia)) result.AddError("Provincia", "Requerido");
            if (string.IsNullOrWhiteSpace(u.Ciudad)) result.AddError("Ciudad", "Requerido");
            
            // Sexo
            if (string.IsNullOrWhiteSpace(u.Sexo)) result.AddError("Sexo", "Requerido");

            // Fecha Nacimiento
            if (u.Nacimiento.HasValue && u.Nacimiento.Value.Date >= DateTime.Now.Date) result.AddError("Nacimiento", "La fecha de nacimiento no es válida");

            return result;
        }
    }
}
