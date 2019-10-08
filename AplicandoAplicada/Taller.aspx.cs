using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AplicandoAplicada
{
    public partial class WebForm1 : System.Web.UI.Page
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
            
            if(!IsPostBack){

                if (LogEmpleado.id_tipo != 1)
                {
                    Server.Transfer("Default.aspx");
                }
            
            Buscadores bus = new Buscadores();
            ordenempleado OrdenEmpleado = bus.buscarempleadoorden(LogEmpleado.id_empleado);
            if(OrdenEmpleado==null){
                lblpatente.Text = "No tienes ningun vehiculo asignado. ";
                lblmodelo.Text = "-";
                btnaceptar.Visible = false;
                btnfinalizar.Visible = false;
            }
            else
            {

            
            int a = int.Parse(OrdenEmpleado.id_orden.ToString());
            orden Orden = bus.buscarorden(a);
            OrdenActual = Orden;
            vehiculo ovehiculo = bus.buscarvehiculoid(int.Parse(Orden.id_vehiculo.ToString()));
            modelo omodelo = bus.buscarmodelo(ovehiculo);
            ordenestado oestado = bus.buscarvestadoorden(Orden.id_orden);
            List<ordenservicio> Lidservidcios = new List<ordenservicio>();
            Lidservidcios = bus.buscarlistaid(Orden.id_orden);
            CheckBoton(oestado);
            List<servicio> Lservicios = ObtenerServicios(Lidservidcios);
            if ((oestado.estado == 1) || (oestado.estado == 2)) { 
            GridView1.DataSource = Lservicios;
            GridView1.DataBind();

            lblpatente.Text = "PATENTE: "+ ovehiculo.patente.ToString();
            lblmodelo.Text = "MODELO: " + omodelo.nombre.ToString();
            }
            else
            {
                lblpatente.Text = "No tienes ningun vehiculo asignado. ";
                lblmodelo.Text = "-";
                    btnaceptar.Visible=false;
                btnfinalizar.Visible=false;

            }
               }
        
        }
        }

        private void CheckBoton(ordenestado oestado)
        {
            if(oestado.estado ==1){
                btnaceptar.Visible = true;
                btnfinalizar.Visible = false;
            }
            if (oestado.estado == 2)
            {

            
                btnaceptar.Visible = false;
                btnfinalizar.Visible = true;
                
            }
        }

      

        private List<servicio> ObtenerServicios(List<ordenservicio> Lidservidcios)
        {
            List<servicio> Lservicio = new List<servicio>();
            Buscadores bus =new Buscadores();
            foreach (ordenservicio idservicios in Lidservidcios)
            {
                servicio oservicio = bus.buscarservicio(idservicios.id_servicio);
                Lservicio.Add(oservicio);
            }
            return Lservicio;


        }

        protected void BtnAceptarTrabajo(object sender, EventArgs e)
        {
            if (txtpwd.Value == LogEmpleado.contraseña)
            {
                using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
                {
                    ordenestado oestado = (from q in DBF.ordenestado where q.id_orden == OrdenActual.id_orden select q).First();
                    oestado.estado = 2;
                    oestado.fecha = System.DateTime.Now;
                    DBF.SaveChanges();
                    CheckBoton(oestado);

                }
            }
        }

        protected void BtnTerminarTrabajo(object sender, EventArgs e)
        {
            if (txtpwd.Value == LogEmpleado.contraseña) { 
            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {
                ordenestado oestado = (from q in DBF.ordenestado where q.id_orden == OrdenActual.id_orden select q).First();
                oestado.estado = 3;
                oestado.fecha = System.DateTime.Now;
                DBF.SaveChanges();
                empleado oempleado = (from q in DBF.empleado where q.id_empleado == LogEmpleado.id_empleado select q).First();
                oempleado.disponibilidad = 0;
                DBF.SaveChanges();
                CheckBoton(oestado);
                Server.Transfer("Taller.aspx");
            }

            }
        }
    }
}