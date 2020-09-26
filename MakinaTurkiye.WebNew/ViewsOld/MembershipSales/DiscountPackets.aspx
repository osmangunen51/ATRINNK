<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<PacketViewModel>" %>

<%@ Import Namespace="NeoSistem.MakinaTurkiye.Web.App_Helper" %>
<%--<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    İndirimli Paketler</asp:Content>--%>
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
                        <span class="glyphicon glyphicon-screenshot"></span>İndirimli Kurumsal Üyelik Paketleri
                    </h4>
                </div>
            </div>
            <div class="col-md-12">
                <div class="alert alert-info">
                    <div class="container" >

                        <div class="col-md-9">
                                <%Response.Write(Model.Description); %><%--<a class="btn btn-success" data-toggle="modal" data-target=".bs-example-modal-sm"  onclick="AddInfoForDemand()"> detaylı bilgi alınız</a> --%>   
                        </div>
                        <div class="col-md-3">
                            <a class="btn btn-primary" data-toggle="modal" data-target=".bs-example-modal-sm">Detaylı bilgi almak istiyorum>></a> 
                            </div>
                    </div>
                        
                
                     </div>
            
            </div>
            <div class="col-md-12">
            <% foreach (var item in Model.PacketItems.OrderBy(p => p.PacketOrder))
               { %>
            <%using (Html.BeginForm("OneStep", "MembershipSales", FormMethod.Post))
              {%>
         <div class="col-md-4 col-sm-12" >
                <div class="panel panel-mt text-center" >
                  <%Response.Write("<div class='panel-heading' style='background:"+item.HeaderColor+"!important'>"); %>
                         <h4 class='m0'>
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
                   <%Response.Write("<div class='panel-body' style='background-color:"+item.PacketColor+";'>"); %>
                    
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
        <button type="button" class="btn btn-primary"  id="SuccessDemand" style="display:none;" ></button>
                <div class="modal fade bs-example-modal-sm"  tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
                  <div class="modal-dialog modal-sm" role="document">
                    <div class="modal-content">
                        <div class="modal-header" style="font-size:15px;">
        <button type="button" class="close"  data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Ürün Bilgileri</h4>
      </div>
                      
                        <div class="modal-body" style="color:#484747; font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;">

                              
                            <div class="form-group">
                                <label class="col-md-3">Ürün Sayınız</label>
                                <div class="col-md-9"><input type="text" id="productNumberForInf" name="productNumber" class="form-control" /></div>
                            </div>
                            <div id="InfSuccess" style="color:#484747; display:none;">
                                 Bilgi alma talebiniz incelendikten sonra en kısa zamanda tarafınıza  bildirilicektir.
                            </div>
                     
                        </div>
                        <div class="modal-footer">
                             <button type="submit" class="btn btn-primary" onclick="AddInfoForDemand()"  >Gönder</button>
                        </div>
                        
                    </div>
                  </div>
                </div>
            <span class="clearfix">
                <br>
                <br>
            </span>
        </div>
    </div>
</asp:Content>
