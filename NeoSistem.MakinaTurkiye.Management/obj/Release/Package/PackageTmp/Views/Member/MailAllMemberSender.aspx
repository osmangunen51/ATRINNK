﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script src="/Scripts/JQuery-qtip.js" type="text/javascript"></script>
  <link href="/Content/qtip.css" rel="Stylesheet" type="text/css" />
    <title>MailAllMemberType</title>
    <script type="text/javascript">
      $(document).ready(function () {

        $("input[name='RelatedCategory']", $('#cons')).change(function (e) {
          if ($(this).val() === '1') {
            $("#bireyselyadakurumsal").hide();
          }
          else if ($(this).val() === '2') {
            $("#bireyselyadakurumsal").hide();
          }
          else if ($(this).val() === '3') {
            $("#bireyselyadakurumsal").show();
          }
          else if ($(this).val() === '4') {
            $("#bireyselyadakurumsal").show();
          }
        });
      });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div style="width: 595px; float:left;">
  <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px;
    border-width: 5px;" id="preLoading">
    <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
    <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
  </div>
  <div style="width: 100%; margin: 0 auto;">
    <input type="hidden" name="OrderName" id="OrderName" value="ProductId" />
    <input type="hidden" name="Order" id="Order" value="DESC" />
    <input type="hidden" name="Page" id="Page" value="1" />
    <input type="hidden" name="ProductStatu" id="ProductStatu" value="<%: ViewData["ProductStatu"] ?? 0 %>" />
    <table cellpadding="19" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" style="width: 5%" unselectable="on" onclick="ProductPost('ProductNo', this);">
            Log Id
          </td>
          <td class="Header" style="width: 9%" unselectable="on" onclick="ProductPost('ProductName', this);">
            Log Adı
          </td>
          <td class="Header" style="width: 6%" unselectable="on" onclick="ProductPost('CMain.CategoryName', this);">
            Açıklama
          </td>
          <td class="Header" style="width: 6%" unselectable="on" onclick="ProductPost('CBrand.CategoryName', this);">
           Gönderilen sayısı
          </td>
          <td class="Header" style="width: 5%" unselectable="on" onclick="ProductPost('CSeries.CategoryName', this);">
            gönderilemeyen Sayısı
          </td>
          <td class="Header" style="width: 5%" unselectable="on" onclick="ProductPost('CModel.CategoryName', this);">
            Başlangıç
          </td>
          <td class="Header" style="width: 5%" unselectable="on">
            bitiş
          </td>
          <td class="Header" style="width: 5%" unselectable="on" onclick="ProductPost('OtherBrand', this);">
           a
          </td>
          <td class="Header" style="width: 5%" unselectable="on" onclick="ProductPost('OtherModel', this);">
           a
          </td>
          <td class="Header" style="width: 5%" unselectable="on">
            a
          </td>
          <td class="Header" style="width: 2%" unselectable="on">
            a
          </td>
          <td class="Header" style="width: 6%" unselectable="on" onclick="ProductPost('MainPartyFullName', this);">
            a
          </td>
          <td class="Header" style="width: 9%" unselectable="on" onclick="ProductPost('StoreName', this);">
            a
          </td>
          <td class="Header" style="width: 5%" unselectable="on">
            a
          </td>
          <td class="Header" style="width: 3%" unselectable="on" onclick="ProductPost('ProductPrice', this);">
            a
          </td>
          <td class="Header" style="width: 5%" unselectable="on" onclick="ProductPost('ProductRecordDate', this);">
            a
          </td>
          <td class="Header" style="width: 5%" unselectable="on" onclick="ProductPost('ProductLastViewDate', this)">
            a
          </td>
          <td class="Header" style="width: 6%">
            a
          </td>
          <td class="Header" style="width: 3%" unselectable="on">
          </td>
        </tr>
        <tr style="background-color: #F1F1F1;">
          <td class="CellBegin" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="ProductNo" class="Search" style="width: 100%; border: none;" value="#" />
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" align="center" style="width: 9%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="ProductName" class="Search" style="width: 60%; border: none;" /><span
                      class="ui-icon ui-icon-close searchClear" onclick="clearSearch('ProductName');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" align="center" style="width: 6%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="FirstCategoryName" class="Search" style="width: 60%; border: none;" /><span
                      class="ui-icon ui-icon-close searchClear" onclick="clearSearch('FirstCategoryName');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 6%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="NameBrand" class="Search" style="width: 60%; border: none" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('NameBrand');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="NameSeries" class="Search" style="width: 40%; border: none" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('NameSeries');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="width: 100%; padding-right: 5px; border: solid 1px #CCC; background-color: #FFF">
                    <input id="NameModel" class="Search" style="width: 60%; border: none" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('NameModel');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="OtherBrand" class="Search" style="width: 60%; border: none" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('OtherBrand');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="OtherModel" class="Search" style="width: 60%; border: none" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('OtherModel');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
          </td>
          <td class="Cell" style="width: 2%">
          </td>
          <td class="Cell" style="width: 6%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="UserName" class="Search" style="width: 50%; border: none" />
                    <span class="ui-icon ui-icon-close searchClear" onclick="clearSearch('UserName');">
                    </span>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 9%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="StoreName" class="Search" style="width: 100%; border: none" />
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td>
                    <select id="MemberType" name="MemberType" onchange="SearchPost();" w="75px">
                      <option value="0">< Seçiniz ></option>
                      <option value="20">Kurumsal</option>
                      <option value="10">Bireysel</option>
                    </select>
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 3%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="ProductPrice" class="Search" style="width: 100%; border: none" />
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="width: 100%; padding-right: 5px; border: solid 1px #CCC; background-color: #FFF">
                    <input id="ProductRecordDate" class="Search date" style="width: 100%; border: none" />
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 5%">
            <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
              <tbody>
                <tr>
                  <td style="border: solid 1px #CCC; background-color: #FFF">
                    <input id="ProductLastViewDate" class="Search date" style="width: 100%; border: none" />
                  </td>
                </tr>
              </tbody>
            </table>
          </td>
          <td class="Cell" style="width: 6%">
          </td>
          <td class="CellEnd" align="center" style="width: 3%">
            <input type="checkbox" class="ch" onclick="check();" />
          </td>
        </tr>
      </thead>
      <tbody id="table">
      <%MakinaTurkiyeEntities entities=new MakinaTurkiyeEntities();
        var mod=entities.MailLogs.ToList();
         %>
      <% int row = 0; %>
