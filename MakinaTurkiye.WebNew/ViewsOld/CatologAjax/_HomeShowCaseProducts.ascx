<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTHomeAdModel>>" %>
<div class="row popular-ilanlar">
    <div class="col-xs-12">
        <h2 style="color: #4F4F4F;
              font-family: Arial;
              font-size: 24px;
              font-weight: 700;
              line-height: 27px;">
            <span>Vitrindeki Makina ve Ekipmanlar</span>
        </h2>
    </div>
    <div class="col-xs-12">
         <%foreach (var item in Model)
                {%>
            <div class="col-md-2 col-xs-6 home-showcase__item">
                <a href="<%:item.ProductUrl %>" title="<%:item.ProductName %>">
                    <div class="home-showcase__img">
                        <img src="<%:item.PicturePath %>" alt="<%:item.ProductName %>" />
                    </div>
                    <h5><%:item.TruncatedProductName %></h5>

                </a>
            </div>
            <% } %>
    </div>
</div>