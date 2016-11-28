using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Catalogos.Models.Intranet;

namespace Catalogos.Models.Catalogos
{
    public class PaisViewModel
    {
        #region Constructor

        public PaisViewModel() { }

        public PaisViewModel(spObtenerPaises_Result p) {
            this.Activo = p.Activo;
            this.Codigo = p.Codigo;
            this.CodMoneda = p.CodMoneda;
            this.FechaUltimaModificacion = p.FechaUltimaModificacion;
            this.Moneda = p.Moneda;
            this.Nombre = p.Nombre;
            this.PaisID = p.PaisID;
            this.UsuarioID = p.UsuarioID;
        }

        #endregion

        #region Metodos

        public Pais MapToModel(PaisViewModel p) {
            Pais model = new Pais { 
                Activo = p.Activo,
                Codigo = p.Codigo,
                CodMoneda = p.CodMoneda,
                FechaUltimaModificacion = p.FechaUltimaModificacion,
                Moneda = p.Moneda,
                Nombre = p.Nombre,
                PaisID = (int)p.PaisID,
                UsuarioID = p.UsuarioID,
            };

            return model;
        }

        #endregion

        #region Propiedades
        [Display(Name="ID")]
        public int? PaisID { get; set; }

        [Display(Name = "País")]
        [Required(ErrorMessage = "El campo {0} es Requerido.")]
        [MinLength(2, ErrorMessage = "La Longitud Minima de {0} es de {1} caracteres.")]
        [StringLength(100, ErrorMessage = "La Longitud Máxima de {0} es de {1} caracteres.")]
        public string Nombre { get; set; }

        [Display(Name = "Codigo")]
        [Required(ErrorMessage = "El campo {0} es Requerido.")]
        [StringLength(2,ErrorMessage = "La Longitud Máxima de {0} es de {1} caracteres.")]
        public string Codigo { get; set; }

        [Display(Name = "Moneda")]
        [Required(ErrorMessage = "El campo {0} es Requerido.")]
        [StringLength(2, ErrorMessage = "La Longitud Máxima de {0} es de {1} caracteres.")]
        public string Moneda { get; set; }

        [Display(Name = "Codigo Moneda")]
        [Required(ErrorMessage = "El campo {0} es Requerido.")]
        [StringLength(2, ErrorMessage = "La Longitud Máxima de {0} es de {1} caracteres.")]
        public string CodMoneda { get; set; }

        [Display(Name = "Ultima Modificación")]
        [DataType(DataType.DateTime, ErrorMessage = "El campo {0} tiene formato inválido.")]
        public DateTime? FechaUltimaModificacion { get; set; }

        [Display(Name = "Usuario")]
        public int? UsuarioID { get; set; }

        public bool? Activo { get; set; }

        public string Accion { get; set; }
        #endregion
    }
}