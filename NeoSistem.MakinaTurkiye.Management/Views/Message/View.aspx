<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<MessageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  View
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%   var entities = new MakinaTurkiyeEntities(); %>
  <% using (Html.BeginForm("View", "Message"))
     { %>
  <table border="0" cellpadding="5" cellspacing="0">
    <tr>
      <td colspan="3">
      </td>
    </tr>
    <tr>
      <td valign="top">
        Kimden
      </td>
      <td>
        :
      </td>
      <td>
        <%
       var memberFrom = entities.Members.SingleOrDefault(c => c.MainPartyId == Model.MainPartyId);
        %>
         
        <%:memberFrom.MemberName + " " + memberFrom.MemberSurname%>
        <%if (Model.FromSecondName != "Bireysel")
            {%>
          <a href="<%:Model.StoreUrl %>">
               ( <%:Model.FromSecondName %>)
          </a>
          <% }
              else {%>
                  (
          <%:Model.FromSecondName %>
          )
          <% } %>
    
      </td>
    </tr>
    <tr>
      <td valign="top">
        Kime
      </td>
      <td>
        :
      </td>
      <td>
         <%
       var memberTo = entities.Members.SingleOrDefault(c => c.MainPartyId == Model.InOutMainPartyId);
        %>
        <%:memberTo.MemberName + " " + memberTo.MemberSurname%>
                  <%if (Model.ToSecondName != "Bireysel")
            {%>
          <a href="<%:Model.StoreUrl %>">
               ( <%:Model.ToSecondName %>)
          </a>
          <% }
              else {%>
                  (
          <%:Model.ToSecondName %>
          )
          <% } %>
      </td>
    </tr>
    <tr>
      <td valign="top">
        Konu
      </td>
      <td>
        :
      </td>
      <td>
        <%:Model.Message.MessageSubject%>
      </td>
    </tr>
    <tr>
      <td valign="top">
        Mesaj İçeriği :
      </td>
      <td>
        :
      </td>
      <td>
        <%= Model.Message.MessageContent%>
      </td>
    </tr>
    <tr>
      <td valign="top">
        Gönderilme Tarihi
      </td>
      <td>
        :
      </td>
      <td>
        <%:Model.Message.MessageDate.ToString("dd.MM.yyyy")%>
      </td>
    </tr>
    <%if (Model.Product != null)
      {  %>
    <tr>
      <td valign="top">
        İlgili İlan No
      </td>
      <td>
        :
      </td>
      <td>
    <%string productUrl =  Helpers.ProductUrl(Model.Product.ProductId,Model.Product.ProductName); %>
    <a href="<%:productUrl %>" target="_blank"><%:Model.Product.ProductNo%></a>
      </td>
    </tr>
    <%} %>
     <%if (Model.Product != null)
       {  %>
    <tr>
      <td valign="top">
        İlgili İlan Adı
      </td>
      <td>
        :
      </td>
      <td>
        <%:Model.Product.ProductName%>
      </td>
    </tr>
    <%} %>
    <tr>
      <td colspan="3">
        <div style="border-top: dashed 1px #DDD; width: 99%; text-align: right; padding-top: 5px">
          <button type="reset">
            Geri Dön</button></div>
      </td>
    </tr>
  </table>
  <% } %>
</asp:Content>
