﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  MemberStore
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%=Html.RenderHtmlPartial("Style") %>
  <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
    <script type="text/javascript">
    $(document).ready(function () {
      $('.date').datepicker();

        

    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginPanel())
    { %>
  <%=Html.RenderHtmlPartial("TabMenu") %>
  <% using (Html.BeginForm("EditMember", "Store", FormMethod.Post))
     { %>
  <div style="width: 1200px; float: left; margin: 20px 0px 0px 20px">
    <table border="0" cellpadding="5" cellspacing="0">
      <tr>
        <td>
          <%: Html.LabelFor(m => m.MemberNo)%>
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.DisplayTextFor(m => m.MemberNo)%>
        </td>
        <td>
        </td>
      </tr>
      <% if (Model.MemberType == (byte)MemberType.Enterprise)
         { %>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.MemberTitleType)%>
        </td>
        <td>
          :
        </td>
        <td>
          <%:Html.DropDownListFor(c => c.MemberTitleType, Model.MemberTitleTypeItems)%>
        </td>
        <td>
        </td>
      </tr>
      <% } %>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.MemberName)%>
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBoxFor(m => m.MemberName, new { style = "width: 250px;" })%>
          &nbsp;&nbsp;&nbsp;&nbsp;<%: Html.TextBoxFor(m => m.Ad1, new { style = "width: 250px;" })%>
          &nbsp;<%: Html.CheckBoxFor(m =>m.Email1Check)%>
          &nbsp;&nbsp;&nbsp;&nbsp;<%: Html.TextBoxFor(m => m.Ad2, new { style = "width: 250px;" })%>
          &nbsp;<%: Html.CheckBoxFor(m =>m.Email2Check)%>
        </td>
        <td>
          <%: Html.ValidationMessageFor(m => m.MemberName)%>
        </td>
      </tr>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.MemberSurname)%>
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBoxFor(m => m.MemberSurname, new { style = "width: 250px;" })%>
          &nbsp;&nbsp;&nbsp;&nbsp;<%: Html.TextBoxFor(m => m.SoyAd1, new { style = "width: 250px;" })%>
          &nbsp;&nbsp;&nbsp;&nbsp;<%: Html.TextBoxFor(m => m.SoyAd2, new { style = "width: 250px;" })%>
        </td>
        <td>
          <%: Html.ValidationMessageFor(m => m.MemberSurname)%>
        </td>
      </tr>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.MemberEmail)%>
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBoxFor(m => m.MemberEmail, new { style = "width: 250px;" })%>
          &nbsp;&nbsp;&nbsp;&nbsp;<%: Html.TextBoxFor(m => m.Eposta1, new { style = "width: 250px;" })%>
          &nbsp;&nbsp;&nbsp;&nbsp;<%: Html.TextBoxFor(m => m.Eposta2, new { style = "width: 250px;" })%>
        </td>
        <td>
          <%: Html.ValidationMessageFor(m => m.MemberEmail)%>
        </td>
      </tr>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.MemberPassword)%>
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBoxFor(m => m.MemberPassword, new { style = "width: 250px;" })%>
        </td>
        <td>
          <%: Html.ValidationMessageFor(m => m.MemberPassword)%>
        </td>
      </tr>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.Active)%>
        </td>
        <td>
          :
        </td>
        <td>
          <div style="width: auto; height: auto; float: left">
            <div style="width: auto; height: auto; float: left; margin-top: 4px;">
              Aktif
            </div>
            <div style="width: auto; height: auto; float: left; margin-left: 5px;">
              <%: Html.RadioButton("Active", true)%></div>
          </div>
          <div style="width: auto; height: auto; float: left; margin-left: 10px;">
            <div style="width: auto; height: auto; float: left; margin-top: 4px;">
              Pasif
            </div>
            <div style="width: auto; height: auto; float: left; margin-left: 5px;">
              <%: Html.RadioButton("Active", false)%></div>
          </div>
        </td>
        <td>
        </td>
      </tr>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.BirthDate)%>
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.TextBox("BirthDate", Model.BirthDate==null?"": Model.BirthDate.Value.ToString("dd.MM.yyyy"),new { @class="date"})%>
        </td>
        <td>
        </td>
      </tr>
      <tr>
        <td>
          <%: Html.LabelFor(m => m.ReceiveEmail)%>
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.CheckBoxFor(m => m.ReceiveEmail)%>
        </td>
        <td>
        </td>
      </tr>
      <tr>
        <td colspan="3">
          <div style="margin: 20px auto; padding-top: 10px; border-top: solid 1px #DDD; width: 99%;
            float: left">
            <button type="submit">
              Kaydet</button>
            <button type="reset">
              İptal</button>
          </div>
        </td>
      </tr>
    </table>
  </div>
  <% } %>
  <%} %>
</asp:Content>
