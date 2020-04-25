﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.CertificateModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Sertifika Ekle
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        function DeleteCertificate(id) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({

                    url: '/Product/DeleteCertificateType',
                    type: 'post',
                    data: {
                        'id': id
                    },
                    dataType: 'json',
                    success: function (data) {
                        $("#row" + id).hide();
                    },
                    error: function (request, error) {
                        alert("Request: " + JSON.stringify(request));
                    }
                });

            }

        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="float: left; margin-left: 20px;">
        <h3>Sertifika Ekle</h3>
        <%if (TempData["Message"] != null)
            {%>
        <p style="font-size: 15px;"><%:TempData["Message"].ToString() %></p>
        <%} %>
        <%using (Html.BeginForm("CertificateTypes", "Product", FormMethod.Post, new { @enctype = "multipart/form-data" }))
            { %>
        <table>
            <tr>
                <td>Sertifika Adı</td>
                <td>:</td>
                <td><%:Html.TextBoxFor(x=>x.CertificateItemModel.Name) %></td>

            </tr>
            <tr>
                <td>Sıra</td>
                <td>:</td>
                <td><%:Html.TextBoxFor(x=>x.CertificateItemModel.Order) %></td>

            </tr>
            <tr>
                <td>İkon</td>
                <td>:</td>
                <td>
                    <input type="file" name="icon" /></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>

                    <input type="submit" value="Ekle" /></td>
            </tr>
        </table>
        <%} %>
    </div>

    <div style="float: left">
        <h3>Sertifikalar</h3>
        <table class="TableList" style="width: 600px">
            <thead>
                <tr>
                    <th class="Heder HeaderBegin">#</th>
                    <th class="Header">Adı</th>
                    <th class="Header">Ikon</th>
                    <th class="Header">Durum</th>
                    <th class="Header">Sıra</th>
                    <th class="Header HeaderEnd"></th>
                </tr>
            </thead>

            <%foreach (var item in Model.CertificateItems)
                {%>
            <tr id="row<%:item.CertificateTypeId %>">
                <td class="Cell CellBegin">
                    <%:item.CertificateTypeId %>
                </td>
                <td class="Cell"><%:item.Name %></td>
                <td class="Cell">
                    <img alt="İkon" style="height: 32px" src="<%:item.IconPath%>" />
                </td>
                <td class="Cell">
                    <%if (item.Active == false)
                        {%>
                        <a href="/Product/CertificateTypeConfirm/<%:item.CertificateTypeId %>">Onayla</a>
                    <% }
                                                   else {%>
                        <p>Onaylandı</p>
                    <% } %>

                </td>
                <td class="Cell"><%:item.Order %></td>
                <td class="Cell CellEnd"><a title="Sil" style="cursor: pointer" onclick="DeleteCertificate(<%:item.CertificateTypeId %>)">
                    <img src="/Content/images/delete.png" style="width: 16px;"></a></td>
            </tr>
            <%} %>
            <tr>
            </tr>
        </table>
    </div>

    <div style="clear: both"></div>
</asp:Content>

