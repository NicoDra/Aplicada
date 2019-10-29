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
    public partial class Reportes : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                MetodosdePago();
                if (LogEmpleado.id_tipo != 5)
                {
                    Server.Transfer("Default.aspx");
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
            
        }

        protected void PDFLST(object sender, EventArgs e)
        {
            
            Buscadores bus = new Buscadores();
            List<stock> Lstock = bus.listastock();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Codigo"), new DataColumn("Detalle"), new DataColumn("Marca"), new DataColumn("Cantidad") });
            foreach (stock ostock in Lstock)
            {
                dt.Rows.Add(ostock.codigo, ostock.detalle, ostock.marca, ostock.cantidad);
            }
            iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(PageSize.A4); 
            var doc = new iTextSharp.text.Document(rec);
            
            rec.BackgroundColor = new BaseColor(System.Drawing.Color.Olive);
            doc.SetPageSize(iTextSharp.text.PageSize.A4);
            string path = Server.MapPath("~");
            PdfWriter.GetInstance(doc, new FileStream(path + "/ReportesLST.pdf", FileMode.Create));
            doc.Open();
            //Cabecera
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.GREEN.Darker());
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_LEFT;
            prgHeading.Add(new Chunk("Taller de Reparaciones Reportes PDF".ToUpper(), fntHead));
            doc.Add(prgHeading);  
            //Generado By
            Paragraph prgGeneratedBY = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntAuthor = new iTextSharp.text.Font(btnAuthor, 8, 2, iTextSharp.text.BaseColor.BLACK);
            prgGeneratedBY.Alignment = Element.ALIGN_RIGHT;
            prgGeneratedBY.Add(new Chunk("Generado por: "+LogEmpleado.nombreyapellido , fntAuthor));  //Agregar LOG Empleado
            prgGeneratedBY.Add(new Chunk("\nFecha Generada : " + DateTime.Now.ToShortDateString(), fntAuthor));  
            doc.Add(prgGeneratedBY);
            //La f Linea  
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            doc.Add(p);
            //Espacio
            doc.Add(new Chunk("\n", fntHead));  
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
            //Agregando Campos a la tabla
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    table.AddCell(dt.Rows[i][j].ToString());
                }
            }
            doc.Add(table);
            doc.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('ReportesLST.pdf','_newtab');", true);
        }

        protected void PDFLSM(object sender, EventArgs e)
        {
            if (txtinput.Value!="") { 
            Buscadores bus = new Buscadores();
            List<stock> Lstock = bus.listastockmarca(txtinput.Value);//hacer input para poner el valor de la marca
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Codigo"), new DataColumn("Detalle"), new DataColumn("Marca"), new DataColumn("Cantidad") });
            foreach (stock ostock in Lstock)
            {
                dt.Rows.Add(ostock.codigo, ostock.detalle, ostock.marca, ostock.cantidad);
            }
            iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(PageSize.A4);
            var doc = new iTextSharp.text.Document(rec);

            rec.BackgroundColor = new BaseColor(System.Drawing.Color.Olive);
            doc.SetPageSize(iTextSharp.text.PageSize.A4);
            string path = Server.MapPath("~");
            PdfWriter.GetInstance(doc, new FileStream(path + "/ReportesLST.pdf", FileMode.Create));
            doc.Open();
            //Cabecera
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.GREEN.Darker());
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_LEFT;
            prgHeading.Add(new Chunk("Taller de Reparaciones Reportes PDF".ToUpper(), fntHead));
            doc.Add(prgHeading);
            //Generado By
            Paragraph prgGeneratedBY = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntAuthor = new iTextSharp.text.Font(btnAuthor, 8, 2, iTextSharp.text.BaseColor.BLACK);
            prgGeneratedBY.Alignment = Element.ALIGN_RIGHT;
            prgGeneratedBY.Add(new Chunk("Generado por: " + LogEmpleado.nombreyapellido, fntAuthor));  //Agregar LOG Empleado
            prgGeneratedBY.Add(new Chunk("\nFecha Generada : " + DateTime.Now.ToShortDateString(), fntAuthor));
            doc.Add(prgGeneratedBY);
            //La f Linea  
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            doc.Add(p);
            //Espacio
            doc.Add(new Chunk("\n", fntHead));
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
            //Agregando Campos a la tabla
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    table.AddCell(dt.Rows[i][j].ToString());
                }
            }
            doc.Add(table);
            doc.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('ReportesLST.pdf','_newtab');", true);
            }
        }

        protected void PDFLSP(object sender, EventArgs e)
        {
            if (txtinput.Value != "")
            {
                Buscadores bus = new Buscadores();
                List<stock> Lstock = bus.listastockproducto(txtinput.Value);//hacer input para poner el valor de la marca
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[4] { new DataColumn("Codigo"), new DataColumn("Detalle"), new DataColumn("Marca"), new DataColumn("Cantidad") });
                foreach (stock ostock in Lstock)
                {
                    dt.Rows.Add(ostock.codigo, ostock.detalle, ostock.marca, ostock.cantidad);
                }
                iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(PageSize.A4);
                var doc = new iTextSharp.text.Document(rec);

                rec.BackgroundColor = new BaseColor(System.Drawing.Color.Olive);
                doc.SetPageSize(iTextSharp.text.PageSize.A4);
                string path = Server.MapPath("~");
                PdfWriter.GetInstance(doc, new FileStream(path + "/ReportesLST.pdf", FileMode.Create));
                doc.Open();
                //Cabecera
                BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.GREEN.Darker());
                Paragraph prgHeading = new Paragraph();
                prgHeading.Alignment = Element.ALIGN_LEFT;
                prgHeading.Add(new Chunk("Taller de Reparaciones Reportes PDF".ToUpper(), fntHead));
                doc.Add(prgHeading);
                //Generado By
                Paragraph prgGeneratedBY = new Paragraph();
                BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font fntAuthor = new iTextSharp.text.Font(btnAuthor, 8, 2, iTextSharp.text.BaseColor.BLACK);
                prgGeneratedBY.Alignment = Element.ALIGN_RIGHT;
                prgGeneratedBY.Add(new Chunk("Generado por: " + LogEmpleado.nombreyapellido, fntAuthor));  //Agregar LOG Empleado
                prgGeneratedBY.Add(new Chunk("\nGenerated Date : " + DateTime.Now.ToShortDateString(), fntAuthor));
                doc.Add(prgGeneratedBY);
                //La f Linea  
                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                doc.Add(p);
                //Espacio
                doc.Add(new Chunk("\n", fntHead));
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
                //Agregando Campos a la tabla
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        table.AddCell(dt.Rows[i][j].ToString());
                    }
                }
                doc.Add(table);
                doc.Close();
                Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('ReportesLST.pdf','_newtab');", true);
            }
        }

        protected void PDFPTP(object sender, EventArgs e)
        {
            Buscadores bus = new Buscadores();
            List<orden>Lorden = bus.listasordenmp(DropMetododePago.SelectedValue);
            Lorden = bus.ordenponervyc(Lorden);
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { new DataColumn("N°Orden"), new DataColumn("Patente"), new DataColumn("Cliente"), new DataColumn("Metodo de Pago") });
            foreach (orden oorden in Lorden)
            {
                dt.Rows.Add(oorden.id_orden, oorden.vehiculo.patente,oorden.vehiculo.cliente.nombre, oorden.mpago);
            }
            iTextSharp.text.Rectangle rec = new iTextSharp.text.Rectangle(PageSize.A4);
            var doc = new iTextSharp.text.Document(rec);

            rec.BackgroundColor = new BaseColor(System.Drawing.Color.Olive);
            doc.SetPageSize(iTextSharp.text.PageSize.A4);
            string path = Server.MapPath("~");
            PdfWriter.GetInstance(doc, new FileStream(path + "/ReportesLST.pdf", FileMode.Create));
            doc.Open();
            //Cabecera
            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 16, 1, iTextSharp.text.BaseColor.GREEN.Darker());
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_LEFT;
            prgHeading.Add(new Chunk("Taller de Reparaciones Reportes PDF".ToUpper(), fntHead));
            doc.Add(prgHeading);
            //Generado By
            Paragraph prgGeneratedBY = new Paragraph();
            BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntAuthor = new iTextSharp.text.Font(btnAuthor, 8, 2, iTextSharp.text.BaseColor.BLACK);
            prgGeneratedBY.Alignment = Element.ALIGN_RIGHT;
            prgGeneratedBY.Add(new Chunk("Generado por: " + LogEmpleado.nombreyapellido, fntAuthor));  //Agregar LOG Empleado
            prgGeneratedBY.Add(new Chunk("\nGenerated Date : " + DateTime.Now.ToShortDateString(), fntAuthor));
            doc.Add(prgGeneratedBY);
            //La f Linea  
            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            doc.Add(p);
            //Espacio
            doc.Add(new Chunk("\n", fntHead));
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
            //Agregando Campos a la tabla
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    table.AddCell(dt.Rows[i][j].ToString());
                }
            }
            doc.Add(table);
            doc.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('ReportesLST.pdf','_newtab');", true);



        }
    }
}