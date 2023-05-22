<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<NeoSistem.Trinnk.Management.Models.HelpListModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Satış Yardım Listesi
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="content">
  <div id="wrapper">
    <div id="jquery-accordion-menu" class="jquery-accordion-menu red" style="float:left;width:20%">
    <input type="text" class="crmtxtara" id="txthelpsearch" onkeyup="yardimfilter()" placeholder="ara" />
    <div class="jquery-accordion-menu-header" id="form" style="background-color:#051367;color:white;">
        Yardım Kategorileri
    </div>
    <ul id="help-list">
       <%foreach (var item in Model.WHelpModels.GroupBy(x=>x.ConstantId))
           {
               var cnstant = Model.WHelpModels.FirstOrDefault(x => x.ConstantId== item.Key).Constant;
               var subjects = Model.WHelpModels.Where(x => x.ConstantId==cnstant.ConstantId);
               %>
                <li><a href="#"><i class="fa fa-home"><%=(cnstant.ConstantId==0?"Kategori Yok":cnstant.ConstantName)%> </i></a>
                    <%if (subjects.Count()>0)
                   {%>
                       <ul class="submenu">
                          <%foreach (var sub in subjects)
                              {%>
                                  <li><a href="#" class="opencnt" data-baslik="<%=sub.Subject%>" data-icerik='<%=Html.Raw(sub.Content)%>'><%=sub.Subject%></a></li>
                              <%}%>
                        </ul>
                   <%}%> 
               </li>
           <%}%>
    </ul>
    <%--<div class="jquery-accordion-menu-footer">
      Footer
    </div>--%>
  </div>
      <div id="pnlcontent" class="pnlcontent" style="min-height:480px;border:1px solid #e2e4e7;float:right;width:80%;padding-left:10px;padding-right:10px;font-size:16px">
          <button id="btncollapse" class="">
              <<
          </button>
          <h3 style="display:inline-table;width:auto" id="pnlbaslik">
          </h3>
          <button id="btnYazdir" style="display:inline-table;float:right;margin-top:25px" onclick="print('pnlcontent')">
              Yazdır
          </button>
          <hr />
          <p id="pnlicerik">
          </p>
  </div>
  </div>

  
