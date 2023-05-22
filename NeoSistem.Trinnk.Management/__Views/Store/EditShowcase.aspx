﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IList<Store>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  MemberStore
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%=Html.RenderHtmlPartial("Style") %>
  <script src="/Scripts/Trinnk.js" type="text/javascript" defer="defer"></script>
  <style type="text/css">
    .row
    {
      width: 460px;
      float: left;
      margin-left: 15px;
      
    }
    .row:hover
    {
      background-color: #efefef;
    }
  </style>
  <script type="text/javascript">
    function check(id) {
      if (confirm('Fİrmayı vitrinden çıkarmak istediğinizden eminmisiniz ?')) {
        $('#StoreId').val(id);
        return true;
      }
      return false;
    }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginPanel())
    { %>
  <% using (Html.BeginForm("EditShowcase", "Store", FormMethod.Post))
     { %>
  <div style="float: left; width: 90%; margin: 20px 0px 0px 25px">
    <div style="float: left; width: 30%; border-bottom: dashed 1px #bababa; height: 20px;">
      <span style="font-size: 13px; font-weight: bold;">Vitrindeki Firmalar</span>
    </div>
  </div>
  <div style="width: 950px; float: left; margin: 20px 0px 20px 5px">
    <% foreach (var item in Model)
       { %>
    <div class="row">
      <div style="float: left; width: 415px; height: 21px; padding-top: 4px; padding-left: 5px;">
        <span style="font-size: 12px;">
          <%:item.StoreName %></span>
      </div>
      <div style="float: left; width: 35px; height: 25px;">
        <button type="submit" onclick="return check(<%:item.MainPartyId %>);">
          Sil</button>
      </div>
      <input type="hidden" id="StoreId" name="MainPartyId" />
    </div>
    <% } %>
  </div>
  <% } %>
  <%} %>
</asp:Content>
