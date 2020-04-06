﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Entities.HelpCategory>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        

        <%using (Html.BeginForm("Create", "Help", FormMethod.Post))
    {%>
    <div style="margin-left:30px;margin-top:30px;">
    <h3>YARDIM KATEGORİSİ EKLE</h3>
  <table class="tableForm" style="padding-top: 10px; height: auto;">
    <tr>
      <td colspan="2" align="right">
        
        <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;">
        </div>
      </td>
    </tr>
    <tr>
      <td>
        KATEGORİ ADI :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.KategoriAd, new { style = "width:300px" })%>
        <%: Html.ValidationMessageFor(model => model.KategoriAd) %>
      </td>
    </tr>
    
    <tr>
      <td colspan="2" align="right">
        <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;">
        </div>
        <br />
       <button type="submit" style="width: 70px; height: 35px;">
          Kaydet
        </button>
        <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/Help/Create'">
          İptal
        </button>
      </td>
    </tr>
  </table>
  </div>
  <%} %>
   <%-- <h2>Create</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

        <fieldset>
            <legend>Fields</legend>
            
           
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.KategoriAd) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.KategoriAd) %>
                <%: Html.ValidationMessageFor(model => model.KategoriAd) %>
            </div>
            <br />
            <button type="submit" style="width: 70px; height: 35px;">
          Kaydet
        </button>
        </fieldset>

    <% } %>

    <div>
        <%: Html.ActionLink("Back to List", "Index") %>
    </div>--%>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

