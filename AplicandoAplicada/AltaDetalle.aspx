<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="AltaDetalle.aspx.cs" Inherits="AplicandoAplicada.AltaDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
     <form id="form1" runat="server">
     <div id="contenidoprincipal" class="prueba">

        
        <br />
        <br />
         
        <input type="text" id="Patente" placeholder="Patente"  runat="server"/> 
            
             <a href="#" class="boton" runat="server" onserverclick="Unnamed_ServerClick">Buscar</a>
         

         
        <br />
        <br />
         
             <asp:Label Text="Cosas del Vehiculo" runat="server" />
               <input type="text" id="Anno" placeholder="Año"  runat="server"/> 
              <input type="text" id="Modelo" placeholder="Modelo"  runat="server"/> 
                <input type="text" id="Cliente" placeholder="Cliente"  runat="server"/> 
                <input type="text" id="Direccion" placeholder="Direccion"  runat="server"/> 
                <input type="text" id="Mail" placeholder="Mail"  runat="server"/> 
                 

        <br />
        <br />
         
             <asp:Label Text="Cosas de Servicio y Empleados Disponibles" runat="server" />

        
        

        
        
        <br />
        <br />
        <a href="#" class="boton" runat="server">Cargar</a>
        <br />




    </div>
    <div id="theDiv" class="prueba" runat="server">
        <h3>La patente no se encontro, cargela</h3>
        
             <br />
         <asp:DropDownList ID="Dmodelo" runat="server" DataSourceID="DMarca" DataTextField="nombre" DataValueField="id_marca" AutoPostBack="True"></asp:DropDownList>
         <asp:SqlDataSource ID="DMarca" runat="server" ConnectionString="<%$ ConnectionStrings:aplicadaBDConnectionString %>" SelectCommand="SELECT DISTINCT [nombre], [id_marca] FROM [marca]"></asp:SqlDataSource>
         <br />
         <br />

         <a href="#" class="boton" runat="server">Cargar Marca</a>
        <br /> 
        <br />
         <asp:DropDownList ID="modelito" runat="server" DataSourceID="Dmodelado" DataTextField="nombre" DataValueField="id_modelo"></asp:DropDownList>
         <asp:SqlDataSource ID="Dmodelado" runat="server" ConnectionString="<%$ ConnectionStrings:aplicadaBDConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT DISTINCT * FROM [modelo] WHERE ([id_marca] = @id_marca)">
             <SelectParameters>
                 <asp:ControlParameter ControlID="Dmodelo" Name="id_marca" PropertyName="SelectedValue" Type="Int32" />
             </SelectParameters>
         </asp:SqlDataSource>
        <br />




         </div>


     </form>
</asp:Content>
