﻿<%@ Control Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewUserControl<ICollection<MakinaTurkiye.Entities.Tables.Common.AddressChangeHistory>>" %>
<% int row = 0; %>
	<%MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities(); %>

<% foreach (var item in Model)
	{ %>
<%
    %>
<% row++; %>
<tr id="row<%: item.AddressChangeHistoryId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
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

       <%var country = entities.Countries.FirstOrDefault(x=>x.CountryId==item.CountryId); %>
      <%var city = entities.Cities.FirstOrDefault(x=>x.CityId==item.CityId); %>
      <% var locality = entities.Localities.FirstOrDefault(x=>x.LocalityId==item.LocalityId); %>
      <%District district=null;
          var town = entities.Towns.FirstOrDefault(x=>x.TownId==item.TownId); %>
      <%if(town!=null) {%>
      <% district = entities.Districts.FirstOrDefault(x=>x.DistrictId==town.DistrictId); %>
      <%}
       %>
      <%  StringBuilder builder = new StringBuilder();
          if(town!=null)
          builder.AppendFormat("{0} ", 
             town.TownName);
          builder.AppendFormat("{0} ", item.Avenue);

          if (!string.IsNullOrWhiteSpace(item.Street))
          {
              builder.AppendFormat("{0} ", item.Street);
          }

          if (!string.IsNullOrWhiteSpace(item.ApartmentNo))
          {
              builder.AppendFormat("No: {0} ", item.ApartmentNo);
          }

          if (!string.IsNullOrWhiteSpace(item.DoorNo))
          {
              builder.AppendFormat("/ {0} ", item.DoorNo);
          }
             
          builder.AppendFormat("{1} {0} {2} / {3}", locality != null ? locality.LocalityName : "", district != null ? district.ZipCode : "", city != null ? city.CityName : "-", country != null ? country.CountryName : "-"); %>
      <%:builder %>
  </td>
    <td>
        <%var address = entities.Addresses.FirstOrDefault(x=>x.AddressId==item.AddressId); %>
        <%string address1=EnumModels.AddressEditForOrder(address); %>
        <%:address1 %>
    </td>
    <td><%:item.UpdatedDate.ToString("dd/MM/yyyy HH:MM") %></td>
  <td class="CellEnd" align="center">
       <a href="javascript:void(0)" onclick="DeletePost(<%:item.AddressChangeHistoryId %>)">Sil</a>
  </td>
</tr>
<% } %>
