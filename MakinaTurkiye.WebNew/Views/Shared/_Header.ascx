﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<% string memberName = string.Empty;
    int memberType = 0;
    int membermainPartyId = 0;
    if (AuthenticationUser.Membership != null)
    {
        membermainPartyId = AuthenticationUser.Membership.MainPartyId;
    }
%>
<%
    MakinaTurkiyeEntities makinaTurkiyeEntities = new MakinaTurkiyeEntities();
    int favoriteProductCount = 0;
    var dataMessage = new NeoSistem.MakinaTurkiye.Data.Message();
    int notReadInboxMessageCount = 0;
    if (AuthenticationUser.Membership != null)
    {
        var memberMainPartyIds = new List<int?>();
        var memberStore = makinaTurkiyeEntities.MemberStores.FirstOrDefault(x => x.MemberMainPartyId == AuthenticationUser.Membership.MainPartyId);
        if (memberStore != null)
        {
            memberMainPartyIds = makinaTurkiyeEntities.MemberStores.Where(x => x.StoreMainPartyId == memberStore.StoreMainPartyId).Select(x => x.MemberMainPartyId).ToList();
        }
        else
        {
            memberMainPartyIds.Add(AuthenticationUser.Membership.MainPartyId);
        }
        var messageCount = makinaTurkiyeEntities.messagechecks.Where(c => memberMainPartyIds.Contains(c.MainPartyId)).ToList().Count;
        var inboxMessageCount = dataMessage.GetItemsByMainPartyIds(string.Join(", ", memberMainPartyIds), (byte)MessageType.Inbox).Rows.Count;
        notReadInboxMessageCount = inboxMessageCount - messageCount;
        var favoriteProducts = makinaTurkiyeEntities.FavoriteProducts.Where(x => x.MainPartyId == AuthenticationUser.Membership.MainPartyId).ToList();
        favoriteProductCount = favoriteProducts.Count;
    }
