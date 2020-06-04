<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NeoSistem.MakinaTurkiye.Web.Models.Home.MTHomeSectorItem>>" %>

<%if (Model.Count > 0)
    {%>
<div class="row" style="margin-top: 1.5rem;">
    <% foreach (var item in Model.ToList())
        {
            if (!string.IsNullOrEmpty(item.ImagePath))
            {
    %>
    <div class="col-6 col-md-4 col-lg-3">
        <a href="<%:item.CategoryUrl %>" class="category-item" title="<%:item.CategoryContentTitle %>">
            <img
                  src="https://s.makinaturkiye.com/image-loading.png"
                data-src="<%:item.ImagePath %>" 
                class="img-lazy"
                alt="<%:item.CategoryContentTitle %> ">
            <div class="category-item__body">
                <h3 class="category-item__title"><%:item.CategoryName %></h3>
                <span class="btn btn-white btn-explore"><i class="fa fa-arrow-right"></i></span>
            </div>
        </a>

    </div>
    <%    }
        }
    %>
</div>
<%} %>
