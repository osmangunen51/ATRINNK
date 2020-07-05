﻿<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.ViewModel.CategoryPropertieViewModel>" %>

<link href="/Content/Site.css" rel="stylesheet" type="text/css" />
<link href="/Content/Ribbon.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery-1.8.3.js" type="text/javascript"></script>
<script type="text/javascript">

    function Delete(orderId) {

        if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
            $.ajax({
                url: '/OrderFirm/DeletePayment',
                data: { id: orderId },
                type: 'post',
                dataType: 'json',
                success: function (data) {

                    if (data) {
                        $('#row' + orderId).hide();
                    }
                }
            });
        }
    }
</script>
<div style="margin: auto">
    <div style="margin-top: 20px;" class="newValue">
        <h3>Özel Soru Tipi Ekle</h3>
        <%using (Html.BeginForm())
            {%>
        <%:Html.HiddenFor(x=>x.CategoryId) %>
        <table>
            <tr>
                <td>Görüntülenecek Soru</td>
                <td>:</td>
                <td><%:Html.DropDownListFor(x=>x.PropertieId,Model.Questions) %></td>
                <td><%:Html.ValidationMessageFor(x=>x.PropertieId) %></td>
                <td>
                    <input type="submit" value="Ekle" /></td>
            </tr>
        </table>
        <% } %>
    </div>
    <div class="data" style="margin-top: 10px;   " >
        <%if (Model.PropertieModels.Count > 0)
            {%>
        <table cellpadding="2" cellspacing="0" class="TableList" style="width: 100%; overflow: scroll;">
            <thead>
                <td class="Header HeaderBegin">Gözüken Ad</td>
                <td class="Header">Tipi</td>

                <td class="Header HeaderEnd">Araçlar</td>
            </thead>
            <%foreach (var item in Model.PropertieModels)
                {%>
            <tr>
                <td class="Cell CellBegin">
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
                    <a href="/Category/QuestionDelete/<%:item.CategoryPropertieId %>">Kaldır</a>
                </td>
            </tr>
            <% } %>
        </table>
        <% } %>
    </div>
</div>


