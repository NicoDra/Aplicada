//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AplicandoAplicada
{
    using System;
    using System.Collections.Generic;
    
    public partial class orden
    {
        public orden()
        {
            this.ordenempleado = new HashSet<ordenempleado>();
            this.ordenestado = new HashSet<ordenestado>();
            this.ordenservicio = new HashSet<ordenservicio>();
        }
    
        public int id_orden { get; set; }
        public Nullable<int> id_vehiculo { get; set; }
    
        public virtual vehiculo vehiculo { get; set; }
        public virtual ICollection<ordenempleado> ordenempleado { get; set; }
        public virtual ICollection<ordenestado> ordenestado { get; set; }
        public virtual ICollection<ordenservicio> ordenservicio { get; set; }
    }
}
