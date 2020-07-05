﻿<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Users.UserFileModel>" %>

<link href="/Content/Site.css" rel="stylesheet" type="text/css" />
<link href="/Content/Ribbon.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery-1.8.3.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui.datepicker-tr.js" type="text/javascript"></script>
<link href="/Content/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
<div style="margin: auto;">
    <h5>Dosya Ekle</h5>
    <%if (TempData["success"] != null)
        {%>
    <p style="font-size: 15px;"><%:TempData["success"].ToString() %></p>
    <% } %>
    <%using (Html.BeginForm("files", "user", FormMethod.Post, new { @enctype = "multipart/form-data" }))
        {%>
    <%:Html.Hidden("userId",Request.QueryString["userId"].ToString()) %>
    <table>
        <tr>
            <td>Dosya Tipi</td>
            <td>:</td>
            <td><%:Html.DropDownList("fileType", Model.FileTypes.ToList()) %></td>
        </tr>
        <tr>
            <td>Dosya/lar</td>
            <td>:</td>
            <td>
                <input type="file" name="files" multiple /></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td>
                <input type="submit" value="Ekle" /></td>
        </tr>
    </table>
    <% } %>
    <%if (Model.UserFileItems.Count > 0)
        {%>
    <h5>Dosyalar</h5>
    <table cellpadding="2" cellspacing="0" class="TableList" style="width: 100%;">
        <tr>
            <th class="HeaderBegin Header">Dosya Tipi</th>
            <th class="Header">Dosya</th>
            <th class="HeaderEnd Header">Araçlar</th>
        </tr>
        <%foreach (var item in Model.UserFileItems)
            {%>
        <tr>
            <td class="CellBegin Cell"><%:item.Type %></td>
            <td class="Cell"><a target="_blank" href="<%:item.FilePath %>">Gör</a></td>
            <td class="Cell CellEnd"><a href="/User/FileDelete/<%:item.UserFileId %>">Sil</a></td>
        </tr>
        <%} %>
    </table>

    <% } %>
</div>
