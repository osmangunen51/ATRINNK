<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.Trinnk.Management.Models.Entities.MemberDescription>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditDesc
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="margin-left:30px;margin-top:30px; width:60%;">
    <h2>Üye Açıklama Düzenleme</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Açıklama</legend>
            
           <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;"> </div>
            <div class="editor-label">
                <%: Html.Label("Başlık :") %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Title) %>
                <%: Html.ValidationMessageFor(model => model.Title) %>
            </div>
            <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;"> </div>
            <div class="editor-label">
                 <%: Html.Label("Açıklama :") %>
            </div>
            <div class="editor-field">
                <%: Html.TextAreaFor(model => model.Description,new { cols = "40%", style = "width:700px;height:250px;" }) %>
                <%: Html.ValidationMessageFor(model => model.Description) %>
            </div>
            <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;"> </div>
            
            
            <% using (Html.BeginForm()) { %>
        <p>
		    <button type="submit" style="width: 70px; height: 35px;">
          Kaydet
        </button>
            
		    <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/Member/BrowseDesc/<%:ViewData["id4"] %>'">
          İptal
        </button>
        </p>
    <% } %>
        </fieldset>

    <% } %>

    
    </div>
    

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

