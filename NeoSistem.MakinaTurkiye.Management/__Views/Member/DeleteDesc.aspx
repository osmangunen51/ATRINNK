<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Entities.BaseMember>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	DeleteDesc
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <div style="margin:20px auto auto 30px">
    <h2>Açıklama Silme Onayı </h2>

    <h3><%: Model.Title %> başlıklı açıklamayı kalıcı olarak silmek istediğinize emin misiniz?</h3>
    
    
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
            
		    <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/Member/BrowseDesc/<%:ViewData["mpid"] %>'">
          İptal
        </button>
        </p>
    <% } %>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

