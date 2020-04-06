<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<AdvertViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Paket Yükselt
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <!-- Başlangıç -->
            <div class="widget">
                <div class="widget-line">
                    <div class="padtb">
                        <div class="widgetcontent ">
                            <div class="singlerow">
                                <img src="/Content/V2/images/market-icon.png" />
                                <h3 class="header-red">
                                    <%if (AuthenticationUser.Membership.MemberType == (byte)MemberType.FastIndividual)
                                        { %>
                   Mevcut üyeliğiniz bu işlemi yapmaya izin vermiyor. 
              <%}
                  else
                  {
              %> 
             Üyelik Paketi Al !
               <% }%>
                                </h3>
                            </div>
                            <div class="pad-15">
                                <p class="font-large">
                                    Makina Türkiye.com’dan üyelik satın alarak 17 sektörde 50 bin makina arasında yerinizi alın!
                                </p>
                                <p class="font-xtralarge">
                                    <strong>İşletmeniz aylık 300 bin kişeye ulaşsın!</strong>
                                </p>
                                <p class="font-large">

                                    <a href="/MemberShipSales/ThreeStepPre/29">Üyelik Paketi Satın Almak İstiyorum</a>

                                </p>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="widget-line">
                    <!-- Reklam Görseli -->
                    <%--         <%if(Model.StoreOld==true){ %>
             <a href="/MemberShipSales/ThreeStepPre/31"><img class="responsiveimg padtb" src="/Content/V2/images/299.jpg" /></a>
            <%}else{ %>
                  <a href="/MemberShipSales/ThreeStepPre/29"><img class="responsiveimg padtb" src="/Content/V2/images/365.jpg" /></a>
            <%} %>--%>
                    <!-- Hemen Tıkla Görseli -->
                    <a href="/reklam-y-141615">
                        <img class="responsiveimg padtb" src="/Content/V2/images/hemen-tikla.png" /></a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
