using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using Catalogos.Models;
using System.ComponentModel;

namespace Catalogos.Models.Login
{
    public class UsuarioViewModel
    {
        public int UsuarioID { get; set; }

        [Required(ErrorMessage = "El Nombre es Requerido.")]
        [StringLength(50, ErrorMessage = "Longitud máxima de {0} debe de ser de {1} caracteres.")]
        [DisplayName("Nombre")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "La Contraseña es Requerida.")]
        [StringLength(50, ErrorMessage = "Longitud máxima de {0} debe de ser de {1} caracteres.")]
        [DisplayName("Password")]
        public string password { get; set; }

        public Boolean Activo { get; set; }
    }
}
