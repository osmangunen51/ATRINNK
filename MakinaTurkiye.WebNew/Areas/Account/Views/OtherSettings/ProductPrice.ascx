<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.MTProductItem>>" %>
<div class="col-sm-12 col-md-12">
    <div>
        <h4 class="mt0 text-info">
            <span class="text-primary glyphicon glyphicon-cog"></span>&nbsp;İlan Ürün Fiyatı Güncelleme
        </h4>
        <div class="well well-mt2">
            <div class="alert alert-info">
                Burada sadece fiyatlarını girdiğiniz ilanlarınız gözükmetedir. Eğer fiyat sorunuz/görüşünüz ise ilanınız bu alanda görünmemektedir. 
            </div>

            <%if (Model.Source.Count != 0)
                {%>
            <div id="Advert">
                <%=Html.RenderHtmlPartial("AdvertPagingfor",Model) %>
            </div>
            <%} %>

            <hr>
        </div>
    </div>
</div>
<div class="modal fade" id="PriceModalEdit" tabindex="-1"
    role="dialog" aria-labelledby="helpModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close"
                    data-dismiss="modal">
                    <span aria-hidden="true">&times;
                    </span><span class="sr-only">Kapat</span></button>
                <h4 class="modal-title" id="H1">Fiyat Güncelleme</h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <%var productPriceTypes = (IEnumerable<global::MakinaTurkiye.Entities.Tables.Common.Constant>)ViewData["ProductPriceTypes"]; %>
                    <%foreach (var item in productPriceTypes)
                        {
                            bool productChoose = false;
                    %>
                    <%if (item.ContstantPropertie == Convert.ToString((byte)ProductPriceType.Price)) { productChoose = true; } %>
                    <div class="radio-inline"><%:Html.RadioButton("productPriceType", item.ContstantPropertie, productChoose, new { @onclick="ProductPriceType("+item.ContstantPropertie+")"})%><%:item.ConstantName %></div>
                    <%} %>
                </div>
                <input type="hidden" id="hdnPriceId" name="hdnPriceId" class="form-control" />
                <div class="row" id="price-wrapper">
                    <input type="text" id="PriceValue" name="PriceValue" class="form-control" />
                </div>
                <div id="price-range" class="row" style="display: none;">
                    <div class="col-xs-2 col-lg-2 pr0">
                        <%:Html.TextBox("ProductPriceBegin", "", new { size = "10",@class="form-control" })%>
                    </div>
                    <div class="col-xs-2 col-lg-2  pl2">
                        <%:Html.TextBox("ProductPriceLast", "", new { size = "3", @class = "form-control" })%>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" onclick="PriceEdit()" class="btn btn-default">
                    Güncelle</button>
                <button type="button"
                    class="btn btn-default" data-dismiss="modal">
                    Vazgeç</button>
            </div>
        </div>
    </div>
</div>
