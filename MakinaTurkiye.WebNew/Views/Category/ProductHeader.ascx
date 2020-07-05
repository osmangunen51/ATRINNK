﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTProductFilteringModel>" %>
<% byte gorunum = Request.QueryString["Gorunum"].ToByte(); %>
<%string formatsize = "class=\"productHeaderContainer\""; %>
<%string formatsize2 = "class=\"productHeaderContainer3\""; %>
<% string page = ""; if (Request.QueryString["Sayfa"] != null) { page = "&Sayfa=" + Request.QueryString["Sayfa"]; }  %>
<% string activeCss = "active"; %>

<% string querySearchType = string.IsNullOrEmpty(Request.QueryString["SearchType"]) ? "" : "&SearchType=" + Request.QueryString["SearchType"].ToString(); %>
<% string queryOrder = string.IsNullOrEmpty(Request.QueryString["Order"]) ? "" : Request.QueryString["Order"].ToString(); %>
<% string display;
    string displayGrid = "";
    string displayList = "";
    if (string.IsNullOrEmpty(Request.QueryString["ulke"]))
    {
        display = string.IsNullOrEmpty(Request.QueryString["Gorunum"]) ? "?Gorunum=Galeri" : "?Gorunum=" + Request.QueryString["Gorunum"];
        displayGrid = "?Gorunum=Galeri";
        displayList = "?Gorunum=Liste";
    }
    else
    {
        display = string.IsNullOrEmpty(Request.QueryString["Gorunum"]) ? "&Gorunum=Galeri" : "&Gorunum=" + Request.QueryString["Gorunum"].ToString();
        displayGrid = "&Gorunum=Galeri";
        displayList = "&Gorunum=Liste";
    }
%>
<% string orderTip = ""; if (Request.QueryString["Order"] != null) { orderTip = "&amp;Order=" + Request.QueryString["Order"]; } %>
<% string searchText = ""; if (Request.QueryString["SearchText"] != null) { searchText = "&SearchText=" + Request.QueryString["SearchText"]; } %>
<% string countryFilter = ""; if (Request.QueryString["ulke"] != null) { countryFilter = "?ulke=" + Request.QueryString["ulke"]; } %>
<% string cityFilter = ""; if (Request.QueryString["sehir"] != null) { cityFilter = "&sehir=" + Request.QueryString["sehir"]; } %>
<% string localityFilter = ""; if (Request.QueryString["ilce"] != null) { localityFilter = "&ilce=" + Request.QueryString["ilce"]; } %>

