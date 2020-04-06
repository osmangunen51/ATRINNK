<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.FooterParentModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Footer Ana Başlık Ekle
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
  <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <%using (Html.BeginForm("FooterParentAdd", "Footer", FormMethod.Post, new { enctype = "multipart/form-data" }))
    {%>
        <%: Html.ValidationSummary(true) %>
        
        <fieldset>
            <legend>Ekle</legend>
            <div class="editor-label" style="margin:20px;">
            <span style="color:#868686; font-family:Arial; font-size:14px;">
              Başlık 
              <br />
              </span>
            </div>
            <div class="editor-field" style="margin-left:20px;">
                <%: Html.TextBoxFor(model => model.FooterParentName, new { style = "width: 300px;border: solid 1px #bababa;" })%>
            </div>
            <div class="editor-label" style="margin-left:20px; margin-top:15px;color:#868686; font-family:Arial; font-size:14px;">
                Sıra
            </div>
               <div class="editor-field" style="margin-left:20px;">
                <%: Html.TextBoxFor(model => model.DisplayOrder, new { style = "width: 300px;border: solid 1px #bababa;" })%>
            </div>
            <p>
                <input type="submit" value="Ekle" />
            </p>
        </fieldset>

    <% } %>
</asp:Content>