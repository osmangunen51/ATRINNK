<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<CategoryModel>>" %>
<label class="col-sm-3 control-label">
    Seri seçiniz*
</label>
<div class="col-sm-6">
    <%:Html.DropDownList("DropDownSeries", new SelectList(Model, "CategoryId", "CategoryName"), "< Lütfen Seçiniz >", new { @class = "form-control" })%>
</div>
