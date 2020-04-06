<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.StoreImage.StoreImagesModel>" %>


<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript" src="<%:Url.Content("~/Areas/Account/Content/FileUpload/filepreview.js") %>"></script>
    <script>
        function ShowLoading() {
            $("#loaderDiv").show();
        }
        function ImageDelete(i) {
            $("#imageParent" + i).hide();


        }
    </script>
    <style type="text/css">
        #formdiv { text-align: center; }
        #file { color: green; padding: 5px; border: 1px dashed #123456; background-color: #f9ffe5; }
        #img { width: 17px; border: none; height: 17px; margin-left: -20px; margin-bottom: 191px; }
        .upload { width: 100%; height: 30px; }

        .previewBox { text-align: center; position: relative; width: 150px; height: 150px; margin-right: 10px; margin-bottom: 20px; float: left; }
            .previewBox img { height: 150px; width: 150px; padding: 5px; border: 1px solid rgb(232, 222, 189); }
        .delete { color: red; font-weight: bold; position: absolute; top: 0; cursor: pointer; width: 20px; height: 20px; border-radius: 50%; background: #ccc; }
    </style>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Firma Görselleri 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>
                Şirket Görselleri
            </h4>
        </div>
    </div>

    <div class="col-sm-12 col-md-12">
        <div>
            <div class="well store-profile-container col-xs-12">
                <div class="row">
                    <form action="" method="post" enctype="multipart/form-data">
                        <div class="col-md-6">
                            <input type="file" class="form-control" id="images" name="images[]" multiple />
                        </div>
                        <div class="col-md-6">
                            <input type="submit" onclick="ShowLoading();" class="btn btn-primary" name='submit_image' value="Seçilen Resimleri Ekle" />
                        </div>
                    </form>
                    <div id="loaderDiv" style="float: right; width: auto; height: 20px; display: none; font-size: 12px; margin-top: 8px;">
                        <img src="../../../../Content/V2/images/loading.gif" width="30" alt="" />&nbsp;
                                                    İşleminiz gerçekleştiriliyor, lütfen bekleyiniz.
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="col-sm-8 col-md-9" id="image_preview">
        <% int i = 0;
            foreach (var item in Model.StoreImageItems)
            {%>

        <div id="imageParent<%:i %>" class="col-md-3 img-thumbnail">

            <%:Ajax.RawActionLink(string.Format("<i class='fa fa-times' aria-hidden='true'></i> "), "DeleteStoreImage",null, new {ImageId=item.ImageId }, new AjaxOptions
{
    // <-- DOM element ID to update
    InsertionMode = InsertionMode.Replace,
     HttpMethod = "POST" // <-- HTTP method
},
new{@onclick="ImageDelete("+i+")"})%>

            <img class="img-thumbnail" src="<%:item.ImagePath %>" />
        </div>
        <%i++;

            } %>
    </div>


</asp:Content>
