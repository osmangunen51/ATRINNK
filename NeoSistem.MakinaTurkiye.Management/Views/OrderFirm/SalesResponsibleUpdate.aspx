﻿<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Orders.SalesResponsibleUpdateModel>" %>
<link href="/Content/Site.css" rel="stylesheet" type="text/css" />
<link href="/Content/Ribbon.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery-1.8.3.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui.datepicker-tr.js" type="text/javascript"></script>
<link href="/Content/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />

<style type="text/css">
    #superbox-innerbox { height: 700px !important; }
</style>
<div style="margin: auto">
    <div style="margin-top: 20px;" class="newValue">
        <%if(TempData["Message"]!=null){%>
        
                <p style="color:#125804"><%:TempData["Message"].ToString() %></p>
        <%}%>
        <h3>Yeni Tarih Ve Açıklama</h3>
        <%using (Html.BeginForm("SalesResponsibleUpdate", "OrderFirm", FormMethod.Post)) 
            {%>
        
        <%:Html.HiddenFor(x => x.OrderId) %>
        <table>
            <tr>
                <td>Yeni Satış Sorumlusu</td>
                <td>:</td>
                <td><%:Html.DropDownListFor(x => x.SalesUserId, Model.SalesResponsibleUser) %></td>
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