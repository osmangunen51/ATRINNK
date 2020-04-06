<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Users.MTUserListModelView>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Hesabınızla İlgili Yetkili Kullanıcılar - Makina Türkiye
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        table tr td, th { font-size: 15px; }
    </style>
    <script type="text/javascript">
        function ShowPassword(pass, counter) {

            var valP = $("#p" + counter).html();
            if (valP != "*******") {
                $("#p" + counter).html("*******");
                $("#p-button-" + counter).html("Göster");
            }
            else {
                $("#p" + counter).html(pass);
                $("#p-button-" + counter).html("Gizle");
            }


        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
  
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div class="well well-mt">

                <%if (Model.IsAllowedToSee)
                    {%>
                <div class="form-top">
                    <div class="pull-left">
                        <div class="form-header-text">Yetkili Kullanıcılar</div>
                    </div>
                    <div class="pull-right">
                        <a class="btn btn-success" href="/Account/Users/Add">Yeni Ekle <i class="fa fa-add"></i></a>
                    </div>
                </div>
                <div class="table-responsive" style="margin-top: 20px;">
                    <%if (Model.MTUserItems.Count > 0)
                        {%>
                    <table class="table table-hover table-sm">
                        <thead>
                            <tr>
                                <th scope="col">#</th>
                                <th scope="col">İsim Soyisim</th>
                                <th scope="col">Email</th>
                                <th scope="col" style="width: 200px;">Şifre</th>
                                <th scope="col">Tarih</th>
                                <th scope="col">Durum</th>
                                <%--         <th scope="col"></th>--%>
                            </tr>
                        </thead>
                        <tbody>
                            <%int counter = 1; %>
                            <%foreach (var item in Model.MTUserItems)
                                {%>
                            <tr>
                                <td><%:counter %></td>
                                <td>
                                    <%:item.NameSurname %>
                                </td>
                                <td>
                                    <%:item.Email %>
                                </td>
                                <td>
                                    <span id="p<%:counter %>">*******</span>

                                    <small style="cursor: pointer; text-decoration: underline; color: #2422a5" id="p-button-<%:counter %>" onclick="ShowPassword(<%:item.Password %>,<%:counter %>)">Göster
                                    </small>
                                </td>
                                <td><%:item.RecordDate.ToString("dd/MM/yyyy HH:mm") %></td>
                                <td>

                                    <%if (item.Active == true)
                                        {%>
                                    <i title="Aktif" class="fa fa-check" style="color: #03720f"></i>
                                    <% }
                                        else
                                        {
                                    %>
                                    <i title="Pasif" class="fa fa-times" style="color: #710000"></i>
                                    <%
                                        }%>
                                </td>
                            </tr>

                            <%counter++;
                                }       %>
                        </tbody>

                    </table>
                    <% }
                        else
                        {%>
                    <div class="alert alert-info" role="alert" style="margin-top: 10px;">
                        Eklenmiş herhangi bir yetkili kullanıcı bulunamadı. Firma hesabınız için yeni bir kullanıcı ekleyip işlem yapmasına izin verebilirsiniz.
                      <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                          <span aria-hidden="true">×</span>
                      </button>
                    </div>
                    <% } %>
                </div>
                <div class="alert alert-info" role="alert">
                    Eklemiş olduğunuz kullanıcılar sizinle aynı yetkilere sahip olacaktır. Yekili kullanıcı silmek için lütfen bizimle iletişime geçiniz.
                </div>

                <% }
                    else
                    {%>
                <div class="alert alert-warning" role="alert">
                    Bu sayfayı görme yetkiniz yoktur. Profil anasayfanıza dönmek için <a href="/Account/Home">tıklayınız</a>
                </div>
                <% } %>
            </div>
        </div>
    </div>
</asp:Content>
