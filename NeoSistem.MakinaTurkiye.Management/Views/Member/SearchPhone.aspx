﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<FilterModel<MemberModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Telefon Numarasına Göre Ara
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        /* Custom Theme */
        #superbox-overlay { background: #e0e4cc; }
        #superbox-container .loading { width: 32px; height: 32px; margin: 0 auto; text-indent: -9999px; background: url(styles/loader.gif) no-repeat 0 0; }
        #superbox .close a { float: right; padding: 0 5px; line-height: 20px; background: #333; cursor: pointer; }
            #superbox .close a span { color: #fff; }
        #superbox .nextprev a { float: left; margin-right: 5px; padding: 0 5px; line-height: 20px; background: #333; cursor: pointer; color: #fff; }
        #superbox .nextprev .disabled { background: #ccc; cursor: default; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
        id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <div class="" style="margin-left:20px;">
        <div style="float:left; width:20%">
        <h4>Telefon Numarasına Göre Kayıt Ara</h4>
        <form id="idForm">
            <table>
                <tr>
                    <td>Numara:</td>
                    <td>
                        <input type="text" id="phoneText" name="phoneText" /></td>
                    <td>
                        <input type="submit" value="Bul" /></td>
                </tr>
            </table>
        </form>
            </div>
        <div style="float:left; width:40%;">
                        <div id="result">

    </div>
        </div>

    </div>

    <script type="text/javascript">
        $("#idForm").submit(function (e) {

            e.preventDefault(); // avoid to execute the actual submit of the form.
            $("#preLoading").show();
            var form = $(this);
            var url = form.attr('action');

            $.ajax({
                type: "POST",
                url: "/Member/SearchPhone",
                data: form.serialize(), // serializes the form's elements.
                success: function (data) {
                    $("#result").html(data);
                                $("#preLoading").hide();
                }
            });


        });

    </script>
</asp:Content>
