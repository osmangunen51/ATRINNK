﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.WebSiteErrorCreateModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Ürünleri Excel Dosyası İle Güncelle
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="margin-left:40px; margin-top:20px;">
    <%if (ViewBag.Updated != null && ViewBag.Updated == true) {%>
        <p style="font-size:15px; color:#003317">Firmalar başarıyla güncellenmiştir.</p>
    <% } %>
      <%using (Html.BeginForm("UpdateStoreByExel","Store",FormMethod.Post,new {@enctype="multipart/form-data" }))
          { %>
        <table border="0" class="tableForm" cellpadding="5" cellspacing="0">
      <tr>
    <td valign="top">
     Ürün Excel Dosyası
    </td>
    <td valign="top">
      :
    </td>
    <td valign="top">
        <input type="file" name="file" />
    </td>

  </tr>
            <tr>
                <td colspan="2"></td>
                <td>
                    <button type="submit" style="width: 70px; height: 35px;" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" aria-disabled="false"><span class="ui-button-text">
        Gönder
        </span></button>
        <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/Product/Index'" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" aria-disabled="false"><span class="ui-button-text">
          İptal
        </span></button>
        <br>
  
                </td>
            </tr>
      <%} %>
        </div>
</asp:Content>

