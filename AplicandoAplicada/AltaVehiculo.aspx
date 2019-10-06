<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="AltaVehiculo.aspx.cs" Inherits="AplicandoAplicada.AltaVehiculo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

     <form id="form1" runat="server">

     <div id="contenidoprincipal">

        <br />
        <br />
        <input type="text" id="patente" placeholder="Patente" />
        <br />
        <br />
         <asp:DropDownList ID="Dmodelo" CssClass="Dmodelo" runat="server" DataSourceID="DMarca" DataTextField="nombre" DataValueField="id_marca" AutoPostBack="True"></asp:DropDownList>
         <asp:SqlDataSource ID="DMarca" runat="server" ConnectionString="<%$ ConnectionStrings:aplicadaBDConnectionString %>" SelectCommand="SELECT DISTINCT [nombre], [id_marca] FROM [marca]"></asp:SqlDataSource>
         <br />
         <br />
         <a href="#" class="boton" runat="server">Cargar Marca</a>
         
        <br />
         <asp:DropDownList ID="modelito" runat="server" DataSourceID="Dmodelado" DataTextField="nombre" DataValueField="id_modelo"></asp:DropDownList>
         <asp:SqlDataSource ID="Dmodelado" runat="server" ConnectionString="<%$ ConnectionStrings:aplicadaBDConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT DISTINCT * FROM [modelo] WHERE ([id_marca] = @id_marca)">
             <SelectParameters>
                 <asp:ControlParameter ControlID="Dmodelo" Name="id_marca" PropertyName="SelectedValue" Type="Int32" />
             </SelectParameters>
         </asp:SqlDataSource>
        <br />
        
        <input type="text" id="modelo" placeholder="Modelo" runat="server"/>
        <br />
        <br />
        <input type="text" id="año" placeholder="Año" />
        <br />
        <br />
         <input type="text" id="dni" placeholder="DNI" />
         <br />
        <br />
        <a href="#" class="boton">Cargar</a>
        <br />




    </div>



     </form>



</asp:Content>
