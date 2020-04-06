<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NeoSistem.MakinaTurkiye.Web.Models.Home.MTStoreNewItem>>" %>

<%if (Model.Count > 0)
    {%>

    <div class="">
        <h2 class="section-title title-news">
            <span>Firmalardan Haberler <a href="<%:AppSettings.SiteAllNewUrl %>?newType=<%:(byte)StoreNewType.Normal %>" style="font-size: 12px;">(Tümünü Gör)</a></span>
        </h2>
    </div>
    <div class="">
        <div class="panel panel-home-new col-xs-12">
            <div class="panel-body news-body">
                <%foreach (var item in Model.ToList())
                    {%>
                <div style="margin-bottom:5px;">
                    <div class="new-container">
                        <div class="new-image pull-left">
                            <a href="<%:item.NewUrl %>">
                                <img src="<%:item.ImagePath %>" style="width: 50px; height: 50px;" alt="<%:item.Title %>" title="<%:item.Title %>" class="img-responsive img-thumbnail" /></a>
                        </div>
                        <div class="pull-left new-title">
                            <span><a href="<%:item.NewUrl %>" title="<%:item.Title %>">
                                <%string title = item.Title;
                                    if (item.Title.Length > 55) { title = item.Title.Substring(0, 55) + ".."; }%>
                                <%:title %>

                                  </a></span>
                        </div>
                        <div class="pull-right new-date">
                            <span class="date small-gray"><i><%:item.Date %></i></span>
                        </div>
                        <div style="clear: both"></div>
                    </div>
           
                </div>
                <% } %>
            </div>

        </div>
    </div>

<% } %>
