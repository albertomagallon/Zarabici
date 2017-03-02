<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<DetailsViewModel>" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="Zarabizi.Models.UserAdministration"%>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Detalles del usuario: <% =Html.Encode(Model.DisplayName) %>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

	<link href='<% =Url.Content("~/Content/MvcMembership/MvcMembership.css") %>' rel="stylesheet" type="text/css" />

    <h2>Detalles del usuario: <% =Html.Encode(Model.DisplayName) %> [<% =Model.Status %>]</h2>

	<h3>Cuenta</h3>
	<div class="mvcMembership-account">
		<dl>
			<dt>Nombre de usuario:</dt>
				<dd><% =Html.Encode(Model.User.UserName) %></dd>
			<% if(Model.User.LastActivityDate == Model.User.CreationDate){ %>
			<dt>�ltima actividad:</dt>
				<dd><em>Nunca</em></dd>
			<dt>�ltima autentificaci�n:</dt>
				<dd><em>Nunca</em></dd>
			<% }else{ %>
			<dt>�ltima actividad:</dt>
				<dd><% =Model.User.LastActivityDate.ToString("MMMM dd, yyyy h:mm:ss tt", CultureInfo.InvariantCulture) %></dd>
			<dt>�ltima autentifiaci�n:</dt>
				<dd><% =Model.User.LastLoginDate.ToString("MMMM dd, yyyy h:mm:ss tt", CultureInfo.InvariantCulture) %></dd>
			<% } %>
			<dt>Creado:</dt>
				<dd><% =Model.User.CreationDate.ToString("MMMM dd, yyyy h:mm:ss tt", CultureInfo.InvariantCulture) %></dd>
		</dl>

		<% using(Html.BeginForm("ChangeApproval", "UserAdministration", new{ id = Model.User.ProviderUserKey })){ %>
			<% =Html.Hidden("isApproved", !Model.User.IsApproved) %>
			<input type="submit" value='<% =(Model.User.IsApproved ? "Unapprove" : "Approve") %> Account' />
		<% } %>
		<% using(Html.BeginForm("DeleteUser", "UserAdministration", new{ id = Model.User.ProviderUserKey })){ %>
			<input type="submit" value="Eliminar cuenta" />
		<% } %>
	</div>

	<h3>Correo electr�nico & Comentarios</h3>
	<div class="mvcMembership-emailAndComments">
		<% using(Html.BeginForm("Details", "UserAdministration", new{ id = Model.User.ProviderUserKey })){ %>
		<fieldset>
			<p>
				<label for="User_Email">Direcci�n de correo electr�nico:</label>
				<% =Html.TextBox("User.Email") %>
			</p>
			<p>
				<label for="User_Comment">Comentarios:</label>
				<% =Html.TextArea("User.Comment") %>
			</p>
			<input type="submit" value="Guardar correo electr�nico y comentario" />
		</fieldset>
		<% } %>
	</div>

	<h3>Contrase�a</h3>
	<div class="mvcMembership-password">
		<% if(Model.User.IsLockedOut){ %>
			<p>Bloqueado desde <% =Model.User.LastLockoutDate.ToString("MMMM dd, yyyy h:mm:ss tt", CultureInfo.InvariantCulture) %></p>
			<% using(Html.BeginForm("Unlock", "UserAdministration", new{ id = Model.User.ProviderUserKey })){ %>
			<input type="submit" value="Desbloquear cuenta" />
			<% } %>
		<% }else{ %>

			<% if(Model.User.LastPasswordChangedDate == Model.User.CreationDate){ %>
			<dl>
				<dt>�ltimo cambio:</dt>
				<dd><em>Nunca</em></dd>
			</dl>
			<% }else{ %>
			<dl>
				<dt>�ltimo cambio:</dt>
				<dd><% =Model.User.LastPasswordChangedDate.ToString("MMMM dd, yyyy h:mm:ss tt", CultureInfo.InvariantCulture) %></dd>
			</dl>
			<% } %>

			<% using(Html.BeginForm("ResetPassword", "UserAdministration", new{ id = Model.User.ProviderUserKey })){ %>
			<fieldset>
				<p>
					<dl>
						<dt>Pregunta de contrase�a de recuperaci�n:</dt>
						<% if(string.IsNullOrEmpty(Model.User.PasswordQuestion) || string.IsNullOrEmpty(Model.User.PasswordQuestion.Trim())){ %>
						<dd><em>No hay una pregunta de recuperaci�n de contrase�a definida.</em></dd>
						<% }else{ %>
						<dd><% =Html.Encode(Model.User.PasswordQuestion) %></dd>
						<% } %>
					</dl>
				</p>
				<p>
					<label for="answer">Respuesta de contrase�a de recuperaci�n:</label>
					<% =Html.TextBox("answer") %>
				</p>
				<input type="submit" value="Resetar contrase�a" />
			</fieldset>
			<% } %>

		<% } %>
	</div>

	<h3>Roles</h3>
	<div class="mvcMembership-userRoles">
		<ul>
			<% foreach(var role in Model.Roles){ %>
			<li>
				<% =Html.ActionLink(role.Key, "Role", new{id = role.Key}) %>
				<% if(role.Value){ %>
					<% using(Html.BeginForm("RemoveFromRole", "UserAdministration", new{id = Model.User.ProviderUserKey, role = role.Key})){ %>
					<input type="submit" value="Remove From" />
					<% } %>
				<% }else{ %>
					<% using(Html.BeginForm("AddToRole", "UserAdministration", new{id = Model.User.ProviderUserKey, role = role.Key})){ %>
					<input type="submit" value="Add To" />
					<% } %>
				<% } %>
			</li>
			<% } %>
		</ul>
		</div>

</asp:Content>