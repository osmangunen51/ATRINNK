﻿<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Orders.EbillNumberModel>" %>

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
        <%if(ViewBag.Success!=null){%>
        
                <p style="color:#125804">Başarılı !</p>
        <%}%>
        <h3>Yeni Tarih Ve Açıklama</h3>
        <%using (Html.BeginForm())
            {%>
        <%:Html.HiddenFor(x=>x.OrderId) %>
        <table>
            <tr>
                <td>E-fatura Numarası</td>
                <td>:</td>
                <td><%:Html.TextBoxFor(x=>x.EbillNumber) %></td>
                <td><%:Html.ValidationMessageFor(x=>x.EbillNumber) %></td>

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


