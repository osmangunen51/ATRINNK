<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.Trinnk.Management.Models.Entities.HelpCategory>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="margin:20px auto auto 30px">
    <h2>
        Yardım Kategori Düzenle</h2>
    <% using (Html.BeginForm())
       {%>
    <%: Html.ValidationSummary(true) %>
    <fieldset>
        <legend>Ana Kategori Düzenleme Ekranı</legend>
        <div class="editor-label">
            <%: Html.Label("Kategori Adı") %>
        </div>
        <br />
        <div class="editor-field">
            <%: Html.TextBoxFor(model => model.KategoriAd) %>
            <%: Html.ValidationMessageFor(model => model.KategoriAd) %>
        </div>
        <p>
            <button type="submit" value="Sil" style="width: 70px; height: 35px">
                Kaydet</button>
            <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/Help/Browse'">
                İptal
            </button>
        </p>
    </fieldset>
    <% } %>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
