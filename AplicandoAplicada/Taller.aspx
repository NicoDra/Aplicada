<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="Taller.aspx.cs" Inherits="AplicandoAplicada.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

<form id="form1" runat="server" class="FormDetalle">

    <div class="contenedor">
		
			<div class="ingresarOrden">
				
				<input type="text" placeholder="INGRESAR NUMERO DE ORDEN">
				<a href="#">Buscar</a>

			</div>
			
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
