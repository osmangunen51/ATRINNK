﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<BannerModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        onload = function () {

            if ($('#hdnSaveMessage').val() == 'true') {
                alert('Değişiklikler başarıyla kaydedilmiştir.');
            }
        }

        function DeleteBanner(BannerId) {

            if (BannerId == 0) {
                alert('Bu bölüme kayıtlı banner olmadığından silme işlemi başarısız !');
            }
            else {
                if (confirm('Banneri Silmek istediğinizden eminmisiniz ?')) {
                    $.ajax({
                        url: '/Banner/Delete',
                        data: { id: BannerId },
                        type: 'post',
                        dataType: 'json',
                        success: function (data) {
                            if (data) {
                                alert('Banner başarıyla silinmiştir !');
                                location.reload(true);
                            }
                            else {
                                alert('Silme işleminde hata oluştur. Daha sonra tekrar deneyiniz. !');
                            }
                        }
                    });
                }
            }
        }
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm("Edit", "Banner", FormMethod.Post, new { enctype = "multipart/form-data" })) { %>
    <%=Html.HiddenFor(c => c.BannerType, new { id = "hdnBannerType" })%>
    <%=Html.HiddenFor(c => c.SaveMessage, new { id = "hdnSaveMessage" })%>
    <div style="width: 845px; height: auto; float: left; margin-left: 20px; margin-top: 20px;">
        <div style="float: left; width: 600px; float: left; border: dashed 1px #bababa; height: 132px; padding: 10px;">
            <div style="width: 98%; height: auto; border-bottom: dashed 1px #bababa; padding: 5px;">
                <span style="font-weight: bold; font-size: 14px">Banner 1</span>
            </div>
            <div style="width: 100%; float: left; margin-top: 10px;">
                <div style="width: 10%; float: left;">
                    Dosya
       
                </div>
                <div style="width: 90%; float: left;">
                    <div style="width: auto; float: left;">
                        <%var banner1 = Model.BannerItems.SingleOrDefault(c => c.BannerType == (byte)BannerType.CategoryBanner1);%>
                        <%= Html.FileUploadFor(c => c.Banner1Rsc, new { style = "border: none; width: 250px; height: 22px; border: solid 1px #bababa" })%>
                    </div>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Açıklama
       
                </div>
                <div style="width: 90%; float: left;">
                    <%=Html.TextBox("Banner1Desc", banner1 != null ? banner1.BannerDescription : "", new { style = "width: 244px;" })%>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Link
       
                </div>
                <div style="width: 90%; float: left;">
                    <div style="float: left;">
                        <%=Html.TextBox("Banner1Link", banner1 != null ? banner1.BannerLink: "", new { style = "width: 244px;" })%>
                    </div>
                    <div style="float: right;">
                        <button onclick="$('#hdnBannerType').val('<%:(byte)BannerType.CategoryBanner1 %>');$('#hdnSaveMessage').val('true');">
                            Kaydet</button>
                        <button onclick="DeleteBanner(<%:banner1 != null ? banner1. BannerId: 0 %>);" type="button">
                            Sil</button>
                    </div>
                </div>
            </div>
        </div>
        <div style="width: 170px; float: left; border: dashed 1px #bababa; margin-left: 10px; height: 150px;">
            <table cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                <tr>
                    <td align="center" valign="middle">
                        <% if (banner1 != null) { %>
                        <% if (banner1.BannerResource.Contains("gif")) { %>
                        <a href="<%:banner1.BannerLink %>">
                            <img src="<%:AppSettings.BannerGifFolder  + banner1.BannerResource %>" /></a>
                        <% } else if (banner1.BannerResource.Contains("swf")) { %>
                        <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" id="Object4" align="middle">
                            <param name="allowScriptAccess" value="sameDomain" />
                            <param name="movie" value="<%:AppSettings.BannerFlashFolder + banner1.BannerResource %>" />
                            <param name="quality" value="high" />
                            <param name="salign" value="tl" />
                            <param name="bgcolor" value="#ffffff" />
                            <param id="Param4" name="FlashVars" value="">
                        </object>
                        <% } else {%>
                        <a href="<%:banner1.BannerLink %>">
                            <img src="<%:AppSettings.BannerImagesThumbFolder+ banner1.BannerResource %>" /></a>
                        <% } %>
                        <% } %>
          </td>
                </tr>
            </table>
        </div>
        <div style="float: left; width: 600px; float: left; border: dashed 1px #bababa; height: 132px; padding: 10px;">
            <div style="width: 98%; height: auto; border-bottom: dashed 1px #bababa; padding: 5px;">
                <span style="font-weight: bold; font-size: 14px">Banner 2</span>
            </div>
            <div style="width: 100%; float: left; margin-top: 10px;">
                <div style="width: 10%; float: left;">
                    Dosya
       
                </div>
                <div style="width: 90%; float: left;">
                    <div style="width: 1px; float: left;">
                        <%var banner2 = Model.BannerItems.SingleOrDefault(c => c.BannerType == (byte)BannerType.CategoryBanner2);%>
                        <%= Html.FileUploadFor(c => c.Banner2Rsc, new { style = "border: none; width: 250px; height: 22px; border: solid 1px #bababa" })%>
                    </div>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Açıklama
       
                </div>
                <div style="width: 90%; float: left;">
                    <%=Html.TextBox("Banner2Desc", banner2 != null ? banner2.BannerDescription : "", new { style = "width: 244px;" })%>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Link
       
                </div>
                <div style="width: 90%; float: left;">
                    <div style="float: left;">
                        <%=Html.TextBox("Banner2Link", banner2 != null ? banner2.BannerLink : "", new { style = "width: 244px;" })%>
                    </div>
                    <div style="float: right;">
                        <button onclick="$('#hdnBannerType').val('<%:(byte)BannerType.CategoryBanner2 %>');$('#hdnSaveMessage').val('true');">
                            Kaydet</button>
                        <button onclick="DeleteBanner(<%:banner2 != null ? banner2.BannerId: 0 %>);" type="button">
                            Sil</button>
                    </div>
                </div>
            </div>
        </div>
        <div style="width: 170px; float: left; border: dashed 1px #bababa; margin-left: 10px; height: 150px;">
            <table cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                <tr>
                    <td align="center" valign="middle">
                        <% if (banner2 != null) { %>
                        <% if (banner2.BannerResource.Contains(".gif")) { %>
                        <a href="<%:banner2.BannerLink %>">
                            <img src="<%:AppSettings.BannerGifFolder  + banner2.BannerResource %>" /></a>
                        <% } else if (banner2.BannerResource.Contains("swf")) { %>
                        <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" id="Object1" align="middle">
                            <param name="allowScriptAccess" value="sameDomain" />
                            <param name="movie" value="<%:AppSettings.BannerFlashFolder + banner2.BannerResource %>" />
                            <param name="quality" value="high" />
                            <param name="salign" value="tl" />
                            <param name="bgcolor" value="#ffffff" />
                            <param id="Param1" name="FlashVars" value="">
                        </object>
                        <% } else {%>
                        <a href="<%:banner2.BannerLink %>">
                            <img src="<%:AppSettings.BannerImagesThumbFolder+ banner2.BannerResource %>" /></a>
                        <% } %>
                        <% } %>
          </td>
                </tr>
            </table>
        </div>
        <div style="float: left; width: 600px; float: left; border: dashed 1px #bababa; height: 132px; padding: 10px;">
            <div style="width: 98%; height: auto; border-bottom: dashed 1px #bababa; padding: 5px;">
                <span style="font-weight: bold; font-size: 14px">Banner 3</span>
            </div>
            <div style="width: 100%; float: left; margin-top: 10px;">
                <div style="width: 10%; float: left;">
                    Dosya
       
                </div>
                <div style="width: 90%; float: left;">
                    <div style="width: auto; float: left;">
                        <%var banner3 = Model.BannerItems.SingleOrDefault(c => c.BannerType == (byte)BannerType.CategoryBanner3);%>
                        <%= Html.FileUploadFor(c => c.Banner3Rsc, new { style = "border: none; width: 250px; height: 22px; border: solid 1px #bababa" })%>
                    </div>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Açıklama
       
                </div>
                <div style="width: 90%; float: left;">
                    <%=Html.TextBox("Banner3Desc", banner3 != null ? banner3.BannerDescription : "", new { style = "width: 244px;" })%>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Link
       
                </div>
                <div style="width: 90%; float: left;">
                    <div style="float: left;">
                        <%=Html.TextBox("Banner3Link", banner3 != null ? banner3.BannerLink : "", new { style = "width: 244px;" })%>
                    </div>
                    <div style="float: right;">
                        <button onclick="$('#hdnBannerType').val('<%:(byte)BannerType.CategoryBanner3 %>');$('#hdnSaveMessage').val('true');">
                            Kaydet</button>
                        <button onclick="DeleteBanner(<%:banner3 != null ? banner3.BannerId: 0 %>);" type="button">
                            Sil</button>
                    </div>
                </div>
            </div>
        </div>
        <div style="width: 170px; float: left; border: dashed 1px #bababa; margin-left: 10px; height: 150px;">
            <table cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                <tr>
                    <td align="center" valign="middle">
                        <% if (banner3 != null) { %>
                        <% if (banner3.BannerResource.Contains(".gif")) { %>
                        <a href="<%:banner3.BannerLink %>">
                            <img src="<%:AppSettings.BannerGifFolder  + banner3.BannerResource %>" /></a>
                        <% } else if (banner3.BannerResource.Contains("swf")) { %>
                        <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" id="Object2" align="middle">
                            <param name="allowScriptAccess" value="sameDomain" />
                            <param name="movie" value="<%:AppSettings.BannerFlashFolder + banner3.BannerResource %>" />
                            <param name="quality" value="high" />
                            <param name="salign" value="tl" />
                            <param name="bgcolor" value="#ffffff" />
                            <param id="Param2" name="FlashVars" value="">
                        </object>
                        <% } else {%>
                        <a href="<%:banner3.BannerLink %>">
                            <img src="<%:AppSettings.BannerImagesThumbFolder+ banner3.BannerResource %>" /></a>
                        <% } %>
                        <% } %>
          </td>
                </tr>
            </table>
        </div>
        <div style="float: left; width: 600px; float: left; border: dashed 1px #bababa; height: 132px; padding: 10px;">
            <div style="width: 98%; height: auto; border-bottom: dashed 1px #bababa; padding: 5px;">
                <span style="font-weight: bold; font-size: 14px">Banner 4</span>
            </div>
            <div style="width: 100%; float: left; margin-top: 10px;">
                <div style="width: 10%; float: left;">
                    Dosya
       
                </div>
                <div style="width: 90%; float: left;">
                    <div style="width: auto; float: left;">
                        <%var banner4 = Model.BannerItems.SingleOrDefault(c => c.BannerType == (byte)BannerType.CategoryBanner4);%>
                        <%= Html.FileUploadFor(c => c.Banner4Rsc, new { style = "border: none; width: 250px; height: 22px; border: solid 1px #bababa" })%>
                    </div>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Açıklama
       
                </div>
                <div style="width: 90%; float: left;">
                    <%=Html.TextBox("Banner4Desc", banner4 != null ? banner4.BannerDescription : "", new { style = "width: 244px;" })%>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Link
       
                </div>
                <div style="width: 90%; float: left;">
                    <div style="float: left;">
                        <%=Html.TextBox("Banner4Link", banner4 != null ? banner4.BannerLink: "", new { style = "width: 244px;" })%>
                    </div>
                    <div style="float: right;">
                        <button onclick="$('#hdnBannerType').val('<%:(byte)BannerType.CategoryBanner4 %>');$('#hdnSaveMessage').val('true');">
                            Kaydet</button>
                        <button onclick="DeleteBanner(<%:banner4 != null ? banner4.BannerId: 0 %>);" type="button">
                            Sil</button>
                    </div>
                </div>
            </div>
        </div>
        <div style="width: 170px; float: left; border: dashed 1px #bababa; margin-left: 10px; height: 150px;">
            <table cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                <tr>
                    <td align="center" valign="middle">
                        <% if (banner4 != null) { %>
                        <% if (banner4.BannerResource.Contains(".gif")) { %>
                        <a href="<%:banner4.BannerLink %>">
                            <img src="<%:AppSettings.BannerGifFolder  + banner4.BannerResource %>" /></a>
                        <% } else if (banner4.BannerResource.Contains("swf")) { %>
                        <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" id="Object3" align="middle">
                            <param name="allowScriptAccess" value="sameDomain" />
                            <param name="movie" value="<%:AppSettings.BannerFlashFolder + banner4.BannerResource %>" />
                            <param name="quality" value="high" />
                            <param name="salign" value="tl" />
                            <param name="bgcolor" value="#ffffff" />
                            <param id="Param3" name="FlashVars" value="">
                        </object>
                        <% } else {%>
                        <a href="<%:banner4.BannerLink %>">
                            <img src="<%:AppSettings.BannerImagesThumbFolder+ banner4.BannerResource %>" /></a>
                        <% } %>
                        <% } %>
          </td>
                </tr>
            </table>
        </div>
        <div style="float: left; width: 600px; float: left; border: dashed 1px #bababa; height: 132px; padding: 10px;">
            <div style="width: 98%; height: auto; border-bottom: dashed 1px #bababa; padding: 5px;">
                <span style="font-weight: bold; font-size: 14px">Banner 5</span>
            </div>
            <div style="width: 100%; float: left; margin-top: 10px;">
                <div style="width: 10%; float: left;">
                    Dosya
       
                </div>
                <div style="width: 90%; float: left;">
                    <div style="width: auto; float: left;">
                        <%var banner5 = Model.BannerItems.SingleOrDefault(c => c.BannerType == (byte)BannerType.CategoryBanner5);%>
                        <%= Html.FileUploadFor(c => c.Banner4Rsc, new { style = "border: none; width: 250px; height: 22px; border: solid 1px #bababa" })%>
                    </div>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Açıklama
       
                </div>
                <div style="width: 90%; float: left;">
                    <%=Html.TextBox("Banner5Desc", banner5 != null ? banner5.BannerDescription : "", new { style = "width: 244px;" })%>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Link
       
                </div>
                <div style="width: 90%; float: left;">
                    <div style="float: left;">
                        <%=Html.TextBox("Banner5Link", banner5 != null ? banner5.BannerLink : "", new { style = "width: 244px;" })%>
                    </div>
                    <div style="float: right;">
                        <button onclick="$('#hdnBannerType').val('<%:(byte)BannerType.CategoryBanner5 %>');$('#hdnSaveMessage').val('true');">
                            Kaydet</button>
                        <button onclick="DeleteBanner(<%:banner5 != null ? banner5.BannerId: 0 %>);" type="button">
                            Sil</button>
                    </div>

                </div>
            </div>
        </div>
        <div style="width: 170px; float: left; border: dashed 1px #bababa; margin-left: 10px; min-height: 150px; height: auto;">
            <table cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                <tr>
                    <td align="center" valign="middle">
                        <% if (banner5 != null) { %>
                        <% if (banner5.BannerResource.Contains(".gif")) { %>
                        <a href="<%:banner5.BannerLink %>">
                            <img src="<%:AppSettings.BannerGifFolder  + banner5.BannerResource %>" /></a>
                        <% } else if (banner5.BannerResource.Contains("swf")) { %>
                        <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" id="Object5" align="middle">
                            <param name="allowScriptAccess" value="sameDomain" />
                            <param name="movie" value="<%:AppSettings.BannerFlashFolder + banner5.BannerResource %>" />
                            <param name="quality" value="high" />
                            <param name="salign" value="tl" />
                            <param name="bgcolor" value="#ffffff" />
                            <param id="Param5" name="FlashVars" value="">
                        </object>
                        <% } else {%>
                        <a href="<%:banner5.BannerLink %>">
                            <img src="<%:AppSettings.BannerImagesThumbFolder+ banner5.BannerResource %>" /></a>
                        <% } %>
                        <% } %>
          </td>
                </tr>
            </table>
        </div>
        <div style="float: left; width: 600px; float: left; border: dashed 1px #bababa; height: 132px; padding: 10px;">
            <div style="width: 98%; height: auto; border-bottom: dashed 1px #bababa; padding: 5px;">
                <span style="font-weight: bold; font-size: 14px">Banner 6</span>
            </div>
            <div style="width: 100%; float: left; margin-top: 10px;">
                <div style="width: 10%; float: left;">
                    Dosya
       
                </div>
                <div style="width: 90%; float: left;">
                    <div style="width: auto; float: left;">
                        <%var banner6 = Model.BannerItems.SingleOrDefault(c => c.BannerType == (byte)BannerType.CategoryBanner6);%>
                        <%= Html.FileUploadFor(c => c.Banner6Rsc, new { style = "border: none; width: 250px; height: 22px; border: solid 1px #bababa" })%>
                    </div>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Açıklama
       
                </div>
                <div style="width: 90%; float: left;">
                    <%=Html.TextBox("Banner6Desc", banner6 != null ? banner6.BannerDescription : "", new { style = "width: 244px;" })%>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Link
       
                </div>
                <div style="width: 90%; float: left;">
                    <div style="float: left;">
                        <%=Html.TextBox("Banner6Link", banner6 != null ? banner6.BannerLink : "", new { style = "width: 244px;" })%>
                    </div>
                    <div style="float: right;">
                        <button onclick="$('#hdnBannerType').val('<%:(byte)BannerType.CategoryBanner6 %>');$('#hdnSaveMessage').val('true');">
                            Kaydet</button>
                        <button onclick="DeleteBanner(<%:banner6 != null ? banner6.BannerId: 0 %>);" type="button">
                            Sil</button>
                    </div>

                </div>
            </div>
        </div>
        <div style="width: 170px; float: left; border: dashed 1px #bababa; margin-left: 10px; min-height: 150px; height: auto;">
            <table cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                <tr>
                    <td align="center" valign="middle">
                        <% if (banner6 != null) { %>
                        <% if (banner6.BannerResource.Contains(".gif")) { %>
                        <a href="<%:banner6.BannerLink %>">
                            <img src="<%:AppSettings.BannerGifFolder  + banner6.BannerResource %>" /></a>
                        <% } else if (banner6.BannerResource.Contains("swf")) { %>
                        <object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,0,0" id="Object6" align="middle">
                            <param name="allowScriptAccess" value="sameDomain" />
                            <param name="movie" value="<%:AppSettings.BannerFlashFolder + banner6.BannerResource %>" />
                            <param name="quality" value="high" />
                            <param name="salign" value="tl" />
                            <param name="bgcolor" value="#ffffff" />
                            <param id="Param6" name="FlashVars" value="">
                        </object>
                        <% } else {%>
                        <a href="<%:banner6.BannerLink %>">
                            <img src="<%:AppSettings.BannerImagesThumbFolder+ banner6.BannerResource %>" /></a>
                        <% } %>
                        <% } %>
          </td>
                </tr>
            </table>
        </div>
    </div>
    <% } %>
</asp:Content>
