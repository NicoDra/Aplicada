using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Diagnostics;

namespace AplicandoAplicada
{
    public partial class AltaDetalle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               theDiv.Visible = false;

            }
            

        }

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {Buscadores bus = new Buscadores();
            string a= Patente.Value;
            
            using (aplicadaBDEntities DBF = new aplicadaBDEntities())
            {
                
                vehiculo objvehiculo = bus.buscarvehiculo(a);
                if (objvehiculo!=null)
                {
                cliente objcliente = bus.ocliente(objvehiculo);
                modelo objmodelo = bus.buscarmodelo(objvehiculo);
                Anno.Value = objmodelo.annio;
                Modelo.Value = objmodelo.nombre;
                Cliente.Value = objcliente.nombre;
                Direccion.Value = objcliente.telefono;
                Mail.Value = objcliente.email;

                }
                else
                {
                    theDiv.Visible = true;
                    //if (MessageBox.Show("No se encontro dicha patente", "Abrir alta cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    Response.Redirect("AltaVehiculo.aspx");
                        
                    //}
                }
                

                
                                                     
            }
        }

        
    }
}