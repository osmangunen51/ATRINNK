<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StoreProfile.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTStoreProfileProductsModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

    <%=Html.RenderHtmlPartial("_HeaderContent") %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="StoreProfileHeaderContent" runat="server">
    <%if (Model.StoreActiveType == 2)
        {  %>
    <%} %>
    <%=Html.RenderHtmlPartial("_HeaderTop",Model.MTStoreProfileHeaderModel) %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="StoprofileMenu" runat="server">
    <%if (Model.StoreActiveType == 2)
        {  %>
    <%=Html.RenderHtmlPartial("_LeftMenu", Model.MTStoreProfileHeaderModel)%>

    <%} %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StoreProfileContent" runat="server">
    <%if (Model.StoreActiveType == 2)
        { %>

    <div class="col-sm-7 col-md-8 col-lg-9">

        <%if (Model.MTProductsProductListModel.MTProductsPageProductLists.Source.Count() > 0)
            {
        %>
        <div class="store-product-container">

            <div class="col-md-12" style="padding: 0px;">
                <div class="categort-filter">
                    <div class="row">
                        <div class="col-xs-12 col-sm-6 col-md-10">
                            <%foreach (var item in Model.CustomFilterModels.Where(x => x.ProductCount > 0))
                                {
                                    string active = "";
                                    if (item.Selected)
                                    {
                                        active = "active";
                                    }
                            %>       <a href="<%:item.FilterUrl %>" class="categort-filter__link <%:active %>"><%:item.FilterName %><span class="category-filter-count">&nbsp;(<%:item.ProductCount %>)</span></a>

                            <%} %>
                        </div>
                        <%--  <div class="col-xs-12 col-sm-6 col-md-2 hidden-xs">
                                <div class="dropdown"> <button class="btn btn-default btn-block dropdown-toggle" type="button" id="btnGroupVerticalDrop1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true"> Akıllı Sıralama <span class="caret"></span> </button> <ul class="dropdown-menu dropdown-menu-right" aria-labelledby="btnGroupVerticalDrop1"> <li> <a class="" href="/boya-cila-makinalari-c-94024?Gorunum=Liste&amp;Order=a-z">a-Z</a> </li> <li> <a class="" href="/boya-cila-makinalari-c-94024?Gorunum=Liste&amp;Order=z-a">Z-a</a> </li> <li> <a class="" href="/boya-cila-makinalari-c-94024?Gorunum=Liste&amp;Order=soneklenen">Son Eklenen</a> </li> <li> <a class="" href="/boya-cila-makinalari-c-94024?Gorunum=Liste">En Çok Görüntülenen</a> </li> </ul> </div> </div> </div>--%>
                    </div>
                </div>
                <%= Html.RenderHtmlPartial("_ProductsProductWindow", Model.MTProductsProductListModel) %>
            </div>
            <div class="StoreProfileContent hidden">
                <div class="row store-detail__product">
                    <div class="col-sm-12 col-md-12">
                        <%--  <div class=" well well-mt2 p0 m0" style="height: 30px;margin-bottom: 20px !important;">
                            <div class="col-sm-7 col-md-4 col-lg-4 pr0">
                                <div class="btn-group btn-group-justified">
                                    <span class="btn btn-sm disabled btn-mt2">Görünüm:</span> <a href="#pencere" data-toggle="tab"
                                        class="btn btn-sm btn-mt2 active"><span class="glyphicon glyphicon-th"></span></a><a href="#liste"
                                            data-toggle="tab" class="btn btn-sm btn-mt2"><span class="glyphicon glyphicon-th-list">
                                            </span></a>
                                </div>
                            </div>
                        </div>--%>

                        <div class="row">
                            <div class="col-md-12">
                                <div class="tab-content">
                                    <%--  <%= Html.RenderHtmlPartial("List", Model.ProductItems) %>--%>
                                    <%-- <%= Html.RenderHtmlPartial("_ProductsProductList", Model.MTProductsProductListModel) %>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    
        </div>
        <%}
            else
            {%>
        <div class="row">
            <div class="col-md-12 search-no-result-container">
                <div class="col-md-6 message">
                    <img src="../../Content/V2/images/not-found.png" />
                    <p class="message-text">
                        Bu kategoride ürün  bulunamamıştır.
                    </p>
                    <div>
                        Dilerseniz firmanın <a href="<%:Model.MTStoreProfileHeaderModel.StoreUrl %>">ansayfasına</a>  veya <a href="<%:AppSettings.SiteAllCategoryUrl %>">tüm ürünler</a> sayfamıza bakabilirsiniz.
                    </div>

                </div>
            </div>
        </div>
        <%}%>
        <%}

            else
            { %>
        <%=Html.Action("NoAccessStore",new{id=Model.MTProductsProductListModel.StoreMainPartyId}) %>
        <%} %>
</asp:Content>

