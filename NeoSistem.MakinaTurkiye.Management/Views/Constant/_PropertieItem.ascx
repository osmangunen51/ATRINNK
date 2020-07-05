﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.PropertieModel>>" %>
<%int row = Model.CurrentPage * Model.PageDimension - Model.PageDimension + 1; %>
<%:Html.HiddenFor(x=>x.CurrentPage) %>
<%foreach (var item in Model.Source.ToList())
    {%>
<tr id="row<%: item.PropertieId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
    <td class="CellBegin">
        <%: row %>
    </td>
    <td class="Cell">
        <%:item.PropertieName %>
       
    </td>
    <td class="Cell">
        <%string type = "test";
            switch (item.PropertieType)
            {
                case (byte)PropertieType.Editor:
                    type = "Editör";
                    break;
                case (byte)PropertieType.MutipleOption:
                    type = "Çoklu Seçim";
                    break;
                case (byte)PropertieType.Text:
                    type = "Normal Yazı";
                    break;
                default:
                    break;
            }
        %>
        <%:type %>
    </td>
    <td class="Cell">
           <%if (type == "Çoklu Seçim") {%>
            <a href="/Constant/PropertieAttr/<%:item.PropertieId %>" style="float:left;" id="lightbox_click" rel="superbox[iframe]" title="Seçim Ekle">Seçim Ekle</a>

        <% } %>
    </td>
    <td class="Cell">
        <div style="float: left;">
            <a style="cursor: pointer;" onclick="DeletePropertie(<%:item.PropertieId %>)">
                <img src="/Content/images/delete.png" />
            </a>
            <a href="/Constant/Propertie/<%:item.PropertieId %>" <%--href="/Help/EditHelp/<%: item.PropertieId %>"--%>>
                <img src="/Content/images/edit.png" />
            </a>
        </div>
    </td>
</tr>
<%row = row + 1; %>
<%} %>
<tr>
    <td class="ui-state ui-state-default" colspan="5" align="left" style="border-color: #DDD; border-top: none; border-bottom: none;">
        <div style="float: right;" class="pagination">
            <ul>
                &nbsp;Sayfa&nbsp;&nbsp;
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
                    <a onclick="PageHelp(<%: page %>)">
                        <%: page %></a>&nbsp;
          <% } %>
                </li>
                <% } %>
            </ul>
        </div>
    </td>
</tr>
