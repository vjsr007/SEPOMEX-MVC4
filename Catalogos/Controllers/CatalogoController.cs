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
using Catalogos.IntranetService;
using Catalogos.Models.Seguridad;

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
            using (var client = new IntranetService.ServiceContractClient())
            {
                if (Accion == "A")
                {
                    return PartialView("~/Views/Catalogo/Pais/EditarPais.cshtml", new PaisViewModel { Accion = Accion});
                }
                else {
                    var f = new Pais { PaisID = PaisID };

                    var res = client.ObtenerPaises(new ObtenerPaisesReq
                    {
                        FiltroPais = f
                    });

                    var model = new PaisViewModel(res.Paises.FirstOrDefault());

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
            using (var client = new IntranetService.ServiceContractClient())
            {
                if (Accion == "A")
                {
                    return PartialView("~/Views/Catalogo/Estado/EditarEstado.cshtml", new EstadoViewModel { Accion = Accion });
                }
                else
                {
                    var f = new Estado { EstadoID = EstadoID };

                    var res = client.ObtenerEstados(new ObtenerEstadosReq
                    {
                        FiltroEstado = f
                    });

                    var model = new EstadoViewModel(res.Estados.FirstOrDefault());

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

        public JsonResult ObtenerPaises(Pais f)
        {
            using (var client = new IntranetService.ServiceContractClient())
            {
                var res = client.ObtenerPaises(new ObtenerPaisesReq
                {
                    FiltroPais = f
                });

                return Json(res.Paises, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidJsonModel]
        public JsonResult EditarPais(PaisViewModel f)
        {
            var Pais = f.MapToModel(f);
            Pais.UsuarioID = ((UsuarioWeb)Session["UsuarioWeb"]).Usuario.UsuarioID;

            using (var client = new IntranetService.ServiceContractClient())
            {

                var res = client.EditarPais(new EditarPaisesReq
                {
                    FiltroPais = Pais
                });

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidJsonModel]
        public JsonResult AgregarPais(PaisViewModel f)
        {
            var Pais = f.MapToModel(f);
            Pais.UsuarioID = ((UsuarioWeb)Session["UsuarioWeb"]).Usuario.UsuarioID;

            using (var client = new IntranetService.ServiceContractClient())
            {

                var res = client.AgregarPais(new AgregarPaisesReq
                {
                    FiltroPais = Pais
                });

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Estado

        public JsonResult ObtenerEstados(Estado f)
        {
            using (var client = new IntranetService.ServiceContractClient())
            {
                var res = client.ObtenerEstados(new ObtenerEstadosReq
                {
                    FiltroEstado = f
                });

                return Json(res.Estados, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidJsonModel]
        public JsonResult EditarEstado(EstadoViewModel f)
        {
            var Estado = f.MapToModel(f);
            Estado.UsuarioID = ((UsuarioWeb)Session["UsuarioWeb"]).Usuario.UsuarioID;

            using (var client = new IntranetService.ServiceContractClient())
            {
                var res = client.EditarEstado(new EditarEstadosReq
                {
                    FiltroEstado = Estado
                });

                return Json(res.Respuesta, JsonRequestBehavior.AllowGet);
            }
        }

        [ValidJsonModel]
        public JsonResult AgregarEstado(EstadoViewModel f)
        {
            var Estado = f.MapToModel(f);

            Estado.UsuarioID = ((UsuarioWeb)Session["UsuarioWeb"]).Usuario.UsuarioID;

            using (var client = new IntranetService.ServiceContractClient())
            {

                var res = client.AgregarEstado(new AgregarEstadosReq
                {
                    FiltroEstado = Estado
                });

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Municipio

        public JsonResult ObtenerMunicipios(Municipio f)
        {
            using (var client = new IntranetService.ServiceContractClient())
            {
                var res = client.ObtenerMunicipios(new ObtenerMunicipiosReq
                {
                    FiltroMunicipio = f
                });

                return Json(res.Municipios, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditarMunicipio(Municipio f)
        {
            using (var client = new IntranetService.ServiceContractClient())
            {
                var res = client.EditarMunicipio(new EditarMunicipiosReq
                {
                    FiltroMunicipio = f
                });

                return Json(res.Respuesta, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AgregarMunicipio(Municipio f)
        {
            var Municipio = new Municipio();

            Municipio.Activo = f.Activo;
            Municipio.Codigo = f.Codigo;
            Municipio.Nombre = f.Nombre;
            Municipio.EstadoID = f.EstadoID;
            Municipio.UsuarioID = ((UsuarioWeb)Session["UsuarioWeb"]).Usuario.UsuarioID;

            using (var client = new IntranetService.ServiceContractClient())
            {

                var res = client.AgregarMunicipio(new AgregarMunicipiosReq
                {
                    FiltroMunicipio = Municipio
                });

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Ciudad

        public JsonResult ObtenerCiudades(Ciudad f)
        {
            using (var client = new IntranetService.ServiceContractClient())
            {
                var res = client.ObtenerCiudades(new ObtenerCiudadesReq
                {
                    FiltroCiudad = f
                });

                return Json(res.Ciudades, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditarCiudad(Ciudad f)
        {
            using (var client = new IntranetService.ServiceContractClient())
            {
                var res = client.EditarCiudad(new EditarCiudadesReq
                {
                    FiltroCiudad = f
                });

                return Json(res.Respuesta, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AgregarCiudad(Ciudad f)
        {
            var Ciudad = new Ciudad();

            Ciudad.Activo = f.Activo;
            Ciudad.Nombre = f.Nombre;
            Ciudad.EstadoID = f.EstadoID;
            Ciudad.MunicipioID = f.MunicipioID;
            Ciudad.UsuarioID = ((UsuarioWeb)Session["UsuarioWeb"]).Usuario.UsuarioID;

            using (var client = new IntranetService.ServiceContractClient())
            {

                var res = client.AgregarCiudad(new AgregarCiudadesReq
                {
                    FiltroCiudad = Ciudad
                });

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region CodigoPostal

        public JsonResult ObtenerCodigosPostales(CodigoPostal f)
        {
            using (var client = new IntranetService.ServiceContractClient())
            {
                var res = client.ObtenerCodigosPostales(new ObtenerCodigosPostalesReq
                {
                    FiltroCodigoPostal = f
                });

                return Json(res.CodigosPostales, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult EditarCodigoPostal(CodigoPostal f)
        {
            using (var client = new IntranetService.ServiceContractClient())
            {
                var res = client.EditarCodigoPostal(new EditarCodigosPostalesReq
                {
                    FiltroCodigoPostal = f
                });

                return Json(res.Respuesta, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AgregarCodigoPostal(CodigoPostal f)
        {
            var CodigoPostal = new CodigoPostal();

            CodigoPostal.Activo = f.Activo;
            CodigoPostal.codigoPostal = f.codigoPostal;
            CodigoPostal.EstadoID = f.EstadoID;
            CodigoPostal.MunicipioID = f.MunicipioID;
            CodigoPostal.Asentamiento = f.Asentamiento;
            CodigoPostal.TipoAsentamiento = f.TipoAsentamiento;
            CodigoPostal.Zona = f.Zona;
            CodigoPostal.UsuarioID = ((UsuarioWeb)Session["UsuarioWeb"]).Usuario.UsuarioID;

            using (var client = new IntranetService.ServiceContractClient())
            {

                var res = client.AgregarCodigoPostal(new AgregarCodigosPostalesReq
                {
                    FiltroCodigoPostal = CodigoPostal
                });

                return Json(res, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #endregion
    }
}