﻿<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.UrlRedirectModels.UrlRedirectItem>" %>

<link href="/Content/Site.css" rel="stylesheet" type="text/css" />
<link href="/Content/Ribbon.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery-1.8.3.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui.datepicker-tr.js" type="text/javascript"></script>
<link href="/Content/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">

</script>
<style type="text/css">
    #superbox-innerbox { height: 700px !important; }
</style>
<div style="margin: auto">
    <div style="margin-top: 20px;" class="newValue">
        <%if (TempData["Message"] != null)
            {%>
        <p style="color: #125804"><%:TempData["Message"] %> </p>
        <%}%>
        <h3>Yeni Url Yönlendirme</h3>
        <%using (Html.BeginForm())
            {%>
        <%:Html.HiddenFor(x=>x.UrlRedirectId) %>
        <table>
            <tr>
                <td>Eski Url</td>
                <td>:</td>
                <td><%:Html.TextBoxFor(x=>x.OldUrl) %></td>
                <td><%:Html.ValidationMessageFor(x=>x.OldUrl) %></td>
            </tr>
            <tr>
                <td>Yeni Url</td>
                <td>:</td>
                <td><%:Html.TextBoxFor(x=>x.NewUrl) %></td>
                <td><%:Html.ValidationMessageFor(x=>x.NewUrl) %></td>

            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <input type="submit" value="Kaydet" />
                </td>
            </tr>
        </table>
        <% } %>
    </div>

</div>


