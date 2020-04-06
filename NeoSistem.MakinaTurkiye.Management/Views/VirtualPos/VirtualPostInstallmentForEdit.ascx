﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<VirtualPostInstallment>>" %>
<div style="float: left; border: solid 1px #bababa; border-bottom: none; border-right: none;
  margin-top: 15px;">
  <% foreach (var item in Model)
     { %>
  <div style="float: left; width: 168px; height: 21px; background-color: #ececee; padding-top: 3px;
    padding-left: 5px; border-bottom: solid 1px #bababa; border-right: solid 1px #bababa">
    <div style="float: left; width: 45px;">
      T-<input type="text" value="<%:item.VirtualPostInstallmentCount %>" style="width: 25px;
        padding-top: 0px; height: 15px; margin-left: 2px;"  onkeyup="setVPICountKeyupData(hdn<%:item.VirtualPosInstallmentId%>, $(this).val());"/>
    </div>
    <div style="float: left; margin-left: 10px; width: 95px; padding: 0px;">
      <div style="float: left;">
        %</div>
      <div style="float: left;">
        <input type="text" id="input<%:item.VirtualPosInstallmentId%>" value="<%:item.VirtualPostInstallmentValue.HasValue ?  item.VirtualPostInstallmentValue.Value.ToString("N") : "" %>"
          style="width: 30px; float: left; padding: 0px; margin-left: 3px; text-align: center;"
          onkeyup="setVPIKeyupData('hdn<%:item.VirtualPosInstallmentId%>','<%:item.VirtualPosInstallmentId%>', $(this).val());" />
      </div>
      <div style="float: right; margin-left: 6px;">
        <a style="cursor: pointer;" title="Sil" onclick="deleteInstallment(<%:item.VirtualPosInstallmentId%>);">
          <img src="/Content/Images/vcard_delete.png" /></a></div>
    </div>
  </div>
  <input type="hidden" name="VirtualPostInstallment" id="hdn<%:item.VirtualPosInstallmentId%>"
    value="<%:item.VirtualPosInstallmentId%>&<%:item.VirtualPostInstallmentValue%>&<%:item.VirtualPostInstallmentCount %>" />
  <% } %>
</div>
