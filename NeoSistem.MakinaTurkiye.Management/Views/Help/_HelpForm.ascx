<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Management.Models.HelpModel>" %>
 <table border="0" class="tableForm" cellpadding="5" cellspacing="0">
     <tr style="height: 40px;">
      <td colspan="3" align="right">
        <button type="submit" style="width: 70px; height: 35px;" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" aria-disabled="false"><span class="ui-button-text">
          Kaydet
        </span></button>
        <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/Help/Index'" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" aria-disabled="false"><span class="ui-button-text">
          İptal
        </span></button>
        <br>
        <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;">
        </div>
      </td>
    </tr>
  <tr>
    <td valign="top">
     Konu
    </td>
    <td valign="top">
      :
    </td>
    <td valign="top">
      <%= Html.TextBoxFor(model => model.Subject, new { style = "height: 20px; width:250px;" })%>
    </td>
  </tr>
   
  <tr>
    <td valign="top">
     İçerik
    </td>
    <td valign="top">
      :
    </td>
    <td valign="top">
      <%= Html.TextAreaFor(model => model.Content, new { style = "height: 20px; width:205px;" })%>
    </td>
  </tr>
   
</table>