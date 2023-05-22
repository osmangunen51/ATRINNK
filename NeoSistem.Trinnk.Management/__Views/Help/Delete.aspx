<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.Trinnk.Management.Models.Entities.HelpCategory>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="margin:20px auto auto 30px">
    <h2>Kategori Silme Onayı </h2>

    <h3><%: Model.KategoriAd %> adlı kategoriyi  silmek istediğinize emin misiniz?</h3>
    <h4 style="color:Red">Uyarı :</h4><h4><%: Model.KategoriAd %> adlı kategoriyi silerseniz ona bağlı olan aşağıdaki <%:Model.HelpSubcategories.Count.ToString() %> alt kategoriyi de silmiş olacaksınız. </h4>
    <ul>
    <%foreach (var item in Model.HelpSubcategories)
      {%>
         <li>
                <%=item.SubCategoryName %>
        </li> 
     <% } %>
    
    </ul>
    <%--<fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">YKID</div>
        <div class="display-field"><%: Model.YKID %></div>
        
        <div class="display-label">KategoriAd</div>
        <div class="display-field"><%: Model.KategoriAd %></div>
        
    </fieldset>--%>
    <% using (Html.BeginForm()) { %>
        <p>
		    <button type="submit" value="Sil" style="width:70px;height:35px" > Sil</button> 
            
		    <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/Help/Browse'">
          İptal
        </button>
        </p>
    <% } %>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

