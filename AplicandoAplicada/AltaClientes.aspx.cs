using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
using System.Data.Entity.Validation;


namespace AplicandoAplicada.Clientes
{
    public partial class AltaClientes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void AddClientes()
        {
            using(aplicadaBDEntities2 DBF = new aplicadaBDEntities2()) //entidades
            {
                cliente Cliente = new cliente
                {
                    dni = dnitxt.Value,
                    nombre = txtayn.Value,
                    telefono= tele.Value,
                    email = mail.Value,
                    
                };

                DBF.cliente.Add(Cliente); //agrega un cliente
                DBF.SaveChanges(); //guarda un cliente


            }
        }

        protected void Cargando(object sender, EventArgs e)
        {

            AddClientes();
            dnitxt.Value="";
            txtayn.Value="";
            tele.Value="";
            mail.Value = "";
        }

        

       
       

    }
}