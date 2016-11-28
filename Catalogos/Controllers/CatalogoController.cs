using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Catalogos.Models.Catalogos;
using Catalogos.Models;
using Catalogos.Filters;
using Catalogos.Models.Login;
using System.Text;
using System.ComponentModel;
using System.Web.Util;
using System.Transactions;
using Catalogos.Utils;
using System.Net;
using System.IO;
using Catalogos.Models.Seguridad;
using Catalogos.Models.Intranet;

namespace Catalogos.Controllers
{
    [Seguridad]
    public partial class CatalogoController: BaseController
    {
        #region Views

        public ActionResult Pais()
        {
            return View("~/Views/Catalogo/Pais/Pais.cshtml");
        }

        public ActionResult PaisEditar(int? PaisID, string Accion)
        {
            using (var client = new IntranetEntities())
            {
                if (Accion == "A")
                {
                    return PartialView("~/Views/Catalogo/Pais/EditarPais.cshtml", new PaisViewModel { Accion = Accion});
                }
                else {
                    var res = client.spObtenerPaises(null, null, null, PaisID);

                    var model = new PaisViewModel(res.FirstOrDefault());

                    model.Accion=Accion;

                    return PartialView("~/Views/Catalogo/Pais/EditarPais.cshtml", model);
                }

            }
        }

        public ActionResult Estado()
        {
            return View("~/Views/Catalogo/Estado/Estado.cshtml");
        }

        public ActionResult EstadoEditar(int? EstadoID, string Accion)
        {
            using (var client = new IntranetEntities())
            {
                if (Accion == "A")
                {
                    return PartialView("~/Views/Catalogo/Estado/EditarEstado.cshtml", new EstadoViewModel { Accion = Accion });
                }
                else
                {
                    var res = client.spObtenerEstados(EstadoID, null,null,null,null);

                    var model = new EstadoViewModel(res.FirstOrDefault());

                    model.Accion = Accion;

                    return PartialView("~/Views/Catalogo/Estado/EditarEstado.cshtml", model);
                }

            }
        }

        public ActionResult Municipio()
        {
            return View("~/Views/Catalogo/Municipio/Municipio.cshtml");
        }
        
        public ActionResult Ciudad()
        {
            return View("~/Views/Catalogo/Ciudad/Ciudad.cshtml");
        }

        public ActionResult CodigoPostal()
        {
            return View("~/Views/Catalogo/CodigoPostal/CodigoPostal.cshtml");
        }

        #endregion

        #region Ajax

        #region Pais

