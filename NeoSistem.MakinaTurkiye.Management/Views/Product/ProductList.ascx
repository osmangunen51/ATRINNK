<%@ Control Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewUserControl<FilterModel<ProductModel>>" %>
<% int row = 0; %>
<% foreach (var item in Model.Source)
    { %>
<% row++; %>
<tr id="row<%: item.ProductId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
    <td class="CellBegin">
        <%: item.ProductNo%>
    </td>
    <td class="Cell">
        <%--<span title="<%: item.ProductName %>">
      <%: item.ProductName%></span>--%>

        <div>

            <textarea  id="<%:item.ProductId %>" rows="4" cols="5" column="product" class="edit-input" style="display: none; width:200px;"></textarea>
            <%= Html.Label("",item.ProductName)%>

            <%--  <%: item.ProductName%>--%>
            <div class="controls">
                <a class="edit" style="float: right; margin-top: 17px; font-size: 11px;" title="Düzenle" href="#">Dzl</a>
            </div>
        </div>

    </td>
    <td class="Cell">
        <%= item.MainCategoryName %>
    </td>
    <td class="Cell">
        <%if (!string.IsNullOrEmpty(item.NameBrand))
            {  %>
        <div>
            <%=Html.Label("",item.NameBrand)%>



            <textarea  column="brand"  id=" <%:item.BrandId %>" class="edit-input" style="display: none;  width:150px;" ></textarea>
            <div class="controls">
                <a class="edit" style="float: right; margin-top: 17px; font-size: 11px;" title="Düzenle" href="#">Dzl</a>
            </div>
        </div>
        <%} %>


        <%--   <%: item.NameBrand%>--%>
    </td>
    <td class="Cell">

        <%if (!string.IsNullOrEmpty(item.NameSeries))
            {  %>
        <div>
            <%=Html.Label(item.NameSeries)%>

                        <textarea  column="serie"   id="<%:item.SeriesId %>"   class="edit-input" style="display: none;  width:150px;" ></textarea>
            <div class="controls">
                <a class="edit" style="float: right; margin-top: 17px; font-size: 11px;" title="Düzenle" href="#">Dzl</a>
            </div>
        </div>
        <%} %>

        <%-- <%: item.NameSeries%>--%>
    </td>
    <td class="Cell">
        <div>
            <%if (!string.IsNullOrEmpty(item.NameModel))
                {  %>

            <%=Html.Label("", item.NameModel)%>
                                    <textarea  column="model"   id="<%:item.ModelId %>"   class="edit-input" style="display: none;  width:150px;" ></textarea>




            <%}
    else {%>
            <label for=""></label>
            
                      <textarea type="text" id="<%:item.ProductId %>"  column="modelNon" class="edit-input" style="display: none; width:150px;" ></textarea>
            
                    <% }%>
            <div class="controls">
                <a class="edit" style="float: right; margin-top: 17px; font-size: 11px;" title="Düzenle" href="#">Dzl</a>
            </div>
        </div>
        <%-- <%: item.NameModel%>--%>
    </td>
    <td class="Cell" style="text-align: center; padding-top: 5px">
        <%if (!string.IsNullOrWhiteSpace(item.OtherBrand))
            {%>
        <a onclick="BrandInsert('<%:item.ProductId %>');">
            <img src="/Content/images/brand.png" />
        </a>
        <%}%>
        <%if (!string.IsNullOrWhiteSpace(item.OtherModel))
            {%>
        <a onclick="ModelInsert('<%:item.ProductId %>');">
            <img src="/Content/images/model.png" />
        </a>
        <%}%>
    </td>
    <td class="Cell">
        <%: item.OtherBrand%>
    </td>
    <td class="Cell">
        <%: item.OtherModel%>
    </td>
    <td class="Cell">
        <% string text = "";
            string urlAdd = "";
            if (item.ProductActiveType != null)
            {
                var acType = (ProductActiveType)item.ProductActiveType;


                switch (acType)
                {
                    case ProductActiveType.Onaylandi:
                        text = "Onaylandı";
                        break;
                    case ProductActiveType.Onaylanmadi:
                        text = "Onaylanmamış";
                        break;
                    case ProductActiveType.Inceleniyor:
                        text = "İnceleniyor";
                        urlAdd = "?pagetype=firstadd";
                        break;
                    case ProductActiveType.Silindi:
                        text = "Silindi";
                        break;
                    case ProductActiveType.CopKutusuYeni:
                        text = "Çöp";
                        break;
                    default:
                        break;
                }
            }
            else { text = "değer yok"; }
        %>
        <div style="height: 20px; margin-top: 4px; text-align: center">
            <%=text %>
        </div>
    </td>
    <td class="Cell">
        <% 
            if (item.ProductActive)
            {
                text = "Aktif";
            }
            else
            {
                text = "Pasif";
            }

        %>
        <div style="height: 20px; margin-top: 4px; text-align: center">
            <%=text %>
        </div>
    </td>
    <td class="Cell">
        <%=item.MainPartyFullName %>
    </td>
    <td class="Cell">
        <%: item.StoreName%>
    </td>
    <td class="Cell">
        <% if (item.MemberType == (byte)MemberType.Individual)
            { %>
    Bireysel
    <% }
        else
        { %>
    Kurumsal
    <% } %>
    </td>
    <td class="Cell">

        <%if (item.ProductPriceType != null && item.ProductPriceType != 0)
            {
                if (item.ProductPriceType == (byte)ProductPriceType.Price)
                {
                    decimal newPrice = 0;
        %>

        <%if (item.DiscountType == (byte)ProductDiscountType.Amount)
            {
                if (item.DiscountAmount.HasValue)
                {
                    newPrice = item.ProductPrice.Value - item.DiscountAmount.Value;
                }
            }
            else
            {
                if (item.DiscountAmount.HasValue)
                    newPrice = item.ProductPrice.Value - ((item.DiscountAmount.Value * item.ProductPrice.Value) / 100);
            }
        %>
        <%if (newPrice != 0)
            { %>

        <%:newPrice.ToString("N2") %>
        <%}%>

        <span style="<%: (newPrice!=0) ? "text-decoration: line-through;":"" %>"><%: item.ProductPrice.Value.ToString("N2")%> </span><%: item.CurrencyName%>
        <%}
            else if (item.ProductPriceType == (byte)ProductPriceType.PriceRange)
            {
        %>
        <%:Convert.ToDecimal(item.ProductPriceBegin).ToString("N2") %>-<%:Convert.ToDecimal(item.ProductPriceLast).ToString("N2") %>   <%: item.CurrencyName%>
        <%}
            else if (item.ProductPriceType == (byte)ProductPriceType.PriceAsk)
            { %>
        Fiyat Sorunuz
      <%}
          else
          {%>
                Fiyat Görüşülür
            <%}
                }

                else
                { %>
        <%:item.ProductPrice.Value.ToString("N2") %>  <%: item.CurrencyName%>
        <%}%>
      
  
    </td>
    <td class="Cell" title="<%: (item.ProductRecordDate == new DateTime() ? "" : item.ProductRecordDate.ToString("dd MMMM yyyy dddd")) %>">
        <%: (item.ProductRecordDate == new DateTime() ? "" : item.ProductRecordDate.ToString("dd.MM.yyyy HH:mm:ss"))%>
    </td>
    <td class="Cell">
        <%:item.ProductLastUpdate.ToString("dd.MM.yyyy HH:mm:ss") %>
    </td>
    <td class="Cell" align="center">
        <a title="Düzenle" href="/Product/Edit/<%= item.ProductId %>">
            <img src="/Content/images/edit.png" hspace="2" />
        </a><a title="Sil" style="cursor: pointer;" onclick="DeletePost(<%: item.ProductId %>);">
            <img src="/Content/images/delete.png" hspace="2" />
            <%string productUrl = Helpers.ProductUrl(item.ProductId, item.ProductName); %>
        </a><a title="Detay" href="<%:productUrl %>" target="_blank">
            <img src="/Content/images/rightAllow.png" />
        </a>
        <%if (item.Doping)
            { %>
        <span style="padding: 1px; background-color: #07a71e; color: #fff">D</span>
        <%}%>
        <%if (item.ProductHomePageId != null)
            { %>
        <span style="padding: 1px; background-color: #07a71e; color: #fff" title="Anasayfa Seçilen Ürün">A</span>
        <%}%>
        <% if (item.VideoCount > 0)
            { %>
        <img src="/Content/images/Slate-slate_icons-video_ico-16x16.png" />
        <% } %>
        <%if (item.ReadyforSale == null || item.ReadyforSale == false)
            { %>
        <a onclick="ProductReadyForSale('<%:item.ProductId %>');">
            <img src="/Content/images/hemen_sat.png" />
        </a>
        <%}
            else
            {  %>
        <a onclick="ProductReadyForSale('<%:item.ProductId %>');">
            <img src="/Content/images/hemen_satma.png" />
        </a>
        <%} %>
                    <%if (NeoSistem.MakinaTurkiye.Management.Models.Authentication.CurrentUserModel.CurrentManagement.UserId == 1) {
                    %>
            <a  style="cursor:pointer; color:Blue;" onclick="DeleteProductSure(<%:item.ProductId %>)">(Sil)</a>
            <%
                } %>
    </td>
    <td class="CellEnd" align="center">
        <%:Html.CheckBox("CheckItems", new { value = item.ProductId, @class="CheckItems" })%>
    </td>
</tr>
<% } %>
<% if (Model.TotalRecord <= 0)
    { %>
<tr class="Row">
    <td colspan="19" class="CellBegin Cell" style="color: #FF0000; padding: 5px; font-weight: 700; font-size: 14px;">Ürün bulunamadı.
    </td>
</tr>
<% } %>
<tr>
    <td class="ui-state ui-state-default" colspan="19" align="left" style="border-color: #DDD; border-top: none; border-bottom: none;">
        <div style="float: right;" class="pagination">
            <ul>
                &nbsp;Sayfa&nbsp;&nbsp;
        <% foreach (int page in Model.TotalLinkPages)
            { %>
                <li>
                    <% if (page == Model.CurrentPage)
                        { %>
                    <span class="currentpage">
                        <%: page %></span>&nbsp;
          <% } %>
                    <% else
                        { %>
                    <a onclick="PagePost(<%: page %>)">
                        <%: page %></a>&nbsp;
          <% } %>
                </li>
                <% } %>
                <li>Gösterim: </li>
                <li>
                    <select id="PageDimension" name="PageDimension" onchange="SearchPost();">
                        <option value="20" <%: Session["product_PAGEDIMENSION"].ToString() == "20" ? "selected=selected" : "" %>>20</option>
                        <option value="50" <%: Session["product_PAGEDIMENSION"].ToString() == "50" ? "selected=selected" : "" %>>50</option>
                        <option value="100" <%: Session["product_PAGEDIMENSION"].ToString() == "100" ? "selected=selected" : "" %>>100</option>
                        <option value="250" <%: Session["product_PAGEDIMENSION"].ToString() == "250" ? "selected=selected" : "" %>>250</option>
                        <option value="500" <%: Session["product_PAGEDIMENSION"].ToString() == "500" ? "selected=selected" : "" %>>500</option>
                        <option value="750" <%: Session["product_PAGEDIMENSION"].ToString() == "750" ? "selected=selected" : "" %>>750</option>
                    </select>
                </li>
            </ul>
        </div>
    </td>
