<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<CategoryViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <% if (!string.IsNullOrEmpty(ViewBag.Canonical))
        { %>
        <link rel="canonical" href="<%= ViewBag.Canonical%>"/>
    <% }  %>

    <% if (Model.ActiveCategory.CategoryParentId.HasValue)
        { %>
    <%if (Model.ProductListViewModel.ProductSearch.Source.Count > 0)
        {  %>
    <meta name="robots" content="INDEX,FOLLOW" />
    <%} %>
    <%else
        {  %>
    <meta name="robots" content="NOINDEX,NOFOLLOW" />
    <%} %>
    <%} %>
    <%=Html.RenderHtmlPartial("CategoryHeaderContent")%>
    <script type="text/javascript">
        onload = function () {
            $('#searchPurchaseAdvert').attr('class', 'searchMenuActive');
            $('#hdnTopSearchType').val('2');
            $('#searchSpan').html('Ürün Arama :');
        }

        function CountrySelected() {
            $('#CountrySelected').show();
            $.ajax({
                type: "POST",
                url: '/Category/CountrySelected',
                data:
                {
                    CategoryId: $('#hdnCategoryId').val(),
                    hasCategory: $('#hdnHasCategory').val(),
                    CategoryIdSecond: $('#hdnCategoryIdSecond').val(),
                },
                success: function (data) {
                    document.getElementById('CountrySelected').innerHTML = data;
                }, error: function (x) {
                    $('#Product').html(x.responseText);
                    $('#divLoading').hide();
                }
            });
        }
        function CitySelected() {
            $('#CitySelectedN').show();
            $.ajax({
                type: "POST",
                url: '/Category/CitySelected',
                data:
                {
                    CategoryId: $('#hdnCategoryId').val(),
                    hasCategory: $('#hdnHasCategory').val(),
                    CategoryIdSecond: $('#hdnCategoryIdSecond').val()
                },
                success: function (data) {
                    document.getElementById('CitySelectedN').innerHTML = data;
                    $('#divLoading').hide();
                }, error: function (x) {
                    $('#Product').html(x.responseText);
                    $('#divLoading').hide();
                }
            });
        }
        function LocalitySelected() {
            $('#LocalitySelectedN').show();
            $.ajax({
                type: "POST",
                url: '/Category/LocalitySelected',
                data:
                {
                    CategoryId: $('#hdnCategoryId').val(),
                    hasCategory: $('#hdnHasCategory').val(),
                    CategoryIdSecond: $('#hdnCategoryIdSecond').val(),
                },
                success: function (data) {
                    document.getElementById('LocalitySelectedN').innerHTML = data;
                    $('#divLoading').hide();
                }, error: function (x) {
                    $('#Product').html(x.responseText);
                    $('#divLoading').hide();
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row clearfix">

        <% 
            if (Model.ActiveCategory.CategoryId == 0)
            { %>
        <%=Html.RenderHtmlPartial("LeftMenuSectorCategories")%>
        <div class="col-sm-7 col-md-8 col-lg-9">
            <div class="row hidden-xs">
                <div class="col-xs-12">
                    <%if (ViewData["ViewNavigation"] != null)
                        { %>
                    <%=ViewData["ViewNavigation"]%>
                    <%}%>
                </div>
            </div>
            <div class="row">
                <%=Html.RenderHtmlPartial("_SectorCategoryList")%>
            </div>
        </div>
        <% }
            else
            { %>
        <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
            <nav>
                <%--	 <% 
                if (!Model.ActiveCategory.CategoryParentId.HasValue)
                { %>
						  <%=Html.RenderHtmlPartial("LeftMenuSectorCategories")%>
					 <%}
                else
                { %>--%>
                <%=Html.RenderHtmlPartial("LeftMenuCategories")%>
                <%--	<% } %>--%>
            </nav>
            <%if (Model.CategoryNameBrand != null)
                {  %>
            <%string urlall = ""; %>
            <%if (Model.CategoryNameBrand.CategoryType != 1)
                {
                    urlall = "/" + Html.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 1).CategoryName) + "/" + Model.TopCategoryItems.Last(c => c.CategoryType == 1).CategoryId + "/" + Helpers.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 0).CategoryName);
                }
                else
                {
                    urlall = "/" + Html.ToUrl(Model.ActiveCategory.CategoryName) + "/" + Model.ActiveCategory.CategoryId + "/" + Helpers.ToUrl(Model.TopCategoryItems.SingleOrDefault(c => c.CategoryType == (byte)CategoryType.ProductGroup).CategoryName);
                } %>
            <div class="panel panel-mt">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-th-list"></span>Seçilen Marka: <a class="allchoice"
                        style="float: right; font-size: 11px;" href="<%:urlall %>">Tüm Markalar</a>
                </div>

                <div class="list-group list-group-mt3" style="overflow: auto; max-height: 195px;">
                    <div class="list-group-item">
                        <%string urlbrand = Request.FilePath;
                            if (Model.CategoryNameSerie != null || Model.CategoryNameModel != null)
                            {
                                //urlbrand = "/" + Html.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 3).CategoryName) + "/" + Model.TopCategoryItems.Last(c => c.CategoryType == 3).CategoryId + "/" + Helpers.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 1).CategoryName);
                                urlbrand = "/" + Html.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 3).CategoryName) + "-" + Html.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 1).CategoryName) + "-c-" + Model.TopCategoryItems.Last(c => c.CategoryType == 1).CategoryId + "-" + Model.TopCategoryItems.Last(c => c.CategoryType == 3).CategoryId;

                            }
                        %>
                        <span class="glyphicon glyphicon-saved"></span><a href="<%:urlbrand %>">
                            <%:Model.CategoryNameBrand.CategoryName%>
                            (<%:Model.ProductListViewModel.ProductSearch.TotalRecord%>)</a>
                    </div>
                </div>

            </div>
            <%} %>
            <%if (Model.CategoryNameSerie != null)
                {  %>
            <%string urlall = "/" + Html.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 4).CategoryName) + "-" + Html.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 3).CategoryName) + "-" + Html.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 1).CategoryName) + "s" + Model.TopCategoryItems.Last(c => c.CategoryType == 4).CategoryId; %>

            <div class="panel panel-mt">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-th-list"></span>Seçilen Seri: <a class="allchoice"
                        style="float: right; font-size: 11px;" href="<%:urlall %>">Tüm Seriler</a>
                </div>
                <div class="list-group list-group-mt3" style="overflow: auto; max-height: 195px;">
                    <div class="list-group-item">
                        <span class="glyphicon glyphicon-saved"></span><a>
                            <%:Model.CategoryNameSerie.CategoryName%>
                            (<%:Model.CategoryNameSerie.ProductCount%>)</a>
                    </div>
                </div>

            </div>
            <%} %>
            <%if (Model.CategoryNameModel != null)
                {  %>
            <% string urlall = "/" + Html.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 5).CategoryName) + "-" + Html.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 3).CategoryName) + "-" + Html.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 1).CategoryName) + "m" + Model.TopCategoryItems.Last(c => c.CategoryType == 5).CategoryId; %>

            <div class="panel panel-mt">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-th-list"></span>Seçilen Model Tipi:: <a class="allchoice"
                        style="float: right; font-size: 11px;" href="<%:urlall %>">Tüm Modeller</a>
                </div>
                <div class="list-group list-group-mt3" style="overflow: auto; max-height: 195px;">
                    <div class="list-group-item">
                        <span class="glyphicon glyphicon-saved"></span><a>
                            <%:Model.CategoryNameModel.CategoryName%>
                            (<%:Model.CategoryNameModel.ProductCount%>)</a>
                    </div>
                </div>
            </div>
            <%} %>
            <%if (Model.BrandSerieOrModelList != null && Model.BrandSerieOrModelList.Count > 0)
                {  %>
            <div class="panel panel-mt">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-th-list"></span>
                    <%if (Model.BrandSerieOrModelList.First().Category.CategoryType == (byte)CategoryType.Brand)
                        {  %>
                    Marka
                    <%}  %>
                    <%if (Model.BrandSerieOrModelList.First().Category.CategoryType == (byte)CategoryType.Series)
                        {  %>
                    Seri
                    <%}  %>
                    <%if (Model.BrandSerieOrModelList.First().Category.CategoryType == (byte)CategoryType.Model)
                        {  %>
                    Model Tipi
                    <%}  %>
                    Seçiniz:
                </div>
                <div class="list-group list-group-mt3" style="overflow: auto; max-height: 195px;">
                    <%string linkbrandcat = ""; %>
                    <%int cnt = 0; %>
                    <%foreach (var items in Model.BrandSerieOrModelList)
                        {  %>
                    <%if (items.Category.CategoryType == (byte)CategoryType.Brand)
                        {%>

                    <%if (items.Category.CategoryParentId != Model.ActiveCategory.CategoryId)
                        {  %>
                    <%linkbrandcat = "/" + Html.ToUrl(items.Category.CategoryName) + "-" + Helpers.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 1).CategoryName) + "-c-" + Model.TopCategoryItems.Last(c => c.CategoryType == 1).CategoryId + "-" + items.Category.CategoryId; %>
                    <%cnt = cnt + 1; %>
                    <div class="list-group-item">
                        <a href="<%:linkbrandcat %>"><i class="text-md fa fa-fw fa-square-o"></i>
                            <%:items.Category.CategoryName%>
                            <span class="text-muted text-sm">(<%=Model.BrandSum.ElementAtOrDefault(cnt-1).ProductCount%>)
                            </span></a>
                    </div>
                    <%}
                        else
                        {  %>
                    <%linkbrandcat = "/" + Html.ToUrl(items.Category.CategoryName) + "-" + Helpers.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 1).CategoryName) + "-b-" + items.Category.CategoryId; %>
                    <div class="list-group-item">
                        <a href="<%:linkbrandcat %>">
                            <%:items.Category.CategoryName%>
                            (<%:items.Category.ProductCount %>)</a>
                    </div>
                    <%} %>

                    <%}
                        else if (items.Category.CategoryType == (byte)CategoryType.Model)
                        {  %>
                    <%linkbrandcat = "/" + Html.ToUrl(items.Category.CategoryName) + "-" + Html.ToUrl(Model.CategoryNameBrand.CategoryName) + "-" + Html.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 1).CategoryName) + "-m-" + items.Category.CategoryId; %>
                    <div class="list-group-item">
                        <a href="<%:linkbrandcat %>"><i class="text-md fa fa-fw fa-square-o"></i>
                            <%:items.Category.CategoryName%>
                            <span class="text-muted text-sm">(<%:items.Category.ProductCount %>) </span></a>
                    </div>
                    <%}
                        else if (items.Category.CategoryType == (byte)CategoryType.Series)
                        { %>
                    <%linkbrandcat = "/" + Html.ToUrl(items.Category.CategoryName) + "-" + Html.ToUrl(items.ParentCategoryName) + "-" + Html.ToUrl(Model.TopCategoryItems.Last(c => c.CategoryType == 1).CategoryName) + "-s-" + items.Category.CategoryId; %>
                    <div class="list-group-item">
                        <a href="<%:linkbrandcat %>"><i class="text-md fa fa-fw fa-square-o"></i>
                            <%:items.Category.CategoryName%>
                            <span class="text-muted text-sm">(<%:items.Category.ProductCount %>) </span></a>
                    </div>
                    <%}%>
                    <%} %>
                </div>
            </div>
            <%if (Model.BrandSerieOrModelList.First().Category.CategoryType == (byte)CategoryType.Brand)
                {  %>
            <div class="panel panel-mt">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-th-list"></span>Model Tipi
                </div>
            </div>
            <%}  %>
            <%if (Model.BrandSerieOrModelList.First().Category.CategoryType == (byte)CategoryType.Series)
                {  %>
            <div class="panel panel-mt">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-th-list"></span>Model Tipi
                </div>
            </div>
            <%}  %>

            <%} %>



            <%if (Model.SelectedCountry == null)
                {  %>
            <div class="panel panel-mt">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-th-list"></span>Ülke Seçiniz:
                </div>
                <%if (Model.CountryList != null)
                    {  %>
                <div class="list-group list-group-mt3" style="overflow: auto; max-height: 195px;">
                    <%foreach (var item in Model.CountryList)
                        {  %>
                    <%string linked = "";
                        string pGroupName = "";
                        if (Model.ActiveCategory.CategoryType == (byte)CategoryType.Brand)
                        {
                            pGroupName = Html.ToUrl(Model.ActiveCategory.CategoryName) + "-" + Html.ToUrl(Model.TopCategoryItems.Where(c => c.CategoryId == Model.ActiveCategory.CategoryParentId).FirstOrDefault().CategoryName);
                        }
                        else if (Model.ActiveCategory.CategoryType == (byte)CategoryType.Model)
                        {
                            pGroupName = Html.ToUrl(Model.TopCategoryItems.FirstOrDefault(c => c.CategoryType == (byte)CategoryType.Brand).CategoryName) + "-" + Html.ToUrl(Model.ActiveCategory.CategoryName);
                        }
                        else if (Model.ActiveCategory.CategoryType == (byte)CategoryType.Series)
                        {
                            pGroupName = Html.ToUrl(Model.TopCategoryItems.Where(c => c.CategoryId == Model.ActiveCategory.CategoryParentId).FirstOrDefault().CategoryName) + "-" + Html.ToUrl(Model.ActiveCategory.CategoryName);
                        }
                        else
                        {
                            if (Model.CategoryNameBrand != null)
                            {
                                pGroupName = Html.ToUrl(Model.CategoryNameBrand.CategoryName + "-" + Model.ActiveCategory.CategoryName);
                            }
                            else
                            {
                                pGroupName = Html.ToUrl(Model.ActiveCategory.CategoryName);
                            }
                        }
                        linked = "/" + pGroupName + "/" + Model.ActiveCategory.CategoryId;
                        if (Model.CategoryNameBrand != null)
                        {
                            linked = linked + "-" + Model.CategoryNameBrand.CategoryId;
                        }
                        linked = linked + "--" + item.CountryId + "/" + Helpers.ToUrl(item.CountryName) + "/";
                    %>
                    <div class="list-group-item">
                        <a href="<%:linked %>"><i class="text-md fa fa-fw fa-square-o"></i>
                            <%:item.CountryName %>
                            <span class="text-muted text-sm">(<%:item.CountryCount %>)</span> </a>
                    </div>
                    <%} %>
                </div>
                <%} %>
            </div>
            <%}
                else
                {  %>
            <div class="panel panel-mt">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-th-list"></span>Seçilen Ülke: <a class="allchoice"
                        style="float: right; font-size: 11px;" href="">Tüm Ülkeler</a>
                </div>
                <div class="list-group list-group-mt3" style="overflow: auto; max-height: 195px;">
                    <div class="list-group-item">
                        <a><i class="text-md fa fa-fw fa-square-o"></i>
                            <%:Model.SelectedCountry.CountryName%>
                            <span class="text-muted text-sm">(<%:Model.ProductListViewModel.ProductSearch.TotalRecord%>)</span>
                        </a>
                    </div>
                </div>
            </div>
            <%} %>
            <%if (Model.SelectedCity == null)
                {  %>
            <div class="panel panel-mt">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-th-list"></span>Şehir Seçiniz:
                </div>
                <%if (Model.CityList != null)
                    {  %>
                <div class="list-group list-group-mt3" style="overflow: auto; max-height: 195px;">
                    <%foreach (var item in Model.CityList)
                        {  %>
                    <%string linked = "";
                        string pGroupName = "";
                        if (Model.ActiveCategory.CategoryType == (byte)CategoryType.Brand)
                        {
                            pGroupName = Html.ToUrl(Model.ActiveCategory.CategoryName) + "-" + Html.ToUrl(Model.TopCategoryItems.Where(c => c.CategoryId == Model.ActiveCategory.CategoryParentId).FirstOrDefault().CategoryName);
                        }
                        else if (Model.ActiveCategory.CategoryType == (byte)CategoryType.Model)
                        {
                            pGroupName = Html.ToUrl(Model.TopCategoryItems.FirstOrDefault(c => c.CategoryType == (byte)CategoryType.Brand).CategoryName) + "-" + Html.ToUrl(Model.ActiveCategory.CategoryName);
                        }
                        else if (Model.ActiveCategory.CategoryType == (byte)CategoryType.Series)
                        {
                            pGroupName = Html.ToUrl(Model.TopCategoryItems.Where(c => c.CategoryId == Model.ActiveCategory.CategoryParentId).FirstOrDefault().CategoryName) + "-" + Html.ToUrl(Model.ActiveCategory.CategoryName);
                        }
                        else
                        {
                            if (this.Page.RouteData.Values["categoryIddown"] != null)
                            {
                                pGroupName = Html.ToUrl(Model.CategoryNameBrand.CategoryName + "-" + Model.ActiveCategory.CategoryName);
                            }
                            else
                            {
                                pGroupName = Html.ToUrl(Model.ActiveCategory.CategoryName);
                            }
                        }
                        linked = "/" + pGroupName + "/" + this.Page.RouteData.Values["categoryId"];
                        if (this.Page.RouteData.Values["categoryIddown"] != null)
                        {
                            linked = linked + "-" + this.Page.RouteData.Values["categoryIddown"];
                        }
                        linked = linked + "--" + item.CountryId + "-" + item.CityId + "/" + Helpers.ToUrl(item.CityName) + "/";
                    %>
                    <div class="list-group-item">
                        <a href="<%:linked %>"><i class="text-md fa fa-fw fa-square-o"></i>
                            <%:item.CityName %>
                            <span class="text-muted text-sm">(<%:item.CityCount %>) </span></a>
                    </div>
                    <%} %>
                </div>
                <%} %>
            </div>
            <%}
                else
                {  %>
            <div class="panel panel-mt">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-th-list"></span>Seçilen Şehir: <a class="allchoice"
                        style="float: right; font-size: 11px;" href="">Tüm Şehirler</a>
                </div>
                <div class="list-group list-group-mt3" style="overflow: auto; max-height: 195px;">
                    <div class="list-group-item">
                        <a><i class="text-md fa fa-fw fa-square-o"></i>
                            <%:Model.SelectedCity.CityName%>
                            <span class="text-muted text-sm">(<%:Model.ProductListViewModel.ProductSearch.TotalRecord%>)
                            </span></a>
                    </div>
                </div>
            </div>
            <%} %>
            <%if (Model.SelectedLocality == null)
                {  %>
            <div class="panel panel-mt">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-th-list"></span>İlçe Seçiniz:
                </div>
                <%if (Model.LocalityList != null)
                    {  %>
                <div class="list-group list-group-mt3" style="overflow: auto; max-height: 195px;">
                    <%foreach (var item in Model.LocalityList)
                        {  %>
                    <%string linked = "";
                        string pGroupName = "";

                        if (Model.ActiveCategory.CategoryType == (byte)CategoryType.Brand)
                        {
                            pGroupName = Html.ToUrl(Model.ActiveCategory.CategoryName) + "-" + Html.ToUrl(Model.TopCategoryItems.Where(c => c.CategoryId == Model.ActiveCategory.CategoryParentId).FirstOrDefault().CategoryName);
                        }
                        else if (Model.ActiveCategory.CategoryType == (byte)CategoryType.Model)
                        {
                            pGroupName = Html.ToUrl(Model.TopCategoryItems.FirstOrDefault(c => c.CategoryType == (byte)CategoryType.Brand).CategoryName) + "-" + Html.ToUrl(Model.ActiveCategory.CategoryName);
                        }
                        else if (Model.ActiveCategory.CategoryType == (byte)CategoryType.Series)
                        {
                            pGroupName = Html.ToUrl(Model.TopCategoryItems.Where(c => c.CategoryId == Model.ActiveCategory.CategoryParentId).FirstOrDefault().CategoryName) + "-" + Html.ToUrl(Model.ActiveCategory.CategoryName);
                        }
                        else
                        {
                            if (this.Page.RouteData.Values["categoryIddown"] != null)
                            {
                                pGroupName = Html.ToUrl(Model.CategoryNameBrand.CategoryName + "-" + Model.ActiveCategory.CategoryName);
                            }
                            else
                            {
                                pGroupName = Html.ToUrl(Model.ActiveCategory.CategoryName);
                            }
                        }
                        linked = "/" + pGroupName + "/" + this.Page.RouteData.Values["categoryId"];
                        if (this.Page.RouteData.Values["categoryIddown"] != null)
                        {
                            linked = linked + "-" + this.Page.RouteData.Values["categoryIddown"];
                        }
                        linked = linked + "--" + item.CountryId + "-" + item.CityId + "-" + item.LocalityId + "/" + Helpers.ToUrl(item.LocalityName) + "-" + Helpers.ToUrl(Model.SelectedCity.CityName) + "/";
                    %>
                    <div class="list-group-item">
                        <a href="<%:linked %>"><i class="text-md fa fa-fw fa-square-o"></i>
                            <%:item.LocalityName %>
                            <span class="text-muted text-sm">(<%:item.LocalityCount %>) </span></a>
                    </div>
                    <%} %>
                </div>
                <%} %>
            </div>
            <%}
                else
                {  %>
            <div class="panel panel-mt">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-th-list"></span>Seçilen İlçe: <a class="allchoice"
                        style="float: right; font-size: 11px;" href="">Tüm İlçeler</a>
                </div>
                <div class="list-group list-group-mt3" style="overflow: auto; max-height: 195px;">
                    <div class="list-group-item">
                        <a><i class="text-md fa fa-fw fa-square-o"></i>
                            <%:Model.SelectedLocality.LocalityName%>
                            <span class="text-muted text-sm">(<%:Model.ProductListViewModel.ProductSearch.TotalRecord%>)
                            </span></a>
                    </div>
                </div>
            </div>
            <%} %>
            
                <% if (Model.PopularVideoModels.Count > 0)
                {%>
            <div class="panel panel-mt">
                <div class="panel-heading">
                    <i class="fa fa-play"></i>&nbsp;&nbsp;Popüler Videolar
                </div>
                <ul class="media-list panel-body">
                    <%foreach (var item in Model.PopularVideoModels)
                        {  %>
                    <li class="media"><a class="pull-left" href="<%:item.VideoUrl %>" title="<%:item.CategoryName %>">
                        <img class="media-object" width="80" height="60" src="<%:item.PicturePath %>" alt="<%:item.ProductName %>" />
                    </a>
                        <div class="media-body">
                            <div class="media-heading">
                                <a href="<%:item.VideoUrl %>" class="text-info">
                                    <%:item.ProductName%></a>
                                <br />
                                <a href="<%:item.VideoUrl %>" class="text-muted text-xs">
                                    <%:item.TruncatetStoreName%></a>
                                <br />
                                <span class="text-muted text-xs">
                                    <%:item.SingularViewCount%> görüntüleme
                                </span>
                            </div>
                        </div>
                    </li>
                    <%} %>
                </ul>
            </div>
            <% }
            %>

            <%--  Banner Menü --%>
            <% if (Model.BannerCategoryLeft != null)
                { %>
            <div style="float: left; width: 252px; height: auto;">
                <table cellpadding="0" cellspacing="0" style="width: 100%; height: 100%;">
                    <tr>
                        <%var banner1 = Model.BannerCategoryLeft; %>
                        <td align="center" valign="middle">
                            <% if (banner1 != null)
                                { %>
                            <% if (banner1.BannerResource.Contains(".gif"))
                                { %>
                            <a href="http://<%:banner1.BannerLink %>" target="_blank">
                                <img src="<%:AppSettings.BannerGifFolder  + banner1.BannerResource %>" alt="" /></a>
                            <% }
                                else if (banner1.BannerResource.Contains("swf"))
                                { %>
                            <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0"
                                width="252">
                                <param name="wmode" value="transparent" />
                                <param name="movie" value="<%:AppSettings.BannerFlashFolder + banner1.BannerResource %>" />
                                <param name="quality" value="high" />
                                <param name="wmode" value="transparent" />
                                <embed src="<%:AppSettings.BannerFlashFolder + banner1.BannerResource %>" quality="high"
                                    pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash"
                                    type="application/x-shockwave-flash" width="252" wmode="transparent">
                                </embed>
                            </object>
                            <% }
                                else
                                {%>
                            <a href="http://<%:banner1.BannerLink %>" target="_blank">
                                <img width="252" src="<%:AppSettings.BannerImagesThumbFolder+ banner1.BannerResource %>"
                                    alt="" /></a>
                            <% } %>
                            <% } %>
                        </td>
                    </tr>
                </table>
            </div>
            <% } %>
            <%--  <script type="text/javascript"><!--
	 google_ad_client = "ca-pub-5337199739337318";
	 /* productadverb */
	 google_ad_slot = "7380353003";
	 google_ad_width = 250;
	 google_ad_height = 250;
