﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<SeoModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script src="/Scripts/FilterModel.debug.js" type="text/javascript"></script>
  <script type="text/javascript">
    filterModel.set_controllerName('Seo');
    filterModel.set_loadElementId('#table');
    filterModel.set_loadingElementId('#preLoading');

    filterModel.register({
      read: function (sender, model) {
        sender.refreshModel({
          PageName: $('#PageName').val(),
          Title: $('#Title').val()
        });
      }
    });
    RegisterHidden('SeoId');
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" id="preLoading">
    <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span><strong>Yükleniyor.</strong> Lütfen bekleyiniz...
  </div>
  <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" width="170px" unselectable="on" onclick="Order('PageName', this);">
            Sayfa
          </td>
          <td class="Header" unselectable="on" onclick="Order('Title', this);">
            Başlık
          </td>
          <td class="Header" style="width: 70px; height: 19px">
          </td>
        </tr>
        <tr style="background-color: #F1F1F1">
          <td class="CellBegin" align="center">
            <%: Html.FilterTextBox("PageName") %>
          </td>
          <td class="Cell">
            <%: Html.FilterTextBox("Title") %>
          </td>
          <td class="CellEnd" style="width: 20px; height: 19px">
          </td>
        </tr>
      </thead>
      <tbody id="table">
        <%= Html.RenderHtmlPartial("SeoList", Model) %>
      </tbody>
      <tfoot>
        <tr>
          <td class="ui-state ui-state-hover" colspan="6" align="right" style="border-color: #DDD; border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%: Model.TotalRecord %></strong>
          </td>
        </tr>
      </tfoot>
    </table>
  </div>
</asp:Content>
