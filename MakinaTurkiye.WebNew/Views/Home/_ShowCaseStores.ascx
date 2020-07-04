﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTHomeStoreModel>>" %>


<div class="row one-cikan-firma-urunleri">
    <div class="col-xs-12">
        <h2 class="section-title">
            <span>Öne Çıkan Firmalar</span>
        </h2>
    </div>

    <div class="col-xs-12">
        <div class="row">
            <%foreach (var store in Model)
              {%>
            <div class="col-md-4 col-sm-6">

                <div class="firma-one-cikan-type-box">
                    <a href="<%:store.StoreUrl %>">
                        <!-- Heading -->
                        <div class="firma-one-cikan-type-heading">
                            <!-- Sol -->
                            <div class="firma-one-cikan-type-heading-sol">
                                <div class="firma-one-cikan-logo">
                                    <img src="/UserFiles/image-loading.png" data-src="<%=store.StoreLogo %>" class="img-lazy" alt="<%=store.StoreName %>" width="80" height="80" style="width: 80px; height: 80px;">
                                </div>
                            </div>
                            <!-- Sol -->
                            <!-- Sağ -->
                            <div class="firma-one-cikan-type-heading-sag">
                                <div class="firma-one-cikan-type-firma-adi">
                                    <h3 class="text-nowrap"><%=store.StoreName%></h3>
                                </div>
                                <div class="firma-one-cikan-type-aciklama">
                                    <p><%=store.StoreAbout%></p>
                                </div>
                            </div>
                            <!-- Sağ -->
                        </div>
                        <!-- Heading -->
                    </a>

                    <!-- Body -->
                    <div class="firma-one-cikan-type-body">
                        <div class="firma-one-cikan-type-image-list">
                            <%foreach (var picture in store.TopProductPictures)
                              {%>
                            <div class="firma-one-type-image-box">
                                <a href="<%=picture.ProductUrl %>">
                                    <img   src="/UserFiles/image-loading.png"
                            data-src="<%=picture.PicturePath %>" alt="<%=picture.PictureName %>" title="<%=picture.PictureName %>" class="img-responsive img-lazy" />
                                </a>
                            </div>
                            <% } %>
                        </div>
                    </div>
                    <!-- Body -->

                </div>
            </div>

            <%  } %>
        </div>
    </div>
</div>


<%--<div class="row">
    <div class="col-xs-12">
        <h2 class="section-title">
            <span>Vitrindeki Firmalar</span>
        </h2>
    </div>
    <div class="col-xs-12">
        <div class="vitrindeki-firmalar">
            <div class="vitrindeki-firmalar__inner">
                <div id="vitrindeki-firmalar" class="carousel slide" data-ride="carousel" data-interval="false">
                    <div class="carousel-inner">
                        <%
                            int pageCount = 0;
                            foreach (var store in Model.ToList())
                            {
                                if (pageCount == 0)
                                { 
                        %>
                        <div class="item <%= Model.First() == store? "active" : string.Empty %>">
                            <%}%>
                            <div class="col-sm-6 col-md-6 p0" style="height: 80px;">
                                <div class="panel-list-item clearfix">

                                    <a href="<%:store.StoreUrl %>" class="text-bold">
                                        <img src="<%=store.StoreLogo %>" alt="<%: store.StoreName %>" class="pull-left" width="70" height="70">
                                        <div class="panel-list-item-caption">
                                            <%=store.StoreName%>
                                            <p class="text-muted">
                                                <%=store.StoreAbout%>
                                            </p>
                                        </div>
                                    </a>
                                </div>
                            </div>
                            <%  
                        pageCount += 1;
                        if (pageCount % 10 == 0)
                        { %>
                        </div>
                        <% pageCount = 0;
                                   } %>
                        <% } %>
                    </div>
                    <a href="/sirketler" class="pull-right label label-mt"><span class="glyphicon glyphicon-list"></span>Tüm Firmaları Listele </a><span class="clearfix"></span>
                </div>
                <ol class="carousel-indicators carousel-indicators-mt carousel-indicators-bl">
                    <%
                        int count = 0;
                        int pageSize = 0;
                        foreach (var store in Model)
                        {
                            if (count % 10 == 0)
                            {
                    %>
                    <li data-target="#vitrindeki-firmalar" data-slide-to="<%:pageSize%>" class="<%= Model.First() == store? "active" : string.Empty %>"></li>
                    <% pageSize += 1;
                    }
                    count += 1;
                } %>
                </ol>
            </div>
        </div>
    </div>
</div>
</div>--%>
 









