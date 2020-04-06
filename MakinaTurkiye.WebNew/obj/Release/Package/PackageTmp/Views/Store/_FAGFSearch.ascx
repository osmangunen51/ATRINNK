<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTStoreViewModel>" %>
<%--<div class="well well-mt2 form-horizontal">--%>
<%--<form method="javascript:void(0)" action="<%:Request.Url.AbsolutePath.ToString() %>">--%>
    <div class="input-group">
<%--        <%:Html.HiddenFor(x=>x.StoreCategoryModel.SelectedCategoryId) %>--%>
        <input type="hidden" name="newCategoryId" id="newCategoryId"/>
        <input type="text" class="search-text-autocomplate form-control" style="border:1px solid #fc8120; border-radius:0; outline:0;" name="searchText"
            id="categoryName" placeholder="Kategoriden Firma Arama" autocomplete="off"><span
                role="status" aria-live="polite" class="ui-helper-hidden-accessible"></span>
<%--        <input type="hidden" id="CategoryId" name="CategoryId">--%>
        <span class="input-group-btn">
            <button id="" class="btn btn-default js-firm-category-search" type="submit" style="background:#fc8120; border-radius:0; color:#fff; border:1px solid #fc8120;">
                <span class="glyphicon glyphicon-search"></span>
            </button>
        </span>
    </div>
      <div class="" id="error-categoryname" style="padding:5px; color:#d00000; display:none;" ></div>
<%--</form>--%>
<%--</div>--%>
