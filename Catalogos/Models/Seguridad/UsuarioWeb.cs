using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Catalogos.IntranetService;

namespace Catalogos.Models.Seguridad
{
    public class UsuarioWeb
    {
        public Usuario Usuario { get; set; }
        public UsuarioFunciones UsuarioFunciones { get; set; }
    }
}