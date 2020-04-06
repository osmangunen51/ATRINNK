<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<MTCategorySearchModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    <
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta name="robots" content="noindex, nofollow" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-md-12" style="padding-right: 0px; padding-left: 0px; margin-top: 20px;">
        <% if (Request.QueryString["SearchText"] != null && Request.QueryString["SearchText"].ToString().Length < 3)
           { %>
                <div class="row">
            <div class="col-md-12 search-no-result-container" >
                <div class="col-md-6 message">
                    <img src="../../Content/V2/images/not-found.png" />
                    <p class="message-text" >
              "<b><%:Request.QueryString["SearchText"].ToString() %></b>" ile ilgili sonuç bulunamamıştır.
                    </p>
                    <div>
                     Arama yapabilmek için en az 3 karakter girmelisiniz kelimenizi arttırabilir veya <a href="<%:AppSettings.SiteAllCategoryUrl %>">tüm ürünler</a> sayfamıza bakabilirsiniz.
                    </div>
     
                </div>
            </div>
        </div>

        <% }
           else
           {%>
        <%if (Model.CategoryModel.TopCategoryItemModels.ToList().Count > 0)
          { %>
        <h4>"<%:Model.SearchText %>" aramanızda <b style="color: #bf0a0a">"<%:Model.TotalCount %>"</b> ilan bulundu </h4>
        <div class="small">Aşağıdaki linklerden arama sonuçlarına ulaşabilirsiniz</div>
        <hr />
        <div class="result-category">
            <div class="row">
                <%foreach (var item in Model.CategoryModel.TopCategoryItemModels.ToList().OrderBy(s => s.CategoryName))
                  {%>

                <div class="col-md-3">
                    <div class="result-category__item searchResultCategory">
                        <img class="result-category__img" src="<%:item.CategoryIcon %>" />
                        <p class="result-category__category-name"><a href="<%:item.CategoryUrl %>"><%:item.CategoryName %>&nbsp<b style="color: #808080">(<%:item.ProductCount %>)</b></a></p>

                        <ul class="subCategory" id="subCategory<%:item.CategoryId %>">
                            <%foreach (var subCategory in Model.CategoryModel.SubCategories.Where(c => c.CategoryParentId == item.CategoryId).OrderBy(x => x.CategoryName))
                              { %>


                            <li><a href="<%:subCategory.CategoryUrl %>"><%:subCategory.CategoryName %>&nbsp<small style="color: #808080">(<%:subCategory.ProductCount %>)</small></a></li>
                            <%} %>

                            <%if (Model.CategoryModel.SubCategories.Where(x => x.CategoryParentId == item.CategoryId).ToList().Count > 5)
                              { %>
                            <li class="showAllSub" id="showAllSub<%:item.CategoryId %>"><b>Tümünü Gör</b><span class="icon-fill-down-arrow" aria-hidden="true"></span></li>
                            <%} %>
                        </ul>

                    </div>
                </div>

                <%} %>
            </div>
        </div>
        <%}
          else
          { %>
        <div class="row">
            <div class="col-md-12 search-no-result-container" >
                <div class="col-md-6 message">
                    <img src="../../Content/V2/images/not-found.png" />
                    <p class="message-text" >
              "<b><%:Model.SearchText %></b>" ile ilgili sonuç bulunamamıştır.
                    </p>
                    <div>
                        Arama kelimenizi değiştirebilir veya <a href="<%:AppSettings.SiteAllCategoryUrl %>">tüm ürünler</a> sayfamıza bakabilirsiniz.
                    </div>
     
                </div>
            </div>
        </div>

        <%} %>
        <%} %>
    </div>
</asp:Content>
