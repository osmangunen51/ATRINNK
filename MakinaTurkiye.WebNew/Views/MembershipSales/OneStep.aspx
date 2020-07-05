﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<PacketViewModel>" %>

<%@ Import Namespace="NeoSistem.MakinaTurkiye.Web.App_Helper" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">

        function Check() {
            if ($('#PacketId').val() == '') {
                alert('Bir sonraki adıma geçmek için lütfen üyelik paket seçiniz.')
                return false;
            }
            else {
                return true;
            }
        }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="row" style="margin-top:10px;">
            <div class="col-xs-12">
                <div class="page-header mt0">
                    <h4 class="mt0">
                        <span class="glyphicon glyphicon-screenshot"></span>Kurumsal Üyelik Paketleri
                    </h4>
                </div>
            </div>
                       <%if(Model.LastPageAdvertAdd==true){ %>
                <div class="col-md-12" >
                      <div class="alert alert-info">
                        Üyelik avantajlarımızdan yararlanmak için paket satın alın, satışlarınızı arttırın. 
                            </div>
                          <%} %>
            <div class="col-md-9">
                <div class="row">
            <% foreach (var item in Model.PacketItems.OrderBy(p => p.PacketOrder))
               { %>
            <%using (Html.BeginForm("OneStep", "MembershipSales", FormMethod.Post))
              {%>
            <div class="col-md-4 col-sm-12" >
                <div class="panel panel-mt text-center" >
                     <%Response.Write("<div class='panel-heading' style='background:"+item.HeaderColor+"!important'>"); %>
                        <h4 class="m0">
                            <%=item.PacketName %>
                                               <%if(item.StarNumber==null || item.StarNumber==0){ %>
                            <small class="text-warning"><span class="glyphicon glyphicon-star"></span></small>
                             <%}else{
                                   for (int i = 0; i < item.StarNumber; i++)
                                   {
                                       %>
                                  <small class="text-warning"><span class="glyphicon glyphicon-star"></span></small>                            
                                   <%}
                                   %>
                                
                             <%} %>
                        </h4>
                    </div>
                   <%Response.Write("<div class='panel-body' style='background-color:"+item.PacketColor+";'1>"); %>
                    
                        <b class="text-lg">
                            <%--<i class="fa fa-turkish-lira"></i>--%>
                           <%-- <%:ComboHelper.FistPacketPrice(item.PacketName)%>--%>
                           <%  decimal packetDayPrice;
                                packetDayPrice = (decimal)(item.PacketPrice / item.PacketDay);
                                
                                 %>
                            <%:packetDayPrice.ToString("N") %>  <i itemprop="priceCurrency" class="fa fa-turkish-lira"></i> / Gün
             <%--              <%if (item.PacketName.Equals("Limitli Paket"))
                             { %>
                                1,34  <i class="fa fa-turkish-lira"></i> / Gün
                           <%}
                           else if (item.PacketName.Equals("Gold Plus Paket"))
                             { %>
                                  4,55  <i class="fa fa-turkish-lira"></i> / Gün
                                 
                           <% }
                             else if (item.PacketName.Equals("Gold Paket"))
                             { %>
                                  2,10  <i class="fa fa-turkish-lira"></i> / Gün
                             <%} %>--%>

                        </b><span class="text-md text-italic">den başlayan fiyatlarla</span>
               </div>
                    <ul class="list-group">
                        <% foreach (var itemPFTItems in Model.PacketFeatureTypeItems.OrderBy(c => c.PacketFeatureTypeOrder))
                           { %>

                        <% var packetFeature = Model.PacketFeatureItems.FirstOrDefault(c => c.PacketFeatureTypeId == itemPFTItems.PacketFeatureTypeId && c.PacketId == item.PacketId); %>
                       <%if (packetFeature != null)
                         { %>
                       
                        <% var packetType = (EPacketFeatureType)packetFeature.FeatureType; %>
                        <%switch (packetType)
                          {
                              case EPacketFeatureType.ProcessCount:%>
                                <%Response.Write("<li class='list-group-item' style='background-color:"+item.PacketColor+";'>"); %>
                        
                            <%:packetFeature.FeatureProcessCount.HasValue ? packetFeature.FeatureProcessCount.Value.ToString() : ""%>
                            <%=itemPFTItems.PacketFeatureTypeName%>
                        </li>
                        <% break;
                              case EPacketFeatureType.Active:%>
                        <% if (packetFeature.FeatureActive.HasValue && packetFeature.FeatureActive.Value)
                           { %>
                  <%Response.Write("<li class='list-group-item' style='background-color:"+item.PacketColor+";'>"); %>
                            <%=itemPFTItems.PacketFeatureTypeName%></li>
                        <% } %>
                        <% break;
                              case EPacketFeatureType.Content:%>
                      <%Response.Write("<li class='list-group-item' style='background-color:"+item.PacketColor+";'>"); %>
                            <%:packetFeature.FeatureContent%>
                            <%=itemPFTItems.PacketFeatureTypeName%>
                        </li>
                        <% break;
                              default:
                           break;
                          } %>
                              <%} %>
                        <% } %>
                        <%Response.Write("<li class='list-group-item' style='background-color:"+item.PacketColor+";'>"); %>
                       
                              <h3><%:item.PacketPrice.ToString("0")%> <i itemprop="priceCurrency" class="fa fa-turkish-lira"></i> /         
                             <%if(item.PacketDay==365){ %>
                                Yıl
                               <%}else if(item.PacketDay%30==0){ %>
                                   <%:item.PacketDay/30 %> Ay
                               <%}else{ %>
                                <%:item.PacketDay %> Gün
                               <%} %>
                                </h3>
                            <%:Html.Hidden("PacketId",item.PacketId) %>
              <%--              <%=Html.DropDownList("PacketId", ComboHelper.GetLimitliPacketPrice(item.PacketName), new { @class = "form-control" })%>--%>
                        </li>
                        <%Response.Write("<li class='list-group-item' style='background-color:"+item.PacketColor+";'>"); %>
                            <button type="submit" class="btn btn-success btn-block">
                                Satın Al</button>
                        </li>
                    </ul>
                             
                </div>
            </div>
            <% } %>
            <% } %>
                    </div>
                <%if(Model.LastPageAdvertAdd==true && Model.AnyOrder==false){ %>
            <div class="row col-md-12">
                <div style="text-align:center;">
            <a class="btn btn-primary" href="/MemberShipSales/DiscountPackets">3 aylık indirimli deneme paketinden yararlanmak istiyorum</a>
                    </div>
            </div>
                    <%} %>
                </div>
            <%--Page Helper Begin--%>
            <div class="col-sm-12 col-md-3 ">
                <div class="panel panel-mt2">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon-question-sign"></span>Sayfa Yardımı
                    </div>
                    <div class="panel-body">
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="#">Silver üyelik ile Gold üyelik
                            arasındaki fark nedir? </a>
                        <br>
                        <br>
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="#">Firma profilimde kaç adet
                            ilan yayınlayabilirim? </a>
                        <br>
                        <br>
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="#">Üyelik paketi satın almanın
                            avantajları nelerdir? </a>
                        <br>
                        <br>
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="#">İlanımı anasayfa vitrinine
                            nasıl eklerim? </a>
                        <br>
                        <br>
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="#">Üyelik paketleri ile ilgili
                            sık sorulanlar </a>
                        <br>
                        <br>
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="#">Hangi üyelik paketi benim
                            için daha uygun? </a>
                        <br>
                        <br>
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="#">İlanımı anasayfa vitrinine
                            nasıl eklerim? </a>
                    </div>
                </div>
            </div>
            <%--Page Helper End--%>
            <span class="clearfix"></span>
            <hr>
            <%--Footer Info Begin--%>
           <%-- <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Quam imperdiet
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Porttitor lorem scelerisque
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Donec vitae dui tempus mattis
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Fusce tempor orci
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Sed in urna quis nulla
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Lorem ipsum dolor sit
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Vivamus iaculis nisl sit
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Donec vitae dui tempus mattis
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Nulla ultrices ullamcorper
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Consectetur adipiscing elit
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Fusce tempor orci
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Quam imperdiet
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Consectetur adipiscing elit
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Vivamus iaculis nisl sit
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Consectetur adipiscing elit
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Fusce tempor orci
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Vulputate volutpat
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Quam imperdiet
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Fusce tempor orci
            </div>
            <div class="col-xs-6 col-md-3">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Lorem ipsum dolor sit
            </div>--%>
            <%--Footer Info End--%>
            <span class="clearfix">
                <br>
                <br>
            </span>
        </div>
    </div>
</asp:Content>
