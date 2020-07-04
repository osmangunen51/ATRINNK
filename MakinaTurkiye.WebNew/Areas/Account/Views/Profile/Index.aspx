<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function DeleteAddress(Id, typeId) {
            if (confirm('Adresi ve adrese ait telefonları silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Profile/DeleteAddress',
                    type: 'post',
                    data: { AddressId: Id, type: typeId },
                    success: function (data) {
                        alert('Silmek istediğiniz adres başarıyla silinmiştir.');
                        $('#divAddressItems').html(data);
                    },
                    error: function (x, l, e) {
                        alert(x.responseText);
                    }
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%
        DealerType pageType = (DealerType)Request.QueryString["DealerType"].ToByte();
        string uControlName = string.Empty;
        string pageTitle = string.Empty;

        switch (pageType)
        {
            case DealerType.Bayii:
                uControlName = "Dealer";
                pageTitle = "Firma Bayi Ağları";
                break;
            case DealerType.YetkiliServis:
                uControlName = "Service";
                pageTitle = "Firma Servis Ağları";
                break;
            case DealerType.Sube:
                uControlName = "Branch";
                pageTitle = "Firma Şubeleri";
                break;
            default:
                break;
        } %>
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">

                <span class="text-primary glyphicon glyphicon-cog"></span>&nbsp; <%=pageTitle %>
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>

                <div class="well store-panel-container">
                    <div class="row">
                        <%using (Html.BeginForm("Index", "Profile", FormMethod.Post, new { enctype = "multipart/form-data" }))
                            {%>
                        <input id="DealerTypeId" name="DealerTypeId" value="<%:Request.QueryString["DealerType"].ToByte() %>"
                            type="hidden" />
                        <%=Html.RenderHtmlPartial(uControlName) %>
                        <% } %>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
