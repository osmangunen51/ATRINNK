function mainImage(productID, picturePath) {
    $.ajax({
        url: '/Advert/mainImage',
        type: 'delete',
        data: { productID: productID, picturePath: picturePath },
        success: function (data) {
            $('#divPictureList').html(data);
        },
        error: function (x, l, e) {
            alert(x.responseText);
        }
    });
}

function DeleteImage(index, productID, picturePath) {
    $.ajax({
        url: '/Advert/DeleteImage',
        type: 'delete',
        data: { index: index, productID: productID, picturePath: picturePath, horizontal: false },
        success: function (data) {
            $('#divPictureList').html(data);
        },
        error: function (x, l, e) {
            // alert(x.responseText);
        }
    });
}