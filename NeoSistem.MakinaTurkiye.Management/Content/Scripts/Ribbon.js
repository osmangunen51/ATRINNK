/// <reference path="MicrosoftAjax.js" />
/// <reference path="NeoSistem.js" />
/// <reference path="jquery.js" />
/// <reference path="JQuery.cookie.js" />

$(document).ready(function () {
  var $x = $.cookie('ribbonActive');

  var group = $("." + $x).attr("group");
  $(".tab" + group).hide();
  $("#" + $x).show();
  $('.cs' + group + ' li a').attr("id", "");
  var e = '.cs' + group + ' .' + $x;
  $(e).attr("id", "selected");
});

function ribbonTabSlide($x) {
  $.cookie('ribbonActive', $x, { expires: 7, path: '/' });

  var group = $("." + $x).attr("group");
  $(".tab" + group).hide();
  $("#" + $x).show();
  $('.cs' + group + ' li a').attr("id", "");
  var e = '.cs' + group + ' .' + $x;
  $(e).attr("id", "selected");

}

function minimizeRibbon(clsName) {
  var e = $('.' + clsName).toggle();
  var value = e[0].style.display;
  if (value === 'none') {
    resizeHeight = 67;
  }
  else {
    resizeHeight = 156;
  }
}

function selectRow(rowId) {
  $(".Row").removeClass("SelectedRow");
  $("#" + rowId).addClass("SelectedRow");
}

function dataProcess(data) {
  var $dict1 = data;
  for (var $key2 in $dict1) {
    try {
      var item = { key: $key2, value: $dict1[$key2] };
      var bool = typeof (item.value) === 'boolean';
      if (bool) {
        $get(item.key).checked = item.value == true ? 'checked' : '';
      }
      else {
        $get(item.key).value = item.value == null ? '' : item.value;
      }
    } catch (e) {
      continue;
    }
  }
}

function CreateView(controllerName, successFunc) {
  /// <param name="successFunc" type="Function">
  /// </param>
  $.ajax({
    url: String.format('/{0}/{1}', controllerName, 'Create'),
    type: 'put',
    dataType: 'json',
    success: function (data) {
      successFunc.apply(null);
      dataProcess(data);
    }
  });
}

function EditView(controllerName, editId, successFunc) {
  /// <param name="successFunc" type="Function">
  /// </param>
  $.ajax({
    url: String.format('/{0}/{1}', controllerName, 'Edit'),
    data: { id: editId },
    type: 'put',
    dataType: 'json',
    success: function (data) {
      successFunc.apply(null);
      dataProcess(data);
    }
  });
}


function DeleteView(controllerName, deleteId, successFunc) {
  /// <param name="successFunc" type="Function">
  /// </param>

  var request = new Sys.Net.WebRequest();
  request.set_url(String.format("/{0}/{1}", controllerName, 'Delete'));
  request.set_body(NeoSistem.Model.Serialize({ id: deleteId }));
  request.add_completed(function (executor) {
    /// <param name="executor" type="Sys.Net.WebRequestExecutor">
    /// </param>
    var func = Function.createDelegate(null, successFunc);
    func(executor.get_responseData());
  });

  request.invoke();
}

function createElement(tagName) {
  return document.createElement(tagName);
}

Type.registerNamespace('Ribbon');

Ribbon.RibbonZone = function Ribbon_RibbonZone() {
}

Ribbon.ButtonType = function () {
  /// <field name="IconItem" type="Number" integer="true" static="true">
  /// </field>
  /// <field name="SmallItem" type="Number" integer="true" static="true">
  /// </field>
  /// <field name="BigItem" type="Number" integer="true" static="true">
  /// </field>
  /// <field name="DropDownIconItem" type="Number" integer="true" static="true">
  /// </field>
  /// <field name="DropDownSmallItem" type="Number" integer="true" static="true">
  /// </field>
  /// <field name="DropDownBigItem" type="Number" integer="true" static="true">
  /// </field>
};

Ribbon.ButtonType.prototype = {
  IconItem: 0,
  SmallItem: 1,
  BigItem: 2,
  DropDownIconItem: 3,
  DropDownSmallItem: 4,
  DropDownBigItem: 5
}

Ribbon.ButtonType.registerEnum('Ribbon.ButtonType', false);

Ribbon.GroupType = function () {
  /// <field name="Group" type="Number" integer="true" static="true">
  /// </field>
  /// <field name="DualGroup" type="Number" integer="true" static="true">
  /// </field>
  /// <field name="MultiLineGroup" type="Number" integer="true" static="true">
  /// </field>
};

