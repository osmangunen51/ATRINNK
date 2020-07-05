<%@ Control Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTStoreProfileHeaderModel>" %>
<%string styleBackground = "";
    if (!string.IsNullOrEmpty(Model.StoreBanner))
    {
        styleBackground = "background-image: url('" + Model.StoreBanner + "?v=" + DateTime.Now.ToString("yyyyMMddHHmmss") + ");";
    }
    else
    {
        styleBackground = "background-image: url('/Content/V2/images/cover1.jpg');";
    }
%>
    <%:Html.Hidden("hdnStoreMainPartyId",Model.MainPartyId) %>
<div class="store-profile-header">
	<div class="store-profile-header__cover hidden-xs" style="<%: styleBackground%>">


	</div>
    <div class="store-profile-header__cover visible-xs" style="background-image: url(&quot;/Content/V2/images/cover1.jpg&quot;);" > </div>
	<div class="store-profile-header__info">
		<div class="container">
			<div class="row">
				<div class="col-sm-5 col-md-4 col-lg-3">
					<div class="store-profile-header__brand-logo">
						<img src="<%=Model.StoreLogoPath %>" title="<%:Model.StoreName %>" alt="<%:Model.StoreName %>">
					</div>
				</div>
				<div class="col-sm-7 col-md-8 col-lg-9 store-profile-header__description">
					<h1 class="store-profile-header__brand-name">
						<%if (!string.IsNullOrEmpty(Model.StoreShortName)) { %>
							<%=Model.StoreShortName %>
            
						<%} else { %>
							<%=Model.StoreName %>
						<%} %>

						<%if (!string.IsNullOrEmpty(ViewBag.Categoryh1)) { %>
							<span class="small"><%:ViewBag.Categoryh1 %></span>
						<%} %>
              
					</h1>
                              <%if (!string.IsNullOrEmpty(Model.StoreShortName)) {%>
                        <span class="hidden-xs" style="font-size:14px; color:#0c3871; font-weight:500"><%=Model.StoreName %></span>
                        <% } %>
					<% if (!string.IsNullOrWhiteSpace(Model.StoreAbout)){ %>
						<h2 class="store-profile-header__brand-about" style="margin-top:5px;"><%=Model.StoreAbout %></h2>
					<%} %>
				</div>
			</div>
		</div>
	</div>
</div>