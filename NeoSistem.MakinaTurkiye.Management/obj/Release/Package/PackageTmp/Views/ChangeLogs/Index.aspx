﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<global::MakinaTurkiye.Entities.StoredProcedures.Stores.StoreChangeInfoResult>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Bilgileri Değitiren Firmalar
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
    <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
    <script type="text/javascript">
        function DeletePost(changeId) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/ChangeLogs/storeChangeHistoryDelete',
                    data: { id: changeId },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        if (data) {
                            $('#row' + changeId).hide();
                        }
                    }
                });
            }
        }

        
        function PagingPost(curentpage) {
            $('#preLoading').show();
            $.ajax({
                url: '/ChangeLogs/Index',
                data: {
                    page: curentpage
                },
                type: 'post',
                success: function (data) {
                    $("#body").html(data);
                    $('#preLoading').hide();
                },
                error: function (x, a, r) {
                    alert("Error");

                }
            });
        }
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
    <script type="text/javascript"> 

</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
    <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <table cellpadding="13" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
        <thead>
            <tr>
                <td class="Header HeaderBegin" style="width: 5%;">MainPartyId
                </td>

                <td class="Header" style="width: 10%;">Mağaza Adı
                </td>

                 <td class="Header" style="width:5%">Tip</td>
                <td class="Header" style="width: 7%;">Tarih
                </td>
                <td class="Header" style="width: 8%;">Araçlar
                </td>
            </tr>

        </thead>
        <tbody id="body">
            <%=Html.RenderHtmlPartial("_StoreChangeList", Model) %>
     

        </tbody>
    </table>
</asp:Content>
