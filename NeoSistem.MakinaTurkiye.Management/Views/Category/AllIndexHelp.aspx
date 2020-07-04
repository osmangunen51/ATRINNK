﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<TreeViewNode>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Yardım Kategorileri
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
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
  <style type="text/css">
    .cutRow
    {
      color: red;
    }
  </style>
  <%byte id = ViewData["id"].ToByte(); %>
  
      <script type="text/javascript">


          function initTrees() {

              $("#mixed").treeview({
                  url: "/Category/GetNodesHelp",
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

//              $('#NCategoryCreateForm').validate({
//                  errorClass: 'field-validation-error',
//                  errorElement: 'span',
//                  messages: {
//                      Create_CategoryName: ' Boş geçilemez',
//                      Create_CategoryOrder: ' Boş geçilemez',
//                      Create_SelectionCategory: ' Lütfen birini seçiniz'
//                  }
//              });

//              $('#SCategoryCreateForm').validate({
//                  errorClass: 'field-validation-error',
//                  errorElement: 'span',
//                  messages: {
//                      Create_SerieModelName: ' Boş geçilemez',
//                      Create_SerieModelOrder: ' Boş geçilemez',
//                      Create_SelectionSerieModel: '  Lütfen birini seçiniz'
//                  }
//              });


              $.metadata.setType('attr', 'validate');
          }
          function PlaceTypeChange() {
              $("input[class = 'categoryPlaceType']").each(function () {
                  var c = $(this).is(":checked");
                  if (c) {
                      var t = $(this).attr("hd");
                      var categoryId = $(this).attr("CatId");
                      $.ajax({
                          type: "POST",
                          url: '/Category/EditCategoryPlaceType',
                          data: { id: t, CategoryId: categoryId, status: 1, isProduct: false },
                          dataType: "json",
                          success: function (data) {

                          }
                      });

                  }
                  else {
                      $.ajax({
                          type: "POST",
                          url: '/Category/EditCategoryPlaceType',
                          data: { id: $(this).attr("hd"), CategoryId: categoryId, status: 0,isProduct:false },
                          dataType: "json",
                          success: function (data) {

                          }
                      })
                  }

              })




          }
          function InitModals() {
              $('#loadingWindow').dialog({ autoOpen: false, modal: true, width: 900, height: 250 });
              $('#SectorCreate').dialog({ autoOpen: false, modal: true, width: 900,
                  buttons: {
                      "Kaydet": function () {
                          var a = $('#SectorCreateForm').valid();
                          if (a) {
                              $.ajax({
                                  url: '/Category/InsertRowHelp',
                                  type: 'post',
                                  data: {
                                      CategoryName: $('#Create_SectorName').val(), //CategoryGroupType: $('#Create_SectorGroup').val(),
                                      CategoryParentId: $('#ParentId').val(),
                                      CategoryOrder: $('#Create_SectorOrder').val(),
                                      CategoryType: 7
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
                                  url: '/Category/InsertRowHelp',
                                  type: 'post',
                                  data: {
                                      CategoryName: $('#Main_SectorName').val(), //CategoryGroupType: $('#Create_SectorGroup').val(),
                                      CategoryParentId: null,
                                      CategoryOrder: $('#Main_SectorOrder').val(),
                                      CategoryType: 7,
                                      
                                  },
                                  success: function (result) {
                                      //                  Insert('#mixed', {
                                      //                    text: $('#Main_SectorName').val(),
                                      //                    groupId: 6,
                                      //                    tool: result.tool,
                                      //                    id: result.id
                                      //                  });
                                      window.location = "/category/allindexHelp";
                                      $('#MainSectorCreate').dialog('close');
                                  },
                                  error: function (x) { alert(x.responseText); }
                              });
                          }
                      }
                  }
              });

              $('#EditCategory').dialog({ autoOpen: false, modal: true, width: 900,
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
                                      CategoryOrder: $('#Edit_Order').val()
                                  },
                                  success: function (entity) {
                                      var _ID = '#' + $('#Edit_CategoryId').val();
                                      $(_ID + ' span:first label:first').html(entity.CategoryName);
                                      $(_ID + ' span:first .sort').html(entity.CategoryOrder);
                                
                                      $('#EditCategory').dialog('close');
                                     
                                   
                                  },
                                  error: function (x) { alert(x.responseText); }
                              });
                          }
                      }
                  }
              });
   
              $('#NCategoryCreate').dialog({ autoOpen: false, modal: true, width: 900,
                  buttons: {
                      "Kaydet": function () {
                          var a = $('#NCategoryCreateForm').valid();
                          if (a) {
                              $.ajax({
                                  url: '/Category/InsertRowHelp',
                                  type: 'post',
                                  data: {
                                      CategoryName: $('#Create_CategoryName').val(),
                                      CategoryParentId: $('#ParentId').val(),
                                      CategoryOrder: $('#Create_CategoryOrder').val(),
                                      CategoryType: 7
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

              $('#SCategoryCreate') .dialog({ autoOpen: false, modal: true, width: 900,
                  buttons: {
                      "Kaydet": function () {
                          var a = $('#SCategoryCreateForm').valid();
                          if (a) {
                              $.ajax({
                                  url: '/Category/InsertRowHelp',
                                  type: 'post',
                                  data: {
                                      CategoryName: $('#Create_SerieModelName').val(),
                                      CategoryParentId: $('#ParentId').val(),
                                      CategoryOrder: $('#Create_SerieModelOrder').val(),
                                      CategoryType: 7
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
                          GetCategoryPlaces(entity.CategoryId);
                          $('#Edit_Order').val(entity.CategoryOrder);
                      
                          $('#EditCategory').dialog('open');


                      }
                  });
                  return false;
              });
          }
          function GetCategoryPlaces(id1) {

              $.ajax({
                  url: '/Category/GetCategoryPlace',
                  type: 'POST',
                  data: { id: id1, isProduct: false },
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
                  success: function () {
                      var current = '<li class="liRow" id=' + sId + '>' + $(sourceObj).html() + '</li>';
                      var tree = targetObj;
                      if ($(targetObj).find('ul').length <= 0) {
                          $('<ul />').appendTo($(targetObj));
                          tree = $(targetObj).treeview({ add: $(targetObj) });
                      }

                      var item = $(current).appendTo($(targetObj).find('ul:first'));

                      $('#mixed').treeview({ remove: sourceObj });

                      $(tree).treeview({ add: item });


                      $('#SourceId').val(0);
                      menuRegister();
                      $('#loadingWindow').dialog('close');
                  },
                  error: function (x) {
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

              var newSubItem = $("<li class=liRow id=\"" + model.id + "\"><span class='folder'><label>" + model.text + "</label><div style=\"float:right; width:665px;\">&nbsp;&nbsp;&nbsp;&nbsp;" + model.tool + "</div></span></li>").appendTo($(parent).find('ul:first'));

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
                              $('#mixed').treeview({ remove: $(id)[0] });
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
      width: 100px;
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
                        where c.MainCategoryType == (byte)MainCategoryType.Yardim
                        select c).Count(); %>
          (Toplam : <%=cou %>)
        </td>
        <td class="Header" style="width: 230px">
          &nbsp;&nbsp;&nbsp;&nbsp;
          <button id="btnSectorCreate">
            &nbsp;&nbsp;&nbsp;&nbsp;Ana Kategori Ekle &nbsp;&nbsp;&nbsp;&nbsp;</button>
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
     
    </table>
    </form>
  </div>
  <div id="SCategoryCreate" title="Kategori Ekle" style="display: none;">
    <form action="/" method="post" id="SCategoryCreateForm">
    <table border="0" cellpadding="5" cellspacing="0">
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
  <div id="EditCategory" title="Kategori Düzenle" style="display: none;">
    <form action="/" method="post" id="EditCategoryForm">
    <table border="0" cellpadding="5" cellspacing="0">
      
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
      </tr>
      <tr>
        <td>
          Sıra
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBox("Edit_Order", "0")%>
        </td>
      </tr>
         <tr>
            <td>Kategori Yeri</td>
                       <td>:</td>
            <td id="categoryPlaceWrap">
                <%--<input type="checkbox"  name="CategoryPlace" hd="<%:(byte)CategoryPlaceType.HomeLeftSide %>" class="categoryPlaceType" />Anasayfa Sol <input type="checkbox"   name="CategoryPlace" class="categoryPlaceType" hd="<%:(byte)CategoryPlaceType.HomeCenter %>" />Anasayfa Orta
                <input type="checkbox"   name="CategoryPlace" class="categoryPlaceType" hd="<%:(byte)CategoryPlaceType.HomeChoicesed %>" />Anasayfa Seçilen
                <input type="checkbox"   name="CategoryPlace" class="categoryPlaceType" hd="<%:(byte)CategoryPlaceType.ProductGroup %>" />Ürün Grubu--%>

            </td>

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
