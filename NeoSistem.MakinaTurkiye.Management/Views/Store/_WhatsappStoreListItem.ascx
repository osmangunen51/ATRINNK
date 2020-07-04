<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Management.Models.WhatsappStoreModel>" %>

<%foreach (var item in Model.StoreWhatsappListItems.ToList())
 {%>
              <tr>
              <td class="Cell"><a target="_blank" href="/Store/EditStore/<%:item.MainPartyId %>"><%:item.StoreName %></a></td>
              <td class="Cell"><%:item.TotalCount %></td>
             </tr>

 <%} %>
<tr>
      <td class="ui-state ui-state-default" colspan="2" align="right" style="border-color: #DDD;
	 border-top: none; border-bottom: none;">
	 <div style="float: right;" class="pagination">
         <div style="float:left; margin-right:10px;">
      <b> <%:Model.TotalPage %></b> Sayfadan <%:Model.CurrentPage %> . sayfadasınız
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
   <td  class="ui-state ui-state-default" colspan="2" align="right" style="border-color: #DDD;
	 border-top: none; border-bottom: none;">
       <b>Toplam Tıklanma Sayısı:</b><%:ViewData["TotalClick"] %>
   </td>
</tr>