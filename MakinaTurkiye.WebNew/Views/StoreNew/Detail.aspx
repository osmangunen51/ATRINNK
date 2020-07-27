﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.StoreNews.MTNewDetailModel>" %>
<asp:Content ID="Content5" ContentPlaceHolderID="HeaderContent" runat="server">
        <link rel="canonical" href="<%:ViewBag.Canonical %>" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div class="fast-access-bar hidden-xs">
        <div class="fast-access-bar__inner">
            <div class="row clearfix">
                <div class="col-xs-12 col-md-6">
                    <ol class="breadcrumb breadcrumb-mt">
                        <li><a target="_self" href="<%:AppSettings.SiteAllNewUrl %>">Haberler</a></li>
                        <li class="active"><%:Model.Title %></li>
                    </ol>

                </div>
            </div>
        </div>
    </div>
    <div class="row">

        <div class="col-md-9 new-container-detail">

            <div class="new-detail-title pull-left">
                <h1><%:Model.Title %></h1>
            </div>
            <%if (Model.UpdateDate != Model.RecordDate)
                {%>
            <div class="pull-right new-item-update">
                <%:Model.UpdateDate.ToString("dd MMM yyyy",CultureInfo.InvariantCulture) %> güncellendi
            </div>
            <% } %>

            <div style="clear: both"></div>
            <div class="new-detail-info row">
                <div class="col-md-12 new-data" style="color:#808080">
                    <%:Model.RecordDate.ToString("dd MMM yyyy",CultureInfo.InvariantCulture) %>, <b><%:Model.NewStoreModel.StoreName %></b> tarafından eklendi
                </div>
            </div>
            <%if (!string.IsNullOrEmpty(Model.ImagePath)) {%>
                            <div class="new-detail-image">
                <img src="<%:Model.ImagePath %>" title="<%:Model.Title %>" alt="<%:Model.Title %>" class="img-responsive" />
            </div>

            <% } %>

            <div class="new-detail-content row">
                <div class="col-md-12">
                    <%=Html.Raw(Model.Content) %>
                </div>
            </div>
            <div class="new-detail-store row">
                <div class="col-md-12">
                    <div class="pull-left">
                        <a href="<%:Model.NewStoreModel.StoreUrl %>" title="<%:Model.NewStoreModel.StoreName %>" class="pull-left">
                            <img src="<%:Model.NewStoreModel.StoreLogoPath %>" class="img-rounded img-responsive" alt="<%:Model.NewStoreModel.StoreName%>" style="border: 1px solid #ccc;" />
                        </a>
                    </div>
                    <div class="pull-left new-store-info" style="margin-left:5px;">
                        <a href=""><%:Model.NewStoreModel.StoreName %></a>
                        <br />
                        <%foreach (var item in Model.NewStoreModel.Phones)
                            {%>
                                    <a href="tel:<%:item %>"><%:item %></a><br />
                            <%} %>
            
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
        <div class="col-md-3 ">
            <%=Html.RenderHtmlPartial("_OtherNews",Model.NewOthers) %>
        </div>
    </div>
</asp:Content>
