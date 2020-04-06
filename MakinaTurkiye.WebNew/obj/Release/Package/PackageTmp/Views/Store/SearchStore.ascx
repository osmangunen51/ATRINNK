﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<StoreModel>>" %>
<div style="width: 100%; padding-top: 10px; float: left;">
  <div style="width: 100%; height: 30px; margin-top: 5px; float: left; margin-left: 5px;">
    <div style="width: 250px; height: 24px; background-color: #74a1d0; color: #fff; font-size: 13px;
      padding-left: 20px; padding-top: 4px; float: left;">
      <span style="font-family: Segoe UI, Arial; font-weight: bold;">MAĞAZA ARAMA (TÜM SONUÇLAR)</span>
    </div>
    <div style="width: 400px; float: left; height: 24px; margin-left: 260px; text-align: right;
      background-color: #e4e4e4; padding-top: 4px; border: solid 1px #c7c7c7;">
      <div style="float: right; margin-right: 10px; margin-top: 2px;">
        <% int pageDimension = 21;%>
        <% foreach (var item in Model.TotalLinkPages)
           { %>
        <% if (Model.CurrentPage == item)
           { %>
        <span class="activepage">
          <%: item%></span>
        <% }
           else
           { %>
        <span class="page" onclick="PageChange(<%: item %>,<%: pageDimension %>);">
          <%: item%></span>
        <% } %>
        <% } %>
      </div>
      <div style="float: right; margin-right: 5px;">
        <img id="imgLoading" style="display: none;" src="/Content/Images/load.gif" width="16"
          height="11" />&nbsp; <span style="font-size: 11px; font-family: Segoe UI Arial;">Sayfa
            : </span>
      </div>
    </div>
  </div>
</div>
<div style="padding-top: 9px; width: 950px; padding-left: 0px; padding-right: 5px;
  margin-top: 10px; float: left;">
  <% if (Model.Source.Count == 0)
     { %><span style="font-size: 11px; font-weight: bold;"> &nbsp; &nbsp; Aradığınız kriterlere
       uygun mağaza bulunamadı.</span>
  <% }
     else
     { %>
  <% int i = 0; %>
  <% foreach (var model in Model.Source)
     { %>
  <% i += 1; %>
          <%string url = "/sirket/" + model.MainPartyId.ToString() + "/" + Helpers.ToUrl(model.StoreName);  %>
  <div class="productContent">
    <div class="productImage" style="text-align: center; height: 120px">
      <%var imagePath = AppSettings.StoreLogoFolder + model.StoreLogo;%>
      <% if (FileHelpers.HasFile(imagePath))
         { %>
      <a href="<%= url %>/urunler">
        <img src="<%= "fgdgdfgdgfdfgdgfd"  %>" style="width: 120px; height: 120px;" />
      </a>
      <% }
         else
         { %>
      <div style="width: 120px; height: 75px; border: solid 1px #000; text-align: center;
        padding-top: 45px;">
        <span style="color: #000;font-size: 11px; font-weight: bold">Firma Logosu Bulunamadı</span>
      </div>
      <% } %>
    </div>
    <div class="productText">
      <div style="width: 100%; float: left; height: 44px;">
      </div>
      <div style="width: 100%; float: left; height: 50px;">
        <a title="<%: model.StoreName %>" style="color: #1672d0; text-decoration: none;
          font-size: 11px; font-weight: bold;" href="<%= url %>/urunler">
          <%: Html.Truncate(model.StoreName, 72)%></a>
      </div>
    </div>
  </div>
  <% if (i % 7 == 0)
     { %>
  <hr style="background: #eee; height: 2px; border: none; margin-left: 5px; float: left;
    width: 98%;" />
  <br />
  <% } %>
  <% } %>
  <% } %>
</div>
