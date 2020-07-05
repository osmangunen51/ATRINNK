<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">

    <%--    <script type="text/javascript">
        function DeleteLogo() {
            if ($('#hiddenDelete').val() == 'false') {
                $('#hiddenDelete').val('true');
                $('[data-rel="addlogo"]').show();
                $('[data-rel="logo"]').hide();
            }
        }
    </script>--%>
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
           
                <%using (Html.BeginForm("UpdateLogo", "Store", FormMethod.Post, new { enctype = "multipart/form-data", role = "form", @class = "form-horizontal" }))
                  {%>
                <input id="hiddenDelete" name="Delete" value="false" type="hidden" />
                <div class="well well-mt4 col-xs-12">
                    <div class="row">
                        <div class="col-xs-4">
                            <img alt="" data-rel="addlogo" style="display: none" src="http://www.placehold.it/400x300&amp;text=logo"
                                class="img-thumbnail" />
                            <% if (!string.IsNullOrWhiteSpace(Model.Store.StoreLogo))
                               { %>
                            <img class="pull-left" width="250" height="180" src="<%=Url.Content(ImageHelpers.GetStoreImage(Model.Store.MainPartyId,Model.Store.StoreLogo,"300")+"?v="+DateTime.Now.ToString("yyyyMMddHHmmss"))%>" alt="<%:Model.Store.StoreName %>" />
                            <% }
                               else
                               { %>
                            <img alt="" src="https://dummyimage.com/400x300/efefef/000000.jpg&text=logo" class="img-thumbnail" />
                            <% } %>
                        </div>
                        <div class="col-xs-8">
                            <div class="hidden-sm hidden-xs alert alert-info">
                                <span class="glyphicon glyphicon-info-sign"></span>Logonuzu yüklemeniz aynı zamanda
                                sonuçların listelendiği sayfalarada tanınmanızı kolaylaştıracaktır
                           
                            </div>
                            <div class="form-group">
                                <%= Html.FileUploadFor(model => Model.Store.StoreLogo, new { @class = "" })%>
                                <p class="help-block">
                                    Eklemek istediğiniz logonun yerini 'Göz At' tıklayarak bulun ve seçin. Eklenen logonuzu
                                    görümünü pencereden kontrol edin. Şuanda logonuza ulaşamıyorsanız devam edebilirsiniz.
                               
                                </p>
                            </div>
                            <%--  <a onclick="DeleteLogo()" class="btn btn-sm btn-default"><span class="glyphicon glyphicon-trash">
                            </span>&nbsp;Logoyu  Sil </a>--%>
                            <button id="Insert" type="submit" class="btn btn-sm btn-default">
                                Logo Yükle
                           
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
