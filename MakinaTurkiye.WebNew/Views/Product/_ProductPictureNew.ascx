<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTProductPictureModel>>" %>

<%if (Model.Count > 0)
    {
        bool first = true;
        int imgindex = 0;
        int miniCounter = 0;
%>

<div class="resim-urun">
    <div id="myCarousel" class="carousel slide" data-ride="carousel">
        <div class="carousel-inner product-popup-gallery">
            <%foreach (var item in Model)
                {
                    string active = "";
                    if (first)
                    {
                        active = "active";
                        first = false;
                    }
            %>
            <div class="item <%=active %>">
                <img src="<%=item.LargePath %>" title="<%=item.ProductName %>" alt="<%=item.ProductName %>" />
                <a href="<%:item.MegaPicturePath %>" title="<%=item.ProductName %>" class="img-product-mega-link" data-title="<%:item.ProductName %>">
                
                    <img alt="Büyük resimi göster" class="res-buyut" src="/Content/V2/images/resim-buyult.png">
                </a>
            </div>
            <%}
                first = true; %>
        </div>
        <a class="left carousel-control" href="#myCarousel" data-slide="prev">‹</a>
        <a class="right carousel-control" href="#myCarousel" data-slide="next">›</a>
    </div>
    <div id="kresim" class="carousel slide  " data-ride="carousel" data-interval="false" style="margin-bottom: 10px;">
        <a class="left carousel-control" href="#kresim" data-slide="prev">‹</a>
        <a class="right carousel-control" href="#kresim" data-slide="next">›</a>
        <div class="carousel-inner" style="padding-right: 25px; margin-left: -3px;">

            <%foreach (var item in Model)
                {
                    miniCounter += 1;
                    string active = "";
                    if (first)
                    {
                        active = "active";
                        first = false;
                    }
                    if (miniCounter == 1)
                    {
            %>
            <div class="item <%=active %>">
                <div class="flex-row flex-nowrap">
                    <%
                        }

                    %>

                    <div class="flex-md-3 flex-lg-3 flex-xs-3 flex-sm-3" style="padding: 0px 2px;">
                        <a data-target="#myCarousel" data-slide-to="<%:imgindex %>" href="#" title="<%: item.ProductName %>  " style="width: 100%;">
                            <img title="<%=item.ProductName %>" src="<%:item.SmallPath %>" alt="<%: item.ProductName %> " alt="<%:item.ProductName %> - <%:imgindex %>" style="min-width: 70px;">
                        </a>
                    </div>

                    <% if (miniCounter == 4)
                        {
                            miniCounter = 0;
                    %>
                </div>
            </div>
            <%
                } %>
            <% 

                    imgindex += 1;
                }
                if (miniCounter > 0 && miniCounter < 4)
                {
            %>
        </div>
    </div>
    <% 
        }
    %>
</div>
</div>
</div>

<%}
    else
    { %>
<img src="//s.makinaturkiye.com/no-image.png" alt="Resim bulunamadı" title="Resim bulunamadı" />
<%} %>

<%--
<%if (Model.Count > 0)
    {%>
 <div class="resim-urun">
                    <div id="myCarousel" class="carousel slide" data-ride="carousel">
                      <!-- Wrapper for slides -->
                      <div class="carousel-inner">
                              <% int a=0;
                                  for (int i = 0; i < Model.Count; i++)
                      {    
                                      if(i==0){%>
                               <div class="item active">
                          <%}else{%>
                                   <div class="item">
                                   <%} %>
                             
                    <img src="<%:Model[i].LargePath %>" alt="<%:Model[i].ProductName %> <%:i + 1 %>"/>
                  <%--        <a href="<%:Model[i].LargePath %>" data-lightbox=" " data-title="  ">
                            <img class="res-buyut" src="/Content/V2/images/buyut-ok.png" alt="">
                          </a>--%>
<%--               </div>
                        <%} %>
                      </div>
                      <!-- buyuk resim oklar -->
                      <a class="left carousel-control" href="#myCarousel" data-slide="prev">‹</a>
                      <a class="right carousel-control" href="#myCarousel" data-slide="next">›</a>
                    </div>
                    <!-- urun kucuk resim -->
                    <div id="kresim" class="carousel slide" data-ride="carousel" data-interval="false">
                      <!-- kucuk resim oklar -->
                      <a class="left carousel-control" href="#kresim" data-slide="prev">‹</a>
                      <a class="right carousel-control" href="#kresim" data-slide="next">›</a>
                      <!-- Kucuk Resımler -->
                      <div class="carousel-inner">
                          <%  decimal pageCount = Math.Ceiling((decimal)Model.Count / (decimal)5);
                              for (int i = 0; i <= pageCount; i++)
                              {
                                  int pageSize = 5;
                                  int takeSize = i+1 * pageSize - pageSize;
                                  if (i+1 == 1)
                                  {%>
                                      <div class="item active">
                                  <%}
                                  else { %>
                                             <div class="item">
                                          <%}%> <div class="row">
                                                 <%
                                  foreach (var item in Model.Skip(takeSize).Take(pageSize))
                                  {%>
                                      
                                 <div class="col-md-2 col-5 col-xs-2 col-sm-2">
                              <a data-target="#myCarousel" data-slide-to="<%:i %>" href="#" title="<%: Model[i].ProductName %> <%:i+1 %>"><img src="<%:Model[i].SmallPath %>"  alt="<%: Model[i].ProductName %> <%:i+1 %>"></a>
                            </div>
                                  <%}%>
                               </div></div>
                            <%  } %>
                 
                  </div>
                        </div>
     </div>
<%} %>--%>
