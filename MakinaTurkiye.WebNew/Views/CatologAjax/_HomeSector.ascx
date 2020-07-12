<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NeoSistem.MakinaTurkiye.Web.Models.Home.MTHomeSectorItem>>" %>

<%if (Model.Count > 0)
    {%>
     <% foreach (var item in Model.ToList())
                {
                    if (!string.IsNullOrEmpty(item.ImagePath))
                    {%>
                        <div class="col-6 col-md-4 col-lg-3">
                            <a href="<%:item.CategoryUrl %>" class="category-item" title="<%:item.CategoryContentTitle %>">
                                <img
                                    src="<%:item.ImagePath %>"
                                    alt="<%:item.CategoryContentTitle %> ">
                                <div class="category-item__body">
                                    <h3 class="category-item__title"><%:item.CategoryContentTitle %></h3>

                                </div>
                            </a>
                        </div>
                    <%}
                }
            %>
<%} %>
