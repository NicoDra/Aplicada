<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="CargarDetalle.aspx.cs" Inherits="AplicandoAplicada.CargarDetalle" %>
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

            <div class="arribaCargarDetalle">

                <input type="text" runat="server" placeholder="INGRESAR NUMERO DE ORDEN" id="txtidorden"/>
                <a href="#" runat="server" class="guardarCambios" onserverclick="BtnBuscar">Buscar Datos</a> <%--//onserverclick="CargarServicios"--%>

            </div>

            <div class="contenedor1">

                <div class="contenedorServicios">
           
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
				
                            <input type="text" runat="server" id="txtpatente" placeholder="Patente" disabled>

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

                            <input type="text" placeholder="Titular Dni" id="txtdni" runat="server" disabled>

                        </div>

                    </div>

                    <div class="duenio">
				
                        <input type="text" placeholder="Apellido" id="txtapellido" runat="server" disabled>
                        <input type="text" placeholder="Nombre" id="txtnombre" runat="server" disabled>
				
                        <input type="text" placeholder="Telefono" id="txttelefono" runat="server" disabled>
                        <input type="text" placeholder="E-Mail" id="txtemail" runat="server" disabled>

                    </div>

                </div>

            </div>

            <div class="btnGuardar">
		
                <a href="#" class="guardarCambios">Imprimir</a>
                <a href="#" class="guardarCambios" runat="server" onserverclick="Avanzar">Pasar a taller</a> 

            </div>

        </form>
</asp:Content>
