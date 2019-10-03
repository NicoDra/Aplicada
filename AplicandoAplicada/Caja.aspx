<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="Caja.aspx.cs" Inherits="AplicandoAplicada.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

<form id="form1" runat="server" class="FormDetalle">
    

    <div class="contenedorCaja">
		
			<div class="buscarOrden">
				
				<input type="text" placeholder="INGRESAR NUMERO DE ORDEN">
				<a href="#">Buscar</a>

			</div>

			<div class="buscarOrden">
				
				<input type="text" placeholder="INGRESAR DNI DEL CLIENTE">
				<a href="#">Buscar</a>

			</div>

			<div class="buscarOrden">
				
				<input type="text" placeholder="INGRESAR PATENTE">
				<a href="#">Buscar</a>

			</div>

			<!-- PONER DROPDOWN LIST PARA PONER LOS TIPOS DE PAGO -->
			
			<div class="orden">
				
				<div class="datos">

					<label for="#">PATENTE</label>
					<label for="#">MODELO</label>					

				</div>

				<div class="servicios">

					<!-- PONER GRIS VIEW CON LOS SERVICIOS -->				

				</div>


			</div>

			<div class="btnTaller">
				
				<a href="#">Imprimir</a>
				<a href="#">Finalizar</a>

			</div>

	</div>

</form>

</asp:Content>
