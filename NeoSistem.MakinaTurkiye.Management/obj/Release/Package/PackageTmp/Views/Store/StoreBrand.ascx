<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<StoreBrand>>" %>
<% foreach (var item in Model)
   { %>
<div style="width: 300px; height: 70px; float: left; margin-bottom: 10px; text-align: left;">
  <div style="width: auto; height: auto; float: left;">
    <img width="50" height="50" style="float: left;" src="<%= FileHelpers.ImageThumbnailName(AppSettings.StoreBrandImageFolder+ item.BrandPicture) %>" />
  </div>
  <div style="float: left; margin-left: 10px; width: 230px; height: 60px;">
    <span style="font-weight: bold; font-size: 12px;">
      <%=item.BrandName%></span>
    <br />
    <span style="font-size: 12px;">
      <%= Html.Truncate( item.BrandDescription, 80)%></span>
    <br />
    <a style="font-size: 12px; color: Red; cursor: pointer;" onclick="DeleteStoreBrand('<%:item.StoreBrandId %>');">
      Sil</a>
  </div>
</div>
<% } %>