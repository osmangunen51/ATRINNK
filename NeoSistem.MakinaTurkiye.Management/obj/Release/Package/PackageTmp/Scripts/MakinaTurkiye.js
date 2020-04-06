/// <reference path="jquery.js" />

$.widget("ui.form", {
  _init: function () {
    var object = this;
    var form = this.element;
    var inputs = form.find("input[type='checkbox'], input[type='button'], .dropdown  ");

    form.find("fieldset").addClass("ui-widget-content");
    form.find("fieldset").css("padding", "5px");
    form.find("legend").addClass("ui-widget-header ui-corner-all");
    form.find("legend").css("padding", "3px");
    form.find("legend").css("margin-left", "3px");
    form.addClass("ui-widget");

    $.each(inputs, function () {
      $(this).addClass('ui-state-default ui-corner-all');
      $(this).wrap("<label />");

      if ($(this).is(":reset, :submit"))
        object.buttons(this);
      else if ($(this).is(":checkbox"))
        object.checkboxes(this);
      else if ($(this).is(":radio"))
        object.radio(this);
      else if ($(this).is("select"))
        object.selector(this);

      if ($(this).hasClass("date"))
        $(this).datepicker();
    });

    $(".hover").hover(function () {
      $(this).addClass("ui-state-hover");
    }, function () {
      $(this).removeClass("ui-state-hover");
    });

  },
  buttons: function (element) {
    if ($(element).is(":submit")) {
      $(element).addClass("ui-priority-primary ui-corner-all ui-state-disabled hover");
      $(element).bind("click", function (event) {
        event.preventDefault();
      });
    }
    else if ($(element).is(":reset"))
      $(element).addClass("ui-priority-secondary ui-corner-all hover");
    $(element).bind('mousedown mouseup', function () {
      $(this).toggleClass('ui-state-active');
    }

			  );
  },
  checkboxes: function (element) {
    $(element).parent("label").after("<span />");
    var parent = $(element).parent("label").next();
      $(element).addClass("ui-helper-hidden");
    parent.css({ width: 16, height: 16, display: "block" });
    parent.wrap("<span class='ui-state-default' style='display:inline-block;width:16px;height:16px;margin-right:5px;'/>");
    parent.parent().addClass('hover');

    if ($(element).attr('checked')) {
      parent.parent("span").addClass('ui-state-active');
      parent.toggleClass("ui-icon ui-icon-check");
    }

    parent.parent("span").click(function (event) {
//      $(this).toggleClass("ui-state-active");
//      parent.toggleClass("ui-icon ui-icon-check");
      $(element).click();
    });

    $(element).click(function () {
      parent.parent("span").toggleClass('ui-state-active');
      parent.toggleClass("ui-icon ui-icon-check");
    });

  },
  radio: function (element) {
    $(element).parent("label").after("<span />");
    var parent = $(element).parent("label").next();
    $(element).addClass("ui-helper-hidden");
    parent.addClass("ui-icon ui-icon-radio-off");
    parent.wrap("<span class='ui-state-default ui-corner-all' style='display:inline-block;width:16px;height:16px;margin-right:5px;'/>");
    parent.parent().addClass('hover');
    parent.parent("span").click(function (event) {
      $(this).toggleClass("ui-state-active");
      parent.toggleClass("ui-icon-radio-off ui-icon-bullet");
      $(element).click();
    });
  },
  selector: function (element) {
    var parent = $(element).parent();

    parent.css({ "display": "block", width: $(element).attr('w').toString(), height: 21, "cursor": "default" }).addClass("ui-state-default ui-corner-all");
    parent.addClass('hover');
    $(element).addClass("ui-helper-hidden");
    parent.append("<span id='labeltext" + $(element).attr('id') + "' style='float:left;display:inline-block;margin:3px'></span><span style='float:right;display:inline-block; margin-top:3px; cursor:default' class='ui-icon ui-icon-triangle-1-s'></span>");
    parent.after("<ul class='ui-helper-reset ui-widget-content ui-helper-hidden' style='position:absolute;z-index:999;  max-height:150px; overflow-y: auto; overflow-x: hidden;'></ul>");
    $.each($(element).find("option"), function () {
      $(parent).next("ul").append("<li class='hover' style='height: 20px; border:none; font-size:12px;margin:1px; padding-top:3px; padding-left:3px; cursor:default' val='" + $(this).val() + "'>" + $(this).html() + "</li>");
    });
    var e = $(element)[0];

    $("#labeltext" + $(element).attr('id')).html(e.options[e.selectedIndex].text);
    $(element).val(e.options[e.selectedIndex].value);

    $(parent).next("ul").find("li").click(function () {
      $("#labeltext" + $(element).attr('id')).html($(this).html());
      $(element).val($(this).attr('val'));
      $(parent).next().fadeOut('fast');
      $(element).trigger('onchange');
    });
    $(parent).click(function (event) {
      $(this).next().fadeIn('fast');
      event.preventDefault();
    });
  }
});

$(function () {
  $(document).form();
});