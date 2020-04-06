<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<MTVideoViewModel>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="canonical" href="<%=ViewBag.Canonical%>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="fast-access-bar hidden-xs">
        <div class="fast-access-bar__inner">
            <div class="row clearfix">
                <div class="col-xs-12 col-md-6">
                    <%=Model.Navigation %>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-5 col-md-4 col-lg-3">
            <div class="panel panel-mt2 left-menu">
                <%=Html.RenderHtmlPartial("_VideoCategories",Model.VideoCategoryModel)%>
            </div>
            <%=Html.RenderHtmlPartial("_PopularVideos", Model.PopularVideoModels)%>
        </div>
        <div class="col-xs-12 col-sm-7 col-md-8 col-lg-9">
            <!--test asdada!-->





            <div class="alert alert-info tumSektorBack">
                <h1 style="font-size: 12px!important; margin-top: 0px!important; margin-bottom: 0px!important;"><%:string.IsNullOrEmpty(Model.VideoCategoryModel.SelectedCategoryName) ? "Tüm Sektör" : Model.VideoCategoryModel.SelectedCategoryName %> Videoları
                    <%if (Request.QueryString["currentPage"] != null)
                      { %>

                    <span class="small"><%:Model.VideoPagingModel.CurrentPage %>. sayfa</span>
                    <%} %>

                </h1>
            </div>
            <%=Html.RenderHtmlPartial("_VideoViewWindow",Model.VideoModels)%>
            <%=Html.RenderHtmlPartial("_SimilarVideos",Model.SimilarVideos) %>

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
