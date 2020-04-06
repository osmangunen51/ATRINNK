﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MemberModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  IndividualEdit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
  <script src="/Scripts/JQuery-dropdowncascading.js" type="text/javascript"></script>
  <script type="text/javascript">
    $(document).ready(function () {
      $('#tabs').tabs();
      $('.tabLi').show();

      $('.ui-widget-header').addClass('ui-state-default');
      $('div.ui-tabs').removeClass('ui-widget-content');

      $(document).ready(function () {
        $('#BirthDate').datepicker();
      });

    });

    function openContent(content) {
      $(content).slideToggle();
    }
  </script>
  <script type="text/javascript">
    $(document).ready(function () {

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
      }); 

    onload = function () {
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

  </script>
  <script type="text/javascript">

    $(document).ready(function () {

      $('#CountryId').DropDownCascading({
        method: "/Member/Cities", target: "#CityId", loader: "#imgLoader"
    });
  

      $('#CityId').DropDownCascading({
        method: "/Member/Localities", target: "#LocalityId", loader: "#imgLoader"
      });

      $('#LocalityId').DropDownCascading({
        method: "/Member/Towns", target: "#TownId", loader: "#imgLoader"
      });

      $('#CityId').change(function () {
        $.ajax({
          url: '/Member/AreaCode',
          type: 'post',
          data: { CityId: $('#CityId').val() },
          success: function (data) {
            $('#PhoneCulture').val(data);
            $('#PhoneCulture').val(data);
          }
        });
      });

    });
  </script>
  <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <div style="float: left; width: 800px; margin-top: 10px;">
             
    <%using (Html.BeginPanel())
      { %>
    <table border="0" cellpadding="0" cellspacing="0" width="900px" style="float: left">
      <tr>
        <td>
          <div id="tabs" style="margin: 0px;">
            <% using (Html.BeginForm())
               { %>
            <ul>
              <li><a href="#tabs-1">Genel Bilgiler</a></li>
              <% if (Model.MemberType > (byte)MemberType.FastIndividual)
                 { %>
              <li><a href="#tabs-2">İletişim Bilgileri</a></li>
              <% } %>
              <li><a href="#tabs-3">İlgilendiği Sektörler</a></li>
            </ul>
            <div id="tabs-1">
  
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
                  <%if(Model.MemberType==(byte)MemberType.FastIndividual){ %>
                  <tr>
                      <td>GSM</td>
                      <td>:</td>
                      <td><%var phoneGsm = Model.PhoneItems.Where(x => x.PhoneType == (byte)PhoneType.Gsm).FirstOrDefault(); %>
                          <%if(phoneGsm!=null){ %>
                          <%:phoneGsm.PhoneCulture+" "+phoneGsm.PhoneAreaCode+" "+phoneGsm.PhoneNumber %>
                          <%} %>
                      </td>
                  </tr>
                    <%} %>
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
                    <%: Html.TextBox("BirthDate", Model.BirthDate.ToString("dd.MM.yyyy"))%>
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
              </table>
            </div>
            <% if (Model.MemberType > (byte)MemberType.FastIndividual)
               { %>
            <div id="tabs-2">
              <div style="float: left; width: 800px;">
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
              <div style="float: left;width:600px">
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
                      <%: Html.DropDownListFor(model => model.LocalityId, Model.LocalityItems, new { style = "font-size: 12px; width: 203px;" })%>
                    </td>
                  </tr>
                 
                  <tr>
                    <td align="right" style="padding-right: 6px;">
                      <%:Html.LabelFor(model => model.TownId)%>*
                    </td>
                    <td style="height: 30px;">
                      <%: Html.DropDownListFor(model => model.TownId, Model.TownItems, new { style = "font-size: 12px; width: 203px;" })%>
                    </td>
                  </tr>
                  <tr>
                    <td align="right" style="padding-right: 6px; padding-top: 5px">
                      <%:Html.LabelFor(model => model.Avenue)%>
                    </td>
                    <td style="padding-top: 5px;">
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
                      <%:Html.TextBoxFor(model => model.ApartmentNo, new { style = "width: 50px;" })%>&nbsp;&nbsp;
                      <%:Html.LabelFor(model => model.DoorNo)%>
                      <%:Html.TextBoxFor(model => model.DoorNo, new { style = "width: 58px;"})%>
                    </td>
                  </tr>
                </table>
              </div>
              <style type="text/css">
                input.membershipValid, textarea.membershipValid
                {
                  border: 1px solid red;
                  border-top: 2px solid red;
                }
                .dropdownAddress
                {
                  width: auto;
                  height: 21px;
                  margin-top: 3px;
                  float: left;
                  border: #c9e6e2 1px solid;
                  border-top: #c9e6e2 2px solid;
                  margin-left: 4px;
                }
                .validationdropdownAddress
                {
                  width: auto;
                  height: 21px;
                  margin-top: 3px;
                  float: left;
                  border: 1px solid red;
                  border-top: 2px solid red;
                  margin-left: 4px;
                }
              </style>
              <script type="text/javascript">
                function Check() {
                  var checkAddressType = true;
                  var checkCountry = true;
                  var checkCity = true;
                  var checkLocality = true;
                  var checkDistrict = true;
                  var checkTown = true;

                  if ($('#MembershipModel_AddressTypeId').val() == "0") {
                    $('#divAddressType').attr('class', 'validationdropdownAddress');
                    checkAddressType = false;
                  }
                  else {
                    $('#divAddressType').attr('class', 'dropdownAddress');
                    checkAddressType = true;
                  }

                  if ($('#MembershipModel_CountryId').val() == 246) {
                    checkCountry = false;
                    checkCity = false;
                    checkLocality = false;
                    checkDistrict = false;
                    checkTown = false;


                    if ($('#MembershipModel_CountryId').val() == "0") {
                      $('#divCountryId').attr('class', 'validationdropdownAddress');
                      checkCountry = false;
                    }
                    else {
                      $('#divCountryId').attr('class', 'dropdownAddress');
                      checkCountry = true;
                    }

                    if ($('#MembershipModel_CityId').val() == "0") {
                      $('#divCityId').attr('class', 'validationdropdownAddress');
                      checkCity = false;
                    }
                    else {
                      $('#divCityId').attr('class', 'dropdownAddress');
                      checkCity = true;
                    }

                    if ($('#MembershipModel_LocalityId').val() == "0") {
                      $('#divLocalityId').attr('class', 'validationdropdownAddress');
                      checkLocality = false;
                    }
                    else {
                      $('#divLocalityId').attr('class', 'dropdownAddress');
                      checkLocality = true;
                    }
                     

                    if ($('#MembershipModel_TownId').val() == "0") {
                      $('#divTownId').attr('class', 'validationdropdownAddress');
                      checkTown = false;
                    }
                    else {
                      $('#divTownId').attr('class', 'dropdownAddress');
                      checkTown = true;
                    }
                  }


                  if (checkAddressType && checkCountry && checkCity && checkLocality && checkDistrict && checkTown) {
                    return true;
                  }
                  window.location.href = '#date'
                  return false;
                }
              </script>
              <%= Html.RenderHtmlPartial("PhoneItem", Model.PhoneItems) %>
            </div>
            <% } %>
            <div id="tabs-3">
              <div style="width: 570px; height: auto; float: left;">
                <% foreach (var item in Model.SectorItems.Where(c=>c.MainCategoryType==(byte)MainCategoryType.Ana_Kategori).OrderBy(c => c.CategoryName).OrderBy(c => c.CategoryOrder))
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
                        <% foreach (var item2 in Model.ParentItems(item.CategoryId).OrderBy(c => c.CategoryName).OrderBy(c => c.CategoryOrder))
                           { %>
                        <li style="float: left; width: 240px;">
                          <div style="width: auto; height: auto; float: left;">
                            <% bool checkRelated = false; %>
                            <% foreach (var itemRelated in Model.MainPartyRelatedSectorItems)
                               { %>
                            <% if (itemRelated.CategoryId == item2.CategoryId)
                               { %>
                            <% checkRelated = true; %>
                            <script type="text/javascript">
                              $('#plusContent<%: item.CategoryId %>').show();
                            </script>
                            <% } %>
                            <% } %>
                            <%:Html.CheckBox("MemberRelatedSectorIdItems", checkRelated, new { value = item2.CategoryId })%>
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
            </div>
            <div style="margin: 20px auto; float: left; padding-top: 10px; border-top: solid 1px #DDD;
              width: 99%;">
              <button type="submit">
                Kaydet</button>
              <button type="reset">
                İptal</button></div>
            <% } %>
          </div>
        </td>
      </tr>
    </table>
    <%} %>
  </div>
</asp:Content>
