﻿<%@ Page Title="" Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.Catalog.ProductContactModel>" %>

<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title><%:Model.StoreModel.StoreName %> ile <%:Model.ProductName %> ürünü için İletişim Kur</title> 
    <link rel="shortcut icon" href="/Content/V2/images/favicon.png?v=2">
    <!-- CSS -->
    <meta name="description" content="<%:Model.StoreModel.StoreName %> ile <%:Model.ProductName %> ürünü hakkında iletişim kurabileceğiniz sayfadır" />
        <meta name="google-site-verification" content="jpeiLIXc-vAKBB2vjRZg3PluGG3hsty0n6vSXUr_C-A" />
    <link rel="stylesheet" href="/Content/V2/public/build/css/bundle-main.css?v=1" /> 
    <script src="/Content/V2/assets/js/jquery.min.js?v=1"></script>
    <script src="/Content/V2/public/build/js/bundle-plugins-min.js?v=1" defer></script>
    <script src="/Content/V2/public/build/js/bundle-main-min.js?v=1" defer></script>
    <script type="text/javascript" src="/Content/v2/assets/js/phonemask.js"></script>
    <style type="text/css">
        a{
            text-decoration:none;
            color:#333;
        }
    </style>
    <script type="text/javascript">
        function AddWhatsappLog(id)
        {
            $.ajax({
                url: '<%:AppSettings.SiteAllCategoryUrl%>/ajax/AddWhatsappLog',
                data: { storeId: id },
                type: 'post',
                success: function (data) {
                        
                    
                    }
                 
                }
            );
        }
    </script>
        <script>(function (w, d, s, l, i) {
    w[l] = w[l] || []; w[l].push({
        'gtm.start':
        new Date().getTime(), event: 'gtm.js'
    }); var f = d.getElementsByTagName(s)[0],
    j = d.createElement(s), dl = l != 'dataLayer' ? '&l=' + l : ''; j.async = true; j.src =
    'https://www.googletagmanager.com/gtm.js?id=' + i + dl; f.parentNode.insertBefore(j, f);
})(window, document, 'script', 'dataLayer', 'GTM-TR7HZJT');</script>
    <script type="text/javascript">
        $(function () {

            $("#PhoneNumber").mask("(999) 999-9999");
            $("#PhoneNumberAgain").mask("(999) 999-9999");


            $("#phone").on("blur", function () {
                var last = $(this).val().substr($(this).val().indexOf("-") + 1);

                if (last.length == 5) {
                    var move = $(this).val().substr($(this).val().indexOf("-") + 1, 1);

                    var lastfour = last.substr(1, 4);

                    var first = $(this).val().substr(0, 9);

                    $(this).val(first + move + '-' + lastfour);
                }
            });
        });

</script>
</head>
<body> 
        <noscript>
        <iframe src="https://www.googletagmanager.com/ns.html?id=GTM-TR7HZJT"
            height="0" width="0" style="display: none; visibility: hidden"></iframe>
    </noscript>
    <%--Buradaki hidden degerleri mesaj popupu icin kullaniliyor--%>
    <input type="hidden" id="hiddenMemberNo" value="<%:Model.StoreModel.MemberNo %>"/>
    
     <input type="hidden" id="hiddenProductNo" value="<%:Model.StoreModel.ProductNo %>"/>
    <input type="hidden" id="hdnProductId" value="<%:Model.ProductId %>" />
