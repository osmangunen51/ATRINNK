﻿<%@ Control Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTStoreProfileHeaderModel>" %>
<style>
    .main-container.container { width: 100%; padding: 0; margin-top: -20px; }

    .fixed { position: fixed; top: 60px; left: 50px; }

    .absolute { position: absolute; bottom: 0px; left: 0 !important; }
</style>




<div id="theiaStickySidebar">
    <div class="StoreProfileMenu">

        <%-- Store Search --%>
        <div class="StoreProfileSearchBox">
            <div class="store-search" style="margin-bottom: 20px;">
                <div class="input-group">
                    <input type="text" class="form-control" name="productName" id="productNameForSearch" placeholder="Firma içi ürün arama.." aria-describedby="basic-addon2" style="border-radius: 0; border: 1px solid #fc8120">
                    <span class="input-group-btn">
                        <button onclick="ProductSearchResult();" id="searchButtonAdvert" class="btn  btn-default" type="submit" style="background: #fc8120; border: 1px solid #fc8120; border-radius: 0; color: #fff;">
                            <span class="glyphicon glyphicon-search"></span>
                        </button>
                    </span>
                </div>
            </div>
            <div class="small" id="dataNo" style="display: none;">
                <div style="padding: 5px!important; margin-bottom: 5px!important;" class="alert alert-danger">
                    aradığınız içerik bulunamadı
                </div>
            </div>
        </div>
        <%-- Store Search --%>


        <%-- Store Menü --%>
        <nav>
            <ul role="menubar" class="list-group">

                <li class="<%: Model.MTStoreProfileMenuActivePage.HomeActive %>"><span class="fa fa-home" aria-hidden="true"></span><a href="<%=Model.StoreUrl %>">Anasayfa</a></li>
                <% if (Model.MTStoreProfileMenuHasModel.HasProducts)
                    { %>
                <li class="<%:Model.MTStoreProfileMenuActivePage.ProductsActive %>"><span class="fa fa-shopping-cart" aria-hidden="true"></span><a href="<%=Model.StoreUrl %>/urunler">Ürünler</a><i class="icon-down-arrow menuarrow"></i>
                    <%  var request = Request;
                        string[] req = request.Url.AbsolutePath.Split('/');
                        if (req.Length > 2)
                        {
                            string[] abs = req[2].ToString().Split('-');
                            int catId = abs[abs.Length - 1].ToInt32();
                    %>   <%=Html.Action("_CategoryProduct", "StoreProfileNew", new {storeId=Model.MainPartyId,categoryId=catId})%>
                    <%}
                        else
                        {%>
                    <%=Html.Action("_CategoryProduct", "StoreProfileNew", new {storeId=Model.MainPartyId})%>
                    <%} %>



                    <%--     <ul class="sub-menu">
                                <li class="sub-menu-item">
                                    <a href="#">Test</a>
                                </li>
                                <li class="sub-menu-item">
                                    <a href="#">Test</a><i class="icon-down-arrow menuarrow"></i>
                                     <ul class="sub-menu">
                                        <li class="sub-menu-item">
                                            <a href="#">Test</a>
                                        </li>
                                        <li class="sub-menu-item">
                                            <a href="#">Test</a>
                                        </li>
                                      </ul>
                                </li>

                            </ul>--%>

                </li>
                <%} %>
                <% if (Model.MTStoreProfileMenuHasModel.HasAbout)
                    { %>
                <li class="<%:Model.MTStoreProfileMenuActivePage.AboutAsActive %>"><span class="fa fa-users" aria-hidden="true"></span><a href="<%=Model.StoreUrl %>/hakkimizda">Hakkımızda</a></li>
                <% } %>
                <% if (Model.MTStoreProfileMenuHasModel.HasBranch)
                    { %>
                <li class="<%:Model.MTStoreProfileMenuActivePage.BranchActive %>"><span class="fa fa-cubes" aria-hidden="true"></span><a href="<%=Model.StoreUrl %>/subelerimiz">Şubelerimiz</a>
                </li>
                <% } %>
                <% if (Model.MTStoreProfileMenuHasModel.HasServices)
                    { %>
                <li class="<%:Model.MTStoreProfileMenuActivePage.ServicesActive%>"><span class="fa fa-share-alt" aria-hidden="true"></span><a href="<%=Model.StoreUrl %>/servisagimiz">Servis Ağımız</a>
                </li>
                <% } %>
                <% if (Model.MTStoreProfileMenuHasModel.HasDealerShip)
                    { %>
                <li class="<%: Model.MTStoreProfileMenuActivePage.DealerShipActive %>"><span class="fa fa-forumbee" aria-hidden="true"></span><a href="<%=Model.StoreUrl %>/bayiliklerimiz">Bayiliklerimiz</a>
                </li>
                <% } %>
                <% if (Model.MTStoreProfileMenuHasModel.HasDealer)
                    { %>
                <li class="<%: Model.MTStoreProfileMenuActivePage.DealerActive %>"><span class="fa fa-sitemap" aria-hidden="true"></span><a href="<%=Model.StoreUrl %>/bayiagimiz">Bayi Ağımız</a>
                </li>
                <% } %>
                <% if (Model.MTStoreProfileMenuHasModel.HasBrand)
                    { %>
                <li class="<%: Model.MTStoreProfileMenuActivePage.BrandActive %>"><span class="fa fa-tag" aria-hidden="true"></span><a href="<%=Model.StoreUrl %>/markalarimiz">Markalarımız</a>
                </li>
                <% } %>
                <%if (Model.MTStoreProfileMenuHasModel.HasImages)
                    {%>
                <li class="<%: Model.MTStoreProfileMenuActivePage.StoreImagesActive %>"><span class="fa fa-video-camera" aria-hidden="true"></span><a href="<%=Model.StoreUrl %>/gorseller">Görsellerimiz</a>
                </li>

                <% } %>
                <%if (Model.MTStoreProfileMenuHasModel.HasVideos)
                    { %>
                <li class="<%: Model.MTStoreProfileMenuActivePage.VideosActive %>"><span class="fa fa-video-camera" aria-hidden="true"></span><a href="<%=Model.StoreUrl %>/videolarimiz">Videolarımız</a>
                </li>
                <%} %>
                <%if (Model.MTStoreProfileMenuHasModel.HasCatolog)
                    {%>
                <li class="<%: Model.MTStoreProfileMenuActivePage.CatologActive %>"><span class="fa fa-file" aria-hidden="true"></span><a href="<%=Model.StoreUrl %>/kataloglar">Kataloglar</a>
                </li>
                <% } %>
                <%if (Model.MTStoreProfileMenuHasModel.HasNew)
                    {%>
                <li class="<%: Model.MTStoreProfileMenuActivePage.NewActive %>"><span class="fa fa-list" aria-hidden="true"></span><a href="<%=Model.StoreUrl %>/haberler">Haberler</a>	</li>
                <% } %>
                <li class="<%: Model.MTStoreProfileMenuActivePage.ConnectionActive %>"><span class="fa fa-phone" aria-hidden="true"></span><a href="<%=Model.StoreUrl %>/iletisim">İletişim</a>
                </li>
            </ul>
        </nav>
        <%-- Store Menü --%>

                <%var videoModel = Model.StoreVideo; %>
        <%if(!string.IsNullOrEmpty(videoModel.VideoUrl)){%>
                <div class="store-menu-video">
       
            <div class="video-container" style="">
                <span class="store-menu-video-title"><%:videoModel.VideoTitle %></span>
                <a style="cursor: pointer" data-toggle="modal" data-target="#video5594">
                    <img class="img-responsive" src="<%:videoModel.PicturePath %>">
                </a>
                <div class="minute-container" style="position: absolute;">
                    <%:videoModel.VideoMinute %>:<%:videoModel.VideoSecond %>
                </div>
                <div class="modal fade" id="video5594" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header" style="height: 50px">
                                <h5 class="modal-title" style="float: left;" id="exampleModalLabel"><%:videoModel.VideoTitle %></h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span style="font-size: 30px;" aria-hidden="true">×</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="videocontent">
                                    <video id="vd" class="video-js vjs-default-skin" controls="" preload="auto" width="100%" height="100%" style="width: 100%; height: 100%; padding: 4px; border: 1px solid #ddd; border-radius: 4px; transition: all .2s ease-in-out; display: inline-block;" poster="" data-setup="{&quot;techOrder&quot;: [&quot;html5&quot;]}">
                                        <source src="https://s.makinaturkiye.com/NewVideos/<%:videoModel.VideoPath %>.mp4" type="video/mp4">
                                    </video>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        
        </div>
        
        <%} %>

        <%-- Store Share --%>
        <script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-54cd19b3604b2ec3" async="async"></script>
        <div class="addthis_inline_share_toolbox"></div>
        <%-- Store Share --%>


    </div>


</div>




