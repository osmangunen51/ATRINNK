﻿<%@ Control Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewUserControl<ICollection<MakinaTurkiye.Entities.Tables.Stores.StoreChangeHistory>>" %>
<% int row = 0; %>
<% foreach (var item in Model)
	{ %>
	<%MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities(); %>
<% row++;

    if (item.Store != null)
    {%>
<tr id="row<%:item.StoreChangeHistoryId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
	 <%: item.Store.StoreNo %>
  </td>

  <td class="Cell">
	 <%: item.Store.StoreName %>
  </td>
  <td class="Cell" align="center">
       <img itemprop="logo" style="border: solid 1px #b6b6b6;" width="70" height="50" src="<%= ImageHelpers.GetStoreImage(item.Store.MainPartyId,item.Store.StoreLogo,"100")%>" />
  </td>
  <td class="Cell">
	 <%
		 string packet = "";
         if (item.Store.StoreActiveType == 1)
		 {
			packet = "İnceleniyor";
		 }
         else if (item.Store.StoreActiveType == 2)
		 {
			packet = "Onaylandı";
		 }
		 else if (item.Store.StoreActiveType == 3)
		 {
			packet = "Onaylanmadı";
		 }
         else if (item.Store.StoreActiveType == 4)
		 {
			packet = "Silindi";
		 }
	 %>
	 <%: packet %>
  </td>
  <td class="Cell">
	 <a href="<%: EnumModels.UrlHttpEdit(item.Store.StoreWeb)%>" rel="external" onmousedown="this.target = '_blank';">
         
		<%: item.Store!=null && !string.IsNullOrEmpty(item.Store.StoreWeb)? item.Store.StoreWeb.Replace("http://","") : ""%></a>
  </td>

    <td><%:item.UpdatedDated.ToString("dd/MM/yyyy HH:MM") %></td>
  <td class="CellEnd" align="center">
       <a href="/ChangeLogs/storedetail?storeChangeHistoryId=<%:item.StoreChangeHistoryId %>">Karşılaştır</a>
      <a href="javascript:void(0)" onclick="DeletePost(<%:item.StoreChangeHistoryId %>)">Sil</a>
  </td>
</tr>

<%
    }

    %>

<% } %>
