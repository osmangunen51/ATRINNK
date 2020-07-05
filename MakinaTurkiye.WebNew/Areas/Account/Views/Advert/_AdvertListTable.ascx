﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.MTProductItem>>" %>
<%:Html.Hidden("CurrentPage",Model.CurrentPage) %>
<%
    string urlAdd = "";
    byte? queryProductActiveType = null;
    if (!string.IsNullOrEmpty(Request.QueryString["ProductActiveType"]))
    {
        queryProductActiveType = Request.QueryString["ProductActiveType"].ToByte();
    }
    else if (!string.IsNullOrEmpty(Request.QueryString["ProductActive"]))
    {
        queryProductActiveType = Request.QueryString["ProductActive"].ToByte();
    }

    if (queryProductActiveType == null)
    {
        queryProductActiveType = Request.Form["ProductActiveType"].ToByte();
    }
    if (queryProductActiveType == 0)
    {
        urlAdd = "?pageType=firstadd";
    }
%>
<script type="text/javascript">
    $(document).ready(function () {
        $(".clearable").each(function () {

            var $inp = $(this).find("input:text"),
                $cle = $(this).find(".clearable__clear");

            $inp.on("input", function () {
                $cle.toggle(!!this.value);
            });

            $cle.on("touchstart click", function (e) {
                e.preventDefault();
                $inp.val("").trigger("input");
            });

        });
        $('.searchText').on('keypress', function (e) {
            if (e.which === 13) {
                var currentPage = $("#CurrentPage").val();
                var displayType = $("#DisplayType").val();
                var activeType = $("#ProductActiveType").val();
                advertPageChange(currentPage, displayType, activeType);


            }
        });
    })
</script>
<style>
    .clearable { position: relative; display: inline-block; }
        .clearable input[type=text] { padding-right: 24px; width: 100%; box-sizing: border-box; }
    .clearable__clear { display: none; position: absolute; right: 0; top: 0; padding: 0 8px; font-style: normal; font-size: 1.2em; user-select: none; cursor: pointer; }
    .clearable input::-ms-clear { /* Remove IE default X */ display: none; }
</style>

<table class="table table-striped">
    <thead class="thead-light">
        <tr>

            <th scope="col">Ürün Numarası</th>
            <th scope="col">Ürün Adı</th>

            <th scope="col">Kategori</th>
            <th scope="col">Marka</th>
            <th scope="col">Fiyat</th>
            <th scope="col">Araçlar</th>
        </tr>
        <tr>
            <td>
                <span class="clearable">
                    <input class="searchText  form-control" id="ProductNo" type="text" value="#" />
                    <i class="clearable__clear">&times;</i>
                </span></td>
            <td>
                <span class="clearable">
                    <input class="searchText form-control" type="text" id="ProductName" placeholder="Ürün Adı" />
                    <i class="clearable__clear">&times;</i>
                </span></td>

            <td>
                <span class="clearable">
                    <input class="searchText  form-control" type="text" id="CategoryName" placeholder="Kategori.." />
                    <i class="clearable__clear">&times;</i>
                </span></td>
            <td>  <span class="clearable">
                    <input class="searchText  form-control" type="text" id="BrandName" placeholder="Marka.." />
                    <i class="clearable__clear">&times;</i>
                </span></td>
            <td></td>
            <td></td>
        </tr>
    </thead>
    <%if (Model.Source.Count > 0) {%>
        <%foreach (var item in Model.Source)
            {%>
    <%   string productUrl = Helpers.ProductUrl(item.ProductId, item.ProductName);
    %>
    <tr>
        <td>
            <a href="<%:productUrl %>">
                <img style="height: 80px;" src="<%=item.ImagePath %>" class="img-responsive" /><br />
                <b><%:item.ProductNo %></b></a></td>
        <td><%:item.ProductName %></td>

        <td><%:item.CategoryName %></td>
        <td><%:item.BrandName %></td>
        <td>
            <strong style="color: #666">
                <%if (!string.IsNullOrEmpty(item.CurrencyCssText) && !string.IsNullOrEmpty(item.ProductPrice))
    { %>
                <i class="<%:item.CurrencyCssText %>"></i>
                <%} %>

                <%if (!string.IsNullOrEmpty(item.ProductPrice))
    {%>
                <span class="<%:!string.IsNullOrEmpty(item.ProductPriceWithDiscount) ? "old-price":"" %>">
                <%:item.ProductPrice %>
                </span>
                    <%if (!string.IsNullOrEmpty(item.ProductPriceWithDiscount)) { %>
                        <%:item.ProductPriceWithDiscount %>
                <%} %>
                        
                <%}
    else
    {%>
                Görüşülür
                <% } %>
        
            </strong></td>
        <td>
            <a title="Ürünü Düzenle" href="/Account/Advert/Edit/<%:item.ProductId %>"><i class="fa fa-edit" style="color: #0219a3; font-size: 20px;"></i></a>

        </td>
    </tr>
    <%  } %>
    
    <% } else {%>
        <tr>
            <td colspan="6">   
                <p class="bg-info">
        Aradığınız kriterlerde ürün bulunamamaştır.
    </p></td>
        </tr>
    <% } %>

