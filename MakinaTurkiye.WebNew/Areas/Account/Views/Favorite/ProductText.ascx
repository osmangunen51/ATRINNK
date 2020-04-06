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
          <%: item %></span>
        <% }
           else
           { %>
        <span class="page" style="cursor: pointer; font-weight: bold;" onclick="PageChange(<%: item %>,<%= (byte)DisplayType.Text %>);">
          <%: item %></span>
        <% } %>
        <% } %>
        <li><a href="#">Sonraki >></a></li>
      </ul>
    </div>
  </div>
</div>
<div style="float: left; width: auto; height: auto">
  <div class="contentWrapper">
    <div class="tableHeader" style="width: 768px;">
      <ul>
        <li style="width: 74px"></li>
        <li class="sep"></li>
        <li style="width: 160px">Firma Adı</li>
        <li class="sep"></li>
        <li style="width: 365px">Faaliyet Alanı</li>
        <li class="sep"></li>
        <li style="width: 70px">İl İlçe</li>
      </ul>
    </div>
    <div class="tableContent" style="width: 766px;">
      <% int j = 0; %>
      <% foreach(var model in Model.Source)
         { %>
      <% j += 1; %>
      <ul <%: j % 2 == 1 ? "class=alternate" : "" %> style="width: 766px;">
        <li style="width: 73px; text-align: center;"><span>
          <img src="/content/images/resimyok.png" alt="Resim" />
          <img src="/content/images/video.png" alt="Video" style="margin-top: 2px" />
          <img src="/content/images/vchar.png" alt="VChar" />
          <img src="/content/images/star.png" alt="Star" />
        </span></li>
        <li class="sep"></li>
        <li style="width: 160px"><span style="color: #0769cd">
          <%:model.ProductName %></span> </li>
        <li class="sep"></li>
        <li style="width: 365px"><span style="color: #0769cd">Tekstil Makinaları | Dikiş Makinaları
          | Ceket Dikim Specialleri | Cad Sistemleri | Ayakkabı Makinaları | Süs Dikiş Makinaları
          | Cad Sistemler</span></li>
        <li class="sep"></li>
        <li style="width: 67px"><span>ISTANBUL / ESENLER</span></li>
      </ul>
      <% } %>
    </div>
  </div>
</div>
