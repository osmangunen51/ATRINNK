<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master"
    Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.AdvertSuccessfullModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>&nbsp;İlan Ekle
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-8 col-md-9">
            <div>
                <div class="well  store-profile-container">
                    <div>
                        <div>
                            <div class="alert alert-info">
                                <span class="glyphicon glyphicon-info-sign"></span>
                                Sayın, <%: AuthenticationUser.Membership.MemberName + " " + AuthenticationUser.Membership.MemberSurname %> ürün eklemeniz başarı ile tamamlanmıştır.
             
                            </div>
                            <br>
                            <br>
                            <br>
                            <div class="row m20">
                                <div class="col-sm-offset-3 col-sm-9 btn-group">
                                    <%  var curProduct = (MakinaTurkiye.Entities.Tables.Catalog.Product)Session["CurProduct"];
                                        var curCategory = new NeoSistem.MakinaTurkiye.Classes.Category();
                                        curCategory.LoadEntity(curProduct.CategoryId);
                                        string productUrl = Helpers.ProductUrl(curProduct.ProductId,curProduct.ProductName); 
                %>
                                    <a href="<%:productUrl %>?pageType=firstadd" class="btn btn-default">
                                        <span class="glyphicon glyphicon-shopping-cart"></span>
                                        İlanı Görüntüle
                  </a>

                                    <a href="/Account/Advert/Advert" class="btn btn-default">
                                        <span class="glyphicon glyphicon-plus"></span>
                                        Yeni İlan Ekle 
                  </a>

                                    <a href="#" class="btn btn-default">
                                        <span class="glyphicon glyphicon-cloud"></span>
                                        Şablonlarımdan Ekle
                  </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
