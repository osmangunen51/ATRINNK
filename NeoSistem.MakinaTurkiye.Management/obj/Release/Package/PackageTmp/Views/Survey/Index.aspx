﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"
  Inherits="System.Web.Mvc.ViewPage<FilterModel<SurveyModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script src="/Scripts/FilterModel.debug.js" type="text/javascript"></script>
   <script type="text/javascript">
     filterModel.set_controllerName('Survey');
     filterModel.set_loadElementId('#table');
     filterModel.set_loadingElementId('#preLoading');

     filterModel.register({
       read: function (sender, model) {
         sender.refreshModel({
           SurveyQuestion: $('#SurveyQuestion').val()
         });
       }
     });
     RegisterHidden('SurveyId');
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" id="preLoading">
    <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
    <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
  </div>
  <div style="width: 100%; margin: 0 auto;">  
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" style="width: 30px;">
          </td>
          <td class="Header" unselectable="on" onclick="Order('SurveyQuestion', this);">
            Anket
          </td>
          <td class="Header" style="width: 70px; height: 19px">
          </td>
        </tr>
        <tr style="background-color: #F1F1F1">
          <td class="CellBegin">
          </td>
          <td class="Cell" align="center"> 
            <%: Html.FilterTextBox("SurveyQuestion") %>
          </td>
          <td class="CellEnd" style="width: 20px; height: 19px">
          </td>
        </tr>
      </thead>
      <tbody id="table">
        <%= Html.RenderHtmlPartial("SurveyList", Model) %>
      </tbody>
      <tfoot>
        <tr>
          <td class="ui-state ui-state-hover" colspan="8" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%: Model.TotalRecord %></strong>
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
  <script type="text/javascript">
    function OpenOption(Id, row, active) {
      $('#' + Id).toggleClass("ui-icon-minusthick");
      $(row).toggleClass('ui-helper-hidden');
      $(active).toggleClass("SelectedRow");
    } 
  </script>
</asp:Content>
