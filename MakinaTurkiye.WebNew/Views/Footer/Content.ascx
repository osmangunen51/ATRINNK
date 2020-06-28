<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NeoSistem.MakinaTurkiye.Web.Models.Footer.MTFooterParentModel>>" %>

<div onclick="topFunction()" id="myBtn" class="mobile-top" title="Go to top"><span class="more-less icon-up-arrow"></span></div>

<div class="footer">
    <div class="container">
        <div class="row" style="margin-bottom: 30px">
            <div class="col-md-12">
                <a class="footer-logo" href="<%:AppSettings.SiteUrlWithoutLastSlash %>">
                    <img src="/Content/V2/images/makinaturkiye-dark.png" srcset="/Content/V2/images/makinaturkiye-dark.png 1x, /Content/V2/images/makinaturkiye-dark.png 2x" alt="Makina Türkiye Alt Logo" width="226" height="30">
                </a>
            </div>
        </div>

        <%--                <div class="footer__top row">
            <div class="col-xs-12 col-sm-8 col-sm-offset-2 col-md-4 col-md-offset-4">
                <p class="ebulten-form__label">e-bülten üyeliği ile yeniliklerimizden haberdar olun</p>
                <div class="ebulten-form clearfix">
                    <form action="/uyelik/bulten-uyeligi" method="get">
                        <input type="text" autocomplete="off" class="ebulten-form__input" name="email" placeholder="E-Posta Adresiniz" />
                        <button type="submit" class="ebulten-form__button">Abone Ol</button>
                    </form>
                </div>
            </div>
        </div>--%>
        <div class="footer__bottom row">
            <div class="col-sm-12 col-md-12">
                <div class="flex-row mb20 flex-md-nowrap flex-lg-nowrap">
                    <%foreach (var item in Model)
                        {
                            string cssMd = "flex-md-3 flex-lg-3";
                            if (item.FooterParentName == "Kategori")
                            {
                                cssMd = "flex-md-2 flex-lg-2";
                            }

                    %>
                    <div class="menu col-6 col-md-4 col-xl-2" data-parent="<%:item.FooterParentName %>">
                        <h4><%:item.FooterParentName %></h4>
                        <ul class="list-unstyled" role="menu">
                            <%foreach (var itemFooterContent in item.FooterContents)
                                {%>
                            <li><a href="<%:itemFooterContent.FooterContentUrl %>"><%:itemFooterContent.FooterContentName %></a></li>
                            <%} %>
                        </ul>
                    </div>

                    <%} %>

                    <div class="menu socialM flex-xs-6 flex-sm-4 flex-md-2 flex-lg-3" role="menu">
                        <h4>Sosyal Medya</h4>
                        <ul class="list-unstyled" role="menu">
                            <li class="">
                                <a href="https://www.facebook.com/makinaturkiyecom" target="_blank" rel="nofollow">
                                    <img src="/Content/v2/images/social/facebook-icon.png" alt="Facebook">
                                    Facebook

                                </a>
                            </li>
                            <li>
                                <a href="https://www.twitter.com/MakinaTurkiye" rel="nofollow" target="_blank">
                                    <img src="/Content/v2/images/social/twitter-icon.png" alt="Twitter">
                                    Twitter</a>
                            </li>
                            <li>
                                <a href="https://www.linkedin.com/company/makinaturkiye-com?trk=company_name" rel="nofollow" target="_blank">
                                    <img src="/Content/v2/images/social/linkedin-icon.png" alt="Facebook">
                                    Linkedin</a>
                            </li>
                            <li>
                                <a href="https://www.instagram.com/makinaturkiye/?hl=tr" rel="nofollow" target="_blank">
                                    <img src="/Content/v2/images/social/instagram-icon.png" alt="İnstagram">
                                    İnstagram</a>
                            </li>
                            <li>
                                <a href="https://tr.pinterest.com/makinaturkiyecom/" rel="nofollow" target="_blank">
                                    <img src="/Content/v2/images/social/pinterest-icon.png" alt="İnstagram">
                                    Pinterest</a>
                            </li>
                            <li>
                                <a href="https://tr.pinterest.com/makinaturkiyecom/" rel="nofollow" target="_blank">
                                    <img src="/Content/v2/images/social/blog.png" style="width: 16px;" alt="İnstagram">
                                    Blog</a>
                            </li>
                        </ul>

                    </div>

                </div>
                <div class="row copyright-v2">
                

                        <div class="copyright-v2__inner">
                            <p>Copyright © 2010-<%:DateTime.Now.Year %> makinaturkiye.com</p>
                            <a href="javascript:;" class="btn upBtn hidden-xs">Başa Dön</a>
                        </div>

                   
                </div>
            </div>
        </div>
    </div>
</div>
