﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  MemberStore
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%=Html.RenderHtmlPartial("Style") %>
  <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
  <script type="text/javascript">
    function openContent(content) {
      $(content).slideToggle();
    }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginPanel())
    { %>
  <%=Html.RenderHtmlPartial("TabMenu") %>
  <% using (Html.BeginForm("EditActivityCategory", "Store", FormMethod.Post))
     { %>
  <div style="width: 600px; float: left; margin: 20px 0px 0px 20px">
    <div style="width: 570px; height: auto; float: left;">
      <% foreach (var item in Model.SectorItems.Where(c=>c.MainCategoryType==(byte)MainCategoryType.Ana_Kategori).OrderBy(c => c.CategoryName).OrderBy(c => c.CategoryOrder))
         { %>
      <div style="width: 400px; float: left; margin-left: 10px;">
        <div style="width: 14px; height: 14px; background-image: url('/Content/Images/plus.png');
          float: left; cursor: pointer;" id="plus<%: item.CategoryId %>" onclick="openContent('#plusContent<%: item.CategoryId %>');">
        </div>
        <div style="width: 250px; min-height: 27px; margin-left: 10px; float: left;">
          <span style="font-size: 12px; cursor: pointer;">
            <%=item.CategoryName%>
          </span>
          <br />
          <div id="plusContent<%: item.CategoryId %>" style="width: 375px; min-height: 27px;
            margin-left: 10px; float: left; display: none;" class="plusContent">
            <ul style="list-style: none; float: left; padding: 0px; width: 730px; border: solid 1px #8fc0d1;
              padding-top: 10px; padding-bottom: 10px; padding-left: 10px; background-color: #f8fbff;">
              <% foreach (var item2 in Model.CategoryGroupParentItemsByCategoryId(item.CategoryId).OrderBy(c => c.CategoryName).OrderBy(c => c.CategoryOrder))
                 { %>
              <li style="float: left; width: 240px;">
                <div style="width: auto; height: auto; float: left;">
                  <% bool checkRelated = false; %>
                  <% foreach (var itemRelated in Model.StoreActivityCategory)
                     { %>
                  <% if (itemRelated.CategoryId == item2.CategoryId)
                     { %>
                  <% checkRelated = true; %>
                  <script type="text/javascript">
                    $('#plusContent<%: item.CategoryId %>').show();
                  </script>
                  <% } %>
                  <% } %>
                  <%:Html.CheckBox("StoreActivityCategory", checkRelated, new { value = item2.CategoryId })%>
                </div>
                <div style="width: auto; height: auto; float: left; margin-left: 3px; margin-top: 4px;">
                  <%: item2.CategoryContentTitle%></div>
              </li>
              <% } %>
            </ul>
          </div>
        </div>
      </div>
      <% } %>
    </div>
    <div style="margin: 20px auto; padding-top: 10px; border-top: solid 1px #DDD; width: 99%;
      float: left">
      <button type="submit">
        Kaydet</button>
      <button type="reset">
        İptal</button>
    </div>
  </div>
  <% } %>
  <%} %>
</asp:Content>
