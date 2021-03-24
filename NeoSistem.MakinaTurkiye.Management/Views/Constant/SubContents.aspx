﻿<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.SubConstantModel>" %>

<link href="/Content/Site.css" rel="stylesheet" type="text/css" />
<link href="/Content/Ribbon.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery-1.8.3.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui.datepicker-tr.js" type="text/javascript"></script>
<link href="/Content/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />

<div style="margin: auto; ">
        <h3>Alt Başlık Ekle</h3>
    <%using (Html.BeginForm())
        {%>
    <%:Html.HiddenFor(x=>x.ConstantId) %>
    <table>
        <tr>
            <td>Alt İçerik</td>
            <td>:</td>
            <td>
                <%:Html.TextAreaFor(x=>x.Content, new { @style="height:25px; width:200px; height:50px;" }) %>
        </tr>

        <tr>
            <td colspan="3"><input type="submit" value="Ekle" /></td>
        </tr>

    </table>
    <% } %>
</div>
<div class="data" style="margin-top: 5px; overflow: scroll;">
    <div style="color:#045b20; font-size:15px">
        <%:Model.Message %>
    </div>
    <%if (Model.Contents.Count > 0)
        {%>
    <table cellpadding="2" cellspacing="0" class="TableList" style="width: 100%;">
        <thead>
            <tr>
                <td class="Header HeaderBegin">İçerik</td>
                 <td class="Header HeaderBegin"></td>
            </tr>
        </thead>
        <%foreach (var item in Model.Contents)
            {%>
        <tr>
            <td class="Cell CellBegin">
                <%:item.Value %>
            </td>

            <td class="Cell CellEnd"><a href="/Constant/DeleteSubConstant/<%:item.Key%>?constantParentId=<%:Model.ConstantId %>" style="cursor: pointer;">Sil</a></td>
        </tr>
        <% } %>
    </table>
    <% } %>
</div>
