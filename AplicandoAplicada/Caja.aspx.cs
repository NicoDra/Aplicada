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
    public partial class WebForm2 : System.Web.UI.Page
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
        public List<orden> LOrden
        {
            get
            {
                if (Session["orden"] == null)
                    Session["orden"] = new List<orden>();
                return (List<orden>)Session["orden"];
            }
            set
            {
                Session["orden"] = value;
            }
        }

        public orden Ordenn
        {
            get
            {
                if (Session["LaOrden"] == null)
                    Session["LaOrden"] = new orden();
                return (orden)Session["LaOrden"];
            }
            set
            {
                Session["LaOrden"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (LogEmpleado.id_tipo != 3)
            {
                Server.Transfer("Default.aspx");
            }
            fecha.Text = DateTime.Now.ToString("dd-MM-yyyy");
            Buscadores bus = new Buscadores();
            List<ordenestado> Lordenestado = bus.buscarListOrdenEstado(3);
            List<orden> Lorden = bus.buscarordeestado(Lordenestado);
            List<vehiculo> LVehiculo = bus.buscarordevehiculo(Lorden);
            LOrden = Lorden;
            
         
        }

        protected void BtnBuscarO(object sender, EventArgs e)
        {
            Buscadores bus = new Buscadores();
            lblmodelo.Text = "La patente ingresada no se encuentra para Cobrar";
            lblpatente.Text = "-";
            lblprecio.Text = "-";
            GridView1.DataSource=null;
            GridView1.DataBind();
            if (txtorden.Value != "")
            {
                txtpatente.Visible = false;
                btnbuscarpatente.Visible = false;
                foreach (orden oOrden in LOrden)
                {

                if (int.Parse(txtorden.Value) == oOrden.id_orden)
                {
                    Ordenn = oOrden;
                    lblpatente.Text = oOrden.vehiculo.patente;
                    cliente objcliente = bus.ocliente(oOrden.vehiculo);
                    NTitular.Text = objcliente.nombre;
                    DNI.Text = objcliente.dni;
                    modelo omodelo = bus.buscarmodelo(oOrden.vehiculo);
                    lblmodelo.Text =omodelo.nombre;
                    NOrden.Text = "N°Orden" + oOrden.id_orden.ToString();
                    CargarGrid(oOrden);

                }

                }

            }
            

        }

        protected void BtnBuscarP(object sender, EventArgs e)
        {
            Buscadores bus = new Buscadores();
            lblmodelo.Text = "La patente ingresada no se encuentra para Cobrar";
            lblpatente.Text = "-";
            GridView1.DataSource = null;
            GridView1.DataBind();
            lblprecio.Text = "-";
            if (txtpatente.Value != "")
            {
                txtorden.Visible = false;
                btnbuscarorden.Visible = false;
                foreach (orden oOrden in LOrden)
                {
                    if (txtpatente.Value == oOrden.vehiculo.patente)
                    {
                        Ordenn = oOrden;
                        lblpatente.Text = oOrden.vehiculo.patente;
                        cliente objcliente = bus.ocliente(oOrden.vehiculo);
                        NTitular.Text = objcliente.nombre;
                        DNI.Text = objcliente.dni;
                        modelo omodelo = bus.buscarmodelo(oOrden.vehiculo);
                        lblmodelo.Text = omodelo.nombre;
                        NOrden.Text = "N°Orden"+oOrden.id_orden.ToString();
                        CargarGrid(oOrden);


                    }

                }

            }
            

                
            
        }


        private void MetodosdePago()
        {
            System.Web.UI.WebControls.ListItem i;
            i = new System.Web.UI.WebControls.ListItem("Tarjeta de Credito", "Tarjeta de Credito");
            DropMetododePago.Items.Add(i);
            i = new System.Web.UI.WebControls.ListItem("Tarjeta de Debito", "Tarjeta de Debito");
            DropMetododePago.Items.Add(i);
            i = new System.Web.UI.WebControls.ListItem("Efectivo", "Efectivo");
            DropMetododePago.Items.Add(i);
            divmetodo.Visible = true;
        }

        private void CargarGrid(orden oOrden)
        {
           
            MetodosdePago();
            Buscadores bus = new Buscadores();
            List<ordenservicio> Lidservidcios = new List<ordenservicio>();
            Lidservidcios = bus.buscarlistaid(oOrden.id_orden);
            List<servicio> Lservicios = ObtenerServicios(Lidservidcios);
            DataTable dtable = new DataTable();
            dtable.Columns.AddRange(new DataColumn[4] { new DataColumn("Detalle"), new DataColumn("Precio"), new DataColumn("Total"), new DataColumn("Cantidad") });
            int preciototal =0;
            foreach (ordenservicio o in Lidservidcios)
            {

                servicio oservicio = Lservicios.Find(x => x.id_servicios == o.id_servicio);
                int cantidad = o.cantidad ?? default(int); 
                string total = (double.Parse(oservicio.precio) * Convert.ToDouble(cantidad)).ToString();
                dtable.Rows.Add(oservicio.detalle, oservicio.precio,total,o.cantidad);
                preciototal = preciototal + int.Parse(total);
            }
            //foreach (servicio x in Lservicios)
            //{
            //    a = a + int.Parse(x.precio);
            //}
            lblprecio.Text = preciototal.ToString();
            //GridView1.DataSource = Lservicios;
            GridView1.DataSource = dtable;
            GridView1.DataBind();
        }

        private List<servicio> ObtenerServicios(List<ordenservicio> Lidservidcios)
        {
            List<servicio> Lservicio = new List<servicio>();
            Buscadores bus = new Buscadores();
            foreach (ordenservicio idservicios in Lidservidcios)
            {
                servicio oservicio = bus.buscarservicio(idservicios.id_servicio);
                Lservicio.Add(oservicio);
            }
            return Lservicio;


        }

        protected void BtnCobrar(object sender, EventArgs e)
        {

            using (aplicadaBDEntities2 DBF = new aplicadaBDEntities2())
            {
                ordenestado oestado = (from q in DBF.ordenestado where q.id_orden == Ordenn.id_orden select q).First();
                oestado.estado = 4;
                oestado.fecha_cobrado = System.DateTime.Now;
                DBF.SaveChanges();
                orden oorden = (from q in DBF.orden where q.id_orden == Ordenn.id_orden select q).First();
                oorden.mpago = DropMetododePago.SelectedValue;
                DBF.SaveChanges();
                ordenempleado ordenemple = new ordenempleado
                {
                    id_orden = oorden.id_orden,
                    id_empleado = LogEmpleado.id_empleado,

                };
                DBF.ordenempleado.Add(ordenemple);
                DBF.SaveChanges();
                Server.Transfer("Default.aspx");


            }
        }

        //public void PDFESTADOCERO() {

        //    iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(PageSize.A4);
        //    var doc = new iTextSharp.text.Document(rec);
        //    string url = "@//";

        //    rec.BackgroundColor = new BaseColor(System.Drawing.Color.Olive);
        //    doc.SetPageSize(iTextSharp.text.PageSize.A4);
        //    string path = Server.MapPath("~");
        //    PdfWriter.GetInstance(doc, new FileStream(path + "/Factura.pdf", FileMode.Create));
        //    doc.Open();
        //    iTextSharp.text.Image Factura = iTextSharp.text.Image.GetInstance("C:/Users/niko_/documents/visual studio 2013/Projects/AplicandoAplicada/AplicandoAplicada/Resources/Factura.jpg");
        //    Factura.BorderWidth = 0;
        //    Factura.Alignment = Element.ALIGN_RIGHT;
        //    float percentage = 0.0f;
        //    percentage = 150 / Factura.Width;
        //    Factura.ScalePercent(percentage * 100);
        //    doc.Add(Factura);

        //    // Cerramos el documento
        //    doc.Close();

        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Factura.pdf','_newtab');", true);
        
        //}

        public void PDFESTADOCERO()
        {

            Buscadores bus = new Buscadores();
            var doc = new iTextSharp.text.Document(PageSize.A4);
            string path = Server.MapPath("~");
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(path + "/Factura.pdf", FileMode.Create));


            vehiculo ovehiculo = bus.buscarvehiculoid(Ordenn.id_vehiculo ?? default(int));
            cliente ocliente = bus.ocliente(ovehiculo);
            modelo omodelo = bus.buscarmodelo(ovehiculo);
            marca omarca = bus.buscarmarca(omodelo);

            doc.Open();

            //Cabecera
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.GREEN.Darker());
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = 1;
            prgHeading.Add(new Chunk("Factura".ToUpper(), fntHead));
            doc.Add(prgHeading);
            doc.Add(Chunk.NEWLINE);

            //Generado By
            Paragraph prgGeneratedBY = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntAuthor = new iTextSharp.text.Font(btnAuthor, 12, 2, iTextSharp.text.BaseColor.BLACK);
            prgGeneratedBY.Alignment = Element.ALIGN_RIGHT;
            prgGeneratedBY.Add(new Chunk("Generado por: " + LogEmpleado.nombreyapellido, fntAuthor));  //Agregar LOG Empleado
            prgGeneratedBY.Add(new Chunk("\nFecha : " + DateTime.Now.ToShortDateString(), fntAuthor));
            prgGeneratedBY.Add(new Chunk("\nN° de Orden : " + Ordenn.id_orden, fntAuthor));
            doc.Add(prgGeneratedBY);
            doc.Add(Chunk.NEWLINE);

            doc.Add(Chunk.NEWLINE);

            //tablados
            PdfPTable tabla2 = new PdfPTable(2);
            tabla2.WidthPercentage = 100;
            tabla2.SpacingAfter = 20;

            PdfPCell vehiculoTitulo = new PdfPCell(new Phrase("Vehiculo"));
            vehiculoTitulo.BorderWidth = 0;
            vehiculoTitulo.BorderWidthRight = 0.75f;
            vehiculoTitulo.BorderWidthTop = 0.75f;
            vehiculoTitulo.BorderWidthLeft = 0.75f;
            vehiculoTitulo.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell clienteTitulo = new PdfPCell(new Phrase("Cliente"));
            clienteTitulo.BorderWidth = 0;
            clienteTitulo.BorderWidthTop = 0.75f;
            clienteTitulo.BorderWidthRight = 0.75f;
            clienteTitulo.HorizontalAlignment = Element.ALIGN_CENTER;

            //PdfPCell blankCell = new PdfPCell(new Phrase(Chunk.NEWLINE));
            //blankCell.BorderWidth = 0;
            //blankCell.BorderWidthRight = 0.75f;
            //blankCell.BorderWidthLeft = 0.75f;

            //PdfPCell blankCell2 = new PdfPCell(new Phrase(Chunk.NEWLINE));
            //blankCell2.BorderWidth = 0;
            //blankCell2.BorderWidthRight = 0.75f;

            PdfPCell patente = new PdfPCell(new Phrase("Patente: " + ovehiculo.patente));
            patente.BorderWidth = 0;
            patente.BorderWidthRight = 0.75f;
            patente.BorderWidthBottom = 0.75f;
            patente.BorderWidthLeft = 0.75f;

            PdfPCell marca = new PdfPCell(new Phrase("Marca: " + omarca.nombre));
            marca.BorderWidth = 0;
            marca.BorderWidthRight = 0.75f;
            marca.BorderWidthLeft = 0.75f;

            PdfPCell modelo = new PdfPCell(new Phrase("Modelo: " + omodelo.nombre));
            modelo.BorderWidth = 0;
            modelo.BorderWidthRight = 0.75f;
            modelo.BorderWidthLeft = 0.75f;

            PdfPCell nombre = new PdfPCell(new Phrase("Apellido y Nombre: " + ocliente.nombre));
            nombre.BorderWidth = 0;
            nombre.BorderWidthLeft = 0.75f;
            nombre.BorderWidthBottom = 0.75f;
            nombre.BorderWidthRight = 0.75f;
            PdfPCell dni = new PdfPCell(new Phrase("DNI: " + ocliente.dni));
            dni.BorderWidth = 0;
            dni.BorderWidthBottom = 0.75f;
            dni.BorderWidthRight = 0.75f;
            
            tabla2.AddCell(vehiculoTitulo);
            tabla2.AddCell(clienteTitulo);
            //tabla2.AddCell(blankCell);
            //tabla2.AddCell(blankCell2);
            tabla2.AddCell(marca);
            tabla2.AddCell(modelo);
            tabla2.AddCell(nombre);
            tabla2.AddCell(patente);
            tabla2.AddCell(dni);

            doc.Add(tabla2);


            doc.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('Factura.pdf','_newtab');", true);
            //Response.Redirect("Presupuesto.pdf");


        }

        protected void BtnImporimir(object sender, EventArgs e)
        {
            PDFESTADOCERO();

        }
        
    
    }
}