using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Catalogos.Models.Intranet;
using System.Web.Mvc;

namespace Catalogos.Models.Catalogos
{
    public class EstadoViewModel
    {
        #region Constructor

        public EstadoViewModel() { }

        public EstadoViewModel(spObtenerEstados_Result p) {
            this.Activo = p.Activo;
            this.Codigo = p.Codigo;
            this.FechaUltimaModificacion = p.FechaUltimaModificacion;
            this.PaisID = p.PaisID;
            this.Nombre = p.Nombre;
            this.EstadoID = p.EstadoID;
            this.UsuarioID = p.UsuarioID;
        }

        #endregion

        #region Metodos

        public Estado MapToModel(EstadoViewModel p) {
            Estado model = new Estado { 
                Activo = p.Activo,
                Codigo = p.Codigo,
                PaisID = (int)p.PaisID,
                FechaUltimaModificacion = p.FechaUltimaModificacion,
                Nombre = p.Nombre,
                EstadoID = (int)p.EstadoID,
                UsuarioID = p.UsuarioID,
            };

            return model;
        }

        public static IEnumerable<SelectListItem> ObtenerPaises()
        {
            using (var client = new IntranetEntities())
            {
                var res = client.spObtenerPaises(null, null, null, null).ToList()
                .Select(p => new SelectListItem 
                { 
                    Selected = false,
                    Text = p.Nombre,
                    Value = p.PaisID.ToString()
                });


                return res;
            }            
        }

        #endregion

        #region Propiedades
        [Display(Name="ID")]
        public int? EstadoID { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es Requerido.")]
        [MinLength(2, ErrorMessage = "La Longitud Minima de {0} es de {1} caracteres.")]
        [StringLength(100, ErrorMessage = "La Longitud Máxima de {0} es de {1} caracteres.")]
        public string Nombre { get; set; }

        [Display(Name = "ID País")]
        [Required(ErrorMessage = "El campo {0} es Requerido.")]
        public int? PaisID { get; set; }

        [Display(Name = "Codigo")]
        [Required(ErrorMessage = "El campo {0} es Requerido.")]
        [StringLength(5,ErrorMessage = "La Longitud Máxima de {0} es de {1} caracteres.")]
        public string Codigo { get; set; }

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