<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IndexViewModel>" %>
<%@ Import Namespace="System.Globalization"%>
<%@ Import Namespace="Zarabizi.Models.UserAdministration"%>
<%@ Import Namespace="PagedList"%>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Adminsitracion de usuarios
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

	<link href='<% =Url.Content("~/Content/MvcMembership/MvcMembership.css") %>' rel="stylesheet" type="text/css" />

    <h2>Administracion de usuarios</h2>
    
    <h3>Usuarios</h3>
    <div class="mvcMembership-allUsers">
    <% if(Model.Users.Count > 0){ %>
		<ul class="users">
			<% foreach(var user in Model.Users){ %>
			<li>
				<span class="username"><% =Html.ActionLink(user.UserName, "Details", new{ id = user.ProviderUserKey}) %></span>
				<span class="email"><a href="mailto:<% =Html.Encode(user.Email) %>"><% =Html.Encode(user.Email) %></a></span>
				<% if(user.IsOnline){ %>
					<span class="isOnline">Conectado</span>
				<% }else{ %>
					<span class="isOffline">Desconectado desde hace
						<%
							var offlineSince = (DateTime.Now - user.LastActivityDate);
							if (offlineSince.TotalSeconds <= 60) Response.Write("1 minuto.");
							else if(offlineSince.TotalMinutes < 60) Response.Write(Math.Floor(offlineSince.TotalMinutes) + " minuto.");
							else if (offlineSince.TotalMinutes < 120) Response.Write("1 hora.");
							else if (offlineSince.TotalHours < 24) Response.Write(Math.Floor(offlineSince.TotalHours) + " horas.");
							else if (offlineSince.TotalHours < 48) Response.Write("1 día.");
							else Response.Write(Math.Floor(offlineSince.TotalDays) + " días.");
						%>
					</span>
				<% } %>
				<% if(!string.IsNullOrEmpty(user.Comment)){ %>
					<span class="comment"><% =Html.Encode(user.Comment) %></span>
				<% } %>
			</li>
			<% } %>
		</ul>
		<ul class="paging">

			<% if (Model.Users.IsFirstPage){ %>
			<li>Primero</li>
			<li>Anterior</li>
			<% }else{ %>
			<li><% =Html.ActionLink("Primero", "Index") %></li>
			<li><% =Html.ActionLink("Anterior", "Index", new { index = Model.Users.PageIndex - 1 })%></li>
			<% } %>

			<li>Página <% =Model.Users.PageNumber%> de <% =Model.Users.PageCount%></li>

			<% if (Model.Users.IsLastPage){ %>
			<li>Siguiente</li>
			<li>Último</li>
			<% }else{ %>
			<li><% =Html.ActionLink("Siguiente", "Index", new { index = Model.Users.PageIndex + 1 })%></li>
			<li><% =Html.ActionLink("Último", "Index", new { index = Model.Users.PageCount - 1 })%></li>
			<% } %>
		</ul>
	<% }else{ %>
		<p>No tiene usuarios registrados.</p>
	<% } %>
	</div>

	<h3>Roles</h3>
	<div class="mvcMembership-allRoles">
	<% if(Model.Roles.Count() > 0 ){ %>
		<ul>
			<% foreach(var role in Model.Roles){ %>
			<li>
				<% =Html.ActionLink(role, "Role", new{id = role}) %>
				<% using(Html.BeginForm("DeleteRole", "UserAdministration", new{id=role})){ %>
				<input type="submit" value="Eliminar" />
				<% } %>
			</li>
			<% } %>
		</ul>
	<% }else{ %>
		<p>No hay roles creados.</p>
	<% } %>
	<% using(Html.BeginForm("CreateRole", "UserAdministration")){ %>
		<fieldset>
			<label for="id">Role:</label>
			<% =Html.TextBox("id") %>
			<input type="submit" value="Crear rol" />
		</fieldset>
	<% } %>
	</div>

</asp:Content>