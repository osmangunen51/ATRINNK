<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StoreProfile.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTStoreVideoModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <%=Html.RenderHtmlPartial("_HeaderContent") %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="StoreProfileHeaderContent" runat="server">
    <%if (Model.StoreActiveType == 2)
        {  %>
    <%} %>
    <%=Html.RenderHtmlPartial("_HeaderTop",Model.MTStoreProfileHeaderModel) %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="StoprofileMenu" runat="server">
    <%if (Model.StoreActiveType == 2)
        {  %>
    <%=Html.RenderHtmlPartial("_LeftMenu", Model.MTStoreProfileHeaderModel)%>

    <%} %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="StoreProfileContent" runat="server">
    <%if (Model.StoreActiveType == 2)
        { %>
    <%:Html.Hidden("hdnStoreMainPartyId",Model.MainPartyId) %>

    <div class="col-sm-7 col-md-8 col-lg-9">

        <div class="StoreProfileContent clearfix" id="StoreProfileVideosNew">

            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
               
                <%foreach (var videoModel in Model.MTVideoModels.ToList())
                    {%>
                <div class="col-md-4 col-lg-4">
                    <div class="video-container" style="width:auto;">
                        <div title="<%:videoModel.VideoTitle %>" style="text-overflow: ellipsis;
white-space: nowrap;
overflow: hidden; width:260px;" class="store-menu-video-title">
                            <%:videoModel.VideoTitle %>

                        </div>
                        <a style="cursor: pointer" title="<%:videoModel.VideoTitle %>"  data-toggle="modal" data-target="#video<%:videoModel.VideoId %>">
                            <img class="img-responsive" src="<%:videoModel.PicturePath %>">
                        </a>
                        <div class="minute-container" style="position: absolute;">
                            <%:videoModel.VideoMinute %>:<%:videoModel.VideoSecond %>
                        </div>
                        <div class="modal fade" id="video<%:videoModel.VideoId  %>" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header" style="height: 50px">
                                        <h5 class="modal-title" style="float: left;" id="exampleModalLabel"><%:videoModel.VideoTitle %></h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span style="font-size: 30px;" aria-hidden="true">×</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="videocontent">
                                            <video id="vd" class="video-js vjs-default-skin" controls="" preload="auto" width="100%" height="100%" style="width: 100%; height: 100%; padding: 4px; border: 1px solid #ddd; border-radius: 4px; transition: all .2s ease-in-out; display: inline-block;" poster="" data-setup="{&quot;techOrder&quot;: [&quot;html5&quot;]}">
                                                <source src="https://s.makinaturkiye.com/NewVideos/<%:videoModel.VideoPath %>.mp4" type="video/mp4">
                                            </video>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%	} %>

                <div class="clearfix"></div>
                <!-- ./StoreProfileContent -->
            </div>
            <!-- /.col-md-5 main content -->
        </div>
        <!-- container -->
    </div>

    <%}
        else
        { %>
    <%=Html.Action("NoAccessStore",new{id=Model.MainPartyId}) %>
    <%} %>
</asp:Content>
