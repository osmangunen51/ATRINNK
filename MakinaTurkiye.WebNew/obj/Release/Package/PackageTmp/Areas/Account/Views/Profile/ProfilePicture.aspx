<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#delete').click(function () {
                $('#deleteButton').val('true');
                $(this).hide();
            });
        });
    </script>
</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Profil Resimi -Makina Türkiye

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>
                Profil Görseli Güncelle
            </h4>
        </div>
    </div>

    <%using (Html.BeginForm("ProfilePicture", "Profile", FormMethod.Post, new { enctype = "multipart/form-data" }))
        {%>
    <input type="hidden" id="deleteButton" name="deleteButton" value="false" />
    <div class="col-sm-12 col-md-12">
        <div>
            <div class="well store-profile-container col-xs-12">
                <div class="row">
                    <div class="col-xs-4">
                        <%if (!string.IsNullOrEmpty(Model.Store.StorePicture))
                            { %>
                        <img alt=".." src="<%=AppSettings.StoreProfilePicture + Model.Store.StorePicture%>?v=<%:Model.Store.MainPartyId %>" class="img-thumbnail">
                        <%}
                            else
                            {%>
                        <img src="https://dummyimage.com/400x300/efefef/000000.jpg&text=Profil Görseli" class="img-thumbnail">
                        <% } %>
                    </div>
                    <div class="col-xs-8">
                        <div class="hidden-sm hidden-xs alert alert-info">
                            <span class="glyphicon glyphicon-info-sign"></span>
                            Profil görseli
                    yüklemeniz aynı zamanda sonuçların listelendiği sayfalarada
                    tanınmanızı kolaylaştıracaktır
                        </div>
                        <div class="form-group">
                            <input type="file" name="ProfilePicture" multiple="multiple" />
                            <p class="help-block">
                                Eklemek istediğiniz görselleri  'Göz
                        At' tıklayarak bulun ve seçin.
                            </p>
                        </div>

                        <button id="delete" type="submit" class="btn btn-sm btn-default">
                            <span class="glyphicon glyphicon-trash"></span>&nbsp;Resmi Sil
                        </button>

                        <button type="submit" class="btn btn-sm btn-default">
                            Yükle
                        </button>
                        &nbsp; Maksimum dosya boyutu: 10 Kb
                    </div>
                </div>
            </div>
            <div class="panel panel-mt col-xs-12 p0">
                <div class="panel-heading">
                    <span class="glyphicon glyphicon-question-sign"></span>
                    Sayfa Yardımı
                </div>
                <div class="panel-body">
                    <b>Firma Profili
                    </b>
                    <br>
                    <i class="fa fa-angle-right"></i>
                    &nbsp;&nbsp;
                <a href="#">Firma görseli nasıl ekleyebilirim, boyutu ne kadar
                  olmalıdır?
                </a>
                    <br>

                    <i class="fa fa-angle-right"></i>
                    &nbsp;&nbsp;
                <a href="#">Firma görselini nasıl değiştirebilirim?
                </a>
                    <br>

                    <i class="fa fa-angle-right"></i>
                    &nbsp;&nbsp;
                <a href="#">Firmada
                  güncelleme yapmak
                </a>
                    <br>

                    <i class="fa fa-angle-right"></i>
                    &nbsp;&nbsp;
                <a href="#">Firma profilinin avantajları
                </a>
                </div>
            </div>
        </div>
    </div>
    <%} %>
</asp:Content>
