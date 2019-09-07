<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="AltaEmpleados.aspx.cs" Inherits="AplicandoAplicada.AltaEmpleados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <div id="contenidoprincipal">

        <br />
        <br />
        <input type="text" id="dni" placeholder="DNI" />
        <br />
        <br />
        <input type="text" id="txtayn" placeholder="Apellido y Nombre" />
        <br />
        <br />
        <select class="select">
            <option value="value">Administrativo</option>
            <option value="value">Mecanico</option>
        </select>
        <br />
        <br />
        <input type="text" id="tele" placeholder="Telefono" />
        <br />
        <br />
        <input type="text" id="dir" placeholder="Direccion" />
        <br />
        <br />
        <a href="#" class="boton">Cargar</a>
        <br />




    </div>


</asp:Content>
