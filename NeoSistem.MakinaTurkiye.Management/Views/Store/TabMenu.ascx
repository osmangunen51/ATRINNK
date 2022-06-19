<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%string tab = "tabLi ui-state-default ui-corner-top divTab";
    string tabActive = "tabLi ui-state-default ui-corner-top ui-tabs-selected ui-state-active divTabActive";
%>
<link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
<script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
<script type="text/javascript">
    $(function () {
        $.superbox.settings = {
            closeTxt: "Kapat",
            loadTxt: "Yükleniyor...",
            nextTxt: "Sonraki",
            prevTxt: "Önceki"
        };
        $.superbox();
    });


    function SpecialMailSend(id) {
        if (confirm('Mail Göndermek İstiyormusunuz?')) {
            $('#preLoading').show();
            var storeId =<%:this.Page.RouteData.Values["id"]%>;
            $.ajax({
                url: '/Member/SpecialMailSend',
                data: {
                    constandId: id, storeid: storeId
                },
                type: 'post',
                success: function (data) {
                    alert(data.Message);
                    $('#preLoading').hide();
                },
                error: function (x, a, r) {
                    alert("Error");
                    $('#preLoading').hide();

                }
            });
        }
    }
</script>
<style type="text/css">
    /* Custom Theme */
    #superbox-overlay { background: #e0e4cc; }
    #superbox-container .loading { width: 32px; height: 32px; margin: 0 auto; text-indent: -9999px; background: url(styles/loader.gif) no-repeat 0 0; }
    #superbox .close a { float: right; padding: 0 5px; line-height: 20px; background: #333; cursor: pointer; }
        #superbox .close a span { color: #fff; }
    #superbox .nextprev a { float: left; margin-right: 5px; padding: 0 5px; line-height: 20px; background: #333; cursor: pointer; color: #fff; }
    #superbox .nextprev .disabled { background: #ccc; cursor: default; }
    .anotherback { background: #617cca4a;; border: 1px solid #7fbf99; }
    .ui-state-active { background: #fff; }
    .mailback { background: #b6c78e4a; border: 1px solid #7fbf99; }
        .mailback a { color: #333 !important; }
</style>
<div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
    id="preLoading">
    <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
    <strong>Mail Gönderiliyor.</strong> Lütfen bekleyiniz...
</div>
<div style="float: left; width: 100%; height: auto; float: left;">
    <%MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
        int id = this.Page.RouteData.Values["id"].ToInt32();
        var mainparty = entities.MemberStores.Where(c => c.StoreMainPartyId == id);
        int mainpartyid = mainparty.FirstOrDefault().MemberMainPartyId.ToInt32();
        var member = entities.Members.Where(c => c.MainPartyId == mainpartyid).SingleOrDefault();
        var storeWeb = entities.Stores.FirstOrDefault(x => x.MainPartyId == id).StoreUrlName;

        var constants = entities.Constants.Where(c => c.ConstantType == (byte)ConstantType.UserSpecialMailType);

    %>
    <div style="float: left;">
        <div class="<%: ViewData["Store"] != null ? tabActive:tab %> anotherback" onclick="window.location.href='/Store/EditStore/<%:this.Page.RouteData.Values["id"] %>'">
            <a class="tabStore" href="/Store/EditStore/<%:this.Page.RouteData.Values["id"] %>">Mağaza Düzenle</a>
        </div>

        <div class="<%: ViewData["Member"] != null ? tabActive:tab %> anotherback" onclick="window.location='/Store/EditMember/<%:this.Page.RouteData.Values["id"] %>'">
            <a class="tabStore" href="/Store/EditMember/<%:this.Page.RouteData.Values["id"] %>">Üyelik Bilgileri</a>
        </div>


        <%if (mainparty.Any(x => x.MemberStoreType == (byte)MemberStoreType.Helper))
            {%>
        <div class="<%: ViewData["Users"] != null ? tabActive:tab %> anotherback" onclick="window.location='/Store/Users/<%:this.Page.RouteData.Values["id"] %>'">
            <a class="tabStore" href="/Store/Users/<%:this.Page.RouteData.Values["id"] %>">Kullanıcılar</a>
        </div>
        <% } %>
        <div class="<%: ViewData["Communication"] != null ? tabActive:tab %> anotherback" onclick="window.location='/Store/EditCommunication/<%:this.Page.RouteData.Values["id"] %>'">
            <a class="tabStore" href="/Store/EditCommunication/<%:this.Page.RouteData.Values["id"] %>">İletişim Bilgileri</a>
        </div>
        <div class="<%: ViewData["ActivityType"] != null ? tabActive:tab %> anotherback" onclick="window.location='/Store/EditActivityType/<%:this.Page.RouteData.Values["id"] %>'">
            <a class="tabStore" href="/Store/EditActivityType/<%:this.Page.RouteData.Values["id"] %>">Faaliyet Tipleri</a>
        </div>
        <div class="<%: ViewData["RelActivityCategory"] != null ? tabActive:tab %> anotherback" onclick="window.location='/Store/EditActivityCategory/<%:this.Page.RouteData.Values["id"] %>'">
            <a class="tabStore" href="/Store/EditActivityCategory/<%:this.Page.RouteData.Values["id"] %>">Faaliyet Alanları</a>
        </div>
        <div class="<%: ViewData["AboutUs"] != null ? tabActive:tab %> anotherback" onclick="window.location='/Store/EditAbout/<%:this.Page.RouteData.Values["id"] %>'">
            <a class="tabStore" href="/Store/EditAbout/<%:this.Page.RouteData.Values["id"] %>">Hakkımızda</a>
        </div>
        <div class="<%: ViewData["StoreProfileHomeDescription"] != null ? tabActive:tab %> anotherback" onclick="window.location='/Store/EditAbout/<%:this.Page.RouteData.Values["id"] %>'">
            <a class="tabStore" href="/Store/StoreProfileHomeDescription/<%:this.Page.RouteData.Values["id"] %>">Firma Profil Açıklama</a>
        </div>

        <div class="<%: ViewData["Branch"] != null ? tabActive:tab %> anotherback" onclick="window.location='/Store/EditDealers/<%:this.Page.RouteData.Values["id"]+"?DealerType="+(byte)DealerType.Sube %>'">
            <a class="tabStore" href="/Store/EditDealers/<%:this.Page.RouteData.Values["id"]+"?DealerType="+(byte)DealerType.Sube %>">Şubelerimiz</a>
        </div>
        <div class="<%: ViewData["Service"] != null ? tabActive:tab %> anotherback" onclick="window.location='/Store/EditDealers/<%:this.Page.RouteData.Values["id"]+"?DealerType="+(byte)DealerType.YetkiliServis %>'">
            <a class="tabStore" href="/Store/EditDealers/<%:this.Page.RouteData.Values["id"]+"?DealerType="+(byte)DealerType.YetkiliServis %>">Servis Ağımız</a>
        </div>
        <div class="<%: ViewData["Dealer"] != null ? tabActive:tab %> anotherback" onclick="window.location='/Store/EditDealers/<%:this.Page.RouteData.Values["id"]+"?DealerType="+(byte)DealerType.Bayii %>'">
            <a class="tabStore" href="/Store/EditDealers/<%:this.Page.RouteData.Values["id"]+"?DealerType="+(byte)DealerType.Bayii %>">Bayi Ağımız</a>
        </div>
        <div class="<%: ViewData["Dealership"] != null ? tabActive:tab %> anotherback" onclick="window.location='/Store/EditDealership/<%:this.Page.RouteData.Values["id"]%>'">
            <a class="tabStore" href="/Store/EditDealership/<%:this.Page.RouteData.Values["id"]%>">Bayilikler</a>
        </div>
        <div class="<%: ViewData["Brand"] != null ? tabActive:tab %> anotherback" onclick="window.location='/Store/EditBrand/<%:this.Page.RouteData.Values["id"]%>'">
            <a class="tabStore" href="/Store/EditBrand/<%:this.Page.RouteData.Values["id"]%>">Markalarımız</a>
        </div>
        <div class="<%: ViewData["StoreImages"] != null ? tabActive:tab %> anotherback" onclick="window.location='/Store/EditStoreImages/<%:this.Page.RouteData.Values["id"]%>'">
            <a class="tabStore" href="/Store/EditStoreImages/<%:this.Page.RouteData.Values["id"]%>">Şirket Görselleri</a>
        </div>
        <div class="<%: ViewData["ProfilePicture"] != null ? tabActive:tab %> anotherback" onclick="window.location='/Store/EditProfilePicture/<%:this.Page.RouteData.Values["id"]%>'">
            <a class="tabStore" href="/Store/EditProfilePicture/<%:this.Page.RouteData.Values["id"]%>">Profil Görseli</a>
        </div>

        <div style="background-color:#7fbf99" class="<%:tab %> anotherback" data-a="BrowseDesc1">
            <a  class="tabStore" data-a="BrowseDesc1" target="_blank" href="/Member/BrowseDesc1/<%=mainpartyid%>">Açıklama</a>
        </div>
        <div class="<%:tab %> anotherback">
            <% NeoSistem.MakinaTurkiye.Management.Controllers.LinkHelper lHelper = new NeoSistem.MakinaTurkiye.Management.Controllers.LinkHelper();
                int storeId = Convert.ToInt32(this.Page.RouteData.Values["id"]);
                var memberstore = entities.MemberStores.FirstOrDefault(x => x.StoreMainPartyId == storeId);

                var link = lHelper.Encrypt(memberstore.MemberMainPartyId.ToString());
            %>
            <a class="tabStore" target="_blank" href="https://www.makinaturkiye.com/membership/LogonAuto?validateId=<%: link%>&returnUrl=/account/advert/advert">Hesabım</a>
        </div>
        <div class="<%:tab %> anotherback" data-a="BrowseDesc1">
            <a class="tabStore" target="_blank" href="http://www.makinaturkiye.com/<%:storeWeb %>">Firma Web</a>
        </div>
        <div class="<%:tab %> anotherback">
            <a class="tabStore" target="_blank" href="/Product/Index?StoreMainPartyId=<%:this.Page.RouteData.Values["id"] %>">Ürünler</a>
        </div>
        <div class="<%:tab %> mailback">

            <a class="tabStore" target="_blank" href="mailto:<%:member.MemberEmail%>">Mail Gönder</a>
        </div>

        <%foreach (var item in constants)
            {%>
        <div class="tabLi ui-state-default ui-corner-top divTab mailback">
            <a href="javascript:void(0)" onclick="SpecialMailSend(<%:item.ConstantId %>)"><%:item.ConstantName %></a>
        </div>
        <%} %>
        <div style="background-color:#4cb4e5" class="<%:tab %> mailback">
            <a href="/Member/storemail/<%=mainpartyid%>" id="lightbox_click" rel="superbox[iframe][1024x600]">Mailing </a>
        </div>
                <div class="<%: ViewData["StoreSector"] != null ? tabActive:tab %> anotherback">
            <a href="/Store/StoreSector/<%:this.Page.RouteData.Values["id"] %>">İlgili Sektör</a>
        </div>
        <div class="<%: ViewData["StoreInformation"] != null ? tabActive:tab %> anotherback">
            <a href="/Store/StoreDetailInformation/<%:this.Page.RouteData.Values["id"] %>">Mağaza Bilgileri </a>
        </div>
                <div class="<%: ViewData["BuyPacket"] != null ? tabActive:tab %> anotherback">
            <a href="/Store/BuyPacket/<%:this.Page.RouteData.Values["id"] %>">Paket Satın Al </a>
        </div>
    </div>
</div>
