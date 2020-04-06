<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<MTStoreViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
    </style>

    <%if (!string.IsNullOrEmpty(Model.PrevPage))
        { %>
    <link rel="prev" href="<%:Model.PrevPage %>" />
    <%} %>
    <%if (!string.IsNullOrEmpty(Model.NextPage))
        { %>
    <link rel="next" href="<%:Model.NextPage %>" />
    <%} %>
    <% if (!string.IsNullOrEmpty(Model.CanonicalUrl))
        { %>
    <link rel="canonical" href="<%= Model.CanonicalUrl%>" />
    <% }  %>

    <meta name="robots" content="INDEX,FOLLOW" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <div class="fast-access-bar hidden-xs">
        <div class="fast-access-bar__inner">
            <div class="row clearfix">
                <div class="col-xs-12 col-md-6">
                    <%=Model.Navigation %>
                </div>
            </div>
        </div>
    </div>


    <div class="row clearfix">
        <div class="col-xs-12 col-sm-3 col-md-3 col-lg-2 leftSideBar" style="padding-right: 0;">
            <div class="theiaStickySidebar">
                <div class="filters">
                    <div class="pos-relative">
                        <div class="filters__header row visible-xs">
                            <div class="col-xs-12">
                                <a href="javascript:;" class="js-close-filters"><span class="icon-close"></span></a>Detaylandır
                            </div>
                        </div>

                        <div class="filters__inner">
                            <div class="pos-absolute">
                                <div class="pos-absolute__inner panel-group" id="filters" role="tablist">
                                    <%=Html.RenderHtmlPartial("_StoreCategories",Model.StoreCategoryModel)%>
                                    <%=Html.RenderHtmlPartial("_FilterActivityType",Model.FilteringContext.MtStoreActivityTypeFilterModel) %>
                                    <%=Html.RenderHtmlPartial("_FilterAddressBox", Model.FilteringContext.StoreAddressFilterModel)%>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>


                <% var display = string.IsNullOrEmpty(Request.QueryString["Gorunum"]) ? "?Gorunum=Liste" : "?Gorunum=" + Request.QueryString["Gorunum"].ToString(); %>
                <% string page = ""; if (Request.QueryString["Sayfa"] != null) { page = "&Sayfa=" + Request.QueryString["Sayfa"]; }  %>
                <% string querySearchType = string.IsNullOrEmpty(Request.QueryString["SearchType"]) ? "" : "&SearchType=" + Request.QueryString["SearchType"].ToString(); %>

                <div class="mobile-filter-buttons visible-xs">
                    <a href="javascript:;" class="js-toggle-filter"><span class="icon-filter"></span>Detaylandır</a>
                    <a href="javascript:;" class="js-toggle-sort"><span class="icon-sort"></span>Sırala</a>
                    <select class="mobile-sort" style="position: absolute; right: 0; width: 50%; height: 100%; opacity: 0">



                        <%foreach (var item in Model.FilteringContext.SortOptionModels)
                            { %>
                        <% if (item.Selected)
                            {%>
                        <option value="<%=item.SortOptionUrl %>" selected="selected"><%=item.SortOptionName%></option>
                        <%}
                            else
                            { %>
                        <option value="<%=item.SortOptionUrl %>"><%=item.SortOptionName%></option>

                        <%} %>
                        <%} %>
                    </select>
                </div>
            </div>
        </div>


        <div class="col-sm-7 col-md-8 col-lg-10">

            <div class="categort-filter ">
                <div class="row">
                    <div class="col-xs-12">
                        <div style="padding: 0px 6px;" class=" section-title section-title--left section-title--category">
                            <h1>
                                <%
                                    if (string.IsNullOrEmpty(Model.StoreCategoryModel.SelectedCategoryName))
                                    {

                                %>
                               Tüm Firmalar <%=Model.FilteringContext.MtStoreActivityTypeFilterModel.SelectedActivityTypeFilterName %>
                                <% }
                                    else
                                    {%>
                                <%=Model.StoreCategoryModel.SelectedCategoryName%> <%=Model.FilteringContext.MtStoreActivityTypeFilterModel.SelectedActivityTypeFilterName %>
                                <%}%>                         
                            </h1>
                            <span><strong><%=Model.FilteringContext.TotalItemCount %></strong> Tedarikçi</span>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-4 pull-left">
                        <div>
                            <%
                                if (!string.IsNullOrEmpty(ViewBag.SelectedCityName))
                                {
                            %>
                            <span style="font-size: 15px; font-style: italic"><%=ViewBag.SelectedCityName%></span>
                            <hr />
                            <%
                                }
                            %>
                        </div>
                        <div class="btn-group pull-left hidden-xs">

                            <div class="btn-group">
                                <a href="#" id="btnGroupVerticalDrop1" class="btn btn-sm btn-mt2 dropdown-toggle"
                                    data-toggle="dropdown">Sıralama <span class="caret"></span></a>
                                <ul class="dropdown-menu pull-right" role="menu" aria-labelledby="btnGroupVerticalDrop1">
                                    <%foreach (var item in Model.FilteringContext.SortOptionModels)
                                        { %>
                                    <% if (item.Selected)
                                        {%>
                                    <li class="active"><a title='<%=item.SortOptionName %>' href='<%=item.SortOptionUrl %>'><%=item.SortOptionName%></a></li>
                                    <%}
                                        else
                                        { %>
                                    <li><a title='<%=item.SortOptionName %>' href='<%=item.SortOptionUrl %>'><%=item.SortOptionName%></a></li>
                                    <%} %>
                                    <%} %>
                                </ul>
                            </div>
                        </div>
                    </div>


                    <div class="col-sm-12 col-md-4 pull-right">
                        <%=Html.RenderHtmlPartial("_FAGFSearch", Model)%>
                    </div>
                </div>
            </div>
            <%if (Model.StoreModels.Count > 0)
                {%>
            <%=Html.RenderHtmlPartial("_StoreViewList", Model.StoreModels)%>
            <%=Html.RenderHtmlPartial("_StorePaging", Model.StorePagingModel)%>
            <% }
                else
                {%>
            <div class="row">
                <div class="col-md-12 search-no-result-container">
                    <div class="col-md-6 message">
                        <img src="../../Content/V2/images/not-found.png" />
                        <p class="message-text">
                            Ulaşmaya çalıştığınız sayfa kaldırılmış veya aktif olmayabilir.
                        </p>
                        <div>
                            3 saniye içinde en uygun sayfaya yönlendirileceksiniz. Lütfen bekleyiniz.
                    <meta http-equiv="refresh" content="3;url=<%:Model.RedirectUrl %>" />
                        </div>

                    </div>
                </div>
            </div>
            <% } %>
        </div>

    </div>

    <%if (!string.IsNullOrEmpty(Model.SeoContent))
        {%>
    <div class="alert alert-info alert-mt">
        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
            ×
        </button>
        <%=Model.SeoContent %>
    </div>
    <%} %>
</asp:Content>

