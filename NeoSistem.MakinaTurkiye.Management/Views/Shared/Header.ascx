<%@ Control Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewUserControl" %>
<script type="text/javascript">
    function checkMenu() {
        $('.menus').css('display', 'none');
    }
</script>
<script type="text/javascript">
    function homePage(prefix) {
        return {
            text: '',
            type: Ribbon.GroupType.Group,
            buttons: [
                {
                    Id: prefix + 'HomePage', text: 'Anasayfa', type: Ribbon.ButtonType.BigItem, image: 'BlogHomePage.png',
                    action: "window.location = '/';"
                }]
        };
    }
    function cliboard(prefix) {
        return {
            text: 'Pano',
            type: Ribbon.GroupType.Group,
            width: '115px',
            buttons: [
                {
                    Id: prefix + 'Paste', text: 'Yapıştır', type: Ribbon.ButtonType.BigItem, image: 'Clipboard.png',
                    action: ''
                },
                {
                    Id: prefix + 'Cut', text: 'Kes', type: Ribbon.ButtonType.SmallItem, image: 'Cut.png', align: 'left',
                    action: ''
                },
                {
                    Id: prefix + 'Copy', text: 'Kopyala', type: Ribbon.ButtonType.SmallItem, image: 'Copy.png', align: 'left',
                    action: ''
                }]
        };
    }

