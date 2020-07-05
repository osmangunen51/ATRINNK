<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<ProductModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
    <script src="/Scripts/JQuery-dropdowncascading.js" type="text/javascript"></script>
    <link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css"
        media="all" />
    <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
</asp:Content>
<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">
    <style type="text/css">
        .adverAdress {
            display: none;
        }
        /* Custom Theme */ #superbox-container .loading {
            width: 32px;
            height: 32px;
            margin: 0 auto;
            text-indent: -9999px;
            background: url(styles/loader.gif) no-repeat 0 0;
        }

        #superbox .close a {
            float: right;
            padding: 0 5px;
            line-height: 20px;
            background: #333;
            cursor: pointer;
        }

            #superbox .close a span {
                color: #fff;
            }

        #superbox .nextprev a {
            float: left;
            margin-right: 5px;
            padding: 0 5px;
            line-height: 20px;
            background: #333;
            cursor: pointer;
            color: #fff;
        }

        #superbox .nextprev .disabled {
            background: #ccc;
            cursor: default;
        }
    </style>
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
      

                var val = $("#ProductPrice").val();
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

        $(function () {
            
            $("#ProductPrice").change(function () {
                     WriteTotalRecord();
            });
            $("#DiscountAmount").change(function () {
                     WriteTotalRecord();
            });
            
            $('#DiscountType').change(function () {
            
                if (this.value == '<%:(byte)ProductDiscountType.Amount%>') {
                    $("#DiscountTypeLabel").html("İndirim Miktarı");
                    $(".DiscountAmountContainer").show();
                    WriteTotalRecord();


                }
                else if (this.value == '<%:(byte)ProductDiscountType.Percentage%>') {
                    $("#DiscountTypeLabel").html("İndirim Yüzdesi");
                    $(".DiscountAmountContainer").show();
                     WriteTotalRecord();
                }
                else {
                    $(".DiscountAmountContainer").hide();

                }

            });

            $('#CategorySector').change(function () {
                $("#ProductGroup").show();
                $("#CategoryParent").hide('');
                $("#CategoryAlt").hide('');
                $("#CategoryAlt1").hide('');

                var sectorId = $('#CategorySector').val();
                $("#hdnCategoryId").val(sectorId);
                $.ajax({
                    url: '/Product/GetCategoryByParentId',
                    data: { id: sectorId, type: 6 },
                    type: "post",
                    success: function (msg) {
                        $('#ProductGroup' + " > option").remove();
                        $.each(msg, function (i) {

                            $('#ProductGroup').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                        });
                    },
                    error: function (e) {
                        alert(e.responseText);
                    }
                });
            });
            $('#ProductGroup').change(function () {
                $("#CategoryParent").show();
                $("#CategoryAlt").hide('');
                $("#CategoryAlt1").hide('');
                var productGroupId = $('#ProductGroup').val();
                $("#hdnCategoryId").val(productGroupId);
                $.ajax({
                    url: '/Product/GetCategoryByParentId',
                    data: { id: productGroupId, type: 1 },
                    type: "post",
                    success: function (msg) {
                        $('#CategoryParent' + " > option").remove();
                        $.each(msg, function (i) {

                            $('#CategoryParent').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                        });
                    },
                    error: function (e) {
                        alert(e.responseText);
                    }
                });
            });
            $('#CategoryParent').change(function () {
                $("#CategoryAlt").show();
                $("#CategoryAlt1").hide('');
                var catParentId = $('#CategoryParent').val();
                $("#hdnCategoryId").val(catParentId);

                $.ajax({
                    url: '/Product/GetCategoryByParentId',
                    data: { id: catParentId, type: 1 },
                    type: "post",
                    success: function (msg) {
                        $('#CategoryAlt' + " > option").remove();
                        if (msg.length > 0) {
                            $.each(msg, function (i) {

                                $('#CategoryAlt').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                            });

                        } else {
                            $("#CategoryAlt").hide();
                        }

                    },
                    error: function (e) {
                        alert(e.responseText);
                    }
                });


                $.ajax({
                    url: '/Product/GetCategoryByParentId',
                    data: { id: catParentId, type: 3 },
                    type: "post",
                    success: function (msg) {
                        $('#CategoryBrand' + " > option").remove();
                        if (msg.length > 0) {
                            $.each(msg, function (i) {

                                $('#CategoryBrand').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                            });

                        } else {
                            $("#CategoryBrand").hide();
                        }

                    },
                    error: function (e) {
                        alert(e.responseText);
                    }
                });
            });

            $('#CategoryAlt').change(function () {
                $("#CategoryAlt1").show();
                var catalId = $('#CategoryAlt').val();
                $("#hdnCategoryId").val(catalId);
                $.ajax({
                    url: '/Product/GetCategoryByParentId',
                    data: { id: catalId, type: 1 },
                    type: "post",
                    success: function (msg) {
                        $('#CategoryAlt1' + " > option").remove();

                        $.each(msg, function (i) {

                            $('#CategoryAlt1').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                        });
                        if (msg.length <= 0) {
                            $("#CategoryAlt1").hide();
                        }
                    },
                    error: function (e) {
                        alert(e.responseText);
                    }
                });
                $.ajax({
                    url: '/Product/GetCategoryByParentId',
                    data: { id: catalId, type: 3 },
                    type: "post",
                    success: function (msg) {
                        $('#CategoryBrand' + " > option").remove();
                        $("#CategoryBrand").show();
                        if (msg.length > 0) {
                            $.each(msg, function (i) {

                                $('#CategoryBrand').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                            });

                        } else {
                            $("#CategoryBrand").hide();
                        }

                    },
                    error: function (e) {
                        alert(e.responseText);
                    }
                });
            });
            $('#CategoryAlt1').change(function () {
                var categoryAlt1 = $("#CategoryAlt1").val();
                $("#CategoryBrand").show();
                $("#hdnCategoryId").val(categoryAlt1);
                $.ajax({
                    url: '/Product/GetCategoryByParentId',
                    data: { id: categoryAlt1, type: 3 },
                    type: "post",
                    success: function (msg) {
                        $('#CategoryBrand' + " > option").remove();
                        if (msg.length > 0) {
                            $.each(msg, function (i) {

                                $('#CategoryBrand').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                            });

                        } else {
                            $("#CategoryBrand").hide();
                        }

                    },
                    error: function (e) {
                        alert(e.responseText);
                    }
                });
            });

            $('#CategoryBrand').change(function () {
                var categoryAlt1 = $("#CategoryBrand").val();
                $("#ModelCategory").show();
                $.ajax({
                    url: '/Product/GetCategoryByParentId',
                    data: { id: categoryAlt1, type: 5 },
                    type: "post",
                    success: function (msg) {
                        $('#ModelCategory' + " > option").remove();
                        if (msg.length > 0) {
                            $.each(msg, function (i) {

                                $('#ModelCategory').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                            });

                        } else {
                            $("#ModelCategory").hide();
                        }

                    },
                    error: function (e) {
                        alert(e.responseText);
                    }
                });
            });
            $('#ModelCategory').change(function () {
                var modelCategoryId = $("#ModelCategory").val();
                $("#ModelCategory").show();
                $.ajax({
                    url: '/Product/GetCategoryByParentId',
                    data: { id: modelCategoryId, type: 4 },
                    type: "post",
                    success: function (msg) {
                        $('#SeriesCategory' + " > option").remove();
                        if (msg.length > 0) {
                            $.each(msg, function (i) {

                                $('#SeriesCategory').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                            });

                        } else {
                            $("#SeriesCategory").hide();
                        }

                    },
                    error: function (e) {
                        alert(e.responseText);
                    }
                });
            });

            $("#IsAdvanceEdit").change(function () {
                if (this.checked) {
                    $(".advanceInfo").show();

                    //Do stuff
                }
                else {
                    $(".advanceInfo").hide();
                }
            });
            <%if (!string.IsNullOrEmpty(ViewBag.check))
        {
            %>

         <%}%>
            $.superbox.settings = {
                closeTxt: "Kapat",
                loadTxt: "Yükleniyor...",
                nextTxt: "Sonraki",
                prevTxt: "Önceki"
            };
            $.superbox();
            var type = $("input[name=productPriceType]:checked").val();
            if (type == "<%:(byte)ProductPriceType.Price%>") {
                $(".priceWrapper").show();
                $("#priceRange").hide();
                $("#currencyWrapper").show();
            }
            else if (type == "<%:(byte)ProductPriceType.PriceRange%>") {
                $(".priceWrapper").hide();
                $("#priceRange").show();
                $("#currencyWrapper").show();

            }
            else {
                $(".priceWrapper").hide();
                $("#priceRange").hide();
                $("#currencyWrapper").hide();
            }

        });
        function Validation() {

            var priceType = $('input[name=productPriceType]:checked').val();;
            var rt = true;
                 console.log($("#BrandId").val(), "brandId");
            if (priceType == "239") {
                var productPriceBegin = $("#ProductPriceBegin").val();
                var productPriceLast = $("#ProductPriceLast").val();

                if (productPriceBegin == "" || productPriceLast == "") {
                    alert("Fiyat aralığını giriniz");
                    rt = false;
                }
            }
            else if (priceType == "238") {
                var productPrice = $("#ProductPrice").val();
                if (productPrice == "" || productPrice == 0) {
                    alert("Lütfen fiyatı giriniz.");
                    rt = false;
                }
            }
            if ($('#IsAdvanceEdit').is(":checked")) {
           
                if ($("#OtherBrand").val() == "" && $("#CategoryBrand").val() == "0" && $("#OtherModel").val() != "") {
                    alert("Marka Seçiniz Veya Diğer Marka Giriniz");
                    rt = false;

                }
                
            if (($("#BrandId").val() == "0" || $("#BrandId").val()=="" || $("#BrandId").val()==null) && $("#OtherModel").val() != "") {
                alert("Lütfen diğer model girmek için marka seçiniz");
                rt = false;
            }


            }
            if ($("#Doping").is(":checked")) {
                var endDate = $("#ProductDopingEndDate").val();
                if (endDate == "") {
                    alert("Doping bitiş tarihini ekleyiniz");
                    rt = false;
                }
            }
            if ($("#IsProductHomePage").is(":checked")) {
                var endDate = $("#ProductHomeEndDate").val();
                if (endDate == "") {
                    alert("Anasayfa bitiş tarihini ekleyiniz");
                    rt = false;
                }
                var begindate = $("#ProductHomeBeginDate").val();
                if (begindate == "") {
                    alert("Anasayfa başlangıç tarihini ekleyiniz");
                    rt = false;
                }
            }


            return rt;
        }
        $(function () { $('.date-pick').datepicker(); });

        $(document).ready(function myfunction() {

            $("#Doping").change(function () {
                if (this.checked) {
                    $("#ProductDopingDateDisplay").show();
                    //Do stuff
                }
                else {
                    $(".dopingDate").val("");
                    $("#ProductDopingDateDisplay").hide();
                }
            });

            $("#IsProductHomePage").change(function () {
                if (this.checked) {
                    $("#ProductHomeDateDisplay").show();
                    //Do stuff
                }
                else {
                    $(".homeDate").val("");

                    $("#ProductHomeDateDisplay").hide();
                }
            });
            var utype = "<%= Model.UnitType%>";
            $('#UnitType').val(utype);
            var ptype = $('#ProductStatu').val();
            if (ptype == 73) {
                $('#pstatu').show();
            }
            else {
                $('#pstatu').hide();
                $('#ModelYear').val(0);
            }

            var ptype = "<%= Model.ProductType%>";
            if (ptype == 104) {
                $('.pselect').hide();
                $('#MenseiId').val(0);
                $('#OrderStatus').val(0);
            }
            else {
                $('.pselect').show();
            }

            var pdet = "<%= Model.BriefDetail%>";
            if (pdet == '86') {
                $('#pdetail').show();
            }
            else {
                $('#pdetail').hide();
            }

            $('#CountryId').DropDownCascading({
                method: "/Product/Cities", target: "#CityId", loader: "#imgLoader"
            });

            $('#CityId').DropDownCascading({
                method: "/Product/Localities", target: "#LocalityId", loader: "#imgLoader"
            });

            $('#LocalityId').DropDownCascading({
                method: "/Product/Towns", target: "#TownId", loader: "#imgLoader"
            });

        });

        function DeletePicture(pictureId, pictureName) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Product/DeletePicture',
                    type: 'Post',
                    data:
                        {
                            ProductId: <%=this.RouteData.Values["id"] %>,
                            PictureId: pictureId,
                            PictureName: pictureName
                        },
                    success: function (data) {
                        $('#divPictureList').html(data);
                    },
                    error: function (x, l, e) {
                        alert(e.responseText);
                    }
                });
            }
        }

        function mainImage(pictureId, pictureName) {
            var h = [];
            $("ul.reorder-photos-list li").each(function () { h.push($(this).attr('id').substr(9)); });
            console.log(h);
            $.ajax({
                url: '/Product/mainImage',
                type: 'Post',
                data:
                    {
                        idArray: " " + h + "",
                        ProductId: <%=this.RouteData.Values["id"] %>,
                        PictureId: pictureId,
                        PictureName: pictureName
                    },
                success: function (data) {
                    $('#divPictureList').html(data);
                },
                error: function (x, l, e) {
                    alert(e.responseText);
                }
            });
        }

        function DeleteVideo(videoId) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Product/DeleteVideo',
                    type: 'Post',
                    data:
                        {
                            ProductId: <%=this.RouteData.Values["id"] %>,
                            VideoId: videoId
                        },
                    success: function (data) {
                        $('#divVideoList').html(data);
                    },
                    error: function (x, l, e) {
                        alert(x.responseText);
                    }
                });
            }
        }

        function DoVideoShowcase(videoId) {
            if (confirm('Vitrinde göstermek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Product/DoVideoShowcase',
                    type: 'Post',
                    data:
                        {
                            ProductId: <%=this.RouteData.Values["id"] %>,
                            VideoId: videoId
                        },
                    success: function (data) {
                        $('#divVideoList').html(data);
                    },
                    error: function (x, l, e) {
                        alert(x.responseText);
                    }
                });
            }
        }
        function ProductPriceType(type) {
            if (type == "<%:(byte)ProductPriceType.Price%>") {
                $(".priceWrapper").show();
                $("#priceRange").hide();
                $("#currencyWrapper").show();
            }
            else if (type == "<%:(byte)ProductPriceType.PriceRange%>") {
                $(".priceWrapper").hide();
                $("#priceRange").show();
                $("#currencyWrapper").show();

            }
            else {
                $(".priceWrapper").hide();
                $("#priceRange").hide();
                $("#currencyWrapper").hide();
            }
        }
        onload = function () {
            $('#CountryId').val(246);
        }
        function tıkla() {
            $('#lightbox_click').trigger('click');
        }


    </script>
    <% using (Html.BeginForm("Edit", "Product", FormMethod.Post, new { enctype = "multipart/form-data" }))
        { %>
    <div style="float: left; width: auto; margin-top: 10px;">
        <% var constantItems = Model.ConstantItems;%>
        <%if (TempData["ProductHomePageMessage"] != null) {%>
        <p style="color:#870505; font-size:17px"><%:TempData["ProductHomePageMessage"] %></p>
        <% } %>
        <%using (Html.BeginPanel())
            { %>
        <table border="0" cellpadding="5" cellspacing="0" style="margin: 20px; padding-bottom: 20px;">
            <tr>
                <td align="left" colspan="2">
                    <%:Html.CheckBoxFor(m=>m.IsAdvanceEdit,new {@id="IsAdvanceEdit" }) %>Gelişmiş Düzenleme 
                    <br />
                    <%:Html.CheckBoxFor(m=>m.IsNewProduct,new {@id="IsProductNew" }) %>Yeni Ürün
                </td>
                <td colspan="3" align="right">
                    <button type="submit" onclick="return Validation();" style="height: 27px;">
                        Kaydet
                    </button>
                    <button type="button" style="height: 27px" onclick="window.location='/Property'">
                        İptal
                    </button>
                    <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 10px">
                    </div>
                </td>

            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.CategoryId)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Model.MainCategoryName%>
                    <%:Html.Hidden("CategoryId",Model.CategoryId,new {@id="hdnCategoryId" }) %>

                    <span class="advanceInfo" style="display: none;">
                        <%:Html.DropDownList("CategorySector",Model.CategorySectors,new {id="CategorySector"}) %>
                        <select id="ProductGroup" style="display: none;">
                        </select>
                        <select id="CategoryParent" style="display: none;">
                        </select>
                        <select id="CategoryAlt" style="display: none;">
                        </select>
                        <select id="CategoryAlt1" style="display: none;">
                        </select>
                    </span>

                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.BrandId)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Model.BrandCategoryName%>

                    <select class="advanceInfo" name="BrandId" id="CategoryBrand" style="display: none;">
                    </select>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.SeriesId)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Model.SerieCategoryName%>
                    <span class="advanceInfo">
                        <select name="SeriesId" id="SeriesCategory" style="display: none;">
                        </select>
                    </span>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.ModelId)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Model.ModelCategoryName%>
                    <select class="advanceInfo" name="ModelId" id="ModelCategory" style="display: none;">
                    </select>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.OtherBrand)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Html.TextBoxFor(m => m.OtherBrand, new { style = "width: 300px" })%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.OtherModel)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Html.TextBoxFor(m => m.OtherModel, new { style = "width: 300px" })%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.ProductNo)%>
                </td>
                <td>:
                </td>
                <td>
                    <span>
                        <%= Model.ProductNo%></span>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.ProductName)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Html.TextBoxFor(m => m.ProductName, new { style = "width: 300px" })%>
                    <%: Html.ValidationMessageFor(m => m.ProductName)%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td valign="top">
                    <%: Html.LabelFor(m => m.ProductDescription)%>
                </td>
                <td valign="top">:
                </td>
                <td>
                    <%: Html.TextAreaFor(m => m.ProductDescription)%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.ProductDescription)%>
                </td>
            </tr>
            <tr>
                <td>Anahtar Kelimeler</td>
                <td>:</td>
                <td><%:Html.TextAreaFor(x=>x.Keywords, new { @style="width:250px"}) %></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.ProductType)%>
                </td>
                <td>:
                </td>
                <td>
                    <% foreach (var item in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductType))
                        { %>
                    <%bool hasItem = false; %>
                    <% if (!string.IsNullOrEmpty(Model.ProductType))
                        { %>
                    <% if (Model.ProductType.ToInt16() == item.ConstantId)
                        { %>
                    <%hasItem = true; %>
                    <% } %>
                    <% } %>
                    <%=item.ConstantName%>&nbsp;
                    <%:Html.RadioButton("fakeProductType", item.ConstantId, hasItem, new { style = "width: 22px; height: 22px", @class = "ActiveName" })%>&nbsp;&nbsp;
                    <%} %>
                    <script type="text/javascript">
                        $('.ActiveName').click(function () {
                            $('#ProductType').val($(this).val());
                            var ptype = $('#ProductType').val();
                            if (ptype == 104) {
                                $('.pselect').hide();
                                $('#MenseiId').val(0);
                                $('#OrderStatus').val(0);
                            }
                            else {
                                $('.pselect').show();
                            }
                        });
                    </script>
                    <input id="ProductType" name="ProductType" type="hidden" value="<%= Model.ProductType %>" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.ProductStatu)%>
                </td>
                <td>:
                </td>
                <td>
                    <% foreach (var item in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductStatu))
                        { %>
                    <%bool hasItem = false; %>
                    <% if (!string.IsNullOrEmpty(Model.ProductStatu))
                        { %>
                    <% if (Model.ProductStatu.ToInt16() == item.ConstantId)
                        { %>
                    <%hasItem = true; %>
                    <% } %>
                    <% } %>
                    <%=item.ConstantName%>&nbsp;
                    <%:Html.RadioButton("fakeProductStatu", item.ConstantId, hasItem, new { style = "width: 22px; height: 22px", @class = "ActiveName2" })%>&nbsp;&nbsp;
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
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.ProductSalesType)%>
                </td>
                <td>:
                </td>
                <td>
                    <% foreach (var item in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductSalesType))
                        { %>
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
                    <%=item.ConstantName%>&nbsp;<%: Html.CheckBox("ProductSalesType", hasItem, new { value = item.ConstantId.ToString() })%>&nbsp;&nbsp;
                    <%} %>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.BriefDetail)%>
                </td>
                <td>:
                </td>
                <td>
                    <% foreach (var item in constantItems.Where(c => c.ConstantType == (byte)ConstantType.ProductBriefDetail))
                        { %>
                    <%bool hasItem = false; %>
                    <% if (!string.IsNullOrEmpty(Model.BriefDetail))
                        { %>
                    <%for (int i = 0; i < Model.BriefDetail.Split(',').Length; i++)
                        {%>
                    <% if (item.ConstantId == Model.BriefDetail.Split(',').GetValue(i).ToInt16())
                        { %>
                    <%hasItem = true; %>
                    <% } %>
                    <% } %>
                    <% } %>

                    <%--  <%: Html.CheckBox("BriefDetail", hasItem, new { value = item.ConstantId.ToString() })%>--%>
                    <%:Html.RadioButton("BriefDetail", item.ConstantId, hasItem, new { @class = "pdetail" })%>
                    <%: item.ConstantName%>
                    <input type="text" id="warrianty" style="display: none" />
                    &nbsp;&nbsp;
                    <%} %>
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
                </td>
                <td></td>
            </tr>
            <tr id="pdetail" style="display: none;">
                <td>Garanti Süresi
                </td>
                <td>:
                </td>
                <td>
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
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="3">
                    <div style="float: left; width: 100%; background-color: #bababa; height: 1px;">
                    </div>
                </td>
            </tr>
            <tr>
                <td>Konum
                </td>
                <td>:
                </td>
                <td>
                    <% var town = Model.TownItems.FirstOrDefault(p => p.Value == Model.TownId.ToString()) == null ? null : Model.TownItems.FirstOrDefault(p => p.Value == Model.TownId.ToString()).Text;
                        var locality = Model.LocalityItems.FirstOrDefault(p => p.Value == Model.LocalityId.ToString()) == null ? null : Model.LocalityItems.FirstOrDefault(p => p.Value == Model.LocalityId.ToString()).Text;
                        var city = Model.CityItems.FirstOrDefault(p => p.Value == Model.CityId.ToString()) == null ? null : Model.CityItems.FirstOrDefault(p => p.Value == Model.CityId.ToString()).Text;
                        var country = Model.CountryItems.FirstOrDefault(p => p.Value == Model.CountryId.ToString()) == null ? null : Model.CountryItems.FirstOrDefault(p => p.Value == Model.CountryId.ToString()).Text;
                        var adress = string.Format("{0} {1} / {2} {3}", town, locality, city, country);
                    %>
                    <textarea class="form-control" disabled="disabled"><%: adress %></textarea>
                    <label class="checkbox-inline">
                        <%:Html.CheckBox("chkAdress", false, new { @id = "chkAdress" })%>
                        İlan adresiniz değilse değiştirebilirsiniz..
                    </label>
                    <script type="text/javascript">
                        $('#chkAdress').click(function () {
                            var adress = $(this).val(); //attr('checked');
                            if (adress) {
                                $('tr.adverAdress').toggle();
                            }
                            else {
                                alert("hide");
                                $('tr.adverAdress').hide();
                            }
                        });

                    </script>
                </td>
            </tr>
            <tr class="adverAdress">
                <td valign="top">
                    <%= Html.LabelFor(model => model.CountryId)%>
                </td>
                <td valign="top">:
                </td>
                <td valign="top">
                    <%:Html.DropDownListFor(model => model.CountryId, Model.CountryItems, new { style = "width : 140px" })%>
                </td>
            </tr>
            <tr class="adverAdress">
                <td>
                    <%= Html.LabelFor(model => model.CityId)%>
                </td>
                <td>:
                </td>
                <td>
                    <%:Html.DropDownListFor(model => model.CityId, Model.CityItems, new { style = "width : 140px" })%>
                </td>
            </tr>
            <tr class="adverAdress">
                <td>
                    <%= Html.LabelFor(model => model.LocalityId)%>
                </td>
                <td>:
                </td>
                <td>
                    <%:Html.DropDownListFor(model => model.LocalityId, Model.LocalityItems, new { style = "width : 140px" })%>
                </td>
            </tr>
            <tr class="adverAdress">
                <td>
                    <%= Html.LabelFor(model => model.TownId)%>
                </td>
                <td>:
                </td>
                <td>
                    <%:Html.DropDownListFor(model => model.TownId, Model.TownItems, new { style = "width : 140px" })%>
                </td>
            </tr>
            <tr class="pselect">
                <td colspan="3">
                    <div style="float: left; width: 100%; background-color: #bababa; height: 1px;">
                    </div>
                </td>
            </tr>
            <tr class="pselect">
                <td>
                    <%= Html.LabelFor(model => model.MenseiId)%>
                </td>
                <td>:
                </td>
                <td>
                    <%:Html.DropDownListFor(model => model.MenseiId, new SelectList(Model.TheOriginItems, "ConstantId", "ConstantName"), new { style = "width : 140px" })%>
                </td>
            </tr>
            <tr class="pselect">
                <td>
                    <%= Html.LabelFor(model => model.OrderStatus)%>
                </td>
                <td>:
                </td>
                <td>
                    <%:Html.DropDownListFor(model => model.OrderStatus, new SelectList(Model.SiparisList, "ConstantId", "ConstantName"), new { style = "width : 140px" })%>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div style="float: left; width: 100%; background-color: #bababa; height: 1px;">
                    </div>
                </td>
            </tr>
            <tr id="pstatu">
                <td>
                    <%: Html.LabelFor(m => m.ModelYear)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Html.TextBox("ModelYear", Model.ModelYear != 0 ? Model.ModelYear.ToString() : "", new { style = "width: 60px;" })%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Fiyat Tipi
                </td>
                <td>:</td>
                <td>
                    <%foreach (var productPriceType in Model.ProductPriceTypes)
                        { %>
                    <% bool choose = false;

                        if (Model.ProductPriceType != 0)
                        {
                            if (productPriceType.ContstantPropertie == Model.ProductPriceType.ToString())
                            {
                                choose = true;
                            }
                        }
                        else
                        {
                            if (productPriceType.ContstantPropertie == Convert.ToString((byte)ProductPriceType.Price))
                            {
                                choose = true;
                            }
                        }

                    %>
                    <%:Html.RadioButton("productPriceType", productPriceType.ContstantPropertie, choose, new { @onclick="ProductPriceType("+productPriceType.ContstantPropertie+")"})%> <%:productPriceType.ConstantName %>  &nbsp;&nbsp;
                    <%}%>   
                </td>
            </tr>
            <tr id="priceRange" style="display: none;">
                <td>Fiyat Aralığı
                </td>
                <td>:</td>
                <td>
                    <%: Html.TextBoxFor(model => model.ProductPriceBegin, new { style = "width: 100px" })%>
                    <%: Html.TextBoxFor(m => m.ProductPriceLast, new {style = "width: 100px" })%>
                </td>
            </tr>
            <tr class="priceWrapper">
                <td>
                    <%: Html.LabelFor(m => m.ProductPrice)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Html.TextBoxFor(model => model.ProductPrice, new { style = "width: 100px" })%>
            
                </td>
                <td></td>
            </tr>
            <tr class="priceWrapper">
                <td>
                    İndirim Tipi
                </td>
                <td>:</td>
                <td>
                              <select id="DiscountType" class="form-control" name="DiscountType">
                                        <option value="0" <%:Model.DiscountType==0 ? "selected":"" %>>İndirim Yok</option>
                                        <option value="1" <%:Model.DiscountType == (byte)ProductDiscountType.Percentage? "selected":"" %>>Yüzdelik İndirim </option>
                                        <option value="2" <%:Model.DiscountType == (byte)ProductDiscountType.Amount? "selected":"" %>>Miktar İndirimi </option>

                                    </select>
                </td>

            </tr>
            <tr class="priceWrapper DiscountAmountContainer">
                <td>
                            <label id="DiscountTypeLabel">
                                    <% string text = Model.DiscountType == 1 ? "Yüzdesi" : "Miktarı"; %>
                                    İndirim <%:text %>
                                </label>
                </td>
                <td>:</td>
                <td>
                      <%:Html.TextBoxFor(x => x.DiscountAmount, new {@class="form-control" }) %>
                </td>

            </tr>
            <tr class="priceWrapper DiscountAmountContainer">
                <td>Yeni Fiyat</td>
                <td>:</td>
                <td>
                          <%:Html.TextBox("TotalPrice", Model.ProductPriceWithDiscount.HasValue ? Model.ProductPriceWithDiscount:0,new {@class="form-control" }) %>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <% bool kdv = true, kdvh = false, fob = false; if (Model.Kdv == true) { kdv = true; } else { kdv = false; kdvh = true; kdv = false; }
                    if (Model.Fob == true) { kdvh = false; fob = true; kdv = false; }
                %>
                <td><%:Html.RadioButton("pricePropertie","kdvdahil",kdv) %>Kdv Dahil
                    <%:Html.RadioButton("pricePropertie","kdvharic",kdvh) %>Kdv Haric
                    <%:Html.RadioButton("pricePropertie","fob",fob) %>Fob
                </td>
            </tr>
            <tr>
                <td>Fiyat Tipi</td>
                <td>:</td>
                <td><%: Html.DropDownListFor(m => m.CurrencyId, Model.CurrencyItems)%></td>
            </tr>
            <tr id="currencyWrapper">
                <td>Birim
                </td>
                <td>:
                </td>
                <td>
                    <select class="form-control" name="UnitType" id="UnitType">
                        <option>< Seçiniz ></option>
                        <% foreach (var itemProductBriefDetail in constantItems.Where(c => c.ConstantType == (byte)ConstantType.Birim))
                            { %>
                        <option value="<%: itemProductBriefDetail.ConstantId%>">
                            <%: itemProductBriefDetail.ConstantName%></option>
                        <% } %>
                    </select>
                </td>
            </tr>

            <tr>
                <td>
                    <%: Html.LabelFor(m => m.ProductRecordDate)%>
                </td>
                <td>:
                </td>
                <td>
                    <span>

                        <%=Model.ProductRecordDate.ToString("dd/MM/yyyy HH:mm:ss") %>
              
                    </span>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.ProductLastUpdate)%>
                </td>
                <td>:
                </td>
                <td>
                    <span>
                        <%= Model.ProductLastUpdate.ToString("dd.MM.yyyy HH:mm:ss")%></span>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.ProductAdvertBeginDate)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Html.TextBox("ProductAdvertBeginDate", Model.ProductAdvertBeginDate != null ? Model.ProductAdvertBeginDate.Value.ToString("dd.MM.yyyy") : "", new { style = "width: 200px;", @class = "date-pick" })%>
                </td>
                <td>
                    <%: Html.ValidationMessageFor(m => m.ProductAdvertBeginDate)%>
                </td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.ProductAdvertEndDate)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Html.TextBox("ProductAdvertEndDate", Model.ProductAdvertEndDate != null ? Model.ProductAdvertEndDate.Value.ToString("dd.MM.yyyy") : "", new { style = "width: 200px;", @class = "date-pick" })%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.ViewCount)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Model.ViewCount%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.SingularViewCount)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Model.SingularViewCount%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="4">
                    <span style="font-weight: bold;">Ürün Resimleri</span>
                    <div id="divPictureList" style="width: 100%; height: auto; float: left; border: solid 1px #bababa; padding-bottom: 20px; margin-top: 5px;">
                        <%=Html.RenderHtmlPartial("PictureList", Model.ProductPictureItems)%>
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <%: Html.LabelFor(m => m.NewProductPicture)%>
                </td>
                <td valign="top">:
                </td>
                <td>
                    <%: Html.FileUploadFor(m => m.NewProductPicture, new { style = "border:solid 1px #bababa", multiple="multiple" })%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td colspan="4">
                    <span style="font-weight: bold;">Videolar</span>
                    <div id="divVideoList" style="width: 100%; height: auto; float: left; border: solid 1px #bababa; padding-bottom: 20px; margin-top: 5px;">
                        <%=Html.RenderHtmlPartial("VideoList", Model.VideoItems)%>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.VideoTitle)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Html.TextBoxFor(m => m.VideoTitle, new { style = "width: 100px" })%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td valign="top">
                    <%: Html.LabelFor(m => m.NewProductVideo)%>
                </td>
                <td valign="top">:
                </td>
                <td>
                    <%: Html.FileUploadFor(m => m.NewProductVideo, new { style = "border:solid 1px #bababa; height: 20px;" })%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.ProductShowcase)%>
                </td>
                <td>:
                </td>
                <td>Aktif&nbsp;<%:Html.RadioButtonFor(m => m.ProductShowcase, true)%>&nbsp;&nbsp; Pasif&nbsp;<%:Html.RadioButtonFor(m => m.ProductShowcase, false)%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%=Html.LabelFor(m=> m.ProductActive ) %>
                </td>
                <td>:
                </td>
                <td>
                    <div style="width: auto; height: auto; float: left">
                        <div style="width: auto; height: auto; float: left; margin-top: 4px;">
                            Aktif
                        </div>
                        <div style="width: auto; height: auto; float: left; margin-left: 5px;">
                            <%: Html.RadioButton("ProductActive", true) %>
                        </div>
                    </div>
                    <div style="width: auto; height: auto; float: left; margin-left: 10px;">
                        <div style="width: auto; height: auto; float: left; margin-top: 4px;">
                            Pasif
                        </div>
                        <div style="width: auto; height: auto; float: left; margin-left: 5px;">
                            <%: Html.RadioButton("ProductActive", false)%>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.ProductActiveType)%>
                </td>
                <td>:
                </td>
                <td>
                    <% bool inceleniyor = false; %>
                    <% bool onaylanmadi = false; %>
                    <% bool onaylandi = false; %>
                    <% bool silindi = false; %>
                    <%bool cop = false; %>
                    <%  var activeType = (ProductActiveType)Model.ProductActiveType;  %>
                    <%switch (activeType)
                        {
                            case ProductActiveType.Inceleniyor:
                                inceleniyor = true;
                                break;
                            case ProductActiveType.Onaylandi:
                                onaylandi = true;
                                break;
                            case ProductActiveType.Onaylanmadi:
                                onaylanmadi = true;
                                break;
                            case ProductActiveType.Silindi:
                                silindi = true;
                                break;
                            case ProductActiveType.CopKutusuYeni:
                                cop = true;
                                break;
                            default:
                                break;
                        } %>
                    İnceleniyor&nbsp;<%: Html.RadioButton("ProductActiveType", "0",inceleniyor)%>&nbsp;&nbsp;
                    Onaylandı&nbsp;<%: Html.RadioButton("ProductActiveType", "1", onaylandi)%>&nbsp;&nbsp;
                    <span style="width: 30px;" onmouseup="tıkla()">Onaylanmadı&nbsp;<%: Html.RadioButton("ProductActiveType", "2", onaylanmadi)%>&nbsp;&nbsp;
                    </span>Silindi&nbsp;<%: Html.RadioButton("ProductActiveType", "3", silindi)%>
                    Çöp Kutusu&nbsp;<%:Html.RadioButton("ProductActiveType","8",cop) %>
                    <a href="/Member/advertmail/<%=Model.StoreMainPartyId %>?productid=<%:Model.ProductId%>"
                        id="lightbox_click" rel="superbox[iframe]"></a>
                </td>
                <td></td>
            </tr>

            <tr>
                <td>Popüler İlan
                </td>
                <td>:
                </td>
                <td>
                    <%:Html.CheckBox("MoneyCondition")%>
                </td>
                <td></td>
            </tr>
            <tr>
                <td>
                    <%: Html.LabelFor(m => m.Doping)%>
                </td>
                <td>:
                </td>
                <td>
                    <%: Html.CheckBoxFor(m => m.Doping, new {@style="float:left;" })%>
                    <% string cssDisplay = Model.Doping == true ? " " : "display:none;"; %>
                    <div id="ProductDopingDateDisplay" style="float: left; <%=cssDisplay%>">
                        <%:Html.TextBox("ProductDopingBeginDate",Model.ProductDopingBeginDate!=null?Model.ProductDopingBeginDate.ToDateTime().ToString("dd.MM.yyyy"):DateTime.Now.ToDateTime().ToString("dd.MM.yyyy"),new {@placeholder="Başlangıç",@class="date-pick homeDate" }) %>
                        <%:Html.TextBox("ProductDopingEndDate",Model.ProductDopingEndDate!=null?Model.ProductDopingEndDate.ToDateTime().ToString("dd.MM.yyyy"):"",new {@placeholder="Bitiş",@class="date-pick homeDate", }) %>
                    </div>
                </td>
                <td></td>
            </tr>
            <%if (Convert.ToBoolean(ViewData["categoryPlaced"]) == true)
                { %>
            <tr>
                <td>Seçilen Kategori</td>
                <td>:</td>
                <%var checkbox = (Model.ChoicedForCategoryIndex == null) ? false : Model.ChoicedForCategoryIndex; %>
                <td><%:Html.CheckBox("ChoicedForCategoryIndex",checkbox) %></td>
            </tr>
            <%} %>

            <tr>
                <td title="Anasayfa sektörlere bağlı ürünlerde gösterilsinmi ?">Anasayfa Sektör Seçilen</td>
                <td>:</td>
                <td><%:Html.CheckBoxFor(x=>x.IsProductHomePage, new {@style="float:left;" })%>
                    <% cssDisplay = Model.IsProductHomePage == true ? " " : "display:none;"; %>
                    <div id="ProductHomeDateDisplay" style="float: left; <%: cssDisplay%>">
                        <%:Html.TextBox("ProductHomeBeginDate",Model.ProductHomeBeginDate!=null?Model.ProductHomeBeginDate.ToDateTime().ToString("dd.MM.yyyy"):DateTime.Now.ToDateTime().ToString("dd.MM.yyyy"),new {@placeholder="Başlangıç",@class="date-pick dopingDate" }) %>
                        <%:Html.TextBox("ProductHomeEndDate",Model.ProductHomeEndDate!=null?Model.ProductHomeEndDate.ToDateTime().ToString("dd.MM.yyyy"):"",new {@placeholder="Bitiş",@class="date-pick dopingDate", }) %>
                    </div>
                </td>
            </tr>
            <%if (Model.AllowProductSellUrl)
                {%>
            <tr>
                <td title="Ödeme Sayfası Linki">Ödeme Sayfası Linki</td>
                <td>:</td>
                <td>

                    <%:Html.TextBoxFor(x => x.ProductSellUrl, new {@style="width:200px" }) %>
                    <% }
                        else
                        { %>
                    <%:Html.HiddenFor(x=>x.ProductSellUrl) %>
                
                </td>
            </tr>
            <%} %>
            <%--<%: Html.CheckBox("ProductSalesType", hasItem, new { value = item.ConstantId.ToString() })--%>
            <tr>
                <td colspan="3" align="right">
                    <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-bottom: 10px">
                    </div>
                    <button type="submit" onclick="return Validation();" style="height: 27px">
                        Kaydet
                    </button>
                    <button type="button" style="height: 27px" onclick="window.location='/Product'">
                        İptal
                    </button>
                </td>
                <td></td>
            </tr>

        </table>
        <%} %>
    </div>
    <% } %>
    <script type="text/javascript" defer="defer">
        var editor = CKEDITOR.replace('ProductDescription', { toolbar: 'webtool' });
        CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
    </script>
</asp:content>
