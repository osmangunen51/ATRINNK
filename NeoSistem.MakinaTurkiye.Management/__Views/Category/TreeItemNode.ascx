<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<TreeViewNode>>" %>
<% foreach (var item in Model)
   { %>
<li>
  <%: item.text %><%= item.tool %>
  <% if (item.hasChildren)
     { %>
  <ul>
    <% Html.RenderPartial("TreeItemNode", Model); %>
  </ul>
  <% } %>
</li>
<% } %>