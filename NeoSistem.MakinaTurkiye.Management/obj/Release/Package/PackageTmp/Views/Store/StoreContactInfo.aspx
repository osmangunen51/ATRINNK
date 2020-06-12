<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>



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
          else if (item.PhoneType == (byte)PhoneType.Whatsapp)
          {
              phoneType = "Whatsapp";
          }
      %>
      <%:phoneType %>
      <%:item.PhoneCulture%>&nbsp;<%:item.PhoneAreaCode%>&nbsp;<%:item.PhoneNumber%>
        <br />
  
      <% }
          }%>
    Email:
        <%:Model.StoreEMail %>
    </div>
      </div>

