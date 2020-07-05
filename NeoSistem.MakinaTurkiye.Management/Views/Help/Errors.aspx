<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.WebSiteErrorModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <% string title = "Sorunlar";
        if (Request.QueryString["type"] != null)
        {
            title = "Öneriler";
        }
    %>
    <%:title %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function DeleteError(id) {
            $.ajax({
                url: '/Help/ErrorDelete',
                data: {
                    ID: id
                },
                type: 'post',
                success: function (data) {
                    $("#row" + id).hide();

                },
                error: function (x, a, r) {
                    alert("Error");

                }
            });
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
        id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <div style="width: 100%; margin: 0 auto;">
        <%:Html.HiddenFor(x=>x.Type) %>
        <button style="margin-top: 10px;" onclick="window.location='/Help/ErrorCreate'">Yeni Ekle</button>
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header">Sorun Tipi</td>
                    <td class="Header">Başlık
                    </td>
                    <td class="Header">Content
                    </td>
                    <td class="Header">Oluşturan
                    </td>
                    <td class="Header">Dosya
                    </td>
                    <td class="Header">Tarih</td>
                    <td class="Header">Durum</td>
                    <td class="Header"></td>
                </tr>
                <tr style="background-color: #F1F1F1">
                    <td class="CellBegin">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td>
                                        <%:Html.DropDownList("ProblemType", Model.ProblemTypes, new {@onChange="FilterData()" }) %>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell"></td>
                    <td class="Cell"></td>
                    <td class="Cell">
                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td>
                                        <%:Html.DropDownList("UserId", Model.Users, new {@onChange="FilterData()" }) %>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell"></td>
                    <td class="Cell"></td>
                    <td class="Cell">

                        <table border="0" cellspacing="0" cellpadding="0" style="width: 100%">
                            <tbody>
                                <tr>
                                    <td>
                                        <select id="ProblemSolved" onchange="FilterData()">
                                            <option value="2">Tümü</option>
                                            <option selected="selected" value="0">Çözülmeyenler</option>
                                            <option value="1">Çözülenler</option>

                                        </select>
                                   
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="CellEnd"></td>
                </tr>
            </thead>
            <tbody id="table">
                <%=Html.RenderHtmlPartial("_WebsiteErrorListItem",Model.WebSiteErrorList) %>
            </tbody>
        </table>
    </div>

    <script type="text/javascript">
        function FilterData() {
            $("#preLoading").show();
            var problemType = $("#ProblemType").val();
            var userId = $("#UserId").val();
            var type = $("#Type").val();
            $.ajax({

                url: '/Help/Errors',
                type: 'post',
                data: {
                    'userId': userId,
                    'problemType': problemType,
                    'type': type,
                    'problemSolved': $("#ProblemSolved").val()
                },

                success: function (data) {
                    $("#table").html(data);
                    $("#preLoading").hide();
                },
                error: function (request, error) {
                    alert("Request: " + JSON.stringify(error));
                }
            });
        }
    </script>
</asp:Content>

