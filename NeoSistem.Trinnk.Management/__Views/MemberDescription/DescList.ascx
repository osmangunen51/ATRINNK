<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<NeoSistem.Trinnk.Management.Models.BaseMemberDescriptionModel>>" %>

   <% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++; %>
<tr id="row<%: item.ID%>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  
  <td class="CellBegin">
    <%: item.Member.MemberName+" "+item.Member.MemberSurname%>
  </td>
  <td class="Cell">
   
  </td>
  <td class="Cell">
    <%: item.Title %>
  </td>
  <td class="Cell">
    <%: Html.Truncate(item.Description,100) %>
  </td>
  <td class="Cell">
    <%: String.Format("{0:g}", item.InputDate) %>
  </td>
  <td class="Cell">
    <%: String.Format("{0:g}", item.LastDate) %>
  </td>
 
  <td class="CellEnd">
  <div style="float: left; width: 35px; height: 18px;">
       
      
        <a href='<%: Url.Action("BrowseDesc","Member",new { id=item.Member.MainPartyId }) %>'>
    <img src='<%: Url.Content("~/Content/Images/product.png") %>' />
        </a>
        
      </div>
   
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="7" align="right" style="border-color: #DDD;
    border-top: 1px; border-bottom: none;">
    <div style="float: right;" class="pagination">
      
    </div>
  </td>
</tr>