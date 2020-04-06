<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StoreSearchModel>" %>
<div class="tab-pane fade in" id="pencere">
  <% foreach (var item in Model.Source)
     { %>
  <%string url = "/sirket/" + item.MainPartyId.ToString() + "/" + Helpers.ToUrl(item.StoreName) + "/sirketprofili";  %>
  <div class="col-sm-6 col-md-3">
        <div class="thumbnail thumbnail-mt">

         <div class="thumbnail thumbnail-mt">
        <a href="<%= url %>">
            <img src="<%= ImageHelpers.GetStoreImage(item.MainPartyId,item.StoreLogo,"300") %>" class="img-thumbnail border0" alt="<%:item.StoreName%>"/>
        </a>

    <div class="caption text-center">
      <a href="<%= url %>">
            <%:Html.Truncate(item.StoreName, 45)%></a>
        <p>
            <%:Html.Truncate(Html.FirstLetterLower(item.StoreAbout) , 45)%>
        </p>
    </div>
    
    </div>
      </div>
  </div>
  <%  } %>
  <span class="clearfix"></span>
</div>

