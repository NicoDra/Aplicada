<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="AltaDetalle.aspx.cs" Inherits="AplicandoAplicada.AltaDetalle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

 <form id="form1" runat="server" class="FormDetalle">

 <div class="contenedor1">

		<div class="contenedorServicios">
			


		</div>		

		<div class="contenedor2">
			
			<div class="vehiculo">

        		<div class="inputbtn">
				
				    <input type="text" runat="server" id="txtpatente" placeholder="Ingresar patente">  
				    <a href="#" runat="server" onserverclick="Unnamed_ServerClick1">Buscar</a>

			    </div>
				
				<input type="text" runat="server" id="txtmodelo" placeholder="Modelo" disabled>
				<input type="text" runat="server" id="txtmarca" placeholder="Marca" disabled>
				<input type="text" runat="server" id="txtaño" placeholder="Año" disabled>

				<div class="inputbtn">
					<input type="text" placeholder="Titular Dni" id="txtdni" runat="server">
				<a href="#">Modificar</a>

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
		
		<a href="#" class="guardarCambios">Cancelar</a>
		<a href="#" class="guardarCambios">Imprimir</a>
		<a href="#" class="guardarCambios">Pasar a taller</a>

	</div>

 </form>   

</asp:Content>
