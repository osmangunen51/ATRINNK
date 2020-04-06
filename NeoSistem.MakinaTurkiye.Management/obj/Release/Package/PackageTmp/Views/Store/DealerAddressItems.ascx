﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Address>>" %>
<% foreach (var item in Model)
   { %>
<div style="width: 300px; height: auto; float: left; margin-bottom: 20px;">
  <div style="width: 100%; height: auto; float: left;">
    <div style="width: 220px; height: auto; float: left; font-size: 12px;">
      <span style="font-size: 12px;">
        <%= EnumModels.AddressEdit(item)%></span>
    </div>
  </div>
  <div style="width: 100%; height: auto; margin-top: 10px; float: left; font-size: 12px;">
    <% foreach (var itemPhones in item.Phones)
       { %>
    <% var pType = (PhoneType)itemPhones.PhoneType;
       string phone = "";
       switch (pType)
       {
         case PhoneType.Phone:
           phone = "Telefon:";
           break;
         case PhoneType.Fax:
           phone = "Fax:";
           break;
         case PhoneType.Gsm:
           phone = "Gsm:";
           break;
         default:
           break;
       }
    %>
    <div style="width: 220px; height: auto; float: left;">
      <div style="width: 50px; float: left;">
        <span style="font-size: 12px;">
          <%:phone%></span>
      </div>
      <div style="width: 150px; float: left;">
      </div>
      <span style="font-size: 12px;">
        <%:itemPhones.PhoneCulture + " " + itemPhones.PhoneAreaCode + " " + itemPhones.PhoneNumber%>
      </span>
    </div>
    <% } %>
  </div>
  <div style="float: left; width: 100%;">
    <a style="font-size: 12px; color: Red; cursor: pointer;" onclick="javascript:var id= $('#hdnDealerType').val(); DeleteAddress('<%:item.AddressId %>',id);">
      Adres ve Telefonu Sil</a>
  </div>
</div>
<% } %>
