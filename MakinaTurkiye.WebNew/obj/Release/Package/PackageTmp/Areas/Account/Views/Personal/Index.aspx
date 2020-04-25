<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master"
    Inherits="System.Web.Mvc.ViewPage<PersonalModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>
                Profilim	Anasayfa
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>

                <div class="col-xs-12 col-sm-8">
                    <table class="table table-striped">
                        <tbody>
                            <tr>
                                <td class="border0">E-mail Adresi:
                                </td>
                                <td class="border0"><%: AuthenticationUser.Membership.MemberEmail %></td>
                            </tr>
                            <tr>
                                <td>İsim Soyisim:
                                </td>
                                <td>
                                    <%: AuthenticationUser.Membership.MemberName %> <%: AuthenticationUser.Membership.MemberSurname %>
                                </td>
                            </tr>
                            <tr>
                                <td>Üyelik Tipi:
                                </td>
                                <td>
                                    <%=((MemberType_Tr)AuthenticationUser.Membership.MemberType).ToString("G").Replace("_"," ") %>
                                </td>
                            </tr>
                            <% if (AuthenticationUser.Membership.BirthDate.HasValue && AuthenticationUser.Membership.BirthDate.ToString() != "01.01.0001 00:00:00")
                                { %>
                            <tr>
                                <td>Doğum Tarihi:
                                </td>
                                <td>
                                    <%: AuthenticationUser.Membership.BirthDate.Value.ToString("dd.MM.yyyy") %>
                                </td>
                            </tr>
                            <% } %>
                            <% if (AuthenticationUser.Membership.MemberType > (byte)MemberType.FastIndividual)
                                { %>
                            <tr>
                                <td>Adres:
                                </td>
                                <td>
                                    <% foreach (var item in Model.AddressItems)
                                        { %>
                                    <%=MakinaTurkiye.Entities.Tables.Common.AddressExtensions.GetAddressEdit(item) %>
                                    <br />
                                    <% } %>
                                </td>
                            </tr>
                            <% }%>
                            <% foreach (var item in Model.PhoneItems)
                                { %>
                            <tr>
                                <td>
                                    <%=((PhoneType_TR)item.PhoneType).ToString("G")%> :
                                </td>
                                <td>
                                    <%= item.PhoneCulture + " " + item.PhoneAreaCode + " " +item.PhoneNumber %>
                                </td>
                            </tr>
                            <% } %>

                        </tbody>
                    </table>
                </div>
                <div class="col-xs-12 col-sm-4">
                    <div class="thumbnail">
                        <% if (!string.IsNullOrWhiteSpace(Model.StoreLogo))
                            { %>
                        <img class="img-thumbnail"  width="250" height="180" src="<%=ImageHelpers.GetStoreImage(Model.StoreMainPartyId,Model.StoreLogo,"300")%>" alt="<%:Model.StoreName %>" />
                        <% }
                            else
                            { %>
                        <img alt="" src="https://dummyimage.com/400x300/efefef/000000.jpg&text=logo" class="img-thumbnail" />
                        <% } %>
                        <div class="caption">
                            Sayın, 
              <b>"<%: AuthenticationUser.Membership.MemberName %> <%: AuthenticationUser.Membership.MemberSurname %>"
              </b>
                            <br>
                            hoş geldiniz..
                        </div>
                    </div>
                </div>
                <div class="col-xs-12">
                    <div class="panel panel-mt">
                        <div class="panel-heading">
                            <span class="glyphicon glyphicon-question-sign"></span>
                            Sayfa
              Yardımı
                        </div>
                        <div class="panel-body">
                            <%foreach (var item in Model.HelpList)
                                { %>
                            <i class="fa fa-angle-right"></i>
                            &nbsp;&nbsp;
              <a href="<%:item.Url%>">
                  <%:item.HelpCategoryName %>
              </a>
                            <br>
                            <%} %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
