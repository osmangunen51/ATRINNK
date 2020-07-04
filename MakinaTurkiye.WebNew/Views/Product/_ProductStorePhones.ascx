<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.Catalog.ProductContactModel>" %>
<div class="urun-iletisim-block">
    <div class="urun-iletisim-body">
        <div class="urun-iletisim-body-firma-bilgileri">
            <div class="urun-iletisim-urun-detay ">
                <div class="urun-iletisim-urun-detay-photo">
                    <a href="<%:Model.ProductUrl %>">
                        <img src="<%=Model.ProductPictureUrl %>" width="180" height="135" class="img-responsive">
                    </a>
                </div>
                <div class="urun-iletisim-urun-detay-urun-adi">
                    <span><b><%=Model.ProductName %></b></span>
                </div>
                <div class="urun-iletisim-urun-detay-ilan-no">
                    <span>İlan No : <%=Model.ProductNo %></span>
                </div>
            </div>
            <div class="urun-iletisim-body-firma-iletisim-bilgileri">
                <div class="urun-iletisim-body-firma-iletisim-bilgileri-ullist">
                    <%var phones = Model.StoreModel.PhoneModels.Where(x => x.ShowPhone == true).ToList(); %>
                    <% if (phones.Count > 0)
                        {
                            foreach (var phone in phones)
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
                            %>
                            <div class="dropdown whatsappMessage">
                                <%   string whatsappURL = "https://api.whatsapp.com/send?phone=" + whatsappPhone + "&text=" + Model.WhatsappMessageItem.MessageContent;
                                %>
                                <a href="<%:whatsappURL %>" onclick="AddWhatsappLog(<%:Model.StoreModel.MainPartyId %>)" onclick="AddWahatt" target="_blank" rel="nofollow">
                                    <span class="visible-xs">Whatsapp
                                    </span>
                                    <span class="hidden-xs">Whatsappla iletişim Kur
                                    </span>
                                </a>
                            </div>
                            <%--  <a  href="<%=whatsappURL %>">Whatsappla İletişim Kur </a>--%>
                            <%  }
                                else
                                {%>
                            <a href="tel:<%=phone.GetFullText(false)%>"><%=phone.GetFullText() %></a>
                            <% }
                            %>
                        </div>
                    </div>
                    <%  } %>
                    <% }
                        else
                        {%>
                    <div class="alert" role="alert">
                        Satıcı bu saatlerde sadece mesaj yoluyla iletişim kabul etmektedir.
                    </div>
                    <% }%>
                </div>
                <%if (AuthenticationUser.Membership.MainPartyId > 0)
                    { %>
                <div style="margin-top: 5px;">

                    <a href="/Account/Message/Index?MessagePageType=1&UyeNo=<%:Model.MemberMainPartyId %> &UrunNo=<%:Model.ProductNo.Replace("#","") %>" class="btn btn-sm btn-primary product-detail-description__button-send-message"><span class="glyphicon glyphicon-envelope"></span>&nbsp;Satıcıya Mesaj Gönder Veya Soru Sor </a>
                </div>
                <%} %>
            </div>
        </div>
    </div>
</div>