Ribbon.GroupType.prototype = {
  Group: 0,
  DualGroup: 1,
  MultiLineGroup: 2
}

Ribbon.GroupType.registerEnum('Ribbon.GroupType', false);

Ribbon.RibbonZone.Render = function Ribbon$RibbonZone$Render(options) {

  var groupArray = options.Groups;
  var renderId = options.RenderElementId || '';
  var strBuilder = new Sys.StringBuilder('');

  for (var i = 0; i < groupArray.length; i++) {
    var groupButtons = groupArray[i].buttons;
    var outerHTML = '';
    var tempOuterHTML = '';

    for (var j = 0; j < groupButtons.length; j++) {

      var get_Id = options.IdPrefix + groupButtons[j].Id;
      var get_text = groupButtons[j].text;
      var get_image = options.ImageLoc + groupButtons[j].image;
      var get_type = groupButtons[j].type;
      var get_buttons = groupButtons[j].buttons;
      var get_align = groupButtons[j].align;
      var get_action = decodeURI(groupButtons[j].action) || '';
      var get_submit = groupButtons[j].submit;

      var getItem_Id = null;
      var getItem_text = null;
      var getItem_image = null;
      var getItem_action = null;
      var getItem_submit = null;

      switch (groupArray[i].type) {
        case Ribbon.GroupType.Group:

          switch (get_type) {
            case Ribbon.ButtonType.IconItem:
              break;
            case Ribbon.ButtonType.SmallItem:
              if (get_submit) {
                outerHTML += getSubmit(get_Id);
                get_action += setSubmit(get_Id);
              }
              outerHTML += String.format(Ribbon.Res.SmallButtonHTML, get_Id, get_text, get_image, get_align, get_action);
              break;
            case Ribbon.ButtonType.BigItem:
              if (get_submit) {
                outerHTML += getSubmit(get_Id);
                get_action += setSubmit(get_Id);
              }
              outerHTML += String.format(Ribbon.Res.BigButtonHTML, get_Id, get_text, get_image, get_action);
              break;
            case Ribbon.ButtonType.DropDownIconItem:
              break;
            case Ribbon.ButtonType.DropDownSmallItem:
              if (get_buttons !== undefined) {
                for (var k = 0; k < get_buttons.length; k++) {

                  getItem_Id = options.IdPrefix + get_buttons[k].Id;
                  getItem_text = get_buttons[k].text;
                  getItem_image = options.ImageLoc + get_buttons[k].image;
                  getItem_action = get_buttons[k].action;
                  getItem_submit = get_buttons[k].submit;

                  if (getItem_submit) {
                    tempOuterHTML += getSubmit(getItem_Id);
                    getItem_action += setSubmit(getItem_Id);
                  }

                  tempOuterHTML += String.format(Ribbon.Res.SmallButtonHTML, getItem_Id, getItem_text, getItem_image, '', getItem_action);
                }
              }
              outerHTML += String.format(Ribbon.Res.DropDownSmallButtonHTML, get_Id, get_text, get_image, get_align, tempOuterHTML);
              break;
            case Ribbon.ButtonType.DropDownBigItem:
              if (get_buttons !== undefined) {
                for (var k = 0; k < get_buttons.length; k++) {

                  getItem_Id = options.IdPrefix + get_buttons[k].Id;
                  getItem_text = get_buttons[k].text;
                  getItem_image = options.ImageLoc + get_buttons[k].image;
                  getItem_action = get_buttons[k].action;
                  getItem_submit = get_buttons[k].submit;

                  if (getItem_submit) {
                    tempOuterHTML += getSubmit(getItem_Id);
                    getItem_action += setSubmit(getItem_Id);
                  }

                  tempOuterHTML += String.format(Ribbon.Res.SmallButtonHTML, getItem_Id, getItem_text, getItem_image, '', getItem_action);
                }
              }
              outerHTML += String.format(Ribbon.Res.DropDownBigButtonHTML, get_Id, get_text, get_image, tempOuterHTML);
              break;
            default:
          }
          break;
        default:
      }  // switch
    } // for groupButtons
    var groupHtml = GetGroupHTML(Ribbon.GroupType.Group);
    if (groupArray[i].seperator == null || groupArray[i].seperator) {
      groupHtml += getSeperator();
    }

    strBuilder.append(String.format(groupHtml, outerHTML, groupArray[i].text, groupArray[i].width));
  } // for groupArray
  if (renderId == '') {
    document.write(strBuilder.toString(''));
  }
  else {
    document.getElementById(renderId).innerHTML = strBuilder.toString('');
  }
};                  // end function (Render)

