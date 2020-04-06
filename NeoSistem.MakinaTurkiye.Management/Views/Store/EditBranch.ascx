﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StoreModel>" %>
<script type="text/javascript">
  function DealerAddForBranch() {
    $.ajax({
      url: '/Store/DealerAddForBranch',
      type: 'post',
      dataType: 'json',
      data: { DealerNameForBranch: $('#DealerNameForBranch').val(), storeId: $('#storeId').val() },
      success: function (data) {
        $('#DealerNameForBranch').val('');

        var dealerItems = eval(data);

        $("#StoreDealerId").empty();

        $("#StoreDealerId").append("<option value=\"0\">" + "< Lütfen Seçiniz >" + "</option>");
        for (var i = 0; i < dealerItems.length; i++) {
          $("#StoreDealerId").append("<option value=\"" + dealerItems[i].StoreDealerId + "\">" + dealerItems[i].DealerName + "</option>");
        }
        alert('Şubeniz başarıyla eklenmiştir.');
      },
      error: function (x, l, e) {
        alert(x.responseText);
      }
    });
  }

  function checkForm() {
    if ($('#StoreDealerId').val() == '0') {
      alert('Ekleyeceğiniz adresin şubesini seçin.');
      return false;
    }
    else {
      return true;
    }
  }
</script>
<div style="width: 500px; float: left;">
  <div style="width: auto; height: auto; margin-top: 20px; margin-left: 20px;">
    <div style="font-size: 13px; font-weight: bold; margin-left: 50px; width: 700px">
      Şube ve iletişim bilgilerini ekleyiniz.
    </div>
    <div style="float: left; height: auto; width: 700px; margin-top: 15px;">
      <div style="width: 105px; height: auto; float: left; padding-right: 3px; text-align: right;
        font-size: 12px; padding-top: 3px; margin-top: 3px;">
        Şube Adı*
      </div>
      <div style="float: left; margin-left: 4px;">
        <div class="textBig">
          <%= Html.TextBox("DealerNameForBranch")%>
        </div>
      </div>
      <div style="float: left; margin-left: 5px;">
        <button type="button" onclick="DealerAddForBranch();">
          Ekle
        </button>
      </div>
    </div>
  </div>
  <div style="width: 400px; height: auto; float: left; margin-top: 10px;">
    <div style="width: 400px; float: left; font-size: 12px; font-weight: bold; margin-left: 50px;
      border-top: solid 1px #bababa; padding-top: 10px; padding-bottom: 10px; padding-left: 10px;">
      İletişim Bilgilerini Ekleyiniz
    </div>
    <div style="width: 105px; height: auto; float: left; padding-right: 3px; text-align: right;
      font-size: 12px; font-size: 12px; margin-top: 5px; margin-left: 20px;">
      Şubeler*
    </div>
    <div style="float: left;">
      <div class="dropdownAddress">
        <%:Html.DropDownList("StoreDealerId", Model.DealerItemsForBranch, new { style = "width : 203px; height: 18px; font-size: 11px; border: none; margin: 1px; margin-right: 2px;" })%></div>
    </div>
  </div>
  <div style="float: left;">
    <%=Html.RenderHtmlPartial("Address") %>
    <div style="margin: 20px auto; padding-top: 10px; border-top: solid 1px #DDD; width: 99%;
      float: left">
      <button type="submit" onclick="return checkForm();">
        Kaydet</button>
      <button type="reset">
        İptal</button>
    </div>
  </div>
</div>
<div style="float: left; margin-left: 100px; width: 300px; margin-top: 80px">
  <div style="float: left; width: 300px; height: 30px;">
    <span style="font-size: 13px; font-weight: bold;">Şubeler ve Adresleri</span>
  </div>
  <div style="float: left;" id="divAddressItems">
    <%=Html.RenderHtmlPartial("DealerAddressItems", Model.DealerAddressItems)%></div>
</div>
<input type="hidden" id="hdnDealerType" value="<%:Request.QueryString["DealerType"] %>" />
<input type="hidden" id="storeId" name="storeId" value="<%:this.Page.RouteData.Values["id"] %>" />