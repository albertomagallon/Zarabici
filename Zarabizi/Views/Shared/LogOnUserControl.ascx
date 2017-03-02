<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
    if (Request.IsAuthenticated) {
%>
        Bienvenido a Zarabizi <b><%= Html.Encode(Page.User.Identity.Name) %></b>!
        [ <%= Html.ActionLink("Salir", "LogOff", "Account", new { Area = "" },new { })%> ]
<%
    }
    else {
%> 
        [ <%= Html.ActionLink("Acceder", "LogOn", "Account", new { Area = "" }, new { })%> ]
<%
    }
%>
