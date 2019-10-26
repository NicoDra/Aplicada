using System;
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
        public List<stock> Lstock
        {
            get
            {
                if (Session["Lstock"] == null)
                    Session["Lstock"] = new List<stock>();
                return (List<stock>)Session["Lstock"];
            }
            set
            {
                Session["Lstock"] = value;
            }
        }
        public DataTable dtable
        {
            get
            {
                if (Session["dtable"] == null)
                    Session["dtable"] = new DataTable();
                return (DataTable)Session["dtable"];
            }
            set
            {
                Session["dtable"] = value;
            }
        }
        public List<servicio> LSM
        {
            get
            {
                if (Session["LSM"] == null)
                    Session["LSM"] = new List<servicio>();
                return (List<servicio>)Session["LSM"];
            }
            set
            {
                Session["LSM"] = value;
            }
        }
        public List<servicio> LSAC
        {
            get
            {
                if (Session["LSAC"] == null)
                    Session["LSAC"] = new List<servicio>();
                return (List<servicio>)Session["LSAC"];
            }
            set
            {
                Session["LSAC"] = value;
            }
        }

        public List<Cantidad> Lcantidades
        {
            get
            {
                if (Session["Lcantidades"] == null)
                    Session["Lcantidades"] = new List<Cantidad>();
                return (List<Cantidad>)Session["Lcantidades"];
            }
            set
            {
                Session["Lcantidades"] = value;
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

           

            if (!IsPostBack)
            {dtable.Clear();
                if (LogEmpleado.id_tipo != 2)
                {
                     
                    Server.Transfer("Default.aspx");
                    //VerGrid();
                }
                if (dtable.Columns.Count == 0) {
                    dtable.Columns.AddRange(new DataColumn[4] { new DataColumn("Detalle"), new DataColumn("Precio"), new DataColumn("Total"), new DataColumn("Cantidad") });
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
                    ordenestado ordenestado = new ordenestado();
                    
                    orden orden = bus.buscarordenporvehiculo(objvehiculo.id_vehiculo);
                    if (orden != null)
                    {
                     ordenestado = bus.buscarvestadoorden(orden.id_orden);
                    }
                    if ((orden==null)||(ordenestado.estado == null) || (ordenestado.estado == 4))
                    {

                    NoAuto.Visible = false;
                    RecargarAuto();
                    servicio oservicio = new servicio();
                    VerGrid(oservicio);
                    A1.Visible = true;
                    btnServicios.Visible = true;
                    DropServicio.Visible = true;
                    txtcantidad.Visible = true;
                    lblpreciototal.Visible = true;
                    btnfinalizar.Visible = true;
                    

                    }
                    else
                    {

                        Label3.Text = "EL VEHICULO YA POSEE UNA ORDEN ACTIVA";
                        NoAuto.Visible = true;

                    }

                }
                else
                {
                    NoAuto.Visible = true;
                    Label3.Text = "PONER ENTRE DE 6 Y 7 CARACTERES";
                    int b = txtpatente.Value.Length;
                    if (b>=6 && b<=7)
                    {
                        NoAuto.Visible = false;
                        Dmodelo.Visible = true;
                        Dmarca.Visible = true;
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
            DropServicio.Visible = true;
            DropTipoServicio.Visible = true;
            
            btnServicios.Visible = true;
            txtcantidad.Visible = true;
            lblpreciototal.Visible = true;
            btnfinalizar.Visible = true;

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
            if ((txtpatente.Value != "") && (txtdni.Value != "") && (StockError.Visible=true))
            {

            Buscadores bus = new Buscadores();
            cliente ocliente = bus.oclientedni(txtdni.Value);
            vehiculo ovehiculo = bus.buscarvehiculo(txtpatente.Value);
            cliente oclientes = bus.ocliente(ovehiculo);

            if ((ovehiculo != null)&&(ovehiculo.id_cliente != null) && (ocliente != null) && (ovehiculo.id_cliente == ocliente.id)&&(Lservi.Count<=5)&& (Lservi.Count>=1))
            {
                CargarOrden();
                btnpasartaller.Visible = true;
                btnServicios.Visible = false;
                btnfinalizar.Visible = false;
                

            }
            else
            {
                Server.Transfer("AltaDetalle.aspx");

            }

            }
            else
            {
                Server.Transfer("AltaDetalle.aspx");
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
                    estado = 0,
                    fecha_presupuesto= System.DateTime.Now

                };
                DBF.ordenestado.Add(oOrdenEstado);
                DBF.SaveChanges();
                ordenempleado ordenemple = new ordenempleado
                {
                    id_orden = oorden.id_orden,
                    id_empleado = LogEmpleado.id_empleado,

                };
                DBF.ordenempleado.Add(ordenemple);
                DBF.SaveChanges();

                foreach (servicio l in LSAC)
                {
                    Cantidad ocantidad = Lcantidades.Find(x => x.codigo == l.id_servicios);
                    ordenservicio ooServicio = new ordenservicio
                    {
                        id_orden = oorden.id_orden,
                        id_servicio = l.id_servicios,
                        cantidad=ocantidad.cantidade
                        
                
                    };

                    DBF.ordenservicio.Add(ooServicio);
                    DBF.SaveChanges();
                }
                OrdenActual = oorden;
                Lcantidades.Clear();

               
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
            Dmodelo.Visible = false;
            Dmarca.Visible = false;
            txtapellido.Disabled = true;
            txtnombre.Disabled = true;
            txttelefono.Disabled = true;
            txtemail.Disabled = true;
            txtaño.Disabled = true;

            RecargarAuto();
            servicio oservicio = new servicio();
            VerGrid(oservicio);

        }
        public void VerGrid(servicio oservicio)
        {
            List<servicio> Lservicios;

            DropServicio.Items.Clear();
            if (GridView2.Rows.Count == 0) { 
            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {
                IQueryable<servicio> lista = (from q in DBF.servicio select q);
                Lservicios = lista.ToList();
                
                Buscadores bus = new Buscadores();
                string a = txtpatente.Value;
                vehiculo objvehiculo = bus.buscarvehiculo(a);
                modelo objmodelo = bus.buscarmodelo(objvehiculo);


                Lservicios = Lservicios.FindAll(ser => ser.id_modelo == objmodelo.id_modelo);
                LSM = Lservicios;
                Lservicios = Lservicios.FindAll(servicio => servicio.id_tipo == int.Parse(DropTipoServicio.SelectedValue));
                
                
                
                foreach (servicio x in Lservicios)
                {
                    ListItem i;
                    i = new ListItem(x.detalle.ToString(), x.id_servicios.ToString());
                    DropServicio.Items.Add(i);
                }

            }



            }
            else
            {
                if (oservicio.precio!="1")
                {

                
                Lservi.Remove(oservicio);
                Lservicios = Lservi.FindAll(servicio => servicio.id_tipo == int.Parse(DropTipoServicio.SelectedValue));
                foreach (servicio x in Lservicios)
                {
                    ListItem i;
                    i = new ListItem(x.detalle.ToString(), x.id_servicios.ToString());
                    DropServicio.Items.Add(i);
                }

                }
                else
                {
                    using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
                    {
                        IQueryable<servicio> lista = (from q in DBF.servicio select q);
                        Lservicios = lista.ToList();
                        oservicio = Lservicios.Find(x => x.id_servicios == oservicio.id_servicios);

                    }
                    Lservi.Add(oservicio);
                    Lservicios = Lservi.FindAll(servicio => servicio.id_tipo == int.Parse(DropTipoServicio.SelectedValue));
                    foreach (servicio x in Lservicios)
                    {
                        ListItem i;
                        i = new ListItem(x.detalle.ToString(), x.id_servicios.ToString());
                        DropServicio.Items.Add(i);
                    }
                }

            }

                
               
            }


        protected void CargarServicios(object sender, EventArgs e)
        {
            StockError.Visible = false;
            StockWarning.Visible = false;
            List<servicio> Lse = new List<servicio>();
            List<servicio> Lservicios = new List<servicio>();


            if ((GridView2.Rows.Count < 5)&&(DropServicio.SelectedValue.ToString()!=""))
            {
                        Buscadores bus = new Buscadores();
                        string id = DropServicio.SelectedValue.ToString();
                        int id_servicio = int.Parse(id);
                        servicio oservicio = bus.buscarservicio(id_servicio);
                        Lse = LSM;
                        foreach (servicio x in Lse)
                        {
                            if (id_servicio == x.id_servicios)
                            {
                                oservicio = x;
                            }
                        }
                        Lse.Remove(oservicio);
                        Lservi = Lse;
                        string detalle = oservicio.detalle;
                        string precio = oservicio.precio;
                        string total = (double.Parse(oservicio.precio) * double.Parse(txtcantidad.Value)).ToString();
                        string cantidad = txtcantidad.Value;
                        Cantidad oCantidad = new Cantidad(oservicio.id_servicios, int.Parse(cantidad));
                        Lcantidades.Add(oCantidad); 

                        List<serviciostock> Lserstock = Lserviciostock(id_servicio.ToString());
                        List<stock> Nstock = Lstockuso(Lserstock);//revisar esto

                        
                        LSAC.Add(oservicio);
                        dtable.Rows.Add(detalle,precio,total,cantidad);
                        lblprecio.Visible = true;
                        int a = int.Parse(lblprecio.Text) + int.Parse(total);
                        lblprecio.Text = a.ToString();

                        foreach (stock ostock in Nstock)
                        {
                            Lstock.Add(ostock);
                            if (int.Parse(ostock.cantidad) <= int.Parse(ostock.minimo))
                            {
                                StockError.Visible = true;
                                Label1.Text = "¡ATENCION! EL STOCK ES MENOR AL MINIMO: " + ostock.detalle;


                            }
                            if ((int.Parse(ostock.cantidad) >= int.Parse(ostock.minimo)) && (int.Parse(ostock.cantidad) <= (int.Parse(ostock.minimo) + 5)) && (StockError.Visible == false))
                            {
                                StockWarning.Visible = true; //Aca alerta queda poco stock Queda restar
                                Label2.Text = "¡ATENCION! EL STOCK ESTA CERCANO AL MINIMO: " + ostock.detalle;
                            }
                        }
                NoAuto.Visible = false;
                //Lservi = Lse;
                GridView2.DataSource = dtable;
                GridView2.DataBind();
                VerGrid(oservicio);
            
            }
            else
            {
                NoAuto.Visible = true;
                Label3.Text = "No ingrese mas de 5 servicios";
            }
                }
        

        protected void Cancelar(object sender, EventArgs e)
        {
            Server.Transfer("AltaDetalle.aspx");

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
                    if (Stock.id_stock == Servistock.id_stock)
                    {
                        stockactivo.Add(Stock);

                    }

                }
            }

            return stockactivo;

        }

        protected void DropTipoServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            servicio oservicio = new servicio();
            VerGrid(oservicio);
        }



        protected void btnpasarataller_ServerClick(object sender, EventArgs e)
        {
            if ((DropMecanicosDispo.SelectedValue.ToString() != ""))
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
                    oestado.fecha_espera = System.DateTime.Now;   //////////////////////////////////////////////////////////
                    DBF.SaveChanges();
                    empleado oempleado = (from q in DBF.empleado where q.id_empleado == ordenemple.id_empleado select q).First();
                    oempleado.disponibilidad = 1;
                    DBF.SaveChanges();
                    foreach (stock ostock in Lstock)
                    {
                        stock Stocko = new stock();
                        Stocko = (from q in DBF.stock where q.id_stock == ostock.id_stock select q).First();
                        Stocko.cantidad = (int.Parse(Stocko.cantidad) - 1).ToString();
                        DBF.SaveChanges();
                        //restar dependiendo la cantidad del servicio
                    }
                    OrdenActual = null;
                    Lstock = null;
                    Server.Transfer("Default.aspx");

                }
            }
            else
            {
                Server.Transfer("AltaDetalle.aspx");
            }
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Detalle = GridView2.SelectedRow.Cells[0].Text;
            string total = GridView2.SelectedRow.Cells[2].Text;
            int w = GridView2.SelectedRow.RowIndex;
            dtable.Rows.RemoveAt(w);
            GridView2.DataSource = dtable;
            GridView2.DataBind();
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
                servicio oservicio = new servicio();
                oservicio = Lservicios.Find(ser => ser.detalle == Detalle);
                int z = int.Parse(lblprecio.Text) - int.Parse(total);
                lblprecio.Text = z.ToString();
                LSAC.Remove(oservicio);
                List<serviciostock> Lserstock = Lserviciostock(oservicio.id_servicios.ToString());
                List<stock> Nstock = Lstockuso(Lserstock);
                List<stock> Copia = Lstock;
                    foreach (stock ostock in Nstock)
                    {
                        stock oostock = Lstock.Find(x => x.id_stock == ostock.id_stock);
                        Lstock.Remove(oostock);
                    }

                
                

                oservicio.precio = "1";
                
                VerGrid(oservicio);
                //Queda remover la lista de stock 
            }
        }

        
    }
}
