﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ICollection<CategoryModel>>" %>

        <%if (Model != null)
          {  %>
     
          <ul style="list-style: none; float: left; padding: 0px; width: 580px; border: solid 1px #8fc0d1;
              padding-top: 10px; padding-left: 10px; background-color: #f8fbff">
                 <%foreach (var item in Model)
                   {  %>
                              <li style="float: left; width: 240px;">
                <div style="width: auto; float: left; height: auto;">
                  <div style="width: auto; float: left; height: auto;">
                  <%: Html.RadioButton("CategoryId", item.CategoryId, false)%> 
                  </div>
                  <div style="width: auto; float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                    color: #000">
                    <%:item.CategoryName%>
                  </div>
                </div>
              </li>
              <%} %>
              </ul>
        <%} %>

