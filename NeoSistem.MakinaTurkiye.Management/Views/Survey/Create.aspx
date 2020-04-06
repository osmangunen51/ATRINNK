﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SurveyModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Create
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script src="/Scripts/JQuery.validate.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <% using (Html.BeginForm())
     { %>
  <div style="float: left; width: 750px">
    <% using (Html.BeginPanel())
       { %>
    <table border="0" cellpadding="5" cellspacing="0" style="font-family: Calibri; font-size: 13px;
      width: 600px; margin: 10px">
      <tr>
        <td colspan="3" align="right">
          <button type="submit">
            Kaydet
          </button>
          <button type="button" style="height: 26px" onclick="window.location='/Survey'">
            İptal
          </button>
          <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 10px">
          </div>
        </td>
      </tr>
      <tr>
        <td style="width: 90px">
          <%: Html.LabelFor(m => m.SurveyQuestion)%>
        </td>
        <td style="width: 1px">
          :
        </td>
        <td>
          <%: Html.TextBoxFor(m => m.SurveyQuestion, new { style = "width: 580px" })%>
        </td>
        <td>
          <% Html.ValidateFor(m => m.SurveyQuestion); %>
        </td>
      </tr>
      <tr>
        <td>
          Aktif
        </td>
        <td>
          :
        </td>
        <td>
          <%: Html.CheckBoxFor(m => m.Active)%>
        </td>
        <td>
          <% Html.ValidateFor(m => m.Active); %>
        </td>
      </tr>
      <tr>
        <td valign="top">
          <label for="OptionContent">
            Seçenekler</label>
        </td>
        <td valign="top">
          :
        </td>
        <td>
          <table class="TableList" border="0" cellpadding="3" cellspacing="0">
            <tr>
              <td style="width: 500px" class="HeaderBegin">
                Seçenek
              </td>
              <td class="Header">
              </td>
            </tr>
            <tbody id="optionBody">
              <%= Html.RenderHtmlPartial("SurveyOptions", Model.SurveyOptions)%>
            </tbody>
            <tr>
              <td class="CellBegin">
                <textarea id="OptionContent" class="required" name="OptionContent" cols="0" rows="2"
                  style="height: 35px; width: 500px"></textarea>
              </td>
              <td class="Cell">
                <span class="ui-icon ui-icon-circle-plus" style="cursor: pointer;" onclick="AddOption();">
                </span>
              </td>
            </tr>
          </table>
        </td>
      </tr>
      <tr>
        <td colspan="3" align="right">
          <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-bottom: 10px">
          </div>
          <button type="submit">
            Kaydet
          </button>
          <button type="button" style="height: 26px" onclick="window.location='/Survey'">
            İptal
          </button>
        </td>
      </tr>
    </table>
    <% } %>
  </div>
  <div style="float: left; width: 477px; margin-top: 7px">
    <%= Html.ValidationSummary(false, "", new { style = "width: 375px; " })%>
    <div class="validation-summary-errors ui-helper-hidden" id="errorOption" style="width: 375px;">
      <ul>
        <li>Seçenek boş geçilemez.</li>
      </ul>
    </div>
  </div>
  <% } %>
  <script type="text/javascript">
    $(document).ready(function () {
      $('#OptionContent').keyup(function () {
        if ($.trim($(this).val()).length >= 0) {
          $(this).removeClass('input-validation-error');
        } else {
          $(this).addClass('input-validation-error');
        }
      });
    });
    function AddOption() {
      if ($.trim($('#OptionContent').val()).length <= 0) {
        $('#errorOption').show();
        $('#OptionContent').addClass('input-validation-error');
        return;
      }
      $('#errorOption').hide();
      $('#OptionContent').removeClass('input-validation-error');
      $.ajax({
        url: '/Survey/AddOption',
        type: 'post',
        data: {
          OptionContent: $('#OptionContent').val()
        },
        success: function (data) {
          $('#optionBody').html(data);
          $('#OptionContent').val('');
        }
      });
    }

    function RemoveOption(index) {
      var dialogResult = confirm('Kaydı silmek istediğinizden eminmisiniz?');
      if (dialogResult) {
        $.ajax({
          url: '/Survey/RemoveOption',
          type: 'post',
          dataType: 'json',
          data: { id: index },
          success: function (result) {
            if (result) {
              $('#opRow' + index).hide();
            }
          }
        });
      }
    }
  </script>
</asp:Content>
