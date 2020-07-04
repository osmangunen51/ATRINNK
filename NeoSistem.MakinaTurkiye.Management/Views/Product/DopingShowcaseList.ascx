﻿<%@ Control Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewUserControl<List<ProductDopingListModel>>" %>
<% int row = 0; %>
<% foreach (var item in Model)
   { %>
   <% row++; %>
    <tr id="row<%: item.ProductId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
       <td class="CellBegin">
            <%: item.CategoryBreadCrumb%>
       </td>  
        <td class="Cell">
           <%:item.StoreShortName %>
       </td>
       <td class="Cell">
             <%string productUrl =  Helpers.ProductUrl(item.ProductId,item.ProductName); %>
        <a href="<%:productUrl %>"><%: item.ProductNo%></a> 
       </td>
        
          <td class="Cell">
            <%: item.ProductName%>
       </td>
  
        <td class="Cell">
            <%: item.BrandName%>
       </td>
       <td class="Cell">
           <%string beginDate = item.ProductDopingBeginDate != null ? item.ProductDopingBeginDate.ToDateTime().ToString("dd.MM.yyyy") : ""; %>
         <%:beginDate %>
       </td>
       <td class="Cell">
                       <%string endDate = item.ProductDopingEndDate != null ? item.ProductDopingEndDate.ToDateTime().ToString("dd.MM.yyyy") : ""; %>
         <%:endDate %>
       </td>
       <td class="Cell">
           
       </td>
       <td class="CellEnd">
                 <a title="Düzenle" style="cursor: pointer;" href="/Product/Edit/<%:item.ProductId %>">
              <img src="/Content/images/edit.png" hspace="2" />
            </a>
           <a title="Sil" style="cursor: pointer;" onclick="DeletePost(<%: item.ProductId %>);">
              <img src="/Content/images/delete.png" hspace="2" />
            </a>
       </td>
    </tr>
 <% } %>


<script type="text/javascript">
    function DeletePost(productId){
              if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/Product/ProductDopingDelete',
          data: { id: productId },
          type: 'post',
          dataType: 'json',
          success: function (data) {
           
            if (data) {
              $('#row' + productId).hide();
            }
          }
        });
      }
}
</script>