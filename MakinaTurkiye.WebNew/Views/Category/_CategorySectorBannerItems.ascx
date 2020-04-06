﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<MTBannerModel>>" %>


<div class="row">
   
   <% string gorunum = Request.QueryString["Gorunum"];
    if (gorunum != "3") 
    {  
    int i = 0;
    
    if (Model != null && Model.Any(c => c.BannerType == (byte)BannerType.CategoryBanner1))
    { 
    var banner1 = Model.SingleOrDefault(c => c.BannerType == (byte)BannerType.CategoryBanner1); 

    if (banner1 != null)
    { 
        %>
         <div class="hidden-xs col-sm-6 col-md-3 mtb20">
            <div class="thumbnail thumbnail-mt2">           
       
            <% if (banner1.BannerResource.Contains(".gif"))
            {
               %>
               <a href="http://<%:banner1.BannerLink %>"><img src="<%:AppSettings.BannerGifFolder  + banner1.BannerResource %>" alt="<%:!string.IsNullOrEmpty(banner1.BannerDescription) ? banner1.BannerDescription : "Banner Reklam" %>"/></a>
                <div class="caption">
                    <a href="http://<%:banner1.BannerLink %>"><%:banner1.BannerDescription %></a>
                </div>
            <%} 
        
              else if (banner1.BannerResource.Contains("swf"))
                 { %>
                  <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0" width="170" height="200" style="z-index:0">
                    <param name="movie" value="<%:AppSettings.BannerFlashFolder + banner1.BannerResource %>">
                    <param name="quality" value="high">
                    <param name="wmode" value="transparent">
                    <param name="menu" value="false">
                    <param name="allowscriptaccess" value="always">
                    <embed src="<%:AppSettings.BannerFlashFolder + banner1.BannerResource %>" quality="high" width="170" height="200" type="application/x-shockwave-flash" wmode="transparent" pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash" allowscriptaccess="always">
                  </object>
              <% }
               else
               {  %>
                <a href="http://<%:banner1.BannerLink %>"><img src="<%:AppSettings.BannerImagesThumbFolder  + banner1.BannerResource %>" alt="<%:!string.IsNullOrEmpty(banner1.BannerDescription) ? banner1.BannerDescription : "Banner Reklam" %>"/></a>
                <div class="caption">
                    <a href="http://<%:banner1.BannerLink %>"><%:banner1.BannerDescription %></a>
                </div>
              <% } %>
            </div>
        </div>
    <% }
    }
    else
    {
       i = i + 1;
       if (i == 1)
       {  
        %>
          <div class="hidden-xs col-sm-6 col-md-3 mtb20">
            <div class="thumbnail thumbnail-mt2">  
        <a href="/help/yardimdetay?CategoryId=143130">
        <img src="../../Content/reklammk.jpg" alt="Buraya Reklam Verebilirsiniz"/></a>
                <div class="caption">
                    <a href="/help/yardimdetay?CategoryId=143130">Buraya Reklam Verebilirsiniz</a>
        </div>
            </div>
            </div>  
    <%  }
    } 
 %>
 
  <!-- Banner 2 -->
   <%  
    
    if (Model != null && Model.Any(c => c.BannerType == (byte)BannerType.CategoryBanner2))
    { 
    var banner2 = Model.SingleOrDefault(c => c.BannerType == (byte)BannerType.CategoryBanner2); 

    if (banner2 != null)
    { 
        %>
         <div class="hidden-xs col-sm-6 col-md-3 mtb20">
            <div class="thumbnail thumbnail-mt2">           
       
            <% if (banner2.BannerResource.Contains(".gif"))
            {
               %>
               <a href="http://<%:banner2.BannerLink %>"><img src="<%:AppSettings.BannerGifFolder  + banner2.BannerResource %>" alt="<%:!string.IsNullOrEmpty(banner2.BannerDescription) ? banner2.BannerDescription : "Banner Reklam" %>"/></a>
                <div class="caption">
                    <a href="http://<%:banner2.BannerLink %>"><%:banner2.BannerDescription %></a>
                </div>
            <%} 
        
              else if (banner2.BannerResource.Contains("swf"))
                 { %>
                  <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0" width="170" height="200" style="z-index:0">
                    <param name="movie" value="<%:AppSettings.BannerFlashFolder + banner2.BannerResource %>">
                    <param name="quality" value="high">
                    <param name="wmode" value="transparent">
                    <param name="menu" value="false">
                    <param name="allowscriptaccess" value="always">
                    <embed src="<%:AppSettings.BannerFlashFolder + banner2.BannerResource %>" quality="high" width="170" height="200" type="application/x-shockwave-flash" wmode="transparent" pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash" allowscriptaccess="always">
                  </object>
              <% }
               else
               {  %>
                <a href="http://<%:banner2.BannerLink %>"><img src="<%:AppSettings.BannerImagesThumbFolder  + banner2.BannerResource %>" alt="<%:!string.IsNullOrEmpty(banner2.BannerDescription) ? banner2.BannerDescription : "Banner Reklam" %>"/></a>
                <div class="caption">
                    <a href="http://<%:banner2.BannerLink %>"><%:banner2.BannerDescription%></a>
                </div>
              <% } %>
            </div>
        </div>
    <% }
    }
    else
    {
       i = i + 1;
       if (i == 1)
       {  
        %>
          <div class="hidden-xs col-sm-6 col-md-3 mtb20">
            <div class="thumbnail thumbnail-mt2">  
        <a href="/Help/YardimDetay?CategoryId=143130">
        <img src="../../Content/reklammk.jpg" alt="Buraya Reklam Verebilirsiniz"/></a>
                <div class="caption">
                    <a href="/Help/YardimDetay?CategoryId=143130">Buraya Reklam Verebilirsiniz</a>
        </div>
            </div>
            </div>  
    <%  }
    } %>
   
     
  <!-- Banner 3 -->
   <%  
    
    if (Model != null && Model.Any(c => c.BannerType == (byte)BannerType.CategoryBanner3))
    { 
    var banner3 = Model.SingleOrDefault(c => c.BannerType == (byte)BannerType.CategoryBanner3); 

    if (banner3 != null)
    { 
        %>
         <div class="hidden-xs col-sm-6 col-md-3 mtb20">
            <div class="thumbnail thumbnail-mt2">           
       
            <% if (banner3.BannerResource.Contains(".gif"))
            {
               %>
               <a href="http://<%:banner3.BannerLink %>"><img src="<%:AppSettings.BannerGifFolder  + banner3.BannerResource %>" alt="<%:!string.IsNullOrEmpty(banner3.BannerDescription) ? banner3.BannerDescription : "Banner Reklam" %>"/></a>
                <div class="caption">
                    <a href="http://<%:banner3.BannerLink %>"><%:banner3.BannerDescription %></a>
                </div>
            <%} 
        
              else if (banner3.BannerResource.Contains("swf"))
                 { %>
                  <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0" width="170" height="200" style="z-index:0">
                    <param name="movie" value="<%:AppSettings.BannerFlashFolder + banner3.BannerResource %>">
                    <param name="quality" value="high">
                    <param name="wmode" value="transparent">
                    <param name="menu" value="false">
                    <param name="allowscriptaccess" value="always">
                    <embed src="<%:AppSettings.BannerFlashFolder + banner3.BannerResource %>" quality="high" width="170" height="200" type="application/x-shockwave-flash" wmode="transparent" pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash" allowscriptaccess="always">
                  </object>
              <% }
               else
               {  %>
                <a href="http://<%:banner3.BannerLink %>"><img src="<%:AppSettings.BannerImagesThumbFolder  + banner3.BannerResource %>" alt="<%:!string.IsNullOrEmpty(banner3.BannerDescription) ? banner3.BannerDescription : "Banner Reklam" %>"/></a>
                <div class="caption">
                    <a href="http://<%:banner3.BannerLink %>"><%:banner3.BannerDescription%></a>
                </div>
              <% } %>
            </div>
        </div>
    <% }
    }
    else
    {
       i = i + 1;
       if (i == 1)
       {  
        %>
          <div class="hidden-xs col-sm-6 col-md-3 mtb20">
            <div class="thumbnail thumbnail-mt2">  
        <a href="/Help/YardimDetay?CategoryId=143130">
        <img src="../../Content/reklammk.jpg" alt="Buraya Reklam Verebilirsiniz"/></a>
                <div class="caption">
                    <a href="/Help/YardimDetay?CategoryId=143130">Buraya Reklam Verebilirsiniz</a>
        </div>
            </div>
            </div>  
    <%  }
    } %>
        
    
 <!-- Banner 4 -->
   <%  
    
        if (Model != null && Model.Any(c => c.BannerType == (byte)BannerType.CategoryBanner4))
    { 
    var banner4 = Model.SingleOrDefault(c => c.BannerType == (byte)BannerType.CategoryBanner4); 

    if (banner4 != null)
    { 
        %>
         <div class="hidden-xs col-sm-6 col-md-3 mtb20">
            <div class="thumbnail thumbnail-mt2">           
       
            <% if (banner4.BannerResource.Contains(".gif"))
            {
               %>
               <a href="http://<%:banner4.BannerLink %>"><img src="<%:AppSettings.BannerGifFolder  + banner4.BannerResource %>" alt="<%:!string.IsNullOrEmpty(banner4.BannerDescription) ? banner4.BannerDescription : "Banner Reklam" %>"/></a>
                <div class="caption">
                    <a href="http://<%:banner4.BannerLink %>"><%:banner4.BannerDescription %></a>
                </div>
            <%} 
        
              else if (banner4.BannerResource.Contains("swf"))
                 { %>
                  <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=5,0,0,0" width="170" height="200" style="z-index:0">
                    <param name="movie" value="<%:AppSettings.BannerFlashFolder + banner4.BannerResource %>">
                    <param name="quality" value="high">
                    <param name="wmode" value="transparent">
                    <param name="menu" value="false">
                    <param name="allowscriptaccess" value="always">
                    <embed src="<%:AppSettings.BannerFlashFolder + banner4.BannerResource %>" quality="high" width="170" height="200" type="application/x-shockwave-flash" wmode="transparent" pluginspage="http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash" allowscriptaccess="always">
                  </object>
              <% }
               else
               {  %>
                <a href="http://<%:banner4.BannerLink %>"><img src="<%:AppSettings.BannerImagesThumbFolder  + banner4.BannerResource %>" alt="<%:!string.IsNullOrEmpty(banner4.BannerDescription) ? banner4.BannerDescription : "Banner Reklam" %>"/></a>
                <div class="caption">
                    <a href="http://<%:banner4.BannerLink %>"><%:banner4.BannerDescription%></a>
                </div>
              <% } %>
            </div>
        </div>
    <% }
    }
    else
    {
       i = i + 1;
       if (i == 1)
       {  
        %>
          <div class="hidden-xs col-sm-6 col-md-3 mtb20">
            <div class="thumbnail thumbnail-mt2">  
        <a href="/Help/YardimDetay?CategoryId=143130">
        <img src="../../Content/reklammk.jpg" alt="Buraya Reklam Verebilirsiniz"/></a>
                <div class="caption">
                    <a href="/Help/YardimDetay?CategoryId=143130">Buraya Reklam Verebilirsiniz</a>
        </div>
            </div>
            </div>  
    <%  }
    }
}%>
</div>
