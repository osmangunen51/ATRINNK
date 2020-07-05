﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTVideoPagingModel>" %>
<%if (Model.TotalPageCount > 0)
  { %>
<div class="row">
    <div class="col-md-12 text-center">
        <ul class="pagination m0">
            <%
      string[] str1 = Request.Url.ToString().Split('?');
      if (Model.TotalPageCount > 0)
      {
          str1[0] += "?currentPage=1";
            %>
            <li><a href="<%= str1[0]%>">&laquo; </a></li>
            <%} %>
      
                  <%  for (int i = Model.FirstPage; i <= Model.LastPage; i++)
                       {
                           string[] str = Request.Url.ToString().Split('?');
                           str[0] += "?currentPage=" + i;
                           if (Model.TotalPageCount >= i)
                           {
                               if (i == Model.CurrentPage)
                               { %>
                               <li class="active"><a href="<%=str[0]%>">
                                    <%=i.ToString()%></a> </li>  
                               <%}
                               else
                               {%>
                                    <li><a href="<%=str[0]%>" >
                                    <%=i.ToString()%></a> </li>  
                               <%} 
                           } %>
                               
                    <%} %>
           
      <%if (Model.TotalPageCount > 0)
      {
          str1 = Request.Url.ToString().Split('?');
          str1[0] += "?currentPage=" + Model.TotalPageCount;
      }
            %>
            <%
      if (Model.TotalPageCount > 0)
      { %>
            <li><a href="<%= str1[0]%>">&raquo; </a></li>
            <% } %>
        </ul>
    </div>
    
    <div class="col-md-12 text-center">
        Toplam
        <%=Model.TotalPageCount %>
        sayfa içerisinde
        <%=Model.CurrentPage %>. sayfayı görmektesiniz.
    </div>
</div>
<%} %>