</script>
<div id="cs">
    <ul class="cstabs" unselectable="on">
        <li><a id="selected" unselectable="on" ondblclick="minimizeRibbon('tab-content');"
            class="tabMainHome" onclick="ribbonTabSlide('tabMainHome');" style="color: #FFF; background: url(/Content/RibbonImages/FileMenu_Background_blue.PNG) repeat-x;"
            group="tabs">Anasayfa</a>
        </li>
        <li><a unselectable="on" class="tabMember" ondblclick="minimizeRibbon('tab-content');"
            onclick="ribbonTabSlide('tabMember');" group="tabs">Üyeler</a> </li>
        <li><a unselectable="on" class="tabStore" ondblclick="minimizeRibbon('tab-content');"
            onclick="ribbonTabSlide('tabStore');" group="tabs">Firmalar</a> </li>
        <li><a unselectable="on" class="tabAdvert" ondblclick="minimizeRibbon('tab-content');"
            onclick="ribbonTabSlide('tabAdvert');" group="tabs">İlanlar</a> </li>


        <li><a unselectable="on" class="tabCategory" ondblclick="minimizeRibbon('tab-content');"
            onclick="ribbonTabSlide('tabCategory');" group="tabs">Kategori Yönetimi</a></li>
        <li><a unselectable="on" class="tabOrder" ondblclick="minimizeRibbon('tab-content');"
            onclick="ribbonTabSlide('tabOrder');" group="tabs">Sipariş</a></li>
        <li><a unselectable="on" class="tabConstant" ondblclick="minimizeRibbon('tab-content');"
            onclick="ribbonTabSlide('tabConstant');" group="tabs">Sabitler</a> </li>
        <li><a unselectable="on" class="tabReport" ondblclick="minimizeRibbon('tab-content');"
            onclick="ribbonTabSlide('tabReport');" group="tabs">Rapor</a></li>
        <li><a unselectable="on" class="tabSeo" ondblclick="minimizeRibbon('tab-content');"
            onclick="ribbonTabSlide('tabSeo');" group="tabs">Seo</a></li>
        <li><a unselectable="on" class="tabUser" ondblclick="minimizeRibbon('tab-content');"
            onclick="ribbonTabSlide('tabUser');" group="tabs">Kullanıcılar</a> </li>
        <li><a unselectable="on" class="tabSales" ondblclick="minimizeRibbon('tab-content');"
            onclick="ribbonTabSlide('tabSales');" group="tabs">Pörtfoy</a></li>


    </ul>
    <div id="newNot" style="background-color: #2db007; display: none; color: #fff; padding: 5px; position: absolute; right: 240px; top: 3px;">
    </div>

    <% MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
        int demandForPacketNumber = entities.CompanyDemandMemberships.Where(c => c.isDemandForPacket == true && c.Status == 0).ToList().Count; %>
    <%if (demandForPacketNumber > 0)
        { %>
    <div style="position: absolute; right: 245px; top: 10px;">
        <a href="/CompanyDemand/DemandForPacket?pagetype=DemandsForPacket" title="İndirimli paket bilgi talebi"><font style="color: red"><%:demandForPacketNumber %></font>Bilgi Talebi</a>
    </div>
    <%} %>




    <div style="position: absolute; right: 350px; top: 10px;">
        <a style="padding: 2px;" href="/StoreSeoNotification/AllNotification" title="Bildirimler" id="notSeo"></a>
    </div>
    <div style="position: absolute; right: 180px; top: 10px;">
        <a class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" style="padding: 2px;" href="/MemberDescription/Notification?UserId=<%:NeoSistem.MakinaTurkiye.Management.Models.Authentication.CurrentUserModel.CurrentManagement.UserId%>" title="Bildirimler" id="notification1"></a>
    </div>
    <div style="position: absolute; right: 80px; top: 10px;">
        <a class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" style="padding: 2px;" href="/MemberDescription/Notification" title="Bildirimler" id="notification"></a>
    </div>
    <div style="float: right; position: absolute; right: 10px; top: 10px;">
        <a class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" style="padding: 2px;" href="/Account/Logout">Çıkış Yap</a>
    </div>



    <%--    <div class="tabtabs" id="tabMainHome">
      <ul class="Ribbon_Zone">
        <script type="text/javascript">

            Ribbon.RibbonZone.Render({
                IdPrefix: 'btn',
                ImageLoc: '/Content/RibbonImages/',
                Groups: [

              {
                  text: 'Banner',
                  type: Ribbon.GroupType.Group,
                  buttons: [
                   {
                       Id: 'HomePageBann er', text: 'Banner', type: Ribbon.ButtonType.BigItem,
                       align: 'left', image: 'ExchangeFolder.png', action: "window.location = '/Banner/EditHomePage';"
                   }]
              }
              ,
                            {
                                text: 'TopluMail',
                                type: Ribbon.GroupType.Group,
                                buttons: [
                                 {
                                     Id: 'TopluMail', text: 'Toplu Mail', type: Ribbon.ButtonType.BigItem,
                                     align: 'left', image: 'e-mail_icon.png', action: "window.location = '/MailMessageSender/index';"
                                 }]
                            }, homePage('Category')

              ,
                {
                    text: 'Doping',
                    type: Ribbon.GroupType.Group,
                    buttons: [
                     {
                         Id: 'Doping', text: 'Dopingli', type: Ribbon.ButtonType.BigItem,
                         align: 'left', image: 'doping.jpeg', action: "window.location = '/Product/DopingShowcase';"
                     }]
                }

            ]
          });

        </script>
      </ul>
    </div>--%>
    <div class="tab-content">
        <div class="tabtabs" id="tabMainHome">
            <ul class="Ribbon_Zone">
                <script type="text/javascript">

                    Ribbon.RibbonZone.Render({
                        IdPrefix: 'btn',
                        ImageLoc: '/Content/RibbonImages/',
                        Groups: [

                            {
                                text: 'Banner',
                                type: Ribbon.GroupType.Group,
                                buttons: [
                                    {
                                        Id: 'HomePageBann er', text: 'Banner', type: Ribbon.ButtonType.BigItem,
                                        align: 'left', image: 'ExchangeFolder.png', action: "window.location = '/Banner/EditHomePage';"
                                    }]
                            }
                            ,
                            {
                                text: 'TopluMail',
                                type: Ribbon.GroupType.Group,
                                buttons: [
                                    {
                                        Id: 'TopluMail', text: 'Toplu Mail', type: Ribbon.ButtonType.BigItem,
                                        align: 'left', image: 'e-mail_icon.png', action: "window.location = '/MailMessageSender/index';"
                                    }]
                            }, homePage('Category')

                            ,
                            {
                                text: 'Doping',
                                type: Ribbon.GroupType.Group,
                                buttons: [
                                    {
                                        Id: 'Doping', text: 'Dopingli', type: Ribbon.ButtonType.BigItem,
                                        align: 'left', image: 'doping.jpeg', action: "window.location = '/Product/DopingShowcase';"
                                    }]
                            }
                            ,
                            {
                                text: 'Anasayfa',
                                type: Ribbon.GroupType.Group,
                                buttons: [
                                    {
                                        Id: 'HomeProductChoosed', text: 'Seçilen<br> Sektör Ürünleri', type: Ribbon.ButtonType.BigItem,
                                        align: 'left', image: 'AdvertCheck.png', action: "window.location = '/Product/HomeSectorProduct';"
                                    },
                                    {
                                        Id: 'UrlRedirect', text: 'Url<br> Yönlendirme', type: Ribbon.ButtonType.BigItem,
                                        align: 'left', image: 'AdvertList.png', action: "window.location = '/UrlRedirect';"
                                    }
                                ]
                            }
                        ]
                    });

                </script>
            </ul>
        </div>
        <div class="tabtabs" id="tabMember" style="display: none">
            <ul class="Ribbon_Zone">
                <script type="text/javascript">
                    Ribbon.RibbonZone.Render(
                        {
                            IdPrefix: 'btn',
                            ImageLoc: '/Content/RibbonImages/',
                            Groups: [
                                {
                                    text: 'Üye Yönetimi',
                                    type: Ribbon.GroupType.Group,
                                    width: '280px',
                                    buttons: [
                                        {
                                            Id: 'MemberManage', text: 'Tüm Üyeler', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'MemberGroup.png', action: "window.location = '/Member';"
                                        },
                                        {
                                            Id: 'MemberBulletin', text: 'Bülten Üyeleri', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'newspaper.png', action: "window.location = '/Member/BulletinMember';"
                                        },

                                        {
                                            Id: 'MemberSearchByPhone', text: 'Numaraya Göre Bul', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'phone-search.png', action: "window.location = '/Member/SearchPhone';"
                                        }
                                    ]
                                }
                            ]

                        });
                </script>
            </ul>
        </div>
        <div class="tabtabs" id="tabConstant" style="display: none">
            <ul class="Ribbon_Zone">
                <script type="text/javascript">
                    Ribbon.RibbonZone.Render(
                        {
                            IdPrefix: 'btn',
                            ImageLoc: '/Content/RibbonImages/',
                            Groups: [
                                {
                                    text: 'Faaliyet Tipi',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'NewActivityType', text: 'Yeni Faaliyet<br />Tipi', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'ActivityTypeAdd.png', action: "window.location='/ActivityType/Create'"
                                        }, {
                                            Id: 'ActivityTypeList', text: 'Faaliyet<br />Tipleri', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'ActivityTypeList.png', action: "window.location='/ActivityType/Index'"
                                        }]
                                },
                                {
                                    text: 'Adres Tipi',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'NewAddressType', text: 'Yeni Adres<br />Tipi', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'addressTypeInsert.png', action: "window.location='/AddressType/Create'"
                                        }, {
                                            Id: 'AddressTypeList', text: 'Adres Tipi<br />Listesi', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'AddressTypeList.png', action: "window.location='/AddressType/Index'"
                                        }]
                                },
                                {
                                    text: 'Adres',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'Countries', text: 'Ülkeler', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'ColumnsDialog.png', action: "window.location='/Constant/country'"
                                        },
                                        {
                                            Id: 'cities', text: 'İl İlçe Mahalle', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'ColumnsDialog.png', action: "window.location='/Constant/SubAddress'"
                                        }

                                    ]
                                },
                                {
                                    text: 'Tipler',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'ConstantType', text: 'Sabit Tipler', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'ColumnsDialog.png', action: "window.location='/Constant/Index'"
                                        }]
                                }, {
                                    text: 'MesajMailing',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'MessagesMailing', text: 'Mesaj</br>Mailing', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'mail.png', action: "window.location ='/MessagesMT';"
                                        }]
                                },
                                {
                                    text: 'Telefon Mesajları',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'MobileMessage', text: 'Telefon</br>Mesajları', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'phone-message.png', action: "window.location ='/MobileMessage';"
                                        }]
                                },
                                {
                                    text: 'Footer Alanı',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'FooterParent', text: 'Footer Ana<br>Başlık', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'footerParent.png', action: "window.location='/Footer/FooterParents'"
                                        },
                                        {
                                            Id: 'FooterContent', text: 'Footer Alt<br>Başlık', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'footerContent.png', action: "window.location='/Footer/FooterContents'"
                                        }
                                    ]
                                },
                                {
                                    text: 'Kategori',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'CategoryPropertie', text: 'Kategori Soru<br>Tanım', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'BookmarkInsert.png', action: "window.location='/Constant/Propertie'"
                                        }
                                    ]
                                }

                            ]

                        });
                </script>
            </ul>
        </div>
        <div class="tabtabs" id="tabReport" style="display: none">
            <ul class="Ribbon_Zone">
                <script type="text/javascript">
                    Ribbon.RibbonZone.Render(
                        {
                            IdPrefix: 'btn',
                            ImageLoc: '/Content/RibbonImages/',
                            Groups: [
                                {
                                    text: 'Mesaj',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'MemberMessage', text: 'Kullanıcı<br />Mesajları', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'GroupInkFormat.png', action: "window.location ='/Message';"
                                        }]
                                },
                                {
                                    text: 'Gitmeyen Mesajlar',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'ErrorMessage', text: 'Gitmeyen<br />Mesajlar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'errormessage.png', action: "window.location ='/sendederrormessage';"
                                        }]
                                },
                                {
                                    text: 'Firma Aranma Talebi',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'CompanyDemand', text: 'Firma Aranma<br>Talebi', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'demand.png', action: "window.location ='/CompanyDemand';"
                                        }]
                                },
                                {
                                    text: 'Kullanıcı Hareketleri',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'UsersLog', text: 'Kullanıcı<br>Hareketleri', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'userbehaviour.png', action: "window.location ='/log';"
                                        }]
                                },
                                {
                                    text: 'Sözlük',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'Dictinoary', text: 'Sözlük', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'dictionary.png', action: "window.location ='/dictionary';"
                                        }]
                                }, {
                                    text: 'Ürün Şikayetleri',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'ComplainProduct', text: 'Ürün Şikayetleri', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'userreview.png', action: "window.location ='/ProductComplain';"
                                        }]
                                },
                                {
                                    text: 'Favorilenmiş Ürünler',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'FavoriteProduct', text: 'Favori Ürünler', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'favorite-icon.png', action: "window.location ='/Product/FavoriteProducts';"
                                        }]
                                }

                            ]
                        });
                </script>
            </ul>
        </div>
        <div class="tabtabs" id="tabOrder" style="display: none">
            <ul class="Ribbon_Zone">
                <script type="text/javascript">
                    Ribbon.RibbonZone.Render(
                        {
                            IdPrefix: 'btn',
                            ImageLoc: '/Content/RibbonImages/',
                            Groups: [
                                {
                                    text: 'Sipariş',
                                    type: Ribbon.GroupType.Group,
                                    width: '270px',
                                    buttons: [
                                        {
                                            Id: 'ConfFirmAll', text: 'Tüm Sipariş<br />Listesi', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'orderList.png', action: "window.location ='/OrderFirm/Index';"
                                        }, {
                                            Id: 'ConfFirmApproved', text: 'Onaylanan Sipariş', type: Ribbon.ButtonType.SmallItem,
                                            align: 'left', image: 'orderList.png', action: "window.location ='/OrderFirm/Index?PacketStatu=<%:(byte)PacketStatu.Onaylandi %>';"
                                        }, {
                                            Id: 'ConfFirmNotApproved', text: 'Onaylanmayan Sipariş', type: Ribbon.ButtonType.SmallItem,
                                            align: 'left', image: 'orderList.png', action: "window.location ='/OrderFirm/Index?PacketStatu=<%:(byte)PacketStatu.Onaylanmadi %>';"
                                        }, {
                                            Id: 'ConfFirmExamined', text: 'Onayda Sipariş', type: Ribbon.ButtonType.SmallItem,
                                            align: 'left', image: 'orderList.png', action: "window.location ='/OrderFirm/Index?PacketStatu=<%:(byte)PacketStatu.Inceleniyor %>';"
                                        }]
                                },
                                {
                                    text: 'Paket',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'AddPacket', text: 'Paket<br />Ekle', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'addPacket.png', action: "window.location ='/Packet/Create';"
                                        },
                                        {
                                            Id: 'PacketList', text: 'Paket<br />Listesi', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'packetList.png', action: "window.location ='/Packet/Index';"
                                        }]
                                }, {
                                    text: 'Paket Özellikleri',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'PaketFeature', text: 'Yeni Paket<br />Özelliği', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'CellsInsertDialog.png', action: "window.location ='/PacketFeatureType/Create';"
                                        },
                                        {
                                            Id: 'PaketFeatureList', text: 'Paket<br />Özellikleri', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'AdvertisePublishAs.png', action: "window.location ='/PacketFeatureType/Index';"
                                        }]
                                }, {
                                    text: 'Banka Bilgileri',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'CreateAccount', text: 'Hesap<br />Oluştur', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'bankAccount.png', action: "window.location ='/BankAccount/Create';"
                                        }, {
                                            Id: 'AccountList', text: 'Hesap<br />Listesi', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'bankAccountList.png', action: "window.location ='/BankAccount/Index';"
                                        }]
                                },
                                {
                                    text: 'Bildirimler',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'NotificationFormList', text: 'Bildirim<br />Listesi', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'BookmarkInsert.png', action: "window.location ='/NotificationForm/Index';"
                                        }]
                                }, {
                                    text: 'Kredi Kartları',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'NewCard', text: 'Yeni Kradi<br />Kartı Tanımı', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'ChartResetToMatchStyle.png', action: "window.location ='/CreditCard/Create';"
                                        },
                                        {
                                            Id: 'CardList', text: 'Kredi Kartı<br />Listesi', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'AdvancedFileProperties.png', action: "window.location ='/CreditCard/Index';"
                                        }]
                                }, {
                                    text: 'Sanal Pos',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'VirtualPosCreate', text: 'Yeni<br />Sanal Pos', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'ReviewInkCommentEraser.png', action: "window.location ='/VirtualPos/Create';"
                                        },
                                        {
                                            Id: 'VirtualPosList', text: 'Sanal Pos<br />Listesi', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'FileServerTasksMenu.png', action: "window.location ='/VirtualPos/Index';"
                                        }]
                                }, {
                                    text: 'LOG',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'CreditCardLog', text: 'Kredi Kartı Logları', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'GroupTableDrawBorders.png', action: "window.location ='/CreditCard/CreditCardLog';"
                                        },
                                        {
                                            Id: 'OrderWriteLog', text: 'Sipariş Logları', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'OrderWriteLogs.png', action: "window.location ='/OrderFirm/OrderWriteLogs';"
                                        }]
                                }, {
                                    text: 'Ürün Satışı',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'ProductSale', text: 'Satılan Ürün<br />Listesi', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'sepet.png', action: "window.location ='/OrderFirm/ProductSales';"
                                        }]
                                }
                            ]
                        });
                </script>
            </ul>
        </div>
        <div class="tabtabs" id="tabCategory" style="display: none">
            <ul class="Ribbon_Zone">
                <script type="text/javascript">

                    Ribbon.RibbonZone.Render({
                        IdPrefix: 'btn',
                        ImageLoc: '/Content/RibbonImages/',
                        Groups: [
                            {
                                text: 'Ana Kategoriler',
                                type: Ribbon.GroupType.Group,
                                buttons: [
                                    {
                                        Id: 'MainCat', text: 'Kategori<br />Yönetimi', type: Ribbon.ButtonType.BigItem,
                                        align: 'left', image: 'BlogCategory.png', action: "window.location = '/Category/AllIndex';"
                                    },
                                    {
                                        Id: 'ImportExcelCategory', text: 'Excel İle Kategori<br />Güncelle', type: Ribbon.ButtonType.BigItem,
                                        align: 'left', image: 'excel.png', action: "window.location ='/Category/UpdateCategoryByExcelFile';"
                                    }
                                ]
                            }, {
                                text: 'Yardım Kategorileri',
                                type: Ribbon.GroupType.Group,
                                width: 'auto',
                                buttons: [
                                    {
                                        Id: 'HelpCat', text: 'Yardım</br>Kategorileri', type: Ribbon.ButtonType.BigItem,
                                        align: 'left', image: 'BreakInsertDialog.png', action: "window.location ='/Category/AllIndexHelp';"
                                    }]
                            },
                            {
                                text: 'Ürün Taşıma',
                                type: Ribbon.GroupType.Group,
                                width: 'auto',
                                buttons: [
                                    {
                                        Id: 'property', text: 'Ürün Taşıma', type: Ribbon.ButtonType.BigItem,
                                        align: 'left', image: 'BreakInsertDialog.png', action: "window.location ='/Category/ProductMove';"
                                    }]
                            },
                            {
                                text: 'Seçilmiş Kategoriler',
                                type: Ribbon.GroupType.Group,
                                width: 'auto',
                                buttons: [
                                    {
                                        Id: 'categorychoiced', text: 'Seçilmiş Kategoriler', type: Ribbon.ButtonType.BigItem,
                                        align: 'left', image: 'choiceCategory.png', action: "window.location ='/Category/CategoryPlaces';"
                                    }]
                            },
                            {
                                text: 'Ana Menü',
                                type: Ribbon.GroupType.Group,
                                width: 'auto',
                                buttons: [
                                    {
                                        Id: 'baseMenu', text: 'Ana Menü', type: Ribbon.ButtonType.BigItem,
                                        align: 'left', image: 'basemenu.png', action: "window.location ='/Category/BaseMenus';"
                                    }]
                            },


                        ]
                    });
                </script>
            </ul>
        </div>

        <div class="tabtabs" id="tabSeo" style="display: none">
            <ul class="Ribbon_Zone">
                <script type="text/javascript">
                    Ribbon.RibbonZone.Render({
                        IdPrefix: 'btn',
                        ImageLoc: '/Content/RibbonImages/',
                        Groups: [
                            {
                                text: 'Seo',
                                type: Ribbon.GroupType.Group,
                                width: 'auto',
                                buttons: [
                                    {
                                        Id: 'SeoManagement', text: 'SEO<br />Yönetimi', type: Ribbon.ButtonType.BigItem,
                                        align: 'left', image: 'FileDocumentManagementInformation.png', action: "window.location ='/Seo';"
                                    }]
                            }, {
                                text: 'Mesaj',
                                type: Ribbon.GroupType.Group,
                                width: 'auto',
                                buttons: [
                                    {
                                        Id: 'SingularViewCountProduct', text: 'İlanlar<br />Tekil Ziy.', type: Ribbon.ButtonType.BigItem,
                                        align: 'left', image: 'AlignDialog.png', action: "window.location ='/LastViewed/LVProductIndex';"
                                    }]
                            }
                        ]
                    });

                </script>
            </ul>
        </div>


        <div class="tabtabs" id="tabStore" style="display: none">
            <ul class="Ribbon_Zone">
                <script type="text/javascript">
                    Ribbon.RibbonZone.Render(
                        {
                            IdPrefix: 'btn',
                            ImageLoc: '/Content/RibbonImages/',
                            Groups: [
                                {
                                    text: 'Ön Kayıt Firma',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'PreRegisterStore', text: 'Ön Kayıt<br> Firmalar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'preregister.png', action: "window.location ='/PreRegistrationStore/';"

                                        },
                                        {
                                            Id: 'PreRegisterStoreCreate', text: 'Ön Kayıt<br> Ekle', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'ChartResetToMatchStyle.png', action: "window.location ='/PreRegistrationStore/Create';"

                                        },
                                    ]
                                },
                                {
                                    text: 'Firma Yönetimi',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [

                                        {
                                            Id: 'StoreAll', text: 'Tüm<br> Firmalar ', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'StoreAll.png', action: "window.location='/Store/Index';"
                                        }, {
                                            Id: 'StoreShowcase', text: 'Vitrindeki<br> Firmalar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'ActiveSurvey.png', action: "<%: "window.location='/Store/EditShowcase';" %>"
                                        }, {
                                            Id: 'StoreCheck', text: 'Onaylanmış<br>Firmalar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'StoreCheck.png', action: "<%: "window.location='/Store/PacketStatuSearch/2';" %>"
                                        }, {
                                            Id: 'StoreUnCheck', text: 'Onaylanmamış<br>Firmalar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'StoreUnCheck.png', action: "<%: "window.location='/Store/PacketStatuSearch/3';" %>"
                                        },


                                        {
                                            Id: 'OnayBekleyenFirmalar', text: 'Onay Bekleyen<br>Firmalar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'ActiveSurvey.png', action: "<%: "window.location='/Store/PacketStatuSearch/1';" %>"
                                        },
                                        {
                                            Id: 'StoreCheck2', text: 'Silinen<br> Firmalar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'StoreCheck.png', action: "<%: "window.location='/Store/PacketStatuSearch/4';" %>"
                                        },
                                        {
                                            Id: 'StoreChangeInfo', text: 'Değişiklik<br> Yapılanlar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'store-list.png', action: "<%: "window.location='/ChangeLogs/';" %>"
                                        },

                                        {
                                            Id: 'LoginLog', text: 'Firma<br> Hareketleri', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'login-32.png', action: "window.location ='/Store/LoginLogs';"

                                        },
                                        {
                                            Id: 'WhatsappStore', text: 'Firma<br> Tıklanmalar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'whatsapp.png', action: "window.location ='/Store/WhatsappStore';"

                                        },
                                        {
                                            Id: 'StoreNew', text: 'Firma<br> Haberler', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'newsstore.png', action: "window.location ='/Store/New?newType=1';"

                                        },
                                        {
                                            Id: 'StoreSuccessStories', text: 'Başarı<br> Hikayeleri', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'newsstore.png', action: "window.location ='/Store/New?newType=2';"

                                        },

                                        {
                                            Id: 'ImportExcelStore', text: 'Excel İle Firma<br />Güncelle', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'excel.png', action: "<%: "window.location='/Store/UpdateStoreByExel';" %>"
                                        },
                                        {
                                            Id: 'CertificateTypes', text: 'Sertifika Tipleri', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'medal.png', action: "<%: "window.location='/Product/CertificateTypes';" %>"
                                        },

                                    ]
                                },
                  


                                {
                                    text: 'Bilgi Değişiklikleri',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'UpdaStore', text: 'Firma Bilgileri', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'store-list.png', action: "<%: "window.location='/ChangeLogs/Store';" %>"
                                        },
                                        {
                                            Id: 'UpdateAddress', text: 'Adres Bilgileri', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'address-list.png', action: "<%: "window.location='/ChangeLogs/address';" %>"
                                        }, {
                                            Id: 'UpdatePhone', text: 'Telefon Bilgileri', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'phone-list.png', action: "<%: "window.location='/ChangeLogs/Phone';" %>"
                                        }

                                    ]
                                }

                            ]
                        });
                </script>
            </ul>
        </div>
        <div class="tabtabs" id="tabAdvert" style="display: none">
            <ul class="Ribbon_Zone">
                <script type="text/javascript">
                    Ribbon.RibbonZone.Render(
                        {
                            IdPrefix: 'btn',
                            ImageLoc: '/Content/RibbonImages/',
                            Groups: [
                                {
                                    text: 'İlan Yönetimi',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'AdvertAll', text: 'Tüm<br />İlanlar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'AdvertList.png', action: "window.location='/Product/'"
                                        },
                                        {
                                            Id: 'AdvertNews', text: 'İncelenen<br>İlanlar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'AdvertNews.png', action: "window.location='/Product/ActiveType/0'"
                                        }, {
                                            Id: 'AdvertCheck', text: 'Onaylanmış<br> İlanlar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'AdvertCheck.png', action: "window.location='/Product/ActiveType/1'"
                                        },
                                        {
                                            Id: 'AdvertUnCheck', text: 'Onaylanmamış<br> İlanlar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'AdvertUnCheck.png', action: "window.location='/Product/ActiveType/2'"
                                        }, {
                                            Id: 'AdvertDeleted', text: 'Silinen<br />İlanlar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'AdvertList.png', action: "window.location='/Product/ActiveType/3'"
                                        }, {
                                            Id: 'AdvertDeletedNew', text: 'Çöp İlanlar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'trash-icon.png', action: "window.location='/Product/ActiveType/8'"
                                        },
                                        {
                                            Id: 'AdvertPassive', text: 'Pasif<br />İlanlar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'AdvertList.png', action: "window.location='/Product/Active/0'"
                                        },
                                        {
                                            Id: 'AdvertShowcase', text: 'Vitrindeki<br />İlanlar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'ActiveSurvey.png', action: "<%: "window.location='/Product/EditShowcase';" %>"
                                        },
                                        {
                                            Id: 'VideoShowcase', text: 'Vitrindeki<br />Videolar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'ActiveSurvey.png', action: "<%: "window.location='/Video/EditShowcase';" %>"
                                        },
                                        {
                                            Id: 'ImportExcel', text: 'Excel İle Ürün<br />Güncelle', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'excel.png', action: "<%: "window.location='/Product/UpdateProductByExcelFile';" %>"
                                        },


                                    ]
                                },
                                {
                                    text: 'Yorumlar',
                                    type: Ribbon.GroupType.Group,
                                    width: '150px',
                                    buttons: [{
                                        Id: 'ProductComment', text: 'Ürün Yorumları', type: Ribbon.ButtonType.SmallItem,

                                        align: 'left', image: 'comment.png', action: "<%: "window.location='/Product/Comment';" %>"
                                    },
                                        {
                                            Id: 'ReportCOmment', text: 'Şikayetli Yorumlar', type: Ribbon.ButtonType.SmallItem,

                                            align: 'left', image: 'comment.png', action: "<%: "window.location='/Product/Comment?Reported=1';" %>"
                                        },
                                    ]
                                }
                            ]
                        });
                </script>
            </ul>
        </div>
        <div class="tabtabs" id="tabUser" style="display: none">
            <ul class="Ribbon_Zone">
                <script type="text/javascript">
                    Ribbon.RibbonZone.Render(
                        {
                            IdPrefix: 'btn',
                            ImageLoc: '/Content/RibbonImages/',
                            Groups: [
                                {
                                    text: 'Kullanıcı',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'NewUser', text: 'Yeni<br />Kullanıcı', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'user.png', action: "window.location='/User/Create'"
                                        }, {
                                            Id: 'UserList', text: 'Kullanıcı<br />Listesi', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'addressbook.png', action: "window.location='/User/Index'"
                                        }]
                                },
                                {
                                    text: "İzin",
                                    type: Ribbon.GroupType.Group,
                                    width: '150px',
                                    buttons: [
                                        {
                                            Id: 'UserPermission', text: 'İzinler', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', action: "window.location ='/Permission';", image: 'UserPermission.png'
                                        },
                                        {
                                            Id: 'UserGroup', text: 'İzin<br />Grupları', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', action: "window.location ='/UserGroup';", image: 'UserGroup.png'
                                        },
                                        {
                                            Id: 'NewUserGroup', text: 'Yeni İzin<br />Grubu', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', action: "window.location ='/UserGroup/Create';", image: 'MemberGroup.png'
                                        }]
                                }]
                        });
                </script>
            </ul>
        </div>
        <div class="tabtabs" id="tabSales" style="display: none">
            <ul class="Ribbon_Zone">
                <script type="text/javascript">
                    Ribbon.RibbonZone.Render(
                        {
                            IdPrefix: 'btn',
                            ImageLoc: '/Content/RibbonImages/',
                            Groups: [
                                {
                                    text: 'Satış',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'Help', text: 'Yardım', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'user.png', action: "window.location='/Help/Index'"
                                        },
                                        {
                                            Id: 'NewHelp', text: 'Yeni Ekle', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'AdvertNews.png', action: "window.location='/Help/Add'"
                                        },
                                        {
                                            Id: 'OrderCount', text: 'Satış Sayıları', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'orderList.png', action: "window.location='/OrderFirm/OrderCount'"
                                        },


                                    ]
                                },


                                {
                                    text: 'Sorun',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [

                                        {
                                            Id: 'ErrorCreate', text: 'Sorun Bildir', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'OrderWriteLogs.png', action: "window.location='/Help/ErrorCreate'"
                                        },
                                        {
                                            Id: 'Errors', text: 'Sorunlar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'errormessage.png', action: "window.location='/Help/Errors'"
                                        }

                                    ]
                                },
                                {
                                    text: 'Öneri',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [

                                        {
                                            Id: 'AdivceCreate', text: 'Öneri Yaz', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'FilePrepareMenu.png', action: "window.location='/Help/ErrorCreate?type=advice'"
                                        },
                                        {
                                            Id: 'Advices', text: 'Öneriler', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'BulletListDefault.png', action: "window.location='/Help/Errors?type=advice'"
                                        }

                                    ]
                                },
                                {
                                    text: 'Üye Açıklama',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'MemberNotes', text: 'Üye</br>Açıklamaları', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'FilePrepareMenu.png', action: "window.location ='/MemberDescription';"
                                        },
                                        {
                                            Id: 'StoreSeo', text: 'Seo</br>Açıklamaları', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'store-list.png', action: "window.location ='/StoreSeoNotification/AllSeoNotification';"
                                        },
                                    ]
                                },

                                {
                                    text: 'Görevler',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'MemberTask', text: 'Görev</br>Atamaları', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'task-list.png', action: "window.location ='/MemberDescription/alltask';"
                                        },
                                        {
                                            Id: 'MemberTaskByDate', text: 'Kullanıcı Atama</br>Sayıları', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'check-list.png', action: "window.location ='/MemberDescription/NotificationCount';"
                                        }
                                    ]
                                },
                                {
                                    text: 'Açıklamasız',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'WithoutDescriptionStore', text: 'Açıklamasız<br> Firmalar', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'StoreAll.png', action: "window.location ='/Store/WithoutDescriptionStore';"
                                        }]
                                },
                                {
                                    text: 'Hatalar',
                                    type: Ribbon.GroupType.Group,
                                    width: 'auto',
                                    buttons: [
                                        {
                                            Id: 'ApplicationLog', text: 'Hata<br> Logları', type: Ribbon.ButtonType.BigItem,
                                            align: 'left', image: 'password.png', action: "window.location ='/ApplicationLog';"
                                        }]
                                }


                            ]
                        });
                </script>
            </ul>
        </div>
    </div>
