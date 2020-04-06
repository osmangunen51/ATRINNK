﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<MessageModel>>" %>
<% int row = 0; %>
<%   var entities = new MakinaTurkiyeEntities(); %>
<% foreach (var item in Model.Source)
   { %>
  <%var checkmessage = entities.messagechecks.Where(c => c.MessageId == item.MessageId).FirstOrDefault(); %> 
<% row++; %>
  <%if (checkmessage == null)
    {  %>
<tr id="row<%: item.MessageId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  
  <td class="CellBegin">
    <div style="overflow: hidden; float: left; height: 15px;font-weight:bold;">
      <%
      var memberFrom = entities.Members.SingleOrDefault(c => c.MainPartyId == item.MainPartyId);
      %>
      <%:memberFrom.MemberName + " " + memberFrom.MemberSurname%>
   
    </div>
  </td>
  <td class="Cell">
    <%
      var memberTo = entities.Members.SingleOrDefault(c => c.MainPartyId == item.InOutMainPartyId);
        
    %>
    <span style="font-weight:bold;">
    <%:memberTo.MemberName + " " + memberTo.MemberSurname%>
       </span>
       <%if (!string.IsNullOrEmpty(item.ToSecondName)) {%>
       <a href="/Store/StoreDetailInformation/<%:item.ToMainPartyId %>" target="_blank" title="<%:item.ToSecondName %>"> (
      <%:item.ToSecondName %>)
           </a>
      <% } %>
<%--       <%if (memberTo.MemberType == 20)
         {  %>
         /
      <%var storid = entities.MemberStores.Where(c => c.MemberMainPartyId == item.InOutMainPartyId).SingleOrDefault().StoreMainPartyId; %>
     <%var storename = entities.Stores.Where(c => c.MainPartyId == storid).SingleOrDefault().StoreName; %>
      <%if (storename != null)
        {  %>
      <%:storename.ToString()%>
      <%} %>
      <%} %>--%>
  </td>
  <td class="Cell">
  <span style="font-weight:bold;">
    <%:item.MessageSubject%>
    </span>
  </td>
  <td class="Cell">
  <span style="font-weight:bold;">
    <%: item.MessageDate.ToString("dd MMMM yyyy dddd")%>
    </span>
  </td>
  <td class="CellEnd">
      <span style="float:left">
     <%if (item.MessageSeenAdmin == true)
         {%>
            <img  src="/Content/Images/Goodshield.png" >
      <% }
         else { %>
         <a id="clickButton<%:item.MessageId %>"  onclick="UpdateDataSeen(<%:item.MessageId %>);" >Arandı</a>
      <%} %>

          <img id="imgCheck<%:item.MessageId %>"   src="/Content/Images/Goodshield.png" style="display:none;"/>
          </span>
    <a href="/Message/View/<%: item.MessageId %>"  style="padding-bottom: 5px; margin-left:5px; float:left;">
      <div style="float: left; margin-right: 10px">
        <img src="/Content/images/product.png" />
      </div>
    </a>

  </td>
</tr>
<%} %>
<%else {  %>
<tr id="Tr1" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  
  <td class="CellBegin">
    <div style="overflow: hidden; float: left; height: 15px;">
      <%
      var memberFrom = entities.Members.SingleOrDefault(c => c.MainPartyId == item.MainPartyId);
      %>
      <%:memberFrom.MemberName + " " + memberFrom.MemberSurname%>
   
    </div>
  </td>
  <td class="Cell">
    <%
      var memberTo = entities.Members.SingleOrDefault(c => c.MainPartyId == item.InOutMainPartyId);
    %>
  
    <%:memberTo.MemberName + " " + memberTo.MemberSurname%>
              <%if (!string.IsNullOrEmpty(item.ToSecondName)) {%>
       <a href="/Store/StoreDetailInformation/<%:item.ToMainPartyId %>" target="_blank" title="<%:item.ToSecondName %>"> (
      <%:item.ToSecondName %>)
           </a>
      <% } %>
<%--       <%if (memberTo.MemberType == 20)
         {  %>
         /
      <%var storid = entities.MemberStores.Where(c => c.MemberMainPartyId == item.InOutMainPartyId).SingleOrDefault().StoreMainPartyId; %>
     <%var storename = entities.Stores.Where(c => c.MainPartyId == storid).SingleOrDefault().StoreName; %>
      <%if (storename != null)
        {  %>
      <%:storename.ToString()%>
      <%} %>
      <%} %>--%>
  </td>
  <td class="Cell">

    <%:item.MessageSubject%>
    
  </td>
  <td class="Cell">
    <%: item.MessageDate.ToString("dd MMMM yyyy dddd")%>
  </td>
  <td class="CellEnd">
      <span style="float:left">
     <%if (item.MessageSeenAdmin == true)
         {%>
            <img  src="/Content/Images/Goodshield.png" >
      <% }
         else { %>
         <a id="clickButton<%:item.MessageId %>"  onclick="UpdateDataSeen(<%:item.MessageId %>);" >Arandı</a>
      <%} %>

          <img id="imgCheck<%:item.MessageId %>"   src="/Content/Images/Goodshield.png" style="display:none;"/>
          </span>
    <a href="/Message/View/<%: item.MessageId %>"  style="padding-bottom: 5px; margin-left:5px; float:left;">
      <div style="float: left; margin-right: 10px">
        <img src="/Content/images/product.png" />
      </div>
    </a>
  </td>
</tr>
<%} %>
<% } %>
<% if (Model.TotalRecord <= 0)
   { %>
<tr class="Row">
  <td colspan="5" class="CellBegin Cell" style="color: #FF0000; padding: 5px; font-weight: 700;
    font-size: 14px;">
    Mesaj bulunamadı.
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="5" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
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
      </ul>
    </div>
  </td>
</tr>
<script type="text/javascript">



</script>