﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StoreSearchModel>" %>
	  <div class="well well-mt2 form-horizontal"> 
					<label>Faaliyet Alanına Göre Firma Ara</label>
					<div class="input-group">
						<input type="text" class="form-control" name="categoryName" id="categoryName" placeholder="Dikiş Makinası Firmaları" />
						<input type="hidden" id="categoryUrlName" name="categoryUrlName" />
						<input type="hidden" id="CategoryId" name="CategoryId" />

						<span class="input-group-btn">
							<button id="btnFirmCategorySearch" class="btn btn-default" type="button"><span class="glyphicon glyphicon-search"></span></button>
						</span>
					</div>
	 </div>

<script type="text/javascript">

    //$("#categoryName").autocomplete({
    //    source: function (request, response) {
    //        $.ajax({
    //            url: "/StoreSearch/SearchCategory",
    //            type: "POST",
    //            dataType: "json",
    //            data: { categoryName: request.term },
    //            success: function (data) {
    //                response($.map(data, function (item) {
    //                    return { label: item.Text + " Firmaları", value: item.Text + " Firmaları", id: item.Value }
    //                }))
    //            },
    //            error: function (x, l, e) {
    //                alert(x.responseText);
    //            }

    //        })
    //    }, select: function (event, ui) {
    //        $('#CategoryId').val(ui.item.id);
    //    }
    //});
    $('#btnFirmCategorySearch').click(function () {

        $.ajax({
            url: '/StoreSearch/CategoryGetUrlName',
            data: { categoryName: $('#categoryName').val() },
            success: function (data) {
                $('#categoryUrlName').val(data);
                window.location = '/sirketler/' + $('#CategoryId').val() + '/' + $('#categoryUrlName').val() + '-firmalari/';
            },
            error: function (x, l, e) {
                alert(x.responseText);
            }
        });
    });


</script>
