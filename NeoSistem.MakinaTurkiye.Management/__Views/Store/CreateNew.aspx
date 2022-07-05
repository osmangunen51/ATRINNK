<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.Stores.StoreNewItem>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   Haber
    Ekle
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="float: left; width: 800px; margin-top: 10px;">
        <%using (Html.BeginForm("CreateNew","Store", FormMethod.Post,new {@enctype="multipart/form-data" }))
            { %>

    
        <%:Html.HiddenFor(x=>x.NewType) %>
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
                <td valign="top">Firma Numarası
                </td>
                <td valign="top">:
                </td>
                <td valign="top">
                    <input type="text" name="StoreNo" />
                    <%:Html.ValidationMessage("StoreNo") %>
                </td>
            </tr>
             <tr>
                <td valign="top">Dosya
                </td>
                <td valign="top">:
                </td>
                <td valign="top">
                    <input type="file" name="file" />
           
                </td>
            </tr>
            <tr>
                <td valign="top">Başlık*
                </td>
                <td valign="top">:
                </td>
                <td valign="top">
                    <%= Html.TextBoxFor(model => model.Title, new { style = "height: 20px; width:250px;" })%>
                    <%:Html.ValidationMessageFor(x=>x.Title) %>
                </td>
            </tr>

            <tr>
                <td valign="top">İçerik*
                </td>
                <td valign="top">:
                </td>
                <td valign="top">
                    <%= Html.TextAreaFor(model => model.Content, new { style = "height: 150px; width:400px;" })%>
                    <%:Html.ValidationMessageFor(x=>x.Content) %>
                </td>
            </tr>
        </table>
        <%} %>
    </div>
        <script type="text/javascript" defer="defer">
        var editor = CKEDITOR.replace('Content', { toolbar: 'webtool' });
        CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
    </script>
</asp:Content>

