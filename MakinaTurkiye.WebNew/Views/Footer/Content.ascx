<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NeoSistem.MakinaTurkiye.Web.Models.Footer.MTFooterParentModel>>" %>
<span class="scroll-top js-scroll-top">
    <span class="icon-up-arrow"></span>
</span>

<div class="footer">
    <div class="container">
        <div class="footer__top row">
            <div class="col-xs-12 col-sm-8 col-sm-offset-2 col-md-4 col-md-offset-4">
                <p class="ebulten-form__label">e-bülten üyeliği ile yeniliklerimizden haberdar olun</p>
                <div class="ebulten-form clearfix">
                    <form action="/uyelik/bulten-uyeligi" method="get">
                        <input type="text" autocomplete="off" class="ebulten-form__input" name="email" placeholder="E-Posta Adresiniz" />
                        <button type="submit" class="ebulten-form__button">Abone Ol</button>
                    </form>
                </div>
            </div>
        </div>
        <div class="footer__bottom row">
            <div class="col-sm-12 col-md-12">
                <div class="flex-row mb20 flex-md-nowrap flex-lg-nowrap">
                    <%foreach (var item in Model)
                        {
                            string cssMd = "flex-md-3 flex-lg-3";
                            if (item.FooterParentName == "Kategori")
                            {
                                cssMd = "flex-md-3 flex-lg-2";
                            }

                    %>
                    <div class="menu flex-xs-6  flex-sm-4 <%:cssMd %>" data-parent="<%:item.FooterParentName %>">
                        <span><%:item.FooterParentName %></span>
                        <ul class="list-unstyled" role="menu">
                            <%foreach (var itemFooterContent in item.FooterContents)
                                {%>
                            <li><a class="text-muted" href="<%:itemFooterContent.FooterContentUrl %>"><%:itemFooterContent.FooterContentName %></a></li>
                            <%} %>
                        </ul>
                    </div>

                    <%} %>

                    <div class="footer-social-media flex-xs-6 flex-sm-4 flex-md-2 flex-lg-3" role="menu">
                        <span>Bizi Takip Edin</span>
                        <ul class="footer-social-media__items list-unstyled clearfix">
                            <li class="footer-social-media__item">
                                <a href="https://www.facebook.com/makinaturkiyecom" target="_blank" rel="nofollow" class="footer-social-media__link footer-social-media__link--fb">Facebook</a>
                            </li>
                            <li class="footer-social-media__item">
                                <a href="https://www.linkedin.com/company/makinaturkiye-com?trk=company_name" rel="nofollow" target="_blank" class="footer-social-media__link footer-social-media__link--in">Linkedin</a>
                            </li>
                            <li class="footer-social-media__item">
                                <a href="https://www.twitter.com/MakinaTurkiye" rel="nofollow" target="_blank" class="footer-social-media__link footer-social-media__link--tw">Twitter</a>
                            </li>
                            <li class="footer-social-media__item">
                                <a href="https://www.instagram.com/makinaturkiye/?hl=tr" rel="nofollow" target="_blank" class="footer-social-media__link footer-social-media__link--inst">Instagram</a>
                            </li>
                            <li class="footer-social-media__item">
                                <a href="https://tr.pinterest.com/makinaturkiyecom/" rel="nofollow" target="_blank" class="footer-social-media__link footer-social-media__link--pin">Pinterest</a>
                            </li>
                            <li class="footer-social-media__item">
                                <a href="https://blog.makinaturkiye.com" rel="nofollow" target="_blank" class="footer-social-media__link footer-social-media__link--blog">Blog</a>
                            </li>
                        </ul>
                          <span><a style="color:#0c3871" href="<%:AppSettings.VideoUrlBase %>">Videolar</a></span>
                    </div>
                   
                </div>
                <div class="row copyright-v2">
                    <div class="col-xs-12 text-muted text-center">
                        <%--      <p class="text-xs">
                            makinaturkiye.com'da yer alan kullanıcıların oluşturduğu tüm
                        içerik, görüş ve bilgilerin doğruluğu, eksiksiz ve değişmez olduğu,
                        yayınlanması ile ilgili yasal yükümlülükler içeriği oluşturan
                        kullanıcıya aittir. Bu içeriğin, görüş ve bilgilerin yanlışlık,
                        eksiklik veya yasalarla düzenlenmiş kurallara aykırılığından
                        makinaturkiye.com hiçbir şekilde sorumlu değildir. Sorularınız için
                        ilan sahibi ile irtibata geçebilirsiniz.
                        </p>--%>
                        <%--<i class="fa fa-phone-square text-primary"></i> 
                     &nbsp;&nbsp;
                     <a class="text-muted" href="tel:0212442223">
                        0212 444 22 23
                     </a>
                     &nbsp;&nbsp;--%>
                        <div class="copyright-v2__inner">
                            <p>Copyright © 2010-<%:DateTime.Now.Year %> makinaturkiye.com</p>
                            <ul>
                                <li><a href="/kullanim-kosullari-y-141618">Kullanım Koşulları</a></li>
                                <li><a href="/gizlilik-ve-guvenlik-y-142519">Gizlilik ve Güvenlik</a></li>
                                <li><a href="/gizlilik-politikasi-y-144081">Gizlilik Politikası</a></li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
