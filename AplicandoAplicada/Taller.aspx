<%@ Page Title="" Language="C#" MasterPageFile="~/Pagina.Master" AutoEventWireup="true" CodeBehind="Taller.aspx.cs" Inherits="AplicandoAplicada.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoPrincipal" runat="server">

    <form id="form1" runat="server" class="FormDetalle">

        <div class="contenedor">
		
                <div class="orden">
				
				    <div class="datos">

					    <label for="#">PATENTE: </label>
					    <label for="#">MODELO: </label>					

				    </div>

				    <div class="servicios">

                        <asp:GridView ID="GridView1" runat="server"></asp:GridView>

				    </div>


			    </div>

                <div class="ContenedorTalleristas">
                  
                      <input type="password" placeholder="ingresar contraseña para confirmar"  runat="server" id="txtpwd"/>
    
                </div>

			    <div class="btnTaller">
				
				    <a href="#">Imprimir</a>
				    <a href="#">Aceptar</a>
				    <a href="#">Finalizar</a>

			    </div>

	    </div>

    </form>

</asp:Content>
