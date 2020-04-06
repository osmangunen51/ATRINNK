﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<StoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
  <style type="text/css">
    .small_input
    {
      width: 120px;
      font-size: 12px;
      padding-left: 5px;
    }
    .big_input
    {
      width: 180px;
      font-size: 12px;
      padding-left: 5px;
    }
    .btnSave
    {
      background-image: url('/Content/Images/btnSave.gif');
      width: 70px;
      height: 24px;
      border: none;
      cursor: pointer;
    }
    .btnAdd
    {
      background-image: url('/Content/Images/btnAdd.gif');
      width: 55px;
      height: 24px;
      border: none;
      cursor: pointer;
    }
    .fileUpload
    {
      font-size: 12px;
      width: 180px;
      height: 20px;
      border: solid 1px #bababa;
    }
    .profileBg
    {
      width: 967px;
      height: auto;
      background-image: url('/Content/Images/profileBg.gif');
      border: solid 1px #cbdfed;
      float: left;
      background-repeat: repeat-x;
      padding-bottom: 15px;
    }
    .profileStartLink
    {
      color: #727679;
      text-decoration: underline;
      font-size: 12px;
    }
    .profileStartLink:hover
    {
      color: #727679;
      font-size: 12px;
      text-decoration: underline;
    }
    .profileStartLink:visited
    {
      color: #727679;
      text-decoration: underline;
      font-size: 12px;
    }
    .speedStep
    {
      width: 900px;
      height: 100px;
      margin-left: 33px;
      margin-top: 60px;
    }
    .speedStepContent
    {
      width: auto;
      height: auto;
      float: left;
      margin-left: 250px;
      font-size: 12px;
      font-weight: bold;
      text-align: center;
    }
    .accordionHeader
    {
      background-image: url('/Content/Images/profilePanelBg.gif');
      border-bottom: solid 1px #aebecd;
    }
    .dropdownBig
    {
      width: 216px;
      height: 20px;
      border: solid 1px #bababa;
    }
    .dropdownBig select
    {
      width: 216px;
      height: 20px;
    }
    
    .textBig
    {
      width: 204px;
      height: 18px;
      border: solid 1px #bababa;
      padding-left: 12px;
      background-color: #fff;
      padding-top: 2px;
    }
    .textBig input
    {
      width: 190px;
      background-color: transparent;
      border: none;
    }
    
    .textBigArea
    {
      width: 204px;
      height: 58px;
      border: solid 1px #bababa;
      padding-left: 12px;
      background-color: #fff;
      padding-top: 4px;
    }
    .textBigArea textarea
    {
      width: 200px;
      height: 50px;
      background-color: transparent;
      border: none;
    }
    
    .textMedium
    {
      width: 64px;
      height: 18px;
      border: solid 1px #bababa;
      padding-left: 12px;
      background-color: #fff;
      padding-top: 2px;
      float: left;
    }
    .textMedium input
    {
      width: 50px;
      background-color: transparent;
      border: none;
    }
    
    .textSmall
    {
      width: 30px;
      height: 18px;
      border: solid 1px #bababa;
      padding-left: 2px;
      background-color: #fff;
      padding-top: 2px;
      float: left;
    }
    .textSmall input
    {
      width: 24px;
      background-color: transparent;
      border: none;
    }
  </style>
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
  <link href="/Content/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
  <script src="/Scripts/JQuery-dropdowncascading.js" type="text/javascript"></script>
  <script type="text/javascript">

    $(document).ready(function () {
      $('#tabs').tabs();
      $('.tabLi').show();

      $('.ui-widget-header').addClass('ui-state-default');
      $('div.ui-tabs').removeClass('ui-widget-content');

      $('#StorePacketBeginDate').datepicker();
      $('#StorePacketEndDate').datepicker();

      RegisterDropDownControls();

      $('div.accordionButton').click(function () {
        $('div.accordionContent').slideUp('normal');
        $(this).next().slideDown('normal');
        RegisterDropDownControls();
      });

      $("div.accordionContent").hide();

      $('#CityId').change(function () {
        $.ajax({
          url: '/Member/AreaCode',
          type: 'post',
          data: { CityId: $('#CityId').val() },
          success: function (data) {
            $('#InstitutionalPhoneAreaCode').val(data);
            $('#TextInstitutionalPhoneAreaCode').val(data);

            $('#InstitutionalPhoneAreaCode2').val(data);
            $('#TextInstitutionalPhoneAreaCode2').val(data);

            $('#InstitutionalFaxAreaCode').val(data);
            $('#TextInstitutionalFaxAreaCode').val(data);

            if ($('#CityId').val() == "34") {
              $('#InstitutionalPhoneAreaCode').val('212');
              $('#InstitutionalPhoneAreaCode2').val('212');
              $('#InstitutionalFaxAreaCode').val('212');
            }

          }
        });

        $('#CategoryId').show();

        if ($('#CityId').val() != "0") {
          if ($('#CityId').val() == "34") {
            $('#AreaCodeTextBox').hide();
            $('#AreaCodeSelect').show();
            $('#AreaCodeTextBox2').hide();
            $('#AreaCodeSelect2').show();
            $('#AreaCodeTextBox3').hide();
            $('#AreaCodeSelect3').show();

            $('#InstitutionalPhoneAreaCode').val('212');
            $('#InstitutionalPhoneAreaCode2').val('212');
            $('#InstitutionalFaxAreaCode').val('212');
          }
          else {
            $('#AreaCodeTextBox').show();
            $('#AreaCodeSelect').hide();
            $('#AreaCodeTextBox2').show();
            $('#AreaCodeSelect2').hide();
            $('#AreaCodeTextBox3').show();
            $('#AreaCodeSelect3').hide();
          }
        }
        else {
          $('#AreaCodeTextBox').show();
          $('#AreaCodeSelect').hide();
          $('#AreaCodeTextBox2').show();
          $('#AreaCodeSelect2').hide();
          $('#AreaCodeTextBox3').show();
          $('#AreaCodeSelect3').hide();
        }

      });

      $('#CountryId').change(function () {
        $.ajax({
          url: '/Member/CultureCode',
          type: 'post',
          data: { CountryId: $('#CountryId').val() },
          success: function (data) {
            $('#InstitutionalPhoneCulture').val(data);
            $('#InstitutionalPhoneCulture2').val(data);

            $('#InstitutionalGSMCulture').val(data);
            $('#InstitutionalGSMCulture2').val(data);

            $('#InstitutionalFaxCulture').val(data);
          }
        });

        if ($(this).val() > 0) {
          if ($(this).val() == 246) {
            $('#AvenueOtherCountries').hide();
            $('#Avenue').show();
          }
          else {
            $('#AvenueOtherCountries').show();
            $('#Avenue').hide();
          }
        }

        if ($(this).val() > 0 && $(this).val() != 246) {
          $('#labelAdres').show();
          $('#labelCadde').hide();
        }
        else if ($(this).val() == 246) {
          $('#labelAdres').hide();
          $('#labelCadde').show();
        }

      });

      $('#Loading').dialog
      ({
        autoOpen: false,
        width: 400,
        height: 200,
        modal: true
      });

      $('#BirthDate').datepicker();

      $('.tabStore').click(function () {

        window.location.href = this.href;
        $('.Content').scrollTop(0);
      });

      $('#InstitutionalGsmNumber').keyup(function () {
        if ($(this).val() != "") {
          $('#trGsmType').show();
        }
        else {
          $('#trGsmType').hide();
        }
      });

    });

    function openContent(content) {
      $(content).slideToggle();
    }

    $(function () {
      $("#accordion").accordion({ header: "h3" });
      $("#accordionParent").accordion({ header: "h2" });
    });

    function RegisterDropDownControls() {


      $('#TextInstitutionalPhoneAreaCode').keyup(function () {
        $('#InstitutionalPhoneAreaCode').val($(this).val());
      });
      $('#DropDownInstitutionalPhoneAreaCode').change(function () {
        $('#InstitutionalPhoneAreaCode').val($(this).val());
      });

      $('#TextInstitutionalPhoneAreaCode2').keyup(function () {
        $('#InstitutionalPhoneAreaCode2').val($(this).val());
      });
      $('#DropDownInstitutionalPhoneAreaCode2').change(function () {
        $('#InstitutionalPhoneAreaCode2').val($(this).val());
      });

      $('#TextInstitutionalFaxAreaCode').keyup(function () {
        $('#InstitutionalFaxAreaCode').val($(this).val());
      });
      $('#DropDownInstitutionalFaxAreaCode').change(function () {
        $('#InstitutionalFaxAreaCode').val($(this).val());
      });

      $('#CountryId').DropDownCascading({
        method: "/Store/Cities", target: "#CityId", loader: "#imgLoader"
      });

      $('#CityId').DropDownCascading({
        method: "/Store/Localities", target: "#LocalityId", loader: "#imgLoader"
      });

      $('#LocalityId').DropDownCascading({
        method: "/Store/Towns", target: "#TownId", loader: "#imgLoader"
      });

    }

    onload = function () {
      $.ajax({
        url: '/Store/CultureCode',
        type: 'post',
        data: { CountryId: $('#CountryId').val() },
        success: function (data) {
          $('#InstitutionalPhoneCulture').val(data);
          $('#InstitutionalPhoneCulture2').val(data);
        }
      });

      if ($('#InstitutionalGsmNumber').val() != "") {
        $('#trGsmType').show();
      }
      else {
        $('#trGsmType').hide();
      }

      var tabId = window.location.hash;

      var tabIdFullPath = window.location.host + window.location.pathname + window.location.search;
      if (tabIdFullPath.indexOf('target') > -1) {
        $('.Content').scrollTop(0);
        window.location.href = (window.location.pathname + window.location.search).replace('?target=', '#tabs-')
      }
      $('.Content').scrollTop(0);

      if (tabId == '#tabs-4') {
        $('#divIletisimAddressForm').html($('#divAddressForm').html());
        $('#AddressInsertType').val('1');
      }
      else if (tabId == '#tabs-5') {
        $('#divBayiiAddressForm').html($('#divAddressForm').html());
        $('#AddressInsertType').val('2');
      }
      else if (tabId == '#tabs-7') {
        $('#divServisAddressForm').html($('#divAddressForm').html());
        $('#AddressInsertType').val('3');
      }
      else if (tabId == '#tabs-8') {
        $('#divSubeAddressForm').html($('#divAddressForm').html());
        $('#AddressInsertType').val('4');
      }




      $.ajax({
        url: '/Member/CultureCode',
        type: 'post',
        data: { CountryId: $('#CountryId').val() },
        success: function (data) {
          $('#InstitutionalPhoneCulture').val(data);
          $('#InstitutionalPhoneCulture2').val(data);

          $('#InstitutionalGSMCulture').val(data);
          $('#InstitutionalGSMCulture2').val(data);

          $('#InstitutionalFaxCulture').val(data);
        },
        error: function (x) {
          alert(x.responseText);
        }
      });

      if ($('#CityId').val() != "0") {
        if ($('#CityId').val() == "34") {
          $('#AreaCodeTextBox').hide();
          $('#AreaCodeSelect').show();
          $('#AreaCodeTextBox2').hide();
          $('#AreaCodeSelect2').show();
          $('#AreaCodeTextBox3').hide();
          $('#AreaCodeSelect3').show();

          $('#InstitutionalPhoneAreaCode').val('212');
          $('#InstitutionalPhoneAreaCode2').val('212');
          $('#InstitutionalFaxAreaCode').val('212');

        }
        else {
          $('#AreaCodeTextBox').show();
          $('#AreaCodeSelect').hide();
          $('#AreaCodeTextBox2').show();
          $('#AreaCodeSelect2').hide();
          $('#AreaCodeTextBox3').show();
          $('#AreaCodeSelect3').hide();
        }
      }
      else {
        $('#AreaCodeTextBox').show();
        $('#AreaCodeSelect').hide();
        $('#AreaCodeTextBox2').show();
        $('#AreaCodeSelect2').hide();
        $('#AreaCodeTextBox3').show();
        $('#AreaCodeSelect3').hide();
      }
    }

    function DeletePicture(AddressId) {
      $.ajax({
        url: '/Store/DeletePicture',
        type: 'delete',
        data: { id: AddressId },
        success: function (data) {
          $('#divPictureList').html(data);
        },
        error: function (x, l, e) {

        }
      });
    }

    function imageResize(imageName, width) {
      if ($('#' + imageName)[0].width > width) {
        $('#' + imageName).attr('width', width);
      }
    }

  </script>
  <style type="text/css">
    #wrapper
    {
      width: 800px;
      margin-left: auto;
      margin-right: auto;
    }
    
    .accordionButton
    {
      width: 780px;
      float: left;
      border: 1px solid #d3d3d3;
      cursor: pointer;
      height: 25px;
      color: #000;
      font-size: 13px;
      padding-left: 20px;
      padding-top: 5px;
    }
    
    .accordionContent
    {
      width: 800px;
      float: left;
      display: none;
      border: 1px solid #d3d3d3;
      background-color: #ccc;
    }
  </style>
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
  <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <input id="AddressInsertType" type="hidden" />
  <div id="Loading">
    <div style="width: 66px; height: 20px; margin: 65px 0px 0px 160px; display: none;">
      Yükleniyor..
    </div>
  </div>
  <%using (Html.BeginPanel())
    { %>
  <table border="0" cellpadding="0" cellspacing="0" width="1000px" style="float: left;">
    <tr>
      <td>
        <div id="tabs" style="margin: 0px;">
          <ul>
            <li class="tabLi" style="display: none;"><a class="tabStore" href="#tabs-1" ref="nothing">
              Mağaza Bilgileri</a></li>
            <li class="tabLi" style="display: none;"><a class="tabStore" href="#tabs-2" ref="nothing">
              Üyelik Bilgileri</a></li>
            <li class="tabLi" style="display: none;"><a class="tabStore" href="#tabs-3" ref="divIletisimAddressForm">
              İletişim Bilgileri</a></li>
            <li class="tabLi" style="display: none;"><a class="tabStore" href="#tabs-4" ref="nothing">
              Faaliyet Tipleri</a></li>
            <li class="tabLi" style="display: none;"><a class="tabStore" href="#tabs-5" ref="nothing">
              Faaliyet Alanları</a></li>
          </ul>
          <% using (Html.BeginForm("Edit", "Store", FormMethod.Post, new { enctype = "multipart/form-data" }))
             { %>
          <div id="tabs-1">
            <%--     GENEL BİLGİLER--%>
            <div style="width: 520px; float: left;">
              <table border="0" cellpadding="5" cellspacing="0" style="margin-left: 20px;">
                <tr>
                  <td style="width: 150px;">
                    <%: Html.LabelFor(m => m.StoreNo)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%: Model.StoreNo%>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td style="width: 150px;">
                    <%: Html.LabelFor(m => m.StoreName)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%: Html.TextBoxFor(m => m.StoreName, new { style = "width: 250px;" })%>
                  </td>
                  <td>
                    <%: Html.ValidationMessageFor(m => m.StoreName)%>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.StoreWeb)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%: Html.TextBoxFor(m => m.StoreWeb, new { style = "width: 250px;" })%>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <a style="color: Blue;" href="http://<%:Model.StoreWeb %>" target="_blank">
                      <%:Model.StoreWeb %></a>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.StoreEMail)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%: Html.TextBoxFor(m => m.StoreEMail, new { style = "width: 250px;" })%>
                  </td>
                  <td>
                    <%: Html.ValidationMessageFor(m => m.StoreEMail)%>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.StoreCapital)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%:Html.DropDownListFor(m => m.StoreCapital, Model.StoreCapitalItems)%>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.StoreEstablishmentDate)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%: Html.TextBoxFor(m => m.StoreEstablishmentDate, new { style = "width: 94px;" })%>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.StoreType)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%:Html.DropDownListFor(m => m.StoreType, Model.StoreTypeItems, new { style = "width: 170px; "})%>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.StoreEmployeesCount)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%: Html.DropDownListFor(c => c.StoreEmployeesCount, Model.EmployeesCountItems, new { style = "width: 170px; " })%>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.StoreEndorsement)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%:Html.DropDownListFor(m => m.StoreEndorsement, Model.StoreEndorsementItems, new { style = "width: 170px; " })%>
                  </td>
                  <td align="left">
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.PacketId)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%: Html.DropDownListFor(m => m.PacketId, Model.StorePacketItems, new { style = "width: 170px; " })%>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.StorePacketBeginDate)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%: Html.TextBox("StorePacketBeginDate", Model.StorePacketBeginDate.ToString("dd.MM.yyyy") != "01.01.0001" ? Model.StorePacketBeginDate.ToString("dd.MM.yyyy") : "", new { style = "width: 94px;" })%>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.StorePacketEndDate)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%: Html.TextBox("StorePacketEndDate", Model.StorePacketEndDate.HasValue ? Model.StorePacketEndDate.Value.ToString("dd.MM.yyyy") : "", new { style = "width: 94px;" })%>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.StoreRecordDate)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%:Model.StoreRecordDate.ToString("dd.MM.yyyy")%>
                  </td>
                  <td>
                    <%: Html.ValidationMessageFor(m => m.StoreActiveType)%>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.PurchasingDepartmentName)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%: Html.TextBoxFor(m => m.PurchasingDepartmentName)%>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.PurchasingDepartmentEmail)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%: Html.TextBoxFor(m => m.PurchasingDepartmentEmail)%>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td valign="top">
                    <%: Html.LabelFor(m => m.StoreAbout)%>
                  </td>
                  <td valign="top">
                    :
                  </td>
                  <td>
                    <%: Html.TextAreaFor(m => m.StoreAbout, new { style = "width:350px; height : 60px;" })%>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.StoreActiveType)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%:Html.DropDownListFor(m => m.StoreActiveType, Model.StoreActiveTypeItems)%>
                  </td>
                  <td>
                    <%: Html.ValidationMessageFor(m => m.StoreActiveType)%>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.PacketStatu)%>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <% 
               bool inceleniyor = false;
               bool onaylandi = false;
               bool onaylanmadi = false;
               bool silindi = false;
               byte packetStatu = Model.PacketStatu;
               if (packetStatu == 0)
               {
                 inceleniyor = true;
               }
               else if (packetStatu == 1)
               {
                 onaylandi = true;
               }
               else if (packetStatu == 2)
               {
                 onaylanmadi = true;
               }
               else if (packetStatu == 3)
               {
                 silindi = true;
               }
                    %>
                    İnceleniyor&nbsp;<%: Html.RadioButton("PacketStatu", "0", inceleniyor)%>&nbsp;&nbsp;
                    Onaylandı&nbsp;<%: Html.RadioButton("PacketStatu", "1", onaylandi)%>&nbsp;&nbsp;
                    Onaylanmadı&nbsp;<%: Html.RadioButton("PacketStatu", "2", onaylanmadi)%>&nbsp;&nbsp;
                    Silindi&nbsp;<%: Html.RadioButton("PacketStatu", "3", silindi)%>&nbsp;&nbsp;
                  </td>
                  <td>
                    <%: Html.ValidationMessageFor(m => m.PacketStatu)%>
                  </td>
                </tr>
                <tr>
                  <td colspan="3">
                    <div style="margin: 20px auto; padding-top: 10px; border-top: solid 1px #DDD; width: 99%;
                      float: left">
                      <button type="submit">
                        Kaydet</button>
                      <button type="reset">
                        İptal</button>
                    </div>
                  </td>
                </tr>
              </table>
            </div>
            <div style="float: left; width: 400px; height: auto; margin-top: 40px;">
              <table>
                <tr>
                  <td valign="top" style="width: 150px;">
                    <%: Html.LabelFor(m => m.StoreLogo)%>
                  </td>
                  <td valign="top">
                    :
                  </td>
                  <td colspan="2">
                    <%: Html.FileUploadFor(m => m.StoreLogo, new { @class = "FileUpload", style = "width: 256px" })%><br />
                    <br />
                    <% if (!String.IsNullOrEmpty(Model.StoreLogo))
                       { %>
                    <% if (FileHelpers.HasFile("/UserFiles/Images/StoreLogo/" + FileHelpers.ImageThumbnailName(Model.StoreLogo)))
                       { %>
                    <img id="imageLogo" height="100" src="/UserFiles/Images/StoreLogo/<%= FileHelpers.ImageThumbnailName(Model.StoreLogo) %>"
                      align="left" style="margin-right: 5px;" />
                    <%--onload="imageResize('imageLogo', 550);" --%>
                    <% }
                       else
                       { %>
                    <%--onload="imageResize('imageLogo' , 550);" --%>
                    <img id="imageLogo" src="/UserFiles/Images/StoreLogo/<%= Model.StoreLogo %>" align="left"
                      style="margin-right: 5px;" height="100" />
                    <% } %>
                    <% } %>
                    <div id="deleteImage" style="margin-top: 5px">
                      <% Html.RenderPartial("ImageDelete",
               new ControlModel { ImageDeleted = false, Text = "", IsImage = Model.IsImage }); %>
                    </div>
                  </td>
                </tr>
              </table>
            </div>
          </div>
          <div id="tabs-2">
            <table border="0" cellpadding="5" cellspacing="0">
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.MemberNo)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%: Html.DisplayTextFor(m => m.MemberNo)%>
                </td>
                <td>
                </td>
              </tr>
              <% if (Model.MemberType == (byte)MemberType.Enterprise)
                 { %>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.MemberTitleType)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%:Html.DropDownListFor(c => c.MemberTitleType, Model.MemberTitleTypeItems)%>
                </td>
                <td>
                </td>
              </tr>
              <% } %>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.MemberNo)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%: Html.DisplayTextFor(m => m.MemberNo)%>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.MemberName)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%: Html.TextBoxFor(m => m.MemberName, new { style = "width: 250px;" })%>
                </td>
                <td>
                  <%: Html.ValidationMessageFor(m => m.MemberName)%>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.MemberSurname)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%: Html.TextBoxFor(m => m.MemberSurname, new { style = "width: 250px;" })%>
                </td>
                <td>
                  <%: Html.ValidationMessageFor(m => m.MemberSurname)%>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.MemberEmail)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%: Html.TextBoxFor(m => m.MemberEmail, new { style = "width: 250px;" })%>
                </td>
                <td>
                  <%: Html.ValidationMessageFor(m => m.MemberEmail)%>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.MemberPassword)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%: Html.TextBoxFor(m => m.MemberPassword, new { style = "width: 250px;" })%>
                </td>
                <td>
                  <%: Html.ValidationMessageFor(m => m.MemberPassword)%>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.Active)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <div style="width: auto; height: auto; float: left">
                    <div style="width: auto; height: auto; float: left; margin-top: 4px;">
                      Aktif
                    </div>
                    <div style="width: auto; height: auto; float: left; margin-left: 5px;">
                      <%: Html.RadioButton("Active", true)%></div>
                  </div>
                  <div style="width: auto; height: auto; float: left; margin-left: 10px;">
                    <div style="width: auto; height: auto; float: left; margin-top: 4px;">
                      Pasif
                    </div>
                    <div style="width: auto; height: auto; float: left; margin-left: 5px;">
                      <%: Html.RadioButton("Active", false)%></div>
                  </div>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.BirthDate)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%: Html.TextBox("BirthDate",Model.BirthDate!=null?Model.BirthDate.ToDateTime().ToString("dd.MM.yyyy"):"")%>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.ReceiveEmail)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%: Html.CheckBoxFor(m => m.ReceiveEmail)%>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td colspan="3">
                  <div style="margin: 20px auto; padding-top: 10px; border-top: solid 1px #DDD; width: 99%;
                    float: left">
                    <button type="submit">
                      Kaydet</button>
                    <button type="reset">
                      İptal</button>
                  </div>
                </td>
              </tr>
            </table>
          </div>
          <div id="tabs-3">
            <%--İLETİŞİM BİLGİLERİ--%> <div style="float: left; width: 800px;">
                <%:EnumModels.AddressEdit(Model.Address) %>
              </div>
              <div style="float: left; width: 800px;">
                <% foreach (var item in Model.PhoneItems)
                   { %>
                <% if (!string.IsNullOrWhiteSpace(item.PhoneNumber))
                   { %>
                <% string phoneType = string.Empty;
                   if (item.PhoneType == (byte)PhoneType.Fax)
                   {
                     phoneType = "Fax :";
                   }
                   else if (item.PhoneType == (byte)PhoneType.Gsm)
                   {
                     phoneType = "Gsm :";
                   }
                   else if (item.PhoneType == (byte)PhoneType.Phone)
                   {
                     phoneType = "Phone :";
                   }
                %>
                <%:phoneType %>
                +90&nbsp;<%:item.PhoneAreaCode%>&nbsp;<%:item.PhoneNumber%><br />
                <% } %>
                <% } %>
              </div>
            <table cellpadding="0" cellspacing="0">
              <tr>
                <td>
                </td>
                <td valign="top" style="height: 20px; width: 100px;">
                </td>
              </tr>
              <tr>
                <td align="right" style="padding-right: 6px;">
                  <%= Html.LabelFor(model => model.AddressTypeId)%>*
                </td>
                <td style="height: 30px;">
                  <div id="divAddressType" class="dropdownAddress">
                    <%:Html.DropDownListFor(model => model.AddressTypeId, Model.AddressTypeItems, new { @class = "small_input", style = "width : 203px; height: 18px; font-size: 11px; margin: 1px; margin-right: 2px;" })%></div>
                </td>
              </tr>
              <%   var dataAddress = new NeoSistem.MakinaTurkiye.Data.Address(); %>
              <tr>
                <td align="right" style="padding-right: 6px;">
                  <%:Html.LabelFor(model => model.CountryId)%>*
                </td>
                <td style="height: 30px;">
                  <div id="divCountryId" class="dropdownAddress">
                    <%:Html.DropDownListFor(model => model.CountryId, Model.CountryItems, new { @class = "small_input", style = "width : 203px; height: 18px; font-size: 11px; margin: 1px; margin-right: 2px;" })%></div>
                </td>
              </tr>
              <tr>
                <td align="right" style="padding-right: 6px;">
                  <%:Html.LabelFor(model => model.CityId)%>*
                </td>
                <td style="height: 30px;">
                  <div id="divCityId" class="dropdownAddress">
                    <%:Html.DropDownListFor(model => model.CityId, Model.CityItems, new { @class = "small_input", style = "width : 203px; height: 18px; font-size: 11px; margin: 1px; margin-right: 2px;" })%></div>
                </td>
              </tr>
              <tr>
                <td align="right" style="padding-right: 6px;">
                  <%:Html.LabelFor(model => model.LocalityId)%>*
                </td>
                <td style="height: 30px;">
                  <%: Html.DropDownListFor(model => model.LocalityId, Model.LocalityItems, new { @class = "small_input", style = "width : 203px; height: 18px; font-size: 11px; margin: 1px; margin-right: 2px;" })%>
                </td>
              </tr>
              <tr>
                <td align="right" style="padding-right: 6px;">
                  <%:Html.LabelFor(model => model.TownId)%>*
                </td>
                <td style="height: 30px;">
                  <%: Html.DropDownListFor(model => model.TownId, Model.TownItems, new { @class = "small_input", style = "width : 203px; height: 18px; font-size: 11px; margin: 1px; margin-right: 2px;" })%>
                </td>
              </tr>
              <tr>
                <td align="right" style="padding-right: 6px; padding-top: 5px" valing="top">
                  <label id="labelCadde">
                    Cadde</label>
                  <label style="display: none;" id="labelAdres">
                    Adres</label>
                </td>
                <td style="padding-top: 5px;">
                  <%:Html.TextAreaFor(model => model.AvenueOtherCountries, new { style="width: 185px; height: 70px; margin-top: 5px; display: none;"})%>
                  <%:Html.TextBoxFor(model => model.Avenue, new { @class = "big_input" })%>
                </td>
              </tr>
              <tr>
                <td align="right" style="padding-right: 6px; padding-top: 5px;">
                  <%:Html.LabelFor(model => model.Street)%>
                </td>
                <td style="padding-top: 3px;">
                  <%:Html.TextBoxFor(model => model.Street, new { @class = "big_input" })%>
                </td>
              </tr>
              <tr>
                <td align="right" style="padding-right: 6px; padding-top: 5px">
                  <%:Html.LabelFor(model => model.ApartmentNo)%>
                </td>
                <td style="padding-top: 5px">
                  <%:Html.TextBoxFor(model => model.ApartmentNo, new { style = "width: 50px;" })%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  <%:Html.LabelFor(model => model.DoorNo)%>
                  <%:Html.TextBoxFor(model => model.DoorNo, new { style = "width: 58px;"})%>
                </td>
              </tr>
            </table>
            <%= Html.RenderHtmlPartial("PhoneItem", Model.PhoneItems) %>
            <div style="margin: 20px auto; padding-top: 10px; border-top: solid 1px #DDD; width: 99%;
              float: left">
              <button type="submit">
                Kaydet</button>
              <button type="reset">
                İptal</button>
            </div>
          </div>
          <div id="tabs-4">
            <div style="width: 390px; height: 170px; border: solid 1px #b0afaf; padding-left: 20px;
              padding-top: 25px;">
              <% foreach (var item in Model.ActivityTypeItems)
                 { %>
              <div style="width: 130px; height: 25px; margin-top: 10px; float: left;">
                <div style="float: left; width: auto; height: auto; float: left">
                  <% bool checkStore = false; %>
                  <% foreach (var itemStore in Model.StoreActivityTypeItems)
                     { %>
                  <% if (itemStore.ActivityTypeId == item.ActivityTypeId)
                     { %>
                  <% checkStore = true; %>
                  <% } %>
                  <% } %>
                  <%:Html.CheckBox("ActivityTypeIdItems", checkStore, new { value = item.ActivityTypeId })%>
                </div>
                <div style="float: left; width: auto; height: auto; float: left; margin-left: 4px;
                  margin-top: 4px;">
                </div>
                <%:item.ActivityName%>
              </div>
              <% } %>
            </div>
            <div style="margin: 20px auto; padding-top: 10px; border-top: solid 1px #DDD; width: 99%;
              float: left">
              <button type="submit">
                Kaydet</button>
              <button type="reset">
                İptal</button>
            </div>
          </div>
          <div id="tabs-5">
            <div style="width: 570px; height: auto; float: left;">
              <% foreach (var item in Model.SectorItems.OrderBy(c => c.CategoryName).OrderBy(c => c.CategoryOrder))
                 { %>
              <div style="width: 400px; float: left; margin-left: 10px;">
                <div style="width: 14px; height: 14px; background-image: url('/Content/Images/plus.png');
                  float: left; cursor: pointer;" id="plus<%: item.CategoryId %>" onclick="openContent('#plusContent<%: item.CategoryId %>');">
                </div>
                <div style="width: 250px; min-height: 27px; margin-left: 10px; float: left;">
                  <span style="font-size: 12px; cursor: pointer;">
                    <%=item.CategoryName%>
                  </span>
                  <br />
                  <div id="plusContent<%: item.CategoryId %>" style="width: 375px; min-height: 27px;
                    margin-left: 10px; float: left; display: none;" class="plusContent">
                    <ul style="list-style: none; float: left; padding: 0px; width: 730px; border: solid 1px #8fc0d1;
                      padding-top: 10px; padding-bottom: 10px; padding-left: 10px; background-color: #f8fbff;">
                      <% foreach (var item2 in Model.CategoryGroupParentItemsByCategoryId(item.CategoryId).OrderBy(c => c.CategoryName).OrderBy(c => c.CategoryOrder))
                         { %>
                      <li style="float: left; width: 240px;">
                        <div style="width: auto; height: auto; float: left;">
                          <% bool checkRelated = false; %>
                          <% foreach (var itemRelated in Model.StoreActivityCategory)
                             { %>
                          <% if (itemRelated.CategoryId == item2.CategoryId)
                             { %>
                          <% checkRelated = true; %>
                          <script type="text/javascript">
                            $('#plusContent<%: item.CategoryId %>').show();
                          </script>
                          <% } %>
                          <% } %>
                          <%:Html.CheckBox("StoreActivityCategory", checkRelated, new { value = item2.CategoryId })%>
                        </div>
                        <div style="width: auto; height: auto; float: left; margin-left: 3px; margin-top: 4px;">
                          <%: item2.CategoryName%></div>
                      </li>
                      <% } %>
                    </ul>
                  </div>
                </div>
              </div>
              <% } %>
            </div>
            <div style="margin: 20px auto; padding-top: 10px; border-top: solid 1px #DDD; width: 99%;
              float: left">
              <button type="submit">
                Kaydet</button>
              <button type="reset">
                İptal</button>
            </div>
          </div>
          <% } %>
        </div>
      </td>
    </tr>
  </table>
  <%} %>
</asp:Content>
