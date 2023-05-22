<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<DealerBrand>>" %>
<% foreach (var item in Model)
   { %>
<div style="width: 90px; height: 70px; float: left; margin-bottom: 10px; text-align: center">
  <img width="50" height="50" src="<%= AppSettings.DealerBrandImageFolder + item.DealerBrandPicture %>" />
  <br />
  <span style="font-size: 12px;">
    <%=item.DealerBrandName %></span>
  <br />
  <a style="font-size: 12px; color: Red; cursor: pointer;" onclick="DeleteDealerBrand('<%:item.DealerBrandId %>');">
    Sil</a>
</div>
<% } %>