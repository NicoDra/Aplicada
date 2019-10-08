using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicandoAplicada
{
    public partial class Default : System.Web.UI.Page
    {
        public empleado LogEmpleado
        {
            get
            {
                if (Session["Empleado"] == null)
                    Session["Empleado"] = new empleado();
                return (empleado)Session["Empleado"];
            }
            set
            {
                Session["Empleado"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            
        }
    }
}