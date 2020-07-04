﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<NeoSistem.MakinaTurkiye.Web.Models.StoreNews.MTStoreNewItemModel>>" %>


<%foreach (var item in Model.Source.ToList())
    {%>
<div class="new-item-mt-c">
    <div class="new-item-mt">
        <%if (!string.IsNullOrEmpty(item.ImagePath))
            {%>
        <div class="new-item-image">
            <div class="new-item2">
                <div class="item-image" style="background-color: #fff; max-height: 230px; text-align: center;">
                    <img src="<%:item.ImagePath %>" alt="<%:item.Title %>" title="<%:item.Title %>" class="img-responsive" />

                </div>
    
                <a class="item-link" href="<%:item.NewUrl %>"></a>
            </div>
        </div>
        <% } %>

        <div class="new-date2">
            <%:item.DateString %>
        </div>

        <div class="new-blog-title">
            <a href="<%:item.NewUrl %>"><%:item.Title %> </a>
        </div>
        <div class="pull-left new-store-name">
            <a href="<%:item.StoreUrl %>" title="<%:item.StoreName %>">
                <% string storeName = item.StoreName;
                    if (item.StoreName.Length > 35)
                    {
                        storeName = item.StoreName.Substring(0, 35) + "..";  %><% } %>
                <%:storeName %></a>
        </div>
        <div class="pull-right">
            <a class="btn background-mt-btn" href="<%:item.NewUrl %>" style="font-size: 12px;">Yazıyı Keşfet</a>
        </div>
        <div style="clear: both"></div>
    </div>
</div>

<%} %>


<%if (Model.TotalPage() > 1)
    {%>
<div class="row ommentPagination" style="text-align: center; margin-top: 10px;">
    <ul class="pagination " style="margin: 0px;">
        <%string prevLink = "javascript:void(0);";
            string nextLink = "javascript:void(0);";%>
        <%if (Model.CurrentPage - 1 > 0)
            {
                int prevP = Model.CurrentPage - 1;
                prevLink = Request.Url.AbsolutePath + "?page=" + prevP.ToString();
            }%>

        <% %>
        <li><a href="<%:prevLink %>">« </a></li>

        <%foreach (var item in Model.TotalPages)
            {

                if (item == Model.CurrentPage)
                {%>
        <li class="active"><span style="background-color: #fc8120; border: none;"><%:item %></span> </li>
        <% }
            else
            {%>
        <li><a href="<%:Request.Url.AbsolutePath %>?page=<%:item %>"><%:item %></a></li>
        <% }
        %>

        <%} %>
        <%if (Model.TotalPage() > Model.CurrentPage)
            {
                int nextP = Model.CurrentPage + 1;
                nextLink = Request.Url.AbsolutePath + "?page=" + nextP.ToString();
            } %>
        <li><a href="<%:nextLink %>">» </a></li>
    </ul>
</div>
<% } %>