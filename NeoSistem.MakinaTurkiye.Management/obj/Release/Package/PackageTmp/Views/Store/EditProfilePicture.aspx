﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Store>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  MemberStore
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%=Html.RenderHtmlPartial("Style") %>
  <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginPanel())
    { %>
  <%=Html.RenderHtmlPartial("TabMenu") %>
  <% using (Html.BeginForm("EditProfilePicture", "Store", FormMethod.Post, new { enctype = "multipart/form-data" }))
     { %>
  <div style="width: 800px; float: left; margin: 20px 0px 20px 20px">
    <div style="float: left; width: 100%;">
      <div style="float: left; width: 180px; margin-top: 20px; margin-left: 30px;">
        <span style="font-size: 12px; font-weight: bold;">Firma Profil Görsel Resmi</span>
      </div>
    </div>
    <div style="float: left; width: 100%;">
      <div style="width: 180px; height: 230px; border: solid 1px #bababa; margin-top: 5px;
        margin-left: 30px; float: left;">
        <% if (FileHelpers.HasFile(AppSettings.StoreProfilePicture + Model.StorePicture))
           { %>
        <table style="width: 100%; height: 100%;">
          <tr>
            <td align="center" valign="middle">
              <img src="<%=AppSettings.StoreProfilePicture + Model.StorePicture%>"
                width="175" />
            </td>
          </tr>
        </table>
        <% }
           else
           { %>
        <div style="float: left; margin-top: 100px; margin-left: 20px;">
          <span style="font-size: 12px;">Profil Resmi Yüklenmedi. </span>
        </div>
        <% } %>
      </div>
      <div style="float: left; margin-left: 30px; margin-top: 40px; width: 500px">
        <div style="float: left; margin-top: 4px;">
          <span style="font-size: 12px;">Profil Resmi :</span></div>
        <div style="float: left; margin-left: 5px;">
          <input type="file" name="ProfilePicture" style="border: solid 1px #bababa; width: 220px;
            height: 20px;" /></div>
        <div style="float: left; margin-left: 5px;">
          <button style="border: solid 1px #bababa; height: 20px;">
            Yükle</button>
        </div>
      </div>
      <div style="float: left; margin-left: 30px; margin-top: 10px;">
        <span style="font-size: 12px;">Firma profil sayfanızda görünmesini istediğiniz firma
          görselinizi ekleyebilirsiniz. </span>
      </div>
    </div>
  </div>
  <input id="storeId" name="storeId" value="<%:this.RouteData.Values["id"] %>" type="hidden" />
  <% } %>
  <%} %>
</asp:Content>
