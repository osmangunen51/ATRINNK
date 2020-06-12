﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<TreeViewNode>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  AllIndex
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <link href="/Content/jquery.treeview.css" rel="stylesheet" type="text/css" />
  <link href="/Content/jquery.contextmenu.css" rel="stylesheet" type="text/css" />
  <script src="/Scripts/JQuery.cookie.js" type="text/javascript"></script>
  <script src="/Scripts/jquery.treeview.js" type="text/javascript"></script>
  <script src="/Scripts/jquery.treeview.async.js" type="text/javascript"></script>
  <script src="/Scripts/jquery.treeview.edit.js" type="text/javascript"></script>
  <script src="/Scripts/jquery.contextmenu.js" type="text/javascript"></script>
  <script src="/Scripts/jquery.validate.js" type="text/javascript"></script>
  <script src="/Scripts/jquery.metadata.js" type="text/javascript"></script>
     <link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
  <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
  <script type="text/javascript">
	 $(function () {
		$.superbox.settings = {
		  closeTxt: "Kapat",
		  loadTxt: "Yükleniyor...",
		  nextTxt: "Sonraki",
		  prevTxt: "Önceki"
		};
		$.superbox();
	 });
	 </script>
		<style type="text/css">
		/* Custom Theme */
		#superbox-overlay{background:#e0e4cc;}
		#superbox-container .loading{width:32px;height:32px;margin:0 auto;text-indent:-9999px;background:url(styles/loader.gif) no-repeat 0 0;}
		#superbox .close a{float:right;padding:0 5px;line-height:20px;background:#333;cursor:pointer;}
		#superbox .close a span{color:#fff;}
		#superbox .nextprev a{float:left;margin-right:5px;padding:0 5px;line-height:20px;background:#333;cursor:pointer;color:#fff;}
		#superbox .nextprev .disabled{background:#ccc;cursor:default;}
	</style>
  <style type="text/css">
    .cutRow
    {
      color: red;
    }
  </style>
  <%byte id = ViewData["id"].ToByte(); %>
    <script type="text/javascript">
      $(document).ready(function () {
          $('td').attr('valign', 'top');

          $('#Edit_SeoTitle').focus(function () {
              $('#activeText').val('#Edit_SeoTitle')
          });

          $('#Edit_SeoDesc').focus(function () {
              $('#activeText').val('#Edit_SeoDesc')
          });

          $('#Edit_SeoKeyword').focus(function () {
              $('#activeText').val('#Edit_SeoKeyword')
          });

     

          $('button').click(function () {
              if ($('#activeText').val() == '') {
                  alert('Parametre göndermek için göndermek istediğiniz alanı seçmelisiniz.')
              }
              else {
                  $($('#activeText').val()).val($($('#activeText').val()).val() + ' ' + $(this).val());
              }
          });

      });
 
   
  </script>
      <script type="text/javascript">

    function initTrees() {
      $("#mixed").treeview({
        url: "/Category/GetNodes",
        collapsed: true,
        persist: 'cookie',
        cookieId: 'cookieTree',
        cookieOptions: {
          expires: 7,
          path: '/'
        },
        ajax: {
          complete: function () {
            menuRegister();
            InitEvents();
          }
        }
      });
  
      $('#SectorCreateForm').validate({
        errorClass: 'field-validation-error',
        errorElement: 'span',
        messages: {
          Create_SectorName: ' Boş geçilemez',
          Create_SectorOrder: ' Boş geçilemez' //Create_SectorGroup: '  Lütfen birini seçiniz.'
        }
      });

      $('#frmMainSector').validate({
        errorClass: 'field-validation-error',
        errorElement: 'span',
        messages: {
          Main_SectorName: ' Boş geçilemez',
          Main_SectorOrder: ' Boş geçilemez' //Create_SectorGroup: '  Lütfen birini seçiniz.'
        }
      });

      $('#EditCategoryForm').validate({
        errorClass: 'field-validation-error',
        errorElement: 'span',
        messages: {
          Edit_Name: '  Boş geçilemez',
          Edit_Order: '  Boş geçilemez'
        }
      });

      $('#NCategoryCreateForm').validate({
        errorClass: 'field-validation-error',
        errorElement: 'span',
        messages: {
          Create_CategoryName: ' Boş geçilemez',
          Create_CategoryOrder: ' Boş geçilemez',
          Create_SelectionCategory: ' Lütfen birini seçiniz'
        }
      });

      $('#SCategoryCreateForm').validate({
        errorClass: 'field-validation-error',
        errorElement: 'span',
        messages: {
          Create_SerieModelName: ' Boş geçilemez',
          Create_SerieModelOrder: ' Boş geçilemez',
          Create_SelectionSerieModel: '  Lütfen birini seçiniz'
        }
      });
      $.metadata.setType('attr', 'validate');
    }

    function InitModals() {
      $('#loadingWindow').dialog({ autoOpen: false, modal: true, width: 400, height: 190 });
      $('#SectorCreate').dialog({ autoOpen: false, modal: true, width: 400,
        buttons: {
          "Kaydet": function () {
            var a = $('#SectorCreateForm').valid();
            if (a) {
              $.ajax({
                url: '/Category/InsertRow',
                type: 'post',
                data: {
                  CategoryName: $('#Create_SectorName').val(), //CategoryGroupType: $('#Create_SectorGroup').val(),
                  CategoryParentId: $('#ParentId').val(),
                  CategoryOrder: $('#Create_SectorOrder').val(),
                  CategoryType: 6
                },
                success: function (result) {
                  Insert($('#ModalId').val(), {
                    text: $('#Create_SectorName').val(),
                    groupId: $('#Create_SectorGroup').val(),
                    tool: result.tool,
                    id: result.id
                  });
                  $('#SectorCreate').dialog('close');
                },
                error: function (x) { alert(x.responseText); }
              });
            }
          }
        }
      });

      $('#MainSectorCreate').dialog({ autoOpen: false, modal: true, width: 400,
        buttons: {
          "Kaydet": function () {
            var a = $('#frmMainSector').valid();
            if (a) {
              $.ajax({
                url: '/Category/InsertRow',
                type: 'post',
                data: {
                  CategoryName: $('#Main_SectorName').val(), //CategoryGroupType: $('#Create_SectorGroup').val(),
                  CategoryParentId: null,
                  CategoryOrder: $('#Main_SectorOrder').val(),
                  CategoryType: 0
                },
                success: function (result) {
                  //                  Insert('#mixed', {
                  //                    text: $('#Main_SectorName').val(),
                  //                    groupId: 6,
                  //                    tool: result.tool,
                  //                    id: result.id
                  //                  });
                  window.location = "/category/allindex";
                  $('#MainSectorCreate').dialog('close');
                },
                error: function (x) { alert(x.responseText); }
              });
            }
          }
        }
      });

      $('#EditCategory').dialog({ autoOpen: false, modal: true, width: 600,height:500,
        buttons: {
          "Kaydet": function () {
            var a = $('#EditCategoryForm').valid();
            if (a) {
              $.ajax({
                url: '/Category/EditPost',
                type: 'post', 
                data: {
                  CategoryId: $('#Edit_CategoryId').val(),
                  CategoryName: $('#Edit_Name').val(),
                  CategoryOrder: $('#Edit_Order').val(),
                  Title: $('#Edit_SeoTitle').val(),
                  Keywords: $('#Edit_SeoKeyWord').val(),
                    Description: $('#Edit_SeoDesc').val(),
                    CategoryContentTitle: $("#CategoryContentTitle").val(),
                    StorePageTitle: $("#CategoryStoreTitle").val(),
                    CategoryUrl: $("#CategoryUrl").val(),
                    BaseMenuOrder: $("#BaseMenuOrder").val()

                },
                success: function (entity) {
                    var _ID = '#' + $('#Edit_CategoryId').val();
                    if (entity.Title && entity.Description && entity.Keywords) {
                        $(_ID + ' span:first div:first .edit').html("Düzenle <b style='color: red'>(S)</b>");
                    } else {
                        $(_ID + ' span:first div:first .edit').html("Düzenle");
                    }
                  
                  $(_ID + ' span:first .sort').html(entity.CategoryOrder);
                  $('#EditCategory').dialog('close');
                },
                error: function (x) { alert(x.responseText); }
              });
            }
          }
        }
      });

      $('#NCategoryCreate').dialog({ autoOpen: false, modal: true, width: 400,
        buttons: {
          "Kaydet": function () {
            var a = $('#NCategoryCreateForm').valid();
            if (a) {
              $.ajax({
                url: '/Category/InsertRow',
                type: 'post',
                data: {
                  CategoryName: $('#Create_CategoryName').val(),
                  CategoryParentId: $('#ParentId').val(),
                  CategoryOrder: $('#Create_CategoryOrder').val(),
                  CategoryType: $("input:radio[name=Create_SelectionCategory]:checked").val()
                },
                success: function (result) {
                  Insert($('#ModalId').val(), {
                    text: $('#Create_CategoryName').val(),
                    tool: result.tool,
                    id: result.id
                  });
                  $('#NCategoryCreate').dialog('close');
                },
                error: function (x) { alert(x.responseText); }
              });
            }
          }
        }
      });

      $('#SCategoryCreate').dialog({ autoOpen: false, modal: true, width: 400,
        buttons: {
          "Kaydet": function () {
            var a = $('#SCategoryCreateForm').valid();
            if (a) {
              $.ajax({
                url: '/Category/InsertRow',
                type: 'post',
                data: {
                  CategoryName: $('#Create_SerieModelName').val(),
                  CategoryParentId: $('#ParentId').val(),
                  CategoryOrder: $('#Create_SerieModelOrder').val(),
                  CategoryType: $("input:radio[name=Create_SelectionSerieModel]:checked").val()
                },
                success: function (result) {
                  if (result) {
                    Insert($('#ModalId').val(), {
                      text: $('#Create_SerieModelName').val(),
                      tool: result.tool,
                      id: result.id
                    });
                  }
                  $('#SCategoryCreate').dialog('close');
                },
                error: function (x) { alert(x.responseText); }
              });
            }
          }
        }
      });


    }
    $(".categoryPlaceType").change(function () {
        if (this.checked) {
       
        }
          });

    function PlaceTypeChange()
    {
        $("input[class = 'categoryPlaceType']").each(function () {
            var c = $(this).is(":checked");
            if(c)
            {
                var t = $(this).attr("hd");
                var categoryId = $(this).attr("CatId");
                $.ajax({
                    type: "POST",
                    url: '/Category/EditCategoryPlaceType',
                    data: { id: t, CategoryId: categoryId, status: 1,isProduct:true },
                    dataType: "json",
                    success: function (data) {

                    }
                });

            }
            else
            {
                var t = $(this).attr("hd");
                var categoryId = $(this).attr("CatId");
                $.ajax({
                    type: "POST",
                    url: '/Category/EditCategoryPlaceType',
                    data: { id: $(this).attr("hd"), CategoryId: categoryId, status: 0, isProduct: true },
                    dataType: "json",
                    success: function (data) {

                    }
                })
            }

        })
    }
          $(document).ready(function () {
      

            $(".categoryPlaceType").change(function () {            
                var select = "";
                var categoryId = $('#Edit_CategoryId').val();
                $("input[class = 'categoryPlaceType']").each(function () {                
                    var c = $(this).is(":checked");
                    if (c) {
                        var t = $(this).attr("hd");
                        $.ajax({
                            type: "POST",
                            url: '/Category/EditCategoryPlaceType',
                            data: { id: $(this).attr("hd"),CategoryId:categoryId,status:1 },
                            dataType: "json",
                            success: function (data) {
                              
                            }
                        })
                    } else {
                   
                    }                
                })                                
            })        
        })

    function InitEvents() {

      $('#btnSectorCreate').click(function () {
        $('#MainSectorCreate').dialog('open');
      });

      $('.sector').live('click', function () {
        $('#ModalId').val($(this).attr('treeid'));
        $('#ParentId').val($(this).attr('parentid'));
        $('#SectorCreate').dialog('open');
        return false;
      });

      $('.groupC').live('click', function () {
        $('#ModalId').val($(this).attr('treeid'));
        $('#ParentId').val($(this).attr('parentid'));
        $('#Create_SectorGroup').val($(this).attr('groupid'));
        $('#SectorCreate').dialog('open');
        return false;
      });

      $('.categoryButton').live('click', function () {
        $('#ModalId').val($(this).attr('treeid'));
        $('#ParentId').val($(this).attr('parentid'));
        $('input[name=Create_SelectionCategory]').attr('checked', false);
        $('#Create_TopCategoryName').html($($(this).attr('treeid') + ' span:first label:first').html());
        $('#NCategoryCreate').dialog('open');
        return false;
      });

      $('.categoryButton2').live('click', function () {
        $('#ModalId').val($(this).attr('treeid'));
        $('#ParentId').val($(this).attr('parentid'));
        $('input[name=Create_SelectionSerieModel]').attr('checked', false);
        $('#Create_TopSerieModelName').html($($(this).attr('treeid') + ' span:first label:first').html());
        $('#SCategoryCreate').dialog('open');
        return false;
      });

      $('.edit').live('click', function () {
        $.ajax({
          url: '/Category/EditCategory',
          type: 'post',
          data: { id: $(this).attr('categoryid') },
          success: function (entity) {
            var ec = entity.CategoryParentId;

            if (ec != null) {
              $('#Edit_TopName').html($($('#' + entity.CategoryParentId.toString()).attr('treeid') + ' span:first label:first').html());
            } else {
              $('#Edit_TopName').html('Sektor');
            }

            $('#Edit_CategoryId').val(entity.CategoryId);

            $('#Edit_Name').val(entity.CategoryName);

            $('#Edit_Order').val(entity.CategoryOrder);

            $('#Edit_SeoTitle').val(entity.Title);

              $('#Edit_SeoKeyWord').val(entity.Keywords);
              $("#CategoryContentTitle").val(entity.CategoryContentTitle);
              $("#CategoryStoreTitle").val(entity.StorePageTitle);
              $("#CategoryUrl").val(entity.CategoryUrl);
              $('#Edit_SeoDesc').val(entity.Description);
              $("#BaseMenuOrder").val(entity.BaseMenuOrder);
              GetCategoryPlaces(entity.CategoryId);

              var src = "/Category/EditCategoryQuestion/" + entity.CategoryId;
              $("#lightbox_click").attr("href", src);
            $('#EditCategory').dialog('open');
          }
        });
        return false;
      });
    }
    function GetCategoryPlaces(id1)
    {
        $.ajax({
            url: '/Category/GetCategoryPlace',
            type: 'POST',
            data: { id: id1, isProduct: true },
            success: function (data) {
                $("#categoryPlaceWrap").html(data);
            }
        });
    }
    $(document).ready(function () {
      initTrees();
      menuRegister();
      InitModals();
    });

    function validPaste() {
      var value = parseInt($('#SourceId').val());
      return value > 0;
    }

    var obj = null;

    var menu1 = [
      {
        'Taşı': function (menuItem, menu) {
          obj = null;
          $('#SourceId').val(this.id);
          $(this).addClass('cutRow');
          obj = this;
          $.contextMenu.hide();
        }
      },
      {
        'Yapıştır': {
          onclick: function (menuItem, menu) {
            try {
              if (obj == this) {
                alert('Seçili kategori üzerine ekleme yapılamaz.');
                return;
              }
              var item = this;
              var result = false;
              $(obj).find('ul li').each(function () {
                if (item == this) {
                  result = true;
                }
              });
              if (result) {
                alert('Üst kategori alt kategoriye eklenemez.');
                return;
              }
              $('#loadingWindow').dialog('open');

              var attr = this.getAttribute('groupid');

              if (attr == null) {
                attr = 0;
              }

              $(obj).removeClass('cutRow');
              updateMove($('#SourceId').val(), this.id, attr, obj, this);

            } finally {
                $.contextMenu.hide();
                //window.location = '/Category/AllIndex';
            }
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
                console.log(menu);
                var CatId = menu.target.id;
                Delete(menuItem, CatId);
            $.contextMenu.hide();
          }
        }
      }
    ];

    function menuRegister() {
      $('.liRow').contextMenu(menu1,
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

    function updateMove(sId, pId, groupId, sourceObj, targetObj) {
      $.ajax({
        url: '/Category/UpdateMove',
        type: 'post',
        data: { sourceId: parseInt(sId), parentId: parseInt(pId), group: parseInt(groupId) },
          success: function (response)
          {
            if (response != null)
            {
                if (response.success == false) {
                    alert(response.responseText);
                    $('#loadingWindow').dialog('close');
                }
              }
            
            var current = '<li class="liRow" id=' + sId + '>' + $(sourceObj).html() + '</li>';
            var tree = targetObj;
            if ($(targetObj).find('ul').length <= 0)
            {
                $('<ul />').appendTo($(targetObj));
                tree = $(targetObj).treeview({ add: $(targetObj) });
            }
            var item = $(current).appendTo($(targetObj).find('ul:first'));
            console.log(sourceObj);
            //$('#mixed').treeview({ remove: sourceObj});
            sourceObj.remove();
            $(tree).treeview({ add: item });
            $('#SourceId').val(0);
            menuRegister();
            $('#loadingWindow').dialog('close');
        },
        error: function (x) {
            debugger;
          alert(x.responseText);
        }
      });
    }

    function Insert(parent, model) {

      var tree = parent;
      if ($(parent).find('ul').length <= 0) {
        $('<ul />').appendTo($(parent));
        $(parent).treeview({ add: $(parent) });
      }

      var newSubItem = $("<li class=liRow id=\"" + model.id + "\"><span class='folder'><label>" + model.text + "</label><div style=\"float:right; width:750px;\">&nbsp;&nbsp;&nbsp;&nbsp;" + model.tool + "</div></span></li>").appendTo($(parent).find('ul:first'));

      $(tree).treeview({ add: $(newSubItem)[0] });

      menuRegister();
    }


          function Delete(id, categoryId) {
          if (confirm('Kategoriyi silmek istediğinizden eminmisiniz ?')) {
            $.ajax({
              url: '/Category/Delete',
              type: 'post',
              data: { cId: categoryId },
              success: function (result) {
                  if (result) {
                      var Itm = document.getElementById(categoryId.toString());
                      console.log(Itm);
                      Itm.remove();
                      //$('#' + id).remove();
                      //$('#mixed').treeview({ remove: $(id)[0] });
                }
                else {
                  alert('Alt kategorisi bulunan kategori silinemez.');
                }
              },
              error: function (x) { alert(x.responseText); }
            });
          }
          return false;
    }

  </script>
  <style type="text/css">
    .column
    {
      float: left;
      width: 75px;
      border-left: 1px solid #DDD;
      text-align: center;
    }
    .liRow
    {
      position: relative;
      z-index: 1000;
    }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%: Html.Hidden("ModalId", 0) %>
  <%: Html.Hidden("ParentId", 1) %>
  <%: Html.Hidden("SourceId", 0) %>
  <table class="TableList" width="100%" cellpadding="3" cellspacing="0">
    <thead>
      <tr>
        <td class="Header HeaderBegin">
           Kategoriler
          <% var entities = new MakinaTurkiyeEntities();%>
          <% int cou = (from c in entities.Categories
                        where c.MainCategoryType == (byte)MainCategoryType.Ana_Kategori
                        select c).Count(); %>
          (Toplam : <%=cou %>)
        </td>
        <td class="Header" style="width: 230px">
          &nbsp;&nbsp;&nbsp;&nbsp;
          <button id="btnSectorCreate">
            &nbsp;&nbsp;&nbsp;&nbsp;SEKTOR EKLE&nbsp;&nbsp;&nbsp;&nbsp;</button>
        </td>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td colspan="2">
          <ul id="mixed">
            Kategoriler yükleniyor...
          </ul>
        </td>
      </tr>
    </tbody>
  </table>
  <div id="NCategoryCreate" title="Kategori Ekle" style="display: none;">
    <form action="/" method="post" id="NCategoryCreateForm">
    <table border="0" cellpadding="5" cellspacing="0">
      <tr>
        <td>
          Üst Kategori Adı
        </td>
        <td>
          :
        </td>
        <td>
          <label id="Create_TopCategoryName">
          </label>
        </td>
      </tr>
      <tr>
        <td>
          Kategori Adı
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBox("Create_CategoryName", "", new { validate = "required:true" })%>
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
          <%: Html.TextBox("Create_CategoryOrder", "0")%>
        </td>
      </tr>
      <tr>
        <td>
        </td>
        <td colspan="2">
          <label for="Create_SelectionCategory_1" style="width: 70px; display: block; float: left;
            cursor: pointer">
            Kategori
          </label>
          <input type="radio" name="Create_SelectionCategory" id="Create_SelectionCategory_1"
            value="1" validate="required:true" style="float: left" />
          <div style="width: 100%; height: 3px; float: left; cursor: pointer">
          </div>
          <label for="Create_SelectionCategory_2" style="width: 70px; display: block; float: left;
            cursor: pointer">
            Marka
          </label>
          <input type="radio" name="Create_SelectionCategory" id="Create_SelectionCategory_2"
            value="3" style="float: left; cursor: pointer" />
        </td>
      </tr>
    </table>
    </form>
  </div>
    <div id="sIconAdd" title="İcon Ekle" style="display:none">
        <form action="/" method="post" id="SIconAddForm"
            >
    <table border="0" cellpadding="5" cellspacing="0">
        <tr>
            <td>İcon</td>
            <td>:</td>
            <td><input type="file" name="iconName" /></td>
        </tr>   
    </table>
         </form>
        </div>
  <div id="SCategoryCreate" title="Kategori Ekle" style="display: none;">
    <form action="/" method="post" id="SCategoryCreateForm">
    <table border="0" cellpadding="5" cellspacing="0">
      <tr>
        <td>
          Üst Kategori Adı
        </td>
        <td>
          :
        </td>
        <td>
          <label id="Create_TopSerieModelName">
          </label>
        </td>
      </tr>
      <tr>
        <td>
          Kategori Adı
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBox("Create_SerieModelName", "", new { validate = "required:true" })%>
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
          <%: Html.TextBox("Create_SerieModelOrder", "0")%>
        </td>
      </tr>
      <tr>
        <td>
        </td>
        <td colspan="2">
          <label for="Radio1" style="width: 70px; display: block; float: left; cursor: pointer">
            Seri
          </label>
          <input type="radio" name="Create_SelectionSerieModel" id="Radio1" value="4" validate="required:true"
            style="float: left; cursor: pointer" />
          <div style="width: 100%; height: 3px; float: left;">
          </div>
          <label for="Radio2" style="width: 70px; display: block; float: left; cursor: pointer">
            Model
          </label>
          <input type="radio" name="Create_SelectionSerieModel" id="Radio2" value="5" style="float: left;
            cursor: pointer" />
        </td>
      </tr>
    </table>
    </form>
  </div>
  <div id="SectorCreate" title="Kategori Ekle" style="display: none;">
    <form action="/" method="post" id="SectorCreateForm">
    <table border="0" cellpadding="5" cellspacing="0">
      <tr>
        <td>
          Kategori Adı
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBox("Create_SectorName", "", new { validate = "required:true" })%>
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
          <%: Html.TextBox("Create_SectorOrder", "0")%>
        </td>
      </tr>
    </table>
    </form>
  </div>
        <input id="activeText" type="hidden" value="" />
  <div id="EditCategory" title="Kategori Düzenle" style="display: none">
    <form action="/" method="post" id="EditCategoryForm">
    <table border="0" cellpadding="5" cellspacing="0">
      <tr>
        <td>
          Üst Kategori Adı
        </td>
        <td>
          :
        </td>
        <td colspan="4">
          <label id="Edit_TopName">
          </label>
        </td>

      </tr>
      <tr>
        <td>
          Kategori Adı
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBox("Edit_Name", "", new { validate = "required:true" })%>
          <%: Html.Hidden("Edit_CategoryId") %>
            
        </td>
          <td>
              <a id="lightbox_click" rel="superbox[iframe]" title="Özel Soru Tanımla"> <span style="padding:2px; color:#014bc1; font-weight:600;">Özel Soru Tanımla</span></a>
          </td>
      </tr>
       <tr>
        <td>
          Kategori Başlık
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBox("CategoryContentTitle", "")%>
          <%: Html.Hidden("Edit_CategoryId") %>
        </td>
           <td>
               Kategori Firma Başlık
           </td>
           <td>:</td>
           <td>
           <%: Html.TextBox("CategoryStoreTitle", "")%>
          <%: Html.Hidden("Edit_CategoryId") %>
           </td>
      </tr>
       <tr>
        <td>
       Kategori Url
        </td>
        <td>
          :
        </td>
        <td colspan="4">
          <%: Html.TextBox("CategoryUrl", "",new { @style = "width:350px"})%>
          <%: Html.Hidden("Edit_CategoryId") %>
        </td>
      </tr>
       <tr>
        <td>
          Seo Title
        </td>
        <td>
          :
        </td>
        <td colspan="4">
          <%: Html.TextBox("Edit_SeoTitle", "",new { @style = "width:350px"})%>
        </td>
      </tr>
       <tr>
        <td>
          Seo Description
        </td>
        <td>
          :
        </td>
        <td colspan="4">
          <%: Html.TextArea("Edit_SeoDesc", "",new { @style = "width:350px;height:50px"})%>
        </td>
      </tr>
       <tr>
        <td>
          Seo Keyword
        </td>
        <td>
          :
        </td>
        <td colspan="4">
          <%: Html.TextBox("Edit_SeoKeyWord", "",new { @style = "width:350px"})%>
        </td>
      </tr>
             <tr>
        <td>
          Sıra
        </td>
        <td>
          :
        </td>
        <td colspan="4">
          <%: Html.TextBox("Edit_Order", "0")%>
        </td>
      </tr>
        <tr>
            <td>Menü Sıra</td>
            <td>:</td>
            <td colspan="4">
                <%:Html.TextBox("BaseMenuOrder") %>
            </td>
        </tr>

        <tr>
            <td>Kategori Yeri</td>
                       <td>:</td>
            <td id="categoryPlaceWrap" colspan="4">
                <%--<input type="checkbox"  name="CategoryPlace" hd="<%:(byte)CategoryPlaceType.HomeLeftSide %>" class="categoryPlaceType" />Anasayfa Sol <input type="checkbox"   name="CategoryPlace" class="categoryPlaceType" hd="<%:(byte)CategoryPlaceType.HomeCenter %>" />Anasayfa Orta
                <input type="checkbox"   name="CategoryPlace" class="categoryPlaceType" hd="<%:(byte)CategoryPlaceType.HomeChoicesed %>" />Anasayfa Seçilen
                <input type="checkbox"   name="CategoryPlace" class="categoryPlaceType" hd="<%:(byte)CategoryPlaceType.ProductGroup %>" />Ürün Grubu--%>

            </td>
                    <tr>
            <td colspan="6">

                          <button type="button" title="Kategori" value="{Kategori}">
            Kategori</button>
              <button type="button" title="Kategori Baslik" value="{KategoriBaslik}">
            KategoriBaslik</button>
          <button type="button" title="Tüm Üst Kategori" value="{UstKategori}">
            UstKategori</button>
          <button type="button" title="Birinci Üst Kategori" value="{IlkUstKategori}">
            IlkUstKategori</button>
            
             <button type="button" title="Birinci Üst Kategori" value="{IlkUstKategoriBaslik}">
            IlkUstKategoriBaslik</button>
          <button type="button" title="Marka" value="{Marka}">
            Marka</button>
          <button type="button" title="Model Markası" value="{ModelMarka}">
            ModelMarka</button>
          <button type="button" title="Model" value="{Model}">
            Model</button>
          <button type="button" title="Seri" value="{Seri}">
            Seri</button>
          <button type="button" title="Ürün Tipi" value="{UrunTipi}">
            UrunTipi</button>
          <button type="button" title="Ürün Durumu" value="{UrunDurumu}">
            UrunDurumu</button>
          <button type="button" title="Satış Detayı" value="{SatisDetayi}">
            SatisDetayi</button>
          <button type="button" title="Kısa Detay" value="{KisaDetay}">
            KisaDetay</button>
          <button type="button" title="Fiyatı" value="{Fiyati}">
            Fiyati</button>
          <button type="button" title="Model Yılı" value="{ModelYili}">
            ModelYili</button>
          <button type="button" title="Ürün Adı" value="{UrunAdi}">
            UrunAdi</button>
          <button type="button" title="Firma Adı" value="{FirmaAdi}">
            FirmaAdi</button>
          <button type="button" title="Ürün Grupları" value="{UrunGrubuIsimleri}">
            UrunGrubuIsimleri</button>
          <button type="button" title="Aktif Kategorinin Alt Kategorisi" value="{AltKategoriForAktifKategori}">
            AltKategoriForAktifKategori</button>
            <button type="button" title="Ulke" value="{Ulke}">
            Ülke</button>
            <button type="button" title="Sehir" value="{Sehir}">
            Şehir</button>
             <button type="button" title="İlçe" value="{Ilce}">
            İlçe</button>
            <button type="button" title="Aranan Kelime" value="{ArananKelime}">
            Aranan Kelime</button>
             <button type="button" title="Aranan Kelime" value="{KategoriBaslik}">
            Kategori Başlık</button>
            </td>
        </tr>
        </tr>
    </table>
    </form>
  </div>
  <div id="MainSectorCreate" title="Kategori Ekle" style="display: none;">
    <form action="/" method="post" id="frmMainSector">
    <table border="0" cellpadding="5" cellspacing="0">
      <tr>
        <td>
          Kategori Adı
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBox("Main_SectorName", "", new { validate = "required:true" })%>
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
          <%: Html.TextBox("Main_SectorOrder", "0")%>
        </td>
      </tr>

    </table>
    </form>
  </div>
  <div id="loadingWindow">
    <p style="text-align: center">
      YÜKLENİYOR....
    </p>
  </div>
</asp:Content>
