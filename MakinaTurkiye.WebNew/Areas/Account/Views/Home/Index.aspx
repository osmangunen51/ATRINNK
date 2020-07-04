﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master"
    Inherits="System.Web.Mvc.ViewPage<MyAccountHomeModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
           <div class="col-md-12">
                <h4 class="mt0 text-info">
                    <span class="text-primary glyphicon glyphicon-cog"></span>
                    Hesabım
                     </h4>
                   <hr />
            </div>
         
    <div class="row">
        <div class="col-sm-4 col-md-3">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenuModel)%>
        </div>
        <% string member = "Sayın, \"" + AuthenticationUser.Membership.MemberName + " " + AuthenticationUser.Membership.MemberSurname + "\" hoş geldiniz.";
           if (AuthenticationUser.Membership.MemberType == (byte)MemberType.FastIndividual)
               member += "<b> (Hızlı Üyesiniz.) </b>";
           else if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Individual)
               member += "<b> (Bireysel Üyesiniz.) </b>";
           else
               member += "<b> (Kurumsal Üyesiniz.) </b>";

           MakinaTurkiyeEntities makinaTurkiyeEntities = new MakinaTurkiyeEntities();
           var messageCount = makinaTurkiyeEntities.messagechecks.Where(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId).ToList().Count;

           IList<Product> productItems = makinaTurkiyeEntities.Products.Where(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId).ToList();
       
  
        %>
 
        <div class="col-sm-8 col-md-6 ">
                <div class="col-xs-12 col-sm-12 col-md-12">
                    <%=member%>
                    <% 
                        if (AuthenticationUser.Membership.MemberType != (byte)MemberType.FastIndividual && AuthenticationUser.Membership.MemberType != (byte)MemberType.Individual)
                        { %>
                    <a href="#">Firma Ayarları </a>
                    <% } %>
                    <%if(ViewData["gelenSayfa"]=="bireyselUyelikOnay"){ %>
                            <div class="alert alert-success alert-dismissable">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                            Bireysel üyeliğiniz aktif edilmiştir.<br />
                                     <%if (Model.MemberType != null)
                                { %>
                                Mesajınızı görmek için <a class="btn btn-primary btn-sm" href="/Account/Message/Index?MessagePageType=0">Tıklayınız</a>
                              <%} %>
                    </div>

                    <%} %>
                    <%else if(ViewData["gelenSayfa"]=="KurumsalOnay"){ %>
                          <div class="alert alert-success alert-dismissable">
                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                          Kurumsal Üyeliğiniz İncelenmektedir.İncelendikten Sonra Onaylanacaktır.
                         
                    </div>

                    <%} %>
                    <br>    
                    <%if (!Model.hasPacket && AuthenticationUser.Membership.MemberType == (byte)MemberType.Enterprise)
                                   { %>
                         <div class="alert alert-info">
                          <span class="fa fa-exclamation-triangle" aria-hidden="true"></span> Üyelik paketi satın almak için <a href="/account/advert/notlimitedaccess">tıklayınız</a>
                        </div>     
                        <%} %>
                    <div class="well mt10">
             
                       <%if(AuthenticationUser.Membership.MemberType==(byte)MemberType.FastIndividual){ %>
                        <div class="row">
                        <div class="col-md-12" style="font-size:13px; color:#808080">
                              Bireysel üyelerimiz portalımızda bütün firmaların ürünlerini inceleyebilir, mesaj gönderebilir, satıcı firmalarla iletişim kurabilir.
Endüstri ve makina sektöründe ki güncel bilgilerden anında haberdar olabilir.
                            <a class="btn btn-primary btn-sm col-md-12" href="/Account/MemberType/Individual">Bireysel Üyeliğe Geçmek İstiyorum</a>
                        </div>
                        <div class="col-md-12" style="font-size:13px; color:#808080; margin-top:15px;">
                             Kurumsal üye olan firmalarımız tüm şirket bilgilerinin yanında ilan, marka, model bilgileri, ürün videosu ve ürün görselleri ekleyerek hemen satış yapmaya başlayabilir.
Ziyaretçilere ulaşarak satışlarını arttırabilir.
                            <a class="btn btn-success btn-sm col-md-12" style="margin-top: 10px;" href="/Account/MemberType/Individual?type=fast">Kurumsal Üyeliğe Geçmek İstiyorum</a>
                        </div>
                     </div>
                        <%}else{ %>
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
                    <% if (AuthenticationUser.Membership.MemberType != (byte)MemberType.FastIndividual && AuthenticationUser.Membership.MemberType != (byte)MemberType.Individual)
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
                                    Toplam İlan Sayısı : <%=Model.ProductCount%>
                                    <br>
                                    Onayda İlan Sayısı : <%:productItems.Where(c => c.ProductActiveType == (byte)ProductActiveType.Inceleniyor).Count()%>
                                    <br>
                                    Onaylanmış İlan Sayısı : <%:productItems.Where(c => c.ProductActiveType == (byte)ProductActiveType.Onaylandi).Count()%>
                                    <br>
                                    Onaylanmamış İlan Sayısı : <%:productItems.Where(c => c.ProductActiveType == (byte)ProductActiveType.Onaylanmadi).Count()%>
                                    <br>
                                    Silinen İlan Sayısı : <%:productItems.Where(c=> c.ProductActiveType == (byte)ProductActiveType.Silindi).Count()%>
                                    <br>
                                    Aktif İlan Sayısı : <%:productItems.Where(c => c.ProductActive == true && c.ProductActiveType == (byte)ProductActiveType.Onaylandi).Count()%>
                                    <br>
                                    Pasif İlan Sayısı : <%:productItems.Where(c => c.ProductActive == false).Count()%>
                                </div>
                            </div>
                        </div>
                        <%if (AuthenticationUser.Membership.MemberType == 20)
                          {
                              var storeview = makinaTurkiyeEntities.MemberStores.Where(c => c.MemberMainPartyId == AuthenticationUser.Membership.MainPartyId).SingleOrDefault().StoreMainPartyId;
                              var store = makinaTurkiyeEntities.Stores.Where(c => c.MainPartyId == storeview).SingleOrDefault();
                              var product = makinaTurkiyeEntities.Products.Where(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId).ToList();
                              int singular = 0;
                              int plural = 0;
                              foreach (var count in product)
                              {
                                  singular = count.SingularViewCount.ToInt32() + singular;
                                  plural = count.ViewCount.ToInt32() + plural;
                              }
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
                                    <%:store.SingularViewCount%> (Tekil)
                 
                                    <br>
                                    <%:store.ViewCount%> (Çoğul)
                 
                                    <br>
                                    <b>Toplam Ürün Görüntülenme:
                  </b>
                                    <br>
                                    <%=singular %> (Tekil)
                 
                                    <br>
                                    <%=plural %> (Çoğul)
                 
                                    <br>
                                    <br>
                                </div>
                            </div>
                        </div>
                        <% } %>
                    </div>
                    <%}%>
                    <div class="panel panel-mt col-xs-12 p0">
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
                    </div>
                </div>
        </div>
          <div class="col-md-3 col-xs-12 col-sm-12">
                    <%if (AuthenticationUser.Membership.MemberType != (byte)MemberType.FastIndividual && AuthenticationUser.Membership.MemberType != (byte)MemberType.Individual)
                      { %>
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <a href="#">Üyelik Paketi
                </a>
                            </h3>
                        </div>
                        <div class="panel-body">
                            <% if (AuthenticationUser.Membership.MemberType < (byte)MemberType.Enterprise)
                               {
                                   if (AuthenticationUser.Membership.MemberType == (byte)MemberType.FastIndividual)
                                   { %>
                            <a href="/Hesap/uyeliktipi/bireysel">Bireysel Üyeliğe Geçmek İstiyorum</a>
                            <br />
                            <a href="/Account/MemberType/Individual?type=fast">Kurumsal Üyeliğe Geçmek İstiyorum</a>
                            <% }
                                   else if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Individual)
                                   { %>
                            <a href="/Account/MemberType/InstitutionalStep">Kurumsal Üyeliğe Geçmek İstiyorum</a>
                            <% }
                               }
                               else
                               {
                                   if (!Model.hasPacket)
                                   { %>
                            <span class="text-bold"><%=Model.PacketDescription %> </span>üyesisiniz.
                            <br />
                              Paket satın almak için <a href="/account/advert/notlimitedaccess">tıklayınız.</a><br />
                        <%--      Paketinizi yükseltmek için <a href="/">tıklayınız.</a>--%>
                                          <%if (Model.OrderPacketEndDate != null) { %>
                            <br /><b>Paket Bitiş Tarihi:</b><%:String.Format("{0:d/M/yyyy}", Model.OrderPacketEndDate) %>
                     
                           <% } %>
                     
                            <%}
                                   else
                                   { %>
                            <span class="text-bold"><%:Model.PacketDescription %> </span>üyesisiniz.
                         <br />
                            

                                         <%if (Model.OrderPacketEndDate != null) { %>
                            <b>Paket Bitiş Tarihi:</b><%:String.Format("{0:M/d/yyyy}", Model.OrderPacketEndDate) %>
                     
                           <% }else{ %>
                              Paketinizi yükseltmek için <a href="/account/advert/notlimitedaccess">tıklayınız.</a>
                                    <%} %>
                             <% }
                               } %>

                            <hr>

                                         <div style="text-align:center;"><a class="btn btn-xs btn-success btn-add-on" href="<%=Model.StoreUrl%>" target="_blank">Firma Sayfam</a></div>
                            <br />
                            <div style="text-align:center;">
                            Sorularınız için:<b> 0212 255 7152</b> 
                             </div>
                            <hr>
                            <%
                              
                                foreach (var item in Model.PacketFeatures)
                                {
                                    var packetType = (EPacketFeatureType)item.FeatureType;
                                    switch (packetType)
                                   {
                                       case EPacketFeatureType.ProcessCount:
                                            %>
                                   <%:item.FeatureProcessCount.HasValue ? item.FeatureProcessCount.Value.ToString() : ""%>
                            <%:item.PacketFeatureTypeName %><br />
                                   <% break;
                                       case EPacketFeatureType.Active:%>
                                                <% if (item.FeatureActive.HasValue &&   item.FeatureActive.Value)
                                                { %>
                      
                                                 <%=item.PacketFeatureTypeName%><br />
                                               <% } %>
                                     <%break;
                                   case EPacketFeatureType.Content:
                                      %>
                                               <%:item.FeatureContent%>
                            <%=item.PacketFeatureTypeName%>   <br />
                            <% break;
                               default:
                               break;
                                   }
                                   
                                    %>
                                       
                                     
                              <%} %>
            
                          
                            <%--<hr>
              <b>
                Kalan Süre:
              </b>
              <br>
              2 ay 14 gün 
              <span class="text-muted">
                (22.06.2014)
              </span>
              <br>
              <a href="#">
                Uzatmak için buraya tıklayın
              </a>--%>
                        </div>
                    </div>
                    <% }
                      else
                      {%>                <div class="panel panel-primary">
                        <div class="panel-heading">
                            <h3 class="panel-title">
                                <a href="#">Üyelik Durumu
                </a>
                            </h3>
                        </div>
              <%if(AuthenticationUser.Membership.MemberType==(byte)MemberType.FastIndividual){ %>
                          <div class="panel-body" style="min-height: 100px;">
                                <b>Hızlı Üyesiniz</b>   
                          </div>
                    
                          <%}else{ %>
                        
                         
                        <div class="panel-body" style="min-height: 100px;">
                            <% if (AuthenticationUser.Membership.MemberType < (byte)MemberType.Enterprise)
                               {
                                   if (AuthenticationUser.Membership.MemberType == (byte)MemberType.FastIndividual)
                                   { %>
                            <a class="btn btn-primary btn-sm col-md-12" href="/Account/MemberType/Individual">Bireysel Üyeliğe Geçmek İstiyorum</a>
                            <br />
                            <a class="btn btn-success btn-sm col-md-12" style="margin-top: 10px;" href="/Account/MemberType/Individual?type=fast">Kurumsal Üyeliğe Geçmek İstiyorum</a>
                            <% }
                                   else if (AuthenticationUser.Membership.MemberType == (byte)MemberType.Individual)
                                   { %>
                            <a class="btn btn-success btn-sm col-md-12" href="/Account/MemberType/InstitutionalStep">Kurumsal Üyeliğe Geçmek İstiyorum</a>
                            <% }
                               }
                               else
                               {
                                   %>
                              <span class="text-bold"><%=Model.PacketDescription%> </span>üyesisiniz.
                    Paketinizi yükseltmek için <a href="account/advert/notlimitedaccess">tıklayınız.</a>
                            <%
                                   if (!Model.hasPacket)
                                   { %>
                          
                                          <%if (Model.OrderPacketEndDate != null) { %>
                            <b>Paket Bitiş Tarihi:</b><%:String.Format("{0:M/d/yyyy}", Model.OrderPacketEndDate) %>
                           <% } %>
                     
                            <%}
                                   else
                                   { %>
                            <span class="text-bold"><%=Model.PacketDescription %> </span>üyesisiniz.
                                          <%if (Model.OrderPacketEndDate != null) { %>
                            <b>Paket Bitiş Tarihi:</b><%:String.Format("{0:M/d/yyyy}", Model.OrderPacketEndDate) %>
                           <% } %>
                   
                            <% }
                               } %>
                        </div>
             
                          <%} %>
                            </div>
                  
              <%} %>


                <%if(Model.ProfileFillRate.ProfilUpdateLink.Count>0){ %>
               <div class="alert alert-info" style="">
                        <div class="progress progress-striped active">
                            <div class="progress-bar" role="progressbar" aria-valuenow="<%:Model.ProfileFillRate.ProfileRate  %>"
                                aria-valuemin="0" aria-valuemax="100" style="width:<%:Model.ProfileFillRate.ProfileRate%>%">
                                <span class="sr-only"><%:Model.ProfileFillRate.ProfileRate %>% Complete
                </span>
                            </div>
                        </div>
                        <strong>Profilinizin tamamlanma oranı %<%:Model.ProfileFillRate.ProfileRate  %>
                         </strong>
                        <br>
                        Profilinizin eksiksiz ve tam olarak doldurulmuş olması
            müşterilerinize vereceğiniz güvenin artmasında önemli rol
            oynadığından bu bilgileri dikkatli bir şekilde doldurmanızı
            öneririz.Profilinizi daha iyi hale getirmek için 
                 
                   <a href="<%:Model.ProfileFillRate.ProfilUpdateLink.First() %>">tıklayınız</a>
                     </div>
                   <%} %>
                  
    </div>


</asp:Content>
