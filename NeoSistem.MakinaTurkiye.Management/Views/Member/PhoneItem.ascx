<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Phone>>" %>
<%   
  string InstitutionalPhoneAreaCode = "";
  string InstitutionalPhoneAreaCode2 = "";
  string InstitutionalPhoneCulture = "";
  string InstitutionalPhoneCulture2 = "";
  string InstitutionalPhoneNumber = "";
  string InstitutionalPhoneNumber2 = "";

  string InstitutionalGSMAreaCode = "";
  string InstitutionalGSMCulture = "";
  string InstitutionalGSMNumber = "";

  string InstitutionalFaxNumber = "";
  string InstitutionalFaxCulture = "";
  string InstitutionalFaxAreaCode = "";

  var phoneItems = Model.Where(c => c.PhoneType == (byte)PhoneType.Phone);
  if (phoneItems.Count() > 0)
  {
    InstitutionalPhoneAreaCode = phoneItems.First().PhoneAreaCode;
    InstitutionalPhoneCulture = phoneItems.First().PhoneCulture;
    InstitutionalPhoneNumber = phoneItems.First().PhoneNumber;
    if (phoneItems.Count() > 1)
    {
      InstitutionalPhoneAreaCode2 = phoneItems.Last().PhoneAreaCode;
      InstitutionalPhoneCulture2 = phoneItems.Last().PhoneCulture;
      InstitutionalPhoneNumber2 = phoneItems.Last().PhoneNumber;
    }
  }
  var gsmItem = Model.FirstOrDefault(c => c.PhoneType == (byte)PhoneType.Gsm);
  if (gsmItem != null)
  {
    InstitutionalGSMAreaCode = gsmItem.PhoneAreaCode;
    InstitutionalGSMCulture = gsmItem.PhoneCulture;
    InstitutionalGSMNumber = gsmItem.PhoneNumber;
  }
  var faxItem = Model.FirstOrDefault(c => c.PhoneType == (byte)PhoneType.Fax);
  if (faxItem != null)
  {
    InstitutionalFaxAreaCode = faxItem.PhoneAreaCode;
    InstitutionalFaxCulture = faxItem.PhoneCulture;
    InstitutionalFaxNumber = faxItem.PhoneNumber;
  }
