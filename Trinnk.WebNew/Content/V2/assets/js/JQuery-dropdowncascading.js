/// <reference path="jquery-1.4.2.min.js" />
(function ($) {
  $.fn.DropDownCascading = function (settings) {
    settings = $.extend({ target: null, method: null, loader: null }, settings);
    var control = $(this);
    control.bind("change", function () {
      if (settings.target == null || settings.method == null) { alert("İşlemi gerçekleştirecek adresi veya işlem sonucunda etkilecenecek elementi belirtmediniz."); }
      else {
        $(settings.target).attr("disabled", "disabled");
        $(settings.loader).css("visibility", "visible");
        var selectedValue = control.val();
        $.ajax({ 
          url: settings.method,
          data: { id: selectedValue },
          success: function (msg) {
            $(settings.target + " > option").remove();
            $.each(msg, function (i) {
              $(settings.target).append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
            });
            $(settings.target).attr("disabled", "");
            $(settings.loader).css("visibility", "hidden");
          },
          error: function (e) { 
          }
        });
      }
    });
  }
})(jQuery); 