//-->
</script>
<script type="text/javascript"
src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
<div style="margin-top:20px;">
<script type="text/javascript"><!--
  google_ad_client = "ca-pub-5337199739337318";
  /* 250&#42;250 */
  google_ad_slot = "2838428816";
  google_ad_width = 250;
  google_ad_height = 250;
//-->
</script>
<script type="text/javascript"
src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
</script>
</div>--%>
        </div>
        <div class="col-xs-12 col-sm-9 col-md-9 col-lg-9">
            <div class="row hidden-xs">
                <div class="col-xs-12">
                    <%if (ViewData["ViewNavigation"] != null)
                        { %>
                    <%=ViewData["ViewNavigation"]%>
                    <%}%>
                </div>
            </div>
            <%= Html.RenderHtmlPartial("ProductHeader", Model.ProductListViewModel)%>
            <div class="well well-mt2 mb20">
                <div class="row">
                    <div class="col-sm-12 col-md-8">

                        <div class="pull-left fontiz">
                            <%-- <span class="pull-right fontiz">--%>

                            <% if (Model.ActiveCategory.CategoryType >= (byte)CategoryType.Brand)
                                { %>
                            <% if (Model.ActiveCategory.CategoryType == (byte)CategoryType.Brand)
                                { %>
                            <h1 style="font-size: 18px; font-weight: bold; margin-top: 0;">
                                <%if (Model.ProductListViewModel.ProductSearchType == ProductSearchType.New)
                                    {%>
                                      Sıfır   
                                 <%}
                                     else if (Model.ProductListViewModel.ProductSearchType == ProductSearchType.Used)
                                     {%>
                                      İkinci El  
                                 <%}%>
                                <%:Model.ActiveCategory.CategoryName%>
                                <%:Model.TopCategoryItems.FirstOrDefault(c => c.CategoryId == Model.ActiveCategory.CategoryParentId.Value).CategoryName%>
                                <%if (Model.SelectedLocality != null)
                                    {%>
                                <%:Model.SelectedLocality.LocalityName%>
                                <%} %>
                                <%if (Model.SelectedCity != null)
                                    {%>
                                <%:Model.SelectedCity.CityName %>
                                <%} %>   </h1>
                            <span>kategorisinde <span style="color: #8f0100; font-weight: 700;">
                                <%:Model.ProductListViewModel.ProductSearch.TotalRecord%></span> <strong>Adet</strong>
                                ürün bulundu. </span>

                            <% }
                                else
                                { %>
                            <h1 style="font-size: 18px; font-weight: bold; margin-top: 0;">
                                <%if (Model.ProductListViewModel.ProductSearchType == ProductSearchType.New)
                                    {%>
                                     Sıfır   
                                 <%}
                                     else if (Model.ProductListViewModel.ProductSearchType == ProductSearchType.Used)
                                     {%>
                                      İkinci El  
                                 <%}%>
                                <%--<%:Model.TopCategoryItems.FirstOrDefault(c => c.CategoryId == Model.ActiveCategory.CategoryParentId.Value).CategoryName%>--%>
                                <%:Model.ActiveCategory.CategoryName%>
                                <%if (Model.SelectedLocality != null)
                                    {%>
                                <%:Model.SelectedLocality.LocalityName%>
                                <%} %>
                                <%if (Model.SelectedCity != null)
                                    {%>
                                <%:Model.SelectedCity.CityName %>
                                <%} %></h1>
                            <span>kategorisinde <span style="color: #8f0100; font-weight: 700;">
                                <%:Model.ProductListViewModel.ProductSearch.TotalRecord%></span> <strong>Adet</strong>
                                ürün bulundu. </span>
                            <% } %>
                            <% }
                                else if (Model.CategoryNameBrand != null)
                                { %>
                            <h1 style="font-size: 18px; font-weight: bold; margin-top: 0;">
                                <%if (Model.ProductListViewModel.ProductSearchType == ProductSearchType.New)
                                    {%>
                                     Sıfır   
                                 <%}
                                     else if (Model.ProductListViewModel.ProductSearchType == ProductSearchType.Used)
                                     {%>
                                     İkinci El  
                                 <%}%>
                                <%:Model.CategoryNameBrand.CategoryName%>
                                <%:Model.ActiveCategory.CategoryName%>
                                <%if (Model.SelectedLocality != null)
                                    {%>
                                <%:Model.SelectedLocality.LocalityName%>
                                <%} %>
                                <%if (Model.SelectedCity != null)
                                    {%>
                                <%:Model.SelectedCity.CityName %>
                                <%} %> </h1>
                            <span>kategorisinde <span style="color: #8f0100; font-weight: 700;">
                                <%:Model.ProductListViewModel.ProductSearch.TotalRecord%></span> <strong>Adet</strong>
                                ürün bulundu.</span>
                            <%}
                                else
                                { %>
                            <h1 style="font-size: 18px; font-weight: bold; margin-top: 0;">
                                <%if (Model.ProductListViewModel.ProductSearchType == ProductSearchType.New)
                                    {%>
                                     Sıfır   
                                 <%}
                                     else if (Model.ProductListViewModel.ProductSearchType == ProductSearchType.Used)
                                     {%>
                                     İkinci El  
                                 <%}%>
                                <%:Model.ActiveCategory.CategoryName%>
                                <%if (Model.SelectedLocality != null)
                                    {%>
                                <%:Model.SelectedLocality.LocalityName%>
                                <%} %>
                                <%if (Model.SelectedCity != null)
                                    {%>
                                <%:Model.SelectedCity.CityName %>
                                <%} %></h1>
                            <span>kategorisinde <span style="color: #8f0100; font-weight: 700;">
                                <%:Model.ProductListViewModel.ProductSearch.TotalRecord%></span> <strong>Adet</strong>
                                ürün bulundu.</span>
                            <% } %>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-4">
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-10">
                    <div class="tab-content" data-rel="productContent">
                        <% if (Model.ProductListViewModel.ProductSearch.Source != null && Model.ProductListViewModel.ProductSearch.Source.Count > 0)
                            { %>
                        <%= Html.RenderHtmlPartial("ProductViewList", Model.ProductListViewModel)%>
                        <%= Html.RenderHtmlPartial("ProductViewWindow", Model.ProductListViewModel)%>
                        <% }
                            else
                            { %>
                        <div class="productNotFound" style="height: 1000px; width: 468;">
                            <span>Bu kategoriye ait herhangi bir ürün bulunamadı.</span>
                            <script type="text/javascript"><!--
    google_ad_client = "ca-pub-5337199739337318";
    /* ifproductempty */
    google_ad_slot = "9812768954";
    google_ad_width = 468;
    google_ad_height = 60;
    //-->
                            </script>
                            <script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js">
                            </script>
                        </div>
                        <% } %>
                    </div>

                </div>
                <div class="hidden-xs hidden-sm col-md-2">
                    <% 
                        string app = "";
                        if (Request.Url.Segments.Length == 2)
                        {
                            app = "";
                        }
                        else
                        {
                            app = Request.Url.Segments[3].ToString();
                        }


                    %>
                    <% if (app.ToString() != "Sektor" || app.ToString() != "Sektor/")
                        {  %>
                    <%= Html.RenderHtmlPartial("_CategoryStores", Model)%>
                    <% } %>
                </div>
            </div>
            <input type="hidden" id="hdnPageNumber" />
            <input type="hidden" id="hdnCategoryId" value="<%:this.Page.RouteData.Values["categoryId"]%>" />
            <input type="hidden" id="hdnCategoryIdSecond" value="<%:this.Page.RouteData.Values["categoryIddown"]%>" />
            <input type="hidden" id="hdnCountryId" value="<%:this.Page.RouteData.Values["CountryId"]%>" />
            <input type="hidden" id="hdnCityId" value="<%:this.Page.RouteData.Values["CityId"]%>" />
            <input type="hidden" id="hdnLocalityId" value="<%:this.Page.RouteData.Values["LocalityId"]%>" />
            <input type="hidden" id="hdnHasCategory" value="<%:Model.ActiveCategory.CategoryType >= (byte)CategoryType.Brand && Model.ActiveCategory.CategoryType != (byte)CategoryType.ProductGroup ? "false" : "true" %>" />
            <input type="hidden" id="hdnDisplayType" value="<%:!string.IsNullOrEmpty(Request.QueryString["Gorunum"])? (Request.QueryString["Gorunum"].ToString()== "" ? DisplayType.List.GetDescription(): Request.QueryString["Gorunum"].ToString()):"" %>" />
            <input type="hidden" id="hdnSearchType" value="<%:!string.IsNullOrEmpty(Request.QueryString["SearchType"])? Request.QueryString["SearchType"].ToString():""%>" />
            <input type="hidden" id="hdnOrder" value="<%: !string.IsNullOrEmpty(Request.QueryString["Order"]) ? Request.QueryString["Order"].ToString():""%>" />
            <script type="text/javascript">

                function ProductPaging(p) {

                    $('#hdnPageNumber').val(p);
                    $('#aWindow').attr('href', '<%: Request.FilePath %>' + '?Gorunum=' + '<%= DisplayType.Window.GetDescription() %>' + '&amp;Sayfa=' + p + '&amp;SearchType=' + $('#hdnSearchType').val() + '&amp;Order=' + $('#hdnOrder').val());
                    $('#aText').attr('href', '<%: Request.FilePath %>' + '?Gorunum=' + '<%= DisplayType.Text.GetDescription() %>' + '&amp;Sayfa=' + p + '&amp;SearchType=' + $('#hdnSearchType').val() + '&amp;Order=' + $('#hdnOrder').val());
                    $('#aList').attr('href', '<%: Request.FilePath %>' + '?Gorunum=' + '<%= DisplayType.List.GetDescription() %>' + '&amp;Sayfa=' + p + '&amp;SearchType=' + $('#hdnSearchType').val() + '&amp;Order=' + $('#hdnOrder').val());

                    $('#aTumu').attr('href', '<%: Request.FilePath %>' + '?Gorunum=' + $('#hdnDisplayType').val() + '&amp;Sayfa=' + p + '&amp;Order=' + $('#hdnOrder').val());
                    $('#aSifir').attr('href', '<%: Request.FilePath %>' + '?Gorunum=' + $('#hdnDisplayType').val() + '&amp;Sayfa=' + p + '&amp;SearchType=sifir' + '&amp;Order=' + $('#hdnOrder').val());
                    $('#aIkinci').attr('href', '<%: Request.FilePath %>' + '?Gorunum=' + $('#hdnDisplayType').val() + '&amp;Sayfa=' + p + '&amp;SearchType=ikinciel' + '&amp;Order=' + $('#hdnOrder').val());
                    $('#aKiralik').attr('href', '<%: Request.FilePath %>' + '?Gorunum=' + $('#hdnDisplayType').val() + '&amp;Sayfa=' + p + '&amp;SearchType=kiralik' + '&amp;Order=' + $('#hdnOrder').val());
                    2
                    $('#aAz').attr('href', '<%: Request.FilePath %>' + '?Gorunum=' + $('#hdnDisplayType').val() + '&amp;Sayfa=' + p + '&amp;SearchType=' + $('#hdnSearchType').val() + '&amp;Order=a-z');
                    $('#aZa').attr('href', '<%: Request.FilePath %>' + '?Gorunum=' + $('#hdnDisplayType').val() + '&amp;Sayfa=' + p + '&amp;SearchType=' + $('#hdnSearchType').val() + '&amp;Order=z-a');
                    $('#aSEklenen').attr('href', '<%: Request.FilePath %>' + '?Gorunum=' + $('#hdnDisplayType').val() + '&amp;Sayfa=' + p + '&amp;SearchType=' + $('#hdnSearchType').val() + '&amp;Order=a-z');
                    $('#aEGoruntulenen').attr('href', '<%: Request.FilePath %>' + '?Gorunum=' + $('#hdnDisplayType').val() + '&amp;Sayfa=' + p + '&amp;SearchType=' + $('#hdnSearchType').val());

                    $('#divLoading').show();

                    $.ajax({
                        type: "POST",
                        url: '/Category/ProductPaging',
                        data:
		  {
		      page: p,
		      CategoryId: $('#hdnCategoryId').val(),
		      hasCategory: $('#hdnHasCategory').val(),
		      displayType: $('#hdnDisplayType').val(),
		      SearchType: $('#hdnSearchType').val(),
		      Order: $('#hdnOrder').val(),
		      CategoryIdSecond: $('#hdnCategoryIdSecond').val(),
		      CountryId: $('#hdnCountryId').val(),
		      CityId: $('#hdnCityId').val(),
		      LocalityId: $('#hdnLocalityId').val()
		  },
                        success: function (data) {
                            $('[data-rel="productContent"]').html(data)
                            $('#divLoading').hide();
                        }, error: function (x) {
                            $('#Product').html(x.responseText);
                            $('#divLoading').hide();
                        }
                    });
                }
            </script>
            <%--  <% }
               else
               { %>
            <% } %>--%>
        </div>
        <% } %>
    <% //TODO: kategori cok satanlar %>


        <div class="col-xs-12">
        <div class="top-seller-list">
            <div class="top-sellet-list__header">
                    Kategoride En Çok Bakılan Ürünler
            </div>
            <ul class="top-seller-list__items">
                <%
                    int categoryid = Model.ActiveCategory.CategoryId.ToInt32();
                    MakinaTurkiyeEntities entiti = new MakinaTurkiyeEntities();
                    var parameter = new System.Data.Objects.ObjectParameter("TotalRecord", typeof(int));
                    int PageDimension = 12;
                    int CurrentPage = 1;
                    var sorgu = entiti.spProductWebSearchMostViewDateByCategoryId(parameter, PageDimension, CurrentPage, 20, categoryid).ToList();
                    int productCount = 0;
                    //int count = 0;
                    foreach (var item in sorgu)
                    {
                        MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
                        var model2 = (from c in entities.Products
                                      where c.ProductId == item.ProductId
                                      select c.Currency.CurrencyName).FirstOrDefault();

                        string tutar = "";
                        string kusurat = string.Empty;

                        if (item.ProductPrice != null)
                        {
                            decimal sayi = (decimal)item.ProductPrice;
                            string tutarson = sayi.ToString("0.00");
                            string[] parcali = tutarson.Split(',');

                            try
                            {
                                if (tutarson.IndexOf('.') > -1)
                                {
                                    parcali = tutarson.Split('.');
                                }
                                tutar = parcali[0];
                                kusurat = parcali[1];

                            }
                            catch (Exception)
                            {


                            }

                        }

                        if (item.ProductPrice != null)
                        {
                            if (tutar != "0,00" && model2 != null)
                            {
                                //var phone=from c in entities.Phones
                                ViewData["dov"] = tutar + " " + model2.ToString();
                            }

                        }
                        string productUrl = Helpers.ProductUrl(item.ProductId, item.ProductName);


                %>
                <li class="top-seller-list__item clearfix">
                    <a href="<%= productUrl %>">
                        <span class="top-seller-list__ranking"><%:productCount +1 %></span>
                        <div class="top-seller-list__image">

                            <%  
                                Dictionary<string, object> imageHtmlAtturbiteTwo = new Dictionary<string, object>();
                                imageHtmlAtturbiteTwo.Add("alt", Html.Truncate(item.MainPicture, 80));
                                //imageHtmlAtturbiteTwo.Add("class", "img-mt2");
                                imageHtmlAtturbiteTwo.Add("title", Html.Truncate(item.MainPicture, 80));
                                if (!string.IsNullOrEmpty(Html.GetProductImage(item.ProductId, item.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px160x120, imageHtmlAtturbiteTwo).ToString()))
                                {
                            %>
                            <%= Html.GetProductImage(item.ProductId, item.MainPicture, NeoSistem.MakinaTurkiye.Web.Helpers.ImageHelpers.ImageSize.px160x120, imageHtmlAtturbiteTwo)%>
                            <% 
                                }
                                else
                                { %>
                            <img src="https://dummyimage.com/200x150/efefef/000000.jpg&text=urun%20resmi%20bulunamad%C4%B1" alt="<%=Html.Truncate(item.ProductName, 80)%>" title="<%=Html.Truncate(item.ProductName, 80)%>" />
                            <% }
                            %>
                        </div>
                        <div class="top-seller-list__item-product-info">
                            <h3 class="top-seller-list__item-product-title"><%= Html.Truncate(item.ProductName, 500)%></h3>
                            <p class="top-seller-list__item-product-brand">Marka : <strong><%=Html.Truncate(item.BrandName,500) %></strong> Model : <strong><%=Html.Truncate(item.ModelName,500) %></strong></p>
                            <p class="top-seller-list__item-product-price">
                                <%if (tutar != "0")
                                    {%>
                                 <% string currencyType = model2;
                                    if (currencyType == "USD")
                                    { %>
                                <i itemprop="priceCurrency" class="fa fa-usd"></i>
                                <%}
                                    else if (currencyType == "EUR")
                                    { %>
                                <i itemprop="priceCurrency" class="fa fa-eur"></i>
                                <%}
                                    else if (currencyType == "JPY")
                                    { %>
                                <i itemprop="priceCurrency" class="fa fa-jpy"></i>
                                <%}
                                    else
                                    {%>
                                <i itemprop="priceCurrency" class="fa fa-turkish-lira"></i>
                                <%}%>
                                   <% string yaz = "";
                                int v = Convert.ToInt32(tutar);
                                if (v > 0 && v < 10)
                                {
                                    yaz = v.ToString();
                                }
                                else
                                {
                                    yaz= string.Format("{0:0,0}",v).Replace(",", ".");
                                }%>
                            <%: yaz%><sup style="font-size: 9px;"><%:kusurat.ToString()=="00" ? "": ","+kusurat %></sup>
                                <% }
                                    else
                                    { %>
                                                       <span class="interview">Fiyat Sorunuz</span>
                                                    <%} %>
                            </p>
                        </div>
                        <p class="top-seller-list__item-product-price">
                            <%if (tutar != "0")
                                {%>
                                                        <% string currencyTypeClone = model2;
                                if (currencyTypeClone == "USD")
                                { %>
                            <i itemprop="priceCurrency" class="fa fa-usd"></i>
                            <%}
                                else if (currencyTypeClone == "EUR")
                                { %>
                            <i itemprop="priceCurrency" class="fa fa-eur"></i>
                            <%}
                                else if (currencyTypeClone == "JPY")
                                { %>
                            <i itemprop="priceCurrency" class="fa fa-jpy"></i>
                            <%}
                                else
                                {%>
                            <i itemprop="priceCurrency" class="fa fa-turkish-lira"></i>
                            <%}%>

                            <% int v = Convert.ToInt32(tutar); %>
                            <%:string.Format("{0:0,0}",v).Replace(",", ".") %><sup style="font-size: 9px;"><%:kusurat.ToString()=="00" ? "": ","+kusurat %></sup>
                            <% }
                                else
                                { %>
                                                       <span class="interview">Fiyat Sorunuz</span>
                                                    <%} %>
                        </p>
                    </a>
                </li>
                <% 
                        productCount += 1;
                    } %>
            </ul>

        </div>
        </div>

    </div>

    <%=Html.RenderHtmlPartial("_CategoryContent",Model.CategoryContentModel)%>
</asp:Content>
