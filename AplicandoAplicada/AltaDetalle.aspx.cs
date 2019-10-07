﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;



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
            
            if (!IsPostBack)
            {
                if (LogEmpleado.id_tipo != 2)
                {
                    Server.Transfer("Default.aspx");
                }
               
           
               

            }
            

        }

       

        protected void Unnamed_ServerClick1(object sender, EventArgs e)
        {
            Buscadores bus = new Buscadores();
            string a = txtpatente.Value;

            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {

                vehiculo objvehiculo = bus.buscarvehiculo(a);
                if (objvehiculo != null)
                {
                    RecargarAuto();
                    VerGrid();
                    A1.Visible = true;
                    btnServicios.Visible = true;

                }
                else
                {
                    int b = txtpatente.Value.Length;
                    if (b>=6)
                    {
                        Dmodelo.Visible = true;
                        modelito.Visible = true;
                        txtmodelo.Visible = false;
                        txtmarca.Visible = false;
                        btnAgregarcliente.Visible = true;
                        txtaño.Disabled = false;
                        btnGuardar.Visible = Visible;

                    }
                    

                    
                    


                }
            }
        }

        protected void BuscarCliente(object sender, EventArgs e)
        {
            Buscadores bus = new Buscadores();
            cliente ocliente = bus.oclientedni(txtdni.Value);
            vehiculo ovehiculo = bus.buscarvehiculo(txtpatente.Value);
            cliente oclientes = new cliente();
            if (ovehiculo != null)
            {
                oclientes = bus.ocliente(ovehiculo);
            }
            else
            {
                oclientes.id = 0;
                
            }
            

            if ((ocliente==null)||(ocliente.dni != oclientes.dni))
            {
                using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
                {

                    if (ocliente != null)
                    {
                        string[] separadas;
                        separadas = ocliente.nombre.Split(' ');
                        txtdni.Value = ocliente.dni;
                        txtapellido.Value = separadas[0];
                        txtnombre.Value = separadas[1];
                        txttelefono.Value = ocliente.telefono;
                        txtemail.Value = ocliente.email;
                        txtapellido.Disabled = true;
                        txtnombre.Disabled = true;
                        txttelefono.Disabled = true;
                        txtemail.Disabled = true;

                        btnGuardar.Visible = true;

                    }
                    else
                    {
                        txtapellido.Disabled = false;
                        txtnombre.Disabled = false;
                        txttelefono.Disabled = false;
                        txtemail.Disabled = false;
                        txtapellido.Value = "";
                        txtnombre.Value = "";
                        txttelefono.Value = "";
                        txtemail.Value = "";
                        txtpatente.Disabled = true;
                        btnGuardar.Visible = true;

                    }

                }

            }
    
        }

        protected void CargaryAvanzar(object sender, EventArgs e)
        {
            Buscadores bus = new Buscadores();
            cliente ocliente = bus.oclientedni(txtdni.Value);
            vehiculo ovehiculo = bus.buscarvehiculo(txtpatente.Value);
           

            if (ovehiculo == null)
            {
                GuardarVehiculo();

            }
            ovehiculo = bus.buscarvehiculo(txtpatente.Value);
            cliente oclientes = bus.ocliente(ovehiculo);

            if ((ovehiculo.id_cliente==null)||( ocliente==null) ||(ocliente.dni != oclientes.dni))
            {
                GuardarCambiodecliente();


            }
            EstadoOriginal();
            VerGrid();
            btnServicios.Visible = true;

        }

        private void GuardarVehiculo()
        {
            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {
                vehiculo ovehiculo = new vehiculo
                {
                    patente = txtpatente.Value,
                    id_modelo = int.Parse(Dmodelo.SelectedValue),
                    annio=txtaño.Value,

                };
                DBF.vehiculo.Add(ovehiculo);
                DBF.SaveChanges();
            }
            
        }

        private void GuardarCambiodecliente()
        {
            Buscadores bus = new Buscadores();
            vehiculo ovehiculo = bus.buscarvehiculo(txtpatente.Value);
            cliente ocliente = bus.ocliente(ovehiculo);
            cliente oclientes = bus.oclientedni(txtdni.Value);
            btnGuardar.Visible = false;
            if ((ocliente != null) && (oclientes!= null))
            {
                    
                        ocliente = bus.oclientedni(txtdni.Value);
                        using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
                        {
                            vehiculo oVehiculo = (from q in DBF.vehiculo where q.id_vehiculo == ovehiculo.id_vehiculo select q).First();
                            oVehiculo.id_cliente = ocliente.id;
                            DBF.SaveChanges();
                        }
                    }
                    else
                    {
                        
                            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
                            {
                                cliente ncliente = new cliente
                                {
                                    dni = txtdni.Value,
                                    nombre = txtapellido.Value + " " + txtnombre.Value,
                                    telefono = txttelefono.Value,
                                    email = txtemail.Value,

                                };

                                DBF.cliente.Add(ncliente);
                                DBF.SaveChanges();

                            }
                            ocliente = bus.oclientedni(txtdni.Value);
                            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
                            {
                                vehiculo oVehiculo = (from q in DBF.vehiculo where q.id_vehiculo == ovehiculo.id_vehiculo select q).First();
                                oVehiculo.id_cliente = ocliente.id;
                                DBF.SaveChanges();
                            }

            }
           
   
        }

        protected void Avanzar(object sender, EventArgs e)
        {
            if ((txtpatente.Value != "") && (txtdni.Value != ""))
            {

            
            Buscadores bus = new Buscadores();
            cliente ocliente = bus.oclientedni(txtdni.Value);
            vehiculo ovehiculo = bus.buscarvehiculo(txtpatente.Value);
            cliente oclientes = bus.ocliente(ovehiculo);

            if ((ovehiculo != null)&&(ovehiculo.id_cliente != null) && (ocliente != null) && (ovehiculo.id_cliente == ocliente.id)&&(Lservi.Count<=5)&& (Lservi.Count>=1))
            {
                CargarOrden();
                Server.Transfer("DetalleTaller.aspx");

            }
            else
            {
                

            }

            }

        }

        private void CargarOrden()
        {
            Buscadores bus = new Buscadores();
            vehiculo ovehiculo = bus.buscarvehiculo(txtpatente.Value);
            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {
                orden oorden = new orden
                {
                    id_vehiculo = ovehiculo.id_vehiculo,
        
                };

                DBF.orden.Add(oorden);
                DBF.SaveChanges();
                ordenestado oOrdenEstado = new ordenestado
                {
                    id_orden = oorden.id_orden,
                    estado = 0

                };
                DBF.ordenestado.Add(oOrdenEstado);
                DBF.SaveChanges();
                foreach (servicio l in Lservi)
                {
                    ordenservicio ooServicio = new ordenservicio
                    {
                        id_orden = oorden.id_orden,
                        id_servicio = l.id_servicios

                    };

                    DBF.ordenservicio.Add(ooServicio);
                    DBF.SaveChanges();
                }
                OrdenActual = oorden;

               
            }
            
        }


        public void RecargarAuto()
        {
            Buscadores bus = new Buscadores();
            string a = txtpatente.Value;
            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {

                vehiculo objvehiculo = bus.buscarvehiculo(a);
                if (objvehiculo != null)
                {
                    txtpatente.Disabled = true;
                    cliente objcliente = bus.ocliente(objvehiculo);
                    modelo objmodelo = bus.buscarmodelo(objvehiculo);
                    txtaño.Value = objvehiculo.annio;
                    txtmodelo.Value = objmodelo.nombre;
                    txtmarca.Value = bus.buscarmarca(objmodelo).nombre.ToString();

                    string[] separadas;
                    if (objcliente.dni != null)
                    {
                        separadas = objcliente.nombre.Split(' ');
                        txtdni.Value = objcliente.dni;
                        txtapellido.Value = separadas[0];
                        txtnombre.Value = separadas[1];
                        txttelefono.Value = objcliente.telefono;
                        txtemail.Value = objcliente.email;
                    }



                }
            }
        }


        public void EstadoOriginal()
        {


            txtmodelo.Visible = true;
            txtmarca.Visible = true;
            modelito.Visible = false;
            Dmodelo.Visible = false;
            txtapellido.Disabled = true;
            txtnombre.Disabled = true;
            txttelefono.Disabled = true;
            txtemail.Disabled = true;
            txtaño.Disabled = true;

            RecargarAuto();

        }
        public void VerGrid()
        {
            List<servicio> Lservicios;

            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {
                IQueryable<servicio> lista = (from q in DBF.servicio select q);
                Lservicios = lista.ToList();
                Buscadores bus = new Buscadores();
                string a = txtpatente.Value;
                vehiculo objvehiculo = bus.buscarvehiculo(a);
                modelo objmodelo = bus.buscarmodelo(objvehiculo);


                Lservicios = Lservicios.FindAll(ser => ser.id_modelo == objmodelo.id_modelo);

                GridView1.DataSource = Lservicios;
                GridView1.DataBind();
               
            }


        }


        protected void CargarServicios(object sender, EventArgs e){
            List<servicio> Lse = new List<servicio>();
            DataTable dt = new DataTable();
            Buscadores bus = new Buscadores();
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Detalle"), new DataColumn("Precio") });
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    System.Web.UI.WebControls.CheckBox chkRow = (row.Cells[3].FindControl("chkRow") as System.Web.UI.WebControls.CheckBox);
                    if (chkRow.Checked)
                    {
                        string detalle = row.Cells[1].Text;
                        string precio = row.Cells[2].Text;
                        string id = row.Cells[0].Text;
                        int id_servicio = int.Parse(id);
                        servicio oservicio = bus.buscarservicio(id_servicio);
                        Lse.Add(oservicio);
                        dt.Rows.Add(detalle, precio);

                    }

                }

            }
            if (Lse.Count <= 5) {
                Lservi = Lse;
            GridView2.DataSource = dt;
            GridView2.DataBind();
            }


        }

        protected void Cancelar(object sender, EventArgs e)
        {
            Server.Transfer("AltaDetalle.aspx");

        }
       





        //    protected void CargarServicios(object sender, EventArgs e){

        //        ServiciosDiv.Visible = true;
        //        DropMecanicosDispo.Visible = true;
                
                
        //        VerGrid();
                

        //    }

        //    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        //        {
        //            string id = GridView1.SelectedRow.Cells[1].Text;
        //            string detalle = GridView1.SelectedRow.Cells[2].Text;
        //            string precio = GridView1.SelectedRow.Cells[3].Text;
        //        List<serviciostock> Lserstock = Lserviciostock(id);
        //        Buscadores bus = new Buscadores();
        //        List<stock> Nstock = Lstockuso(Lserstock);
        //        foreach(stock ostock in Nstock){
        //            if (int.Parse(ostock.cantidad) <= int.Parse(ostock.minimo))
        //            {
        //                StockError.Visible = true;

        //            }

        //        }


                
                


                
                
        //        servicio Servi = new servicio();
        //        Servi.id_servicios = int.Parse(id);
        //        Servi.detalle = detalle;
        //        Servi.precio = precio;
                
                
        //        Lservi.Add(Servi);
        //        GridView2.DataSource = Lservi;
        //        GridView2.DataBind();
        //    }

        //    protected void EliminarGrid2(object sender, EventArgs e)
        //    {


        //        string id = GridView2.SelectedRow.Cells[1].Text;
        //        string detalle = GridView2.SelectedRow.Cells[2].Text;
        //        string precio = GridView2.SelectedRow.Cells[3].Text;

        //        servicio Servi = new servicio();
        //        Servi.id_servicios = int.Parse(id);
        //        Servi.detalle = detalle;
        //        Servi.precio = precio;

        //        Servi = Lservi.Find(servicio => servicio.id_servicios == Servi.id_servicios);
        //        Lservi.Remove(Servi);
        //        GridView2.DataSource = Lservi;
        //        GridView2.DataBind();
        //    }


        //    public List<serviciostock> Lserviciostock(String id )
        //    {
        //        Buscadores bus = new Buscadores();
        //        List<serviciostock> Lserviciostocks =bus.Lstockservi();
        //        List<serviciostock> NLserviciostock = new List<serviciostock>();
        //        NLserviciostock = Lserviciostocks.FindAll(s => s.id_servicio == int.Parse(id));

        //        return NLserviciostock;

        //    }

        //    public List<stock> Lstockuso(List<serviciostock> Lstockservi)
        //    {
        //        Buscadores bus = new Buscadores();
        //        List<stock> Lstock = bus.Lstock();
        //        List<stock> stockactivo = new List<stock>();
        //        foreach (stock Stock in Lstock)
        //        {
        //            foreach (serviciostock Servistock in Lstockservi)
        //            {
        //                if (Stock.id_stock==Servistock.id_stock)
        //                {
        //                    stockactivo.Add(Stock);

        //                }

        //            }
        //        }

        //        return stockactivo;

        //    }
        
        

        

        
    }
}