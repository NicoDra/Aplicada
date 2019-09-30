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
        public List<servicio> Lservi
        {
            get
            {
                if (Session["ListaServicios"] == null)
                    Session["ListaServicios"] = new List<servicio>();
                return (List<servicio>)Session["ListaServicios"];
            }
            set
            {
                Session["ListaServicios"] = value;
            }
        }
      
       

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
               DivAAuto.Visible = false;
               ServiciosDiv.Visible = false;
               DropMecanicosDispo.Visible = false;
               Lservi.Clear();
           
               

            }
            

        }

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {Buscadores bus = new Buscadores();
            string a= Patente.Value;
            
            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {
                
                vehiculo objvehiculo = bus.buscarvehiculo(a);
                if (a!="")
                {
                    Anno.Disabled = true;
                cliente objcliente = bus.ocliente(objvehiculo);
                modelo objmodelo = bus.buscarmodelo(objvehiculo);
                Anno.Value = objvehiculo.annio;
                Modelo.Value = objmodelo.nombre;
               
                    
                Cliente.Value = objcliente.nombre;
                Direccion.Value = objcliente.telefono;
                Mail.Value = objcliente.email;

                }
                else
                {
                    DivAAuto.Visible = true;
                    VehiculoError.Visible = true;
                   
                }

                

                
                                                     
            }
        
        
        }

        public void VerGrid()
        {
            List<servicio> Lservicios;
            


            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {

                
        
                    IQueryable<servicio> lista = (from q in DBF.servicio select q);
                    Lservicios = lista.ToList();
                Buscadores bus = new Buscadores();
                    string a = Patente.Value;
                vehiculo objvehiculo = bus.buscarvehiculo(a);
                modelo objmodelo = bus.buscarmodelo(objvehiculo);


                Lservicios = Lservicios.FindAll(ser => ser.id_modelo == objmodelo.id_modelo);

                    GridView1.DataSource = Lservicios;
                    GridView1.DataBind();
               

                

            }


        }
            protected void CargarServicios(object sender, EventArgs e){

                ServiciosDiv.Visible = true;
                DropMecanicosDispo.Visible = true;
                
                
                VerGrid();
                

            }

            protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
                {
                    string id = GridView1.SelectedRow.Cells[1].Text;
                    string detalle = GridView1.SelectedRow.Cells[2].Text;
                    string precio = GridView1.SelectedRow.Cells[3].Text;
                List<serviciostock> Lserstock = Lserviciostock(id);
                Buscadores bus = new Buscadores();
                List<stock> Nstock = Lstockuso(Lserstock);
                foreach(stock ostock in Nstock){
                    if (int.Parse(ostock.cantidad) <= int.Parse(ostock.minimo))
                    {
                        StockError.Visible = true;

                    }

                }


                
                


                
                
                servicio Servi = new servicio();
                Servi.id_servicios = int.Parse(id);
                Servi.detalle = detalle;
                Servi.precio = precio;
                
                
                Lservi.Add(Servi);
                GridView2.DataSource = Lservi;
                GridView2.DataBind();
            }

            protected void EliminarGrid2(object sender, EventArgs e)
            {


                string id = GridView2.SelectedRow.Cells[1].Text;
                string detalle = GridView2.SelectedRow.Cells[2].Text;
                string precio = GridView2.SelectedRow.Cells[3].Text;

                servicio Servi = new servicio();
                Servi.id_servicios = int.Parse(id);
                Servi.detalle = detalle;
                Servi.precio = precio;

                Servi = Lservi.Find(servicio => servicio.id_servicios == Servi.id_servicios);
                Lservi.Remove(Servi);
                GridView2.DataSource = Lservi;
                GridView2.DataBind();
            }


            public List<serviciostock> Lserviciostock(String id )
            {
                Buscadores bus = new Buscadores();
                List<serviciostock> Lserviciostocks =bus.Lstockservi();
                List<serviciostock> NLserviciostock = new List<serviciostock>();
                NLserviciostock = Lserviciostocks.FindAll(s => s.id_servicio == int.Parse(id));

                return NLserviciostock;

            }

            public List<stock> Lstockuso(List<serviciostock> Lstockservi)
            {
                Buscadores bus = new Buscadores();
                List<stock> Lstock = bus.Lstock();
                List<stock> stockactivo = new List<stock>();
                foreach (stock Stock in Lstock)
                {
                    foreach (serviciostock Servistock in Lstockservi)
                    {
                        if (Stock.id_stock==Servistock.id_stock)
                        {
                            stockactivo.Add(Stock);

                        }

                    }
                }

                return stockactivo;

            }
        
        

        

        
    }
}