﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Category>>" %>
<div class="categoryPanel pnla" style="width: 340px;">
  <div class="categoryPanelTitle" style="margin-left: 20px; width: 150px">
    <span class="cNumber1">Ana Kategoriyi Seçin*</span>
  </div>
  <div class="sectorPanel">
    <ul class="iListCategory1" control="#DropDownCategory" show="#ListCategory">
      <% foreach (var item in Model)
         { %>
          <%
           int deger = 0;
           if (item.CategoryParentId == null) { } else { deger = item.CategoryParentId.ToInt32(); } %>
      <li value="<%: item.CategoryId %>" name="ParentCategory" parent="<%:deger %>">
        <div style="float: left; width: 95%">
          <a>
            <%: item.CategoryName %>
          </a>
        </div>
        <div style="float: left; width: 5%">
          >>
        </div>
      </li>
      <% } %>
    </ul>
  </div>
</div>
