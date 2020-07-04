<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.MemberDescriptionCountModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Tarihe Göre Atama Sayıları
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">

        function PagingNotCount(curentpage) {

            $('#preLoading').show();
            $.ajax({
                url: '/MemberDescription/NotificationCount',
                data: {
                    CurrentPage: curentpage
                },
                type: 'post',
                success: function (data) {
                    $("#table").html(data);
                    $('#preLoading').hide();

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
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header HeaderBegin">#
                    </td>
                    <td class="Header ">Tarih
                    </td>

                    <td class="Header ">Toplam Adet
                    </td>
                    <%var member = Model.Source.FirstOrDefault(); %>
                    <%foreach (var item in member.Usercounts.OrderBy(x=>x.UserName).ToList())
                        {
                    %>
                    <td class="Header">
                        <%:item.UserName %>
                    </td>
                    <%  } %>
                </tr>
            </thead>
            <tbody id="table">

                <%=Html.RenderHtmlPartial("_NotificationCountList",Model) %>
            </tbody>

        </table>
    </div>



</asp:Content>

