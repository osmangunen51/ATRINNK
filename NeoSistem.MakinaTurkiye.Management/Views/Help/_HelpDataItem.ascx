﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Management.Models.HelpListModel>" %>
<%int row = 0; %>
<%foreach (var item in Model.HelpModels.ToList())
    {%>
<tr id="row<%: item.ID %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  
  <td class="CellBegin">
    <%: item.Subject %>
  </td>
    <td class="Cell">
        <%if (!string.IsNullOrEmpty(item.Content))
            {

          
            if (item.Content.Length > 50)
            { %>
             <%:item.Content.Substring(0, 50) %>
        <%}
                                           else { %>
            <%:item.Content %>
        <%}   }%>
        
       
    </td>
    <td class="Cell"><%:item.RecordDate %></td>
    <td class="CellBegin">
    <%: (item.ConstantId>0?item.Constant.ConstantName:"") %>
  </td>
     <td class="Cell"> <div style="float: left;">
            <a style="cursor: pointer;" onclick="DeleteHelp(<%:item.ID %>)">
        <img src="/Content/images/delete.png" />
    </a>
     <a href="/Help/EditHelp/<%: item.ID %>">
      <img src="/Content/images/edit.png"/>
    </a>
 </div></td>
</tr>
    <%} %>

<tr>
  <td class="ui-state ui-state-default" colspan="5" align="left" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
        &nbsp;Sayfa&nbsp;&nbsp;
        <% for (int i=1; i<=Model.TotalPage; i++)
           { %>
        <li>
          <% if (i == Model.CurrentPage)
             { %>
          <span class="currentpage">
            <%: i %></span>&nbsp;
          <% } %>
          <% else
             { %>
          <a onclick="PageHelp(<%: i %>)">
            <%: i %></a>&nbsp;
          <% } %>
        </li>
        <% } %>

      </ul>
    </div>
  </td>
</tr>