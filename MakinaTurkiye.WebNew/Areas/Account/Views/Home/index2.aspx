<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master"
    Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.AccountModels.MTAccountHomeModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function CommentDelete(id) {
            if (confirm('Yorum Silmek İstediğinize Eminmisiniz?')) {
                $.ajax({
                    url: '/Account/Home/CommentDelete',
                    data: { productCommentId: id },
                    type: 'post',
                    success: function (data) {
                        if (data) {
                            alert("Yorumunuz Kaldırıldı.");
                            $("#row" + id).hide();
                        }
                    }
                });
            }
        }
        function ProductCommentPaging(p) {
            $.ajax({
                url: '/Account/home/commentpaging',
                data: { page: p },
                type: 'post',
                success: function (data) {
                    if (data) {

                        $("#data").html(data);
                    }
                }
            });

        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenuModel)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>
                Hesabım
            </h4>
        </div>
    </div>

    <div class="row">

        <%=Html.RenderHtmlPartial("_AccountHomeCenter",Model.AccountHomeCenterCenterModel) %>

        <div class="col-md-3 col-xs-12 col-sm-12">
            <%if (Model.AccountHomeCenterCenterModel.MemberType != (byte)MemberType.FastIndividual && Model.AccountHomeCenterCenterModel.MemberType != (byte)MemberType.Individual)
                { %>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <a href="#">Üyelik Paketi  </a>
                    </h3>
                </div>
                <div class="panel-body">
                    <% if (Model.AccountHomeCenterCenterModel.MemberType < (byte)MemberType.Enterprise)
                        {
                            if (Model.AccountHomeCenterCenterModel.MemberType == (byte)MemberType.FastIndividual)
                            { %>
                    <a href="/Hesap/uyeliktipi/bireysel">Bireysel Üyeliğe Geçmek İstiyorum</a>
                    <br />
                    <a href="/Account/MemberType/Individual?type=fast">Kurumsal Üyeliğe Geçmek İstiyorum</a>
                    <% }
                        else if (Model.AccountHomeCenterCenterModel.MemberType == (byte)MemberType.Individual)
                        { %>
                    <a href="/Account/MemberType/InstitutionalStep">Kurumsal Üyeliğe Geçmek İstiyorum</a>
                    <% }
                        }
                        else
                        {
                            if (!Model.AccountHomeCenterCenterModel.HasPacket)
                            { %>
                    <span class="text-bold"><%=Model.PacketDescription %> </span>üyesisiniz.
                            <br />
                    Paket satın almak için <a href="/account/advert/notlimitedaccess">tıklayınız.</a><br />
                    <%--      Paketinizi yükseltmek için <a href="/">tıklayınız.</a>--%>


                    <%}
                        else
                        { %>
                    <span class="text-bold"><%:Model.PacketDescription %> </span>üyesisiniz.
                         <br />


                    <%if (Model.OrderPacketEndDate != null)
                        { %>
                    <b>Paket Bitiş Tarihi:</b><%:Model.OrderPacketEndDate.Value.ToString("D") %>


                    <% }
                        else
                        { %>
                              Paketinizi yükseltmek için <a href="/account/advert/notlimitedaccess">tıklayınız.</a>
                    <%} %>
                    <% }
                        } %>

                    <hr>
                    <%if (Model.AccountHomeCenterCenterModel.OrderPriceCheck == false)
                        { %>
                    <div style="text-align: center;"><a class="btn btn-xs btn-success btn-add-on" href="/MemberShipSales/BeforePayCreditCard">Kalan Ödemelerim</a></div>
                    <%} %>
                    <div style="text-align: center;"><a class="btn btn-xs btn-success btn-add-on" href="<%=Model.StoreUrl%>" target="_blank">Firma Sayfam</a></div>
                    <br />
                    <div style="text-align: center;">
                        Sorularınız için:<b>0850 511 1 666</b>
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
                    <% if (item.FeatureActive.HasValue && item.FeatureActive.Value)
                        { %>

                    <%=item.PacketFeatureTypeName%><br />
                    <% } %>
                    <%break;
                        case EPacketFeatureType.Content:
                    %>
                    <%:item.FeatureContent%>
                    <%=item.PacketFeatureTypeName%>
                    <br />
                    <% break;
                            default:
                                break;
                        }

                    %>


                    <%} %>
                    <hr />
                    <%if (Model.PacketFinishDay < 30 && Model.AccountHomeCenterCenterModel.HasPacket)
                        {%>
                    <div>
                        <a class="btn btn-warning" href="/MemberShipSales/ThreeStepPre/29">Paketimi Yenile</a>
                    </div>

                    <% } %>


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
                {%>
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <a href="#">Üyelik Durumu</a>
                    </h3>
                </div>
                <%if (Model.AccountHomeCenterCenterModel.MemberType == (byte)MemberType.FastIndividual)
                    { %>
                <div class="panel-body" style="min-height: 100px;">
                    <b>Hızlı Üyesiniz</b>
                </div>

                <%}
                    else
                    { %>


                <div class="panel-body" style="min-height: 100px;">
                    <% if (Model.AccountHomeCenterCenterModel.MemberType < (byte)MemberType.Enterprise)
                        {
                            if (Model.AccountHomeCenterCenterModel.MemberType == (byte)MemberType.FastIndividual)
                            { %>
                    <a class="btn btn-primary btn-sm col-md-12" href="/Account/MemberType/Individual">Bireysel Üyeliğe Geçmek İstiyorum</a>
                    <br />
                    <a class="btn btn-success btn-sm col-md-12" style="margin-top: 10px;" href="/Account/MemberType/Individual?type=fast">Kurumsal Üyeliğe Geçmek İstiyorum</a>
                    <% }
                        else if (Model.AccountHomeCenterCenterModel.MemberType == (byte)MemberType.Individual)
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
                        if (!Model.AccountHomeCenterCenterModel.HasPacket)
                        { %>

                    <%if (Model.OrderPacketEndDate != null)
                        { %>
                    <b>Paket Bitiş Tarihi:</b><%:String.Format("{0:M/d/yyyy}", Model.OrderPacketEndDate) %>
                    <% } %>

                    <%}
                        else
                        { %>
                    <span class="text-bold"><%=Model.PacketDescription %> </span>üyesisiniz.
                                          <%if (Model.OrderPacketEndDate != null)
                                              { %>
                    <b>Paket Bitiş Tarihi:</b><%:String.Format("{0:M/d/yyyy}", Model.OrderPacketEndDate) %>
                    <% } %>

                    <% }
                        } %>
                </div>

                <%} %>
            </div>

            <%} %>

            <%if (Model.ProfileFillRate.ProfilUpdateLink.Count > 0)
                { %>
            <div class="alert alert-info" style="">
                <div class="progress progress-striped active">
                    <div class="progress-bar" role="progressbar" aria-valuenow="<%:Model.ProfileFillRate.ProfileRate  %>"
                        aria-valuemin="0" aria-valuemax="100" style="width: <%:Model.ProfileFillRate.ProfileRate%>%">
                        <span class="sr-only"><%:Model.ProfileFillRate.ProfileRate %>% Complete</span>
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
    </div>


</asp:Content>
