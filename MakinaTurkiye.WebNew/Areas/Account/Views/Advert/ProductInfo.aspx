<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<ProductModel>"
    ValidateRequest="false" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript" src="/Content/v2/assets/js/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>

    <!--Test!-->

    <script type="text/javascript">
        function MakeMoneytoint(productPriceVal) {
            var productPrice = productPriceVal.replace(".", "").substring(0, (productPriceVal.indexOf(","))).replace(",", "");
            return productPrice;
        }
        function WriteTotalRecord() {


            var val = $("#ProductPrice1").val();
            var productPriceVal = val;

            var productPrice = MakeMoneytoint(productPriceVal);

            if ($("#DiscountType").val() == '<%:(byte)ProductDiscountType.Amount%>') {

                var newPrice = Number(productPrice) - Number($("#DiscountAmount").val());

                $("#TotalPrice").val(newPrice);


            }
            else if ($("#DiscountType").val() == '<%:(byte)ProductDiscountType.Percentage%>') {
                var newPrice = Number(productPrice) - Number(productPrice) * Number($("#DiscountAmount").val()) / 100;
                $("#TotalPrice").val(newPrice);
            }
        }
        $(document).ready(function () {
            $('#DiscountType').on('change', function () {

                if (this.value == '<%:(byte)ProductDiscountType.Amount%>') {
                    $("#DiscountTypeLabel").html("İndirim Miktarı");
                    $("#DiscountAmountContainer").show();
                    WriteTotalRecord()


                }
                else if (this.value == '<%:(byte)ProductDiscountType.Percentage%>') {
                    $("#DiscountTypeLabel").html("İndirim Yüzdesi");
                    $("#DiscountAmountContainer").show();
                    WriteTotalRecord();
                }
                else {
                    $("#DiscountAmountContainer").hide();

                }

            });

            $("#ProductPrice1").change(function () {
                var val = this.value;
                WriteTotalRecord();
            });
            $("#DiscountAmount").change(function () {
                WriteTotalRecord();
            });
            $('#ProductPrice1').maskMoney();
            $('#ProductPriceBegin').maskMoney();
            $('#ProductPriceLast').maskMoney();

            var type = $("input[name=productPriceType]:checked").val();
            ProductPriceType(type);
            $("#fakeProductType").change(function () {
                if ($("#fakeProductType [value=104]").attr("checked")) {
                    $('#r1edit:input').removeAttr('disabled');
                }
                else {
                    $('#r1edit:input').attr('disabled', true);
                }
            });

            $('input[rel=ProductName]').change(function () {
                var productName = $('#ProductName').val();
                $.ajax({
                    url: '/Account/ilan/CheckProductName',
                    data: { productname: productName },
                    type: "POST",
                    success: function (data) {

                        if (data) {
                            anyProductName = false;
                            $("#checkProduct").show();

                            $("#btnSkip").html("Yinede Devam Et");
                        }
                        else {
                            $("#checkProduct").hide();

                            $("#btnSkip").html("Devam");
                        }

                    }
                });
            });

            $.metadata.setType('attr', 'validate');

            $('#CountryId').DropDownCascading({
                method: "/Advert/Cities", target: "#CityId", loader: "#imgLoader"
            });

            $('#CityId').DropDownCascading({
                method: "/Advert/Localities", target: "#LocalityId", loader: "#imgLoader"
            });

            $('#LocalityId').DropDownCascading({
                method: "/Advert/Towns", target: "#TownId", loader: "#imgLoader"
            });

            $('#CityId').change(function () {
                $.ajax({
                    url: '/Membership/AreaCode',
                    type: 'post',
                    data: { CityId: $('#CityId').val() },
                    success: function (data) {
                        $('#InstitutionalPhoneAreaCode').val(data);
                        $('#InstitutionalPhoneAreaCode2').val(data);
                    }
                });
            });

        });

        function advertCheck() {
            var checkMenseiId = false;
            var checkCountry = true;
            var checkCity = true;
            var checkLocality = true;
            var checkTown = true;
            var checkName = true;
            var advertTime = false;
            var checkOrderStat = false;
            var checkWarrianty = true;
            var productPriceRange = true;
            var productPrice = true;
            var anyProductName = true;
            var pdetail = $('#warrianty').val();
            if (pdetail == "{ value = 86 }") {
                if ($('#WarrantyPeriod').val() == "0") {
                    checkWarrianty = false;
                    $('#warrantyValMes').show();
                }
                else {
                    checkWarrianty = true;
                    $('#warrantyValMes').hide();
                }
            }
            else {
                var checkWarrianty = true;
            }

            if ($('#ProductName').val() == "") {
                $('#pNameValMes').show();
                checkName = false;
            }
            else
                checkName = true;

            if ($('#ProductPublicationDate').val() == "") {
                $('#advertTimeValMes').show();
                advertTime = false;
            }
            else {
                $('.AdverTime').each(function myfunction(index, value) {
                    if ($(this).attr('checked') == 'checked') {
                        advertTime = true;
                        $('#advertTimeValMes').hide();
                    }
                });
            }

            var hasRecord = false;
            $('.ActiveName').each(function myfunction(index, value) {
                if ($(this)[0].checked) {
                    hasRecord = true;
                }
            });
            if ($("#UnitType").val() == "0") {
                alert("Lütfen ürün birimi seçiniz");
                return false;
            }

            var hasRecord2 = false;
            $('.ActiveName2').each(function myfunction(index, value) {
                if ($(this)[0].checked) {
                    hasRecord2 = true;
                }
            });

            var ptype = $('#ProductType').val();
            if (ptype != 104) {

                if ($('#MenseiId').val() == "0") {
                    $('#divMenseiId').attr('class', 'col-sm-5 col-md-3 validationdropdownAddress');
                    $('#divMenseiIdValMes').show();
                    checkMenseiId = false;
                }
                else if ($('#MenseiId').val() == "9999") {
                    $('#divMenseiId').attr('class', 'col-sm-5 col-md-3 validationdropdownAddress');
                    checkMenseiId = true;
                    $('#divMenseiIdValMes').hide();
                }
                else {
                    $('#divMenseiId').attr('class', 'col-sm-5 col-md-3 dropdownAddress');
                    checkMenseiId = true;
                    $('#divMenseiIdValMes').hide();
                }

                if ($('#OrderStatus').val() == "0") {
                    checkOrderStat = false;
                    $('#ordStatValMes').show();
                }
                else {
                    checkOrderStat = true;
                    $('#ordStatValMes').hide();
                }
            }
            else {
                checkMenseiId = true;
                checkOrderStat = true;
            }

            var type = $("input[name=productPriceType]:checked").val();

            if (type == "<%:(byte)ProductPriceType.PriceRange%>") {
                if (firstPrice != "" && secondPrice != "") {
                    var firstPrice = $("#ProductPriceBegin").val().replace(',', "").replace(".", "");
                    var secondPrice = $("#ProductPriceLast").val().replace(',', "").replace(".", "");

                    firstPrice = parseInt(firstPrice);
                    secondPrice = parseInt(secondPrice);
                    if (firstPrice >= secondPrice) {
                        productPriceRange = false;
                        $("#productPriceRangeVal").show();

                    }
                }

            }
            else if (type == "<%:(byte)ProductPriceType.Price%>") {

                var productPriceCheck = $("#ProductPrice1").val();
                if (productPriceCheck == "" || productPriceCheck == "0") {
                    $("#productPriceVal").show();
                    productPrice = false;

                }
                if ($("#DiscountType").val() != "0" && $("#DiscountAmount").val() == "") {
                    alert("Lütfen indirim miktarı/oranı giriniz.");
                    return false;
                }


            }

            var adress = $('#chkAdress').attr('checked');
            if (adress == 'checked') {
                if ($('#CountryId').val() == "0") {
                    $('#countryValMes').show();
                    checkCountry = false;
                }
                else {
                    $('#countryValMes').hide();
                    checkCountry = true;
                }

                if ($('#CityId').val() == "0") {
                    $('#divCityId').attr('class', 'col-sm-5 col-md-3 validationdropdownAddress');
                    checkCity = false;
                    $('#cityValMes').show();
                }
                else {
                    $('#divCityId').attr('class', 'col-sm-5 col-md-3 dropdownAddress');
                    checkCity = true;
                    $('#cityValMes').hide();
                }

                if ($('#LocalityId').val() == "0") {
                    $('#divLocalityId').attr('class', 'col-sm-5 col-md-3 validationdropdownAddress');
                    checkLocality = false;
                    $('#localValMes').show();
                }
                else {
                    $('#divLocalityId').attr('class', 'col-sm-5 col-md-3 dropdownAddress');
                    checkLocality = true;
                    $('#localValMes').hide();
                }


                if ($('#TownId').val() == "0") {
                    $('#divTownId').attr('class', 'col-sm-5 col-md-3 validationdropdownAddress');
                    checkTown = false;
                    $('#townValMes').show();
                }
                else {
                    $('#divTownId').attr('class', 'col-sm-5 col-md-3 dropdownAddress');
                    checkTown = true;
                    $('#townValMes').hide();
                }
            }

            if (!hasRecord) {
                $('#divProductType').show();
                window.location = '#divProductType';
            }
            else {
                $('#divProductType').hide();
            }

            if (!hasRecord2) {
                $('#divProductStatu').show();
                window.location = '#divProductStatu';
            }
            else {
                $('#divProductStatu').hide();
            }


            if (checkCountry && checkCity && checkLocality && checkOrderStat && checkTown && hasRecord && hasRecord2 && checkMenseiId && checkName && advertTime && productPriceRange && productPrice) {
                return true;
                alert("GÖnderildi");
            }

            return false;

        }
        function Comma(Num) { //function to add commas to textboxes
            Num += '';
            Num = Num.replace('.', ''); Num = Num.replace('.', ''); Num = Num.replace('.', '');
            Num = Num.replace('.', ''); Num = Num.replace('.', ''); Num = Num.replace('.', '');
            x = Num.split(',');
            x1 = x[0];
            x2 = x.length > 1 ? ',' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + '.' + '$2');
            return x1 + x2;
        }
        function isNumberKeyForm(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function ProductPriceType(type) {
            if (type == "<%:(byte)ProductPriceType.Price%>") {
                $("#price-wrapper").show();
                $("#price-range").hide();
                $("#currencyWrapper").show();
                $("#DiscountContainer").show();


            }
            else if (type == "<%:(byte)ProductPriceType.PriceRange%>") {
                $("#price-wrapper").hide();
                $("#price-range").show();
                $("#currencyWrapper").show();
                $("#DiscountContainer").hide();


            }
            else if (type == "<%:(byte)ProductPriceType.PriceDiscuss%>") {
                $("#price-wrapper").hide();
                $("#priceKdvWrapper").hide();
                $("#price-range").hide();
                $("#DiscountContainer").hide();
            }
            else if (type == "<%:(byte)ProductPriceType.PriceAsk%>") {
                $("#price-wrapper").hide();
                $("#priceKdvWrapper").hide();
                $("#price-range").hide();
                $("#currencyWrapper").hide();
                $("#DiscountContainer").hide();

            }

        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">İlan Ekle
            </h4>
        </div>
    </div>
    <div class="row">

        <div class="col-sm-12 col-md-12 ">
            <div>
                <div class="well store-panel-container">
                    <div>
                        <div>
                            <div class="row">

                                <div class="col-xs-12">
                                    <div class="col-md-2"></div>
                                    <% List<string> varCategoryTreeName = (List<string>)Session["TreeListForCategoryName"]; %>
                                    <%
                                        for (int i = varCategoryTreeName.Count - 1; i >= 0; i--)
                                        { %>
                                    <b>
                                        <%=varCategoryTreeName[i].ToString()%>
                                    </b>>
                                    <% } %>
                                    <%--<% foreach (string item in varCategoryTreeName)
                       { %>
                       <b>
                        <%=item%>
                        </b>
                        >

                    <% } %>--%>
                                    <br>
                                    <br>
                                    <span class="clearfix"></span>
                                </div>
                            </div>
                            <hr>
                            <%using (Html.BeginForm("ProductInfo", "Advert", FormMethod.Post, new { id = "formAdvert product-add-form", @class = "form-horizontal", role = "form" }))
                                {
                                    var constantItems = Model.ConstantItems;%>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Ürün İsmi
                                </label>
                                <div class="col-sm-10">
                                    <%:Html.TextBoxFor(model => model.ProductName, new { @class = "form-control", size = "97", maxlength = "97", @rel = "ProductName" })%>
                                    <br />
                                    <label id="pNameValMes" class="label label-danger small" style="display: none;">
                                        Lütfen Ürün Adı alanını giriniz.
                                    </label>

                                    <label id="checkProduct" class="label label-warning" style="display: none;">
                                        Bu şekilde tanımlanan ürün adı mevcuttur, Yinede devam edebilirsiniz.
                                    </label>

                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Ürün Detayı:
                                </label>
                                <div class="col-sm-10">
                                    <%:Html.TextAreaFor(model => model.ProductDescription, new { style = "width:640px; height:355px;" })%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Anahtar Kelimeler:</label>
                                <div class="col-sm-10">
                                    <input type="text" name="Keywords" data-role="tagsinput" class="form-control col-md-8" /><br />
                                    <small>Virgül ile ayırınız</small>
                                </div>
                            </div>

                            <%foreach (var item in Model.MTProductPropertieModel.MTProductProperties)
                                {%>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    <%:item.DisplayName %>:
                                </label>
                                <div class="col-sm-5 col-md-3 ">
                                    <%if (item.Type == (byte)PropertieType.Text)
                                        {%>
                                    <input type="text" name="<%:item.InputName %>" class="form-control" />
                                    <% }
                                        else if (item.Type == (byte)PropertieType.Editor)
                                        {%>
                                    <textarea id="<%:item.InputName %>" name="<%:item.InputName %>"></textarea>
                                    <script type="text/javascript" defer="defer">
                                        var editor = CKEDITOR.replace('<%:item.InputName%>', { toolbar: 'webtool' });
                                        CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
                                    </script>
                                    <% }
                                        else
                                        {%>
                                    <%:Html.DropDownList(item.InputName, item.Attrs, new { @class = "form-control" }) %>
                                    <% } %>
                                </div>
                            </div>

                            <%} %>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Ürün Tipi:
                                </label>
                                <div class="col-sm-10">
                                    <% foreach (var itemProductType in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductType).OrderBy(k => k.Order))
                                        { %>
                                    <label class="radio-inline">
                                        <%:Html.RadioButton("fakeProductType", itemProductType.ConstantId, false, new { @class = "ActiveName" })%>
                                        <%: itemProductType.ConstantName%>
                                    </label>
                                    <% } %>
                                    <script type="text/javascript">
                                        $('.ActiveName').click(function () {
                                            $('#ProductType').val($(this).val());
                                            var ptype = $('#ProductType').val();
                                            if (ptype == 104) {
                                                $('#pselect').hide();
                                                $('#MenseiId').val(0);
                                                $('#OrderStatus').val(0);
                                            }
                                            else {
                                                $('#pselect').show();
                                            }
                                        });
                                    </script>
                                    <input id="ProductType" name="ProductType" type="hidden" />
                                    <div class="bs-example bs-example-bg-classes">
                                        <label id="divProductType" class="label label-danger" style="display: none;">
                                            Lütfen ürün tipini seçiniz.
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Ürün Durumu:
                                </label>
                                <div class="col-sm-10">
                                    <% foreach (var itemProductStatu in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductStatu).OrderBy(k => k.Order))
                                        { %>
                                    <label class="radio-inline">
                                        <%:Html.RadioButton("fakeProductStatu", itemProductStatu.ConstantId, false, new { @class = "ActiveName2" })%>
                                        <%: itemProductStatu.ConstantName%>
                                    </label>
                                    <% } %>
                                    <script type="text/javascript">
                                        $('.ActiveName2').click(function () {
                                            $('#ProductStatu').val($(this).val());

                                            var ptype = $('#ProductStatu').val();
                                            if (ptype == 73) {
                                                $('#pstatu').show();
                                            }
                                            else {
                                                $('#pstatu').hide();
                                                $('#ModelYear').val(0);
                                            }
                                        });
                                    </script>
                                    <input id="ProductStatu" name="ProductStatu" type="hidden" />
                                    <div class="bs-example bs-example-bg-classes">
                                        <label id="divProductStatu" class="label label-danger" style="display: none;">
                                            Lütfen ürün durumunu seçiniz.
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Satış Detayı:
                                </label>
                                <div class="col-sm-10">
                                    <% foreach (var itemProductSalesType in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductSalesType).OrderBy(k => k.Order))
                                        { %>
                                    <label class="checkbox-inline">
                                        <%:Html.CheckBox("ProductSalesType", new { value = itemProductSalesType.ConstantId })%>
                                        <%: itemProductSalesType.ConstantName%>
                                    </label>
                                    <% } %>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Kısa Detay:
                                </label>
                                <div class="col-sm-10">
                                    <% foreach (var itemProductBriefDetail in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductBriefDetail).OrderBy(k => k.Order))
                                        { %>
                                    <label class="radio-inline">
                                        <%:Html.RadioButton("ProductBriefDetail", itemProductBriefDetail.ConstantId, new { @class = "pdetail" })%>
                                        <%: itemProductBriefDetail.ConstantName%>
                                        <input type="text" id="warrianty" style="display: none" />
                                    </label>
                                    <% } %>
                                    <script type="text/javascript">
                                        $('.pdetail').click(function () {
                                            var pdetail = $(this).val();
                                            $('#warrianty').val(pdetail);
                                            if (pdetail == '86') {
                                                $('#pdetail').show();
                                            }
                                            else {
                                                $('#pdetail').hide();
                                            }
                                        });

                                    </script>
                                </div>
                            </div>

                            <div id="pstatu" style="display: none;">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Model Yılı
                                    </label>
                                    <div class="col-sm-2">
                                        <%:Html.TextBox("DropDownModelDate", "", new { @class = "form-control", size = "10" })%>
                                    </div>
                                </div>
                            </div>
                            <div id="pdetail" style="display: none;">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Garanti Süresi
                                    </label>
                                    <div class="col-sm-3">
                                        <select class="form-control" name="WarrantyPeriod" id="WarrantyPeriod">
                                            <option value="0">< Seçiniz ></option>
                                            <%for (int i = 1; i < 11; i++)
                                                { %>
                                            <option value="<%: i%>">
                                                <%: i%>
                                                Yıl</option>
                                            <% } %>
                                        </select>
                                    </div>
                                    <div class="col-sm-5 bs-example bs-example-bg-classes">
                                        <label id="warrantyValMes" class="label label-danger" style="display: none;">
                                            Lütfen garanti süresi seçiniz.
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div id="pselect">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Mensei
                                    </label>
                                    <div id="divMenseiId" class="col-sm-5 col-md-3 dropdownAddress">
                                        <%:Html.DropDownListFor(model => model.MenseiId, new SelectList(Model.TheOriginItems, "ConstantId", "ConstantName"), new { @class = "form-control" })%>
                                    </div>
                                    <div class="col-sm-5 bs-example bs-example-bg-classes">
                                        <label id="divMenseiIdValMes" class="label label-danger" style="display: none;">
                                            Lütfen menşei seçiniz.
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Teslim Durumu
                                    </label>
                                    <div class="col-sm-5 col-md-3 dropdownAddress" id="div1">
                                        <%:Html.DropDownListFor(model => model.OrderStatus, new SelectList(Model.SiparisList, "ConstantId", "ConstantName"), new { @class = "form-control" })%>
                                    </div>
                                    <div class="col-sm-5 bs-example bs-example-bg-classes">
                                        <label id="ordStatValMes" class="label label-danger" style="display: none;">
                                            Lütfen teslim durumunu seçiniz.
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Konum
                                </label>
                                <div class="col-sm-5 col-md-3">
                                    <% var town = Model.TownItems.FirstOrDefault(p => p.Value == Model.TownId.ToString()).Text;
                                        var locality = Model.LocalityItems.FirstOrDefault(p => p.Value == Model.LocalityId.ToString()).Text;
                                        var city = Model.CityItems.FirstOrDefault(p => p.Value == Model.CityId.ToString()).Text;
                                        var country = Model.CountryItems.FirstOrDefault(p => p.Value == Model.CountryId.ToString()).Text;
                                        var adress = string.Format("{0} {1} / {2} {3}", town, locality, city, country);
                                    %>
                                    <textarea class="form-control" disabled="disabled"><%: adress %></textarea>
                                    <label class="checkbox-inline">
                                        <%:Html.CheckBox("chkAdress", false, new { @id = "chkAdress" })%>
                                        İlan adresiniz değilse değiştirebilirsiniz..
                                    </label>
                                    <script type="text/javascript">
                                        $('#chkAdress').click(function () {
                                            var adress = $(this).attr('checked');
                                            if (adress == 'checked') {
                                                $('#adverAdress').show();
                                            }
                                            else {
                                                $('#adverAdress').hide();
                                            }
                                        });

                                    </script>
                                </div>
                            </div>
                            <div id="adverAdress" style="display: none">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Ülke
                                    </label>
                                    <div id="divCountryId" class="col-sm-5 col-md-3 dropdownAddress">
                                        <%:Html.DropDownListFor(model => model.CountryId, Model.CountryItems, new { @class = "form-control" })%>
                                    </div>
                                    <div class="bs-example bs-example-bg-classes">
                                        <label id="countryValMes" class="label label-danger" style="display: none;">
                                            Lütfen ülke seçiniz.
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Şehir
                                    </label>
                                    <div id="divCityId" class="col-sm-5 col-md-3 dropdownAddress">
                                        <%:Html.DropDownListFor(model => model.CityId, Model.CityItems, new { @class = "form-control" })%>
                                    </div>
                                    <div class="bs-example bs-example-bg-classes">
                                        <label id="cityValMes" class="label label-danger" style="display: none;">
                                            Lütfen şehir seçiniz.
                                        </label>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Semt
                                    </label>
                                    <div id="divLocalityId" class="col-sm-5 col-md-3 dropdownAddress">
                                        <%:Html.DropDownListFor(model => model.LocalityId, Model.LocalityItems, new { @class = "form-control" })%>
                                        <div class="bs-example bs-example-bg-classes">
                                            <label id="localValMes" class="label label-danger" style="display: none;">
                                                Lütfen semt seçiniz.
                                            </label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Mahalle / Köy
                                    </label>
                                    <div id="divTownId" class="col-sm-5 col-md-3 dropdownAddress">
                                        <%:Html.DropDownListFor(model => model.TownId, Model.TownItems, new { @class = "form-control" })%>
                                    </div>
                                    <div class="bs-example bs-example-bg-classes">
                                        <label id="townValMes" class="label label-danger" style="display: none;">
                                            Lütfen mahalle/köy seçiniz.
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    İlan Süresi
                                </label>
                                <div class="col-sm-3 col-lg-1">
                                    <%:Html.TextBox("ProductPublicationDate", "", new { @class = "form-control" })%>
                                    <br />
                                    <label id="advertTimeValMes" class="label label-danger" style="display: none;">
                                        Lütfen ilan süresini giriniz.
                                    </label>
                                </div>
                                <div class="col-sm-7">
                                    <div class="radio-inline">
                                        <label>
                                            <%:Html.RadioButton("ProductPublicationDateType", "1", false, new { @class = "AdverTime" })%>
                                            Gün
                                        </label>
                                    </div>
                                    <div class="radio-inline">
                                        <label>
                                            <%:Html.RadioButton("ProductPublicationDateType", "2", false, new { @class = "AdverTime" })%>
                                            Ay
                                        </label>
                                    </div>
                                    <div class="radio-inline">
                                        <label>
                                            <%:Html.RadioButton("ProductPublicationDateType", "3", true, new { @class = "AdverTime" })%>
                                            Yıl
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" <%:Model.AllowSellUrl == false ? "style=display:none" : "" %>>
                                <label class="col-sm-2 control-label">
                                    Harici Ürün Satış Linkiniz
                                </label>
                                <div class="col-md-5">
                                    <%:Html.TextBoxFor(x => x.ProductSellUrl, new { @class = "form-control" }) %>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Fiyat Tipi</label>
                                <div class="col-md-8">
                                    <%foreach (var productPriceTypeItem in Model.ProductPriceTypes)
                                        {
                                            bool productChoose = false;
                                    %>

                                    <%if (productPriceTypeItem.ContstantPropertie == Convert.ToString((byte)ProductPriceType.Price)) { productChoose = true; } %>
                                    <div class="radio-inline"><%:Html.RadioButton("productPriceType", productPriceTypeItem.ContstantPropertie, productChoose, new { @onclick = "ProductPriceType(" + productPriceTypeItem.ContstantPropertie + ")" })%><%:productPriceTypeItem.ConstantName %></div>
                                    <%} %>
                                </div>
                            </div>
                            <div id="price-wrapper">


                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Fiyat
                                    </label>
                                    <div class="col-sm-3 col-lg-3">
                                        <input type="text" class="form-control" name="ProductPrice1" id="ProductPrice1" data-thousands="." data-decimal="," />
                                        <label id="productPriceVal" class="label label-danger" style="display: none;">
                                            Fiyatı giriniz
                                        </label>
                                    </div>

                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        İndirim Tipi
                                    </label>
                                    <div class="col-sm-3 col-lg-3">
                                        <select name="DiscountType" id="DiscountType" class="form-control">
                                            <option value="0">İndirim Yok</option>
                                            <option value="<%:(byte)ProductDiscountType.Percentage %>">Yüzdelik İndirim</option>
                                            <option value="<%:(byte)ProductDiscountType.Amount %>">Miktar İndirimi</option>
                                        </select>
                                    </div>
                                </div>

                                <div id="DiscountAmountContainer" style="display: none;">
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label" id="DiscountTypeLabel">
                                            İndirim Miktarı 
                                        </label>
                                        <div class="col-sm-3 col-lg-3">
                                            <%:Html.TextBoxFor(x => x.DiscountAmount, new {@class="form-control" }) %>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <label class="col-sm-2 control-label">
                                            Yeni Fiyat
                                        </label>
                                        <div class="col-sm-3">
                                            <%:Html.TextBoxFor(x=>x.TotalPrice,new {@class="form-control" }) %>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group" id="price-range" style="display: none;">
                                <label class="col-sm-2 control-label">
                                    Fiyat Aralığı
                                </label>
                                <div class="col-md-4 col-lg-2 pr0">
                                    <input type="text" name="ProductPriceBegin" id="ProductPriceBegin" size="3" class="form-control" data-thousands="." data-decimal="," />
                                </div>

                                <div class="col-md-4 col-lg-2  pl2">
                                    <input type="text" name="ProductPriceLast" id="ProductPriceLast" size="3" class="form-control" data-thousands="." data-decimal="," />
                                </div>
                                <div class="bs-example bs-example-bg-classes">
                                    <label id="productPriceRangeVal" class="label label-danger" style="display: none;">
                                        Başlanıç fiyatı son fiyatından düşük olmalıdır
                                    </label>
                                </div>
                            </div>

                            <div class="form-group" id="priceKdvWrapper">
                                <label class="col-sm-2 control-label">
                                </label>

                                <div class="col-xs-4 col-md-4">

                                    <label><%:Html.RadioButton("pricePropertie", "kdvdahil",true)%> Kdv Dahil</label>
                                    <label><%:Html.RadioButton("pricePropertie","kdvharic")%> Kdv Hariç</label>
                                    <label><%:Html.RadioButton("pricePropertie", "fob")%> Fob</label>
                                </div>
                            </div>
                            <div class="form-group" id="currencyWrapper">
                                <label class="col-sm-2 control-label">
                                    Birim
                                </label>
                                <div class="col-xs-2 col-lg-2 pr0">
                                    <select class="form-control" name="UnitType">
                                        <option value="0">< Seçiniz ></option>
                                        <% foreach (var itemProductBriefDetail in constantItems.Where(c => c.ConstantType == (byte)ConstantType.Birim).OrderBy(k => k.Order))
                                            { %>
                                        <option value="<%: itemProductBriefDetail.ConstantId%>">
                                            <%: itemProductBriefDetail.ConstantName%></option>
                                        <% } %>
                                    </select>
                                </div>
                                <div class="col-xs-3 col-md-2">
                                    <%: Html.DropDownListFor(m => m.CurrencyId, Model.CurrencyItems, new { @class="form-control"})%>
                                </div>
                            </div>
                            <%if (ViewData["ProductSales"].ToBoolean() == true)
                                {  %>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">
                                    Ürün satışa hazır
                                </label>
                                <div class="col-sm-3">
                                    <div class="checkbox">
                                        <label>
                                            <%:Html.CheckBoxFor(m => m.ReadyforSale, Model.ReadyforSale)%>
                                        </label>
                                    </div>
                                </div>
                            </div>


                            <% } %>
                            <%if (Model.CertificateTypes.Count > 0)
                                {%>
                            <div class="form-group">
                                <label class="col-sm-2 control-label">Sertifikalar</label>
                                <div class="col-sm-10">
                                    <%foreach (var item in Model.CertificateTypes)
                                        {%>
                                    <label class="checkbox-inline">
                                        <input type="checkbox" name="certificateTypes" checked value="<%:item.Value %>" />
                                        <%:item.Text %>
                                    </label>
                                    <% } %>
                                </div>
                            </div>
                            <% } %>

                            <div class="form-group">
                                <div class="col-sm-offset-3 col-sm-9 btn-group">
                                    <button type="submit" class="btn btn-primary" id="btnSkip" onclick="return advertCheck();">
                                        Devam</button>
                                </div>
                            </div>
                            <% } %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" defer="defer">
        var editor = CKEDITOR.replace('ProductDescription', { toolbar: 'webtool', format_tags: 'p;h1;h2;h3;h4;h5;h6;pre;address;div', enterMode: CKEDITOR.ENTER_DIV });
        CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
    </script>
</asp:Content>
