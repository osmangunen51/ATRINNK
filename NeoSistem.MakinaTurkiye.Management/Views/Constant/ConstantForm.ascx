<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ConstantModel>" %>

<table border="0" cellpadding="5" cellspacing="0">
  <tr>
    <td valign="top">
      Sabit Açıklama
    </td>
    <td valign="top">
      :
    </td>
    <td valign="top">
      <%= Html.TextBoxFor(model => model.ConstantName, new { style="height: 20px; width:205px;" })%>
    </td>
  </tr>
    <tr>
    <td valign="top">
      Sıra
    </td>
    <td valign="top">
      :
    </td>
    <td valign="top">
      <%= Html.TextBoxFor(model => model.Order, new { style="height: 20px; width:205px;" })%>
    </td>
  </tr>


  <tr>
    <td colspan="2">
    </td>
    <td align="right">
      <button type="button" onclick="saveConstant();">
        Kaydet</button>
      <button type="button" onclick="closeDialog();">
        Kapat</button>
    </td>
  </tr>
</table>
