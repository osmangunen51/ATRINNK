<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<MTVideoSearchViewModel>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-5 col-md-4 col-lg-3">
            <div class="panel panel-mt2 left-menu">
                <%=Html.RenderHtmlPartial("_VideoCategories",Model.VideoCategoryModel)%>
            </div>
        </div>
        <div class="col-xs-12 col-sm-7 col-md-8 col-lg-9">
         
            <div class="row hidden-xs">
                <div class="col-xs-12 sektorBack">
                </div>
            </div>

            <div class="alert alert-info tumSektorBack">
                     <%:string.IsNullOrEmpty(Model.VideoCategoryModel.SelectedCategoryName) ? "" : Model.VideoCategoryModel.SelectedCategoryName+" Kategorisinde" %> 
                   "<b><%:Model.SearchText %> </b>" kelimesine ait <%:Model.VideoModels.ToList().Count %> adet video bulundu
            </div>
                              <%if(Model.Store.StoreName!=null){ %>
                           <div class="row" style="margin-top:10px;">
        <div class="col-sm-3">
            <div class="pr text-center border1">
               <a href="<%=Model.Store.StoreVideosPageUrl %>">
                    <img src="<%=Model.Store.StoreLogo %>" class="img-thumbnail border0" alt="<%:Model.Store.StoreName%>"/>
                </a> 
            </div>
        </div>
         <div class="col-xs-3">
           <h4 class="media-heading">
                <a href="<%=Model.Store.StoreVideosPageUrl %>"><%:Model.Store.StoreName%></a>
            <%--   <a><%:item.TruncateStoreName%></a>--%>
           </h4>
            <%:Model.Store.StoreAbout %>
             <p class="text-muted"></p>
             <div class="btn-group pull-right">
<%--                 <a href="<%:item.BrandUrlForStoreProfile %>" class="btn btn-sm"><span class="glyphicon glyphicon-certificate"></span>&nbsp;Markalar </a>--%>
                 
                 <%--<a href="<%:item.WebSiteUrl%>" target="_blank" class="btn btn-sm"><span class="glyphicon glyphicon-globe"></span>&nbsp;Website </a>--%>
            
                 <a href="<%=Model.Store.StoreVideosPageUrl%>" class="btn btn-sm btn-mt4">Tüm Videoları </a>
             </div>
         </div>
    </div>
     <hr />
    
            <%} %>

            <%=Html.RenderHtmlPartial("_VideoViewWindow",Model.VideoModels)%>
   

            <%=Html.RenderHtmlPartial("_VideoPaging", Model.VideoPagingModel)%>
        </div>
    </div>
    <style type="text/css">
        /*.tumSektorBack {
            background-color: #f2e6ef;
            border: 1px solid #b36b7f;
            color: #b36b7f;
        }

        .sektorBack ol.breadcrumb {
            background-color: #f2e6ef;
        }

            .sektorBack ol.breadcrumb li {
                color: #b36b7f;
            }*/
    </style>
</asp:Content>
