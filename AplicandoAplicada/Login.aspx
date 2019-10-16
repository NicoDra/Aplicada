<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AplicandoAplicada.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <form id="form1" runat="server" class="FormDetalleTaller">

        <div class="login">

            <h2>Iniciar Sesion</h2>

            <div class="form">

                <input type="text" placeholder="Ingrese correo electronico" runat="server" id="txtemail"/>
                <input type="password" placeholder="Contraseña" runat="server" id="txtcontraseña"/>

            </div>

            <a href="#" class="btnLogin" runat="server" id="btnLogin" onserverclick="btnLogin_ServerClick">Acceder</a>

        </div>

    </form>

</asp:Content>
