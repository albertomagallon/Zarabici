<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<HandleErrorInfo>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Error: Rol duplicado
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <h2>Error: Rol duplicado</h2>
    <p>Duplicado rol por <em><% =Model.ControllerName %>.<% =Model.ActionName %></em>.</p>
    <p>Esta excepción se produce cuando el proveedor de la función devuelve una lista de papeles que contienen duplicados. Si está utilizando el valor predeterminado Asp.Net rol de proveedor y base de datos, revisar sus <strong>ASPNETDB</strong> database's <strong>aspnet_Roles</strong> tabla de duplicados en el <strong>RoleName</strong> column.</p>

</asp:Content>