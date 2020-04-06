﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StoreModel>" %>
<table border="0" cellpadding="2" cellspacing="0">
  <tr>
    <td rowspan="7" valign="top">
      <%: Html.Thumbnail(Model.StoreLogo, new { path = "user", size = 80 })%>
    </td>
    <td style="font-weight: 700" valign="top">
      Mağaza Adı
    </td>
    <td valign="top">
      :
    </td>
    <td valign="top">
      <%: Model.StoreName %>
    </td>
  </tr>
  <tr>
    <td style="font-weight: 700" valign="top">
      Paket Adı
    </td>
    <td valign="top">
      :
    </td>
    <td valign="top">
      <%: Model.PacketName %>
    </td>
  </tr>
  <tr>
    <td style="font-weight: 700" valign="top">
      Paket Başlangıç
    </td>
    <td valign="top">
      :
    </td>
    <td valign="top">
      <%: Model.StorePacketBeginDate.ToString("dd.MM.yyyy") %>
    </td>
  </tr>
  <tr>
    <td style="font-weight: 700" valign="top">
      Paket Bitiş
    </td>
    <td valign="top">
      :
    </td>
    <td valign="top">
      <%: Model.StorePacketEndDate.ToString("dd.MM.yyyy")%>
    </td>
  </tr>
  <tr>
    <td style="font-weight: 700" valign="top">
      E-Posta
    </td>
    <td valign="top">
      :
    </td>
    <td valign="top">
      <%: Model.StoreEMail %>
    </td>
  </tr>
  <tr>
    <td style="font-weight: 700" valign="top">
      Web
    </td>
    <td valign="top">
      :
    </td>
    <td valign="top">
      <%: Model.StoreWeb %>
    </td>
  </tr>
  <tr>
    <td style="font-weight: 700" valign="top">
      Durum
    </td>
    <td valign="top">
      :
    </td>
    <td valign="top">
      <%: Model.StatuText %>
    </td>
  </tr>
</table>
