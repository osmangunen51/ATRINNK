<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Entities.HelpSubcategory>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	DeleteSub
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Alt Kategori Silme Onayı</h2>

    <h3><%=Model.HelpCategory.KategoriAd %> kategorisine bağlı olan <%=Model.SubCategoryName%> alt kategorisini <br /> silmek istediğinize emin misiniz?</h3>
   
    <% using (Html.BeginForm()) { %>
        <p>
		    <p>
		    <button type="submit" value="Sil" style="width:70px;height:35px" > Sil</button> 
            
		    <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/Help/BrowseSub'">
          İptal
        </button>
        </p>
        </p>
    <% } %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

