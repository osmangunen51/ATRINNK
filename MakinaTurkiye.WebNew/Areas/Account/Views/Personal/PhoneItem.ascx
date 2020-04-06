<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MakinaTurkiye.Entities.Tables.Common.Phone>>" %>
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
    
    string InstitutionalWpAreaCode = "";
    string InstitutionalWpCulture = "";
    string InstitutionalWpNumber = "";
    
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
    var gsmItem = Model.SingleOrDefault(c => c.PhoneType == (byte)PhoneType.Gsm);
    if (gsmItem != null)
    {
        InstitutionalGSMAreaCode = gsmItem.PhoneAreaCode;
        InstitutionalGSMCulture = gsmItem.PhoneCulture;
        InstitutionalGSMNumber = gsmItem.PhoneNumber;
    }
    var wgsmItem = Model.SingleOrDefault(x=>x.PhoneType==(byte)PhoneType.Whatsapp);
    if(wgsmItem!=null)
    {
        InstitutionalWpAreaCode = wgsmItem.PhoneAreaCode;
        InstitutionalWpCulture = wgsmItem.PhoneCulture;
        InstitutionalWpNumber = wgsmItem.PhoneNumber;
    }
    var faxItem = Model.SingleOrDefault(c => c.PhoneType == (byte)PhoneType.Fax);
    if (faxItem != null)
    {
        InstitutionalFaxAreaCode = faxItem.PhoneAreaCode;
        InstitutionalFaxCulture = faxItem.PhoneCulture;
        InstitutionalFaxNumber = faxItem.PhoneNumber;
    }
%>
<div class="form-group" data-rel="gsm-wrapper">
    <label for="inputPassword3" class="col-sm-3 control-label">
        GSM :
    </label>
    <div class="col-sm-2">
        <input type="text" name="InstitutionalGSMCulture" id="InstitutionalGSMCulture" value="<%:InstitutionalGSMCulture == "" || InstitutionalGSMCulture==null ? "+90" :InstitutionalGSMCulture  %>"
            class="form-control" />
    </div>
    <div class="col-sm-2">
        <select id="MembershipModel_InstitutionalGSMAreaCode"  class="form-control" name="MembershipModel.InstitutionalGSMAreaCode">
            <option value="0" <%:gsmItem!= null  ? "selected=\"selected\"" : "" %>>Seç</option>
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
              <%if(gsmItem!=null)
              { 
              if(gsmItem.PhoneAreaCode!="" && gsmItem.PhoneAreaCode!=null)
              {
                if(int.Parse(gsmItem.PhoneAreaCode)>555){%>
                  <option value="<%:gsmItem.PhoneAreaCode %>" <%:gsmItem!= null && int.Parse(gsmItem.PhoneAreaCode) >= 555 ? "selected=\"selected\"" : "" %>>
                                        <%:gsmItem.PhoneAreaCode %></option>
            <%}}
              } %>
         
        </select>
        <%if(gsmItem!=null){ %>
        <input type="hidden" name="InstitutionalGSMAreaCode" id="InstitutionalGSMAreaCode" value="<%:gsmItem.PhoneAreaCode %>" />
    
        <%}else{ %>
        <input type="hidden" name="InstitutionalGSMAreaCode" id="InstitutionalGSMAreaCode" value="" />

        <%} %>
        
    </div>
    <div class="col-sm-2">
        <input type="text"  name="InstitutionalGsmNumber" id="InstitutionalGsmNumber" value="<%:InstitutionalGSMNumber  %>"
            class="form-control" />
    </div>
</div>
               <div class="form-group" data-rel="whatsapp-wrapper">
                                <label for="inputPassword3" class="col-sm-3 control-label">
                                   Whatsapp Gsm :
                                </label>
                                <div class="col-sm-2">
                                    <%:Html.TextBox("GsmWhatsappCulture",InstitutionalWpCulture,new Dictionary<string, object> { { "class", "form-control" },{"Value","+90"}})%>
                                </div>
                                <div class="col-sm-2">
                                    <select id="GsmWhatsappAreaCode"  class="form-control" name="GsmWhatsappAreaCode">
            <option value="0" <%:wgsmItem!= null  ? "selected=\"selected\"" : "" %>>Seç</option>
            <option value="530" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "530" ? "selected=\"selected\"" : "" %>>
                530</option>
            <option value="531" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "531" ? "selected=\"selected\"" : "" %>>
                531</option>
            <option value="532" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "532" ? "selected=\"selected\"" : "" %>>
                532</option>
            <option value="533" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "533" ? "selected=\"selected\"" : "" %>>
                533</option>
            <option value="534" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "534" ? "selected=\"selected\"" : "" %>>
                534</option>
            <option value="535" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "535" ? "selected=\"selected\"" : "" %>>
                535</option>
            <option value="536" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "536" ? "selected=\"selected\"" : "" %>>
                536</option>
            <option value="537" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "537" ? "selected=\"selected\"" : "" %>>
                537</option>
            <option value="538" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "538" ? "selected=\"selected\"" : "" %>>
                538</option>
            <option value="539" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "539" ? "selected=\"selected\"" : "" %>>
                539</option>
            <option value="541" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "541" ? "selected=\"selected\"" : "" %>>
                541</option>
            <option value="542" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "542" ? "selected=\"selected\"" : "" %>>
                542</option>
            <option value="543" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "543" ? "selected=\"selected\"" : "" %>>
                543</option>
            <option value="544" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "544" ? "selected=\"selected\"" : "" %>>
                544</option>
            <option value="545" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "545" ? "selected=\"selected\"" : "" %>>
                545</option>
            <option value="546" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "546" ? "selected=\"selected\"" : "" %>>
                546</option>
            <option value="547" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "547" ? "selected=\"selected\"" : "" %>>
                547</option>
            <option value="548" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "548" ? "selected=\"selected\"" : "" %>>
                548</option>
            <option value="549" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "549" ? "selected=\"selected\"" : "" %>>
                549</option>
            <option value="501" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "501" ? "selected=\"selected\"" : "" %>>
                501</option>
            <option value="505" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "505" ? "selected=\"selected\"" : "" %>>
                505</option>
            <option value="506" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "506" ? "selected=\"selected\"" : "" %>>
                506</option>
            <option value="507" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "507" ? "selected=\"selected\"" : "" %>>
                507</option>
            <option value="551" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "551" ? "selected=\"selected\"" : "" %>>
                551</option>
            <option value="552" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "552" ? "selected=\"selected\"" : "" %>>
                552</option>
            <option value="553" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "553" ? "selected=\"selected\"" : "" %>>
                553</option>
            <option value="554" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "554" ? "selected=\"selected\"" : "" %>>
                554</option>
            <option value="555" <%:wgsmItem!= null && wgsmItem.PhoneAreaCode == "555" ? "selected=\"selected\"" : "" %>>
                555</option>
                                    </select>
                                </div>
                             
                                <div class="col-sm-2">
                                    <%:Html.TextBox("GsmWhatsappNumber",InstitutionalWpNumber, new Dictionary<string, object> { { "class", "form-control" }})%>
                                </div>
                            </div>
