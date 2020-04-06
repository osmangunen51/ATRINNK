<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ActivityTypeModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Makina Türkiye | Faaliyet Tipi Ekle
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <script type="text/javascript">
    $(function () { $('.date-pick').datepicker(); });
  </script>
  <% Html.EnableClientValidation();%>
  <%using (Html.BeginForm("Create", "ActivityType", FormMethod.Post, new { enctype = "multipart/form-data" }))
    {%>
  <%Html.ValidationSummary();%>
  <table class="tableForm" style="padding-top: 10px; height: auto;">
    <tr style="height: 40px;">
      <td colspan="2" align="right">
        <button type="submit" style="width: 70px; height: 35px;">
          Kaydet
        </button>
        <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/ActivityType/Index'">
          İptal
        </button>
        <br />
        <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;">
        </div>
      </td>
    </tr>
    <tr>
      <td style="width: 120px;">
        <%: Html.LabelFor(model => model.ActivityName) %>
        :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.ActivityName, new { style = "width:300px" })%>
        <%: Html.ValidationMessageFor(model => model.ActivityName)%>
      </td>
    </tr>
    <tr>
      <td style="width: 120px;">
         <%: Html.LabelFor(model => model.Order) %>
        :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.Order, new { style = "width:300px" })%>
        <%: Html.ValidationMessageFor(model => model.Order)%>
      </td>
    </tr>
    <tr style="height: 40px;">
      <td colspan="2" align="right" style="padding-bottom: 10px;">
        <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;">
        </div>
        <br />
        <button type="submit" style="width: 70px; height: 35px;">
          Kaydet
        </button>
        <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/ActivityType/Index'">
          İptal
        </button>
      </td>
    </tr>
  </table>
  <%} %>
</asp:Content>
