<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTStoreOtherProductModel>"  %>

<%if(Model.ProductItemModels.Count>0) 
    {%>
<header style="margin-top:0px;">
            <div class="menu">
              <div  class="urun-bilgisi">
                <div class="panel urun-alan" style="margin-bottom:0;">
               <%--   <div class="panel-heading">
                    <div class="urunler-link">
                      <div class="row">
                        <div class="col-md-8 col-xs-9 col-xs-8">
                          <a href="#">Satıcının Diğer Ürünleri</a>
                        </div>
                        <div class="col-md-4 col-xs-3 col-xs-4">
                          <div class="text-right">
                            <a href="">Tümü</a>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>--%>
                    <div class="panel-body" style="margin-top:0px;padding-top:0px;padding-bottom:0px;">
                            <%  foreach (var item in Model.ProductItemModels.Take(5))
                        { %>

                      <div class="urunler-item">
                        <div class="row">
                          <div class="col-md-6 col-xs-6">
                            <div class="firma-logo">
                              <a href="<%:item.ProductUrl %>"><img alt="<%:item.ProductName %>" src="<%:item.SmallPicturePah.Replace("100x75","200x150") %>"></a>
                            </div>
                          </div>
                          <div class="col-md-6 col-xs-6">
                            <div class="urun-baslik">
                              <a href="<%:item.ProductUrl %>" style="font-weight:normal;"><%:item.ProductName %></a> <br>
                              <%--<span><%:item.ModelName %></span>--%>
                            </div>
                          </div>
                        </div>
                      </div>
                        <%} %>
               
                    </div>
                </div>
              </div>
            </div>
          </header>
          <script type="text/javascript">
              // Create a clone of the menu, right next to original.
              $('.menu').addClass('original').clone().insertAfter('.menu').addClass('cloned').css('position', 'fixed').css('top', '85').css('margin-top', '85').css('z-index', '500').removeClass('original').hide();

              scrollIntervalID = setInterval(stickIt, 10);


              function stickIt() {

                  var orgElementPos = $('.original').offset();
                  orgElementTop = orgElementPos.top;

                  if ($(window).scrollTop() >= (orgElementTop)) {
                      // scrolled past the original position; now only show the cloned, sticky element.

                      // Cloned element should always have same left position and width as original element.
                      orgElement = $('.original');
                      coordsOrgElement = orgElement.offset();
                      leftOrgElement = coordsOrgElement.left;
                      widthOrgElement = orgElement.css('width');
                      $('.cloned').css('left', leftOrgElement + 'px').css('top', 0).css('width', widthOrgElement).show();
                      $('.original').css('visibility', 'hidden');
                  } else {
                      // not scrolled past the menu; only show the original menu.
                      $('.cloned').hide();
                      $('.original').css('visibility', 'visible');
                  }
              }
          </script>        
     




<%} %>