function GetGroupHTML(typeName) {
  /// <param name="typeName" type="Ribbon.GroupType">
  /// </param>
  switch (typeName) {
    case Ribbon.GroupType.Group:
      return Ribbon.Res.GroupHTML;
    case Ribbon.GroupType.DualGroup:
      return Ribbon.Res.DualGroupHTML;
    case Ribbon.GroupType.MultiLineGroup:
      return Ribbon.Res.MultiLineGroupHTML;
    default:
      return Ribbon.Res.GroupHTML;
  }
}

function getSeperator() {
  return '<li style="padding-left: 2px; padding-right: 2px;"> <div class="RibbonSeparatorV"></div></li>';
}
function getSubmit(Id) {
  return String.format('<input type="submit" id="ribbon_Submit_{0}" style="display:none" />', Id);
}
function setSubmit(Id) {
  return String.format('$get(\'ribbon_Submit_{0}\').click();', Id);
}

Ribbon.RibbonZone.registerClass('Ribbon.RibbonZone');

function hoverButton(Id) {
  document.getElementById('ribbonButtonleft' + Id).style.backgroundImage = "url('/content/ribbonimages/buttonLeft.png')";
  document.getElementById('ribbonButtonMiddle' + Id).style.backgroundImage = "url('/content/ribbonimages/buttonMiddle.png')";
  document.getElementById('ribbonButtonRight' + Id).style.backgroundImage = "url('/content/ribbonimages/buttonRight.png')";

}

function downButton(Id) {
  document.getElementById('ribbonButtonleft' + Id).style.backgroundImage = "url('/content/ribbonimages/buttonLeftDown.png')";
  document.getElementById('ribbonButtonMiddle' + Id).style.backgroundImage = "url('/content/ribbonimages/buttonMiddleDown.png')";
  document.getElementById('ribbonButtonRight' + Id).style.backgroundImage = "url('/content/ribbonimages/buttonRightDown.png')";
  document.getElementById('ribbonButtonleft' + Id).style.backgroundColor = "#FFF";
  document.getElementById('ribbonButtonRight' + Id).style.backgroundColor = "#FFF";
}

function leaveButton(Id) {
  document.getElementById('ribbonButtonleft' + Id).style.backgroundImage = "";
  document.getElementById('ribbonButtonMiddle' + Id).style.backgroundImage = "";
  document.getElementById('ribbonButtonRight' + Id).style.backgroundImage = "";
  document.getElementById('ribbonButtonleft' + Id).style.backgroundColor = "transparent";
  document.getElementById('ribbonButtonRight' + Id).style.backgroundColor = "transparent";
}


function smallHoverButton(Id) {
  document.getElementById('ribbonButtonleft' + Id).style.backgroundImage = "url('/content/ribbonimages/smallbuttonLeft.png')";
  document.getElementById('ribbonButtonMiddle' + Id).style.backgroundImage = "url('/content/ribbonimages/smallbuttonMiddle.png')";
  document.getElementById('ribbonButtonRight' + Id).style.backgroundImage = "url('/content/ribbonimages/smallbuttonRight.png')";
}

function smallDownButton(Id) {
  document.getElementById('ribbonButtonleft' + Id).style.backgroundImage = "url('/content/ribbonimages/smallbuttonLeftDown.png')";
  document.getElementById('ribbonButtonMiddle' + Id).style.backgroundImage = "url('/content/ribbonimages/smallbuttonMiddleDown.png')";
  document.getElementById('ribbonButtonRight' + Id).style.backgroundImage = "url('/content/ribbonimages/smallbuttonRightDown.png')";
}

function smallLeaveButton(Id) {
  document.getElementById('ribbonButtonleft' + Id).style.backgroundImage = "";
  document.getElementById('ribbonButtonMiddle' + Id).style.backgroundImage = "";
  document.getElementById('ribbonButtonRight' + Id).style.backgroundImage = "";
}