<% foreach (var item in mod)
   { %>
<% row++; %>
<tr>
  <td class="CellBegin">
    <%: item.MailLogId%>
  </td>
  <td class="Cell">
    <span>
      <%: item.MaillogTypeName%></span>
  </td>
  <td class="Cell">
    <%= item.Explanesth%>
  </td>
  <td class="Cell">
    <%: item.MaillogCount%>
  </td>
  <td class="Cell">
    <%: item.MaillogFailCount%>
  </td>
  <td class="Cell">
    <%: item.MaillogStart%>
  </td>
  <td class="Cell" style="text-align: center; padding-top: 5px">
    <%:item.MaillogFinish%>
  </td>
  <td class="Cell">
    b
  </td>
  <td class="Cell">
   b
  </td>
  <td class="Cell">
  b
  </td>
  <td class="Cell">
   b
  </td>
  <td class="Cell">
    b
  </td>
  <td class="Cell">
   b
  </td>
  <td class="Cell">
   b
  </td>
  <td class="Cell">
    b
  </td>
  <td class="Cell">
    b
  </td>
  <td class="Cell">
  </td>
  <td class="Cell" align="center">
a
  </td>
  <td class="CellEnd" align="center">
 a
  </td>
</tr>
<%} %>

      </tbody>
    </table>
  </div>

  </div>
</asp:Content>
