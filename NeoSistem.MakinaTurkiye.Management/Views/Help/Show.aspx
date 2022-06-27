<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.HelpListModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Satış Yardım Listesi
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
.fa {
  display: inline-block;
  font-style: normal;
  font-weight: normal;
  line-height: 1;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

* {
  box-sizing: border-box;
}

body {
  background: #f0f0f0;
}

.content {
  /*width: 260px;*/
  /*margin: 20px auto;*/
}

#demo-list a {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  width: 100%;
}

.jquery-accordion-menu, .jquery-accordion-menu * {
  box-sizing: border-box;
  outline: 0;
}

.jquery-accordion-menu {
  min-width: 260px;
  float: left;
  position: relative;
 border:1px solid #e2e4e7
}

.jquery-accordion-menu .jquery-accordion-menu-footer, .jquery-accordion-menu .jquery-accordion-menu-header {
  width: 100%;
  height: 50px;
  padding-left: 22px;
  float: left;
  line-height: 50px;
  font-weight: 600;
  color: #222222;
  background: #414956;
  font-size:18px;
}

.jquery-accordion-menu ul {
  margin: 0;
  padding: 0;
  list-style: none;
}

.jquery-accordion-menu ul li {
  width: 100%;
  display: block;
  float: left;
  position: relative;
}

.jquery-accordion-menu ul li a {
  width: 100%;
  padding: 14px 22px;
  float: left;
  text-decoration: none;
  color: #222222;
  font-size: 13px;
  background: #f5f5f5;
  white-space: nowrap;
  position: relative;
  overflow: hidden;
  transition: color .2s linear, background .2s linear;
}

.jquery-accordion-menu>ul>li.active, .jquery-accordion-menu>ul>li:hover>a {
  color: #fff;
  background: #4177dc;
}

.jquery-accordion-menu>ul>li>a {
  border-bottom: solid 1px #3b424d;
}

.jquery-accordion-menu ul li a i {
  width: 34px;
  float: left;
  line-height: 18px;
  font-size: 16px;
  text-align: left;
}

.jquery-accordion-menu .submenu-indicator {
  float: right;
  right: 22px;
  position: absolute;
  line-height: 19px;
  font-size: 20px;
  transition: transform .3s linear;
}

.jquery-accordion-menu ul ul.submenu .submenu-indicator {
  line-height: 16px;
}

.jquery-accordion-menu .submenu-indicator-minus>.submenu-indicator {
  transform: rotate(45deg);
}

.jquery-accordion-menu ul ul.submenu, .jquery-accordion-menu ul ul.submenu li ul.submenu {
  width: 100%;
  display: none;
  position: static;
}

.jquery-accordion-menu ul ul.submenu li {
  clear:both;
  width: 100%;
}

.jquery-accordion-menu ul ul.submenu li a {
  /*width: 285px;*/
  float: right;
  font-size: 13px;
  background: #86a2e9;
  border-top: none;
  position: relative;
  border-left: solid 6px transparent;
  transition: border .2s linear;
  display: inline-block;
  white-space: nowrap;
  overflow: hidden !important;
  text-overflow: ellipsis;
  margin:2px;
  border-radius:6px;
  margin-right:1px;
}

.jquery-accordion-menu ul ul.submenu li:hover>a {
  border-left-color: #414956;
}

.jquery-accordion-menu ul ul.submenu>li>a {
  padding-left: 10px;
}

.jquery-accordion-menu ul ul.submenu>li>ul.submenu>li>a {
  padding-left: 25px;
}

.jquery-accordion-menu ul ul.submenu>li>ul.submenu>li>ul.submenu>li>a {
  padding-left: 30px;
}

.jquery-accordion-menu ul li .jquery-accordion-menu-label, .jquery-accordion-menu ul ul.submenu li .jquery-accordion-menu-label {
  min-width: 20px;
  padding: 1px 2px 1px 1px;
  position: absolute;
  right: 18px;
  top: 14px;
  font-size: 11px;
  font-weight: 800;
  color: #555;
  text-align: center;
  line-height: 18px;
  background: #f0f0f0;
  border-radius: 100%;
}

.jquery-accordion-menu ul ul.submenu li .jquery-accordion-menu-label {
  top: 12px;
}

.ink {
  display: block;
  position: absolute;
  background: rgba(255,255,255,.3);
  border-radius: 100%;
  transform: scale(0);
}

.animate-ink {
  animation: ripple .5s linear;
}

@keyframes ripple {
  100% {
    opacity: 0;
    transform: scale(2.5);
  }
}

.red.jquery-accordion-menu .jquery-accordion-menu-footer,.red.jquery-accordion-menu .jquery-accordion-menu-header,.red.jquery-accordion-menu ul li a {
	background: #86a2e9
}

.red.jquery-accordion-menu>ul>li.active>a,.red.jquery-accordion-menu>ul>li:hover>a {
	background: #3d6bcd
}

.red.jquery-accordion-menu>ul>li>a {
	border-bottom-color: #ebf1f6
}

.red.jquery-accordion-menu ul ul.submenu li:hover>a {
	border-left-color: #ebf1f6
}
pnlcontent
{
    width:80% !important;
}
.pnlcontentfullpage
{
    width:100% !important;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="content">
  <div id="jquery-accordion-menu" class="jquery-accordion-menu red" style="float:left;width:20%">
    <div class="jquery-accordion-menu-header" id="form">
        Yardım Kategorileri
        
    </div>
    <input type="text" id="txthelpsearch" onkeyup="yardimfilter()" placeholder="yardım konusu giriniz" style="width: 100%;
    font-size: 16px;
    padding: 12px 20px 12px 20px;
    border: 1px solid #ddd;
    margin-bottom: 12px;
    /* margin-left: 5px; */
    margin-right: 5px;
    min-height: 32px;">
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
          <button id="btncollapse" style="display: inline-table;
    padding: 3px !important;
        border: 1px solid #d3d3d3;
    background: #e6e6e6 url('/content/smoothness/images/ui-bg_glass_75_e6e6e6_1x400.png') 50% 50% repeat-x;
    font-weight: normal;
    color: #555555;
    ">
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
</script>
</asp:Content>

