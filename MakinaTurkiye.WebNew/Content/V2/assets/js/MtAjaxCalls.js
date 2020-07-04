
$("#ActivityCategory").change(function () {
    var sectorId = this.value;
    $("#SubCategory").html("");
    if (sectorId != "") {
        $.ajax({

            url: '/Account/StoreActivity/GetSubCategory',
            type: 'GET',
            data: {
                'sectorId': sectorId
            },
            dataType: 'json',
            success: function (data) {
                console.log(data);
                $.each(data.Result, function (i, item) {
                    $("#subCategoryContainer").show();
                    $("#SubCategory").append(
                        "<option value='" + item.CategoryId + "' >" + item.CategoryName + "</option>"
                    );

                });

            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
    }
    else {
        $("#subCategoryContainer").hide();
        $("#SubCategory").html("");
    }

});

function DeleteStoreActivityCategory(id) {
    if (confirm('Kaldırmak istediğinize emin misiniz?')) {
        $.ajax({

            url: '/Account/StoreActivity/Delete',
            type: 'GET',
            data: {
                'storeActivityCategoryId':id
            },
            dataType: 'json',
            success: function (data) {
                if (data) {
                    $("#storeActivity"+id).hide();
                }
            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });

    } 

}

function DeleteStoreSector(id) {
    if (confirm('Kaldırmak istediğinize emin misiniz?')) {
        $.ajax({

            url: '/Account/Store/DeleteSector',
            type: 'GET',
            data: {
                'storeSectorId': id
            },
            dataType: 'json',
            success: function (data) {
                if (data) {
                    $("#storeSector" + id).hide();
                }
            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });

    }

}
function AddFavoriteProductItem(id) {

    $("[data-productid=product-favorite-item-" + id + "]").attr("title", "Favorilerimden Kaldır");
    $.ajax({
        url: '/Product/AddFavoriteProduct',
        type: 'post',
        dataType: 'json',
        data:
            {
                ProductId: id
            },
        success: function (data) {
            if (data == true) {
 
                var item = $("[data-productid=product-favorite-item-" + id + "]");
               
                $("[data-productid=product-favorite-item-" + id + "]").attr("onclick", "RemoveFavoriteProductItem("+id+")");
                $("[data-productid=product-favorite-item-" + id + "]").attr("title", "Favorilerimden Kaldır");

                item.html("<i class='fa fa-heart'></i>");

                //                        $.facebox('Görüntülemiş olduğunuz ürün başarıyla favori ürünlerinize eklenmiştir.');
                //                        $('#divFavroriteProductImage').css('background-image', 'url(/Content/Images/removeFavorite.png)');
            }
            else {
                $("#FavoriteProductLoading").hide();
                //  $.facebox('Görüntülemiş olduğunuz ürünü favori ürünlerinize eklemek için sitemize üye girişi yapmanız veya üye olmanız gerekmektedir.');
                window.location.href = '/Uyelik/kullanicigirisi';
            }
        }
    });

}


function RemoveFavoriteProductItem(id) {


    $.ajax({
        url: '/Product/RemoveFavoriteProduct',
        type: 'post',
        data:
            {
                ProductId: id
            },
        success: function (data) {
            //                    $.facebox('Görüntülemiş olduğunuz ürün favori ürünlerinizden çıkarılmıştır.');
            var item = $("[data-productid=product-favorite-item-" + id + "]");

            $("[data-productid=product-favorite-item-" + id + "]").attr("onclick", "AddFavoriteProductItem(" + id + ")");
            $("[data-productid=product-favorite-item-" + id + "]").attr("title", "Favorilerime Ekle");

            item.html("<i class='fa fa-heart-o' ></i>");
            //                    $('#divFavroriteProductImage').css('background-image', 'url(/Content/Images/addFavorite.png)');
        },
        error: function (x, l, e) {
            $.facebox('Görüntülemiş olduğunuz ürün favori ürünlerinizden çıkarılırken bir sorun oluştu.');
        }
    });

}



$(document).ready(function () {

    $(".product-list-favorite-icon-c").click(function (event) {
        event.preventDefault();
    });
      
});