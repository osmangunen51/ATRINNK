﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<StoreDetailInformationModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Mağaza Bilgileri
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
  <style type="text/css">
    .small_input
    {
      width: 120px;
      font-size: 12px;
      padding-left: 5px;
    }
    .big_input
    {
      width: 180px;
      font-size: 12px;
      padding-left: 5px;
    }
    .btnSave
    {
      background-image: url('/Content/Images/btnSave.gif');
      width: 70px;
      height: 24px;
      border: none;
      cursor: pointer;
    }
    .btnAdd
    {
      background-image: url('/Content/Images/btnAdd.gif');
      width: 55px;
      height: 24px;
      border: none;
      cursor: pointer;
    }
    .fileUpload
    {
      font-size: 12px;
      width: 180px;
      height: 20px;
      border: solid 1px #bababa;
    }
    .profileBg
    {
      width: 967px;
      height: auto;
      background-image: url('/Content/Images/profileBg.gif');
      border: solid 1px #cbdfed;
      float: left;
      background-repeat: repeat-x;
      padding-bottom: 15px;
    }
    .profileStartLink
    {
      color: #727679;
      text-decoration: underline;
      font-size: 12px;
    }
    .profileStartLink:hover
    {
      color: #727679;
      font-size: 12px;
      text-decoration: underline;
    }
    .profileStartLink:visited
    {
      color: #727679;
      text-decoration: underline;
      font-size: 12px;
    }
    .speedStep
    {
      width: 900px;
      height: 100px;
      margin-left: 33px;
      margin-top: 60px;
    }
    .speedStepContent
    {
      width: auto;
      height: auto;
      float: left;
      margin-left: 250px;
      font-size: 12px;
      font-weight: bold;
      text-align: center;
    }
    .accordionHeader
    {
      background-image: url('/Content/Images/profilePanelBg.gif');
      border-bottom: solid 1px #aebecd;
    }
    .dropdownBig
    {
      width: 216px;
      height: 20px;
      border: solid 1px #bababa;
    }
    .dropdownBig select
    {
      width: 216px;
      height: 20px;
    }
    
    .textBig
    {
      width: 204px;
      height: 18px;
      border: solid 1px #bababa;
      padding-left: 12px;
      background-color: #fff;
      padding-top: 2px;
    }
    .textBig input
    {
      width: 190px;
      background-color: transparent;
      border: none;
    }
    
    .textBigArea
    {
      width: 204px;
      height: 58px;
      border: solid 1px #bababa;
      padding-left: 12px;
      background-color: #fff;
      padding-top: 4px;
    }
    .textBigArea textarea
    {
      width: 200px;
      height: 50px;
      background-color: transparent;
      border: none;
    }
    
    .textMedium
    {
      width: 64px;
      height: 18px;
      border: solid 1px #bababa;
      padding-left: 12px;
      background-color: #fff;
      padding-top: 2px;
      float: left;
    }
    .textMedium input
    {
      width: 50px;
      background-color: transparent;
      border: none;
    }
    
    .textSmall
    {
      width: 30px;
      height: 18px;
      border: solid 1px #bababa;
      padding-left: 2px;
      background-color: #fff;
      padding-top: 2px;
      float: left;
    }
    .textSmall input
    {
      width: 24px;
      background-color: transparent;
      border: none;
    }
  </style>
      <%=Html.RenderHtmlPartial("Style") %>
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
  <link href="/Content/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
  <script src="/Scripts/JQuery-dropdowncascading.js" type="text/javascript"></script>


  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
  <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     <div class="Content" style="margin: 20px 0px 0px"">
      <%=Html.RenderHtmlPartial("TabMenu") %>
    <%var storeInfo = Model.StoreInformationModel; %>
  
  <table border="0" cellpadding="0" cellspacing="0" width="100%" style="float: left; font-size:14px;">
    <tr>
      <td>
            <%--     GENEL BİLGİLER--%>
            <div style="width: 30%; float: left; margin-right: 10px; min-height:400px;  padding-right:5px; padding-left:5px;   border: 1px solid #cab2b2;
    margin-left: 10px;
    margin-top: 5px;">
              <table border="0" cellpadding="5" cellspacing="0">
        
                 <tr>
                  <td style="width: 150px;">
                   Tele Satış Sorumlusu
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                      <%if (string.IsNullOrEmpty(storeInfo.SalesManagerName)) storeInfo.SalesManagerName = "Tanımsız"; %>
                    <%: storeInfo.SalesManagerName%>
                  </td>
             
                </tr>
                          <tr>
                  <td style="width: 150px;">
                    Portföy Yöneticisi
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                      <%if (string.IsNullOrEmpty(storeInfo.PortfoyName)) storeInfo.PortfoyName = "Tanımsız"; %>
                    <%: storeInfo.PortfoyName%>
                  </td>
             
                </tr>
                  <tr>
                  <td style="width: 150px;">
                    Paket Adı
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                      <%string colorPacket = "#b68c05";
    if (storeInfo.PacketEndDate != null) { colorPacket = "#05762a"; }%>
                      <%if (string.IsNullOrEmpty(storeInfo.PacketName)) storeInfo.PacketName = "Tanımlanmamış"; %>
                   <span style="color:#FF0010"><%: storeInfo.PacketName%></span>
                  </td>
             
                </tr>
                   <tr>
                  <td style="width: 150px;">
                    Paket Bitiş Tarihi
                  </td>
                  <td>
                    :
                  </td>
                  <td style="color:#FF0010">
                  <%if (storeInfo.PacketEndDate != null) { %>
                      <%:storeInfo.PacketEndDate.ToDateTime().ToString("dd-MM-yyyy") %>                
                  <%} %>
                  </td>
                </tr>
                   <tr>
                  <td style="width: 150px;">
                    Firma No
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                   
                      <%:storeInfo.StoreNo %>                
                  </td>
                </tr>
                           <tr>
                  <td style="width: 150px;">
                   Firma Ünvanı
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                   
                      <%:storeInfo.StoreFullName %>                
                  </td>
                </tr>
                  <tr>
                  <td style="width: 150px;">
                   Firma Adı
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                   
                      <%:storeInfo.StoreName %>                
                  </td>
                </tr>
                 <tr>
                  <td style="width: 150px;">
                 Firma Url Adresi
                  </td>
                  <td>
                    :
                  </td>
                  <td>
                   <a href="<%:storeInfo.StoreUrl %>" target="_blank">
                      <%:storeInfo.StoreUrl %>        
                       </a>
                  </td>
                </tr>
                <tr>
                  <td style="width: 150px;">
                Web Adresi
                  </td>
                  <td>
                    :
                  </td>
                  <td >
                  <%if (!string.IsNullOrEmpty(storeInfo.StoreWebUrl)) {%>
                           <a style="color:#FF0010" href="<%:!storeInfo.StoreWebUrl.Contains("http") ? "http://" + storeInfo.StoreWebUrl : storeInfo.StoreWebUrl %>">
                      <%:storeInfo.StoreWebUrl %>               
                       </a>
                      <% } %>
                  </td>
                </tr>
                  <tr>
                  <td style="width: 150px;">
                Vergi Dairesi/No
                      </td>
                  <td>
                    :
                  </td>
                  <td>
                   
                      <%:storeInfo.TaxOffice %>/<%:storeInfo.TaxNumber %>                
                  </td>
                </tr>
                 <tr>
                  <td style="width: 150px;">
                 Faaliyet Alanları
                      </td>
                  <td>
                    :
                  </td>
                  <td>
                   <%=Model.StoreActivityTypes %>             
                  </td>
                </tr>
                  <tr>
                      <td>Detaylar</td>
                      <td>:</td>
                      <td><%:storeInfo.StoreOtherInfo %></td>
                  </tr>
                  <tr>
                      <td colspan="3">
                            <span style="font-size:16px; font-weight:600">Kullanıcı Bilgileri</span>
                      </td>
                  </tr>
                  <tr>
                     <td>Üye No</td>
                      <td>:</td>
                      <td><%:Model.MemberInformationModel.MemberNo %></td>
                  </tr>
                  <tr>
                      <td>Üye Adı Soyadı
                      </td>
                      <td>:</td>
                      <td><%:Model.MemberInformationModel.MemberName %> <%:Model.MemberInformationModel.MemberSurname %></td>
                  </tr>
                  <%foreach (var item in Model.MemberInformationModel.MemberEmails)
    {%>
                   <tr>
                      <td>E-posta
                      </td>
                      <td>:</td>
                      <td><a href="mailto:<%:item %>"><%:item%></a></td>
                  </tr>
                     <% } %>
                  <%if (storeInfo.StoreUpdatedDate.HasValue) {%>
                                      <tr>
                      <td>Bilgi Kontrollü Güncellenme Tarh.</td>
                      <td>:</td>
                      <td><%:storeInfo.StoreUpdatedDate.Value.ToString("dd.MM.yyyy HH:mm") %></td>
                  </tr>
                  <% } %>

            
              </table>
            </div>
            <div style="float: left; min-height:400px;
    width: 30%;
    height: auto;
    background: #eceaea;
    padding-left: 5px;
    padding-left: 10px;
    padding-right: 10px;
    border: 1px solid #cab2b2;
    margin-top: 5px; ">
              <table>
                  <tr>
                      <td>Firma Görüntülenme Sayıları</td>
                      <td>:</td>
                      <td><%:Model.StoreInformationModel.ViewCount %> Çoğul <br /> <%:Model.StoreInformationModel.StoreSingularViewCount %> Tekil</td>
                  </tr>
                  <%if (Model.ActiveProductCount != 0) {%>
                   <tr>
                      <td>Ürün İstatistikleri</td>
                      <td>:</td>
                      <td><span  style="font-size:12px; color:#FF0010">Aktif Ürün:<%:Model.ActiveProductCount %> <br />Pasif Ürün:<%:Model.PasiveProductCount%>
                          <br /><%:Model.SingularProductViewCount %> Tekil<br /><%:Model.ProductViewCount %> Çoğul
                          </span></td>
                  </tr>
                  <% } %>
                  <tr>
                      <td>Whatsapp Tıklanma Sayısı</td>
                      <td>:</td>
                     <td style="color:#FF0010"> <%:Model.WhatsappClickCount %></td>
                  </tr>
         
                <tr>
                  <td valign="top" style="width: 150px;">
                    Firma Logo
                  </td>
                  <td valign="top">
                    :
                  </td>
                  <td colspan="2">
          
                    <br />
                    <% if (!String.IsNullOrEmpty(storeInfo.StoreLogo))
                       { %>
             
                      <% string logoThumb = "//s.makinaturkiye.com/Store/" + storeInfo.StoreMainPartyId + "/" + storeInfo.StoreLogo;  %>
                      <a href="<%:storeInfo.StoreUrl %>" target="_blank">
                    <img id="imageLogo"  src="<%:logoThumb %>"
                      align="left" style="margin-right: 5px; width:100%; border: solid 1px #b6b6b6;"  />
                          </a>
                    <%--onload="imageResize('imageLogo', 550);" --%>
         
                    <% } %>
            
                  </td>
                    
                </tr>
      
                  <tr>
                      <td>Adres</td>
                      <td>:</td>
                      <td><%:Model.StoreContactInfoModel.StoreAddress %></td>
                  </tr>  
                
                     <%foreach (var item in Model.StoreContactInfoModel.StorePhones)
                      {
                          string phoneType = "Sabit";
                          if (item.PhoneType == (byte)PhoneType.Fax)
                          {
                              phoneType = "Fax";
                          }
                          else if (item.PhoneType == (byte)PhoneType.Gsm)
                          {
                              phoneType = "Gsm";
                          }
                          else if (item.PhoneType == (byte)PhoneType.Whatsapp)
                          {
                              phoneType = "Whatsapp";
                          }
                          %>
                  <tr>
                        <td><%:phoneType %></td>   
                  <td>:</td>
                  <td><%:Html.Raw(item.PhoneCulture+" "+item.PhoneAreaCode+" "+item.PhoneNumber) %></td>
                      </tr>
                     <% } %>
                  <tr>
                      <td>Hakkında</td>
                      <td>:</td>
                      <td><%:storeInfo.StoreShortDetail%></td>
                  </tr>
              </table>
            </div>
          <div style="float:left; border: 1px solid #cab2b2;
    width: 15%;
    margin-top: 5px;
    margin-left: 10px; min-height:400px;">
            <table>
                <tr>
                    <td colspan="3"><a style="font-size:15px; font-weight:600" href="/Member/MemberDescription/<%:Model.MemberMainPartyId %>">Son Üye Açıklama</a></td>
                </tr>
               
              <%foreach (var item in Model.StoreMemberDescriptionItems)
                    {%>
                        <tr>
                            <td colspan="3">
                               <b> <%:item.Title %></b>(<%:item.UserName %>)<br />
                                <span style="padding-left:5px; font-size:12px;"><%:Html.Raw(item.Description) %></span> <b style="font-size:12px; color:#0fe54e"><br /><%:item.RecordDate.HasValue==true?item.RecordDate.ToDateTime().ToString("dd-MM-yyyy hh:mm"):"" %></b>
                            </td>
                        </tr>
                    <%} %>
            </table>
              </div>

                    <div style="float:left; border: 1px solid #cab2b2;
    width: 15%;
    margin-top: 5px;
    margin-left: 10px; min-height:400px;">
            <table>
                <tr>
                    <td colspan="3"><a style="font-size:15px; font-weight:600" href="/StoreSeoNotification/Index?storeMainPartyId=<%:Model.StoreInformationModel.StoreMainPartyId %>">Son Seo Açıklama</a></td>
                </tr>
               
              <%foreach (var item in Model.StoreSeoNotificationItems)
                    {%>
                        <tr>
                            <td colspan="3">
      
                                <span style="padding-left:5px; font-size:12px;"><%:Html.Raw(item.Description) %>(<%:item.UserName %>)<br /></span> <b style="font-size:12px; color:#0fe54e"><br /><%:item.RecordDate.HasValue==true?item.RecordDate.ToDateTime().ToString("dd-MM-yyyy hh:mm"):"" %></b>
                            </td>
                        </tr>
                    <%} %>
            </table>
              </div>

      </td>
    </tr>
  </table>
    </div>
</asp:Content>
