﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTBannerModel>" %>
<% if (Model.BannerType == (byte)BannerType.CategoryHorizontalBlankBanner)
   { %>
        <div class="row mb20 mt20">
            <div class="col-xs-12 text-center">
               <a href="/kategori-sayfalari-reklamlari-y-143130" target="_blank" >
                    <img src="/Content/banners/728x90banner.gif" alt="<%: Model.CategoryId  %> - Reklam alanı " class="img-thumbnail">
               </a>
            </div>
        </div>
  <%}
   else
   {%>
      <div class="row mb20">
            <div class="col-xs-12 text-center">
                    <% if (Model.BannerResource.Contains(".gif"))
                      {  %>
                         <a href="<%:Model.BannerLink %>"  target="_blank" >
                            <img src="<%:AppSettings.BannerImagesThumbFolder  + Model.BannerResource %>" class="img-thumbnail" alt="<%:!string.IsNullOrEmpty(Model.BannerDescription) ? Model.BannerDescription : "" %>" />
                         </a> 
                    <%}
                      else 
                      {%>
                         <a href="<%:Model.BannerLink %>" target="_blank">
                            <img src="<%:AppSettings.BannerImagesThumbFolder  + Model.BannerResource %>"  class="img-thumbnail" alt="<%:!string.IsNullOrEmpty(Model.BannerDescription) ? Model.BannerDescription : "" %>" />
                         </a>
                      <%} %>
            </div>
        </div>   
   <%}%>
