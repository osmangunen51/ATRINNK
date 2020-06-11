﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<MTProductDetailViewModel>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <%=Html.RenderHtmlPartial("_ProductHeader") %>
    <%if(Model.OnlyStoreSee){ %>
          <meta name="robots" content="NOINDEX,NOFOLLOW" />
    <%}else if(Model.ProductDetailModel.IsActive){
          %>
    <script type="text/javascript" src="/Content/v2/assets/js/lightbox.min.js"></script>
    <meta name="robots" content="INDEX,FOLLOW" />
    <% }%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
 <%if (Model.ProductDetailModel.IsActive || Model.OnlyStoreSee==true)
     { %>
    <%=Html.RenderHtmlPartial("_ProductPageHaderNew",Model.ProductPageHeaderModel) %>
    <%=Html.RenderHtmlPartial("_ProductStoreMessage",Model.ProductStoreMessageModel) %>
<%--    <%=Html.RenderHtmlPartial("_ProductMegaPicture",Model.ProductPictureModels) %>--%>
    <div class="urun-detay">
    <div class="row">
        <div class="col-md-9">
            <div class="row">
                <div class="col-md-5">
                         <div class="thumbnail">
                         <%=Html.RenderHtmlPartial("_ProductPictureNew",Model.ProductPictureModels)%>
                             </div>
                   <ul class="nav nav-pills favoriler">
                  <li class="favori"> <a href="#"> <i class="fa fa-heart-o"></i> Favorilere Ekle</a> </li>
                  <li class="yazdir"> <a href="#"> <i class="fa fa-print"></i> Yazdır</a> </li>
                  <li class="sikayet"> <a  data-toggle="modal" data-target="#ComplaintModal" id="ComplaintProduct" href="#"> <i class="fa fa-flag"></i> Şikayet Et</a> </li>
                </ul>
                <%=Html.RenderHtmlPartial("_ProductSocial") %>
                </div>
                <div class="col-md-7">
                       <%=Html.RenderHtmlPartial("_ProductDetailNew", Model.ProductDetailModel)%>
                </div>
                     <%=Html.RenderHtmlPartial("_ProductTabNew",Model.ProductTabModel) %>
            </div>
<%--        <div class="col-md-5">
       
       <%--     <%=Html.RenderHtmlPartial("_ProductSocial")%>--%>
        </div>
    <div class="col-md-3">
           <%=Html.RenderHtmlPartial("_ProductStoreNew", Model.ProductStoreModel)%>
 <%--           <script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
            <!-- Ürün Detay Sağ Üst -->
            <ins class="adsbygoogle"
                 style="display:inline-block;width:300px;height:250px"
                 data-ad-client="ca-pub-5337199739337318"
                 data-ad-slot="4218995628"></ins>
            <script>
            (adsbygoogle = window.adsbygoogle || []).push({});
            </script>--%>
             <%--   <%=Html.RenderHtmlPartial("_StoreOtherProductNew", Model.StoreOtherProductModel)%>--%>
    </div>
     
     
    </div>
        </div>

   <%=Html.RenderHtmlPartial("_SimilarProductNew",Model.SimilarProductModel) %>

    <hr />

<%--    <%=Html.RenderHtmlPartial("_ProductVideo",Model.ProductTabModel) %>--%>

    <%=Html.RenderHtmlPartial("_ProductComplain",Model.ProductComplainModel) %>
    <%}
      else
        { %>

        <%=Html.RenderHtmlPartial("_ProductPageHader",Model.ProductPageHeaderModel) %>
        <div class="row">
        <div class="col-xs-12">
            <h3>
                <span class="text-warning glyphicon glyphicon-warning-sign"></span>&nbsp; İlan Yayında Değil
            </h3>
            <div class="well">
                Sayfa kaldırılmış veya adres eksik girilmiş olabilir.
                <br>
                Lütfen adresi doğru yazdığınızdan emin olun.
                <br>
                Eğer Sık Kullanılanlar listesinden bu sayfaya yönlendirildiyseniz, <a href="#">Makina
                    Türkiye Anasayfa </a>veya <a href="#">Site Haritası </a>linklerini kullanarak
                ulaşmaya çalışın.
                <br>
                Eğer bunun teknik bir sorun olduğunu düşünüyorsanız, sayfayı <a href="#">Destek Masası
                </a>'na bildirin.
                <br>
            </div>
        </div>
      </div>
    <div class="row">
        <%=Html.RenderHtmlPartial("_StoreOtherProduct", Model.StoreOtherProductModel)%>
<%--        <%=Html.RenderHtmlPartial("_SimilarProduct",Model.SimilarProductModel) %>--%>
      
    </div>
    <%  } %>
</asp:Content>
