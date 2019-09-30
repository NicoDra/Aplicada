<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="AltaDetalle.aspx.cs" Inherits="AplicandoAplicada.AltaDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">
    
    <div id="VehiculoError" class="error" runat="server" visible="false">

        <p>ERROR VEHICULO NO ENCONTRADO</p>

    </div> 

    <div id="ClienteError" class="error" runat="server" visible="false">

        <p>ERROR CLIENTE NO ENCONTRADO</p>

    </div>

    <div id="StockError" class="warning" runat="server" visible="false">

        <p>ATENCION, QUEDA POCO STOCK</p>

    </div> 

    
    <form id="form1" runat="server">
     <div id="contenidoprincipal" class="prueba izquierda">

        <br />
        <br />  
        <input type="text" id="Patente" placeholder="Patente"  runat="server"/> 
         <br />     
             <a href="#" class="boton" runat="server" onserverclick="Unnamed_ServerClick">Buscar</a>

        <br />
        <br />
        
             <asp:Label Text="Cosas del Vehiculo" runat="server" />
               <input type="text" id="Anno" placeholder="Año"  runat="server" disabled="disabled"  /> 
         <br />
              <input type="text" id="Modelo" placeholder="Modelo"  runat="server" disabled="disabled"/> <br />
                <input type="text" id="Cliente" placeholder="Cliente"  runat="server" disabled="disabled"/> <br />
                <input type="text" id="Direccion" placeholder="Direccion"  runat="server" disabled="disabled"/> <br />
                <input type="text" id="Mail" placeholder="Mail"  runat="server" disabled="disabled"/> 

        <br />
        <br />
         
             <asp:Label Text="Cosas de Servicio y Empleados Disponibles" runat="server" />
                
        <br />
        <br />
        <a href="#" class="boton" runat="server"  onserverclick="CargarServicios">Cargar Servicios</a>
         <asp:GridView ID="GridView2" runat="server" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" GridLines="None"  OnSelectedIndexChanged="EliminarGrid2" CellSpacing="1">
             <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
             <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
             <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
             <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
             <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
             <SortedAscendingCellStyle BackColor="#F1F1F1" />
             <SortedAscendingHeaderStyle BackColor="#594B9C" />
             <SortedDescendingCellStyle BackColor="#CAC9C9" />
             <SortedDescendingHeaderStyle BackColor="#33276A" />
             <Columns>
                 <asp:CommandField SelectText="Eliminar " ShowSelectButton="True" />

             </Columns>
         </asp:GridView>
        <br />
         <asp:DropDownList ID="DropMecanicosDispo" runat="server" DataSourceID="DMecanicos" DataTextField="nombreyapellido" DataValueField="id_empleado" ></asp:DropDownList>



         <asp:SqlDataSource ID="DMecanicos" runat="server" ConnectionString="<%$ ConnectionStrings:aplicadaBDConnectionString %>" SelectCommand="SELECT [nombreyapellido], [id_empleado], [id_tipo] FROM [empleado] WHERE ([id_tipo] = 1) AND ([disponibilidad] = 0)"></asp:SqlDataSource>



    </div>

    <div id="DivAAuto" class="prueba derecha" runat="server">
        <h3>Alta de Vehiculo</h3>
        <input type="text" id="patenterepe" placeholder="Patente"  runat="server"/> 
        
             <br />
        <input type="text" id="año" placeholder="Año" />
        <br />
        <br />
         <asp:DropDownList ID="Dmodelo" runat="server" DataSourceID="DMarca" DataTextField="nombre" DataValueField="id_marca" AutoPostBack="True"></asp:DropDownList>
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
        

         </div>


    <div id="ServiciosDiv"  runat="server" class="prueba derecha">
            <br/>

            <h3>Alta de Vehiculo</h3>
            <br/>
            <asp:GridView ID="GridView1" runat="server" CellPadding="3" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellSpacing="1" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" GridLines="None">
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
            <PagerStyle ForeColor="Black" HorizontalAlign="Right" BackColor="#C6C3C6" />
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#594B9C" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#33276A" />
            <Columns>
                 <asp:CommandField SelectText="SELECCIONAR " ShowSelectButton="True" />
           </Columns>
        </asp:GridView>
        <br />
         </div>
         

     </form>

</asp:Content>
