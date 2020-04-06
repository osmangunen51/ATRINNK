﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StoreProfile.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTConnectionModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
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

<asp:Content ID="Content2" ContentPlaceHolderID="StoreProfileContent" runat="server">
    <%if (Model.StoreActiveType == 2)
        { %>

    <%string wGsm = ""; %>
    <div class="col-sm-7 col-md-8 col-lg-9">
        <div class="StoreProfileContent">

            <div class="col-sm-12 mt20">
                <table class="table table-striped">
                    <tbody>

                        <tr>
                            <th style="width: 160px;">Yetkili :
                            </th>
                            <td>
                                <%=Model.AuthorizedNameSurname%>
                            </td>
                        </tr>
                        <tr>
                            <th>Firma Ünvanı :
                            </th>
                            <td>
                                <%=Model.StoreName %>
                            </td>
                        </tr>

                        <tr>
                            <th>Adres :
                            </th>
                            <td>
                                <%:Model.AddressMap %>
                            </td>
                        </tr>

                        <% foreach (var item in Model.Phones.Where(x=>x.ShowPhone==true))
                            { %>
                        <% if (item.PhoneType == PhoneType.Fax)
                            { %>
                        <tr>
                            <th>Fax :
                            </th>
                            <td>
                                <%= item.GetFullText() %>
                            </td>
                        </tr>
                        <% }
                            else if (item.PhoneType == PhoneType.Gsm)
                            { %>
                        <tr>
                            <th>GSM :
                            </th>
                            <td>
                                <%= item.GetFullText()%>
                            </td>
                        </tr>
                        <% }
                            else if (item.PhoneType == PhoneType.Whatsapp)
                            {
                                wGsm = item.GetFullText();
                        %>

                        <%}
                            else
                            { %>
                        <tr>
                            <th>Telefon :
                            </th>
                            <td>
                                <%= item.GetFullText() %>
                            </td>
                        </tr>
                        <% } %>
                        <% } %>

                        <%if (wGsm != "")
                            { %>
                        <tr>
                            <th>Whatsapp:
                            </th>
                            <td>
                                <%string storeUrl = Request.Url.AbsoluteUri; %>
                                <%string whatsappUrl = "https://api.whatsapp.com/send?phone=" + wGsm.Replace("+", "").Replace(" ", "") + "&text='" + storeUrl + "' firma sayfanız aracılığıyla"; %>

                                <img src="/Content/SocialIcon/wp-32.png" alt="whatsapp logo" />
                                <a href="<%:whatsappUrl %>" rel="external" target="_blank">Whatsappla İletişim Kur</a>

                            </td>
                        </tr>
                        <%} %>
                        <tr>
                            <th>Web :
                            </th>
                            <td>
                                <%--    <a href="<%:storeWeb%>" rel="nofollow nofollow" target="_blank">--%>

                                <a href="<%:Model.StoreWebUrl %>" rel="nofollow" target="_blank">
                                    <%:Html.Truncate(Model.StoreWebUrl,50)%>
                                </a>

                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="col-sm-12">
                <div style="width: 100%; overflow: hidden; height: 280px;">
                    <iframe id="mainMap" height="470" width="100%" frameborder="0" scrolling="no" marginheight="0" marginwidth="0" src="  https://maps.google.com/maps?q= 
													                <%= Model.AddressMap %>
										                &amp;num=1&amp;ie=UTF8&amp;t=m&amp;z=14&amp;iwloc=A&amp;output=embed"
                        style="border: 0; margin-top: -150px;"></iframe>
                </div>
            </div>

            <div class="clearfix"></div>
            <!-- ./StoreProfileContent -->
        </div>
        <!-- /.col-md-5 main content -->
    </div>


    <%}
        else
        { %>
    <%=Html.Action("NoAccessStore",new{id=Model.MainPartyId}) %>
    <%} %>

    <%if (Model.StoreActiveType == 2 && !string.IsNullOrEmpty(Model.MtJsonLdModel.JsonLdString))
        {%>
    <script type="application/ld+json">
        <%=Model.MtJsonLdModel.JsonLdString%> 
    </script>
    <%} %>
</asp:Content>
