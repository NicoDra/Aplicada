using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

        public List<servicio> LSUSO
        {
            get
            {
                if (Session["LSUSO"] == null)
                    Session["LSUSO"] = new List<servicio>();
                return (List<servicio>)Session["LSUSO"];
            }
            set
            {
                Session["LSUSO"] = value;
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
            OrdenActual.vehiculo = ovehiculo;
            modelo omodelo = bus.buscarmodelo(ovehiculo);
            marca omarca = bus.buscarmarca(omodelo);
            OrdenActual.vehiculo.modelo = omodelo;
            OrdenActual.vehiculo.modelo.marca = omarca;
            ordenestado oestado = bus.buscarvestadoorden(Orden.id_orden);
            List<ordenservicio> Lidservidcios = new List<ordenservicio>();
            Lidservidcios = bus.buscarlistaid(Orden.id_orden);
            CheckBoton(oestado);
            List<servicio> Lservicios = ObtenerServicios(Lidservidcios);
            LSUSO = Lservicios;
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
                    oestado.fecha_mecanico = System.DateTime.Now;
                    DBF.SaveChanges();
                    PDFESTADOCERO();
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
                oestado.fecha_servicefin = System.DateTime.Now;
                DBF.SaveChanges();
                empleado oempleado = (from q in DBF.empleado where q.id_empleado == LogEmpleado.id_empleado select q).First();
                oempleado.disponibilidad = 0;
                DBF.SaveChanges();
                CheckBoton(oestado);
                Server.Transfer("Taller.aspx");
            }

            }
        }

        public void PDFESTADOCERO()
        {
            Buscadores bus = new Buscadores();
            var doc = new iTextSharp.text.Document(PageSize.A4);
            string path = Server.MapPath("~");
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path + "/Taller.pdf", FileMode.Create));
            doc.Open();

            //Cabecera
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.GREEN.Darker());
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = 1;
            prgHeading.Add(new Chunk("Taller de Reparaciones".ToUpper(), fntHead));
            doc.Add(prgHeading);
            doc.Add(Chunk.NEWLINE);
            //Generado By
            Paragraph prgGeneratedBY = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntAuthor = new iTextSharp.text.Font(btnAuthor, 12, 2, iTextSharp.text.BaseColor.BLACK);
            prgGeneratedBY.Alignment = Element.ALIGN_RIGHT;
            prgGeneratedBY.Add(new Chunk("Generado por: " + LogEmpleado.nombreyapellido, fntAuthor));  //Agregar LOG Empleado
            prgGeneratedBY.Add(new Chunk("\nFecha : " + DateTime.Now.ToShortDateString(), fntAuthor));
            prgGeneratedBY.Add(new Chunk("\nN° de Orden : " + OrdenActual.id_orden, fntAuthor));
            doc.Add(prgGeneratedBY);
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            doc.Add(p);
            //Espacio
            doc.Add(new Chunk("\n", fntHead)); 
            //Datos
            Paragraph Datos = new Paragraph();
            BaseFont bfntDatos = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntDatos = new iTextSharp.text.Font(bfntDatos, 12, 0, iTextSharp.text.BaseColor.BLACK);
            Datos.Alignment = Element.ALIGN_CENTER;
            Datos.Add(new Chunk("\nPatente: " + OrdenActual.vehiculo.patente + "   Modelo:" + OrdenActual.vehiculo.modelo.nombre + "  Marca:  " + OrdenActual.vehiculo.modelo.marca.nombre, fntDatos));
            doc.Add(Datos);
            //Espacio
            doc.Add(new Chunk("\n", fntHead)); 

                foreach(servicio oservicio in LSUSO){
                    doc.Add(p);
                    Paragraph Dato = new Paragraph();
                    Dato.Alignment = Element.ALIGN_LEFT;
                    Dato.Add(new Chunk("\nServicio: " + oservicio.detalle , fntDatos));
                    doc.Add(Dato);
                    //Espacio
                    doc.Add(new Chunk("\n", fntHead)); 
                    //Creo una tabla
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Codigo"), new DataColumn("Detalle"), new DataColumn("Marca"), new DataColumn("Cantidad") });//nombre de las columnas
                    List<stock> Lstock = bus.Lstockuso(oservicio.id_servicios.ToString());
                    foreach (stock ostock in Lstock)
                    {
                        dt.Rows.Add(ostock.codigo, ostock.detalle, ostock.marca, ostock.cantidad); //Agrego las filas a la tabla
                    }
                    //Tabla
                    PdfPTable table = new PdfPTable(dt.Columns.Count);

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        string cellText = Server.HtmlDecode(dt.Columns[i].ColumnName);
                        PdfPCell cell = new PdfPCell();
                        cell.Phrase = new Phrase(cellText, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, 1, new BaseColor(System.Drawing.ColorTranslator.FromHtml("#000000"))));
                        cell.BackgroundColor = new BaseColor(System.Drawing.ColorTranslator.FromHtml("#C8C8C8"));
                        cell.HorizontalAlignment = Element.ALIGN_CENTER;
                        cell.PaddingBottom = 5;
                        table.AddCell(cell);
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            table.AddCell(dt.Rows[i][j].ToString());
                        }
                    }
                    doc.Add(table);


                    //Espacio
                    doc.Add(new Chunk("\n", fntHead)); 
                }

            
            doc.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Taller.pdf','_newtab');", true);


        }
    }
}