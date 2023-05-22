<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.Trinnk.Management.Models.Stores.StoreSeoNotificationModel>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
    Firma Seo Bildirimleri
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top: 20px;">
        <h2 style="float: left;">Firma Seo Bildirimleri
        </h2>
    </div>
    <div style="width: 100%; margin: 0 auto;">
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header">Firma Adı</td>
                    <td class="Header">Başlık</td>
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
                        string backgroundColor = "";
                        var compateFisrst = DateTime.Compare(itemMemberDesc.LastDate.Value.AddMinutes(45), DateTime.Now);
              
                        if (compateFisrst > 0 && itemMemberDesc.IsFirst)
                        {
                            backgroundColor = "background-color:#f97b7b";
                            if (itemMemberDesc.LastDate.Value.AddMinutes(30) > DateTime.Now)
                            {
                                backgroundColor = "background-color:#f1dc65";
                            }
                        }
                        else if ( itemMemberDesc.IsFirst)
                        {
                            backgroundColor = "background-color:#f97b7b";
                        }

                %>
                <tr id="row<%:itemMemberDesc.ID %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>" style=" <%=backgroundColor%>">
                    <td class="Cell"><a href="/Store/EditStore/<%:itemMemberDesc.StoreID %>"><%:itemMemberDesc.StoreName %></a></td>
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
                                             Response.Write("<font style='color:#9d0825'>" + lastDate[1] + "</font>");
                                         } %></td>
                    <td class="Cell" style="background-color: #2776e5; color: #fff"><%:itemMemberDesc.FromUserName %></td>
                    <td class="Cell" style="background-color: #31c854; color: #fff">
                        <%:itemMemberDesc.ToUserName %>
                    </td>
                    <td class="Cell">
                        <a href="/StoreSeoNotification/Create/<%:Request.QueryString["storeMainPartyId"] %>?storeNotId=<%:itemMemberDesc.ID %>">
                            <img src="/Content/images/ac.png" />
                        </a>
                        <% if (itemMemberDesc.IsFirst == true)
                            {%>
                        <a style="color: #339966;" href="/StoreSeoNotification/FirstProcess/<%:itemMemberDesc.ID %>?isFirst=0"><b>Öncelikli</b> Kaldır</a>
                        <%}
                            else
                            {%>
                        <a href="/StoreSeoNotification/FirstProcess/<%:itemMemberDesc.ID %>?isFirst=1">Öncelik Ver</a>
                        <%} %>
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

</script>
</asp:Content>

