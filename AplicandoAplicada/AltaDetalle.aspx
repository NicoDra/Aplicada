<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="AltaDetalle.aspx.cs" Inherits="AplicandoAplicada.AltaDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <form id="form1" runat="server" class="FormDetalle">

 <div class="contenedor1">

		<div class="contenedorServicios">
           
            <div class="divaux">
                <asp:GridView ID="GridView1" CssClass="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="id_servicios" HeaderText="Id_Servicio"  > 
                        
                        <ItemStyle Width="0px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="detalle" HeaderText="Detalle" /> 
                        <asp:BoundField DataField="precio" HeaderText="Precio ($)" />
                        <asp:TemplateField HeaderText="Selecionar">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkRow" runat="server" style="text-align: center" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        
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


        <a href="#" runat="server" visible="false" class="guardarCambios" id="btnServicios" onserverclick="CargarServicios">Guardar</a>


            <div class="divaux">
                <asp:GridView ID="GridView2" CssClass="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="detalle" HeaderText="Detalle" /> 
                        <asp:BoundField DataField="precio" HeaderText="Precio ($)" />
                        
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


		</div>		

		<div class="contenedor2">
			
			<div class="vehiculo">

        		<div class="inputbtn">
				
				    <input type="text" runat="server" id="txtpatente" placeholder="Ingresar patente">  
				    <a href="#" runat="server" onserverclick="Unnamed_ServerClick1">Buscar</a>

			    </div>
				
				<input type="text" runat="server" id="txtmodelo" placeholder="Modelo" disabled visible="true">

                <div class="drop">   
                    <%-- Aqui empieza el drop --%>
                    <asp:DropDownList ID="Dmodelo" CssClass="Dmodelo" runat="server" DataSourceID="DMarca" DataTextField="nombre" DataValueField="id_marca" AutoPostBack="True" Visible="false"></asp:DropDownList>
                        <asp:SqlDataSource ID="DMarca" runat="server" ConnectionString="<%$ ConnectionStrings:aplicadaBDConnectionString %>" SelectCommand="SELECT DISTINCT [nombre], [id_marca] FROM [marca]"></asp:SqlDataSource>

                    <asp:DropDownList ID="modelito" CssClass="modelito" runat="server" DataSourceID="Dmodelado" DataTextField="nombre" DataValueField="id_modelo" Visible="false"></asp:DropDownList>
                        <asp:SqlDataSource ID="Dmodelado" runat="server" ConnectionString="<%$ ConnectionStrings:aplicadaBDConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT DISTINCT * FROM [modelo] WHERE ([id_marca] = @id_marca)">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="Dmodelo" Name="id_marca" PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    <%-- Aqui termina el Drops --%>
                </div>



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
		<a href="#" class="guardarCambios">Imprimir</a>
        
		<a href="#" class="guardarCambios" runat="server" onserverclick="Avanzar">Pasar a taller</a>

	</div>

 </form>   

</asp:Content>
