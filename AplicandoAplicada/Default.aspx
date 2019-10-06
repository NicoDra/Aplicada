<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AplicandoAplicada.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    <form id="form1" class="form1" runat="server">
        <link href="https://fonts.googleapis.com/css?family=Oswald&display=swap" rel="stylesheet"/>
        <h2 class="TitulosHome">Taller de Reparaciones</h2>
        <div><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></div>
    <div class="slider">
        <ul>
            <li>
                <img src="Imagen/Slider/1.png" alt=""  /></li>
            <li>
                <img src="Imagen/Slider/2.png" alt=""  /></li>
            <li><img src="Imagen/Slider/3.png" alt="" /></li>

        </ul>

    </div>
        <br />
    </form>
    <br />

</asp:Content>
