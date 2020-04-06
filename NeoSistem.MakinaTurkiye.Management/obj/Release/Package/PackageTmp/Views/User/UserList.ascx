<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<UserModel>>" %>
<% int row = 0; %>
<% foreach (var item in Model.Source)
    { %>
<% row++; %>
<tr id="row<%: item.UserId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
    <td class="CellBegin">
        <%: item.UserName %>
    </td>
    <td class="Cell">
        <%if (ViewData["Admin"] != null && Convert.ToBoolean(ViewData["Admin"]) == true)
                  {%>
                   <%: item.UserPass %>
            <%}
                  else { %>
        
                *****
        <%} %>
     
    </td>
    <td class="Cell">
        <%if (item.Active)
            {%>
        <img src="/Content/Images/Goodshield.png" title="Aktif Kullanıcı">
        <% }
            else
            {%>
        <img src="/Content/Images/Errorshield.png" title="Pasif Kullanıcı">
        <% } %>
    </td>
    <td class="Cell">
        <%if (item.ActiveForDesc)
            {%>
        <img src="/Content/Images/Goodshield.png" title="Aktif Kullanıcı">
        <% }
            else
            {%>
        <img src="/Content/Images/Errorshield.png" title="Pasif Kullanıcı">
        <% } %>

    </td>
    <td class="Cell">
        <%if (item.EndWorkDate.HasValue && item.EndWorkDate.Value <= DateTime.Now.Date)
            {%>
            <%:item.EndWorkDate.Value.ToString("dd MMMM yyyy") %> Tarihli İşten Ayrıldı 
        <% }
                                                                                           else {%>
        Devam Ediyor
        <% } %>
    </td>
    <td class="CellEnd">
              <%if (ViewData["Admin"] != null && Convert.ToBoolean(ViewData["Admin"]) == true)
                  {%>
                    <a href="/User/Edit/<%: item.UserId %>" style="padding-bottom: 5px;">
            <img style="float: left; margin-right: 10px; display: block;" src="/Content/images/edit.png" />
        </a><a style="cursor: pointer;" onclick="Delete(<%: item.UserId %>);">
            <img style="float: left; margin-right: 5px; display: block;" src="/Content/images/delete.png" />
        </a>
        <a href="/User/CreateMailTemplate/<%=item.UserId  %>">
            <img src="/Content/RibbonImages/mail.png" style="width: 16px;" /></a>
        <% }%>

    </td>
</tr>
<% } %>
<% if (Model.TotalRecord <= 0)
    { %>
<tr class="Row">
    <td colspan="4" class="CellBegin Cell" style="color: #FF0000; padding: 5px; font-weight: 700; font-size: 14px;">Kullanıcı bulunamadı.
    </td>
</tr>
<% } %>
<tr>
    <td class="ui-state ui-state-default" colspan="8" align="right" style="border-color: #DDD; border-top: none; border-bottom: none;">
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
                    <a onclick="PagingPost(<%: page %>)">
                        <%: page %></a>&nbsp;
          <% } %>
                </li>
                <% } %>
            </ul>
        </div>
    </td>
</tr>
