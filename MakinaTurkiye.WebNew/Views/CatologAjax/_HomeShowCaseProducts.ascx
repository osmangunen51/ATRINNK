﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTHomeAdModel>>" %>
<div class="row">
    <div class="col-md-12">
        <div class="home-showcase__header">
            <h2 style="color: #fff !important;margin: 0px;font-family: Arial; line-height: 24px !important;font-size:18px">Vitrindeki Makina ve Ekipmanlar</h2>
        </div>
        <div class="home-showcase__body">
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
</div>
