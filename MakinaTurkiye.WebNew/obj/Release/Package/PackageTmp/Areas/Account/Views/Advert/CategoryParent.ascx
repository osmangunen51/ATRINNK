<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MakinaTurkiye.Entities.Tables.Catalog.Category>>" %>
<div class="col-sm-4" data-rel="categoryPanel">
    <label data-rel="title">Ana Kategoriyi Seçiniz*</label>
    <select class="form-control" style="height:auto;"  size="10" data-rel="iListCategory">
    <% foreach (var item in Model)
        { %>
            <option value="<%: item.CategoryId %>" name="ParentCategory" parent="<%: item.CategoryParentId %>"><%: item.CategoryName %></option>
        <% } %>
    </select>
</div>
