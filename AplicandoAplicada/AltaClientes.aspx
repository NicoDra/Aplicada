<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="AltaClientes.aspx.cs" Inherits="AplicandoAplicada.Clientes.AltaClientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <form id="form1" runat="server">
    <div id="contenidoprincipal">

        

        <br />
        <br />
        <input type="text" id="dnitxt" name="dnitxt" placeholder="DNI" runat="server" />
        <br />
        <br />
        <input type="text" id="txtayn" placeholder="Apellido y Nombre"  runat="server" />
        <br />
        <br />
        <input type="text" id="tele" placeholder="Telefono"  runat="server" />
        <br />
        <br />
        <input type="text" id="mail" placeholder="Mail"  runat="server" />
        <br />
        <a href="#" id="boton" class="boton" runat="server" onserverclick="Cargando">Cargar</a>
        <br />
        
        
        
        <br />




    </div>
    
    </form>
    
</asp:Content>
