﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.AccountModels.MTAccountHomeCenterModel>" %>
<% string member = "Sayın, \"" + Model.MemberName + " " + Model.MemberSurname + "\" hoş geldiniz.";
    if (Model.MemberType == (byte)MemberType.FastIndividual)
        member += "<b> (Hızlı Üyesiniz.) </b>";
    else if (Model.MemberType == (byte)MemberType.Individual)
        member += "<b> (Bireysel Üyesiniz.) </b>";
    else
        member += "<b> (Kurumsal Üyesiniz.) </b>";

    MakinaTurkiyeEntities makinaTurkiyeEntities = new MakinaTurkiyeEntities();
    var messageCount = makinaTurkiyeEntities.messagechecks.Where(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId).ToList().Count;
%>
<div class="col-md-9 col-sm-12">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12">

            <%=member%>
            <%if (Model.MessageSended)
                {%>

            <div class="alert alert-success alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                Mesajınız gönderilmiştir. Alıcılar içi  üyelik avantajlarından yararlanmak için üyeliğinizi tamamlamanız gerekmektedir.
                                 <br />
                <a href="/Account/MemberType/Individual"><b>Üyeliğinizi tamamlamak için tıklayınız</b></a>
            </div>
            <% } %>
            <% 
                if (Model.MemberType != (byte)MemberType.FastIndividual && Model.MemberType != (byte)MemberType.Individual)
                { %>
            <a href="#">Firma Ayarları </a>
            <% } %>
            <%if (Model.LastPage == "bireyselUyelikOnay")
                { %>
            <div class="alert alert-success alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                Bireysel üyeliğiniz aktif edilmiştir.<br />
                <%--            <%if (Model.MemberType )
                { %>
                                Mesajınızı görmek için <a class="btn btn-primary btn-sm" href="/Account/Message/Index?MessagePageType=0">Tıklayınız</a>
            <%} %>--%>
            </div>

            <%} %>
            <%else if (Model.LastPage == "KurumsalOnay")
                { %>
            <div class="alert alert-success alert-dismissable">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                Kurumsal Üyeliğiniz İncelenmektedir.İncelendikten Sonra Onaylanacaktır.
                         
            </div>

            <%} %>

            <br>

            <%if (!Model.HasPacket && Model.MemberType == (byte)MemberType.Enterprise)
                { %>
            <div class="alert alert-info">
                <span class="fa fa-exclamation-triangle" aria-hidden="true"></span>Üyelik paketi satın almak için <a href="/account/advert/notlimitedaccess">tıklayınız</a>
            </div>
            <%} %>
            <div class="well mt10">

                <%if (Model.MemberType == (byte)MemberType.FastIndividual)
                    { %>
                <div class="row">
                    <div class="col-md-12" style="font-size: 13px; color: #808080">
                        Bireysel üyelerimiz portalımızda bütün firmaların ürünlerini inceleyebilir, mesaj gönderebilir, satıcı firmalarla iletişim kurabilir.
Endüstri ve makina sektöründe ki güncel bilgilerden anında haberdar olabilir.
                            <a class="btn btn-primary btn-sm col-md-12" href="/Account/MemberType/Individual">Alıcılar İçin Üyelik</a>
                    </div>
                    <div class="col-md-12" style="font-size: 13px; color: #808080; margin-top: 15px;">
                        Kurumsal üye olan firmalarımız tüm şirket bilgilerinin yanında ilan, marka, model bilgileri, ürün videosu ve ürün görselleri ekleyerek hemen satış yapmaya başlayabilir.
Ziyaretçilere ulaşarak satışlarını arttırabilir.
                            <a class="btn btn-success btn-sm col-md-12" style="margin-top: 10px;" href="/Account/MemberType/Individual?type=fast">Satıcılar İçin Üyelik</a>
                    </div>
                </div>
                <%}
                    else
                    { %>
                <span class="glyphicon glyphicon-envelope"></span>
                <% if (Model.InboxMessageCount > 0)
                    { %>
                                Gelen Kutunuzda <%:Model.InboxMessageCount - messageCount.ToInt32()%> Mesajınız Bulunmaktadır.
           
                                        <% }
                                            else
                                            { %>
                                   Gelen Kutunuzda Mesaj Bulunmamıştır.
            
                                        <%  } %>

                <%} %>
            </div>
            <% if (Model.MemberType != (byte)MemberType.FastIndividual && Model.MemberType != (byte)MemberType.Individual)
                { %>

            <div class="row">
                <div class="col-xs-12 col-sm-6">
                    <div class="panel panel-mt2">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <a href="#">Genel İlan Bilgileri
                                </a>
                            </h3>
                        </div>
                        <div class="panel-body">
                            Toplam İlan Sayısı : <%=Model.TotalProductCount%>
                            <br>
                            Onayda İlan Sayısı :<%:Model.CheckingProductCount %>
                            <br>
                            Onaylanmış İlan Sayısı :<%:Model.CheckedProductCount %>
                            <br>
                            Onaylanmamış İlan Sayısı :<%:Model.NotCheckedProductCount %>
                            <br>
                            Silinen İlan Sayısı :<%:Model.DeletedProductCount %>
                            <br>
                            Aktif İlan Sayısı :<%:Model.ActiveProductCount %>
                            <br>
                            Pasif İlan Sayısı :<%:Model.PasiveProductCount %>
                        </div>
                    </div>
                </div>

                <%if (Model.MemberType == 20)
                    {

                %>

                <div class="col-xs-12 col-sm-6">
                    <div class="panel panel-mt2">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <a href="#">İstatistikler
                                </a>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <b>Firma Görüntülenme:
                            </b>
                            <br>
                            <%:Model.ViewSingularCount %>


                            <br>
                            <%:Model.ViewMultipleCount %>


                            <br>
                            <b>Toplam Ürün Görüntülenme:
                            </b>
                            <br>
                            <%:Model.ViewProductSingularCount %>(Tekil)
                 
                                    <br>
                            <%:Model.ViewProductMultipleCount %> (Çoğul)
                 
                                    <br>
                            <br>
                        </div>
                    </div>
                </div>
                <% } %>
            </div>
            <%}%>
            <%if (Model.HasPacket)
                {%>

            <div class="panel panel-mt col-xs-12 p0">
                <div class="panel-heading">
                    <span class="fa fa-bullhorn"></span>
                    Sizden Haberler
                </div>
                <div class="panel-body" style="font-size: 15px;">
                    Firmanıza ait son yenilikleri, katıldığınız fuarları makinaturkiye.com anasayfasında müşterilerinize sunabilirsiniz.
                <br />
                    <a class="btn btn-success" href="/Account/StoreNew/Create?newType=<%:(byte)StoreNewType.Normal %>">Şimdi Ekle!</a>
                </div>
            </div>
            <div class="panel panel-mt col-xs-12 p0">
                <div class="panel-heading">
                    <span class="fa fa-bullhorn"></span>
                    Başarı Hikayeleriniz
                </div>
                <div class="panel-body" style="font-size: 15px;">
                    Firmanıza ait başarı hikayelerini makinaturkiye.com anasayfasında müşterilerinize sunabilirsiniz.
                <br />
                    <a class="btn background-mt-btn" href="/Account/StoreNew/Create?newType=<%:(byte)StoreNewType.SuccessStories %>">Şimdi Ekle!</a>
                </div>
            </div>

            <% } %>
            <%if (Model.ProductComments.Source.Count > 0)
                {%>
            <div class="panel panel-mt col-md-12">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-comment"></span>
                    Yorumlarım
           
                </div>
                <div class="panel-body">

                    <table class="table table-hover">
                        <tr>
                            <th>Ürün Adı</th>
                            <th>Yorum</th>
                            <th>Puan</th>
                            <th>Tarih</th>
                            <th>Durum</th>
                            <th></th>
                        </tr>
                        <tbody id="data">
                            <%=Html.RenderHtmlPartial("_CommentList",Model.ProductComments) %>
                        </tbody>
                    </table>
                </div>
            </div>
            <% }%>

<%--            <div class="panel panel-mt col-xs-12 p0">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-question-sign"></span>
                    Sayfa Yardımı
           
                </div>
                <div class="panel-body">
                    <%foreach (var item in Model.HelpList)
                        {%>
                    <i
                        class="fa fa-angle-right"></i>
                    &nbsp;&nbsp;
             
                            <a href="<%:item.Url %>"><%:item.HelpCategoryName %>
                            </a>
                    <br>

                    <%} %>
                </div>
            </div>--%>
        </div>
    </div>
</div>
