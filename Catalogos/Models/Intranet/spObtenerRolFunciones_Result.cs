//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Catalogos.Models.Intranet
{
    using System;
    
    public partial class spObtenerRolFunciones_Result
    {
        public int RolID { get; set; }
        public string Rol { get; set; }
        public bool RolActivo { get; set; }
        public int FuncionID { get; set; }
        public Nullable<int> FuncionPadreID { get; set; }
        public string FuncionPadre { get; set; }
        public string Funcion { get; set; }
        public string FuncionDescripcion { get; set; }
        public string Url { get; set; }
        public int TipoFuncionID { get; set; }
        public bool FuncionActivo { get; set; }
        public string TipoFuncion { get; set; }
        public string Metadata { get; set; }
    }
}
