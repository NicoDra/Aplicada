﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicandoAplicada
{
    public partial class WebForm3 : System.Web.UI.Page
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

        public orden OrdenActual
        {
            get
            {
                if (Session["Orden"] == null)
                    Session["Orden"] = new orden();
                return (orden)Session["Orden"];
            }
            set
            {
                Session["Orden"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnpasarataller_ServerClick(object sender, EventArgs e)
        {
            if (LogEmpleado.contraseña == txtpwd.Value)
            {
                using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
                {

                    ordenempleado ordenemple = new ordenempleado
                    {
                        id_orden = OrdenActual.id_orden,
                        id_empleado = int.Parse(DropMecanicosDispo.SelectedValue.ToString())

                    };

                    DBF.ordenempleado.Add(ordenemple);
                    DBF.SaveChanges();
                    ordenestado oestado = (from q in DBF.ordenestado where q.id_orden == OrdenActual.id_orden select q).First();
                    oestado.estado = 1;
                    DBF.SaveChanges();
                    OrdenActual = null;
                    Server.Transfer("Default.aspx");

                }
            }
        }
    }
}