%><table>
  <tr>
    <td colspan="2">
      <div style="width: 508px; height: 35px; margin-top: 5px; margin-left: 10px">
        <div style="width: 60px; height: 25px; text-align: left; float: left;">
          <span style="font-size: 12px; color: #4c4c4c">Telefon(1)</span> :
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 7px;">
          <input type="text" name="InstitutionalPhoneCulture" id="InstitutionalPhoneCulture"
            value="<%:InstitutionalPhoneCulture %>" class="small_input" style="width: 30px;
            height: 17px; padding-top: 3px; font-size: 12px;" />
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 5px;">
          <div id="AreaCodeTextBox">
            <input type="text" name="TextInstitutionalPhoneAreaCode" id="TextInstitutionalPhoneAreaCode"
              value="<%:InstitutionalPhoneAreaCode %>" class="small_input" style="width: 39px;
              height: 17px; padding-top: 3px; font-size: 12px;" />
          </div>
          <div id="AreaCodeSelect" style="width: auto; height: 23px; float: left; border: #c9e6e2 1px solid;
            border-top: #c9e6e2 2px solid">
            <select id="DropDownInstitutionalPhoneAreaCode" name="DropDownInstitutionalPhoneAreaCode"
              style="height: 18px; font-size: 11px; border: none; margin: 2px;">
              <%
                string selected0 = "";
                string selected212 = "";
                string selected216 = "";
                if (InstitutionalPhoneAreaCode == "212")
                  selected212 = "selected=\"selected\"";
                else if (InstitutionalPhoneAreaCode == "216")
                  selected216 = "selected=\"selected\"";
                else
                  selected0 = "selected=\"selected\"";
              %>
              <option value="0" <%:selected0 %>>Seç</option>
              <option value="212" <%:selected212 %>>212</option>
              <option value="216" <%:selected216 %>>216</option>
            </select>
          </div>
          <input type="hidden" id="InstitutionalPhoneAreaCode" name="InstitutionalPhoneAreaCode"
            value="<%:InstitutionalPhoneAreaCode %>" />
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <input type="text" name="InstitutionalPhoneNumber" id="InstitutionalPhoneNumber"
            value="<%:InstitutionalPhoneNumber  %>" class="small_input" style="width: 75px;
            height: 17px; padding-top: 3px; font-size: 12px;" />
        </div>
      </div>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <div style="width: 508px; height: 35px; margin-top: 5px; margin-left: 10px;">
        <div style="width: 60px; height: 25px; text-align: right; float: left;">
          <span style="font-size: 12px; color: #4c4c4c">Telefon(2)</span> :
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 7px;">
          <input type="text" name="InstitutionalPhoneCulture2" id="InstitutionalPhoneCulture2"
            value="<%:InstitutionalPhoneCulture2  %>" class="small_input" style="width: 30px;
            height: 17px; padding-top: 3px; font-size: 12px;" />
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 5px;">
          <div id="AreaCodeTextBox2">
            <input type="text" name="TextInstitutionalPhoneAreaCode2" id="TextInstitutionalPhoneAreaCode2"
              value="<%: InstitutionalPhoneAreaCode2 %>" class="small_input" style="width: 39px;
              height: 17px; padding-top: 3px; font-size: 12px;" />
          </div>
          <div id="AreaCodeSelect2" style="width: auto; height: 23px; float: left; border: #c9e6e2 1px solid;
            border-top: #c9e6e2 2px solid">
            <select id="DropDownInstitutionalPhoneAreaCode2" name="DropDownInstitutionalPhoneAreaCode2"
              style="height: 18px; font-size: 11px; border: none; margin: 2px;">
              <% 
                string selected2_0 = "";
                string selected2_212 = "";
                string selected2_216 = "";
                if (InstitutionalPhoneAreaCode2 == "212")
                  selected2_212 = "selected=\"selected\"";
                else if (InstitutionalPhoneAreaCode2 == "216")
                  selected2_216 = "selected=\"selected\"";
                else
                  selected2_0 = "selected=\"selected\"";
              %>
              <option value="0" <%:selected2_0 %>>Seç</option>
              <option value="212" <%:selected2_212 %>>212</option>
              <option value="216" <%:selected2_216 %>>216</option>
            </select>
          </div>
          <input type="hidden" id="InstitutionalPhoneAreaCode2" name="InstitutionalPhoneAreaCode2"
            value="<%:InstitutionalPhoneAreaCode2%>" />
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <input type="text" name="InstitutionalPhoneNumber2" id="InstitutionalPhoneNumber2"
            value="<%:InstitutionalPhoneNumber2  %>" class="small_input" style="width: 75px;
            height: 17px; padding-top: 3px; font-size: 12px;" />
        </div>
      </div>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <div style="width: 508px; height: 35px; margin-top: 5px; margin-left: 10px;">
        <div style="width: 60px; height: 25px; text-align: right; float: left;">
          <span style="font-size: 12px; color: #4c4c4c">Fax</span> :
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 7px;">
          <input type="text" name="InstitutionalFaxCulture" id="InstitutionalFaxCulture" value="<%:InstitutionalFaxCulture  %>"
            class="small_input" style="width: 30px; height: 17px; padding-top: 3px; font-size: 12px;" />
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 5px;">
          <div id="AreaCodeTextBox3">
            <input type="text" name="TextInstitutionalFaxAreaCode" id="TextInstitutionalFaxAreaCode"
              value="<%:InstitutionalFaxAreaCode  %>" class="small_input" style="width: 39px;
              height: 17px; padding-top: 3px; font-size: 12px;" />
          </div>
          <div id="AreaCodeSelect3" style="width: auto; height: 23px; float: left; border: #c9e6e2 1px solid;
            border-top: #c9e6e2 2px solid">
            <select id="DropDownInstitutionalFaxAreaCode" name="DropDownInstitutionalFaxAreaCode"
              style="height: 18px; font-size: 11px; border: none; margin: 2px;">
              <% 
                string selected3_0 = "";
                string selected3_212 = "";
                string selected3_216 = "";
                if (InstitutionalFaxAreaCode == "212")
                  selected3_212 = "selected=\"selected\"";
                else if (InstitutionalFaxAreaCode == "216")
                  selected3_216 = "selected=\"selected\"";
                else
                  selected3_0 = "selected=\"selected\"";
              %>
              <option value="0" <%:selected3_0 %>>Seç</option>
              <option value="212" <%:selected3_212 %>>212</option>
              <option value="216" <%:selected3_216 %>>216</option>
            </select>
          </div>
          <script type="text/javascript">
            if ($('#DropDownInstitutionalFaxAreaCode').css('display') != 'none') {
              $('#InstitutionalFaxAreaCode').val($('#DropDownInstitutionalFaxAreaCode').val());
            }
          </script>
          <input type="hidden" id="InstitutionalFaxAreaCode" name="InstitutionalFaxAreaCode"
            value="<%:InstitutionalFaxAreaCode%>" />
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <input type="text" name="InstitutionalFaxNumber" id="InstitutionalFaxNumber" value="<%:InstitutionalFaxNumber  %>"
            class="small_input" style="width: 75px; height: 17px; padding-top: 3px; font-size: 12px;" />
        </div>
      </div>
    </td>
  </tr>
  <tr id="trGsmType">
    <td style="width: 330px;" colspan="2">
      <div style="float: left; margin-left: 17px;">
        <span style="font-size: 12px;">Operatör :</span>
      </div>
      <div style="float: left; margin-left: 9px;">
        <%=Html.RadioButton("GsmType", 1, gsmItem != null && gsmItem.GsmType == 1 ? true : false, new { style = "height: 11px" })%>
        <span style="font-size: 12px; color: #000">Vodafone</span>
        <%=Html.RadioButton("GsmType", 3, gsmItem != null && gsmItem.GsmType == 3 ? true : false, new { style = "height: 11px" })%>&nbsp;<span
          style="font-size: 12px; color: #000">Turkcell</span>
        <%=Html.RadioButton("GsmType", 2, gsmItem != null && gsmItem.GsmType == 2 ? true : false, new { style = "height: 11px" })%>&nbsp;<span
          style="font-size: 12px; color: #000">Avea</span></div>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <div style="width: 508px; height: 35px; margin-top: 5px; margin-left: 10px;">
        <div style="width: 60px; height: 25px; text-align: right; float: left;">
          <span style="font-size: 12px; color: #4c4c4c">Gsm</span> :
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 7px;">
          <input type="text" name="InstitutionalGSMCulture" id="InstitutionalGSMCulture" value="<%:InstitutionalGSMCulture  %>"
            class="small_input" style="width: 30px; height: 17px; padding-top: 3px; font-size: 12px;" />
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 5px;">
          <div style="width: auto; height: 23px; float: left; border: #c9e6e2 1px solid; border-top: #c9e6e2 2px solid">
            <select style="height: 18px; font-size: 11px; border: none; margin: 2px;" id="InstitutionalGSMAreaCode"
              name="InstitutionalGSMAreaCode">
              <option value="0">Seç</option>
              <option value="530" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "530" ? "selected=\"selected\"" : "" %>>
                530</option>
              <option value="531" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "531" ? "selected=\"selected\"" : "" %>>
                531</option>
              <option value="532" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "532" ? "selected=\"selected\"" : "" %>>
                532</option>
              <option value="533" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "533" ? "selected=\"selected\"" : "" %>>
                533</option>
              <option value="534" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "534" ? "selected=\"selected\"" : "" %>>
                534</option>
              <option value="535" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "535" ? "selected=\"selected\"" : "" %>>
                535</option>
              <option value="536" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "536" ? "selected=\"selected\"" : "" %>>
                536</option>
              <option value="537" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "537" ? "selected=\"selected\"" : "" %>>
                537</option>
              <option value="538" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "538" ? "selected=\"selected\"" : "" %>>
                538</option>
              <option value="539" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "539" ? "selected=\"selected\"" : "" %>>
                539</option>
              <option value="541" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "541" ? "selected=\"selected\"" : "" %>>
                541</option>
              <option value="542" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "542" ? "selected=\"selected\"" : "" %>>
                542</option>
              <option value="543" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "543" ? "selected=\"selected\"" : "" %>>
                543</option>
              <option value="544" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "544" ? "selected=\"selected\"" : "" %>>
                544</option>
              <option value="545" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "545" ? "selected=\"selected\"" : "" %>>
                545</option>
              <option value="546" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "546" ? "selected=\"selected\"" : "" %>>
                546</option>
              <option value="547" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "547" ? "selected=\"selected\"" : "" %>>
                547</option>
              <option value="548" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "548" ? "selected=\"selected\"" : "" %>>
                548</option>
              <option value="549" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "549" ? "selected=\"selected\"" : "" %>>
                549</option>
              <option value="501" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "501" ? "selected=\"selected\"" : "" %>>
                501</option>
              <option value="505" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "505" ? "selected=\"selected\"" : "" %>>
                505</option>
              <option value="506" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "506" ? "selected=\"selected\"" : "" %>>
                506</option>
              <option value="507" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "507" ? "selected=\"selected\"" : "" %>>
                507</option>
              <option value="551" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "551" ? "selected=\"selected\"" : "" %>>
                551</option>
              <option value="552" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "552" ? "selected=\"selected\"" : "" %>>
                552</option>
              <option value="553" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "553" ? "selected=\"selected\"" : "" %>>
                553</option>
              <option value="554" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "554" ? "selected=\"selected\"" : "" %>>
                554</option>
              <option value="555" <%:gsmItem!= null && gsmItem.PhoneAreaCode == "555" ? "selected=\"selected\"" : "" %>>
                555</option>
            </select>
          </div>
        </div>
        <div style="width: auto; height: auto; float: left; margin-left: 3px;">
          <input type="text" name="InstitutionalGsmNumber" id="InstitutionalGsmNumber" value="<%:InstitutionalGSMNumber  %>"
            class="small_input" style="width: 75px; height: 17px; padding-top: 3px; font-size: 12px;" />
        </div>
      </div>
    </td>
  </tr>
</table>
