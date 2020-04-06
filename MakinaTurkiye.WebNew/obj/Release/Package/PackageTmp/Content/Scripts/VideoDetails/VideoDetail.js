
$(function () {
    setInterval(function () {
        if ($("#automaticPlay").is(":checked")) {
            $('#vd_html5_api').on('ended', function() {
                VideoFinished();
            });
        }
    }, 3000);

    $("#automaticPlay").change(function () {
        if ($(this).is(':checked'))
        {
           $('#vd_html5_api').on('ended', function() {
                VideoFinished();
            });
        }
        else
        {
            $(this).removeAttr("checked");
            
        }
        $.ajax({
            url: '/Videos/SetVideoAutomaticStatus',
            type: 'post',
            data: {
                status: $(this).is(':checked')
                //                productId: productId
            }
        });
        
    })
   
});
function VideoFinished() {
     //What you want to do after the eventa<
    alert($('ul.media-list li:eq(0) a:eq(0)').attr('href'));
    location.href =""+$('ul.media-list li:eq(0) a:eq(0)').attr('href');
}
function PopulerVideosRemoveItemVideo(videoId) {
    $(window).load(function () {
        var nextVideoIdItem = "V_" + videoId;
        $("li#" + nextVideoIdItem).remove();
    });
}


