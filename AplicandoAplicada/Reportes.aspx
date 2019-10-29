<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="AplicandoAplicada.Reportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <form id="form1" runat="server" class="FormDetalleTaller">
        <div class="contenedor">
            <h2>Reportes para generar</h2>
        <div class="contenidoprincipal">
            
            <h3>Reportes de Stock</h3>
            <input type="text" runat="server" id="txtinput" placeholder="Agrege Marca o Producto"  visible="true">
            <a href="#" class="btnReport" runat="server" id="LST" onserverclick="PDFLST">Lista Stock TOTAL</a>
            <a href="#" class="btnReport" runat="server" id="LSM" onserverclick="PDFLSM">Lista Stock MARCA</a>
            <a href="#" class="btnReport" runat="server" id="LSP" onserverclick="PDFLSP">Lista Stock PRODUCTO</a>
            
        </div>

            <div class="contenidoprincipal">
            
            <h3>Reportes por tipo de pago</h3>

                <asp:DropDownList ID="DropMetododePago" runat="server" CssClass="modelito"></asp:DropDownList>
            <a href="#" class="btnReport" runat="server" id="PTP" onserverclick="PDFPTP">Por tipo de Pago</a>
 
        </div>

</div>
    </form>
</asp:Content>
