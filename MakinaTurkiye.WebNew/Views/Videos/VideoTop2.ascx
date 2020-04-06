<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<% var product = new ProductDetailInfo();
    product = ViewData["makinaentities"] as ProductDetailInfo;
    var video = new Video();
    if (ViewData["groupname"] == null) { ViewData["groupname"] = "Makina"; };
    string productUrl = Helpers.ProductUrl(product.ProductId, product.ProductName);
    string storeLogo = ImageHelpers.GetStoreImage((int)ViewData["storeMainPartyId"], (string)ViewData["storeLogo"], "100");
%>
<div class="col-sm-12 col-md-8">
    <%if (Page.RouteData.Values["VideoId"] != null)
        { %>
    <%video = ViewData["video"] as Video; %>
    <style>
        /*.videocontent {
            width: 100%;
            max-width: 755px;
            margin: 0 auto;
            min-width: 230px;
            height: 400px;
            max-height: 400px;
        }*/
    </style>
    <div class="videocontent">
        <video id="vd" class="video-js vjs-default-skin" controls preload="auto" autoplay="autoplay" width="100%"
            height="100%" style="padding: 4px; border: 1px solid #ddd; border-radius: 4px; transition: all .2s ease-in-out; display: inline-block;"
            poster="" data-setup='{"techOrder": ["html5", "flash"]}'>
            <source src="https://s.makinaturkiye.com/NewVideos/<%= video.VideoPath %>.mp4" type='video/mp4' />
        </video>
    </div>
    <%}
        else
        {  %>
    <img class="img-thumbnail" src="https://dummyimage.com/800x400/efefef/000000.jpg&text=video" alt=".." />
    <%} %>
    <h3>
        <%:product.ProductName %>
    </h3>
    <div class="well">

        <div class="col-xs-12  col-sm-9 media">
            <a class="pull-left" href="/Sirket/<%=product.StoreMainPartyId %>/<%=Helpers.ToUrl(product.StoreName) %>/SirketProfili">
                <img class="media-object" src="<%: storeLogo %>" alt="<%:product.ProductName %>" />
            </a>
            <div class="media-body">
                <div class="media-heading">
                    <span class="badge disabled mb10">
                        <%=video.SingularViewCount %>
                        Görüntüleme </span>
                    <br />
                    <a href="/Sirket/<%=product.StoreMainPartyId %>/<%=Helpers.ToUrl(product.StoreName) %>/SirketProfili">
                        <%:product.StoreName %>
                    </a>
                    <br />
                    tarafından
                    <%:video.VideoRecordDate.ToDateTime().ToShortDateString()%>
                    tarihinde eklendi.
                    <br />
                    <br />
                    <div class="btn-group">
                        <a href="#" class="btn btn-xs btn-default">
                            <span class="glyphicon glyphicon-facetime-video"></span>&nbsp;Diğer Videolar </a>

                        <a href="/Sirket/<%=product.StoreMainPartyId %>/<%=Helpers.ToUrl(product.StoreName) %>/Urunler" class="btn btn-xs btn-default">
                            <span class="glyphicon glyphicon-shopping-cart"></span>&nbsp;Tüm Ürünler </a>

                        <%--<a href="javascript:void(0)" class="btn btn-xs btn-default">
                            <span class="glyphicon glyphicon-envelope"></span>&nbsp;Mesaj Gönder</a>

                        <a href="javascript:void(0)" class="btn btn-xs btn-default">
                            <span class="glyphicon glyphicon-share"></span>&nbsp;Paylaş</a>--%>
                    </div>
                </div>
            </div>
        </div>
        <div class="hidden-xs col-sm-3 text-right">
            <span>
                <%:product.BrandName %>-<%:product.ModelName %>
            </span>
            <br />
            <span>
                <%if (product.ProductTypeText != "" || product.ProductTypeText != null || product.ProductTypeText != " ")
                    {%>
                <%: product.ProductTypeText%>
                <%}
                    else
                    { %>Satılık<%} %>-<%:product.ProductStatuText%>
            </span>
            <br />
            <br />
            <div class="btn-group pull-right">
                <a href="#" class="btn btn-xs btn-default disabled"><b>
                    <% string currencyType = (string)ViewData["currencyType"];
                        string tutar = ViewData["dov"].ToString();
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
                    <%if (tutar != "0")
                        {%>
                    <%
                        decimal moneyvalue = Convert.ToDecimal(tutar.Replace(",00", ""));
                        string html = String.Format("{0:C}", moneyvalue);
                        tutar = html;
                    %>
                    <%:tutar.Replace("₺","") %>
                    <% }
                        else
                        { %>
                    <span style="color: Green; font-size: 11px; font-weight: bold;">00,00</span>
                    <%} %>
                </b></a><a href="<%=productUrl %>" class="btn btn-xs btn-danger">Ürünü İncele </a>
            </div>
        </div>
        <%-- Social Media --%>
        <div class="col-xs-12 col-sm-9 media text-center">
         
        </div>
        <span class="clearfix"></span>&nbsp;<br />
     
    </div>
    <%-- <h4>
        Yorumlar <span class="text-muted">(3 Yorum) </span>
    </h4>
    <hr />
    <div>
        <img src="http://www.placehold.it/66x50" class="img-left pull-left" alt="..">
        <a href="#">Lorem ipsum dolor </a><span class="text-muted">3 gün önce </span>
        <p class="text-dark">
            Consectetur adipiscing elit. Nulla massa libero, suscipit vel arcu quis, ultricies
            fermentum arcu.
        </p>
    </div>
    <span class="clearfix"></span>
    <hr />
    <div>
        <img src="http://www.placehold.it/66x50" class="img-left pull-left" alt="..">
        <a href="#">Lorem ipsum dolor </a><span class="text-muted">3 gün önce </span>
        <p class="text-dark">
            Consectetur adipiscing elit. Nulla massa libero, suscipit vel arcu quis, ultricies
            fermentum arcu.
        </p>
    </div>
    <span class="clearfix"></span>
    <hr />
    <div>
        <img src="http://www.placehold.it/66x50" class="img-left pull-left" alt="..">
        <a href="#">Lorem ipsum dolor </a><span class="text-muted">3 gün önce </span>
        <p class="text-dark">
            Consectetur adipiscing elit. Nulla massa libero, suscipit vel arcu quis, ultricies
            fermentum arcu.
        </p>
    </div>
    <span class="clearfix"></span>--%>
</div>
<div class="visible-xs mb20">
    &nbsp;
</div>
<div class="visible-sm mb20">
    &nbsp;
</div>
<script type="text/javascript">
    var videoItemId = <%=video.VideoId %>;
    PopulerVideosRemoveItemVideo(videoItemId);
</script>