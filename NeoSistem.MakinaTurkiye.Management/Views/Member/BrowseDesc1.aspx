<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.BrowseDescModel>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
    BrowseDesc
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <%if (Model.BaseMemberDescriptionModelItems.Where(x => x.LastDate != null).Count() == 0)
        {
            string newUrl = "/Member/CreateDesc1/";
            if (Model.RegistrationType == (byte)RegistrationType.Full)
            {
                newUrl += Model.MemberMainPartyId;
            }
            else
            {
                newUrl += Model.PreRegistrationStoreId + "?regType=" + (byte)RegistrationType.Pre;
            }
    %>
    <button type="button" style="width: 100px; float: right; height: 35px; margin-right: 300px; margin-top: 20px;" onclick="window.location='<%:newUrl %>'">
        Yeni Açıklama Girişi
    </button>
    <%
        } %>
    <button type="button" style="width: 100px; float: right; height: 35px; margin-right: 200px; margin-top: 20px;" onclick="window.location='/StoreSeoNotification/Index?storeMainPartyId=<%:Model.StoreMainPartyId %>'">
        Yeni Seo Açıklama 
    </button>
    <div style="margin-top: 20px;">
        <h2 style="float: left;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Üye Açıklamaları - <%= Model.MemberNameSurname%>
            <%if (!string.IsNullOrEmpty(Model.AuthName))
                {%>
            <span style="font-size: 13px"><%:Model.AuthName %></span>
            <% } %>
            <%if (Model.IsProductAdded.HasValue && Model.IsProductAdded.Value)
                {%>
            <img src="/Content/images/Accept-icon.png" title="Veri Girişi Tamamlandı" alt="Veri Girişi Tamamlandı" />
            <%}
                else
                { %>
            <img src="/Content/images/cross.png" title="Veri Girişi Tamamlanmadı" alt="Veri Girişi Tamamlanmadı" />
            <%
                } %>
        </h2>
        <div style="float: left;">
            <%if (string.IsNullOrEmpty(Model.AuthName))
                {%>
            <%if (Model.RegistrationType == (byte)RegistrationType.Full)
                {%>
            <table style="margin-top: 17px;">
                <tr>
                    <td><b>Portföy Yöneticisi</b></td>
                    <td>:</td>
                    <%using (Html.BeginForm())
                        {%>
                    <td>
                        <%:Html.HiddenFor(x=>x.StoreMainPartyId)%>
                        <%:Html.HiddenFor(x=>x.MemberMainPartyId) %>

                        <%:Html.DropDownListFor(x=>x.AuthorizedId,Model.Users) %>
                    </td>
                    <td>
                        <input type="submit" value="Kaydet" /></td>
                    <%} %>
                </tr>
            </table>
            <% } %>
            <% } %>
        </div>
    </div>
    <div style="width: 100%; margin: 0 auto;">
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header HeaderBegin">Üye Adı
                    </td>
                    <td class="Header">Firma Adı</td>
                    <td class="Header">Başlık</td>
                    <td class="Header">İçerik</td>
                    <td class="Header">İşlem Tarihi</td>
                    <td class="Header">Hatırlatma Tarihi</td>
                    <td class="Header">Atayan</td>
                    <td class="Header">Atanan</td>
                    <td class="Header">Kayıt Türü</td>
                    <td class="Header HeaderEnd">Araçlar</td>
                </tr>
            </thead>
            <tbody>
                <%int row = 0; %>
                <%foreach (var itemMemberDesc in Model.BaseMemberDescriptionModelItems.ToList())
                    {
                        row++;
                        string backColor = "";
                        if (itemMemberDesc.Title == "Ödeme")
                        {
                            backColor = "#dabd9b";
                        }
                %>
                <tr id="row<%:itemMemberDesc.ID %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>" style="background-color: <%:backColor%>">
                    <td class="Cell">
                        <%:Html.Truncate(Model.MemberNameSurname.ToString(),15)%>..</td>
                    <td class="Cell"><%if (!string.IsNullOrEmpty(Model.StoreName))
                                         {
                    %>
                        <a target="_blank" href="/Store/EditStore/<%:itemMemberDesc.StoreID %>"><%:Html.Truncate(Model.StoreName.ToString(),20)%></a>
                        <%}%></td>
                    <td class="Cell"><%:itemMemberDesc.Title %></td>
                    <td class="Cell" style="font-size: 15px;"><% if (itemMemberDesc.Description != null)
                                                                  { %>
                        <%=Html.Raw(itemMemberDesc.Description)%>
                        <% }
                        %>
                    </td>
                    <td class="Cell"><% if (itemMemberDesc.InputDate != null)
                                         {
                                             string[] Inputdate = itemMemberDesc.InputDate.ToString().Split(' ');
                                             Response.Write(Inputdate[0] + " ");
                                             Response.Write("<font style='color:#C5D5DD'>" + Inputdate[1] + "</font>");
                                         }%></td>
                    <td class="Cell"><%if (itemMemberDesc.LastDate.ToDateTime().Date >= DateTime.Now.Date)
                                         {
                                             string[] lastDate = itemMemberDesc.LastDate.ToString().Split(' ');
                                             Response.Write(lastDate[0] + " ");
                                             Response.Write("<font style='color:#C5D5DD'>" + lastDate[1] + "</font>");
                                         } %></td>
                    <td class="Cell" style="background-color: #2776e5; color: #fff"><%:itemMemberDesc.UserName %></td>
                    <td class="Cell" style="background-color: #31c854; color: #fff">
                        <%:itemMemberDesc.ToUserName %>
                    </td>
                    <td class="Cell">
                        <%string type = "Normal";
                            if (!itemMemberDesc.StoreID.HasValue)
                            { type = "Ön Kayıt"; }%>
                        <%:type %>
                    </td>
                    <td class="Cell">
                        <%if (itemMemberDesc.Description != "Mail" && !string.IsNullOrEmpty(itemMemberDesc.Description) && itemMemberDesc.Title != "Ödeme" && itemMemberDesc.Title != "Bilgi +kayıt tar + tıklama sayısı+" && itemMemberDesc.LastDate.ToDateTime().Date >= DateTime.Now.Date)
                            {%>
                        <a href="/Member/EditDesc1/<%:itemMemberDesc.ID %>">
                            <img src="/Content/images/ac.png" alt="" />
                        </a>

                        <a style="cursor: pointer;" onclick="DeletePost(<%:itemMemberDesc.ID %>);">
                            <img src="/Content/images/delete.png" hspace="5" />
                        </a>
                        <% } %>
                        <%if (Model.BaseMemberDescriptionModelItems.Where(x => x.LastDate.ToDateTime().Date >= DateTime.Now.Date).Count() == 0 && row == 1)
                            {%>
                        <a href="/Member/EditDesc1/<%:itemMemberDesc.ID %>">
                            <img src="/Content/images/ac.png" alt="" />
                        </a>
                        <% } %>

                    </td>
                </tr>

                <%} %>
                <tr></tr>
            </tbody>
        </table>
    </div>


</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function DeletePost(descId) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Member/DeleteDescription',
                    data: { id: descId },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {

                        if (data.IsSuccess) {
                            $('#row' + descId).hide();
                        }
                        alert(data.Message);
                    }
                });
            }
        }
    </script>
</asp:Content>

