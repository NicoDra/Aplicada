<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="Caja.aspx.cs" Inherits="AplicandoAplicada.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

<form id="form1" runat="server" class="FormDetalle">
    

    <div class="contenedorCaja">
		
			<div class="buscarOrden">
				
				<input type="text" placeholder="INGRESAR NUMERO DE ORDEN" runat="server" id="txtorden"/>
				<a href="#" runat="server" onserverclick="BtnBuscarO" id="btnbuscarorden">Buscar</a>
                <%--<asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>--%>
			</div>

			<%--<div class="buscarOrden">
				
				<input type="text" placeholder="INGRESAR DNI DEL CLIENTE"/>
				<a href="#" runat="server" onserverclick="BtnBuscar">Buscar</a>

			</div>--%>

			<div class="buscarOrden">
				
				<input type="text" placeholder="INGRESAR PATENTE" runat="server" id="txtpatente" />
				<a href="#" runat="server" onserverclick="BtnBuscarP" id="btnbuscarpatente">Buscar</a>

			</div>

			<!-- PONER DROPDOWN LIST PARA PONER LOS TIPOS DE PAGO -->
			
			<div class="orden">
				
				<div class="datos">

					    <asp:Label ID="lblpatente" runat="server" Text="PATENTE: "></asp:Label>
                        <asp:Label ID="lblmodelo" runat="server" Text="MODELO: "></asp:Label>		
                    <asp:Label ID="lblprecio" runat="server" Text="PRECIO TOTAL: "></asp:Label>				

				</div>

				<div class="servicios">

					<div class="divaux">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="gridTaller">
                            <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="detalle" HeaderText="Detalle" >
                       
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="precio" HeaderText="Precio ($)" > 
                            <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        
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


			</div>

			<div class="btnTaller">
				
				<a href="#">Imprimir</a>
				<a href="#" runat="server" onserverclick="BtnCobrar">Cobrar</a>

			</div>

	</div>

</form>

</asp:Content>