// The Ribbon Namespace
var SBRibbon = {

  // Get DOM object properties
  _gO: function (obj, coord) {
    obj = document.getElementById(obj);
    var val = obj["offset" + coord];
    while ((obj = obj.offsetParent) != null) {
      val += obj["offset" + coord];
    }
    return val;
  },

  // Show a Gallery
  OpenGallery: function (src, trg) {
    var src1 = document.getElementById(src);
    document.getElementById(trg).style.display = (document.getElementById(trg).style.display == "none") ? "block" : "none";
    document.getElementById(trg).style.left = SBRibbon._gO(src, "Left") + "px";
    document.getElementById(trg).style.top = (SBRibbon._gO(src, "Top") + src1.offsetHeight - 1) + "px";
  },

  // Show a menu
  ShowMenu: function (src, trg) {
    document.getElementById(trg).style.display = (document.getElementById(trg).style.display == "none") ? "block" : "none";
    document.getElementById(trg).style.left = SBRibbon._gO(src.id, "Left") + "px";
    document.getElementById(trg).style.top = (SBRibbon._gO(src.id, "Top") + src.offsetHeight) + "px";
  },

  // Keep the menu opened
  KeepMenu: function (trg) {
    document.getElementById(trg).style.display = "";
    var menuName = document.getElementById(trg).getAttribute("MenuOpener");
    var typeName = document.getElementById(trg).getAttribute("type");
    if (typeName === 'small') {
      smallHoverButton(menuName);
    }
    else {
      hoverButton(menuName);
    }
  },

  // Hide a menu
  HideMenu: function (trg) {
    document.getElementById(trg).style.display = "none";
    var menuName = document.getElementById(trg).getAttribute("MenuOpener");
    var typeName = document.getElementById(trg).getAttribute("type");
    if (typeName === 'small') {
      smallLeaveButton(menuName);
    }
    else {
      leaveButton(menuName);
    }
  },

  // Register the "File Menu"
  RegisterFileMenu: function (text, container, menu, color) {
    try {
      if (color == null) {
        color = "green";
      }
      var divTag = document.createElement("div");
      divTag.id = "SBRibbon_FileMenu";
      divTag.className = "Ribbon_FileMenu_" + color; // Change this classname to customize the File Menu, See CSS file
      divTag.innerHTML = text;
      divTag.setAttribute("onclick", "SBRibbon.ShowMenu(this, '" + menu + "_FileMenu')");
      document.body.appendChild(divTag);

      var temp_bar = document.getElementById(container + "_header");
      temp_bar.insertBefore(divTag, temp_bar.firstChild);

      document.getElementById("SBRibbon_FileMenu").appendChild(document.getElementById(menu + "_FileMenu"));

    } catch (e) { alert(e); }
  },

  About: function () {
    alert("SBRibbon, http://aspnetribbon.codeplex.com");
  }

};

