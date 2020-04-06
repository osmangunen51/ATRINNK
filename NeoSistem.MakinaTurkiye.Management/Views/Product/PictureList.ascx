<style type="text/css">
    .gallery {
        width: 100%;
        float: left;
        margin-top: 100px;
    }

        .gallery ul {
            margin: 0;
            padding: 0;
            list-style-type: none;
        }

            .gallery ul li {
                padding: 7px;
                border: 2px solid #ccc;
                float: left;
                margin: 10px 7px;
                background: none;
                width: auto;
                height: auto;
            }
</style>

<script type="text/javascript">
    $(document).ready(function () {
        $("ul.reorder-photos-list").sortable({ tolerance: 'pointer' });
        $('.reorder_link').html('Sıralamayı kaydet');
        $('.reorder_link').attr("id", "save_reorder");
        $('#reorder-helper').slideDown('slow');
        $('.image_link').attr("href", "javascript:void(0);");
        $('.image_link').css("cursor", "move");
        $("#save_reorder").click(function (e) {
            if (!$("#save_reorder i").length) {
                $(this).html('').prepend('<img src="/Content/Images/ajax-loader.gif"/>');
                $("ul.reorder-photos-list").sortable('destroy');
                $("#reorder-helper").html("Fotoğraf sıraları kaydediliyor...Lütfen bekleyiniz").removeClass('light_box').addClass('notice notice_error');

                var h = [];
                $("ul.reorder-photos-list li").each(function () { h.push($(this).attr('id').substr(9)); });
                $.ajax({
                    type: "POST",
                    url: "/Product/UpdatePictureOrder",
                    data: { idArray: " " + h + "" },
                    success: function (html) {
                        console.log(h);
                        window.location.reload();
                        /*$("#reorder-helper").html( "Reorder Completed - Image reorder have been successfully completed. Please reload the page for testing the reorder." ).removeClass('light_box').addClass('notice notice_success');
                        $('.reorder_link').html('reorder photos');
                        $('.reorder_link').attr("id","");*/
                    }

                });
                return false;
            }
            e.preventDefault();
        });


    });

</script>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ICollection<PictureModel>>" %>


<% if (Model.Count > 0) { %>
<div class="gallery">
    <ul class="reorder_ul reorder-photos-list">


        <% foreach (var item in Model) { %>
        <li id="image_li_<%=item.PictureId %>" class="ui-sortable-handle">
            <div style="width: 100px; height: 120px; margin-left: 10px; margin-top: 10px; float: left; text-align: center;">

                <%= Html.GetProductImage(item.ProductId, item.PicturePath, NeoSistem.MakinaTurkiye.Management.Helper.ImageHelpers.ImageSize.px100x75)%>
                <a style="cursor: pointer; text-decoration: none;" onclick="DeletePicture('<%=item.PictureId %>','<%=item.PicturePath %>');">Sil</a>
                <a style="cursor: pointer; text-decoration: none;" onclick="mainImage('<%=item.PictureId %>','<%=item.PicturePath %>');">Ana Resim</a>
            </div>
        </li>
        <% } %>
    </ul>
</div>
<% } else { %>
<div style="float: left; margin-top: 20px; margin-left: 20px;">
    Herhangi bir ürün resmi bulunamadı.
</div>
<% } %>
