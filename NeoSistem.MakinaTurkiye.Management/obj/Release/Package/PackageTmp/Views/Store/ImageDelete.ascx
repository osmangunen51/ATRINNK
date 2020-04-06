<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ControlModel>" %>
<% if (Model.IsImage)
   {%>
<span style="color: Green">
  <%= Model.Text %></span>
<%= Ajax.ActionLink(Model.ImageDeleted ? "Vazgeç" : "Resimi Sil", "ImageDelete", new { deleted = Model.ImageDeleted }, new AjaxOptions { UpdateTargetId = "deleteImage" })%>
<% }   %>