%>
<div class="overlay-body"></div>
<div class="new-header">
    <div class="container-fluid">
        <div class="new-header__top clearfix">
            <div class="new-header__top-left">
                <a class="site-logo" href="<%:AppSettings.SiteUrlWithoutLastSlash %>">
                    <img src="<%:Url.Content("~/Content/V2/images/makinaturkiye-dark.png") %>"
                        srcset="<%:Url.Content("~/Content/V2/images/makinaturkiye-dark.png") %> 1x, <%:Url.Content("~/Content/V2/images/makinaturkiye-dark.png") %> 2x" alt="makinaturkiye.com" width="226" height="30">
                          <span class="site-slogan">Dünya'nın Makinesi Bir Arada</span>
                    </a>
   
            </div>
            <div class="new-header__top-right">
                <a href="<%:AppSettings.VideoUrlBase %>" class="hidden-md hidden-lg">
                    <i class="mt-icon icon-play-mt"></i>
                </a>
                <a href="https://blog.makinaturkiye.com" class="hidden-md hidden-lg">
                    <i class="mt-icon icon-blog-mt"></i>
                </a>
                <a href="<%:AppSettings.SiteUrl+"yardim" %>" class="hidden-md hidden-lg">
                    <i class="mt-icon icon-answer-mt"></i>
                </a>
                <div class="hidden-xs hidden-sm">
                    <%
                        if (membermainPartyId != 0)
                        {
                            memberName = AuthenticationUser.Membership.MemberName + " " + AuthenticationUser.Membership.MemberSurname;
                            memberType = (byte)AuthenticationUser.Membership.MemberType; %>
                    <div class="user-member-name clearfix">
                        <span><%=memberName %></span>
                        <a href="<%:AppSettings.SiteUrl %>Account/Home">Hesabım</a>
                    </div>
                    <% }  %>


                    <ul class="user-topmenu">

                        <%if (notReadInboxMessageCount > 0)
                            { %>

                        <li class="user-topmenu__item hidden-sm hidden-md hidden-lg">
                            <a href="<%:AppSettings.SiteUrl %>Account/Message/Index?MessagePageType=0" class="user-topmenu__link">
                                <span class="icon-paper-plane"></span>Mesajlar <span class="label label-primary pull-right"><%=notReadInboxMessageCount %></span>
                            </a>
                        </li>

                        <% } %>

                        <li class="user-topmenu__item hidden-sm hidden-md hidden-lg commentLi" style="display: none;">
                            <a href="<%:AppSettings.SiteUrl %>account/ilan/Comments" class="user-topmenu__link">
                                <span class="glyphicon glyphicon-comment"></span>Yorumlar <span class="label label-primary pull-right commentCountvalue"></span>
                            </a>
                        </li>

                        <li class="user-topmenu__item hidden-sm hidden-md hidden-lg" id="MobileCategoriesContainer">
                            <a href="<%:AppSettings.SiteAllCategoryUrl%>" class="user-topmenu__link">
                                <span class="icon-menu"></span>Kategori
                            </a>
                        </li>
                        <li class="user-topmenu__item hidden-sm hidden-md hidden-lg" id="MobileCustomersContainer">
                            <a href="<%:AppSettings.SiteUrl%>sirketler" class="user-topmenu__link">
                                <span class="icon-briefcase"></span>Firma
                            </a>
                        </li>
                        <li class="user-topmenu__item">
                            <a href="<%if (membermainPartyId != 0)
                                {%>
                                    <%:AppSettings.SiteUrl + "Account/Advert/Advert" %>
                                <% }
                                else
                                {%>
                             <%:AppSettings.SiteUrl + "uyelik/kullanicigirisi" %>  
                                <%}%>
                                "
                                class="user-topmenu">
                                <i class="mt-icon icon-advertise-mt"></i>
                                <span class="text">İlan Ver</span>
                            </a>
                        </li>
                        <%if (membermainPartyId != 0)
                            { %>
                        <%if (favoriteProductCount > 0)
                            { %>
                        <li class="user-topmenu__item"><a href="<%:AppSettings.SiteUrl %>Account/Favorite/Product" id="favoriteProductCount" class="user-topmenu hidden-xs">
                            <i class="mt-icon icon-favorite-mt"></i>
                            <span class="text">Favoriler</span>
                            <span class="badge"><%=favoriteProductCount %></span>

                        </a>
                        </li>
                        <%} %>

                        <%if (notReadInboxMessageCount > 0)
                            { %>
                        <li class="user-topmenu__item">

                            <a href="<%:AppSettings.SiteUrl %>Account/Message/Index?MessagePageType=0" class="user-topmenu">
                                <i class="mt-icon icon-message-mt"></i>
                                <span class="text">Mesajlar</span>
                                <span class="badge"><%=notReadInboxMessageCount %></span>
                            </a>
                        </li>

                        <% } %>

                        <%} %>

                        <li class="user-topmenu__item" style="margin-right: 10px">
                            <div class="dropdown ac-dropdown">
                                <a style="cursor: pointer;" class="user-topmenu hidden-xs" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">
                                    <i class="mt-icon icon-user-mt"></i>

                                    <span class="text">
                                        <%if (membermainPartyId == 0)
                                            {%>
                                        Giriş Yap
                                            <% }
                                                else
                                                {%>
                                        <% memberName = AuthenticationUser.CurrentUser.Membership.MemberName + " " + AuthenticationUser.CurrentUser.Membership.MemberSurname;
                                            memberName = memberName.Length > 12 ? memberName.Substring(0, 12) + ".." : memberName;
                                        %>
                                        <%:memberName %>
                                        <%
                                            }%>
                                        <i class="fa fa-caret-down" style="position: absolute"></i></span>


                                </a>
                                <%string baseUrl = AppSettings.SiteUrlWithoutLastSlash; %>
                                <ul class="dropdown-menu account-dropdown" aria-labelledby="dropdownMenu1">
                                    <%if (membermainPartyId == 0)
                                        {%>


                                    <li><a class="bold" href="<%:baseUrl%>/uyelik/kullanicigirisi">Giriş Yap</a>
                                        <a class="bold" href="<%:baseUrl%>/uyelik/hizliuyelik/uyeliktipi-0">Üye Ol</a>

                                    </li>


                                    <li><a href="<%:baseUrl%>/uyelik/kullanicigirisi?ReturnUrl=/Account/Home">Hesabım</a>

                                        <a href="<%:baseUrl%>/uyelik/kullanicigirisi?ReturnUrl=/Account/Personal">Profilim</a>
                                        <a href="<%:baseUrl%>/uyelik/kullanicigirisi?ReturnUrl=/Account/Message/Index?MessagePageType=0">Mesajlarım</a>
                                        <a href="<%:baseUrl%>/uyelik/kullanicigirisi?ReturnUrl=/Account/Favorite/Product">Favorilerim</a>
                                    </li>

                                    <% }
                                        else
                                        {%>
                                    <li><a href="<%:baseUrl%>/Account/Home">Hesabım</a>
                                        <a href="<%:baseUrl%>/Account/Personal">Profilim</a>
                                        <a href="<%:baseUrl%>/Account/Advert/Index?ProductActive=1&DisplayType=2">İlanlarım</a>
                                        <a href="<%:baseUrl%>/Account/Message/Index?MessagePageType=0">Mesajlarım
                                        <%if (notReadInboxMessageCount != 0)
                                            {%>
                                            <span class="badge badge-primary" style="background-color: #ef0d0d"><%:notReadInboxMessageCount %></span>
                                            <% } %>
                                        </a>
                                        <a href="<%:AppSettings.SiteUrl %>Account/Favorite/Product">Favorilerim
                                               <%if (favoriteProductCount != 0)
                                                   {%>
                                            <span class="badge badge-primary"><%:favoriteProductCount %></span>
                                            <% } %>

                                        </a>
                                    </li>

                                    <%if (AuthenticationUser.Membership.MemberType != (byte)MemberType.FastIndividual && AuthenticationUser.Membership.MemberType != (byte)MemberType.Individual)
                                        { %>
                                    <li><a tabindex="-1" href="<%:AppSettings.SiteUrl %>Account/Personal">Firma Ayarları </a></li>
                                    <%}
                                        else
                                        {%>

                                    <%}%>

                                    <li role="presentation"><a role="menuitem"  tabindex="-1" href="<%:AppSettings.SiteUrl %>Uyelik/OturumuKapat">Çıkış Yap </a></li>
                                    <% } %>
                                </ul>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            <div class="new-header__top-center">
                <div class="row">
                    <div class="new-header__search">
                        <%string searchLink = AppSettings.SiteUrl + "kelime-arama?CategoryId=0";
                        %>
                        <form class="navbar-form" action="<%:searchLink %>" method="get" role="search">
                            <%string searchText = "";
                                if (Request.QueryString["SearchText"] != null)
                                {
                                    searchText = Request.QueryString["SearchText"].ToString();
                                }%>
                                <button type="submit">
                                <img src="/Content/v2/images/icon/search-icon.png" alt="Ara">
                            </button>
                            <input type="text" id="SearchText" value="<%:searchText %>" name="SearchText" autocomplete="off" class="search-text-autocomplate" placeholder="Ürün, Kategori, Firma veya Video Ara" required x-webkit-speech speech>
                         <div class="input-group-append">
                                <a href="<%:AppSettings.SiteUrlWithoutLastSlash+"/detayli-arama" %>"  class="btn site-search__btn" ">
                                    <span class="icon">
                                        <i class="fa fa-angle-down"></i>
                                    </span>
                                </a>
                            </div>
                        </form>
                    </div>
                </div>
                <div class="row hidden-xs hidden-sm">
                    <div class="site-search__bottom">
                        <ul class="site-search__store">
                            <li <%--class="dropdown  ac-dropdown"--%>>
                                <a href="<%:AppSettings.StoreAllUrl %>" <%--class="dropdown-toggle" data-toggle="dropdown"--%>>Tüm Mağazalar<%--<i class="fa fa-caret-down" style="float: right; padding: 3px;"></i>--%></a>
                                <div class="dropdown-menu ">
                                    <h4 class="search-store_title">Tüm Mağazalar</h4>
                                    <div class="scrollable" id="StoreCategories">
                                    </div>
                                </div>
                            </li>
                            <%--             <li>
                                <a href="<%:AppSettings.SiteUrlWithoutLastSlash %>/alim-talebi">Alım Talebi</a>
                            </li>--%>
                        </ul>
                        <ul class="site-search__links">
                            <li>
                                <a target="_blank" href="<%:AppSettings.VideoUrlBase %>">Videolar</a>
                            </li>
                            <li>
                                <a target="_blank" href="https://blog.makinaturkiye.com">Blog</a>
                            </li>
                            <li>
                                <a target="_blank" href="<%:AppSettings.SiteUrlWithoutLastSlash %>/yardim">Yardım</a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>

        </div>

        <div class="new-header__bottom clearfix hidden-md hidden-lg">
            <div class="new-header__bottom-left hidden-md">
                <a href="javascript:void(0)" class="navbar-toggle header__mobile-menu " type="button" data-toggle="collapse" data-target=".js-navbar-collapse">
                    <span class="header__mobile-menu-icon icon-menu"></span>
                </a>
            </div>

            <div class="new-header__bottom-left hidden-md">
                <div class="header-button header-category-wrapper">
                    <div class="header-category" id="SectorsCategories">
                        <%--        <%:Html.Action("_HeaderMainMenu","Home") %>--%>
                    </div>
                </div>

                <div class="header-button header-sector-wrapper">
                    <div class="header-category" id="">
                        <%--        <%:Html.Action("_HeaderStoreCategoryGeneral","Store") %>--%>
                    </div>
                </div>

            </div>
            <div class="new-header__bottom-center">
                <%--        <%:Html.Action("_HeaderTopMenu","Header") %>--%>
            </div>
            <div class="new-header__bottom-right hidden-md hidden-lg">
                <ul class="nav navbar-nav navbar-right" role="menu">
                    <% 
                        if (AuthenticationUser.Membership != null)
                        {
                            membermainPartyId = AuthenticationUser.Membership.MainPartyId;
                        }

                        if (membermainPartyId != 0)
                        {
                            memberName = AuthenticationUser.Membership.MemberName + " " + AuthenticationUser.Membership.MemberSurname;
                            memberType = (byte)AuthenticationUser.Membership.MemberType;
                            memberName = memberName.Length > 12 ? memberName.Substring(0, 12) + ".." : memberName;
                    %>

                    <li><a href="<%:AppSettings.SiteUrl %>Account/Home" class="plr5"><span class="text-md  glyphicon glyphicon-user"></span>
                        <%=memberName %>
                    </a>


                    </li>
                    <li class="dropdown"><a href="#" id="A2" class="plr5" data-toggle="dropdown"><span
                        class="caret"></span>
                        <div class="visible-xs">
                            <span class="caret"></span>&nbsp;Ayarlar
                        </div>
                    </a>
                        <ul class="dropdown-menu dropdown-menu-mt" role="menu" aria-labelledby="dropdownMenu1">
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="<%:AppSettings.SiteUrl %>Account/Home"><i
                                class="fa fa-angle-right"></i>&nbsp; Hesabım </a></li>
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="<%:AppSettings.SiteUrl %>Account/Personal"><i
                                class="fa fa-angle-right"></i>&nbsp; Profilim </a></li>
                            <%if (AuthenticationUser.Membership.MemberType != (byte)MemberType.FastIndividual && AuthenticationUser.Membership.MemberType != (byte)MemberType.Individual)
                                { %>
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="<%:AppSettings.SiteUrl %>Account/Advert/Index?ProductActive=1&DisplayType=2"><i class="fa fa-angle-right"></i>&nbsp; İlanlarım </a></li>
                            <%}
                                else
                                {%>

                            <%}%>

                            <li role="presentation"><a role="menuitem" tabindex="-1" href="<%:AppSettings.SiteUrl %>Account/Message/Index?MessagePageType=0"><i class="fa fa-angle-right"></i>&nbsp; Mesajlarım </a></li>


                            <li role="presentation"><a role="menuitem" tabindex="-1" href="<%:AppSettings.SiteUrl %>Account/Favorite/Product"><i class="fa fa-angle-right"></i>&nbsp; Favorilerim </a></li>

                            <%if (AuthenticationUser.Membership.MemberType != (byte)MemberType.FastIndividual && AuthenticationUser.Membership.MemberType != (byte)MemberType.Individual)
                                { %>
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="<%:AppSettings.SiteUrl %>Account/Personal"><i class="fa fa-angle-right"></i>&nbsp; Firma Ayarları </a></li>
                            <%}
                                else
                                {%>

                            <%}%>

                            <li role="presentation" class="divider"></li>
                            <li role="presentation"><a role="menuitem" tabindex="-1" href="<%:AppSettings.SiteUrl %>Uyelik/OturumuKapat">
                                <span class="glyphicon glyphicon-log-out" style="padding-right: 5px;"></span>Çıkış </a></li>
                        </ul>
                    </li>

                    <%if (notReadInboxMessageCount > 0)
                        { %>
                    <li><a href="<%:AppSettings.SiteUrl %>Account/Message/Index?MessagePageType=0" class="plr5 tooltips tooltip-mt badge-link" data-toggle="tooltip" data-placement="bottom"
                        title="<%=notReadInboxMessageCount %> Okunmamış mesajınız var.">
                        <span class="badge-link__count"><%=notReadInboxMessageCount %></span>
                        <span class="text-md glyphicon glyphicon-envelope"></span>
                    </a></li>

                    <% } %>
                    <%if (favoriteProductCount > 0)
                        { %>
                    <li><a href="<%:AppSettings.SiteUrl %>Account/Favorite/Product" id="favoriteProductCount" class="plr5 tooltips tooltip-mt badge-link" data-toggle="tooltip" data-placement="bottom"
                        title="<%=favoriteProductCount %> favorilere eklenmiş ürününüz var">
                        <span class="badge-link__count"><%=favoriteProductCount %></span>
                        <span class="text-md  glyphicon glyphicon-heart"></span>
                    </a>
                    </li>
                    <%} %>
                    <li>
                        <a title="Çıkış Yap" href="<%:AppSettings.SiteUrl %>Uyelik/OturumuKapat"><span style="font-size: 20px;" class="glyphicon glyphicon-log-out" style="padding-right: 5px;"></span></a>
                    </li>

                    <% }
                        else
                        { %>
                    <li><a class="btn-login" href="<%:AppSettings.SiteUrl %>uyelik/kullanicigirisi">Giriş Yap</a> </li>
                    <% } %>
                    <li><a href="<%:AppSettings.SiteUrl %>Account/ilan/Comments" id="" style="display: none;" class="hidden-xs plr5 tooltips tooltip-mt badge-link commentLi" data-toggle="tooltip" data-placement="bottom"
                        title="İncelenmemiş yorum bulunmaktadır.">
                        <span class="badge-link__count commentCountvalue"></span>
                        <span class="text-md glyphicon glyphicon-comment"></span>
                    </a></li>
                </ul>
            </div>
        </div>
    </div>
