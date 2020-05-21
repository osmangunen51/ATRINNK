﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Catolog.CategoryImageModel>" %>

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
                if (confirm('Icon Silmek istediğinizden eminmisiniz ?')) {
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
        function DeleteBanner(BannerId) {

            if (confirm('Banneri Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Category/BannerImageDelete',
                    data: { bannerId: BannerId },
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
        function openTab(evt, cityName) {
            // Declare all variables
            var i, tabcontent, tablinks;

            // Get all elements with class="tabcontent" and hide them
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }

            // Get all elements with class="tablinks" and remove the class "active"
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }

            // Show the current tab, and add an "active" class to the button that opened the tab
            document.getElementById(cityName).style.display = "block";
            evt.currentTarget.className += " active";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="tab">
        <button class="tablinks" onclick="openTab(event, 'Banner')">Banner</button>
        <button class="tablinks" onclick="openTab(event, 'Icon')">İkon</button>
    </div>
    <div id="Banner" class="tabcontent">
        <h3>Banner Ekle</h3>
        <%using (Html.BeginForm("AddBanner", "Category", FormMethod.Post, new { @enctype = "multipart/form-data" }))
            {%>
        <%:Html.HiddenFor(x => x.CategoryId) %>
        <div style="float: left; width: 600px; float: left; border: dashed 1px #bababa; height: 132px; padding: 10px;">
            <div style="width: 98%; height: auto; border-bottom: dashed 1px #bababa; padding: 5px;">
                <span style="font-weight: bold; font-size: 14px">Yeni Slider</span>
            </div>
            <div style="width: 100%; float: left; margin-top: 10px;">
                <div style="width: 10%; float: left;">
                    Dosya
                </div>
                <div style="width: 90%; float: left;">
                    <div style="width: auto; float: left;">
                        <input type="file" name="BannerImage" style="border: none; width: 250px; height: 22px; border: solid 1px #bababa" />
                    </div>
                </div>
            </div>

            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Sıra
                </div>
                <div style="width: 90%; float: left;">
                    <%=Html.TextBox("Order","", new { style = "width: 244px;" })%>
                </div>
            </div>
                        <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Alt Etiket
                </div>
                <div style="width: 90%; float: left;">
                    <%=Html.TextBox("BannerAltTag","", new { style = "width: 244px;" })%>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Link
                </div>
                <div style="width: 90%; float: left;">
                    <div style="float: left;">
                        <%=Html.TextBox("Link","", new { style = "width: 244px;" })%>
                    </div>


                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Resim Tipi
                </div>
                <div style="width: 90%; float: left;">
                    <div style="float: left">
                        <%:Html.RadioButton("ImageType",0) %>  Bilgisayar
                    <%:Html.RadioButton("ImageType",1) %> Tablet
                    <%:Html.RadioButton("ImageType",2) %> Telefon
                    </div>
                    <div style="float: right">
                        <div style="float: right;">
                            <button onclick="$('#hdnBannerType').val('<%:(byte)BannerType.MainSlider%>');$('#hdnSaveMessage').val('true');">
                                Kaydet</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div style="width: 600px; float: left; border: dashed 1px #bababa; height: 332px; padding: 10px;">
            <%foreach (var item in Model.BannerItems)
                {%>
            <div style="width: 98%; height: auto; border-bottom: dashed 1px #bababa; padding: 5px;">
                <span style="font-weight: bold; font-size: 14px">Slider Banner</span>
            </div>
            <div style="width: 100%; float: left; margin-top: 10px;">
                <div style="width: 10%; float: left;">
                    Dosya
                </div>
                <div style="width: 90%; float: left;">
                    <div style="width: auto; float: left;">
                        <a href="<%:item.BannerLink %>">
                            <img src="<%:item.BannerResource %>" width="330px">
                        </a>
                    </div>
                </div>
            </div>



            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Order
                </div>
                <div style="width: 90%; float: left;">
                    <label><%:item.BannerOrder %></label>
                </div>
            </div>
                        <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Alt Tag
                </div>
                <div style="width: 90%; float: left;">
                    <label><%:item.BannerAltTag %></label>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px">
                <div style="width: 10%; float: left;">
                    Resim Tipi
                </div>
                <div style="width: 90%; float: left;">
                    <% string type = "";
                        switch (item.BannerImageType)
                        {
                            case (byte)BannerImageType.Pc:
                                type = "Bilgisayar";
                                break;
                            case (byte)BannerImageType.Mobile:
                                type = "Telefon";
                                break;

                            default:
                                type = "Tablet";
                                break;
                        } %>
                    <label><%:type %></label>
                </div>
            </div>
            <div style="width: 100%; float: left; margin-top: 5px;">
                <div style="width: 10%; float: left;">
                    Link
                </div>
                <div style="width: 90%; float: left;">
                    <div style="float: left;">
                        <label><%:item.BannerLink %></label>
                    </div>
                    <div style="float: right;">
                        <button onclick="DeleteBanner(<%:item.BannerId %>);" type="button" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" aria-disabled="false">
                            <span class="ui-button-text">Sil</span></button>
                    </div>
                </div>
            </div>

            <%} %>
        </div>


        <% } %>
    </div>
    <div id="Icon" class="tabcontent" style="display: none;">
        <h3>İcon Ekle</h3>
        <% using (Html.BeginForm("addImages", "Category", FormMethod.Post, new { enctype = "multipart/form-data" }))
            { %>
        <%:Html.HiddenFor(x => x.CategoryId) %>
        <div style="width: 845px; height: auto; float: left; margin-left: 20px; margin-top: 20px;">
            <div style="float: left; width: 600px; float: left; border: dashed 1px #bababa; height: 132px; padding: 10px;">
                <div style="width: 98%; height: auto; border-bottom: dashed 1px #bababa; padding: 5px;">
                    <span style="font-weight: bold; font-size: 14px">İcon</span>
                </div>
                <div style="width: 100%; float: left; margin-top: 10px;">
                    <div style="width: 10%; float: left;">
                        Dosya
       
                    </div>

                    <div style="width: 90%; float: left;">
                        <div style="width: auto; float: left;">
                            <%:Html.FileUploadFor(x=>x.IconModel.IconUrl, new { @style="border: none; width: 250px; height: 22px; border: solid 1px #bababa"}) %>
                        </div>
                        <div style="float: left;">
                            <button type="submit">
                                Ekle</button>
                        </div>
                    </div>

                </div>
            </div>
            <div style="width: 170px; float: left; border: dashed 1px #bababa; margin-left: 10px; height: 150px;">
                <table cellpadding="0" cellspacing="0" width="100%" style="height: 100%;">
                    <tr>
                        <td align="center" valign="middle">
                            <% if (Model.IconModel.IconUrl != "")
                                { %>
                            <img src="<%:Model.IconModel.IconUrl %>" />

                            <% } %>
                        </td>
                    </tr>
                </table>
            </div>

        </div>
        <% } %>
    </div>
</asp:Content>
