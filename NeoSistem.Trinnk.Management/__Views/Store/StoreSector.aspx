<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage< NeoSistem.Trinnk.Management.Models.Stores.StoreSectorModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Firma Ürün İlgili Sektör
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <%=Html.RenderHtmlPartial("Style") %>
    <script src="/Scripts/Trinnk.js" type="text/javascript" defer="defer"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%=Html.RenderHtmlPartial("TabMenu") %>
    <div style="margin-top: 70px; margin-left: 10px;">

        <%if (TempData["success"] != null)
            {
        %>
        <div style="font-size: 20px">İşlem Başarılı</div>
        <% } %>
        <%using (Html.BeginForm())
            {%>
        <%:Html.HiddenFor(x=>x.MainPartyId) %>
        <%foreach (var item in Model.SectorCategories)
            {%>
        <div style="float: left; width: 300px;">
            <input type="checkbox" name="SectorCategoriesForm[]" value="<%:item.Value %>" <%:item.Selected == true ? "checked" : ""%> />
            <%:item.Text %>
        </div>

        <%  } %>
        <div style="clear: both"></div>
        <div style="margin-top: 10px;">
            <input type="submit" value="Kaydet" />
        </div>
        <% } %>
    </div>
</asp:Content>