</div>

<%:Html.Action("_HeaderBaseMenu", "Home") %>


<script>
    var utils = {
        escapeRegExChars: function (value) {
            return value.replace(/[|\\{}()[\]^$+*?.]/g, "\\$&");
        }
    };
    var Category = "Oneri";
    $('#SearchText').Autocomplete({
        serviceUrl: '<%=AppSettings.SiteUrl + Url.Action("Index", "Search")%>',
       showNoSuggestionNotice: true,
       noSuggestionNotice: '',
       minChars: 2,
       noCache: true,
       groupby: 'category',
       maxHeight: 600,
       appendTo: $('#SearchText').parentNode,
       autoSelectFirst: false,
       formatGroup: function (category) {
           return "<span class='disabled suggestion-wrapper'><span class='suggestion-value'>" + category + "</span></span>";
       },
       onSearchComplete: function (query, suggestions) {
           Category = "Oneri";
       },
       formatResult: function (suggestion, currentValue) {


           $(".overlay-body").show();
           $(".new-header__search").css("z-index", "5000");
           var pattern, words;
           if (!currentValue && suggestion.data.category !== "Gecmis") {
               return suggestion.value;
           }
           words = utils.escapeRegExChars(currentValue);
           words = words.split(' ').join('|');
           pattern = '(' + words + ')';

           if (suggestion.data.category !== Category && suggestion.data.category !== "Gecmis") {
               Category = suggestion.data.category;
               return "<span class='suggestion-wrapper'><span class='suggestion-group'><span>" + suggestion.data.category + "<span></span></span>";
           }
           else {

           }

           if (suggestion.data.category == "Gecmis" && suggestion.Url == "#") {
               return "<span class='suggestion-wrapper'><span class='suggestion-group'><span>" + suggestion.Name + "</span></span></span>";
           }
           if (suggestion.Name.length > 85) {
               return "<span class='suggestion-wrapper' ><span class='suggestion-value' style='direction:rtl'>" + suggestion.Name.replace(new RegExp(pattern, 'gi'), '<strong>$1<\/strong>')
                   .replace(/&/g, '&amp;')
                   .replace(/</g, '&lt;')
                   .replace(/>/g, '&gt;')
                   .replace(/"/g, '&quot;')
                   .replace(/&lt;(\/?strong)&gt;/g, '<$1>') + "</span></span>";
           }
           else {
               return "<span class='suggestion-wrapper' ><span class='suggestion-value'>" + suggestion.Name.replace(new RegExp(pattern, 'gi'), '<strong>$1<\/strong>')
                   .replace(/&/g, '&amp;')
                   .replace(/</g, '&lt;')
                   .replace(/>/g, '&gt;')
                   .replace(/"/g, '&quot;')
                   .replace(/&lt;(\/?strong)&gt;/g, '<$1>') + "</span></span>";
           }

       },
       onSelect: function (suggestion) {
           console.log(suggestion);

           if (suggestion.data.category == "Oneri" || suggestion.data.category == "Gecmis") {
               $('#SearchText').val(suggestion.Name);
               this.form.submit();
           }
           if (suggestion.data.category == "Markalar") {
               if (suggestion.url != "#") {
                   window.location = suggestion.Url;
               }
           }
           if (suggestion.data.category == "Ürün Kategorileri") {
               if (suggestion.url != "#") {
                   window.location = suggestion.Url;
               }
           }
           else if (suggestion.data.category == "Firma Kategorileri") {
               if (suggestion.url != "#") {
                   window.location = suggestion.Url;
               }
           }
           else if (suggestion.data.category == "Videolar") {
               if (suggestion.url != "#") {
                   window.location = suggestion.Url;

               }
           }
           else if (suggestion.data.category == "Tedarikçiler") {
               if (suggestion.url != "#") {
                   window.location = suggestion.Url;
               }
           }
       }
   });
    $(document).ready(function () {
        $("input[id='SearchText']").click(function () {
            $(this).val('');
        });
        $("#SearchText").focus(function () {
            $(this).val('');
        });
        $("#SearchText").focusout(function(){
    $(".overlay-body").hide();
});
});



</script>
<script type="text/javascript">
    $(document).ready(function () {
        $.ajax({
            url: '<%:AppSettings.SiteUrl+"/Home/GetStoreProductComment"%>',
            data: {},
            contentType: "application/json",
            type: 'post',
            success: function (data) {
                if (data != 0) {
                    $(".commentLi").show();
                    $(".commentCountvalue").html(data);

                }
            }
        }
        );
    });

</script>
