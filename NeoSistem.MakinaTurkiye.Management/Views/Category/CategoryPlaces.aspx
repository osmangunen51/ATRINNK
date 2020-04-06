﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<CategoryPlaceModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  MK Yönetim Sistemi | Sabit Alanlar
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
   <link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
  <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
  	<style type="text/css">
		/* Custom Theme */
		#superbox-overlay{background:#e0e4cc;}
		#superbox-container .loading{width:32px;height:32px;margin:0 auto;text-indent:-9999px;background:url(styles/loader.gif) no-repeat 0 0;}
		#superbox .close a{float:right;padding:0 5px;line-height:20px;background:#333;cursor:pointer;}
		#superbox .close a span{color:#fff;}
		#superbox .nextprev a{float:left;margin-right:5px;padding:0 5px;line-height:20px;background:#333;cursor:pointer;color:#fff;}
		#superbox .nextprev .disabled{background:#ccc;cursor:default;}
	</style>
  <script type="text/javascript">
    function tıkla() {
      $('#lightbox_click').trigger('click');
    }
    $(document).ready(function () {
      $('#NewConstantForm').dialog({ autoOpen: false, modal: true, width: 420, height: 140, resizable: false });
    });

    function DeleteCategoryPlace(placeId) {
      if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/Category/DeleteCategoryPlace',
          data: { id: placeId },
          type: 'post',
          dataType: 'json',
          success: function (data) {
            
            if (data) {
                $('#row' + placeId).hide();
            }
            else {
              alert('Silme işlemi başarısız.');
            }
          }
        });
      }
    }
    function GetCategoryPlaces(placeType)
    {
       
        $.ajax({
            type: 'post',
            url: '/Category/GetCategoryPlacesByCategoryType',
            data: { type:placeType },
            success: function (data) {
                $(".dataList").html(data);
            },
            error: function (x, y, z) {
                alert('Bir Hata Oluştu. Lütfen Destek Ekibimiz ile İletişime Geçiniz');
            }
        });

    }
    
    function ShowEditOrder(id)
    {
        $("#displayOrderWrap"+id).hide();
        $("#showEditOrderWrap"+id).show();
    }
    function EditOrder(id)
    {
        var Order = $("#txtOrder" + id).val();
        $.ajax({
            url: '/Category/EditCategoryPlaceOrder',
            data: { id: id,order:Order },
            type: 'post',
            success: function (data) {
                $("#txtOrder" + id).val(Order);
                $("#displayOrderWrap" + id).show();
                $("#showEditOrderWrap" + id).hide();
                $("#orderWrap" + id).html(Order);
            }, error: function (x, l, e) {
                alert(e.responseText);
            }
        });

    }
    
  </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <input id="ConstantId" type="hidden" value="0" />
  <input id="ConstantType" type="hidden" value="0" />
  <div id="NewConstantForm" title="">
    <%= Html.RenderHtmlPartial("ConstantForm", new ConstantModel())%>
  </div>
  <div style="width: auto; height: auto; float: left; padding: 10px 0px 10px 10px">
    <select   onchange="GetCategoryPlaces($(this).val())">
      <option value="0">< Tümünü Göster ></option>
        <option value="<%=(byte)CategoryPlaceType.HomeLeftSide %>">Anasayfa Sol</option>
        <option value="<%=(byte)CategoryPlaceType.HomeCenter %>">Anasayfa Orta</option>
        <option value="<%=(byte)CategoryPlaceType.HomeChoicesed %>">Anasayfa Seçilmiş</option>
        <option value="<%=(byte)CategoryPlaceType.ProductGroup %>">Ürün Grubu</option>

    </select>&nbsp;&nbsp;
  </div>
  <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" unselectable="on">
          Kategori Adı
          </td>
             <td class="Header" style="width: 70px; height: 19px">
              Bölge Tipi
          </td>
          <td id="tabloadd" class="Header" style="width: 70px; height: 19px;display:none;">
          </td>
          <td class="Header" style="width: 70px; height: 19px">
              Sıra
          </td>
            <td class="Header" style="width: 70px; height: 19px"></td>
        </tr>
      </thead>
      <tbody id="table" class="dataList">
        <%= Html.RenderHtmlPartial("CategoryPlaceList",Model.Categories)%>
      </tbody>
      <tfoot>
        <tr>
            
          <td class="ui-state ui-state-hover" style="border-color: #DDD;
            border-top: none; padding-right: 10px" colspan="2">
           : <div id="statisticproduct" style="margin-top: 5px">

            </div>
          </td>
          <td class="ui-state ui-state-hover" colspan="2" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
           
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
</asp:Content>
