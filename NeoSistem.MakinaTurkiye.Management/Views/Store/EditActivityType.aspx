<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  MemberStore
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%=Html.RenderHtmlPartial("Style") %>
  <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginPanel())
    { %>
  <%=Html.RenderHtmlPartial("TabMenu") %>
  <% using (Html.BeginForm("EditActivityType", "Store", FormMethod.Post))
     { %>
  <div style="width: 600px; float: left; margin: 20px 0px 0px 20px">
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
