﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<StoreModel>>" %>
<style type="text/css">
  
</style>
<div class="listHeader" style="width: 768px;">
  <div class="listContent" style="width: 740px;">
    <div style="float: left; width: 300px">
      <%--<span style="margin-right: 3px;">Görünüm :</span> <a href="/Account/Favorite/Store?DisplayType=<%= (byte)DisplayType.Window %>">
        <img src="/content/images/window.png" alt="Pencere" style="float: left" /><span>Pencere</span>
      </a><a href="/Account/Favorite/Store?DisplayType=<%= (byte)DisplayType.List %>">
        <img src="/content/images/list.png" alt="Liste" style="float: left" /><span>Liste</span>
      </a><a href="/Account/Favorite/Store?DisplayType=<%= (byte)DisplayType.Text %>">
        <img src="/content/images/text.png" alt="Metin" style="float: left" /><span>Metin</span></a>--%>
    </div>
    <div style="float: right;">
      <%--<span style="margin-top: 2px; margin-right: 3px;">Sırala : </span>
      <select style="font-size: 11px; font-family: Arial">
        <option value="1">A `dan Z`ye</option>
        <option value="1">Z `den A`ya</option>
      </select>--%>
    </div>
  </div>
  <div class="listContent" style="margin-top: 4px; width: 740px">
    <div style="float: left; width: 300px">
      <%--    <span>Toplam 513 sayfa içerisinde</span> <span style="color: #0769cd">3.</span>
      <span>sayfadasınız.</span>--%>
    </div>
    <div style="float: right;" class="listPaging">
      <ul>
        <li>Sayfa :</li>
        <% foreach (var item in Model.TotalLinkPages)
           { %>
        <% if (Model.CurrentPage == item)
           { %>
        <span class="activepage" style="cursor: pointer; color: red; font-weight: bold;">
          <%: item %></span>
        <% }
           else
           { %>
        <span class="page" style="cursor: pointer; font-weight: bold;" onclick="PageChange(<%: item %>,<%= (byte)DisplayType.Window %>);">
          <%: item %></span>
        <% } %>
        <% } %>
        <li>
      </ul>
    </div>
  </div>
</div>
<div style="width: 700px; height: auto; float: left; margin-top: 20px; margin-left: 50px;">
  <% int j = 0; %>
  <% foreach (var model in Model.Source)
     { %>
  <% j += 1; %>
  <div class="productWindowContent">
    <div class="productWindowImage" style="text-align: center; padding-top: 45px; height: 75px">
      <% string logoPath = AppSettings.StoreLogoThumb150x90Folder + model.StoreLogo;%>
      <img src="<%=logoPath%>" />
    </div>
    <div class="productWindowText">
      <a style="color: #1672d0; margin-top: 5px; text-decoration: none;" href="#">
        <%:model.StoreName%></a>
    </div>
  </div>
  <% } %>
</div>
