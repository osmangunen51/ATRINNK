<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTHomeProductRelatedItem>>" %>

 
  <div class="owl-carousel overflowdouble2">
               <% 
                   int StepCounter = 0;
                   foreach (var populerAd in Model){ 
                       StepCounter+=1;
                       if (StepCounter == 1)
                       { 
                       %>
                            <div class="item double-item">
                       <%
                       }
                       
                       %>
            
               <div class="product-card-wide">
                  <a href="<%=populerAd.ProductUrl %>" class="product-image">
                    <img class="img-lazy-l" alt="<%:populerAd.ProductName %>"  src="/UserFiles/image-loading.png" data-src="<%:populerAd.PicturePath %>" title="<%:populerAd.ProductName %>"/>
                  </a> 
                   <div class="text-container">
       <span class="product-details">
                        <a class="product-details" title="<%:populerAd.ProductName %>" href="<%=populerAd.ProductUrl %>"> <%= populerAd.ProductName %></a>
                    </span>
                   <span style="font-size:17px; font-weight:600"> 
                            <%if(populerAd.ProductPrice!=""){ %>
                        <i class="<%:populerAd.CurrencyCss %>"></i>
                       <%} %>
             <%=populerAd.ProductPrice %>  
                  
                       </span>
                       <span style="display:none">
                           <%:populerAd.BrandName %>
                           <%:populerAd.ModelName %>
                       </span>
<%--                  <a href="<%=populerAd.SameCategoryUrl %>"  title="<%=populerAd.CategoryName %>" class="product-category similar"><%=populerAd.CategoryName %> <i class="fa fa-chevron-right"></i></a> --%>
                   </div>
             </div>
           
              <%
                       if (StepCounter == 2)
                       {
                       %>
                           </div>   
                       <%
                                   StepCounter = 0;
                               }

                           }

                           if (StepCounter == 1) {
 %>
                           </div>   
                       <%
                           }

                           %>

          </div>