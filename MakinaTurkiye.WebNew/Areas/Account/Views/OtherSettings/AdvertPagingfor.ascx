<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.MTProductItem>>" %>

<table class="table table-hover table-condensed">
    <thead>
        <tr>
            <th style="width: 100px">Ürün No
            </th>
            <th>Ürün Adı
            </th>
            <th>Fiyatı
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        <%
            foreach (var count in Model.Source)
            {
        %>
        <%string productUrl = Helpers.ProductUrl(count.ProductId, count.ProductName); %>
        <tr>
            <td>
                <a href="<%=productUrl%>">
                    <%=count.ProductNo%></a>
            </td>
            <td>
                <a href="<%=productUrl%>">
                    <%=Helpers.Truncate(count.ProductName, 100)%></a>
            </td>
            <td>
                <div id="priceEditWrapper<%:count.ProductId %>" style="display: none;">
                    <%:Html.TextBox("editPriceValue", count.ProductPriceDecimal.Value.ToString("N2"), new {@id="editPriceValue"+count.ProductId ,@class="form-control"})%>
                    <button type="button" class="btn btn-info" onclick="PriceEditClick(<%:count.ProductId %>)">Kaydet</button>
                    <button type="button" class="btn btn-info" onclick="PriceEditCancel(<%:count.ProductId %>)">İptal</button>
                </div>
                <div id="priceDisplay<%:count.ProductId %>">
                    <span id="priceDisplayValue<%:count.ProductId %>" style="width: 50px!important;"><%=count.ProductPrice%></span> - <a href="#" onclick="PriceEditShow(<%=count.ProductId %>)" class="btn btn-xs btn-default"><span class="glyphicon glyphicon-pencil"></span></a>
                </div>
            </td>
            <td>
                <a href="<%=productUrl%>" class="btn btn-xs btn-default"><span class="glyphicon glyphicon-eye-open"></span>&nbsp;İncele </a>
            </td>
        </tr>
        <%
            }
        %>
    </tbody>
</table>

<hr>
<div class="row">
    <% int pageCount = Model.TotalLinkPages.Count(); %>
    <div class="col-md-6 ">
        Toplam
                    <%: pageCount%>
                    sayfa içerisinde
                    <%: Model.CurrentPage%>. sayfayı görmektesiniz.
    </div>
    <div class="col-md-6 text-right">
        <ul class="pagination m0">
            <li><a onclick="PageChangeOtherSettings(1,2,1);" href="#">&laquo; </a></li>
            <% foreach (var item in Model.TotalLinkPages)
                { %>
            <% if (Model.CurrentPage == item)
                { %>
            <li class="active"><a href="#">
                <%: item%></a></li>
            <% }
                else
                { %>
            <li>
                <a onclick="PageChangeOtherSettings(<%: item %>,2,1);" href="#"><%: item%></a></li>
            <% } %>
            <% } %>
            <li><a href="#" onclick="PageChangeOtherSettings(<%: pageCount%>,2,1);">&raquo; </a></li>
        </ul>
    </div>
</div>
