<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.BaseMenuModels.BaseMenuCreateModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Base Menü Düzenle
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2 style="margin-left: 20px; margin-top: 25px;">Ana Menü Düzenle </h2>
    <%if (TempData["success"] != null)
        {
    %>
    <div style="font-size: 20px"><%:TempData["success"].ToString() %></div>
    <% } %>
    <% using (Html.BeginForm(new { style = " margin-left:20px; margin-top:25px;", @enctype = "multipart/form-data" }))
        {%>

    <div style="float: left; width: 40%;">

        <%: Html.ValidationSummary(true)%>
        <%:Html.HiddenFor(x=>x.BaseMenuId) %>

        <table>
            <tr>
                <td>Menü İsmi</td>
                <td>:</td>
                <td><%:Html.TextBoxFor(x=>x.BaseMenuName) %></td>
            </tr>
            <tr>
                <td>Sektörler</td>
                <td>:</td>
                <td>
                    <div style="width: 400px">
                        <%foreach (var item in Model.SectorCategories)
                            {%>
                        <input type="checkbox" name="SectorCategoriesForm[]" value="<%:item.Value %>" <%:item.Selected==true ? "checked" : ""%> />
                        <%:item.Text %><br />
                        <%  } %>
                    </div>

                </td>

            </tr>
            <tr>
                <td>Aktif</td>
                <td>:</td>
                <td><%:Html.CheckBoxFor(x=>x.Active) %></td>
            </tr>
            <tr>
                <td>Sıra</td>
                <td>:</td>
                <td><%:Html.TextBoxFor(x=>x.Order) %></td>
            </tr>
            <tr>
                <td>Anasayfa Sıra</td>
                <td>:</td>
                <td><%:Html.TextBoxFor(x=>x.HomePageOrder) %></td>
            </tr>
            <tr>
                <td>Arka plan css</td>
                <td>:</td>
                <td><%:Html.TextAreaFor(x=>x.BackgroundCss) %></td>
            </tr>
                        <tr>
                <td>Tab Arka Plan Css</td>
                <td>:</td>
                <td><%:Html.TextAreaFor(x=>x.TabBackgroundCss) %></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    <input type="submit" value="Kaydet" /></td>
            </tr>
        </table>

    </div>
    <div style="float: left; margin-left: 20px; width: 40%">
        <table>
            <tr>
                <td><a id="lightbox_click" rel="superbox[iframe]"
                    title="Yeni Fotoğraf Ekle" href="/Category/BaseMenuImage/<%:Model.BaseMenuId %>">Fotoğraf Ekle</a></td>
                <td></td>
                <td></td>

            </tr>
            <%foreach (var item in Model.BaseMenuImages)
                {%>
            <tr>
                <td colspan="3">
                    <img id="img<%:item.Key %>" style="width: 150px;" src="<%:item.Value %>" /><br />
                    <a href="/Category/BaseMEnuImage/<%:Model.BaseMenuId %>?imageId=<%:item.Key %>" id="lightbox_click" rel="superbox[iframe]"
                        title="Yeni Fotoğraf Ekle">Düzenle</a> | <a onclick="DeleteImage(<%:item.Key %>)" title="Sil" style="cursor: pointer;">Sil</a>
                </td>
            </tr>
            <%  } %>
        </table>
    </div>
    <% } %>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

    <link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
    <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox-1.js"></script>
    <script type="text/javascript">
        $(function () {
            $.superbox.settings = {
                closeTxt: "Kapat",
                loadTxt: "Yükleniyor...",
                nextTxt: "Sonraki",
                prevTxt: "Önceki"
            };
            $.superbox();
        });
    </script>
    <style type="text/css">
        /* Custom Theme */
        #superbox-overlay { background: #e0e4cc; }
        #superbox-container .loading { width: 32px; height: 32px; margin: 0 auto; text-indent: -9999px; background: url(styles/loader.gif) no-repeat 0 0; }
        #superbox .close a { float: right; padding: 0 5px; line-height: 20px; background: #333; cursor: pointer; }
            #superbox .close a span { color: #fff; }
        #superbox .nextprev a { float: left; margin-right: 5px; padding: 0 5px; line-height: 20px; background: #333; cursor: pointer; color: #fff; }
        #superbox .nextprev .disabled { background: #ccc; cursor: default; }
    </style>
    <script type="text/javascript">
        function DeleteImage(id) {

            if (confirm('Silmek istediğinize eminmisiniz ?')) {
                $.ajax({

                    url: '/Category/DeleteBaseImage',
                    type: 'post',
                    data: {
                        'id': id
                    },
                    dataType: 'json',
                    success: function (data) {
                        $("#img" + id).hide();
                    },
                    error: function (request, error) {
                        alert("Request: " + JSON.stringify(request));
                    }
                });
            }
        }


    </script>
</asp:Content>

