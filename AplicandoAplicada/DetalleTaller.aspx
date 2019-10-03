<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="DetalleTaller.aspx.cs" Inherits="AplicandoAplicada.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <form id="form1" runat="server" class="FormDetalleTaller">

        <div class="ContenedorTalleristas">
    
            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="select"></asp:DropDownList>
    
        </div>

        <div class="btnGuardar">
		
            <a href="#" class="guardarCambios">Nueva Orden/Detalle</a>
            <a href="#" class="guardarCambios">Pasar a Taller</a>

        </div>

    </form>

</asp:Content>
