﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="AltaDetalle.aspx.cs" Inherits="AplicandoAplicada.AltaDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <form id="form1" runat="server" class="FormDetalle">

        <div class="StockError" id="StockError" runat="server" visible="false">

            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
             
        </div>

        <div class="StockWarning" id="StockWarning" runat="server" visible="false">

            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>

        </div>

        <div class="StockWarning" id="NoAuto" runat="server" visible="false">

            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>

        </div>
 <div class="contenedor1">

		<div class="contenedorServicios">
           
            <div class="divaux drop">
                <asp:DropDownList ID="DropTipoServicio" CssClass="modelito" runat="server" DataSourceID="Tiposdeservicios" DataTextField="tipodeservicio" DataValueField="id" AutoPostBack="True" OnSelectedIndexChanged="DropTipoServicio_SelectedIndexChanged"></asp:DropDownList>
                <asp:EntityDataSource ID="Tiposdeservicios" runat="server" ConnectionString="name=aplicadaBDEntities2" DefaultContainerName="aplicadaBDEntities2" EnableFlattening="False" EntitySetName="tiposervicio" EntityTypeFilter="tiposervicio" Select="it.[id], it.[tipodeservicio]"></asp:EntityDataSource>
                <br />
                <br />
                <asp:DropDownList ID="DropServicio" CssClass="modelito" runat="server" Visible="false"></asp:DropDownList>
               <input type="number" runat="server" id="txtcantidad" value="1" visible="false"/>
                
            </div>


        <a href="#" runat="server" visible="false" class="guardarCambios" id="btnServicios" onserverclick="CargarServicios">Guardar</a>


            <div class="divaux">
                <asp:GridView ID="GridView2" CssClass="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="detalle" HeaderText="Detalle" /> 
                        <asp:BoundField DataField="precio" HeaderText="Precio ($)" />
                        <asp:BoundField DataField="total" HeaderText="Total ($)" />
                        <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                        
                        <asp:CommandField SelectText="Eliminar" ButtonType="Button" ShowSelectButton="true" />
                        
                    </Columns>

                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
               
                </asp:GridView>

                

            </div>
            <asp:Label ID="lblpreciototal" runat="server" Text="Precio Total" Visible="false">
                <asp:Label ID="lblprecio" runat="server" Text="0" Visible="false"></asp:Label>

            </asp:Label>
            <a href="#" class="guardarCambios" runat="server" onserverclick="Avanzar" id="btnfinalizar" visible="false">Finalizar Presupuesto</a>
		</div>		

		<div class="contenedor2">
			
			<div class="vehiculo">

        		<div class="inputbtn">
				
				    <input type="text" runat="server" id="txtpatente" placeholder="Ingresar patente"/>  
				    <a href="#" runat="server" onserverclick="Unnamed_ServerClick1">Buscar</a>

			    </div>
				
				<input type="text" runat="server" id="txtmodelo" placeholder="Modelo" disabled visible="true">

                <div class="drop">   
                    <%-- Aqui empieza el drop --%>
                    <asp:DropDownList ID="Dmarca" CssClass="modelito" runat="server" DataSourceID="EMarca" DataTextField="nombre" DataValueField="id_marca" AutoPostBack="True" Visible="False"></asp:DropDownList>
                    <asp:EntityDataSource ID="EMarca" runat="server" ConnectionString="name=aplicadaBDEntities2" DefaultContainerName="aplicadaBDEntities2" EnableFlattening="False" EntitySetName="marca" Select="it.[id_marca], it.[nombre]"></asp:EntityDataSource>

                    <asp:DropDownList ID="Dmodelo" CssClass="modelito" runat="server" DataSourceID="SqlDataSource1" DataTextField="nombre" DataValueField="id_modelo" Visible="False"></asp:DropDownList>
                    <%-- Aqui termina el Drops --%>
                </div>



				<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:aplicadaBDConnectionString %>" SelectCommand="SELECT * FROM [modelo] WHERE ([id_marca] = @id_marca)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="Dmarca" Name="id_marca" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>



				<input type="text" runat="server" id="txtmarca" placeholder="Marca" disabled visible="true">
				<input type="text" runat="server" id="txtaño" placeholder="Año" disabled>

				<div class="inputbtn">
					<input type="text" placeholder="Titular Dni" id="txtdni" runat="server" >
				<a href="#" runat="server" onserverclick="BuscarCliente" visible="false" id="btnAgregarcliente">Agregar </a>
                 <a href="#" runat="server"  visible="false" id="A1" onserverclick="BuscarCliente">Modificar </a>

			</div>

			</div>

			<div class="duenio">
				
				<input type="text" placeholder="Apellido" id="txtapellido" runat="server" disabled="disabled">
				<input type="text" placeholder="Nombre" id="txtnombre" runat="server" disabled>
				
				<input type="text" placeholder="Telefono" id="txttelefono" runat="server" disabled>
				<input type="text" placeholder="E-Mail" id="txtemail" runat="server" disabled> 
                <a href="#" runat="server"  visible="false" id="btnGuardar" onserverclick="CargaryAvanzar" class="guardarCambios">Guardar</a>

			</div>

		</div>

	</div>

	<div class="btnGuardar">
		
		<a href="#" class="guardarCambios" runat="server" onserverclick="Cancelar">Cancelar</a>
		<asp:DropDownList ID="DropMecanicosDispo" runat="server" DataSourceID="DMecanicos" DataTextField="nombreyapellido" DataValueField="id_empleado" CssClass="select modelitoo" ></asp:DropDownList>
         <asp:SqlDataSource ID="DMecanicos" runat="server" ConnectionString="<%$ ConnectionStrings:aplicadaBDConnectionString %>" SelectCommand="SELECT [nombreyapellido], [id_empleado], [id_tipo] FROM [empleado] WHERE ([id_tipo] = 1) AND ([disponibilidad] = 0)"></asp:SqlDataSource>
        <a href="#" class="guardarCambios" runat="server"  id="btnpasartaller" visible="false" onserverclick="btnpasarataller_ServerClick">Pasar a taller</a>
		

	</div>

 </form>   

</asp:Content>
