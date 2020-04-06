﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<CategoryModel>>" %>
<% foreach (var item in Model)
   { %>
<% if (item.CategoryParentId == 1)
   { %>
<% 
  string guid = Guid.NewGuid().ToString("N");
  string className = "child-of-group-" + item.CategoryId;  %>
<tr rowid="<%: item.CategoryId %>" id="group-<%: item.CategoryId %>" class="f child-of-group-<%: item.CategoryParentId %> <%: (item.IsParent > 0 ? "parent":"") %> collapsed Row " parentid="<%: item.CategoryParentId %>" load="true" typeid="<%: item.CategoryType %>" route="<%: item.CategoryRoute %>">
  <td class="CellBegin">
    <span class="folder" id="rowCategoryName<%: item.CategoryId %>">
      <%: item.CategoryName %></span>
  </td>
  <td class="Cell" align="center">
    <a style="cursor: pointer;" onclick="DialogOpen('#group-<%: item.CategoryId %>');">Alt Kategori Ekle</a>
  </td>
  <td class="Cell" align="center">
    Kategori
  </td>
  <td class="Cell" align="center" id="rowCategoryOrder<%: item.CategoryId %>">
    <%: item.CategoryOrder %>
  </td>
  <td class="Cell" align="center">
    <img src="/content/images/<%: item.Active ? "Goodshield.png" : "Errorshield.png" %>" alt="<%: item.Active %>" title="<%: item.Active ? "Aktif" : "Pasif" %>" />
  </td>
  <td class="Cell" align="center">
    <a style="cursor: pointer;" onclick="EditCategory('#group-<%: item.CategoryId %>', <%: item.CategoryId %>);">Düzenle</a>
  </td>
  <td class="Cell">
    &nbsp;&nbsp;&nbsp;<a style="cursor: pointer;" onclick="Delete(<%: item.CategoryId %>);">Sil</a>
  </td>
</tr>
<% foreach (var group in CategoryProductGroupModel.ProductGroups())
   { %> 
<tr unselectable="on" group="<%: group.CategoryProductGroupId %>" rowid="<%: item.CategoryId %>" id="<%: group.GroupName  %>-<%: item.CategoryId %>" class="context <%: className %> parent collapsed Row " parentid="<%: item.CategoryParentId %>" load="false" route="<%: item.CategoryRoute %>" typeid="<%: item.CategoryType %>" >
  <td class="CellBegin">
    &nbsp;&nbsp;&nbsp;<%: group.CategoryProductGroupName %>
  </td>
  <td class="Cell" align="center">
    <a style="cursor: pointer;" onclick="DialogOpen('#group-<%: item.CategoryId %>');$('#CategoryGroupType').val(<%: group.CategoryProductGroupId %>);$('#CategoryType').val(1);$('#categoryTypeContainer').hide();">Alt Kategori Ekle</a>
  </td>
  <td class="Cell" align="center">
    Ürün Grubu
  </td>
  <td class="Cell" align="center">
  </td>
  <td class="Cell" align="center">
  </td>
  <td class="Cell" align="center">
  </td>
  <td class="Cell" align="center">
  </td>
</tr>
<% } %>
<% }
   else
   { %>
<%= Html.RenderHtmlPartial("ChildrenItem", item) %>
<% } %>
<% } %>