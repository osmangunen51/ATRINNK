﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ICollection<CategoryItemModelForPlace>>" %>
<% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++; %>
<tr id="row<%: item.CategoryPlaceChoiceId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%: item.CategoryName %>
  </td>
    <td class="Cell">
        <%:item.CategoryType %>
    </td>
     <td class="Cell">
         <div id="displayOrderWrap<%:item.CategoryPlaceChoiceId %>">
   <span id="orderWrap<%:item.CategoryPlaceChoiceId %>"> <%: item.Order %></span>
           <img title="Düzenle" src="/Content/images/edit.png" onclick="ShowEditOrder(<%:item.CategoryPlaceChoiceId %>)" />
             </div>
         <div style="display:none;" id="showEditOrderWrap<%:item.CategoryPlaceChoiceId %>">
             <input type="text" id="txtOrder<%:item.CategoryPlaceChoiceId %>" value="<%:item.Order %>" />
            <input type="button" onclick="EditOrder(<%:item.CategoryPlaceChoiceId%>)" value="Kaydet" />
         </div>
  </td>
    <td>
        <a href="javascript:void(0)" onclick="DeleteCategoryPlace(<%:item.CategoryPlaceChoiceId %>)">Sil</a>
    </td>
    </tr>
<%} %>
    <tr>
  <td class="ui-state ui-state-default" colspan="4" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
          <li><a href="">1</a></li>
          <li>2</li>
      </ul>
    </div>
  </td>
</tr>
