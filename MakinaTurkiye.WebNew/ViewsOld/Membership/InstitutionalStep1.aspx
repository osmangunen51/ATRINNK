<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<MembershipViewModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#Insert').click(function () {
                $('#InsertButton').val('true');
                $(this).hide();
                $('#loaderDiv').show();
            });

            $('#nextStatus').click(function () {
                $('#InsertButton').val('false');
                $(this).hide();
                $('#loaderDiv').show();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% Html.EnableClientValidation(); %>
    <%using (Html.BeginForm("KurumsalUyelik/Adim-1", "Uyelik", FormMethod.Post, new { enctype = "multipart/form-data", id = "formContent" }))
      {%>
    <input type="hidden" id="InsertButton" name="InsertButton" value="false" />
    <div class="row">
        <div class="col-xs-12">
            <h4 class="mt0">
                <span class="glyphicon glyphicon-user" style="padding-right: 5px;"></span>Firma Logonuzu Ekleyin
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-7 col-md-8 pr">
            <div class="row hidden-xs">
                <div class="col-xs-12">
                    <ol class="breadcrumb breadcrumb-mt">
                        <li><a href="/">Anasayfa </a></li>
                        <li class="active">Kurumsal Üyelik 1. Adım </li>
                    </ol>
                </div>
            </div>
            <div>
                <div class="well well-mt4 col-xs-12">
                    <div class="row">
                        <div class="col-xs-4">
                            <% if (!string.IsNullOrWhiteSpace(Model.MembershipModel.StoreLogo))
                               { %>
                            <img src="<%= AppSettings.StoreLogoThumb300x200Folder  + Model.MembershipModel.StoreLogo %>"
                                alt="" class="img-thumbnail" />
                            <% }
                               else
                               { %>
                            <img alt=".." src="https://dummyimage.com/400x300/efefef/000000.jpg&text=logo" class="img-thumbnail" />
                            <% } %>
                        </div>
                        <div class="col-xs-8">
                            <div class="hidden-sm hidden-xs alert alert-info">
                                <span class="glyphicon glyphicon-info-sign"></span>Logonuzu yüklemeniz aynı zamanda
                                sonuçların listelendiği sayfalarada tanınmanızı kolaylaştıracaktır
                           
                            </div>
                            <div class="form-group">
                                <%: Html.FileUploadFor(model => model.MembershipModel.StoreLogo, new 
{ @class = "" })%>
                                <p class="help-block">
                                    Eklemek istediğiniz logonun yerini 'Göz At' tıklayarak bulun ve seçin. Eklenen logonuzu
                                    görümünü pencereden kontrol edin. Şuanda logonuza ulaşamıyorsanız devam edebilirsiniz.
                               
                                </p>
                            </div>
                            <button id="Insert" type="submit" class="btn btn-sm btn-default">
                                Yükle
                           
                            </button>
                            &nbsp; Maksimum dosya boyutu: 10 Kb
                           
                            <div id="loaderDiv" style="display: none; width: auto; height: 20px; font-size: 12px; margin-top: 8px;">
                                <img src="/Content/v2/images/loadSmall.gif" height="20px" alt="">&nbsp; Kaydediliyor, lütfen bekleyiniz.
                            </div>
                            <div class="form-group">
                                <p class="help-block">
                                    <br />
                                    Şu anda logonuza ulaşamıyorsanız, devam edebilirsiniz.
                                   
                                    <button type="submit" class="btn btn-sm btn-default" id="nextStatus">
                                        Sonraki Adım</button>
                                </p>
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
                        <br />
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="#">Firma logomu nasıl ekleyebilirim,
                            boyutu ne kadar olmalıdır? </a>
                        <br />
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="#">Firma görselini nasıl değiştirebilirim?
                        </a>
                        <br />
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="#">Firmada güncelleme yapmak
                        </a>
                        <br />
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="#">Firma profilinin avantajları
                        </a>
                    </div>
                </div>
            </div>
        </div>
         <% } %>
        <%CompanyDemandMembership model1 = new CompanyDemandMembership(); %>
        <%= Html.RenderHtmlPartial("LeftPanel",model1) %>
    </div>
   
</asp:Content>
