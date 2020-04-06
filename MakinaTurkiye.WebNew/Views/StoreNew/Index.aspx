<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.StoreNews.MTStoreNewModel>" %>
<asp:Content ID="Content5" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="canonical" href="<%:ViewBag.Canonical %>" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div class="fast-access-bar hidden-xs">
        <div class="fast-access-bar__inner">
            <div class="row clearfix">
                <div class="col-xs-12 col-md-6">
                    <ol class="breadcrumb breadcrumb-mt">
                        <li class="active"><a target="_self" href="https://haber.makinaturkiye.com">Haberler</a></li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <div class="row clearfix news-container-p1">
        <%=Html.RenderHtmlPartial("_NewItem",Model.MTStoreNews) %>
    </div>

</asp:Content>
