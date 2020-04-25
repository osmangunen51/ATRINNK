<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<ProductModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript" src="/Content/v2/assets/js/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>

    <script src="/Scripts/jquery.maskMoney.js" type="text/javascript"></script>
    <script type="text/javascript">
        function MakeMoneytoint(productPriceVal) {
            var productPrice = 0;
            if (productPriceVal.indexOf(",") > 0) {
                productPrice = productPriceVal.replace(".", "").substring(0, (productPriceVal.indexOf(","))).replace(",", "");
            }
            else {
                productPrice = productPriceVal.substring(0, (productPriceVal.indexOf("."))).replace(".", "");
            }
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

            $('#DiscountType').change(function () {

                if (this.value == '<%:(byte)ProductDiscountType.Amount%>') {
                    $("#DiscountTypeLabel").html("İndirim Miktarı");
                    $("#DiscountAmountContainer").show();
                    WriteTotalRecord();


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
                WriteTotalRecord();
            });
            $("#DiscountAmount").change(function () {
                WriteTotalRecord();
            });

            $('#ProductPrice1').maskMoney();
            $('#ProductPriceBegin').maskMoney();
            $('#ProductPriceLast').maskMoney();

            $('input[rel=ProductName]').change(function () {
                var productName = $('#ProductName').val();
                $.ajax({
                    url: '/Account/ilan/CheckProductName',
                    data: { productname: productName },
                    type: "POST",
                    success: function (data) {
                        isProductName = (data == "true" ? true : false);
                        if (isProductName) {
                            anyProductName = false;
                            $("#checkProduct").show();

                            $("#saveProductButton").html("Yinede Kaydet");
                        }
                        else {
                            $("#checkProduct").hide();

                            $("#saveProductButton").html("Kaydet");
                        }

                    }
                });
            });

            var type = $("input[name=productPriceType]:checked").val();
            if (type == "<%:(byte)ProductPriceType.Price%>") {
                $("#price-wrapper").show();
                $("#price-range").hide();
                $("#currencyWrapper").show();
                $("#ProductPriceBegin").val("");
                $("#ProductPriceLast").val("");
                $("#DiscountAmountContainer").show();

            }
            else if (type == "<%:(byte)ProductPriceType.PriceRange%>") {
                $("#price-wrapper").hide();
                $("#price-range").show();
                $("#currencyWrapper").show();
                $("#ProductPrice1").val("");
                $("#DiscountAmountContainer").hide();
            }
            else {
                $("#price-wrapper").hide();
                $("#price-range").hide();
                $("#currencyWrapper").hide();
                $("#ProductPriceBegin").val("");
                $("#ProductPriceLast").val("");
                $("#ProductPrice1").val("");

                $("#DiscountAmountContainer").hide();
            }


            $("#OtherBrandClick").click(function () {
                $("#BrandNameDisplay").hide();
                $("#OtherBrandWrapper").show();
                $(this).hide();
            });
            $("#OtherBrandSave").click(function () {
                var otherBrand = $("#OtherBrandText").val();
                otherBrand = $.trim(otherBrand);
                if (otherBrand != "") {
                    $("#BrandNameDisplayName").html(otherBrand);
                    $("#BrandNameDisplay").show();
                    $("#OtherBrandWrapper").hide();
                    $("#OtherBrandClick").show();
                }
                else {
                    $("#BrandNameDisplay").show();
                    $("#OtherBrandWrapper").hide();
                    $("#OtherBrandClick").show();
                }
            });
            $("#OtherBrandCancel").click(function () {

                var otherBrand = $("#OtherBrandText").val();
                otherBrand = $.trim(otherBrand);
                if (otherBrand != "") {
                    $("#BrandNameDisplay").show();
                    $("#OtherBrandWrapper").hide();
                    $("#OtherBrandClick").show();
                }
                else {
                    $("#BrandNameDisplay").show();
                    $("#OtherBrandWrapper").hide();
                    $("#OtherBrandClick").show();
                }
            });



            $("#OtherModelClick").click(function () {
                $("#ModelNameDisplay").hide();
                $("#OtherModelWrapper").show();
                $(this).hide();


            });
            $("#OtherModelSave").click(function () {

                var otherBrand = $("#OtherModelText").val();
                otherBrand = $.trim(otherBrand);
                if (otherBrand != "") {
                    $("#ModelNameDisplayName").html(otherBrand);
                    $("#ModelNameDisplay").show();
                    $("#OtherModelWrapper").hide();
                    $("#OtherModelClick").show();
                }
                else {
                    $("#ModelNameDisplay").show();
                    $("#OtherModelWrapper").hide();
                    $("#OtherModelClick").show();
                }
            });
            $("#OtherModelCancel").click(function () {

                var otherBrand = $("#OtherModelText").val();
                otherBrand = $.trim(otherBrand);
                if (otherBrand != "") {
                    $("#ModelNameDisplay").show();
                    $("#OtherModelWrapper").hide();
                    $("#OtherModelClick").show();
                }
                else {
                    $("#ModelNameDisplay").show();
                    $("#OtherModelWrapper").hide();
                    $("#OtherModelClick").show();
                }
            });


            var ptype = "<%= Model.ProductType%>";
            if (ptype == 104) {
                $('#pselect').hide();
                $('#MenseiId').val(0);
                $('#OrderStatus').val(0);
            }
            else {
                $('#pselect').show();
            }
            var utype = "<%= Model.UnitType%>";
            $('#UnitType').val(utype);
            var pdet = "<%= Model.BriefDetail%>";
            if (pdet == '86') {
                $('#pdetail').show();
            }
            else {
                $('#pdetail').hide();
            }
            var ptype = $('#ProductStatu').val();
            if (ptype == 73) {
                $('#pstatu').show();
            }
            else {
                $('#pstatu').hide();
                $('#ModelYear').val(0);
            }

            $('#CountryId').DropDownCascading({
                method: '/Areas/Account/Advert/Cities', target: "#CityId", loader: "#imgLoader"
            });

            $('#CityId').DropDownCascading({
                method: "/Advert/Localities", target: "#LocalityId", loader: "#imgLoader"
            });

            $('#LocalityId').DropDownCascading({
                method: "/Advert/Towns", target: "#TownId", loader: "#imgLoader"
            });
        });

        function DeletePicture(pictureId, pictureName) {
            if (confirm('Resmi Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Account/ilan/DeletePictureEdit',
                    type: 'Post',
                    data:
                    {
                        ProductId: <%=this.RouteData.Values["id"] %>,
                        PictureId: pictureId,
                        PictureName: pictureName
                    },
                    success: function (data) {
                        $('#divPictureList').html(data);
                        alert('Resim başarıyla silinmiştir');
                    },
                    error: function (x, l, e) {
                        alert(x.responseText);
                    }
                });
            }
        }

        function mainImage(productID, picturePath) {
            var h = [];
            $("#divPictureList div").each(function () { h.push($(this).attr('id').substr(9)); });
            console.log(h);
            $.ajax({
                url: '/Account/ilan/mainImageEdit',
                type: 'post',
                data: { idArray: " " + h + "", productID: productID, picturePath: picturePath },
                success: function (data) {
                    $('#divPictureList').html(data);
                },
                error: function (x, l, e) {
                    alert(x.responseText);
                }
            });
        }



        function DeleteVideo(productId, videoId) {
            if (confirm('Videoyu Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Account/ilan/DeleteVideoEdit',
                    type: 'Post',
                    data:
                    {
                        ProductId: productId,
                        VideoId: videoId
                    },
                    success: function (data) {
                        $('#divVideoList').html(data);
                        alert('Video başarıyla silinmiştir');
                        location.reload();
                    },
                    error: function (x, l, e) {
                        alert(x.responseText);
                    }
                });
            }
        }



        onload = function () {
            $('#CountryId').val(246);
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

        function advertCheck() {

            console.log("unittye", $("#UnitType").val());
            var a = $("input[id^='ProductSalesType']:checkbox:checked");

            var fakeType = $("input[name='fakeProductType']:checked").val();

            if (a.length == 0) {
                alert("Satış Durumunu Seçiniz.");
                return false;
            }
            else if ($("#MenseiId").val() == "0" && fakeType != "104") {
                alert("Ürün Mensei Seçmelisiniz.")
                return false;
            }
            else if ($("#OrderStatus").val() == "0" && fakeType != "104") {
                alert("Teslim Durumunu Seçiniz");
                return false;
            }

            var type = $("input[name=productPriceType]:checked").val();
            if (type == "<%:(byte)ProductPriceType.PriceRange%>") {

                var firstPrice = $("#ProductPriceBegin").val().replace(',', "").replace(".", "");
                var secondPrice = $("#ProductPriceLast").val().replace(',', "").replace(".", "");
                if (firstPrice != "" && secondPrice != "") {
                    firstPrice = parseInt(firstPrice);
                    secondPrice = parseInt(secondPrice);
                    if (firstPrice >= secondPrice) {
                        $("#ProductPriceBegin").focus();
                        $("#productPriceRangeVal").show();
                        alert("Fiyat başlangıç değeri son değerinden büyük olamaz.");
                        return false;
                    }
                }
                else {
                    alert("Fiyat başlangıç ve bitiş değerilerini giriniz.");
                    $("#ProductPriceBegin").focus();
                    return false;
                }


                if ($("#UnitType").val() == "0" || $("#UnitType").val() == null) {
                    alert("Lütfen Birim Seçiniz");
                    return false;
                }

            }
            else if (type == "<%:(byte)ProductPriceType.Price%>") {

                var productPriceCheck = $("#ProductPrice1").val();
                if (productPriceCheck == "" || productPriceCheck == null) {
                    $("#ProductPriceError").show();
                    $("#ProductPrice1").focus();
                    return false;
                }
                if ($("#UnitType").val() == "0" || $("#UnitType").val() == null) {
                    alert("Lütfen Birim Seçiniz");
                    return false;
                }

            }
        }
        function ProductPriceType(type) {
            if (type == "<%:(byte)ProductPriceType.Price%>") {
                $("#price-wrapper").show();
                $("#price-range").hide();
                $("#currencyWrapper").show();
                $("#DiscountAmountContainer").show();

            }
            else if (type == "<%:(byte)ProductPriceType.PriceRange%>") {
                $("#price-wrapper").hide();
                $("#price-range").show();
                $("#currencyWrapper").show();
                $("#DiscountAmountContainer").hide();
            }
            else {
                $("#price-wrapper").hide();
                $("#price-range").hide();
                $("#currencyWrapper").hide();
                $("#DiscountAmountContainer").hide();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <% var constantItems = Model.ConstantItems;%>
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>İlan Düzenleme
            </h4>
        </div>
    </div>
    <div class="row">

        <div class="col-sm-12 col-md-12  store-panel-container">
            <div>
                <div class="loading">Loading&#8230;</div>

                <%if (ViewData["success"] != null && ViewData["success"].ToString() == "success")
                    { %>
                <%string url = MakinaTurkiye.Utilities.HttpHelpers.UrlBuilder.GetProductUrl(Model.ProductId, Model.ProductName); %>
                <div class="alert alert-success" style="border-radius: 0px!important;">
                    <strong>İlanınız güncelenmiştir görmek için <a href="<%:url %>">tıklayınız</a></strong>
                </div>
                <%} %>
                <div>
                    <%using (Html.BeginForm("Edit", "Advert", FormMethod.Post, new { id = "formContent", enctype = "multipart/form-data", @class = "form-horizontal" }))
                        {%>

                    <div style="position: absolute; right: 35px; top: 5px;" class="small row">
                        <%if (Model.ProductRecordDate != Model.ProductLastUpdate)
                            {%>
                        <b>Son Güncellenme Tarihi:</b>  <%:Model.ProductLastUpdate.ToString("dd.MM.yyyy HH:mm:ss") %>
                        <% } %>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Kategori Adı
                        </label>
                        <div class="col-sm-9">
                            <p class="form-control-static">
                                <%= Model.CategoryName%>
                            </p>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Marka Adı
                        </label>
                        <div class="col-sm-9">
                            <div class="form-control-static">
                                <span id="BrandNameDisplay" style="border: 1px solid #e1e1e1; background-color: #fff; font-size: 12px; padding: 5px; width: auto;"><span id="BrandNameDisplayName"><%= Model.BrandName%></span> <i style="margin-left: 10px; padding-left: 40px; color: #333; cursor: pointer;" class="glyphicon glyphicon-pencil" id="OtherBrandClick"></i></span>

                                <div id="OtherBrandWrapper" style="display: none;" class="col-md-6">
                                    <%:Html.TextBoxFor(x => x.OtherBrand
, new {@class="form-control col-md-12",@id="OtherBrandText" })%><br />
                                    <button style="margin-top: 5px;" type="button" class="btn btn-info" id="OtherBrandSave">Kaydet</button>
                                    <button style="margin-top: 5px;" type="button" class="btn btn-info" id="OtherBrandCancel">İptal</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Seri
                        </label>
                        <div class="col-sm-9">
                            <p class="form-control-static">
                                <%= Model.SeriesName%>
                            </p>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Model
                        </label>
                        <div class="col-sm-9">
                            <div class="form-control-static">
                                <span id="ModelNameDisplay" style="border: 1px solid #e1e1e1; background-color: #fff; font-size: 12px; padding: 5px; width: auto;"><span id="ModelNameDisplayName"><%= Model.ModelName%> </span><i style="margin-left: 10px; color: #333; padding-left: 40px; cursor: pointer;" class="glyphicon glyphicon-pencil" id="OtherModelClick"></i></span>

                                <div id="OtherModelWrapper" style="display: none;" class="col-md-6">
                                    <%:Html.TextBoxFor(x => x.OtherModel
, new {@class="form-control col-md-12",@id="OtherModelText" })%><br />
                                    <button style="margin-top: 5px;" type="button" class="btn btn-info" id="OtherModelSave">Kaydet</button>
                                    <button style="margin-top: 5px;" type="button" class="btn btn-info" id="OtherModelCancel">İptal</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--      <div class="form-group">
                            <label class="col-sm-3 control-label">
                                <%: Html.LabelFor(m => m.OtherBrand)%>
                            </label>
                            <div class="col-sm-3">
                                <%: Html.TextBoxFor(m => m.OtherBrand, new { @class = "form-control" })%>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">
                                <%: Html.LabelFor(m => m.OtherModel)%>
                            </label>
                            <div class="col-sm-3">
                                <%: Html.TextBoxFor(m => m.OtherModel, new { @class = "form-control" })%>
                            </div>
                        </div>--%>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <%: Html.LabelFor(m => m.ProductNo)%>
                        </label>
                        <div class="col-sm-9">
                            <p class="form-control-static">
                                <%= Model.ProductNo%>
                            </p>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <%: Html.LabelFor(m => m.ProductName)%>
                        </label>
                        <div class="col-sm-9">
                            <%: Html.TextBoxFor(m => m.ProductName, new { @class = "form-control",@rel="ProductName",@size="97",@maxlength="97" })%>
                            <%--<%: Html.ValidationMessageFor(m => m.ProductName)%>--%>
                            <label id="pNameValMes" class="label label-danger" style="display: none;">
                                Lütfen Ürün Adı alanını giriniz.
                            </label>
                            <label id="checkProduct" class="label label-warning" style="display: none;">
                                Bu şekilde tanımlanan ürün adı mevcuttur.Yinede devam edebilirsiniz.
                            </label>
                        </div>

                        <!--Test!-->

                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <%: Html.LabelFor(m => m.ProductDescription)%>
                        </label>
                        <div class="col-sm-9">
                            <%-- <div class="summernote">
                                </div>--%>
                            <%: Html.TextAreaFor(m => m.ProductDescription)%>
                            <%: Html.ValidationMessageFor(m => m.ProductDescription)%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Anahtar Kelimeler:</label>
                        <div class="col-sm-9">

                            <input type="text" name="Keywords" value="<%:Model.Keywords %>"  data-role="tagsinput" class="form-control" /><br />
                            <small>Virgül ile ayırınız</small>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-3 control-label">Sertifikalar</label>
                        <div class="col-sm-9">
                            <%foreach (var item in Model.CertificateTypes)
                                {%>
                            <label class="checkbox-inline">
                                <input type="checkbox" name="certificateTypes" <%:item.Selected==true  ? "checked":"" %> value="<%:item.Value %>" />
                                <%:item.Text %>
                            </label>
                            <% } %>
                        </div>
                    </div>
                    <%foreach (var item in Model.MTProductPropertieModel.MTProductProperties)
                        {%>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <%:item.DisplayName %>:
                        </label>
                        <div class="col-sm-5 col-md-3 ">
                            <%if (item.Type == (byte)PropertieType.Text)
                                {%>
                            <input type="text" name="<%:item.InputName %>" value="<%:item.Value %>" class="form-control" />
                            <% }
                                else if (item.Type == (byte)PropertieType.Editor)
                                {%>
                            <textarea id="<%:item.InputName %>" name="<%:item.InputName %>"><%:item.Value %></textarea>
                            <script type="text/javascript" defer="defer">
                                var editor = CKEDITOR.replace('<%:item.InputName%>', { toolbar: 'webtool' });
                                CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
                            </script>
                            <% }
                                else
                                {%>
                            <%:Html.DropDownList(item.InputName,item.Attrs,new {@class="form-control" }) %>
                            <% } %>
                        </div>
                    </div>

                    <%} %>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <%: Html.LabelFor(m => m.ProductType)%>
                        </label>
                        <div class="col-sm-9">
                            <% foreach (var item in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductType))
                                { %>
                            <label class="radio-inline">
                                <%bool hasItem = false; %>
                                <% if (!string.IsNullOrEmpty(Model.ProductType))
                                    { %>
                                <% if (Model.ProductType.ToInt16() == item.ConstantId)
                                    { %>
                                <%hasItem = true; %>
                                <% } %>
                                <% } %>
                                <%:Html.RadioButton("fakeProductType", item.ConstantId, hasItem, new { @class = "ActiveName" })%>
                                <%=item.ConstantName%>
                            </label>
                            <%} %>
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
                            <input id="ProductType" name="ProductType" type="hidden" value="<%= Model.ProductType %>" />
                            <div class="bs-example bs-example-bg-classes">
                                <label id="divProductType" class="label label-danger" style="display: none;">
                                    Lütfen ürün tipini seçiniz.
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <%: Html.LabelFor(m => m.ProductStatu)%>
                        </label>
                        <div class="col-sm-9">
                            <% foreach (var item in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductStatu))
                                { %>
                            <label class="radio-inline">
                                <%bool hasItem = false; %>
                                <% if (!string.IsNullOrEmpty(Model.ProductStatu))
                                    { %>
                                <% if (Model.ProductStatu.ToInt16() == item.ConstantId)
                                    { %>
                                <%hasItem = true; %>
                                <% } %>
                                <% } %>
                                <%:Html.RadioButton("fakeProductStatu", item.ConstantId, hasItem, new { @class = "ActiveName2" })%><%=item.ConstantName%></label>
                            <%} %>
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
                            <input id="ProductStatu" name="ProductStatu" type="hidden" value="<%= Model.ProductStatu %>" />
                            <input id="CurrentListedPage" name="CurrentListedPage" type="hidden" value="<%= Model.CurrentListedPage %>" />
                            <div class="bs-example bs-example-bg-classes">
                                <label id="divProductStatu" class="label label-danger" style="display: none;">
                                    Lütfen ürün durumunu seçiniz.
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <%: Html.LabelFor(m => m.ProductSalesType)%>
                        </label>
                        <div class="col-sm-9">
                            <% foreach (var item in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductSalesType))
                                { %>
                            <label class="checkbox-inline">
                                <%bool hasItem = false; %>
                                <% if (!string.IsNullOrEmpty(Model.ProductSalesType))
                                    { %>
                                <%for (int i = 0; i < Model.ProductSalesType.Split(',').Length; i++)
                                    {%>
                                <% if (item.ConstantId == Model.ProductSalesType.Split(',').GetValue(i).ToInt16())
                                    { %>
                                <%hasItem = true; %>
                                <% } %>
                                <% } %>
                                <% } %>
                                <%: Html.CheckBox("ProductSalesType", hasItem, new { value = item.ConstantId.ToString() })%><%=item.ConstantName%>
                            </label>
                            <%} %>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Kısa Detay:
                        </label>
                        <div class="col-sm-9">
                            <% foreach (var item in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductBriefDetail))
                                { %>
                            <%bool hasItem = false; %>
                            <% if (!string.IsNullOrEmpty(Model.BriefDetail))
                                { %>
                            <% if (Model.BriefDetail.ToInt16() == item.ConstantId)
                                { %>
                            <%hasItem = true; %>
                            <% } %>
                            <% } %>
                            <label class="radio-inline">
                                <%:Html.RadioButton("BriefDetail", item.ConstantId, hasItem, new { @class = "pdetail" })%>
                                <%: item.ConstantName%>
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
                            <label class="col-sm-3 control-label">
                                Model Yılı
                            </label>
                            <div class="col-sm-2">
                                <%:Html.TextBox("ModelYear", Model.ModelYear, new { @class = "form-control", size = "10" })%>
                            </div>
                        </div>
                    </div>
                    <div id="pdetail" style="display: none;">
                        <div class="form-group">
                            <label class="col-sm-3 control-label">
                                Garanti Süresi
                            </label>
                            <div class="col-sm-3">
                                <select class="form-control" name="WarrantyPeriod" id="WarrantyPeriod">
                                    <option value="0">< Seçiniz ></option>
                                    <%for (int i = 1; i < 11; i++)
                                        { %>
                                    <%if (Model.WarrantyPeriod == i.ToString())
                                        { %>
                                    <option selected="selected" value="<%: i%>">
                                        <%: i%>
                                            Yıl</option>
                                    <% }
                                        else
                                        { %>
                                    <option value="<%: i%>">
                                        <%: i%>
                                            Yıl</option>
                                    <% }  %>
                                    <% } %>
                                </select>
                            </div>
                            <div class="bs-example bs-example-bg-classes">
                                <label id="warrantyValMes" class="label label-danger" style="display: none;">
                                    Lütfen garanti süresi seçiniz.
                                </label>
                            </div>
                        </div>
                    </div>
                    <div id="pselect">
                        <div class="form-group">
                            <label class="col-sm-3 control-label">
                                Mensei
                            </label>
                            <div class="col-sm-5 col-md-3">
                                <%:Html.DropDownListFor(model => model.MenseiId, new SelectList(Model.TheOriginItems, "ConstantId", "ConstantName"), new { @class = "form-control" })%>
                            </div>
                            <div class="bs-example bs-example-bg-classes">
                                <label id="divMenseiIdValMes" class="label label-danger" style="display: none;">
                                    Lütfen menşei seçiniz.
                                </label>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">
                                Teslim Durumu
                            </label>
                            <div class="col-sm-5 col-md-3">
                                <%:Html.DropDownListFor(model => model.OrderStatus, new SelectList(Model.SiparisList, "ConstantId", "ConstantName"), new { @class = "form-control" })%>
                            </div>
                            <div class="bs-example bs-example-bg-classes">
                                <label id="ordStatValMes" class="label label-danger" style="display: none;">
                                    Lütfen teslim durumunu seçiniz.
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Konum
                        </label>
                        <div class="col-sm-5 col-md-3">
                            <% string townText = "", localityText = "", cityText = "", countryText = "";
                                var town = Model.TownItems.FirstOrDefault(p => p.Value == Model.TownId.ToString());
                                var locality = Model.LocalityItems.FirstOrDefault(p => p.Value == Model.LocalityId.ToString());
                                var city = Model.CityItems.FirstOrDefault(p => p.Value == Model.CityId.ToString());
                                var country = Model.CountryItems.FirstOrDefault(p => p.Value == Model.CountryId.ToString());
                                if (town != null) townText = town.Text;
                                localityText = locality != null ? locality.Text : "";
                                cityText = city != null ? city.Text : "";
                                countryText = country != null ? country.Text : "";
                                var adress = string.Format("{0} {1} / {2} {3}", townText, localityText, cityText, countryText);
                            %>
                            <textarea class="form-control" disabled="disabled"><%: adress %></textarea>
                            <label class="checkbox-inline">
                                <%:Html.CheckBox("chkAdress", false, new { @id = "chkAdress" })%>
                                    İlan adresiniz değilse değiştirebilirsiniz..
                            </label>
                            <script type="text/javascript">
                                $("#chkAdress").change(function () {
                                    if (this.checked) {
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
                            <label class="col-sm-3 control-label">
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
                            <label class="col-sm-3 control-label">
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
                            <label class="col-sm-3 control-label">
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
                            <label class="col-sm-3 control-label">
                                Mahalle / Köy
                            </label>
                            <div id="divTownId" class="col-sm-5 col-md-3 dropdownAddress">
                                <%:Html.DropDownListFor(model => model.TownId, Model.TownItems, new { @class="form-control"})%>
                            </div>
                            <div class="bs-example bs-example-bg-classes">
                                <label id="townValMes" class="label label-danger" style="display: none;">
                                    Lütfen mahalle/köy seçiniz.
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group" <%:Model.AllowSellUrl==false ? "style=display:none":"" %>>
                        <label class="col-sm-3  control-label">
                            Harici Ürün Satış Linkiniz
                        </label>
                        <div class="col-md-5">
                            <%:Html.TextBoxFor(x=>x.ProductSellUrl, new {@class="form-control" }) %>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Fiyat Tipi</label>
                        <div class="col-md-8">
                            <%foreach (var productPriceTypeItem in Model.ProductPriceTypes)
                                {
                                    bool productChoose = false;
                            %>
                            <%if (productPriceTypeItem.ContstantPropertie == Model.ProductPriceType.ToString() || Model.ProductPriceType.ToString() == "") { productChoose = true; } %>
                            <div class="radio-inlin=e"><%:Html.RadioButton("productPriceType", productPriceTypeItem.ContstantPropertie, productChoose, new { @onclick="ProductPriceType("+productPriceTypeItem.ContstantPropertie+")"})%><%:productPriceTypeItem.ConstantName %></div>
                            <%} %>
                        </div>
                    </div>
                    <div id="price-range">
                        <div class="form-group" style="display: none;">
                            <label class="col-sm-3 control-label">
                                Fiyat Aralığı
                            </label>
                            <div class="col-xs-2 col-lg-2 pr0">
                                <%string priceBegin = (Model.ProductPriceBegin == 0) ? "" : Model.ProductPriceBegin.ToDecimal().ToString("0#.00");
                                    string priceLast = (Model.ProductPriceLast == 0) ? "" : Model.ProductPriceLast.ToDecimal().ToString("0#.00"); %>
                                <input type="text" name="ProductPriceBegin" id="ProductPriceBegin" value="<%:priceBegin %>" size="3" class="form-control" data-thousands="." data-decimal="," />


                            </div>

                            <div class="col-xs-2 col-lg-2  pl2">
                                <input type="text" name="ProductPriceLast" value="<%:priceLast %>" id="ProductPriceLast" size="3" class="form-control" data-thousands="." data-decimal="," />

                            </div>
                        </div>

                    </div>
                    <div id="price-wrapper">
                        <div class="form-group">
                            <label class="col-sm-3 control-label">
                                Fiyat
                            </label>
                            <div class="col-xs-3 col-lg-3">

                                <%string price = (Model.ProductPrice == 0) ? "" : Model.ProductPrice.ToDecimal().ToString("0#.00"); %>
                                <input type="text" class="form-control" value="<%:price %>" name="ProductPrice1" id="ProductPrice1" data-thousands="." data-decimal="," />
                                <div class="bs-example bs-example-bg-classes">
                                    <label id="ProductPriceError" class="label label-danger" style="display: none;">
                                        Lütfen Fiyat Giriniz.
                                    </label>
                                </div>
                            </div>

                            <%--                <%string priceOdd = (Model.ProductOdd == 0) ? "" : String.Format("{0:0,0}", Model.ProductOdd); %>
                            <div class="col-xs-2 col-lg-1  pl2">
                                <%:Html.TextBox("ProductOdd",priceOdd, new { size = "3", @class = "form-control" })%>
                            </div>--%>
                        </div>


                        <div class="form-group">
                            <label class="col-sm-3 control-label">
                                İndirim Tipi
                            </label>
                            <div class="col-xs-2 col-lg-2">
                                <select id="DiscountType" class="form-control" name="DiscountType">
                                    <option value="0" <%:Model.DiscountType==0 ? "selected":"" %>>İndirim Yok</option>
                                    <option value="1" <%:Model.DiscountType == (byte)ProductDiscountType.Percentage? "selected":"" %>>Yüzdelik İndirim </option>
                                    <option value="2" <%:Model.DiscountType == (byte)ProductDiscountType.Amount? "selected":"" %>>Miktar İndirimi </option>

                                </select>
                            </div>
                        </div>
                    </div>
                    <% string style = Model.DiscountType == 0 ? "display:none;" : ""; %>
                    <div id="DiscountAmountContainer" style="<%: style%>">
                        <div class="form-group">
                            <label class="col-sm-3 control-label" id="DiscountTypeLabel">
                                <% string text = Model.DiscountType == 1 ? "Yüzdesi" : "Miktarı"; %>
                                    İndirim <%:text %>
                            </label>
                            <div class="col-sm-3 col-lg-3">
                                <%:Html.TextBoxFor(x => x.DiscountAmount, new {@class="form-control" }) %>
                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">
                                Yeni Fiyat
                            </label>
                            <div class="col-sm-3">
                                <%:Html.TextBoxFor(x=>x.TotalPrice,new {@class="form-control" }) %>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3">
                        </label>

                        <div class="col-xs-4 col-md-4">
                            <% bool kdv = true, kdvh = false, fob = false; if (Model.Kdv == true)
                                {
                                    kdv = true;
                                    kdvh = false;
                                }
                                else if (Model.Fob == true)
                                {
                                    fob = true;
                                    kdv = false;
                                    kdvh = false;
                                }
                                else if (Model.Kdv == false)
                                {
                                    kdvh = true;
                                    kdv = false;
                                } %>
                            <label><%:Html.RadioButton("pricePropertie","kdvdahil", kdv)%> Kdv Dahil</label>
                            <label><%:Html.RadioButton("pricePropertie","kdvharic", kdvh)%> Kdv Hariç</label>
                            <label><%:Html.RadioButton("pricePropertie", "fob",fob)%> Fob</label>
                        </div>
                    </div>
                    <div class="form-group" id="currencyWrapper">
                        <label class="col-sm-3 control-label">
                            Birim
                        </label>
                        <div class="col-xs-2 col-lg-2 pr0">
                            <select class="form-control" name="UnitType" id="UnitType">
                                <option value="0">< Seçiniz ></option>
                                <% foreach (var itemProductBriefDetail in constantItems.Where(c => c.ConstantType == (byte)ConstantType.Birim))
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

                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <%: Html.LabelFor(m => m.ProductAdvertBeginDate)%>
                        </label>
                        <div class="col-xs-3 col-lg-2">
                            <%: Html.TextBox("ProductAdvertBeginDate", Model.ProductAdvertBeginDate != null ? Model.ProductAdvertBeginDate.ToString("dd.MM.yyyy") : "", new { @class = "form-control", disabled = "disabled" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <%: Html.LabelFor(m => m.ProductAdvertEndDate)%>
                        </label>
                        <div class="col-xs-3 col-lg-2">
                            <%: Html.TextBox("ProductAdvertBeginDate", Model.ProductAdvertEndDate != null ? Model.ProductAdvertEndDate.ToString("dd.MM.yyyy") : "", new { @class = "form-control", disabled = "disabled" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Paket Bitiş Tarihi
                        </label>
                        <div class="col-xs-2 col-lg-1">
                            <%:Html.TextBox("ProductPublicationDate", "", new { @class = "form-control", validate = "required:true" })%>
                        </div>
                        <div class="col-sm-5">
                            <div class="radio-inline">
                                <%:Html.RadioButton("ProductPublicationDateType", "1", false)%>
                                    Gün
                            </div>
                            <div class="radio-inline">
                                <%:Html.RadioButton("ProductPublicationDateType", "2", false)%>
                                    Ay
                            </div>
                            <div class="radio-inline">
                                <%:Html.RadioButton("ProductPublicationDateType", "3", false)%>
                                    Yıl
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            <%: Html.LabelFor(m => m.ProductActive)%>
                        </label>
                        <div class="col-sm-9">
                            <label class="radio-inline">
                                <%: Html.RadioButton("ProductActive", true)%>
                                    Aktif
                            </label>
                            <label class="radio-inline">
                                <%: Html.RadioButton("ProductActive", false)%>
                                    Pasif
                            </label>
                        </div>
                    </div>



                    <div class="form-group">
                        <%--                              <%using (Ajax.BeginForm("ImageUpdate", "Advert", new AjaxOptions { HttpMethod = "POST", UpdateTargetId = "divSonuc" })) {%>--%>
                        <label class="col-xs-12 col-sm-3 control-label">
                            <%: Html.LabelFor(m => m.NewProductPicture)%>
                        </label>
                        <div class="col-xs-3 col-md-2 col-lg-1">
                            <a id="btnUploadImages" class="btn-block btn btn-default btn-xs">Yükle
                            </a>
                        </div>
                        <div class="col-xs-5">
                            <%: Html.FileUploadFor(m => m.NewProductPicture, new {  multiple="multiple" })%>
                        </div>
                        <%--    <% } %>--%>
                    </div>

                    <div class="form-group">
                        <label class="col-xs-12 col-sm-3 control-label">
                            Ürün Resimleri:
                        </label>
                        <div id="divPictureList" class="col-xs-12  col-sm-9">
                            <%=Html.RenderHtmlPartial("EditProductPicture", Model.ProductPictureItems)%>
                            <span class="clearfix"></span>
                        </div>
                    </div>
                    <%--                 <div class="form-group">
                            <label class="col-sm-3 control-label">
                                <%: Html.LabelFor(m => m.VideoTitle)%>
                            </label>
                            <div class="col-sm-5">
                                <%: Html.TextBoxFor(m => m.VideoTitle, new { @class = "form-control" })%>
                            </div>
                        </div>--%>
                    <%--               <div class="form-group">
                            <label class="col-xs-12 col-sm-3 control-label">
                                <%: Html.LabelFor(m => m.NewProductVideo)%>
                            </label>
      
                            <div class="col-xs-5">
                                <%: Html.FileUploadFor(m => m.NewProductVideo)%>
                            </div>
                        </div>--%>
                    <%--                        <div class="form-group">
                            <label class="col-xs-12 col-sm-3 control-label">
                                Videolar:
                            </label>
                            <div class="col-xs-12  col-sm-9">
                                <p class="form-control-static">
                           
                                    <%=Html.RenderHtmlPartial("EditProductVideo", Model.VideoItems)%>
                                </p>
                            </div>
                        </div>--%>
                    <div class="form-group">
                        <label class="col-xs-12 col-sm-3 control-label">
                            Ürün Katoloğu
                        </label>

                        <div class="col-xs-5">
                            <input type="file" id="NewProductCatolog" name="NewProductCatolog" multiple />

                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-xs-12 col-sm-3 control-label">
                            Ürün Katologları
                        </label>
                        <div class="col-xs-12  col-sm-9">
                            <%=Html.RenderHtmlPartial("_ProductCatologList",Model.MTProductCatologItems) %>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-9 btn-group">
                            <a href="/Account/Advert/Index?ProductActive=1&DisplayType=2&currentPage=<%:Model.CurrentListedPage %>" type="submit" class="btn btn-default">İptal </a>
                            <button type="submit" id="saveProductButton" onclick="return advertCheck();" class="btn btn-primary">
                                Kaydet</button>
                        </div>
                    </div>
                    <%} %>
                </div>
            </div>

        </div>
    </div>
    <script type="text/javascript" defer="defer">
        var editor = CKEDITOR.replace('ProductDescription', { toolbar: 'webtool', format_tags: 'p;h1;h2;h3;h4;h5;h6;pre;address;div', enterMode: CKEDITOR.ENTER_DIV });
        CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');

        function DeleteCatolog(cid) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Account/ilan/DeleteCatolog',
                    type: "POST",
                    data: { id: cid },
                    success: function (result) {
                        if (result) {
                            $("#catolog" + cid).hide();
                        }

                    },
                    error: function (err) {
                        $(".loading").hide();
                        alert(err.statusText);
                    }
                });
            }
        }

        $(document).ready(function () {
            $('#btnUploadImages').click(function () {


                // Checking whether FormData is available in browser  
                if (window.FormData !== undefined) {

                    var fileUpload = $("#NewProductPicture").get(0);
                    var files = fileUpload.files;
                    if (files.length > 0) {
                        $(".loading").show();
                        // Create FormData object  
                        var fileData = new FormData();

                        // Looping over all files and add it to FormData object  
                        for (var i = 0; i < files.length; i++) {
                            fileData.append(files[i].name, files[i]);
                        }

                        // Adding one more key to FormData object  
                        fileData.append('ProductId', "<%=this.RouteData.Values["id"] %>");
                        fileData.append('ProductName', $("#ProductName").val());

                        $.ajax({
                            url: '/Account/ilan/EditImagesNewAjax',
                            type: "POST",
                            contentType: false, // Not to set any content header  
                            processData: false, // Not to process data  
                            data: fileData,
                            success: function (result) {
                                $(".loading").hide();
                                $('#divPictureList').html(result);
                                $("#NewProductPicture").replaceWith($("#NewProductPicture").val('').clone(true));
                            },
                            error: function (err) {
                                $(".loading").hide();
                                alert(err.statusText);
                            }
                        });
                    }
                    else {
                        alert("Lütfen eklemek istediğinizi resimleri seçiniz.");
                    }
                } else {
                    alert("FormData is not supported.");
                }
            });
        });
    </script>
</asp:Content>
