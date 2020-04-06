﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.Helpers.PagingModel<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTProductsPageProductList>>" %>
<div class="row">
    <div class="col-md-4 ">
        Toplam
        <%:Model.TotalRecord %>
        ürün,
  
        <%:Model.TotalPage %>
        sayfa bulunmaktadır.
    </div>
    <div class="col-md-8 text-right">
        <ul class="pagination m0">
            <li><span id="spanLoading" style="display: none">Yükleniyor.. Lütfen
                Bekleyiniz&nbsp;&nbsp;</span></li>
            <% int pageDimension = 24;%>
            <%
                int prevPage = (Model.CurrentPage > 1) ? Model.CurrentPage - 1 : Model.CurrentPage;
                int nextPage = (Model.CurrentPage < Model.TotalPage) ? Model.CurrentPage + 1 : Model.CurrentPage;
            %>

            <li><a style="cursor: pointer;" onclick="PageChangeNew(<%: prevPage%>, <%=pageDimension%>);">«</a>
            </li>
            <% foreach (int i in Model.TotalLinkPages)
                { %>
            <% if (Model.CurrentPage == i)
                { %>
            <li class="active"><a style="cursor: pointer;" onclick="PageChangeNew(<%: i%>, <%=pageDimension%>);">
                <%: i%></a></li>
            <% }
                else
                { %>
            <li><a style="cursor: pointer;" onclick="PageChangeNew(<%: i%>, <%=pageDimension%>);">
                <%: i%></a></li>
            <% } %>
            <% } %>

            <li><a style="cursor: pointer;" onclick="PageChangeNew(<%: nextPage%>, <%=pageDimension%>);">» </a>
            </li>
        </ul>
    </div>
</div>
