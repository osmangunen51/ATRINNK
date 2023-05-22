<%@ Control Language="C#" Inherits="NeoSistem.Trinnk.Core.Web.ViewUserControl<FilterModel<ProductModel>>" %>
<% int row = 0; %>
<% foreach (var item in Model.Source)
   { %>
<% row++; %>
<tr id="row<%: item.ProductId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%: item.ProductNo%>
  </td>
  <td class="Cell">
    <span title="<%: item.ProductName %>">
      <%: item.ProductName%></span>
  </td>
  <td class="Cell">
    <%= item.MainCategoryName %>
  </td>
  <td class="Cell">
    <%: item.NameBrand%>
  </td>
  <td class="Cell">
    <%: item.NameSeries%>
  </td>
  <td class="Cell">
    <%: item.NameModel%>
  </td>
  <td class="Cell">
    <%: item.ViewCount%>
  </td>
  <td class="Cell">
    <%: item.SingularViewCount%>
  </td>
</tr>
<% } %>
<% if (Model.TotalRecord <= 0)
   { %>
<tr class="Row">
  <td colspan="8" class="CellBegin Cell" style="color: #FF0000; padding: 5px; font-weight: 700;
    font-size: 14px;">
    Ürün bulunamadı.
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="8" align="left" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
        &nbsp;Sayfa&nbsp;&nbsp;
        <% foreach (int page in Model.TotalLinkPages)
           { %>
        <li>
          <% if (page == Model.CurrentPage)
             { %>
          <span class="currentpage">
            <%: page %></span>&nbsp;
          <% } %>
          <% else
             { %>
          <a onclick="PagePost(<%: page %>)">
            <%: page %></a>&nbsp;
          <% } %>
        </li>
        <% } %>
        <li>Gösterim: </li>
        <li>
          <select id="PageDimension" name="PageDimension" onchange="SearchPost();">
            <option value="20" <%: Session["product_PAGEDIMENSION"].ToString() == "20" ? "selected=selected" : "" %>>
              20</option>
            <option value="50" <%: Session["product_PAGEDIMENSION"].ToString() == "50" ? "selected=selected" : "" %>>
              50</option>
            <option value="100" <%: Session["product_PAGEDIMENSION"].ToString() == "100" ? "selected=selected" : "" %>>
              100</option>
          </select>
        </li>
      </ul>
    </div>
  </td>
</tr>
