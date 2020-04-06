<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<MTSearchProductViewModel>" %>


<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta name="robots" content="noindex, nofollow" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% if (Request.QueryString["SearchText"] != null && Request.QueryString["SearchText"].ToString().Length < 3)
        { %>

    <div class="row">
        <div class="col-md-12 search-no-result-container">
            <div class="col-md-6 message">
                <img src="../../Content/V2/images/not-found.png" />
                <p class="message-text">
                    "<b><%:Request.QueryString["SearchText"].ToString() %></b>" ile ilgili sonuç bulunamamıştır.
                </p>
                <div>
                    Arama yapabilmek için en az 3 karakter girmelisiniz kelimenizi arttırabilir veya <a href="<%:AppSettings.SiteAllCategoryUrl %>">tüm ürünler</a> sayfamıza bakabilirsiniz.
                </div>

            </div>
        </div>
    </div>
    <% }
        else
        {%>

    <div class="row">
        <%= Html.RenderHtmlPartial("_ProductLeftCategories", Model.CategoryModel)%>
        <div class="col-sm-7 col-md-8 col-lg-9">
            <div class="row hidden-xs">
                <div class="col-xs-12">
                    <%=Model.Navigation %>
                </div>
            </div>
            <div class=" well well-mt2 p0 m0" style="height: 30px;">
                <div class="col-sm-7 col-md-4 col-lg-4 pr0">
                    <div class="btn-group btn-group-justified">
                        <span class="btn btn-sm disabled btn-mt2">Görünüm:
                        </span>

                        <a href="#pencere" data-toggle="tab" class="btn btn-sm btn-mt2">
                            <span class="glyphicon glyphicon-th"></span>
                        </a>

                        <a href="#liste" data-toggle="tab" class="btn btn-sm btn-mt2 active">
                            <span class="glyphicon glyphicon-th-list"></span>
                        </a>
                    </div>
                </div>

                <div class="col-sm-12 col-md-8  col-lg-8 pl0">
                    <div class="btn-group btn-group-justified">
                        <%foreach (var item in Model.FilteringContext.CustomFilterModels)
                            {%>
                        <a href="<%:item.FilterUrl %>" class="btn btn-sm btn-mt2 <%=item.Selected ? "active": "" %>">
                            <%:item.FilterName %>
                        </a>
                        <%} %>

                        <div class="btn-group">
                            <a href="#" id="btnGroupVerticalDrop1" class="btn btn-sm btn-mt2 dropdown-toggle" data-toggle="dropdown">Akıllı Sıralama 
                  <span class="caret"></span>
                            </a>
                            <ul class="dropdown-menu pull-right" role="menu" aria-labelledby="btnGroupVerticalDrop1">
                                <li>
                                    <a href="#">Yeni Gelenler
                                    </a>
                                </li>
                                <li>
                                    <a href="#">Baş Harfe Göre
                                    </a>
                                </li>
                                <li>
                                    <a href="#">İlk Bitecek Ürünler
                                    </a>
                                </li>
                                <li>
                                    <a href="#">Popüler Ürünler
                                    </a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="well well-mt2 mb20">
                <div class="row">
                    <div class="col-sm-12 col-md-6">
                        <span class="pull-left" style="font-size: 14px;">
                            <%if (string.IsNullOrEmpty(Model.CategoryModel.SelectedCategoryName))
                                { %>
                      "<strong><%:Model.SearchText%></strong>" kelimesinde <span style="color: #8f0100; font-weight: 700;"><%:Model.FilteringContext.TotalItemCount%></span> <strong>Adet</strong> ürün bulundu.
                <%}
                    else
                    {
                %> 
                     "<strong><%:Model.SearchText %> / <%:Model.CategoryModel.SelectedCategoryName%></strong>" kategorisinde <span style="color: #8f0100; font-weight: 700;"><%:Model.FilteringContext.TotalItemCount%></span> <strong>Adet</strong> ürün bulundu.
                <%} %>
                        </span>
                    </div>
                    <div class="col-sm-12 col-md-6">
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="tab-content">
                        <%= Html.RenderHtmlPartial("_SearchProductWindow", Model.SearchProductModels)%>
                        <%= Html.RenderHtmlPartial("_SearchProductList", Model.SearchProductModels)%>
                    </div>
                    <%= Html.RenderHtmlPartial("_ProductPaging", Model.PagingModel)%>
                </div>
            </div>
        </div>
    </div>
    <%} %>
</asp:Content>
