<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Messages.MemberMessageDetailModel>" %>

<%if (Model.Phones.Count>0)
  { %>

<div class="col-sm-6">
    <b>Gönderen </b>
    <br />
    İsim Soyisim:
    <%: Model.Member.MemberName%>
    <%: Model.Member.MemberSurname%>
    <br />
    Adres:
    <%if (Model.Address != null)
      {  %>
    <%:MakinaTurkiye.Entities.Tables.Common.AddressExtensions.GetAddressEdit(Model.Address) %>
    <%} %>
    <%else
      {  %>
    Adres bilgisi bulunamadı.
    <%} %>
    <br />
    Mail Adresi:
    <%:Model.Member.MemberEmail %>
    <br />
    <% foreach (var item in Model.Phones)
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
       phoneType = "Telefon :";
   }
%>
    <%:phoneType %>
     +90&nbsp;<%:item.PhoneAreaCode%>&nbsp;<%:item.PhoneNumber%>
    <%if (item.PhoneType == (byte)PhoneType.Gsm && (item.active == 0 || item.active == null))
        {%>
    <a href="javascript:void(0)" class="plr5 tooltips tooltip-mt" data-toggle="tooltip" data-placement="bottom" title="" data-original-title="Onaylanmamış Telefon Numarası"><span  class="hidden-xs text-md text-danger  glyphicon glyphicon-question-sign"></span>
        </a>
    <% }
        else {%>
        <a href="javascript:void(0)" class="plr5 tooltips tooltip-mt" data-toggle="tooltip" data-placement="bottom" title="" data-original-title="Onaylanmış Telefon Numarası"><span style="color:#03ba28" class="hidden-xs text-md glyphicon glyphicon-ok-sign"></span>
        </a>
    <%} %>
    <br />
    <%} %>



<% } %>
    </div>
<% } %>

