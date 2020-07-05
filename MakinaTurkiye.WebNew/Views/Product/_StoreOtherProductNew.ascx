<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTStoreOtherProductModel>"  %>

<%if(Model.ProductItemModels.Count>0) 
    {%>
<header style="margin-top:0px;" class="hidden-xs">
            <div class="menu">
              <div  class="urun-bilgisi">

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
                    <div class="">
                            <%  foreach (var item in Model.ProductItemModels.Take(5))
                        { %>

                        <a href="<%:item.ProductUrl %>" class="saller-prd">
                            <div class="saller-prd__image">
                                <img  alt="<%:item.ProductName %>" src="<%:item.SmallPicturePah.Replace("100x75","200x150") %>">
                            </div>
                            <h5 class="saller-prd__title"><%:item.ProductName %></h5>
                        </a>
                        <%} %>
               
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
