<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StoreModel>" %>
<% foreach (var item in Model.DealerServisAddressItems)
   { %>
<div style="width: 100%; height: auto; float: left; margin-bottom: 20px;">
  <div style="width: 100%; height: auto; float: left;">
    <div style="width: 70px; height: auto; float: left;">
      <span style="font-weight: bold;">
        <%:item.DealerName%></span> :
    </div>
    <div style="width: 220px; height: auto; float: left; margin-left: 10px;">
<%--      <%= EnumModels.AddressEdit(item) %>--%>
    </div>
  </div>
  <div style="width: 100%; height: auto; margin-top: 10px; float: left;">
    <% foreach (var itemPhones in item.MemberPhoneItemsForAddress)
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
    <div style="width: 220px; height: auto; float: left; margin-left: 80px;">
      <div style="width: 50px; float: left;">
        <%:phone %>
      </div>
      <div style="width: 150px; float: left;">
      </div>
      <%:itemPhones.PhoneCulture+ " " + itemPhones.PhoneAreaCode+ " " + itemPhones.PhoneNumber %>
    </div>
    <% } %>
  </div>
</div>
<% } %>
