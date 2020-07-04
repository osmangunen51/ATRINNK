<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTHomeLeftCategoriesModel>" %>
       <div class="hidden-xs hidden-sm hidden-md col-lg-3">
            <h3 class="category-list__header"><span class="icon-menu"></span>Önerilen Sayfalar</h3>
                <ul class="category-list">
                    <%foreach (var categoryItem in Model.HomeLeftChoicedCategories)
                      {%>
                          
                        <li class="category-list__item"><a href="<%:categoryItem.CategoryUrl %>" class="category-list__link"><%:categoryItem.CategoryName %></a></li>   
                     <% } %>
                   
                  
                    <li class="category-list__item category-list__item--more has-sub-item">
                        <a href="javascript:;" class="category-list__link">Tüm Kategoriler</a>
                        <div class="hover-fix"></div>
                        <ul class="category-list__more-menu">
                            <%foreach (var sectorItem in Model.HomeLeftAllSectors)
                              {%>
                                  <li><a href="<%:sectorItem.CategoryUrl %>"><%:sectorItem.CategoryName %></a></li>
                                  
                              <%} %>
                
                        </ul>
                    </li>
                </ul>

            </div>