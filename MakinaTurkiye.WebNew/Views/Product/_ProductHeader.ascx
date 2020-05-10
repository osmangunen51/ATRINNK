﻿<%@ Control Language="C#" %>
<link rel="canonical" href="<%:ViewBag.Canonical %>" />
<script type="text/javascript" src="//s7.addthis.com/js/300/addthis_widget.js#pubid=ra-54cd19b3604b2ec3" async="async"></script>
<script src="https://www.google.com/recaptcha/api.js" async defer></script>

<style type="text/css">
    .well {
        background: #fff;
        text-align: center;
    }

    .modal {
        text-align: left;
    }
</style>
<style type="text/css">
    .btn-default-price-color {
        background-color: #314288;
    }


</style>
    <script type="text/javascript">
   
        function onBeginSend() {
            $("#loading").show();
        }
        function onSuccess(data, content, xhr) {
            $("#loading").hide();
            if (data)
            {
                $("#ajaxSuccess").show();
                $("#ajaxError").hide();
                $("#UserName").val('');
                $("#UserSurname").val('');
                $("#UserPhone").val('');
                $("#UserEmail").val(' ');
                $("#UserComment").val('');
                $(".col-md-6>input:checkbox").prop('checked', false);
            }
            else {
                $("#ajaxError").show();
                $("#ajaxSuccess").hide();
            }
         
          
        }
        function ajaxError() {
            $("#ajaxError").show();
            $("#ajaxSuccess").hide();
        }

                function CertificatePopUpGallery() {
            $('.certificate-popup-gallery').magnificPopup({
                delegate: 'div a',
                type: 'image',
                tLoading: 'Resim Yükeniyor #%curr%...',
                mainClass: 'mfp-img-mobile',
                gallery: {
                    enabled: true,
                    navigateByImgClick: true,
                    preload: [0, 1] // Will preload 0 - before current, and 1 after the current image
                },
                image: {
                    tError: '<a href="%url%"> #%curr%</a> resim yüklenemedi',
                    titleSrc: function (item) {
                        return item.el.attr('title') + '<small></small>';
                    }
                }
            });
        }


        $(document).ready(function () {
            CertificatePopUpGallery();
        });
</script>

