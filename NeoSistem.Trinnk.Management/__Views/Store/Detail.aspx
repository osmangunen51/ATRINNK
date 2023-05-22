﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.Trinnk.Core.Web.ViewPage<StoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript">
    $(document).ready(function () {
      $('#tabs').tabs();

      $('.ui-widget-header').addClass('ui-state-default');
      $('div.ui-tabs').removeClass('ui-widget-content');

      $('#StoreEstablishmentDate').datepicker();
      $('#StorePacketBeginDate').datepicker();
      $('#StorePacketEndDate').datepicker();

    });

    function openContent(content) {
      $(content).slideToggle();
    }
  </script>
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
  <script src="/Scripts/Trinnk.js" type="text/javascript" defer="defer"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <table border="0" cellpadding="0" cellspacing="0" width="100%" style="float: left">
    <tr>
      <td>
        <div id="tabs" style="margin: 0px">
          <% using (Html.BeginForm())
             { %>
          <ul>
            <li><a href="#tabs-1">Genel Bilgiler</a></li>
            <li><a href="#tabs-2">Detaylı Bilgiler</a></li>
            <li><a href="#tabs-3">Faaliyet Tipleri</a></li>
            <li><a href="#tabs-4">Faaliyet Alanları</a></li>
          </ul>
          <div id="tabs-1">
            <table border="0" cellpadding="5" cellspacing="0">
              <tr>
                <td valign="top">
                  <%: Html.LabelFor(m => m.StoreLogo)%>
                </td>
                <td valign="top">
                  :
                </td>
                <td colspan="2">
                  <% if (!String.IsNullOrEmpty(Model.StoreLogo))
                     { %>
                  <img src="/UserFiles/Images/StoreLogo/<%= Model.StoreLogo %>" align="left" style="margin-right: 5px;" />
                  <% } %>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.StoreName) %>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%:Model.StoreName %>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.StoreWeb)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%:Model.StoreWeb %>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.StoreEMail)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%:Model.StoreEMail %>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.StoreCapital)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%:Model.StoreCapital %>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.StoreEstablishmentDate)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%:Model.StoreEstablishmentDate%>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.StoreType)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <% if (Model.StoreType == 1)
                     { %>
                  Anonim Şirketi
                  <% }
                     else
                     { %>
                  Limited Şirketi
                  <% } %>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.StoreEmployeesCount)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%: Model.StoreEmployeesCount%>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.StoreEndorsement)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%:Model.StoreEndorsement %>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.StorePacketId)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%:Model.StorePacketName %>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.StorePacketBeginDate)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%:Model.StorePacketBeginDate.ToString("dd.MM.yyyy")%>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.StorePacketEndDate)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%:Model.StorePacketEndDate.ToString("dd.MM.yyyy")%>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.StoreActiveType)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <% if (Model.StoreActiveType == 1)
                     { %>
                  Aktif
                  <% }
                     else if (Model.StoreActiveType == 2)
                     { %>
                  Pasif
                  <% } %>
                  <%else
                    {%>
                  Silindi
                  <% } %>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.StoreRecordDate)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%:Model.StoreRecordDate.ToString("dd.MM.yyyy")%>
                </td>
                <td>
                  <%: Html.ValidationMessageFor(m => m.StoreActiveType)%>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.SalesDepartmentName)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%: Model.SalesDepartmentName%>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.SalesDepartmentEmail)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%: Model.SalesDepartmentEmail %>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.PurchasingDepartmentName)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%: Model.PurchasingDepartmentName %>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td>
                  <%: Html.LabelFor(m => m.PurchasingDepartmentEmail)%>
                </td>
                <td>
                  :
                </td>
                <td>
                  <%: Model.PurchasingDepartmentEmail  %>
                </td>
                <td>
                </td>
              </tr>
            </table>
          </div>
          <div id="tabs-2">
            <table border="0" cellpadding="5" cellspacing="0">
              <tr>
                <td valign="top">
                  <%: Html.LabelFor(m => m.StoreDescription)%>
                </td>
                <td valign="top">
                  :
                </td>
                <td>
                  <%:Model.StoreDescription%>
                </td>
                <td>
                </td>
              </tr>
              <tr>
                <td valign="top">
                  <%: Html.LabelFor(m => m.StoreAbout)%>
                </td>
                <td valign="top">
                  :
                </td>
                <td>
                  <%:Model.StoreAbout %>
                </td>
                <td>
                </td>
              </tr>
            </table>
          </div>
          <div id="tabs-3">
            <div style="width: 390px; height: 170px; border: solid 1px #b0afaf; padding-left: 20px;
              padding-top: 25px;">
              <% foreach (var item in Model.ActivityTypeItems)
                 { %>
              <div style="width: 130px; height: 25px; margin-top: 10px; float: left;">
                <div style="float: left; width: auto; height: auto; float: left">
                  <% bool checkStore = false; %>
                  <% foreach (var itemStore in Model.StoreActivityTypeItems)
                     { %>
                  <% if (itemStore.ActivityTypeId == item.ActivityTypeId)
                     { %>
                  <% checkStore = true; %>
                  <% } %>
                  <% } %>
                  <%:Html.CheckBox("ActivityTypeIdItems", checkStore, new { value = item.ActivityTypeId })%>
                </div>
                <div style="float: left; width: auto; height: auto; float: left; margin-left: 4px;
                  margin-top: 4px;">
                </div>
                <%:item.ActivityName%>
              </div>
              <% } %>
            </div>
          </div>
          <div id="tabs-4">
            <div style="width: 570px; height: 270px;">
              <% foreach (var item in Model.SectorItems)
                 { %>
              <div style="width: 275px; float: left; margin-left: 10px;">
                <div style="width: 14px; height: 14px; background-image: url('/Content/Images/plus.png');
                  float: left; cursor: pointer;" id="plus<%: item.CategoryId %>" onclick="openContent('#plusContent<%: item.CategoryId %>');">
                </div>
                <div style="width: 250px; min-height: 27px; margin-left: 10px; float: left;">
                  <span style="font-size: 12px; cursor: pointer;">
                    <%=item.CategoryName%>
                  </span>
                  <br />
                  <div id="plusContent<%: item.CategoryId %>" style="display: none; width: auto; height: auto;
                    float: left;" class="plusContent">
                    <ul style="list-style: none; float: left; padding: 0px;">
                      <% foreach (var item2 in Model.CategoryItems.Where(c => c.CategoryParentId == item.CategoryId))
                         { %>
                      <li style="width: auto; float: left; height: auto; margin-left: 10px;">
                        <div style="width: auto; height: auto; float: left;">
                          <% bool checkRelated = false; %>
                          <% foreach (var itemRelated in Model.MainPartyRelatedCategoryItems)
                             { %>
                          <% if (itemRelated.CategoryId == item2.CategoryId)
                             { %>
                          <% checkRelated = true; %>
                          <% } %>
                          <% } %>
                          <%:Html.CheckBox("StoreRelatedIdItems", checkRelated, new { value = item2.CategoryId })%>
                        </div>
                        <div style="width: auto; height: auto; float: left; margin-left: 3px; margin-top: 4px;">
                          <%: item2.CategoryName%></div>
                      </li>
                      <% } %>
                    </ul>
                  </div>
                </div>
              </div>
              <% } %>
            </div>
          </div>
          <% } %>
        </div>
      </td>
    </tr>
  </table>
</asp:Content>