</div>
<script type="text/javascript">
    var btncollapse = $("#btncollapse");
    var btnYazdir = $("#btnYazdir");
    var solpanel = $("#jquery-accordion-menu");
    var pnlcontent = $("#pnlcontent");
    btncollapse.hide();
    btnYazdir.hide();
    btncollapse.click(function () {
        
        solpanel.toggle();
        if (pnlcontent.hasClass("pnlcontent"))
        {
            btncollapse.html('<span class="ui-button-text">>></span>');
            pnlcontent.removeClass("pnlcontent");
            pnlcontent.addClass("pnlcontentfullpage");
        }
        else {
            btncollapse.html('<span class="ui-button-text"><<</span>');
            pnlcontent.removeClass("pnlcontentfullpage");
            pnlcontent.addClass("pnlcontent");
        }
    });
    function yardimfilter() {
        // Declare variables
        var input, filter, ul, li, a, i, txtValue;
        input = document.getElementById('txthelpsearch');
        filter = input.value.toLowerCase();
        //$('#help-list>li').filter(function (index) {
        //    return filter.length == 0 || $(this).text().toLowerCase().indexOf(filter.toLowerCase());
        //}).css("border", "3px double red");
        $('#help-list>li').each(function () {
            var hasMatch = filter.length == 0 || $(this).text().toLowerCase().indexOf(filter.toLowerCase()) > 0;
            $(this).toggle(hasMatch);
            //$(this).parent().toggle(hasMatch);
        });

        $('#help-list>li>.submenu >li').each(function () {
            var hasMatch = filter.length == 0 || $(this).text().toLowerCase().indexOf(filter.toLowerCase()) > 0;
            $(this).toggle(hasMatch);
            $(this).parent().toggle(true);
        });
    }

    $(".opencnt").click(
        function () {
            var baslik = $(this).attr("data-baslik");
            var icerik = $(this).attr("data-icerik");
            $("#pnlbaslik").html(baslik);
            $("#pnlicerik").html(icerik);
            //btncollapse.attr("class", "");
            btncollapse.show();
            btnYazdir.show();
        }
    );
    ; (function ($, window, document, undefined) {
        var pluginName = "jqueryAccordionMenu";
        var defaults = {
            speed: 300,
            showDelay: 0,
            hideDelay: 0,
            singleOpen: true,
            clickEffect: true
        };

        function Plugin(element, options) {
            this.element = element;
            this.settings = $.extend({}, defaults, options);
            this._defaults = defaults;
            this._name = pluginName;
            this.init();
        }

        $.extend(Plugin.prototype, {
            init: function () {
                this.openSubmenu();
                this.submenuIndicators();
                if (defaults.clickEffect) {
                    this.addClickEffect();
                }
            },
            openSubmenu: function () {
                $(this.element).children('ul').find('li').bind('click touchstart', function (e) {
                    e.stopPropagation();
                    e.preventDefault();
                    if ($(this).children('.submenu').length > 0) {
                        if ($(this).children('.submenu').css('display') == 'none') {
                            $(this).children('.submenu').delay(defaults.showDelay).slideDown(defaults.speed);
                            $(this).children('.submenu').siblings('a').addClass('submenu-indicator-minus');
                            if (defaults.singleOpen) {
                                $(this).siblings().children('.submenu').slideUp(defaults.speed);
                                $(this).siblings().children('.submenu').siblings('a').removeClass('submenu-indicator-minus');
                            }
                            return false;
                        } else {
                            $(this).children('.submenu').delay(defaults.hideDelay).slideUp(defaults.speed);
                        }

                        if ($(this).children('.submenu').siblings('a').hasClass('submenu-indicator-minus')) {
                            $(this).children('.submenu').siblings('a').removeClass('submenu-indicator-minus');
                        }
                    }
                    window.location.href = $(this).children('a').attr('href');
                })
            },
            submenuIndicators: function () {
                if ($(this.element).find('.submenu').length > 0) {
                    $(this.element).find('.submenu').siblings('a').append('<span class="submenu-indicator">+</span>');
                }
            },
            addClickEffect: function () {
                var ink, d, x, y;
                $(this.element).find('a').bind('click touchstart', function (e) {
                    $('.ink').remove();
                    if ($(this).children('.ink').length === 0) {
                        $(this).prepend('<span class="ink"></span>');
                    }
                    ink = $(this).find('.ink');
                    ink.removeClass('animate-ink');
                    if (!ink.height() && !ink.width()) {
                        d = Math.max($(this).outerWidth(), $(this).outerHeight());
                        ink.css({
                            height: d,
                            width: d
                        });
                    }
                    x = e.pageX - $(this).offset().left - ink.width() / 2;
                    y = e.pageY - $(this).offset().top - ink.height() / 2;
                    ink.css({
                        top: y + 'px',
                        left: x + 'px'
                    }).addClass('animate-ink');
                })
            }
        });
        $.fn[pluginName] = function (options) {
            this.each(function () {
                if (!$.data(this, 'plugin_' + pluginName)) {
                    $.data(this, 'plugin_' + pluginName, new Plugin(this, options));
                }
            });
            return this;
        }
    })(jQuery, window, document);
    jQuery('#jquery-accordion-menu').jqueryAccordionMenu();
    function print(divName)
    {
        btncollapse.hide();
        btnYazdir.hide();
        var printContents = document.getElementById(divName).innerHTML;
        btncollapse.show();
        btnYazdir.show();
        w = window.open();
        w.document.write(printContents);
        w.print();
        w.close();
    }

    $(window).scroll(function () {
        var offset = 0;
        var sticky = false;
        var top = $(window).scrollTop();

        if ($("#wrapper").offset().top < top) {
            $("#jquery-accordion-menu").addClass("sticky");
            sticky = true;
        } else {
            $("#jquery-accordion-menu").removeClass("sticky");
        }
    });
</script>
</asp:Content>

