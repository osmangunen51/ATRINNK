<%@ Control Language="C#" Inherits="NeoSistem.Trinnk.Core.Web.ViewUserControl<ICollection<Trinnk.Entities.Tables.Common.PhoneChangeHistory>>" %>
<% int row = 0; %>
<% foreach (var item in Model)
	{ %>
	<%TrinnkEntities entities = new TrinnkEntities(); %>
<% row++; %>
<tr id="row<%: item.PhoneChangeHistoryId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
    <td class="CellBegin"><%:row %></td>
    <td><%:item.MainPartyId %></td>
  <td >
      <%var member = entities.Members.FirstOrDefault(x=>x.MainPartyId==item.MainPartyId); %>
      <%if(member!=null) {%>
        <%:member.MemberName+member.MemberSurname %>
      <%} %>
  </td>

  <td >
	  <%var store = entities.Stores.FirstOrDefault(x=>x.MainPartyId==item.MainPartyId); %>
      <%if(store!=null){ %>
      <%:store.StoreName %>
      <%} %>
  </td>
  <td class="Cell" align="center">
       <%:item.PhoneCulture %>
  </td>
  <td class="Cell">
	 <%:item.PhoneAreaCode %>
  </td>
  <td class="Cell">
	<%:item.PhoneNumber %>
  </td>

    <td><%:Convert.ToDateTime(item.UpdatedDate).ToString("dd/MM/yyyy HH:MM") %></td>
  <td class="CellEnd" align="center">
       <a onclick="DeletePost(<%:item.PhoneChangeHistoryId %>)" href="javascript:void(0)">Sil</a>
  </td>
</tr>
<% } %>
