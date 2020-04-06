<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StoreModel>" %>
<% foreach (var item in Model.StoreDealerItems)
   { %>
<div style="width: auto; height: 120px; float: left; margin-right: 30px; margin-top: 10px;">
  <div style="width: auto; height: 20px; padding-left: 7px; font-size: 12px;">
    <%:item.DealerName%>
  </div>
  <div style="width: auto; height: 90px; float: left; margin-top: 10px;">
    <%foreach (var itemPicture in Model.PictureItems.Where(c => c.StoreDealerId == item.StoreDealerId))
      {%>
    <div style="width: 50px; height: auto; float: left; margin-left: 7px;">
      <div style="width: 100%; height: auto; float: left;">
        <img width="50" height="70" src="<%:AppSettings.StoreDealerImageFolder + itemPicture.PicturePath %>" />
      </div>
      <div style="width: 100%; height: auto; float: left; text-align: center; margin-top: 4px;">
        <span style="font-size: 11px; color: #d73434; cursor: pointer;" onclick="DeletePicture('<%: itemPicture.PictureId %>');">
          Sil</span>
      </div>
    </div>
    <%} %>
  </div>
</div>
<% } %>