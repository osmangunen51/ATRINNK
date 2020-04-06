﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.ViewModel.PropertieViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Ürün Grubu Özellik Ekle
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
     <link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
  <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
  <script type="text/javascript">
	 $(function () {
		$.superbox.settings = {
		  closeTxt: "Kapat",
		  loadTxt: "Yükleniyor...",
		  nextTxt: "Sonraki",
		  prevTxt: "Önceki"
		};
		$.superbox();
	 });
	 </script>
		<style type="text/css">
		/* Custom Theme */
		#superbox-overlay{background:#e0e4cc;}
		#superbox-container .loading{width:32px;height:32px;margin:0 auto;text-indent:-9999px;background:url(styles/loader.gif) no-repeat 0 0;}
		#superbox .close a{float:right;padding:0 5px;line-height:20px;background:#333;cursor:pointer;}
		#superbox .close a span{color:#fff;}
		#superbox .nextprev a{float:left;margin-right:5px;padding:0 5px;line-height:20px;background:#333;cursor:pointer;color:#fff;}
		#superbox .nextprev .disabled{background:#ccc;cursor:default;}
	</style>

    <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
    <script type="text/javascript">
        function GetConstant() {
            $.ajax({
                url: '/Constant/GetProperties',
                data: {
                    currentPage: $("#CurrentPage").val()
                },
                type: 'post',
                success: function (data) {
                    $("#table").html(data);

                },
                error: function (x, a, r) {
                    alert("Error");

                }
            });
        }
 
        function DeletePropertie(id) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz bu özelliğe ait tüm veriler ürünler ve kategorilerden silinecek, Onaylıyorum ?')) {
                $.ajax({
                    url: '/Constant/DeletePropertie',
                    data: {
                        ID: id
                    },
                    type: 'post',
                    success: function (data) {
                        GetConstant();

                    },
                    error: function (x, a, r) {
                        alert("Error");

                    }
                });
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


    <%--      <div style="margin-left:20px">
      <%
          foreach (ModelState modelState in ViewData.ModelState.Values) {
              foreach (ModelError error in modelState.Errors) {
                 %>
      <p style="color:#d50606; font-size:16px; ">* <%:error.ErrorMessage %></p>
              <%}
          }
          %>
          </div>--%>
    <div style="float: left; width: 40%; margin-top: 10px;">
        <%using (Html.BeginForm("Propertie", "Constant", FormMethod.Post, new { @enctype = "multipart/form-data" }))
            { %>


        <table border="0" class="tableForm" cellpadding="5" cellspacing="0">
            <tr style="height: 40px;">
                <td colspan="3" align="right">
                    <button type="submit" style="width: 70px; height: 35px;" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" aria-disabled="false">
                        <span class="ui-button-text">Kaydet
                        </span>
                    </button>
                    <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/Help/Index'" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" aria-disabled="false">
                        <span class="ui-button-text">İptal
                        </span>
                    </button>
                    <br>
                    <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px; margin-bottom: 10px;">
                    </div>
                </td>
            </tr>
            <tr>
                <td valign="top">Gözükecek Ad
                </td>
                <td valign="top">:
                </td>
                <td valign="top">
                    <%= Html.TextBoxFor(model => model.PropertieName, new { style = "height: 20px; width:250px;" })%>
                    <%:Html.ValidationMessageFor(x=>x.PropertieName) %>
                </td>
            </tr>

            <tr>
                <td valign="top">İçerik Tipi
                </td>
                <td valign="top">:
                </td>
                <td valign="top">
                    <%= Html.DropDownListFor(model => model.PropertieType,Model.PropertieTypes)%>
          
                </td>
            </tr>
        </table>

        <%} %>
    </div>
    <div style="float: left; width: 50%; margin-top: 10px;">

        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header HeaderBegin">ID
                    </td>
                    <td class="Header">Başlık
                    </td>
                    <td class="Header">Tip
                    </td>
                    <td class="Header"></td>
                    <td class="Header HeaderEnd"></td>

                </tr>
            </thead>
            <tbody id="table">
                <%=Html.RenderHtmlPartial("_PropertieItem",Model.Properties) %>
            </tbody>
        </table>
    </div>
</asp:Content>

