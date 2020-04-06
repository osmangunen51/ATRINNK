<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<MakinaTurkiye.Entities.Tables.Common.Address>>" %>
<% foreach (var item in Model)
   {
     
        %>
<div>
    <%=MakinaTurkiye.Entities.Tables.Common.AddressExtensions.GetAddressEdit(item)%>
    <br />
<%--    <% foreach (var itemPhones in item.phone)
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
    <%:phone%>:
    <%:itemPhones.PhoneCulture + " " + itemPhones.PhoneAreaCode + " " + itemPhones.PhoneNumber%>--%>
    <br />
<%--    <% } %>--%>
    <a onclick="javascript:var id= $('#hdnDealerType').val(); DeleteAddress('<%:item.AddressId %>',id);"
        class="btn btn-xs btn-default">Adres ve Telefonu Sil </a>
</div>
<% } %>
