﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.NotificationModelBase>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
    Bildirimler
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">

    <%:Html.Hidden("OrderType", "0") %>
    <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
        id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <div style="font-size: 18px; margin-top: 20px; float: right;">
        Filtrele:
                 <select name="users" id="UserId" onchange="SearchPost();">
                     <%foreach (var item in Model.Users)
                         {
                             string selected = "";
                             if (item.Selected)
                                 selected = "selected";
                     %>
                     <option value="<%:item.Value %>" <%:selected %>><%:item.Text %></option>
                     <%} %>
                 </select>
        <%:Html.DropDownList("UserType",Model.UserTypes,new {@id="UserType", @onchange="SearchPost();"}) %>
    </div>
    <div style="width: 100%; margin: 0 auto;">
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header HeaderBegin">#</td>
                    <td class="Header">Üye Adı</td>
                    <td class="Header">Firma Adı</td>
                    <td class="Header">Başlık</td>
                    <td class="Header">Açıklama</td>
                    <td class="Header">Oluşuturulma Tarihi</td>
                    <td class="Header" unselectable="on" onclick="OrderBy()">Hatırlatma Tarihi</td>
                    <td class="Header">Atayan</td>
                    <td class="Header">Atanan</td>
                    <td class="Header">Kayıt Türü</td>
                    <td class="Header HeaderEnd ">Araçlar</td>
                </tr>
                <tr style="background-color: #F1F1F1">
                    <td class="CellBegin"></td>
                    <td class="Cell"></td>
                    <td class="Cell"></td>
                    <td class="Cell">
                                       <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td style="border: solid 1px #CCC; background-color: #FFF;">
                                 
                                        <%:Html.DropDownList("ConstantId",Model.Titles,new {@onchange="SearchPost();" }) %>
                        
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </td>
                    <td class="Cell"></td>
                    <td class="Cell"></td>
                    <td class="Cell"></td>
                    <td class="Cell"></td>
                    <td class="Cell"></td>
                       <td class="Cell"></td>
                    <td class="CellEnd"></td>
                </tr>
            </thead>

            <tbody id="table">
                <%=Html.RenderHtmlPartial("_NotificationList",Model.Notifications) %>
            </tbody>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
    <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox-1.js"></script>

    <script type="text/javascript">

        function PagingPost(p) {

            $("#CurrentPage").val(p);
            SearchPost();
        }
        function OrderBy() {
            if ($("#OrderType").val() == "1") {
                $("#OrderType").val("2");
                PagingPost($("#CurrentPage").val());
                return;
            }

            $("#OrderType").val("1");
            PagingPost($("#CurrentPage").val());

        }
        function SearchPost() {
            $('#preLoading').show();
            $.ajax({
                url: "/MemberDescription/Notification",
                data: {
                    page: $('#CurrentPage').val(),
                    userId: $("#UserId").val(),
                    userGroupId: $("#UserType").val(),
                    orderBy: $("#OrderType").val(),
                    constantId: $("#ConstantId").val()
                },
                type: 'post',
                success: function (data) {
                    $('#table').html(data);
                    $('#preLoading').hide();
                },
                error: function (x, a, r) {
                    $('#preLoading').hide();
                }
            });
        }
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
        #superbox-overlay {
            background: #e0e4cc;
        }

        #superbox-container .loading {
            width: 32px;
            height: 32px;
            margin: 0 auto;
            text-indent: -9999px;
            background: url(styles/loader.gif) no-repeat 0 0;
        }

        #superbox .close a {
            float: right;
            padding: 0 5px;
            line-height: 20px;
            background: #333;
            cursor: pointer;
        }

            #superbox .close a span {
                color: #fff;
            }

        #superbox .nextprev a {
            float: left;
            margin-right: 5px;
            padding: 0 5px;
            line-height: 20px;
            background: #333;
            cursor: pointer;
            color: #fff;
        }

        #superbox .nextprev .disabled {
            background: #ccc;
            cursor: default;
        }
    </style>
</asp:Content>

