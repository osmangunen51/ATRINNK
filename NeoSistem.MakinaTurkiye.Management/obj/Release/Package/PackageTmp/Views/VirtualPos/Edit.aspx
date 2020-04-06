<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<VirtualPos>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginForm("Edit", "VirtualPos", FormMethod.Post))
    {%>
  <table class="tableForm" style="padding-top: 10px; height: auto;">
    <tr>
      <td colspan="2">
        <button type="submit" style="width: 70px; height: 35px;">
          Kaydet
        </button>
        <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/VirtualPos/Index'">
          İptal
        </button>
        <br />
        <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;">
        </div>
      </td>
    </tr>
    <tr>
      <td>
        Sanal Pos Adı :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.VirtualPostName, new { style = "width:300px" })%>
      </td>
    </tr>
    <tr>
      <td>
        Mağaza Numarası :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.VirtualPosClientId, new { style = "width:300px" })%>
      </td>
    </tr>
    <tr>
      <td>
        Sanal Pos Türü :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.VirtualPosStoreType, new { style = "width:300px" })%>
      </td>
    </tr>
    <tr>
      <td>
        Mağaza Key :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.VirtualPosStoreKey, new { style = "width:300px" })%>
      </td>
    </tr>
    <tr>
      <td>
        Post Url :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.VirtualPosPostUrl, new { style = "width:300px" })%>
      </td>
    </tr>
    <tr>
      <td>
        Api Kullanıcı Adı :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.VirtualPosApiUserName, new { style = "width:300px" })%>
      </td>
    </tr>
    <tr>
      <td>
        Api Şifre :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.VirtualPosApiPass, new { style = "width:300px" })%>
      </td>
    </tr>
    <tr>
      <td>
        Api Url :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.VirtualPosApiUrl, new { style = "width:300px" })%>
      </td>
    </tr>
    <tr>
      <td>
        Aktif :
      </td>
      <td align="left">
        <%: Html.CheckBox("VirtualPosActive", Model.VirtualPosActive.Value)%>
      </td>
    </tr>
    <tr>
      <td colspan="2" align="right">
        <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;">
        </div>
        <br />
        <button type="submit" style="width: 70px; height: 35px;">
          Kaydet
        </button>
        <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/VirtualPos/Index'">
          İptal
        </button>
      </td>
    </tr>
  </table>
  <%} %>
</asp:Content>