<div class="form-group">
    <label for="inputPassword3" class="col-sm-3 control-label">
        Telefon (1)* :
    </label>
    <div class="col-sm-2">
        <input type="hidden" id="MembershipModel_InstitutionalPhoneAreaCode" name="MembershipModel.InstitutionalPhoneAreaCode" />
        <input type="text" name="InstitutionalPhoneCulture" id="InstitutionalPhoneCulture"
            value="<%:InstitutionalPhoneCulture=="" || InstitutionalPhoneCulture==null ? "+90":InstitutionalPhoneCulture %>" class="form-control" />
    </div>
    <div class="col-sm-2">
        <input type="text" name="TextInstitutionalPhoneAreaCode" id="TextInstitutionalPhoneAreaCode"
            value="<%:InstitutionalPhoneAreaCode %>" class="form-control" />
        <select id="DropDownInstitutionalPhoneAreaCode" name="DropDownInstitutionalPhoneAreaCode"
            class="form-control">
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
        <input type="hidden" id="InstitutionalPhoneAreaCode" name="InstitutionalPhoneAreaCode"
            value="<%:InstitutionalPhoneAreaCode %>" />
    </div>
    <div class="col-sm-2">
        <input type="text" name="InstitutionalPhoneNumber" id="InstitutionalPhoneNumber"
            value="<%:InstitutionalPhoneNumber  %>" class="form-control" />
    </div>
</div>
<div class="form-group">
    <input type="hidden" id="MembershipModel_InstitutionalPhoneAreaCode2" name="MembershipModel.InstitutionalPhoneAreaCode2" />
    <label for="inputPassword3" class="col-sm-3 control-label">
        Telefon (2) :
    </label>
    <div class="col-sm-2">
        <input type="text" name="InstitutionalPhoneCulture2" id="InstitutionalPhoneCulture2"
            value="<%:InstitutionalPhoneCulture2=="" || InstitutionalPhoneCulture2==null ? "+90":InstitutionalPhoneCulture%>" class="form-control" />
    </div>
    <div class="col-sm-2">
        <select id="DropDownInstitutionalPhoneAreaCode2" name="DropDownInstitutionalPhoneAreaCode2"
            class="form-control">
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
        <input type="text" name="TextInstitutionalPhoneAreaCode2" id="TextInstitutionalPhoneAreaCode2"
            value="<%: InstitutionalPhoneAreaCode2 %>" class="form-control" />
        <input type="hidden" id="InstitutionalPhoneAreaCode2" name="InstitutionalPhoneAreaCode2"
            value="<%: InstitutionalPhoneAreaCode2 %>" />
    </div>
    <div class="col-sm-2">
        <input type="text" name="InstitutionalPhoneNumber2" id="InstitutionalPhoneNumber2"
            value="<%:InstitutionalPhoneNumber2  %>" class="form-control" />
    </div>
</div>
<div class="form-group">
    <input type="hidden" id="MembershipModel_InstitutionalFaxAreaCode" name="MembershipModel.InstitutionalFaxAreaCode" />
    <label for="inputPassword3" class="col-sm-3 control-label">
        Fax :
    </label>
    <div class="col-sm-2">
        <input type="text"  name="InstitutionalFaxCulture" id="InstitutionalFaxCulture" value="<%:InstitutionalFaxCulture  %>"
            class="form-control" />
    </div>
    <div class="col-sm-2">
        <input type="text" name="TextInstitutionalFaxAreaCode" id="TextInstitutionalFaxAreaCode"
            value="<%:InstitutionalFaxAreaCode  %>" class="form-control" />
        <select id="DropDownInstitutionalFaxAreaCode" name="DropDownInstitutionalFaxAreaCode"
            class="form-control">
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
        <input type="hidden"  id="InstitutionalFaxAreaCode" name="InstitutionalFaxAreaCode"
            value="<%:InstitutionalFaxAreaCode  %>" />
    </div>
    <div class="col-sm-2">
        <input type="text" name="InstitutionalFaxNumber" id="InstitutionalFaxNumber" value="<%:InstitutionalFaxNumber  %>"
            class="form-control" />
    </div>
</div>
