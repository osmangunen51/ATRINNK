﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<IEnumerable<CategoryModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <link href="/Content/screen.css" rel="stylesheet" type="text/css" />
  <script src="/Scripts/jquery.cookie.js" type="text/javascript"></script>
  <script src="/Scripts/jquery.scrollTo-min.js" type="text/javascript"></script>
  <script src="/Scripts/jquery.treeTable.js" type="text/javascript"></script>
  <link href="/Content/jquery.treeTable.css" rel="stylesheet" type="text/css" />
  <link href="/Content/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
  <link href="/Content/jquery.contextmenu.css" rel="stylesheet" type="text/css" />
  <script src="/Scripts/jquery.contextmenu.js" type="text/javascript"></script>
  <style type="text/css">
    .cutRow
    {
      background: #F8F8F8;
      color: #555;
    }
  </style>
  <script type="text/javascript">

    function validPaste() {
      var value = parseInt($('#sourceId').val());
      return value > 0;
    }
    var obj = null;
    var $element = null;
    var menu1 = [
      {
        'Taşı': function (menuItem, menu) {
          $('#sourceId').val($(this).attr('rowid'));
          $(this).addClass('cutRow');
          $(this).removeClass("selected");
          $(this).removeClass("Row");
          obj = this; 
          $.contextMenu.hide();
        }
      },
      {
        'Yapıştır': {
          onclick: function (menuItem, menu) {
        $('#Loading').dialog('open');
          var attr = this.getAttribute('group'); 
          if (attr == null) {
              attr = 0;
          }
          $element = this;
          updateMove($('#sourceId').val(), $(this).attr('rowid'), attr); 
          $(obj).removeClass('cutRow');
          $(obj).addClass("selected");
          $(obj).addClass("Row"); 
          },
          id: 'cntPaste'
        }
      },
      $.contextMenu.separator,
      {
        'Düzenle': {
          onclick: function (menuItem, menu) { 
            $.contextMenu.hide();
          }
        }
      },
      $.contextMenu.separator,
      {
        'Sil': {
          onclick: function (menuItem, menu) { 
            $.contextMenu.hide();
          }
        }
      }
    ];

    function menuRegister() {
    $('.Row').contextMenu(menu1,{appendTo: ''});
      $('.context').contextMenu(menu1,
      { 
        theme: 'vista',
        shadow: false,
        beforeShow: function () {
          
          $(this.menu).find('.context-menu-item').each(function () {
            var $element = $(this)[0];
            if ($element.id == 'cntPaste') {
              if (!validPaste()) {
                $(this).addClass("context-menu-item-disabled");
              } else {
                $(this).removeClass("context-menu-item-disabled");
              }
            }
          });
        }
      });
    }

    $(function () {
      menuRegister();
    });

    function LoadData(Id, d, group) {
      if ($(d).attr('load') !== 'true') {
        $('#Loading').dialog('open');
        $.ajax({
          url: '/Category/TreeLoad',
          data: { id: Id, groupId: group},
          type: 'post',
          success: function (data) {
            if ($(d).attr('load') !== 'true') {
//              $('.initialized').removeClass("initialized");
              
//              register();
//              $(d).expand();
//              $('#group-1').expand();
              var $e = $(data);
              $(d).after($e);
               $e.each(function(i){
                 $($e[i]).appendBranchTo(d[0]);
               });
              $('.Content').scrollTo(d, 0, { over: { top: -10} });
              $get(d[0].id).setAttribute('load', 'true');
              $('#Loading').dialog('close');
            }
          },
          error: function (x) {
            alert(x.responseText);
          }
        });
      }
    }

    function updateMove(sId, pId, groupId) {
      $.ajax({
        url: '/Category/UpdateMove',
        type: 'post',
        data: { sourceId: parseInt(sId), parentId: parseInt(pId), group: parseInt(groupId) },
        success: function (result) { 
            $(obj).appendBranchTo($element);
            $('#sourceId').val(0);  
            $('#Loading').dialog('close');
        },
        error: function(x){  
        }
      });
    }

    function register() {

      $("#dnd-example").treeTable({
        expandPost: function (a) {
          LoadData($(a).attr('rowid'), a, $(a).attr('group'));
        }
      });

      menuRegister();

      //			$("#dnd-example .file, #dnd-example .f").draggable({
      //				helper: "clone",
      //				opacity: .75,
      //				refreshPositions: false,
      //				revert: "invalid",
      //				revertDuration: 300,
      //				scroll: true
      //			});

      // Configure droppable rows
      $("#dnd-example .folder").each(function () {
        $(this).parents("tr").droppable({
          accept: ".file, .folder, .f",
          deactivate: function (e, ui) {
            $('#' + this.id + ' td:first span.dd').remove();
          },
          over: function (e, ui) {
            $('#' + this.id + ' td:first span.dd').remove();
          },
          out: function (e, ui) {
            $('#' + this.id + ' td:first span.dd').remove();
          },
          drop: function (e, ui) {
            $($(ui.draggable)).appendBranchTo(this);
            if (this.id != $(ui.draggable.parents("tr")[0]).id && !$(this).is(".expanded")) {
              $(this).expand();
            }
            updateMove($(ui.draggable).attr('rowid'), $(this).attr('rowid'));
          },
          hoverClass: "accept",
          over: function (e, ui) {
            $('#' + this.id + ' td:first').append($('#ds').clone());
            $('#' + this.id + ' td:first span.dd').show();
          }
        });
      });


      $("table#dnd-example tbody tr").mousedown(function () {
        $("tr.selected").removeClass("selected");
        $(this).addClass("selected");
      });

      $("table#dnd-example tbody tr span").mousedown(function () {
        $($(this).parents("tr")[0]).trigger("mousedown");
      });

    }
    $(document).ready(function () {

      $('#Loading').dialog
      ({
        autoOpen: false,
        width: 400,
        height: 150,
        modal: true
      });

      register();

      $('.categoryAdd').dialog({ autoOpen: false, resizable: false, modal: true, width: 430 });

      $('.categoryEdit').dialog({ autoOpen: false, resizable: false, modal: true, width: 430 });

      $('#group-1').expand();

    });


    function DialogClose() {
      $('#CategoryGroupType').val(0);
      $('#CategoryGroupType').hide();
      $('.categoryAdd').dialog('close');
    }

    function EditDialogClose() {
      $('.categoryEdit').dialog('close');
    }

    Boolean.parse = function (str) {
      switch (str.toString().toLowerCase()) {
        case "true":
          return true;
        case "false":
          return false;
        default:
          throw new Error("Boolean.parse: Cannot convert string to boolean.");
      }
    };

    function DialogOpen(Id) {


      $('#CategoryName').val('');
      $('#CategoryOrder').val('');
      $('#ActiveRow').val(Id);
      var parentId = 0;
      if ($(Id).attr('dataid') != null) {
        parentId = $(Id).attr('dataid');
      } else {
        parentId = $(Id).attr('rowid');
      }

      var category = Boolean.parse($(Id).attr('category') || false);
      var brand = Boolean.parse($(Id).attr('brand') || false);
      var serie = Boolean.parse($(Id).attr('serie') || false);
      var model = Boolean.parse($(Id).attr('model') || false);

      var typeId = parseInt($(Id).attr('typeid'));


      $('.CategoryType').attr('checked', false);
      $('#categoryTypeContainer').show();
      $('#CategoryGroupType').val(0);

      //      CategoryType_cat

      //      CategoryType_bra

      //      CategoryType_ser

      //      CategoryType_mod

      switch (typeId) {
        case 0: // Sektor
          $('#CategoryGroupType').show();
          $('#categoryTypeContainer').hide();
            $('#CategoryType_cat').attr('checked', true);
          break;
        case 1: // Ana Kategori
          $('#categoryType_1').show();
          $('#categoryType_2').hide();
          if (category) {
            $('#categoryType_1').hide();
            $('#CategoryType_cat').attr('checked', true);
          }
          if (brand) {
            $('#categoryType_1').hide();
            $('#CategoryType_bra').attr('checked', true);
          }
          break;
        case 2: // Kategori
          $('#categoryType_1').show();
          $('#categoryType_2').hide();
          if (category) {
            $('#categoryType_1').hide();
            $('#CategoryType_cat').attr('checked', true);
            break;
          }
          if (brand) {
            $('#categoryType_1').hide();
            $('#CategoryType_bra').attr('checked', true);
            break;
          }
          
            $('#CategoryType_cat').attr('checked', true);
          break;
        case 3: // Marka
          $('#categoryType_1').hide();
          $('#categoryType_2').show();
          if (serie) {
            $('#categoryType_2').hide();
            $('#CategoryType_ser').attr('checked', true);
            break;
          }
          if (model) {
            $('#categoryType_2').hide();
            $('#CategoryType_mod').attr('checked', true);
            break;
          }
          
            $('#CategoryType_cat').attr('checked', true);
          break;
        case 4: // Seri
          $('#categoryTypeContainer').hide();
          $('#CategoryType_mod').attr('checked', true);
          break;
        case 5: // Model
          alert('Model altına ekleme yapılamaz');
          return;
          break;
        default:
          $('#CategoryGroupType').hide();
          break;
      }
      $('#CategoryParentId').val(parentId);
      $('.categoryAdd').dialog('open');
    }

    function EditCategory(node, Id) {
 
      $.ajax({
        url: '/Category/EditCategory',
        type: 'post',
        data: { id: Id },
        dataType: 'json',
        success: function (entity) {
          $('#EditCategoryName').val(entity.CategoryName);
          $('#EditCategoryOrder').val(entity.CategoryOrder);
            $('#EditCategoryActive').attr('checked', entity.Active);
            $("#EditCategoryContentTitle").val(entity.CategoryContentTitle);
          $('#EditCategoryId').val(entity.CategoryId);
          $('#EditRow').val(node);
          $('.categoryEdit').dialog('open');
        },
        error: function (x) {
          alert(x.responseText);
        }
      });
    }

    function EditCategoryPost() {
      var Id = $('#EditRow').val();
      $.ajax({
        url: '/Category/EditPost',
        type: 'post',
        dataType: 'json',
        data: {
          CategoryId: $('#EditCategoryId').val(),
          CategoryName: $('#EditCategoryName').val(),
          CategoryOrder: $('#EditCategoryOrder').val(),
          Active: $('#EditCategoryActive').attr('checked')
        },
        success: function (entity) {
          $('#rowCategoryName' + entity.CategoryId).html(entity.CategoryName);
          $('#rowCategoryOrder' + entity.CategoryId).html(entity.CategoryOrder);
          $('.categoryEdit').dialog('close');
        }
      });
    }

    function InsertRow() {
      var result = $('#CategoryName').val();
      if ($.trim(result) !== '') {
        $('#CategoryName').removeClass('input-validation-error');
        var Id = $('#ActiveRow').val();

        if ($('#CategoryGroupType').val() !== 0) {
          switch (parseInt($('#CategoryGroupType').val())) {
            //						case 1:  
            //							Id = '#Makina-' + $('#CategoryParentId').val();  
            //							break;  
              <% foreach (var item in CategoryProductGroupModel.ProductGroups())
                 { %>
              case <%: item.CategoryProductGroupId %>: 
                Id = '#<%: item.GroupName %>-' + $('#CategoryParentId').val(); 
              break;
              <% } %>
            default:
              break;
          }
        }

        var categoryTypeId_value = parseInt(GroupControl());
        

//      var groupTypeId = GroupControl();

//        if (groupTypeId == 0) {
//          alert('Kategori tipi seçilmedi.');
//          return;
//        }


//                if (categoryTypeId_value == 0) {
//          alert('Kategori tipi seçilmedi.');
//      
//          return;
//        }


        $.ajax({
          url: '/Category/InsertRow',
          data: {
            CategoryName: $('#CategoryName').val(),
            CategoryParentId: $('#CategoryParentId').val(),
            CategoryType: $('#CategoryType').val(),
            CategoryGroupType: $('#CategoryGroupType').val(),
            CategoryOrder: $('#CategoryOrder').val(),
            Active: $('#Active').attr('checked')
          },
          type: 'post',
          success: function (result) {
            register();
            $('.initialized').removeClass("initialized");
            if ($(Id).is('.expanded')) {
              $(Id).after(result);
            }
            register();
            if (!$(Id).is('.expanded')) {
              $(Id).expand();
              if ($(Id).attr('load') == 'true') {
                $(Id).after(result);
              }
            }
            switch (categoryTypeId_value) {
              case 1: $(Id).attr('category', true); break;
              case 2: $(Id).attr('category', true); break;
              case 3: $(Id).attr('brand', true); break;
              case 4: $(Id).attr('serie', true); break;
              case 5: $(Id).attr('model', true); break;
              default: break;
            }
            $('#group-1').expand();
            $('.Content').scrollTo(Id, 0, { over: { top: -10} });
            DialogClose();
          },
          error: function (x) {
            DialogClose();
          }
        });
      }
      else {
        $('#CategoryName').addClass('input-validation-error');
        $('#CategoryName').focus();
      }
    }

    function GetCategoryType() {
      var value = 0;
      if ($('#CategoryGroupType').val() > 0) {
        value = 1;
      } 
      return value;
    }

    function GroupControl()
    {  var value = 0;
       $('#categoryTypeContainer input').each(function () {
        if ($(this).attr('checked')) {
          value = $(this).val();
        }
      });
      return value;
    }

    function Delete(Id) {
      var re = confirm('Kaydı silmek istediğinizden eminmisiniz?');
      if (re) {
        $('#Loading').dialog('open');
        $.ajax({
          url: '/Category/Delete',
          type: 'post',
          data: { id: Id },
          success: function (result) {
            if (result) {
              $('#node-' + Id).remove();
            } else {
              alert('Kategori silinemedi.');
            }
            $('#Loading').dialog('close');
          }
        });
      }
    }

    window.onerror = function () {
      return false;
    } 

  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div id="Loading">
    <div style="width: 160px; height: 20px; margin: 45px 0px 0px 130px;">
      Kategoriler Yükleniyor..
    </div>
  </div>

  <span id="ds" class="file dd" style="display: none; background-image: url('/content/images/rightAllow.png');"></span>
  <div style="width: 100%; margin: auto;">
    <div style="float: left; width: 100%;">
      <input type="hidden" id="sourceId" value="0" />
      <table class="TableList tree" id="dnd-example" width="100%">
        <thead bgcolor="#f5f5f5">
          <tr>
            <td class="Header HeaderBegin" style="width: 400px">
              Kategori Adı
            </td>
            <td class="Header" style="width: 100px">
            </td>
            <td class="Header" style="width: 100px">
              Tip
            </td>
            <td class="Header" style="width: 100px">
              Sıra
            </td>
            <td class="Header" style="width: 100px">
            </td>
            <td class="Header" style="width: 60px">
            </td>
            <td class="Header">
            </td>
          </tr>
        </thead>
        <tbody id="treeContent">
          <tr rowid="1" id="group-1" class="f parent collapsed Row" parentid="-1" load="false">
            <td class="CellBegin">
              <span class="folder">SEKTÖRLER</span>
            </td>
            <td class="Cell" align="center">
              <a style="cursor: pointer;" onclick="DialogOpen('#group-1');">Alt Kategori Ekle</a>
            </td>
            <td class="Cell" align="center">
              Sektor
            </td>
            <td class="Cell">
            </td>
            <td class="Cell">
            </td>
            <td class="Cell">
            </td>
            <td class="Cell">
            </td>
          </tr>
        </tbody>
        <tbody id="treeContent2">
        </tbody>
        <tfoot>
          <tr>
            <td class="ui-state ui-state-hover" colspan="7" align="right" style="border-color: #DDD; border-top: none; padding-right: 10px; height: 20px">
              Toplam Kayıt :
              <%= Model.Count().ToString() %>
            </td>
          </tr>
        </tfoot>
      </table>
    </div>
  </div>
  <div title="Kategori Duzenle" class="categoryEdit">
    <table border="0" cellpadding="5" cellspacing="0" style="width: 100%">
      <tr>
        <td>
          Kategori Adı
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBox("EditCategoryName") %>
          <%: Html.Hidden("EditCategoryId") %>
          <input type="hidden" id="EditRow" />
        </td>
      </tr>

      <tr>
        <td>
          Sıra
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBox("EditCategoryOrder") %>
        </td>
      </tr>
      <tr>
        <td>
          Aktif
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.CheckBox("EditCategoryActive")%>
        </td>
      </tr>
    
      <tr>
        <td align="right" colspan="3">
          <button type="button" onclick="EditCategoryPost();">
            Kaydet
          </button>
          <button type="button" onclick="EditDialogClose();">
            Kapat
          </button>
        </td>
      </tr>
    </table>
  </div>
  <div title="Kategori Ekle" class="categoryAdd">
    <table border="0" cellpadding="5" cellspacing="0" style="width: 100%">
      <tr>
        <td>
          Kategori Adi
          <input type="hidden" id="CategoryParentId" />
          <input type="hidden" id="ActiveRow" />
          <input type="hidden" id="CategoryType" />
        </td>
        <td>
          :
        </td>
        <td>
          <input type="text" id="CategoryName" style="width: 300px" />
        </td>
      </tr>
      <tr>
        <td>
          Sıra
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBox("CategoryOrder") %>
        </td>
      </tr>
      <tr>
        <td>
          Aktif
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.CheckBox("Active", true) %>
        </td>
      </tr>
      <tr>
        <td>
        </td>
        <td>
        </td>
        <td>
          <div id="categoryTypeContainer">
            <div id="categoryType_1">
              <input type="radio" onclick="$('#CategoryType').val($(this).val());" class="CategoryType" name="CategoryType" id="CategoryType_cat" value="2" />Kategori
              <input type="radio" onclick="$('#CategoryType').val($(this).val());" class="CategoryType" name="CategoryType" id="CategoryType_bra" value="3" />Marka
            </div>
            <div id="categoryType_2">
              <input type="radio" onclick="$('#CategoryType').val($(this).val());" class="CategoryType" name="CategoryType" id="CategoryType_ser" value="4" />Seri
              <input type="radio" onclick="$('#CategoryType').val($(this).val());" class="CategoryType" name="CategoryType" id="CategoryType_mod" value="5" />Model
            </div>
          </div>
          <select id="CategoryGroupType" style="display: none">
            <option value="0" selected="selected">-- Seçiniz --</option>
            <% foreach (var item in CategoryProductGroupModel.ProductGroups())
               { %>
            <option value="<%: item.CategoryProductGroupId %>">
              <%: item.CategoryProductGroupName %></option>
            <% } %>
          </select>
        </td>
      </tr>
      <tr>
        <td align="right" colspan="3">
          <button type="button" onclick="InsertRow();">
            Kaydet
          </button>
          <button type="button" onclick="DialogClose();">
            Kapat
          </button>
        </td>
      </tr>
    </table>
  </div>
</asp:Content>
