<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MemberModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  IndividualEdit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
  <script src="/Scripts/JQuery-dropdowncascading.js" type="text/javascript"></script>
  <script type="text/javascript">
    $(document).ready(function () {
      $('#tabs').tabs();

      $('.ui-widget-header').addClass('ui-state-default');
      $('div.ui-tabs').removeClass('ui-widget-content');

    });

    function openContent(content) {
      $(content).slideToggle();
    }
     
  </script>
  <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <div style="float: left; width: 800px; margin-top: 10px;">
    <%using (Html.BeginPanel())
      { %>
    <table border="0" cellpadding="0" cellspacing="0" width="750px" style="float: left">
      <tr>
        <td>
          <div id="tabs" style="margin: 0px;">
            <% using (Html.BeginForm())
               { %>
            <ul>
              <li><a href="#tabs-1">Genel Bilgiler</a></li>
              <li><a href="#tabs-2">İletişim Bilgileri</a></li>
              <li><a href="#tabs-3">İlgilendiği Sektörler</a></li>
              <li><a href="#tabs-4">Mesajlar</a></li>
              <li><a href="#tabs-5">Mağazalar</a></li>
            </ul>
            <div id="tabs-1">
              <table border="0" cellpadding="5" cellspacing="0">
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.MemberNo) %>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%:Model.MemberNo%>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.MemberName) %>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%:Model.MemberName %>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.MemberSurname) %>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%:Model.MemberSurname %>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.MemberEmail) %>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%:Model.MemberEmail %>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.MemberPassword) %>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%:Model.MemberPassword %>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.Active) %>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <% if (Model.Active)
                       { %>
                    Aktif
                    <% }
                       else
                       { %>
                    Pasif
                    <% } %>
                  </td>
                  <td>
                  </td>
                </tr>
                <tr>
                  <td>
                    <%: Html.LabelFor(m => m.BirthDate) %>
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                    <%:Model.BirthDate.ToString("dd.MM.yyyy")%>
                  </td>
                  <td>
                  </td>
                </tr>
              </table>
            </div>
            <div id="tabs-2">
              <div style="width: 90%; padding-bottom: 10px">
                <div id="phoneList">
                  <%= Html.RenderHtmlPartial("PhoneList", Model.PhoneList) %>
                </div>
              </div>
              <div style="width: 90%; padding-bottom: 10px; margin-top: 10px;">
                <div id="addressList">
                  <%= Html.RenderHtmlPartial("AddressList", Model.AddressList) %>
                </div>
              </div>
            </div>
            <div id="tabs-3">
              <div style="width: 570px; height: auto; float: left;">
                <% foreach (var item in Model.SectorItems)
                   { %>
                <ul style="list-style-type: none; padding: 0px;">
                  <li><span style="font-size: 12px; cursor: pointer;">
                    <% if (Model.CategoryItems.Any(c => c.CategoryParentId == item.CategoryId && Model.MainPartyRelatedSectorItems.Any(a => c.CategoryId == a.CategoryId)))
                       { %>
                    <%=item.CategoryName%>
                    <% } %>
                  </span></li>
                  <% foreach (var item2 in Model.CategoryItems.Where(c => c.CategoryParentId == item.CategoryId))
                     { %>
                  <% foreach (var itemRelated in Model.MainPartyRelatedSectorItems)
                     { %>
                  <% if (itemRelated.CategoryId == item2.CategoryId)
                     { %>
                  <li style="padding-left: 30px; margin-top: 5px; margin-bottom: 5px;">•&nbsp;<%: item2.CategoryName%></li>
                  <% } %>
                  <% } %>
                  <% } %>
                  <% } %>
                </ul>
              </div>
            </div>
            <div id="tabs-4">
            </div>
            <div id="tabs-5">
            </div>
            <% } %>
          </div>
        </td>
      </tr>
    </table>
    <%} %>
  </div>
</asp:Content>