Ribbon.Res = {
  'GroupHTML': '<li unselectable="on" class="RibbonGroup"> <table cellpadding="0" cellspacing="0"> \
      <tr><td valign="top"> <div style="height: 80px; overflow: hidden; width:{2}">{0}</div></td></tr> \
      <tr><td class="RibbonGroupText" valign="bottom"><span unselectable="on" >{1}</span> </td></tr> \
      </table></li>',

  'DualGroupHTML': '<li unselectable="on" class="RibbonGroup"><table cellpadding="0" cellspacing="0"> \
      <tr> \
        <td valign="top"><div style="height: 80px; overflow: hidden;">{0}</div></td> \
        <td valign="top"><div style="height: 80px; overflow: hidden; float: left">{1}</div></td> \
      </tr> \
      <tr><td colspan="2" class="RibbonGroupText" valign="bottom"><span unselectable="on" >{2}</span></td></tr> \
    </table></li>',

  'MultiLineGroupHTML': '<li unselectable="on" class="RibbonGroup"><table cellpadding="0" cellspacing="0"> \
      <tr> \
        <td valign="top"> \
          <div style="height: 80px; overflow: hidden; clear: both">{0}</div> \
          <div style="height: 80px; overflow: hidden; clear: both">{1}</div> \
          <div style="height: 80px; overflow: hidden; clear: both">{2}</div> \
        </td> \
      </tr> \
      <tr><td class="RibbonGroupText" valign="bottom"><span unselectable="on" >{3}</span></td></tr> \
    </table></li>',

  'BigButtonHTML': '<div unselectable="on" class="RibbonBigItem_Out" onmouseover="hoverButton(this.id);" onmouseout="leaveButton(this.id);" onmousedown="downButton(this.id);" onmouseup="hoverButton(this.id);" id="{0}"> \
        <div id="ribbonButtonleft{0}" class="RibbonBigItemLeft"></div> \
        <div id="ribbonButtonRight{0}" class="RibbonBigItemRight"></div> \
        <div id="ribbonButtonMiddle{0}" class="RibbonBigItemMiddle" onclick="{3}"> \
          <div class="RibbonBigItemInnerMiddle" > \
            <table cellpadding="0" cellspacing="0" align="center"> \
              <tr><td style="text-align: center"> <a> <img src="{2}" alt="{1}" /> </a> </td></tr> \
              <tr><td style="text-align: center;"> <a unselectable="on" > {1} </a> </td></tr> \
            </table> \
          </div> \
        </div> \
      </div>',

  'SmallButtonHTML': '<div unselectable="on" class="RibbonSmallItem_Out" onmouseover="smallHoverButton(this.id);" onmouseout="smallLeaveButton(this.id);" id="{0}" onmousedown="smallDownButton(this.id);" onmouseup="smallHoverButton(this.id);"  style="float:{3}"> \
        <div id="ribbonButtonleft{0}" class="RibbonSmallItemLeft"></div> \
        <div id="ribbonButtonRight{0}" class="RibbonSmallItemRight"></div> \
        <div id="ribbonButtonMiddle{0}" class="RibbonSmallItemMiddle" onclick="{4}"> \
          <div class="RibbonSmallItemInnerMiddle" > \
            <table cellpadding="0" cellspacing="0" align="center"> \
              <tr> \
                <td style="text-align: center; width: 18px"> <a> <img alt="{1}" src="{2}" height="16" width="16" /></a></td> \
                <td style="padding-top: 1px; padding-left:3px" valign="top"><a unselectable="on" >{1}</a></td> \
              </tr> \
            </table> \
          </div> \
        </div> \
      </div> ',

  'DropDownBigButtonHTML': '<div unselectable="on" class="RibbonBigItem_Out" onmouseover="hoverButton(this.id);" onmouseout="leaveButton(this.id);" onmousedown="downButton(this.id);" onmouseup="hoverButton(this.id);" onclick="checkMenu();SBRibbon.ShowMenu(this, \'{0}Menu\');" id="{0}"> \
        <div id="ribbonButtonleft{0}" class="RibbonBigItemLeft"></div> \
        <div id="ribbonButtonRight{0}" class="RibbonBigItemRight"></div> \
        <div id="ribbonButtonMiddle{0}" class="RibbonBigItemMiddle"> \
          <div class="RibbonBigItemInnerMiddle" > \
            <table cellpadding="0" cellspacing="0" align="center"> \
              <tr><td style="text-align: center"><a><img src="{2}" alt="{1}" /></a></td></tr> \
              <tr><td style="text-align: center;"><span unselectable="on" >{1}</span></td></tr> \
              <tr><td style="text-align: center; background: url(/Content/RibbonImages/arrow_dr2.gif) no-repeat bottom center"> \
                  <img src="/Content/RibbonImages/blank.gif" style="height: 13px;" /> \
                </td></tr> \
            </table> \
          </div> \
        </div> \
      </div><div onmouseover="SBRibbon.KeepMenu(\'{0}Menu\');" onmouseout="SBRibbon.HideMenu(\'{0}Menu\');" id="{0}Menu" style="display: none;" class="RibbonMenu menus" menuopener="{0}" type="large">{3}</div>',

  'DropDownSmallButtonHTML': '<div unselectable="on" class="RibbonSmallItem_Out" onmouseover="smallHoverButton(this.id);" onmouseout="smallLeaveButton(this.id);" id="{0}" onmousedown="smallDownButton(this.id);" onmouseup="smallHoverButton(this.id);" onclick="checkMenu();SBRibbon.ShowMenu(this, \'{0}Menu\');" style="float:{3}"> \
        <div id="ribbonButtonleft{0}" class="RibbonSmallItemLeft"></div> \
        <div id="ribbonButtonRight{0}" class="RibbonSmallItemRight"></div> \
        <div id="ribbonButtonMiddle{0}" class="RibbonSmallItemMiddle"> \
          <div class="RibbonSmallItemInnerMiddle" > \
            <table cellpadding="0" cellspacing="0" align="center"> \
              <tr> \
                <td style="text-align: center; width: 18px"><img alt="{1}" src="{2}" height="16" width="16" /></td> \
                <td style="font-size: 8pt; padding-top: 1px; padding-bottom: 0px;" valign="top"><span unselectable="on" >{1}</span></td> \
                <td style="padding-top: 0px; padding-bottom: 0px;"><img src="/Content/RibbonImages/arrow_dr2.gif" /></td> \
              </tr> \
            </table> \
          </div> \
        </div></div> \
        <div id="{0}Menu" style="display: none" class="RibbonMenu menus" onmouseover="SBRibbon.KeepMenu(\'{0}Menu\');" onmouseout="SBRibbon.HideMenu(\'{0}Menu\');" menuopener="{0}" type="small">{4}</div>'
};