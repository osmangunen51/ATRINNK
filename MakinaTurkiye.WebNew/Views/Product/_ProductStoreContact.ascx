<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProductContactModel>" %>

<div class="modal fade" id="PostCommentsModal" tabindex="-1"
    role="dialog" aria-labelledby="helpModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header productMessageModelHeader">
                <div class="urun-iletisim-block">
                    <div class="urun-iletisim-heading">
                        <div class="urun-iletisim-firma-logo">
                            <a href="<%:Model.StoreUrl %>">
                                <img src="<%=Model.StoreModel.StoreLogoPath %>" width="99" height="66">
                            </a>
                        </div>
                        <div class="urun-iletisim-firma-adi">
                            <h2><b><%=Model.StoreModel.StoreName %></b></h2>
                            <div class="urun-iletisim-yetkili-adi">
                                <span>Yetkili: <%=Model.StoreModel.MemberName + " " + Model.StoreModel.MemberSurname%> </span>
                            </div>
                        </div>
                        <div style="float: right">
                            <button type="button"
                                style="background: transparent; border: 0px;"
                                data-dismiss="modal">
                                <i class="fa fa-times" style="font-size: 24px; color: #ccc"></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-body" style="padding-top: 5px;">
                <ul class="nav nav-tabs productmessage-tab">
                    <li class="active"><a data-toggle="tab" href="#contact">İletişim Bilgileri</a></li>
                    <%if ( AuthenticationUser.CurrentUser.Membership==null || AuthenticationUser.CurrentUser.Membership.MainPartyId == 0) {%>
                                    <li ><a data-toggle="tab" href="#message">Mesaj Gönder</a></li>
                    <% } %>
        
                </ul>
                <div class="tab-content">
                    <div id="contact" class="tab-pane fade in active">
                        <%=Html.RenderHtmlPartial("_ProductStorePhones",Model) %>
                    </div>
                       <%if (AuthenticationUser.CurrentUser.Membership == null || AuthenticationUser.CurrentUser.Membership.MainPartyId == 0)
                                 {%>
                    <div id="message" class="tab-pane fade in ">
                        <%=Html.RenderHtmlPartial("_ProductStoreMessageNew", Model.StoreMessage) %>
                    </div>
                    <%} %>


                </div>
            </div>

        </div>
    </div>
</div>
