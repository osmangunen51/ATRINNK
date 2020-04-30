<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Stores.StoreSeoNotificationModel>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
    Firma Seo Bildirimleri
</asp:Content>

<asp:content id="Content5" contentplaceholderid="MainContent" runat="server">
    <%if (Model.BaseMemberDescriptionModelItems.Where(x => x.LastDate != null).Count() == 0)
        {
            string newUrl = "/StoreSeoNotification/Create/"+Request.QueryString["storeMainPartyId"];
    %>
    <button type="button" style="width: 100px; float: right; height: 35px; margin-right: 300px; margin-top: 20px;" onclick="window.location='<%:newUrl %>'">
        Yeni Açıklama Girişi
    </button>
    <%
        } %>

    <div style="margin-top: 20px;">
        <h2 style="float: left;"><%=Model.StoreName %> Seo Bildirimleri
        </h2>
    </div>
    <div style="width: 100%; margin: 0 auto;">
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header">İçerik</td>
                    <td class="Header">İşlem Tarihi</td>
                    <td class="Header">Hatırlatma Tarihi</td>
                    <td class="Header">Atayan</td>
                    <td class="Header">Atanan</td>
     
                    <td class="Header HeaderEnd">Araçlar</td>
                </tr>
            </thead>
            <tbody>
                <%int row = 0; %>
                <%foreach (var itemMemberDesc in Model.BaseMemberDescriptionModelItems.ToList())
                    {
                %>
                <tr id="row<%:itemMemberDesc.ID %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
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
                    <td class="Cell" style="background-color: #2776e5; color: #fff"><%:itemMemberDesc.FromUserName %></td>
                    <td class="Cell" style="background-color: #31c854; color: #fff">
                        <%:itemMemberDesc.ToUserName %>
                    </td>
                    <td class="Cell">
                        <a href="/StoreSeoNotification/Create/<%:Request.QueryString["storeMainPartyId"] %>?storeNotId=<%:itemMemberDesc.ID %>">
                            <img src="/Content/images/ac.png" />
                        </a>

                        <a style="cursor: pointer;" onclick="DeletePost(<%:itemMemberDesc.ID %>);">
                            <img src="/Content/images/delete.png" hspace="5" />
                        </a>

                    </td>
                </tr>

                <%} %>
                <tr></tr>
            </tbody>
        </table>
    </div>


</asp:content>

<asp:Content ID="Content6" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        function DeletePost(descId) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/StoreSeoNotification/Delete',
                    data: { id: descId },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        if (data) {
                            $('#row' + descId).hide();
                        }
                        alert(data.Message);
                    }
                });
            }
        }
    </script>
</asp:Content>