        public JsonResult ObtenerPaises(PaisViewModel f)
        {
            using (var client = new IntranetEntities())
            {
                var res = client.spObtenerPaises(f.Activo, f.Nombre, f.Codigo, f.PaisID).ToList();

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidJsonModel]
        public JsonResult EditarPais(PaisViewModel f)
        {
            var Pais = f.MapToModel(f);
            Pais.UsuarioID = ((UsuarioWeb)Session["UsuarioWeb"]).Usuario.UsuarioID;

            using (var client = new IntranetEntities())
            {

                var res = client.spEditarPais(Pais.PaisID, Pais.Nombre, Pais.Codigo, Pais.Moneda, Pais.CodMoneda, Pais.UsuarioID, Pais.Activo);

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidJsonModel]
        public JsonResult AgregarPais(PaisViewModel f)
        {
            var Pais = f.MapToModel(f);
            Pais.UsuarioID = ((UsuarioWeb)Session["UsuarioWeb"]).Usuario.UsuarioID;

            using (var client = new IntranetEntities())
            {

                var res = client.spAgregarPais(Pais.Nombre, Pais.Codigo, Pais.Moneda, Pais.CodMoneda, Pais.UsuarioID);

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Estado

        public JsonResult ObtenerEstados(Estado f)
        {
            using (var client = new IntranetEntities())
            {
                var res = client.spObtenerEstados(f.EstadoID, f.Codigo, f.Nombre, f.PaisID, f.Activo).ToList();

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidJsonModel]
        public JsonResult EditarEstado(EstadoViewModel f)
        {
            var Estado = f.MapToModel(f);
            Estado.UsuarioID = ((UsuarioWeb)Session["UsuarioWeb"]).Usuario.UsuarioID;

            using (var client = new IntranetEntities())
            {
                var res = client.spEditarEstados(Estado.EstadoID, Estado.Codigo, Estado.Nombre, Estado.PaisID, Estado.UsuarioID, Estado.Activo);

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidJsonModel]
        public JsonResult AgregarEstado(EstadoViewModel f)
        {
            var Estado = f.MapToModel(f);

            Estado.UsuarioID = ((UsuarioWeb)Session["UsuarioWeb"]).Usuario.UsuarioID;

            using (var client = new IntranetEntities())
            {

                var res = client.spAgregarEstados(Estado.Codigo, Estado.Nombre, Estado.PaisID, Estado.UsuarioID, Estado.Activo);

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Municipio

        public JsonResult ObtenerMunicipios(Municipio f)
        {
            using (var client = new IntranetEntities())
            {
                var res = client.spObtenerMunicipios(f.MunicipioID,f.Codigo,f.Nombre,f.EstadoID, f.Activo, null).ToList();

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditarMunicipio(Municipio f)
        {
            using (var client = new IntranetEntities())
            {
                var res = client.spEditarMunicipios(f.MunicipioID,f.Codigo, f.Nombre, f.EstadoID, f.Activo, f.UsuarioID);

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AgregarMunicipio(Municipio f)
        {
            f.UsuarioID = ((UsuarioWeb)Session["UsuarioWeb"]).Usuario.UsuarioID;

            using (var client = new IntranetEntities())
            {

                var res = client.spAgregarMunicipios(f.Codigo, f.Nombre, f.EstadoID, f.Activo, f.UsuarioID);

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Ciudad

        public JsonResult ObtenerCiudades(Ciudad f)
        {
            using (var client = new IntranetEntities())
            {
                var res = client.spObtenerCiudades(f.CiudadID, f.MunicipioID, f.Nombre, f.EstadoID, f.Activo, null).ToList();

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditarCiudad(Ciudad f)
        {
            using (var client = new IntranetEntities())
            {
                var res = client.spEditarCiudades(f.CiudadID, f.MunicipioID, f.Nombre, f.EstadoID, f.Activo, f.UsuarioID);

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AgregarCiudad(Ciudad f)
        {
            f.UsuarioID = ((UsuarioWeb)Session["UsuarioWeb"]).Usuario.UsuarioID;

            using (var client = new IntranetEntities())
            {

                var res = client.spAgregarCiudades(f.MunicipioID, f.Nombre, f.EstadoID, f.Activo, f.UsuarioID);

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region CodigoPostal

        public JsonResult ObtenerCodigosPostales(CodigoPostal f)
        {
            using (var client = new IntranetEntities())
            {
                var res = client.spObtenerCodigosPostales(f.CodigoPostal1, f.TipoAsentamiento, f.Asentamiento, f.Zona, f.MunicipioID, f.CiudadID, null, f.Activo, null).ToList();

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditarCodigoPostal(CodigoPostal f)
        {
            using (var client = new IntranetEntities())
            {
                var res = client.spEditarCodigosPostales(f.CodigoPostalID, f.CodigoPostal1, f.TipoAsentamiento, f.Asentamiento, f.Zona, f.MunicipioID, f.CiudadID, f.Activo, f.UsuarioID);

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AgregarCodigoPostal(CodigoPostal f)
        {
            f.UsuarioID = ((UsuarioWeb)Session["UsuarioWeb"]).Usuario.UsuarioID;

            using (var client = new IntranetEntities())
            {

                var res = client.spAgregarCodigosPostales(f.CodigoPostal1, f.TipoAsentamiento, f.Asentamiento, f.Zona, f.MunicipioID, f.CiudadID, f.Activo, f.UsuarioID);

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #endregion
    }
}