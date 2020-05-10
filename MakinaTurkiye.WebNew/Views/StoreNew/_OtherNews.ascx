﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NeoSistem.MakinaTurkiye.Web.Models.StoreNews.MTNewOtherItem>>" %>
<div class=" new-container-detail">
    <div class="" style="font-weight: 600; font-size: 18px; text-align: center; margin-bottom: 10px;">Son Haberler</div>
    <ul class="list-group new-others-list">
        <%foreach (var item in Model.ToList())
            {%>
        <li class="list-group-item">
            <div style="float: left;">
                <a href="<%:item.NewUrl %>" title="<%:item.Title %>">
                    <img src="<%:item.ImagePath %>" alt="<%:item.Title %>" style="width: 75px;" class="img-thumbnail" /></a>
            </div>
            <div style="float: left; width: 175px;">
                <a href="<%:item.NewUrl %>" title="<%:item.Title %>">
                    <%string title = item.Title;
                        if (item.Title.Length > 60) { title = title.Substring(0, 60)+".."; }%>
                    <%:title %>
                </a>
                <div>
                    <span class="small" style="color: #ccc;"><%:item.RecordDate.ToString("dd MMM, yyyy",CultureInfo.InvariantCulture) %></span>
                </div>
            </div>
            <div style="clear: both;"></div>
        </li>
        <%} %>
    </ul>
</div>
