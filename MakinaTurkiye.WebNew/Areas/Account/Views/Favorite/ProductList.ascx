﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<ProductModel>>" %>
<div class="listHeader" style="width: 768px;">
  <div class="listContent" style="width: 740px;">
    <div style="float: left; width: 300px">
      <span style="margin-right: 3px;">Görünüm :</span> <a href="/Account/Favorite/Product?DisplayType=<%= (byte)DisplayType.Window %>">
        <img src="/content/images/window.png" alt="Pencere" style="float: left" /><span>Pencere</span>
      </a><a href="/Account/Favorite/Product?DisplayType=<%= (byte)DisplayType.List %>">
        <img src="/content/images/list.png" alt="Liste" style="float: left" /><span>Liste</span>
      </a><a href="/Account/Favorite/Product?DisplayType=<%= (byte)DisplayType.Text %>">
        <img src="/content/images/text.png" alt="Metin" style="float: left" /><span>Metin</span></a>
    </div>
    <div style="float: right;">
      <span style="margin-top: 2px; margin-right: 3px;">Sırala : </span>
      <select style="font-size: 11px; font-family: Arial">
        <option value="1">A `dan Z`ye</option>
        <option value="1">Z `den A`ya</option>
      </select>
    </div>
  </div>
  <div class="listContent" style="margin-top: 4px; width: 740px">
    <div style="float: left; width: 300px">
      <span>Toplam 513 sayfa içerisinde</span> <span style="color: #0769cd">3.</span>
      <span>sayfadasınız.</span>
    </div>
    <div style="float: right;" class="listPaging">
      <ul>
        <% foreach(var item in Model.TotalLinkPages)
           { %>
        <% if(Model.CurrentPage == item)
           { %>
        <span class="activepage" style="cursor: pointer; color: red; font-weight: bold;">
          <%: item%></span>
        <% }
           else
           { %>
        <span class="page" style="cursor: pointer; font-weight: bold;" onclick="PageChange(<%: item %>,<%= (byte)DisplayType.List %>);">
          <%: item%></span>
        <% } %>
        <% } %>
        <li><a href="#">Sonraki >></a></li>
      </ul>
    </div>
  </div>
</div>
<div style="width: 760px; height: auto; float: left">
  <% foreach(var model in Model.Source)
     { %>
  <div class="productListContent" style="border-bottom: solid 2px #eee; margin-top: 10px;
    width: 760px;">
    <div class="productListImage">
      <div class="image" style="text-align: center">
        <span style="font-size: 11px; font-weight: bold; display: block; margin-top: 45px;
          margin-left: 35px;">Resim
          <br />
          120 x 120</span>
      </div>
      <a>
        <img src="/content/images/starts.png" alt="Rating" style="margin-top: 5px" align="left" />
        <span>Yorumlar</span> </a>
    </div>
    <div class="productListText" style="width: 500px;">
      <a href="#" style="text-decoration: none; color: #1672d0; font-weight: bold;
        height: 40px;">
        <%:model.ProductName%>
      </a><span style="color: #888; font-size: 11px;">Marka : Samsung</span> <span style="margin-top: 5px;
        color: #888; font-size: 11px;">Model Tipi : W8500</span> <span style="color: #888;
          margin-top: 5px;">Model yılı : 1985</span> <span style="color: #888; margin-top: 5px;
            font-size: 11px;">Kısa Detay : <b>Az Kullanılmış garantisi devam ediyor.</b></span>
    </div>
    <div style="width: 110px; height: 168px; float: left; background-color: #d8eaee;
      color: #bce1e7; margin-left: 10px; padding-left: 9px; padding-top: 8px;">
      <span style="font-size: 11px; font-weight: bold; color: #333333;">Durumu : </span>
      <span style="font-size: 11px; color: #0066ff;">İkinciel</span>
      <br />
      <span style="font-size: 11px; font-weight: bold; color: #333333; margin-top: 10px;">
        Fiyatı :</span> <span style="font-weight: bold; color: #f53b3c; font-size: 12px;
          margin-top: 5px; display: block;">1.596.885.00TL</span> <span style="font-size: 11px;
            font-weight: bold; color: #333333; margin-top: 5px; display: block;">İl/İlçe :
      </span><span style="font-size: 11px; color: #0066ff; margin-top: 5px; display: block;">
        İstanbul / Maltepe</span> <span style="font-size: 11px; font-weight: bold; color: #333333;
          margin-top: 5px; display: block;">Firma Adı : </span><span style="font-size: 11px;
            color: #0066ff; margin-top: 5px; display: block;">İmaksan Makine A.Ş</span>
      <span style="font-size: 11px; font-weight: bold; color: #333333; margin-top: 5px;
        display: block;">Üyelik Tarihi</span> <span style="font-size: 11px; color: #0066ff;">
          17 / 02 / 2010</span>
    </div>
  </div>
  <% } %>
</div>
