<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTStoreAndProductDetailModel>" %>

<h1 style="font-size: 24px!important;">
    <%:Model.VideoTitle %> / Video
</h1>
<div class="well">
    <div class="col-xs-12  col-sm-9 media">
        <a class="pull-left" href="<%:Model.StoreUrl %>">
            <img class="media-object" src="<%:Model.StoreLogo %>" alt=" <%:Model.ProductName %>" />
        </a>
        <div class="media-body">
            <div class="media-heading">
                <span class="badge disabled mb10">
                    <%=Model.VideoSingularViewCount %>
                        Görüntüleme </span>
                <br />
                <a href="<%:Model.StoreUrl %>">
                    <%:Model.StoreName %>
                </a>
                <br />
                tarafından
                    <%:Model.VideoRecordDate.ToDateTime().ToShortDateString()%>
                    tarihinde eklendi.
                    <br />
                <br />
                <div class="btn-group">
                    <a href="<%:Model.StoreVideosPageUrl%>" class="btn btn-xs btn-default">
                        <span class="glyphicon glyphicon-facetime-video"></span>&nbsp;Diğer Videolar </a>

                    <a href="<%:Model.StoreProductsUrl%>" class="btn btn-xs btn-default">
                        <span class="glyphicon glyphicon-shopping-cart"></span>&nbsp;Tüm Ürünler </a>

                </div>
            </div>
        </div>
    </div>
    <div class="hidden-xs col-sm-3 text-right">
        <span>
            <%:Model.BrandName %>-<%:Model.ModelName %>
        </span>
        <br />
        <span>
            <%if (Model.ProductTypeText != "" || Model.ProductTypeText != null || Model.ProductTypeText != " ")
                {%>
            <%: Model.ProductTypeText%>
            <%}
                else
                { %>Satılık<%} %>-<%:Model.ProductStatus%>
        </span>
        <br />
        <br />
        <div class="btn-group pull-right">
            <a href="#" class="btn btn-xs btn-default disabled"><b>
                <%:Model.ProductPrice!=""?Model.ProductPrice:"--"%>

            </b></a><a href="<%:Model.ProductUrl %>" class="btn btn-xs btn-danger">Ürünü İncele </a>
        </div>
    </div>
    <%-- Social Media --%>
    <div class="col-xs-12 col-sm-9 media text-center">
    </div>
    <span class="clearfix"></span>&nbsp;<br />
    <%:SeoModel.GeneralforAll(ViewData["SEOPAGETYPE"].ToByte()).Description%>
</div>