</tr>
<tr>
    <td class="ui-state ui-state-default" colspan="19" align="left" style="border-color: #DDD; border-top: none; border-bottom: none;">
        <div style="float: right;">
            <button type="button" onclick="ExportProducts()">
                <img src="../../Content/RibbonImages/excel.png" />
            </button>
        </div>

    </td>
</tr>


<script type="text/javascript">
    $(function () {
        $('a.edit').bind("click", function (e) {

            e.preventDefault();
            var dad = $(this).parent().parent();

            var lbl = dad.find('label');
            lbl.hide();
            dad.find('textarea').val(lbl.text()).show().css('height', '100%').focus();
        });

        $('textarea').focusout(function () {
            var productidforupdate = $(this).val();
            var productidd = $(this).attr("id");
            var columncategory = $(this).attr("column");
            var dadproduct = $(this).parent().parent().parent();
            var findproductid = dadproduct.attr("id");
            $.ajax({
                url: '/Product/ProductColumn',
                data: { id: productidd, productidforupdate: productidforupdate, columncategory: columncategory },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    var e = data;
                    if (e) {
                        $("#" + findproductid).css('background-color', '#86F886');
                    }
                    else {
                        $("#" + findproductid).css('background-color', '#FF6B6B');

                    }
                }
            });



            var dad = $(this).parent();
            $(this).hide();
            dad.find('label').text(this.value).show();
        });
    });


    function ExportProducts() {
        var data = {
            ProductNo: $('#ProductNo').val(),
            ProductName: $('#ProductName').val(),
            FirstCategoryName: $('#FirstCategoryName').val(),
            NameBrand: $('#NameBrand').val(),
            NameSeries: $('#NameSeries').val(),
            NameModel: $('#NameModel').val(),
            OtherBrand: $('#OtherBrand').val(),
            OtherModel: $('#OtherModel').val(),
            UserName: $('#UserName').val(),
            StoreName: $('#StoreName').val(),
            MemberType: $('#MemberType').val(),
            ProductPrice: $('#ProductPrice').val(),
            ProductRecordDate: $('#ProductRecordDate').val(),
            ProductLastViewDate: $('#ProductLastViewDate').val(),
            OrderName: $('#OrderName').val(),
            Order: $('#Order').val(),
            Page: $('#Page').val(),
            PageDimension: $('#PageDimension').val(),
            ProductActiveType: $('#ProductActiveType').val(),
            StoreMainPartyId: $('#StoreMainPartyId').val()
        };

        var s = "?" + $.param(data) + "";


        var url = '/Product/ExportProducts' + s;
        window.open(url);
    }
</script>
