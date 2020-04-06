<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTProductStoreModel>" %>

<%if(!string.IsNullOrEmpty(Model.StoreName))
    {%>
<div class="col-sm-4 col-md-3">
    <div class="product-store-information" >
        <input id="hiddenProductNo" value="<%:Model.ProductNo %>" type="hidden" />
        <input id="hiddenMemberNo" value="<%:Model.MemberNo %>" type="hidden" />
        <div class="product-store-information__header">
            <div class="product-store-information__title">
                <div style="font-size:20px;">SATICI FİRMA BİLGİLERİ</div>
            </div>
        </div>
        <div class="product-store-information__brand-image" >
            <% if (Model.MainPartyId>0)
                { %>
            <a    href='<%=Model.StoreUrl %>'>
                <img  src="<%=Model.StoreLogoPath%>" class="img-thumbnail" alt="<%= Model.StoreName %>" />
            </a>
            <% } %>
        </div>
        <div class="product-store-information__content">
            <div class="product-store-information__brand-name" >
                <% if (Model.MainPartyId>0)
                    { %>
                <a href="<%= Model.StoreUrl %>">
                    <%= Model.TruncateStoreName %>
                </a>
                <% } %>
            </div>
            <div class="product-store-information__follow">
                <%string removeFavoriStoreCss = "";  %>
                <%string addFavoriStoreCss = "";  %>
                <% if (Model.IsFavoriteStore)
                   { %>
                <% addFavoriStoreCss = addFavoriStoreCss + "display:none;"; %>
                <% }
                   else
                   { %>
                <% removeFavoriStoreCss = removeFavoriStoreCss + "display:none;"; %>
                <% } %>
                <%if(AuthenticationUser.Membership!=null){ %>
                 <a id="aRemoveFavoriteStore" href="/Product/RemoveFavoriteStoreProduct/<%:Model.MainPartyId %>"
                    style="<%=removeFavoriStoreCss %>" class="product-store-information__follow-button btn btn-xs btn-success"><span class="glyphicon glyphicon-ok"></span>&nbsp;Takibi Bırak </a>
                <a class="product-store-information__follow-button btn btn-xs btn-info" id="aAddFavoriteStore"  href="/Product/AddFavoriteStoreProduct/<%:Model.MainPartyId %>"><span class='glyphicon glyphicon-bookmark'></span>&nbsp;Takip Et</a>
                <%}else{ %>
                <a class="product-store-information__follow-button btn btn-xs btn-info" data-toggle="modal" data-target=".bs-example-modal-sm" id="aAddFavoriteStore"  onclick="StoreFallowError()"><span class='glyphicon glyphicon-bookmark'></span>&nbsp;Takip Et</a>

                <div class="modal fade bs-example-modal-sm" id="myModal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
                      <div class="modal-dialog" role="document">
                        <div class="modal-content">
                          <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" id="myModalLabel">Bilgi</h4>
                          </div>
                          <div class="modal-body" style="font-size:16px;">
                            Firmayı takip etmek için giriş yapmalısınız.
                          </div>
                          <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Kapat</button>
                            <a  class="btn btn-primary" href="/uyelik/kullanicigirisi" style="color:#fff;">Giriş Yap</a>
                          </div>
                        </div>
                      </div>
                    </div>

               
                <%} %>

        <%--        <a id="aAddFavoriteStore" onclick="AddFavoriteStore('<%=Model.MainPartyId %>','<%=Model.StoreName %>');"
                    style="<%=addFavoriStoreCss %>" class="product-store-information__follow-button btn btn-xs btn-info"><span class="glyphicon glyphicon-bookmark"></span>&nbsp;Takip Et </a>--%>
            </div>
            <ul class="product-store-information__phone-list">
                <%
                    foreach (var item in Model.PhoneModels)
                    {
                        if (item.PhoneType == PhoneType.Fax)
                        { %>
                <li class="product-store-information__phone-list-item clearfix">
                    <div class="product-store-information__phone-list-ikon">
                        <span class="glyphicon glyphicon-inbox"></span>
                    </div>
                    <span class="product-store-information__phone-list-value">
                  <%= item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber%>
                    </span>
                </li>
                <% }
                   
                        else if (item.PhoneType == PhoneType.Gsm)
                        { %>
                <li class="product-store-information__phone-list-item clearfix">
                    <div class="product-store-information__phone-list-ikon">
                        <span class="glyphicon glyphicon-phone text-info"></span>
                    </div>
                    <span class="product-store-information__phone-list-value"> <div class="hidden-xs"><%= item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber%></div>
                        <div class="visible-xs">
                            <a style="color:#0000FF; font-weight:normal; font-size:12px; text-decoration:underline"  href="tel:<%= item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber%>"><%= item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber%></a>
                        </div></span>
                </li>
                <% }
                   else if(item.PhoneType==PhoneType.Whatsapp)
                        {%>
                             <li class="product-store-information__phone-list-item clearfix">
                    <div class="product-store-information__phone-list-ikon">
                            <img src="/Content/SocialIcon/wp-24.png" alt="whatsapp logo" style="height:20px;" />
                    </div>
                    <span class="product-store-information__phone-list-value"> 
                        <%string wgsmUrl=item.PhoneCulture.Replace("+","")+item.PhoneAreaCode+item.PhoneNumber.Replace(" ",""); %>
                    
                            <a style='color:#0000FF; font-weight:normal; font-size:12px; text-decoration:underline' href='https://api.whatsapp.com/send?phone=<%:wgsmUrl %>&text=makinaturkiye.com <%:Model.ProductName %> ürünü hakkında' rel='external' target='_blank'>Whatsapp Hattı</a>    
                        
                     
                </li>
                        <%}
                        else
                        { %>
                <li class="product-store-information__phone-list-item clearfix">
                    <div class="product-store-information__phone-list-ikon">
                        <span class="glyphicon glyphicon-phone-alt text-success"></span>
                    </div>
                    <span class="product-store-information__phone-list-value" > <div class="hidden-xs"><%= item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber%></div>
                        <div class="visible-xs">
                            <a style="color:#0000FF; font-weight:normal; font-size:12px; text-decoration:underline" href="tel:<%= item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber%>"><%= item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber%></a>
                        </div></span>
                </li>
                <%}
                    } %>
            </ul>
            <div class="product-store-information__address">
                <p>
                    <b class="text-muted">Yetkili: </b>
                    <%= Model.MemberName + " " + Model.MemberSurname%>
                </p>

                <p  >
                    <%if (Model.LocalityName == "" || Model.CityName == "")
                      {%>
                    <%=Model.LocalityName + " / " + Model.CityName + " / " + Model.CountryName%> 
                    <% }
                      else
                      { %>
                    <%=Model.LocalityName + " / " + Model.CityName + " / " + Model.CountryName%>
                    <%} %> 
                </p>

            </div>
        </div>
        <div class="product-store-information__footer">
            <% if (Model.MainPartyId>0)
               {%>
                    <a href="<%= Model.StoreAllProductUrl%>" class="product-store-information__all-link"><span class="glyphicon glyphicon-list-alt"></span>&nbsp;Tüm İlanları </a>
            <% } %>
        </div>
    </div>
</div>
<%} %>
