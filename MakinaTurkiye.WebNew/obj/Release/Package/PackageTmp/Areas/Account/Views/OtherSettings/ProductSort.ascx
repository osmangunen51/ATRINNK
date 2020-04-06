<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.MTProductItem>>" %>
<script type="text/javascript">

    function PageChangeOtherSettings1(p, d, a) {
        $(".loading").show();
        $.ajax({
            url: '/Account/OtherSettings/AdvertSortList',
            type: 'post',
            data: {
                page: p,
                productName: $("#ProductName").val(),
                brandName: $("#BrandName").val(),
                categoryName: $("#CategoryName").val(),
                productNo: $("#ProductNo").val()
            },
            success: function (data) {
                console.log(data);
                $('#Advert').html(data);
                       $(".loading").hide();
            }
        });
    }
</script>
<style>
    .clearable {
        position: relative;
        display: inline-block;
    }

        .clearable input[type=text] {
            padding-right: 24px;
            width: 100%;
            box-sizing: border-box;
        }

    .clearable__clear {
        display: none;
        position: absolute;
        right: 0;
        top: 0;
        padding: 0 8px;
        font-style: normal;
        font-size: 1.2em;
        user-select: none;
        cursor: pointer;
    }

    .clearable input::-ms-clear { /* Remove IE default X */
        display: none;
    }
</style>
<div class="col-sm-12 col-md-12">
    <div>
        <h4 class="mt0 text-info">
            <span class="text-primary glyphicon glyphicon-cog"></span>&nbsp;İlan Ürün Sıralaması
        </h4>
        <div class="well well-mt2">
            <a class="btn btn-success" style="float: right; font-size: 13px;" onclick="ExportExcel();">Tüm Ürünleri Excele Aktar</a>
            <div id="Advert">


                        <%if (Model.Source.Count != 0)
                            {%>
                        <%=Html.RenderHtmlPartial("AdvertSortList",Model) %>
                        <%
                            }  %>
                  

            </div>
            <hr>
        </div>
    </div>
</div>
<div class="modal fade" id="SortModalEdit" tabindex="-1"
    role="dialog" aria-labelledby="helpModalLabel" aria-hidden="true">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close"
                    data-dismiss="modal">
                    <span aria-hidden="true">&times;
                    </span><span class="sr-only">Kapat</span></button>
                <h4 class="modal-title" id="H1">Sıra Numarası</h4>
            </div>
            <div class="modal-body">
                <input type="hidden" id="hdnSortId" name="hdnSortId" class="form-control" />
                <input type="text" id="SortValue" name="SortValue" class="form-control" />
            </div>
            <div class="modal-footer">
                <button type="button" onclick="SortEdit()" class="btn btn-default">
                    Güncelle</button>
                <button type="button"
                    class="btn btn-default" data-dismiss="modal">
                    Vazgeç</button>
            </div>
        </div>
    </div>
</div>