<input type="hidden" id="hdnMainPartyId" value="<%:Model.MemberMainPartyId %>" />
<input type="hidden" id="hdnMemberEmail" value="<%:Model.MemberEmail %>" />

    <!-- Ürün İletişim Logo -->
    <div class="container" style="margin-top: 20px; margin-bottom: 5px">

        <div class="row">
            <div class="col-md-8 col-md-offset-2">
                <div class="urun-iletisim-header">
                    <div class="urun-iletisim-makina-logo">
                        <a href="<%:Url.Action("index","home") %>">
                            <img src="/Content/V2/images/logo_nomt.png" alt="Makina Türkiye Logo"></a>
                    </div>
                    <div class="urun-iletisim-logo-yani-text">
                        <span><b>Satıcıyla İletişim Kur</b></span>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <!-- Ürün İletişim Logo -->


    <div class="container">
        <div class="row">
            <div class="col-md-8 col-md-offset-2">

                <div class="urun-iletisim-block">

                    <!-- Heading -->
                    <div class="urun-iletisim-heading">
                        <!-- Logo -->
                        <div class="urun-iletisim-firma-logo">
                            <a href="<%:Model.StoreUrl %>">
                            <img src="<%=Model.StoreModel.StoreLogoPath %>"" width="99" height="66">
                             </a>
                        </div>
                        <!-- Logo -->
                        <!-- Firma Adı -->
                        <div class="urun-iletisim-firma-adi">

                            <h2><b><a href="<%:Model.StoreUrl %>"><%=Model.StoreModel.StoreName %></a></b></h2>
                            <!-- Yetkili Adı -->
                            <div class="urun-iletisim-yetkili-adi">
                                <span>Yetkili: <%=Model.StoreModel.MemberName + " " + Model.StoreModel.MemberSurname%> </span>
                            </div>
                            <!-- Yetkili Adı -->
                        </div>
                        <!-- Firma Adı -->

                    </div>
                    <!-- Heading -->

                    <!-- Body -->
                    <div class="urun-iletisim-body">

                        <div class="urun-iletisim-body-firma-bilgileri">

                            <div class="urun-iletisim-urun-detay">
                                <div class="urun-iletisim-urun-detay-photo">
                                    <a href="<%:Model.ProductUrl %>">
                                    <img src="<%=Model.ProductPictureUrl %>" width="180" height="135" class="img-responsive">
                                        </a>
                                </div>
                                <div class="urun-iletisim-urun-detay-urun-adi">
                                    <span><b><a href="<%:Model.ProductUrl %>"><%=Model.ProductName %></a></b></span>
                                </div>
                                <div class="urun-iletisim-urun-detay-ilan-no">
                                    <span>İlan No : <%=Model.ProductNo %></span>
                                </div>
                                        <%if(AuthenticationUser.Membership.MainPartyId>0){ %>
                                        <div>
                                             
                                    <a href="/Account/Message/Index?MessagePageType=1&UyeNo=<%:Model.MemberMainPartyId %> &UrunNo=<%:Model.ProductNo.Replace("#","") %>" class="btn btn-sm btn-primary product-detail-description__button-send-message"><span class="glyphicon glyphicon-envelope"></span>&nbsp;Satıcıya Mesaj Gönder Veya Soru Sor </a>
                                        </div>
                                        <%} %>
                            </div>


                            <div class="urun-iletisim-body-firma-iletisim-bilgileri">
                                <div class="urun-iletisim-body-firma-iletisim-bilgileri-ullist">
                                    <%foreach (var phone in Model.StoreModel.PhoneModels)
                                        {%>
                                    <div class="urun-iletisim-firma-tel-box" style="border-top: none; border-left: none;">
                                        <div class="urun-iletisim-firma-tel-box-icon">
                                             
                                           <%if (phone.PhoneType == PhoneType.Fax)
                                               {%> 
                                             <i class="glyphicon glyphicon-inbox text-success"></i>
                                           <%  }
                                               else if (phone.PhoneType == PhoneType.Whatsapp)
                                               {%>
                                               <img src="https://www.makinaturkiye.com/Content/SocialIcon/wp-24.png">
                                               <% }
                                                   else if (phone.PhoneType == PhoneType.Gsm)//gsm ve digerleri icin
                                                   { %>
                                                <i class="fa fa-phone text-success"></i>
                                                <%}
                                                    else
                                                    {%> 
                                                <i class="glyphicon glyphicon-phone-alt text-success"></i>
                                               <% }%>  
                                        </div>
                                        <div class="urun-iletisim-firma-tel-box-telefon-text">
                                            <%if (phone.PhoneType == PhoneType.Whatsapp)
                                                {
                                                    string whatsappPhone = phone.GetFullText(false).Replace("+", "");
                                                       
                                                    string whatsappURL = "https://api.whatsapp.com/send?phone=" + whatsappPhone + "&text=Merhaba makinaturkiye.com '"+Model.ProductName+"' ilanı hakkında bilgi almak istiyorum.  İlan Adresi:" +Model.ProductUrl;

                                                    %>
                                             <a onclick="AddWhatsappLog(<%:Model.StoreModel.MainPartyId %>)" href="<%=whatsappURL %>">Whatsappla İletişim Kur </a>
                                              <%  }
                                                  else
                                                  {%>
                                                    <a href="tel:<%=phone.GetFullText(false)%>"><%=phone.GetFullText() %></a>
                                               <% }
                                                 %>
                                        </div>
                            
                                    </div>

                                    <%  } %>
                            
                                </div>

                            </div>
            

                        </div>
             <%if(AuthenticationUser.Membership.MainPartyId==0){ %>
                        <%=Html.RenderHtmlPartial("_ProductStoreMessageNew",Model.StoreMessage) %>
                        <%} %>         
                    <!-- Footer -->

                </div>

            </div>
        </div>
    </div> 
        </div>
     
</body>
</html>
