<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<MTAdvancedSearchModel>" %>


<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <meta name="description" content="Makinaturkiye.com detaylı arama aracı sayesinde, binlerce ilan arasından detaylandırarak aradığınız ilana daha kolay ulaşabilirsiniz." />
    <style type="text/css">
        .scrollCategory{
    overflow: auto;
    white-space: nowrap; 
        }
        select option{
            font-size:12px!important;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#sector').change(function () {
                var CategoryId = this.value;
                FillCategory(CategoryId, "productGroup", "productGorupLoading");
                FillCity(CategoryId,0,0,0,0);
                $("#category").html("");
                $("#category").hide();
                $("#brand").html("");
                $("#serie").html("");
                $("#model").html("");
                $("#brandsRow").hide();
                $("#locality").attr("disabled",true);
            })
            $("#productGroup").change(function () {
                var CategoryId = this.value;
                FillCategory(CategoryId, "category", "categoryLoading");
                FillCity(CategoryId, 0, 0, 0, 0);
                $("#brand").html("");
                $("#serie").html("");
                $("#model").html("");
                $("#brandsRow").hide();
                $("#locality").attr("disabled", true);
                
            })
            $('#category').change(function () {
                var CategoryId = this.value;
                FillBrand(CategoryId);
                FillCity(CategoryId, 0, 0, 0, 0);
                $("#brandsRow").show();
                $("#serie").html("");
                $("#model").html("");
                $("#model").hide();
                $("#serie").hide();
                $("#locality").attr("disabled", true);
                
             
            })
            $("#brand").change(function () {
                var categoryId = $("#category option:selected").val();
                var brandId = this.value;
         
                FillModel(categoryId, brandId);
                FillCity(categoryId, brandId, 0, 0, 0);
                $("#serie").html("");
                $("#serie").hide();
                $("#locality").attr("disabled", true);
                

            })
            $("#model").change(function () {

                var categoryId = $("#category option:selected").val();
                var brandId = $("#brand option:selected").val();
                var modelId = this.value;
                FillCity(categoryId, brandId, modelId, 0,0);
                FillSerie(categoryId, brandId, modelId);
                $("#locality").attr("disabled", true);
                
            })
            $("#serie").change(function () {

                var categoryId = $("#category option:selected").val();
                var brandId = $("#brand option:selected").val();
                var modelId = $("#model option:selected").val();
                var seriId = this.value;
                FillCity(categoryId, brandId, modelId, seriId, 0);
                $("#locality").attr("disabled", true);
                
               
            })

            $("#city").change(function () {
                var sector = $("#sector option:selected").val();
                var productGroupId = $("#productGroup option:selected").val();
                var categoryId = $("#category option:selected").val();
                var brandId = $("#brand option:selected").val();
                var modelId=$("#model option:selected").val();
                var serieId=$("#seri option:selected").val();
                if (serieId == null) serieId = 0;
                if (brandId == null) brandId = 0;
                if (modelId == null) modelId = 0;
                var cityId = this.value;
                if (categoryId == null || categoryId == 0) categoryId = productGroupId;
                if (categoryId == null || categoryId == 0) categoryId = sector;
                $("#locality").removeAttr("disabled");
                    
                FillCity(categoryId, brandId, modelId, serieId, cityId);
            })


        });

        function FillCategory(CategoryId, divName, loadingId) {
            $("#" + loadingId).show();
            $.ajax({
                url: '/Category/GetSubCategory',
                data: { categoryId: CategoryId },
                type: 'post',
                success: function (data) {
                    $("#" + divName).html("");
                    if (data.length > 0) {
                        $("#" + divName).append("<option value='0' selected >Tümü</option>");
                        $.each(data, function (key, value) {
                       
                            $("#" + divName).show();
                            $("#" + divName).append("<option value='" + value.Value + "'>" + value.Name + "</option>");
                        });
                    }
                    $("#"+loadingId).hide();
                },
                error: function (x, l, e) {
                }
            });
        }
        function FillCity(CategoryId,BrandId,ModelId,SerieId,CityId)
        {
            $.ajax({
                url: '/Category/GetCities',
                data: { categoryId: CategoryId, brandId: BrandId, modelId: ModelId, serieId: SerieId, cityId: CityId },
                type: 'post',
                success: function (data) {
                    if (CityId == 0) {

                        $("#city").html("");
                        $("#country").html("");
                        if (data.Countries.length > 0) {
                           
                            $.each(data.Countries, function (key, value) {

                                $("#country").append("<option value='" + value.Value + "'>" + value.Name + "</option>");
                            });
                        }

                    if (data.Cities.length > 0) {
                   
                        $.each(data.Cities, function (key, value) {
                        
                            $("#city").append("<option value='" + value.Value + "'>" + value.Name + "</option>");
                        });
                    }
                    }
                    else if(CityId>0)
                    {
                        if (data.Localities.length > 0) {
                            $("#locality").html("");
                           
                            $("#locality").append("<option value='0' selected >Tümü</option>");
                            $.each(data.Localities, function (key, value) {

                                $("#locality").append("<option value='" + value.Value + "'>" + value.Name + "</option>");
                            });
                        }
                    }
       
              
                },
                error: function (x, l, e) {
                }
            });

        }
        function FillBrand(CategoryId)
        {
            $("#brandLoading").show();
            $.ajax({
                url: '/Category/GetBrands',
                data: { selectedCategoryId: CategoryId },
                type: 'post',
                success: function (data) {
                    $("#brand").html("");
                    if (data.FilterItems.length > 0) {
                        $("#brand").append("<option value='0' selected>Tümü</option>");
                        $.each(data.FilterItems, function (key, value) {
                            $("#brand").show();
                            $("#brand").append("<option value='" + value.Value + "'>" + value.Name + "</option>");
                        });
                    }
                    $("#brandLoading").hide();
                },
                error: function (x, l, e) {
                }
            });

        }
        function FillModel(CategoryId,BrandId) {
            $("#modelLoading").show();
            $.ajax({
                url: '/Category/GetModels',
                data: { selectedCategoryId: CategoryId, selectedBrandId: BrandId },
                type: 'post',
                success: function (data) {
                    $("#model").html("");
                    if (data.FilterItems.length > 0) {
                        $("#model").append("<option value='0' selected>Tümü</option>")
                        $.each(data.FilterItems, function (key, value) {
                            $("#model").show();
                            $("#model").append("<option value='" + value.Value + "'>" + value.Name + "</option>");
                        });
                    }
                    $("#modelLoading").hide();
                },
                error: function (x, l, e) {
                }
            });
        }
        function FillSerie(CategoryId, BrandId,ModelId) {
            $("#serieLoading").show();
            $.ajax({
                url: '/Category/GetSeries',
                data: { selectedCategoryId: CategoryId, selectedBrandId: BrandId,selectedModelId:ModelId },
                type: 'post',
                success: function (data) {
                    $("#serie").html("");
                    if (data.FilterItems.length > 0) {
                        $("#serie").append("<option value='0'>Tümü</option>");
                        $.each(data.FilterItems, function (key, value) {
                            $("#serie").show();
                            $("#serie").append("<option value='" + value.Value + "'>" + value.Name + "</option>");
                        });
                    }
                    $("#serieLoading").hide();
                },
                error: function (x, l, e) {
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
      <h1 class="section-title" style="margin:0px 0px 20px">
                <span>Detaylı Arama</span>
        </h1> 
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-9 ">

                <div class="row">
        <div></div>
    </div>
    <div class="row"> 
      
        <%--<h1 style="font-size:20px">Detaylı Arama</h1>--%>
       
        <ul class="nav nav-tabs nav-justified" id="myTab" role="tablist">
<%--            <li class="nav-item active">
                   <a class="nav-link active" id="advert-tab" data-toggle="tab" href="#advert" role="tab" aria-controls="home" aria-selected="true">Ürün Ara</a>
            </li>--%>
<%--             <li class="nav-item">
                   <a class="nav-link active" id="advert-tab2" data-toggle="tab" href="#advert" role="tab" aria-controls="home" aria-selected="true">Firma Arama</a>
            </li>
             <li class="nav-item">
                   <a class="nav-link active" id="advert-tab3" data-toggle="tab" href="#advert" role="tab" aria-controls="home" aria-selected="true">Kategoriden Firma Ara</a>
            </li>
             <li class="nav-item">
                   <a class="nav-link active" id="advert-tab4" data-toggle="tab" href="#advert" role="tab" aria-controls="home" aria-selected="true">Ürün Kodu İle Arama</a>
            </li>--%>

        </ul>
        <div class="tab-content" id="myTabContent">
            <div class="tab-pane fade show active in" id="advert" role="tabpanel" aria-labelledby="advert-tab">
                <div class="col-md-12" style="background-color: #fff; border: 1px solid #b9c7ef; padding: 10px;">
                    <%using (Html.BeginForm("AdvancedSearch", "Category", FormMethod.Post))
                        { %>
                    <div class="row">
                        <div class="col-xs-12">
                            <h2 class="section-title2" style="margin: 10px 0px 10px">
                                <span class="bgwhite">Kategori</span>
                            </h2>

                        </div>
                        <div class="col-xs-12 scrollCategory">
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">

                                    <select class="form-control" name="sector" id="sector" size="10">
                                        <%foreach (var item in Model.SectorList)
                                            {%>
                                        <option value="<%:item.CategoryId %>"><%:item.CategoryName%></option>
                                        <%} %>
                                    </select>
                                    <img src="/Content/V2/images/loading.gif" style="width: 30px; display: none;" id="productGorupLoading" />
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">

                                    <select class="form-control" id="productGroup" name="ProductGroup" size="10" style="display: none;">
                                    </select>
                                    <img src="/Content/V2/images/loading.gif" style="width: 30px; display: none;" id="categoryLoading" />
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">

                                    <select class="form-control" name="category" id="category" style="display: none;" size="10">
                                    </select>
                                    <img src="/Content/V2/images/loading.gif" style="width: 30px; display: none;" id="brandLoading" />
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row" id="brandsRow" style="display: none;">
                        <div class="col-xs-12">
                            <h2 class="section-title2" style="margin: 10px 0px 10px">
                                <span class="bgwhite">Marka</span>
                            </h2>
                        </div>
                        <div class="col-xs-12">
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">

                                    <select class="form-control" id="brand" name="brand" style="display: none;" size="10">
                                    </select>
                                    <img src="/Content/V2/images/loading.gif" style="width: 30px; display: none;" id="modelLoading" />
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">

                                    <select class="form-control" id="model" name="model" style="display: none;" size="10">
                                    </select>
                                    <img src="/Content/V2/images/loading.gif" style="width: 30px; display: none;" id="serieLoading" />
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">

                                    <select class="form-control" id="serie" name="serie" style="display: none;" size="10">
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12">
                            <h2 class="section-title2" style="margin: 10px 0px 10px">
                                <span class="bgwhite">Adres</span>
                            </h2>

                        </div>
                        <div class="col-xs-12">
                            <div class="col-md-3">
                                <select class="form-control" id="country" name="country">
                                    <option value="0">Ülke</option>
                                </select>
                                <select class="form-control" id="city" name="city" style="margin-top: 10px;">
                                    <option value="0">İl</option>
                                </select>
                                <select class="form-control" disabled id="locality" name="locality" style="margin-top: 10px;">
                                    <option value="0">İlçe</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="margin-top: 20px;">

                        <div class="col-xs-12 text-center">
                            <input type="submit" class="btn btn-primary" value="Ara" style="padding-left:25px;padding-right:25px;" />
                        </div>
                    </div>
                </div>
                <%} %>
            </div>
        </div>
</div>


        </div>
        <div class="col-xs-12 col-sm-12 col-md-3 ">
            <div class="panel panel-default" >
                <div class="panel-heading">
                    <h3 class="panel-title">Arama İpuçları</h3>
                </div>
                <div class="panel-body">
                    <p>MakinaTürkiye arama motoru, büyük/küçük harf ayrımı yapmamaktadır.</p>
                </div>
            </div>
        </div>
    </div>


     
            
 
     
</asp:Content>
