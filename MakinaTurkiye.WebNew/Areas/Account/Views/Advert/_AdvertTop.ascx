<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.MTAdvertsTopViewModel>" %>

<div class="well well-mt2 p0 m0" style="height: 30px;">
    <div class="col-sm-12 col-md-4 col-lg-4 pr0">
        <div class="btn-group btn-group-justified">

            <%   string currentUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                string sCurrentValue = Request.QueryString["DisplayType"];
                string newvalue = ((byte)DisplayType.List).ToString();
                string urlList = "";
                urlList = currentUrl.Replace("DisplayType=" + sCurrentValue, "DisplayType=" + newvalue);

            %>
            <span class="btn btn-sm disabled btn-mt2">Görünüm: </span><a href="<%:urlList %>"
                class="btn btn-sm btn-mt2 <%:(sCurrentValue==newvalue?"active":"") %>"><span class="glyphicon glyphicon-th"></span>
            </a>

            <%   
                newvalue = ((byte)DisplayType.Table).ToString();
                string urlTable = "";
                urlTable = currentUrl.Replace("DisplayType=" + sCurrentValue, "DisplayType=" + newvalue);
            %>
            <a href="<%:urlTable %>"
                class="btn btn-sm btn-mt2 <%:(sCurrentValue==newvalue?"active":"") %>"><span class="glyphicon glyphicon-th-list"></span>
            </a>
        </div>
    </div>
    <div class="hidden-sm hidden-xs col-md-8  col-lg-8 pl0 mb20">
        <div class="btn-group btn-group-justified">
            <span class="btn btn-sm disabled btn-mt2"></span>
            <div class="btn-group">
                <a href="#" class="btn btn-sm btn-mt2 dropdown-toggle" data-toggle="dropdown">Sıralama
                                            <span class="caret"></span></a>
                <ul class="dropdown-menu dropdown-menu-mt" role="menu">
                    <%foreach (var item in Model.MTOrderFilter.ToList())
                        {%>
                    <li class="<%:item.CssClass %>"><a href="<%:item.FilterUrl %>"><%:item.FilterName %> </a></li>
                    <%} %>
                </ul>
            </div>
            <div class="btn-group">
                <a href="#" class="btn btn-sm btn-mt2 dropdown-toggle" data-toggle="dropdown">Kategoriler
                                            <span class="caret"></span></a>
                <ul class="dropdown-menu dropdown-menu-mt" role="menu">
                    <%for (int i = 0; i < Model.MTCategoriesFilter.Count; i++)
                        {
                            var categoryFilter = Model.MTCategoriesFilter[i];
                    %>
                    <li><a href="<%:categoryFilter.FilterUrl %>"><%:categoryFilter.FilterName %> </a></li>
                    <% } %>
                </ul>
            </div>
            <div class="btn-group">
                <a href="#" class="btn btn-sm btn-mt2 dropdown-toggle" data-toggle="dropdown">Tüm İlanlar
                                            <span class="text-muted">(<%:Model.TotalProductCount%>) </span><span class="caret"></span></a>
                <ul class="dropdown-menu dropdown-menu-mt" role="menu">
                    <%foreach (var item in Model.MTAdvertFilterItemModel)
                        {%>
                    <li class="<%:item.CssClass%>"><a href="<%:item.FilterUrl %>"><%:item.FilterName %> 
                    </a></li>

                    <%} %>
                </ul>
            </div>
        </div>
    </div>
</div>
