<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="AltaDetalle.aspx.cs" Inherits="AplicandoAplicada.AltaDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
     <div id="contenidoprincipal">

        
        <br />
        <br />
         <asp:Panel runat="server" class="Panel" > 
        <input type="text" id="Patente" placeholder="Patente" /> 
            
             <input type="image" class="imagenboton" src="Imagen/lupa.png"/>

         </asp:Panel>
        <br />
        <br />
         <asp:Panel runat="server" class="Panel" Visible="false">
             <asp:Label Text="Cosas del Vehiculo" runat="server" /></asp:Panel>
        <br />
        <br />
         <asp:Panel runat="server" class="Panel" Visible="false">
             <asp:Label Text="Cosas de Servicio y Empleados Disponibles" runat="server" />

         </asp:Panel>
        
        <br />
        <br />
        <a href="#" class="boton">Cargar</a>
        <br />




    </div>
</asp:Content>
