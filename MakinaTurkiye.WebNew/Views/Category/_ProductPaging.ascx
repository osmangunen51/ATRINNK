﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTProductPagingModel>" %>
<%if (Model.TotalPageCount > 0)
  { %>
<div class="row">
    <div class="col-md-12 text-center">
        <ul class="pagination" style="margin-bottom:20px">
            <li><a href="<%=  Model.FirstPageUrl%>">&laquo; </a></li>

            <%  foreach(var key in Model.PageUrls.Keys)
                {
                    if (Model.TotalPageCount >= key)
                    {
                        if (key == Model.CurrentPageIndex)
                        { %>
                               <li class="active"><a href="<%=Model.PageUrls[key]%>">
                                    <%=key.ToString()%></a> </li>  
                               <%}
                                   else
                                   {
                                       
                                       %>
                                    <li><a href="<%=(key==1)?Model.FirstPageUrl:Model.PageUrls[key]%>" >
                                    <%=key.ToString()%></a> </li>  
                               <%}
                    } %>
                               
                    <%} %>

            <li><a href="<%= Model.LastPageUrl%>">&raquo; </a></li>
        </ul>
    </div>
    <div class="col-md-12 text-center">
        Toplam
        <%=Model.TotalPageCount %>
        sayfa içerisinde
        <%=Model.CurrentPageIndex%>. sayfayı görmektesiniz.
    </div>
</div>
<%} %>