</table>



<% if (Model.Source.Count > 0)
    { %>
<div class="row">
    <div class="col-md-5 ">
        Toplam
        <%= Model.TotalPages.Count() %>
        sayfa içerisinde
        <%= Model.CurrentPage %>.sayfayı görmektesiniz.
    </div>
    <div class="col-md-7 text-right">
        <ul class="pagination m0">
            <li><a href="#">&laquo;</a></li>
            <% Session["TotalRecord"] = Model.TotalRecord; %>
            <%  foreach (int item in Model.TotalLinkPages)
                {
                    if (Model.CurrentPage == item)
                    { %>
            <li class="active"><a onclick="advertPageChange(<%: item %>,<%= (byte) DisplayType.Table %>, <%= queryProductActiveType %>);" href="javascript:void(0)">
                <%: item %></a></li>
            <% }
                else
                {
            %>
            <li data-rel="liActive" data-id="<%: item %>"><a onclick="advertPageChange(<%: item %>,<%= (byte) DisplayType.Table %>, <%= queryProductActiveType %>);" href="javascript:void(0)">
                <%: item %></a></li>
            <% } %>
            <% } %>
            <%if (Model.CurrentPage < Model.TotalPages.Count())
                {
            %>
            <li data-id="<%: Model.CurrentPage+1 %>"><a onclick="advertPageChange(<%: Model.CurrentPage+1  %>,<%= (byte) DisplayType.Table %>, <%= queryProductActiveType %>);" href="javascript:void(0)">>></a> </li>

            <%} %>
        </ul>
    </div>
</div>
<% } %>

<%:Html.HiddenFor(x=>x.CurrentPage,new { @id="currentPage"}) %>


<script type="text/javascript">
    function getUrlVars() {
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    }

    function advertPageChange(p, d, a) {
        $(".loading").show();
        $('[data-rel="liActive"]').removeClass("active");
        $('[data-rel="liActive"][data-id="' + p + '"]').addClass("active");
        try {
            //                          var curpage = "&currentPage=1";
            //                        if(getUrlVars()["currentPage"]!=null){
            var curpage = "&currentPage=" + p;
            //}

            //history.pushState(window.location.search, "page new", "?ProductActive=" + getUrlVars()["ProductActive"] + "&DisplayType=" + getUrlVars()["DisplayType"] + curpage);
            history.pushState(window.location.search, "page new", "?ProductActiveType=" + $('#hdnActiveType').val() + "&DisplayType=" + getUrlVars()["DisplayType"] + curpage);

        } catch (e) {

        }

        var ProductNo = $("#ProductNo").val();
        var CategoryName = $("#CategoryName").val();
        var ProductName = $("#ProductName").val();
        var BrandName = $("#BrandName").val();

        $.ajax({
            url: '/Account/ilan/AdvertPaging',
            type: 'post',
            data: {
                page: p, displayType: d, advertListType: $('#hdnActiveType').val(),
                productActive: $("#ProductActive").val(), productActiveType: a,
                productNo: ProductNo,
                categoryName: CategoryName,
                productName: ProductName,
                brandName: BrandName
            },
            success: function (data) {

                $('[data-rel="tab-content"]').html(data);
                $(".loading").hide();
                $("#ProductNo").val(ProductNo);
                $("#CategoryName").val(CategoryName);
                $("#ProductName").val(ProductName);
                $("#BrandName").val(BrandName);

            }
        });


    }

</script>
