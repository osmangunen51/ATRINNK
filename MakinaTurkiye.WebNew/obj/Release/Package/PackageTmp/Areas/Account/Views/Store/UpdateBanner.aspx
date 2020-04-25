﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function DeletePicture(ID) {
            if (confirm('Resimi Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Account/Store/DeleteBanner',
                    data: { id: ID },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        var e = data;
                        if (e) {
                            $("#imgBanner").prop("src", "https://dummyimage.com/1400x250/efefef/000000.jpg&text=banner");
                            $(this).hide();
                        }
                        else {
                            alert('Bu sabit kullanılıyor.Silme işlemi başarısız.');
                        }
                    }
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">Firma Logosunu Güncelle
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>

                <%using (Html.BeginForm("UpdateBanner", "Store", FormMethod.Post, new { enctype = "multipart/form-data", role = "form", @class = "form-horizontal" }))
                    {%>
                <input id="hiddenDelete" name="Delete" value="false" type="hidden" />
                <div class="well  col-xs-12" style="background-color: #fff!important; border: 1px solid #ccc;">
                    <%if (ViewData["message"] != null)
                        {%>
                    <div class="alert alert-warning">
                        <strong>Hata!</strong><%:ViewData["message"] %>
                    </div>
                    <% } %>
                    <div class="row">
                        <div class="col-md-12">
                            <img alt="" data-rel="addlogo" style="display: none" src="http://www.placehold.it/1400x250&amp;text=banner"
                                class="img-thumbnail" />
                            <% if (!string.IsNullOrWhiteSpace(Model.StoreBanner))
                                { %>
                            <img class="pull-left img-thumbnail" id="imgBanner" src="<%:Url.Content(ImageHelpers.GetStoreBanner(Model.Store.MainPartyId,Model.StoreBanner)+"?v="+DateTime.Now.ToString("yyyyMMddHHmmss")) %>" />
                            <div onclick="DeletePicture(<%:Model.Store.MainPartyId %>)" style="cursor: pointer; text-decoration: underline; color: #333">Kaldır</div>
                            <% }
                                else
                                { %>
                            <img alt="" src="https://dummyimage.com/1400x250/efefef/000000.jpg&text=banner" class="img-thumbnail" />
                            <% } %>
                        </div>
                        <div class="col-md-12">
                            <div class="hidden-sm hidden-xs alert alert-info">
                                <span class="glyphicon glyphicon-info-sign"></span>Önerilen boyutları minumum <b>1400x250</b> Banner fotoğrafı yüklemeniz kişiselleştirilmiş firma sayfası deneyimi sunmaktadır.
                           
                            </div>
                        </div>
                        <div class="col-md-12" style="margin-left: 15px;">

                            <div class="form-group">
                                <%= Html.FileUploadFor(model => Model.Store.StoreLogo, new { @class = "" })%>
                                <p class="help-block">
                                    Eklemek istediğiniz banner fotoğrafını yerini 'Göz At' tıklayarak bulun ve seçin. Eklenen banner resminiz küçük halini
                                    pencereden kontrol edin.
                               
                                </p>
                            </div>
                            <%--  <a onclick="DeleteLogo()" class="btn btn-sm btn-default"><span class="glyphicon glyphicon-trash">
                            </span>&nbsp;Logoyu  Sil </a>--%>
                            <button id="Insert" type="submit" class="btn btn-sm btn-default">
                                Yükle
                           
                            </button>
                            &nbsp; Maksimum dosya boyutu: 250 Kb
                           
                            <div id="loaderDiv" style="display: none; width: auto; height: 20px; font-size: 12px; margin-top: 8px;">
                                <img src="/Content/images/load.gif" alt="">&nbsp; Kaydediliyor, lütfen bekleyiniz.
                            </div>
                        </div>
                    </div>
                </div>
                <div class="panel panel-mt col-xs-12 p0">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon-question-sign"></span>Sayfa Yardımı
                   
                    </div>
                    <div class="panel-body">
                        <b>Firma Profili </b>
                        <br>
                        <%foreach (var item in Model.HelpList)
                            {%>
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="<%:item.HelpCategoryName %>"><%:item.HelpCategoryName %> </a>
                        <br>
                        <%} %>
                    </div>
                </div>
                <%} %>
            </div>
        </div>
    </div>
</asp:Content>
