<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTOtherVideosModel>" %>
<div class="col-sm-12 col-md-4">
            <div class="panel panel-mt2">
                <div class="panel-heading margin0" style="padding:0px;height:40px; padding-top:10px; padding-left:10px;">
                    <div class="pull-left">
                    <i class="fa fa-play"></i>&nbsp;&nbsp;Sıradaki
                    </div>
                      <%string cssAutomatic = "";
                        string checked1 = "checked";
                        bool status = (bool)Model.VideoAutomaticStatus;
                        if (status == true)
                            checked1 = "checked";
                        else
                            checked1 = "";
                        if (Model.MTVideoModels.Count == 0) { cssAutomatic = "display:none;"; checked1 = ""; } %>
                    <div class="pull-right" style="margin-top:-7px;<%:cssAutomatic%>" >
                 <a href="#" class="pull-left" style="margin-right:5px;margin-top:7px; font-size:11px; color:#808080;" data-toggle="tooltip" data-placement="top" title="Sıradaki Video için otomatik geçiş yapar" > Otomatik Oynat
                 </a>
                         <label class="switch">
                          <input id="automaticPlay" <%:checked1 %> type="checkbox">
                          <div class="slider round"></div>
                        </label>
                     </div>
                    </div>
                <ul class="media-list panel-body">
                    <% if (Model.MTVideoModels.Count == 0)
                        {%>
                Herhangi bir video bulunamadı.
                <%}
                    else
                    {
                        foreach (var modelItem in Model.MTVideoModels)
                        {
                %>
                    <li class="media" id="V_<%= modelItem.VideoId %>">
                        <a class="pull-left" href="<%:modelItem.VideoUrl %>">
                        <% if (FileHelpers.HasFile(modelItem.PicturePath))
                            { %>
                                  <div class="videos-minuteOther">
                           <span class="text-mutedx"><%=modelItem.VideoMinute%>:<%=modelItem.VideoSecond%></span>
                         </div>
                        <img class="media-object" src="<%=modelItem.PicturePath %>" width="120" alt="<%=modelItem.TruncateProductName %>" />
                        <%}
                            else
                            {%>
                        <img class="media-object" src="https://dummyimage.com/120x80/efefef/000000.jpg&text=Video Resmi Bulunamadı" alt="...">
                        <%} %>
                    </a>
                        <div class="media-body">
                            <div class="media-heading">
                                <a href="<%:modelItem.VideoUrl %>" title="<%= modelItem.CategoryName %>" class="text-info">
                                    <%=modelItem.TruncateProductName%> </a>
                                <br />
                                <a href="#" class="text-muted text-s"><%=modelItem.StoreName%> </a>
                                <br />
                                <span class="text-muted text-xs"><%=modelItem.SingularViewCount%> görüntüleme </span>
                
                          </div>
                           
                        </div>
                 
                    </li>
                    <%}
                    } %>
                </ul>
            </div>
        </div>