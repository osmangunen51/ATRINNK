﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.FavoriteListItemModel>>" %>

<%foreach (var item in Model.Source.ToList())
 {%>
              <tr>
                <td class="Cell CellBegin">
                    <%:item.ProductNo %>
                </td>
                  <td class="Cell">
                      <a target="_blank" href="/Product/Edit/<%:item.ProductId %>">
                      <%:item.ProductName %>
                          </a>

                  </td>
                   <td class="Cell">
                       <a href="/Store/StoreDetailInformation/<%:item.ReceiverStoreMainPartyId %>" target="_blank">
                       <%:item.ReceiverStoreName %>
                           </a>
                   </td>
                  <td class="Cell CellEnd"><%:item.AdedMemberName %></td>
       
             </tr>

 <%} %>
<tr>
      <td class="ui-state ui-state-default" colspan="4" align="right" style="border-color: #DDD;
	 border-top: none; border-bottom: none;">
	 <div style="float: right;" class="pagination">
         <div style="float:left; margin-right:10px;">
      <b> <%:Model.CurrentPage %> . sayfadasınız
             </div>
		<ul style="float:right;">
         
		  <% foreach (int i in Model.TotalLinkPages )
       { %>
		  <li>
			 <% if (i == Model.CurrentPage)
       { %>
			 <span class="currentpage">
				<%: i%></span>&nbsp;
			 <% } %>
			 <% else
       { %>
			 <a href="javascript:void(0)" onclick="PagingStore(<%:i %>)">
				<%: i%></a>&nbsp;
			 <% } %>
		  </li>
		  <% } %>

      
		</ul>
	 </div>
  </td>

</tr>
<tr>
   <td  class="ui-state ui-state-default" colspan="4" align="right" style="border-color: #DDD;
	 border-top: none; border-bottom: none;">
       <b>Toplam Adet:</b><%:Model.TotalRecord %>
   </td>
</tr>