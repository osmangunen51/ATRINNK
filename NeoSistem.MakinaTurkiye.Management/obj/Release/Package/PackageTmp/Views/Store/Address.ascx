﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StoreModel>" %>
<script src="/Scripts/JQuery-dropdowncascading.js" type="text/javascript"></script>
<table cellpadding="0" cellspacing="0">
  <tr>
    <td align="right" style="width:128px;">
      <%:Html.LabelFor(model => model.CountryId)%>
    </td>
    <td style="height: 30px;">
      <div id="divCountryId" class="dropdownAddress">
        <%:Html.DropDownListFor(model => model.CountryId, Model.CountryItems, new { @class = "textMedium", style = "width : 203px; height: 18px; font-size: 11px; border: none; margin: 1px; margin-right: 2px;" })%></div>
    </td>
  </tr>
  <tr>
    <td align="right">
      <%:Html.LabelFor(model => model.CityId)%>
    </td>
    <td style="height: 30px;">
      <div id="divCityId" class="dropdownAddress">
        <%:Html.DropDownListFor(model => model.CityId, Model.CityItems, new { @class = "textMedium", style = "width : 203px; height: 18px; font-size: 11px; border: none; margin: 1px; margin-right: 2px;" })%></div>
    </td>
  </tr>
  <tr>
    <td align="right">
      <%:Html.LabelFor(model => model.LocalityId)%>
    </td>
    <td style="height: 30px;">
      <div id="divLocalityId" class="dropdownAddress">
        <%:Html.DropDownListFor(model => model.LocalityId, Model.LocalityItems, new { @class = "textMedium", style = "width : 203px; height: 18px; font-size: 11px; border: none; margin: 1px; margin-right: 2px;" })%></div>
    </td>
  </tr>
  <tr>
    <td align="right">
      <%:Html.LabelFor(model => model.TownId)%>
    </td>
    <td style="height: 30px;">
      <div id="divTownId" class="dropdownAddress">
        <%:Html.DropDownListFor(model => model.TownId, Model.TownItems, new { @class = "textMedium", style = "width : 203px; height: 18px; font-size: 11px; border: none; margin: 1px; margin-right: 2px;" })%></div>
    </td>
  </tr>
  <tr>
    <td align="right">
      <label>
        Posta Kodu</label>
    </td>
    <td style="height: 30px; padding-left: 4px;">
      <%:Html.TextBox("ZipCode", "", new { @class = "textMedium", disabled = "@disabled" })%>
    </td>
  </tr>
  <tr>
    <td align="right">
      <label>
        Cadde</label>
    </td>
    <td style="height: 30px; padding-left: 4px;">
      <%:Html.TextBoxFor(model => model.Avenue, new { @class = "textMedium" })%>
    </td>
  </tr>
  <tr>
    <td align="right">
      <%:Html.LabelFor(model => model.Street)%>
    </td>
    <td style="height: 30px; padding-left: 4px;">
      <%:Html.TextBoxFor(model => model.Street, new { @class = "textMedium" })%>
    </td>
  </tr>
  <tr>
    <td align="right">
      <%:Html.LabelFor(model => model.ApartmentNo)%>
    </td>
    <td style="height: 30px; padding-left: 4px;">
      <div style="float: left;">
        <%:Html.TextBoxFor(model => model.ApartmentNo, new { maxLength = "6", @class = "textMedium" })%>
      </div>
      <div style="float: left; margin-left: 4px; padding-top: 3px;">
        <%:Html.LabelFor(model => model.DoorNo)%>
      </div>
      <div style="float: left; margin-left: 4px">
        <%:Html.TextBoxFor(model => model.DoorNo, new { maxLength = "4", @class = "textSmall", style = "padding-left:8px" })%>
      </div>
    </td>
  </tr>
  <tr>
    <td>
    </td>
    <td>
      <span style="font-size: 12px;">* En az bir telefon numarası giriniz.</span>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <div style="width: 350px; height: 35px; margin-left: 51px;">
        <div style="width: 77px; height: 25px; text-align: right; float: left;">
          <span style="font-size: 12px; color: #000">
            <%:Html.LabelFor(model => model.InstitutionalPhoneNumber)%></span> :
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <%:Html.TextBoxFor(model => model.InstitutionalPhoneCulture, new { @class = "textSmall", style = "width: 30px;", validate = "required:true" })%>
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <div id="AreaCodeTextBox">
            <%:Html.TextBox("TextInstitutionalPhoneAreaCode", Model.InstitutionalPhoneAreaCode, new { @class = "textSmall", style = "width: 30px;" })%>
          </div>
          <div id="AreaCodeSelect" class="dropdownAddress" style="margin-left: 2px; margin-top: 0px;">
            <select id="DropDownInstitutionalPhoneAreaCode" name="DropDownInstitutionalPhoneAreaCode"
              style="font-size: 11px; border: none; padding-top: 1px; height: 19px">
              <option value="0">Seç</option>
              <option value="212">212</option>
              <option value="216">216</option>
            </select>
          </div>
          <input type="hidden" id="InstitutionalPhoneAreaCode" name="InstitutionalPhoneAreaCode" />
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <%:Html.TextBoxFor(model => model.InstitutionalPhoneNumber, new { @class = "textSmall", style = "width: 86px;", validate = "required:true" })%>
        </div>
      </div>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <div style="width: 350px; height: 35px; margin-left: 51px;">
        <div style="width: 77px; height: 25px; text-align: right; float: left;">
          <span style="font-size: 12px; color: #000;">
            <%:Html.LabelFor(model => model.InstitutionalPhoneNumber2)%></span> :
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <%:Html.TextBoxFor(model => model.InstitutionalPhoneCulture2, new { @class = "textSmall", style = "width: 30px;" })%>
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <div id="AreaCodeTextBox2">
            <%:Html.TextBox("TextInstitutionalPhoneAreaCode2", Model.InstitutionalPhoneAreaCode2, new { @class = "textSmall", style = "width: 30px;" })%>
          </div>
          <div id="AreaCodeSelect2" class="dropdownAddress" style="margin-left: 2px; margin-top: 0px;">
            <select id="DropDownInstitutionalPhoneAreaCode2" name="DropDownInstitutionalPhoneAreaCode2"
              style="font-size: 11px; border: none; padding-top: 1px; height: 19px">
              <option value="0">Seç</option>
              <option value="212">212</option>
              <option value="216">216</option>
            </select>
          </div>
          <input type="hidden" id="InstitutionalPhoneAreaCode2" name="InstitutionalPhoneAreaCode2" />
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <%:Html.TextBoxFor(model => model.InstitutionalPhoneNumber2, new { @class = "textSmall", style = "width: 86px;" })%>
        </div>
      </div>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <div style="width: 350px; height: 35px; margin-left: 51px;">
        <div style="width: 78px; height: 25px; text-align: right; float: left;">
          <%:Html.LabelFor(model => model.InstitutionalFaxNumber)%>
          :
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <%:Html.TextBoxFor(model => model.InstitutionalFaxCulture, new { @class = "textSmall", style = "width: 30px;" })%>
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <div id="AreaCodeTextBox3">
            <%:Html.TextBox("TextInstitutionalFaxAreaCode", Model.InstitutionalFaxAreaCode, new { @class = "textSmall", style = "width: 30px;" })%>
          </div>
          <div id="AreaCodeSelect3" class="dropdownAddress" style="margin-left: 2px; margin-top: 0px;
            padding-top: 1px; height: 19px">
            <select id="DropDownInstitutionalFaxAreaCode" name="DropDownInstitutionalFaxAreaCode"
              style="height: 18px; font-size: 11px; border: none;">
              <option value="0">Seç</option>
              <option value="212">212</option>
              <option value="216">216</option>
            </select>
          </div>
          <input type="hidden" id="InstitutionalFaxAreaCode" name="InstitutionalFaxAreaCode" />
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <%:Html.TextBoxFor(model => model.InstitutionalFaxNumber, new { @class = "textSmall", style = "width: 86px;" })%>
        </div>
      </div>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <div id="divGsm" style="width: 350px; height: 35px; margin-left: 51px;">
        <div style="width: 78px; height: 25px; text-align: right; float: left;">
          <%:Html.LabelFor(model => model.InstitutionalGSMNumber)%>
          :
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <%:Html.TextBoxFor(model => model.InstitutionalGSMCulture, new { @class = "textSmall", style = "width: 30px;" })%>
        </div>
        <div id="AreaCodeSelect4" class="dropdownAddress" style="margin-left: 4px; margin-top: 0px;
          padding-top: 1px; height: 19px">
          <select style="height: 18px; font-size: 11px; border: none;" id="InstitutionalGSMAreaCode"
            name="InstitutionalGSMAreaCode">
            <option value="0">Seç</option>
            <option value="530">530</option>
            <option value="531">531</option>
            <option value="532">532</option>
            <option value="533">533</option>
            <option value="534">534</option>
            <option value="535">535</option>
            <option value="536">536</option>
            <option value="537">537</option>
            <option value="538">538</option>
            <option value="539">539</option>
            <option value="541">541</option>
            <option value="542">542</option>
            <option value="543">543</option>
            <option value="544">544</option>
            <option value="545">545</option>
            <option value="546">546</option>
            <option value="547">547</option>
            <option value="548">548</option>
            <option value="549">549</option>
            <option value="501">501</option>
            <option value="505">505</option>
            <option value="506">506</option>
            <option value="507">507</option>
            <option value="551">551</option>
            <option value="552">552</option>
            <option value="553">553</option>
            <option value="554">554</option>
            <option value="555">555</option>
          </select>
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <%:Html.TextBoxFor(model => model.InstitutionalGSMNumber, new { @class = "textSmall", style = "width: 86px;" })%>
        </div>
      </div>
    </td>
  </tr>
  <tr id="trGsmType" style="display: none;">
    <td align="right">
     <span style="font-size:12px;">Operatör:*</span> &nbsp;
    </td>
    <td>
      <%=Html.RadioButton("GsmType", 1, false, new { style = "height: 11px", id = "gsmVodafone"})%>
      <span style="font-size:12px;">Vodafone</span>
      <%=Html.RadioButton("GsmType", 3, false, new { style = "height: 11px", id = "gsmTurkcell"  })%><span style="font-size:12px;">Turkcell</span>
      <%=Html.RadioButton("GsmType", 2, false, new { style = "height: 11px", id = "gsmAvea" })%><span style="font-size:12px;">Avea</span>
    </td>
  </tr>
  <tr id="trOperatorUyari" style="display: none;">
    <td align="right">
    </td>
    <td>
      <span style="font-size: 12px; color: Red;">Operatör seçimi yapmalısınız.</span>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <div class="sep_short">
      </div>
    </td>
  </tr>
</table>
