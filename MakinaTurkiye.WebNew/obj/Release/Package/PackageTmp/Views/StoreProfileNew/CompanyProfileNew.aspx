<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StoreProfile.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTCompanyProfileModel>" %>

<%@ Import Namespace="System.Globalization" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <%=Html.RenderHtmlPartial("_HeaderContent") %>

    <script type="text/javascript">
function CertificatePopUpGallery() {
    $('.certificate-popup-gallery').magnificPopup({
        delegate: 'div a',
        type: 'image',
        tLoading: 'Resim Yükeniyor #%curr%...',
        mainClass: 'mfp-img-mobile',
        gallery: {
            enabled: true,
            navigateByImgClick: true,
            preload: [0, 1] // Will preload 0 - before current, and 1 after the current image
        },
        image: {
            tError: '<a href="%url%"> #%curr%</a> resim yüklenemedi',
            titleSrc: function (item) {
                return item.el.attr('title') + '<small></small>';
            }
        }
    });
}


        $(document).ready(function () {
CertificatePopUpGallery();
            $(".more-show-about").on("click", function () {
                var showText = $(this).html();
                if (showText == "Devamını Oku") {
                    $("#aboutTextDetail").css("height", "auto");

                    $(this).html("Daha Az");
                }
                else {
                    $("#aboutTextDetail").css("height", "50px");
                    $(this).html("Devamını Oku");
                }
            });
        });
    </script>
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
        {  %>

    <div class="col-sm-7 col-md-8 col-lg-9">
        <div class="StoreProfileContent">
            <%if (Model.SliderImages.Count > 0)
                { %>
            <div id="StoreProfileImageSlider" class="owl-carousel owl-theme">

                <% int a = 1; foreach (var item in Model.SliderImages)
                    {
                %>
                <div class="StoreProfileImageSlider">
                    <div class="item">
                        <img src="<%:item %>" alt="<%:Model.MTStoreAboutModel.StoreName %> resimleri <%:a %>" />
                        <!-- /.item -->
                    </div>
                </div>
                <%a++;
                    } %>

                <!-- /.slider -->
                <!-- /#StoreProfileImageSlider -->
            </div>
            <%} %>

            <div class="clearfix"></div>
            <%=Html.RenderHtmlPartial("_PopularProducts",Model.MTPopularProductsModels) %>
            <div class="clearfix"></div>
            <%=Html.RenderHtmlPartial("_StoreAbout",Model.MTStoreAboutModel) %>
            <div class="clearfix"></div>
            <%=Html.RenderHtmlPartial("_StoreCertificates",Model) %>
        </div>
        <!-- /.col-md-8 main content -->
    </div>

    <!-- container -->
    <%--    </div>
    <!-- /#StoreProfileMain -->
    </div>
    </div>--%>



    <%}%>
    <% 
        else
        {  %>
    <%=Html.Action("NoAccessStore",new{id=Model.MainPartyId}) %>

    <% } %>
</asp:Content>
