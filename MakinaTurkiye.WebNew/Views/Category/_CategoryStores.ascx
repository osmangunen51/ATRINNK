<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTCategoryStoreModel>" %>
<% string gorunumt = Request.QueryString["Gorunum"]; %>

<%if (gorunumt != "3")
    {  %>



    <div class="col-xs-12">
        <div class="category-firms">
            <div class="sector-firms-slider owl-carousel clearfix js-sector-firm-slider">
                <%
                    var asastores = Model.StoreItemModes.ToList();
                    if (asastores.Count > 0)
                    {
                        foreach (var item in asastores)
                        {
                            string logoPath = "";
                            if (item != null)
                            {
                                logoPath = item.PictureLogoPath;
                %>
                <div class="sector-firms-slider__item item">
                    <div class="sector-firms-slider__img">
                        <a href="<%:item.StoreUrl %>">
                            <img src="<%=item.PictureLogoPath %>" alt="<%=item.StoreName %>" />
                        </a>
                    </div>
                    <a href="<%=item.StoreUrl %>" target="_blank">
                        <p style="color: #333; display: -webkit-box; -webkit-line-clamp: 2; -webkit-box-orient: vertical; overflow: hidden; min-height: 32px;"><%=item.StoreName%></p>
                    </a>
                    <div class="sector-firms-slider__category">
                        <a href="<%:item.StoreProductCategoryUrl %>" class="btn-xs btn btn-store-category">
                            <%--<span><%:Html.Truncatet(Model.SelectedCategoryName, 38) %></span>--%>
                            <span><%:Html.Truncatet(Model.SelectedCategoryName, 38) %></span>
                        </a>
                    </div>
                </div>


                <%
                            }
                        }
                    } %>
            </div>
        </div>
    </div>

<% } %>