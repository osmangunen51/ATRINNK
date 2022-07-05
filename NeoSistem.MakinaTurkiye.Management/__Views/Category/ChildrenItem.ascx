<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CategoryModel>" %>
<% string className = "child-of-node-" + Model.CategoryParentId;

   if (Model.CategoryGroupType > 0)
   {
     var categoryGroup = CategoryProductGroupModel.ProductGroups().Single(c => c.CategoryProductGroupId == Model.CategoryGroupType);

     className = "child-of-" + categoryGroup.GroupName + "-" + Model.CategoryParentId.ToString();
   }

   string color = ""; %>
<% if (Model.CategoryType == 3)
   {  %>
<% color = "RowMark"; %>
<% } %>
<% if (Model.CategoryType == 4)
   {  %>
<% color = "RowSerie"; %>
<% } %>
<% if (Model.CategoryType == 5)
   {  %>
<% color = "RowModel"; %>
<% } %>
<%
  
  
  ICollection<CategoryModel> items = null;
  if (Model.CategoryId != 1)
  {
    var curCategory = new NeoSistem.MakinaTurkiye.Classes.Category(); 
    var categoryTreeName = TreeHelpers.TreeHelper(Model.CategoryId); 
    int sectorId = categoryTreeName.ToArray()[0].ToInt32(); 
    curCategory.LoadEntity(sectorId);
    items = CacheUtilities.CategoryOrderSector(curCategory).AsCollection<CategoryModel>();
  }
  else
  {
    var dataCategory = new NeoSistem.MakinaTurkiye.Data.Category();
    items = dataCategory.GetCategorySectorAndProductGroup().AsCollection<CategoryModel>();
  }

  //var items = CacheUtilities.GetCategories().AsCollection<CategoryModel>().Where(c => c.CategoryParentId == Model.CategoryId);  
  
  
%>
<tr rowid="<%: Model.CategoryId %>" id="node-<%: Model.CategoryId %>" class="context f <%: className %> parent collapsed Row <%: color %>" parentid="<%: Model.CategoryParentId %>" model="<%: items.Any(c => c.CategoryType == 5).ToString().ToLower() %>" serie="<%: items.Any(c => c.CategoryType == 4).ToString().ToLower() %>" brand="<%: items.Any(c => c.CategoryType == 3).ToString().ToLower() %>" category="<%: items.Any(c => c.CategoryType == 2).ToString().ToLower() %>" load="false" 
typeid="<%: Model.CategoryType %>" route="<%: Model.CategoryRoute %>">
  <td class="CellBegin">
    <span class="folder" id="rowCategoryName<%: Model.CategoryId %>">
      <%: Model.CategoryName%></span>
  </td>
  <td class="Cell" align="center">
    <a style="cursor: pointer;" onclick="DialogOpen('#node-<%: Model.CategoryId %>');">Alt Kategori Ekle</a>
  </td>
  <td class="Cell" align="center">
    <% if (Model.CategoryType == 3)
       { %>
    Marka
    <% } %>
    <% if (Model.CategoryType == 4)
       { %>
    Seri
    <% } %>
    <% if (Model.CategoryType == 5)
       { %>
    Model
    <% } %>
    <% if (Model.CategoryType == 0)
       { %>
    Sektor
    <% } %>
    <% if (Model.CategoryType == 1)
       { %>
    Ana Kategori
    <% } %>
    <% if (Model.CategoryType == 2)
       { %>
    Kategori
    <% } %>
  </td>
  <td class="Cell" align="center" id="rowCategoryOrder<%: Model.CategoryId %>">
    <%: Model.CategoryOrder %>
  </td>
  <td class="Cell" align="center">
    <img src="/content/images/<%: Model.Active ? "Goodshield.png" : "Errorshield.png" %>" alt="<%: Model.Active %>" title="<%: Model.Active ? "Aktif" : "Pasif" %>" />
  </td>
  <td class="Cell" align="center">
    <a style="cursor: pointer;" onclick="EditCategory('#node-<%: Model.CategoryId %>', <%: Model.CategoryId %>);">Düzenle</a>
  </td>
  <td class="Cell">
    &nbsp;&nbsp;&nbsp;<a style="cursor: pointer;" onclick="Delete(<%: Model.CategoryId %>);">Sil</a>
  </td>
</tr>