<div class="row">
    <div class="col-xs-9 col-sm-6 col-md-8">
        <a href="<%: Request.FilePath %><%:countryFilter %><%:cityFilter %><%:localityFilter %><%= display %><%:page  %><%:orderTip %><%:searchText %>" class="categort-filter__link <%=Model.CustomFilterModels.FirstOrDefault(k => k.Selected).FilterId == (byte)ProductSearchTypeV2.All ? activeCss : string.Empty%>">Tümü</a>
        <a href="<%: Request.FilePath %><%:countryFilter %><%:cityFilter %><%:localityFilter %><%= display %><%:page  %><%:orderTip %><%:searchText %>&amp;SearchType=sifir" class="categort-filter__link <%= Model.CustomFilterModels.FirstOrDefault(k => k.Selected).FilterId == (byte)ProductSearchTypeV2.New ? activeCss : string.Empty %>">Sıfır<span class="category-filter-count">&nbsp(<%: Model.CustomFilterModels.FirstOrDefault(x => x.FilterId == (byte)ProductSearchTypeV2.New).ProductCount %>)</span></a>
        <%var usedProduct = Model.CustomFilterModels.FirstOrDefault(x => x.FilterId == (byte)ProductSearchTypeV2.Used);
            if (usedProduct != null)
            {
                if (usedProduct.ProductCount > 0)
                {
        %>
        <a href="<%: Request.FilePath %><%:countryFilter %><%:cityFilter %><%:localityFilter %><%= display %><%:page  %><%:orderTip %><%:searchText %>&amp;SearchType=ikinciel" class="categort-filter__link <%= Model.CustomFilterModels.FirstOrDefault(k => k.Selected).FilterId == (byte)ProductSearchTypeV2.Used ? activeCss : string.Empty%>">İkinci El<span class="category-filter-count">&nbsp(<%:usedProduct.ProductCount %>)</span></a>
        <%}
            } %>

        <% var service = Model.CustomFilterModels.FirstOrDefault(x => x.FilterId == (byte)ProductSearchTypeV2.Services);
            if (service != null)
            {
                if (service.ProductCount > 0)
                {
        %>
        <a href="<%: Request.FilePath %><%:countryFilter %><%:cityFilter %><%:localityFilter %><%= display %><%:page  %><%:orderTip %><%: searchText %>&amp;SearchType=hizmet" class="categort-filter__link <%= Model.CustomFilterModels.FirstOrDefault(k => k.Selected).FilterId == (byte)ProductSearchTypeV2.Services ? activeCss : string.Empty%>">Hizmet&nbsp<span class="category-filter-count">(<%:service.ProductCount %>)</span></a>
        <% }
            } %>
    </div>
    <div class="col-xs-3 col-sm-6 col-md-4 ">
        <div class="col-md-7 hidden-xs col-xs-1">
            <div class="row">
                <div class="dropdown">
                    <button class="btn btn-default btn-block dropdown-toggle" type="button" id="btnGroupVerticalDrop1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                        Sıralama Türü Seçiniz
                    <span class="caret"></span>
                    </button>
                    <ul class="dropdown-menu dropdown-menu-right" aria-labelledby="btnGroupVerticalDrop1">
                        <li>
                            <a class="<%= queryOrder == "a-z" ? activeCss : string.Empty%>" href="<%: Request.FilePath %><%:countryFilter %><%:cityFilter %><%:localityFilter %><%= display %><%: searchText %><%:page  %><%:querySearchType %>&amp;Order=a-z">a-Z</a>
                        </li>
                        <li>
                            <a class="<%= queryOrder == "z-a" ? activeCss : string.Empty%>" href="<%: Request.FilePath %><%:countryFilter %><%:cityFilter %><%:localityFilter %><%= display %><%: searchText %><%:page  %><%:querySearchType %>&amp;Order=z-a">Z-a</a>
                        </li>
                        <li>
                            <a class="<%= queryOrder == "soneklenen" ? activeCss : string.Empty%>" href="<%: Request.FilePath %><%:countryFilter %><%:cityFilter %><%:localityFilter %><%= display %><%: searchText %><%:page  %><%:querySearchType %>&amp;Order=soneklenen">Son Eklenen</a>
                        </li>
                        <li>
                            <a class="<%= queryOrder == "encokgoruntulenen" ? activeCss : string.Empty%>" href="<%: Request.FilePath %><%:countryFilter %><%:cityFilter %><%:localityFilter %><%= display %><%: searchText %><%:page  %><%:querySearchType %>">En Çok Görüntülenen</a>
                        </li>
                        <li>
                            <a class="<%= queryOrder == "fiyat-artan" ? activeCss : string.Empty%>" href="<%: Request.FilePath %><%:countryFilter %><%:cityFilter %><%:localityFilter %><%= display %><%: searchText %><%:page  %><%:querySearchType %>&amp;Order=fiyat-artan">Fiyata Göre Artan</a>

                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div class="col-md-5 col-xs-11">
           <div class="categort-filter__view__buttons">
                <a href="<%: Request.FilePath %><%:countryFilter %><%:cityFilter %><%:localityFilter %><%= displayGrid %><%: searchText %><%:page  %><%:querySearchType %>" class="<%:Request.QueryString["Gorunum"] == null || (Request.QueryString["Gorunum"]!=null && Request.QueryString["Gorunum"].ToString()=="Galeri") ? "active":"" %>"><i class="fa fa-th"></i></a>
                <a href="<%: Request.FilePath %><%:countryFilter %><%:cityFilter %><%:localityFilter %><%=displayList   %><%: searchText %><%:page  %><%:querySearchType %>" class="<%: (Request.QueryString["Gorunum"]!=null && Request.QueryString["Gorunum"].ToString()=="Liste") ?  "active":""%>"><i class="fa fa-list"></i></a>
            </div>
        </div>
    </div>
</div>

