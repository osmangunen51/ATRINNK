﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  MemberStore
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%=Html.RenderHtmlPartial("Style") %>
  <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
  <script src="/Scripts/JQuery-dropdowncascading.js" type="text/javascript"></script>
  <script type="text/javascript">

    $(document).ready(function () {
      RegisterDropDownControls();

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
//              $('#InstitutionalPhoneAreaCode').val('212');
//              $('#InstitutionalPhoneAreaCode2').val('212');
//              $('#InstitutionalFaxAreaCode').val('212');
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

//            $('#InstitutionalPhoneAreaCode').val('212');
//            $('#InstitutionalPhoneAreaCode2').val('212');
//            $('#InstitutionalFaxAreaCode').val('212');
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
            $('#InstitutionalWGSMCulture').val(data);
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

      $('#BirthDate').datepicker();

      $('#InstitutionalGsmNumber').keyup(function () {
        if ($(this).val() != "") {
          $('#trGsmType').show();
        }
        else {
          $('#trGsmType').hide();
        }
      });

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

//          $('#InstitutionalPhoneAreaCode').val('212');
//          $('#InstitutionalPhoneAreaCode2').val('212');
//          $('#InstitutionalFaxAreaCode').val('212');

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

  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginPanel())
    { %>
  <%=Html.RenderHtmlPartial("TabMenu") %>
  <% using (Html.BeginForm("EditCommunication", "Store", FormMethod.Post))
     { %>
  <div style="width: 800px; float: left; margin: 20px 0px 0px 20px">
    <div style="float: left; width: 800px;">
      <%:EnumModels.AddressEdit(Model.Address) %>
    </div>
    <div style="float: left; width: 800px;">
      <% foreach (var item in Model.PhoneItems)
         { %>
      <% if (!string.IsNullOrWhiteSpace(item.PhoneNumber))
         { %>
      <% string phoneType = string.Empty;
          bool whatsappActive=true;
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
          else if(item.PhoneType==(byte)PhoneType.Whatsapp)
          {
              phoneType = "Whatsapp";
    

          }
      %>
      <%:phoneType %>
      <%:item.PhoneCulture%>&nbsp;<%:item.PhoneAreaCode%>&nbsp;<%:item.PhoneNumber%><br />
  
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
          <%:Html.TextBoxFor(model => model.ApartmentNo, new { style = "width: 50px;" })%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <%:Html.LabelFor(model => model.DoorNo)%>
          <%:Html.TextBoxFor(model => model.DoorNo, new { style = "width: 58px;"})%>
        </td>
      </tr>
    </table>
    <%= Html.RenderHtmlPartial("PhoneItem", Model.PhoneItems) %>
      <div style="margin-left:80px;">
           
        <%:Html.CheckBoxFor(x=>x.IsWhatsappNotUsing) %> Whatsapp Kullanmıyor
      
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
  <%} %>
</asp:Content>
