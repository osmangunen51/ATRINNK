<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.Account.GetCategoriesModel>" %>
<div class="tab-pane <%=Model.IsActive ? "active" : string.Empty %>" data-rel="tabcontent" id="baa<%: Model.ParentCategoryID %>">
        <% foreach (var productGroup in Model.ProductGroupList.Where(c=> c.ProductCount.Value > 0))
            { %>
            <div class="col-xs-12">
                <h3>
                <%:productGroup.CategoryName%>									
                </h3>
                </div>   
                <% foreach (var item2 in Model.CategoryList(productGroup.CategoryId).OrderBy(c=>c.CategoryOrder).ThenBy(c => c.CategoryName))
                { %>
                <% 
                    bool checkRelatedCategory = Model.MemberRelatedCategory.Any(c => c.CategoryId == item2.CategoryId);  %>
                    <div class="col-md-6 col-lg-6">
                        <div class="checkbox">
                            <label>
                                <%: Html.CheckBox("MainPartyRelatedCategory", checkRelatedCategory, new { value = item2.CategoryId })%>
                                <%:item2.CategoryName%>
                            </label>
                        </div>
                    </div>    
                <% } %>
            <% } %>
        <span class="clearfix">
        </span>
</div>