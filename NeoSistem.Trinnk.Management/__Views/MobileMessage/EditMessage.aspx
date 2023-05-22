<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.Trinnk.Management.Models.Entities.MobileMessage>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditConstants
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <script type="text/javascript">
    $(document).ready(function () {
      $('td').attr('valign', 'top');

      $('.cke_show_borders').focus(function () {
        $('#activeText').val('.cke_show_borders')
      });
      $('#MessagesMTPropertie').focus(function () {
        $('#activeText').val('#MessagesMTPropertie')
      });

      $('#MessagesMTTitle').focus(function () {
        $('#activeText').val('#MessagesMTTitle')
      });

      $('#MessagesMTName').focus(function () {
        $('#activeText').val('#MessagesMTName')
      });


      $('button').click(function () {
        if ($('#activeText').val() == '') {
          alert('Parametre göndermek için göndermek istediğiniz alanı seçmelisiniz.')
        }
        else {
          $($('#activeText').val()).val($($('#activeText').val()).val() + ' ' + $(this).val());
        }
      });

    });
 
   
  </script>
 <%using (Html.BeginForm("EditMessage", "MobileMessage", FormMethod.Post, new { enctype = "multipart/form-data" }))
    {%>
        <%: Html.ValidationSummary(true) %>
        <%:Html.HiddenFor(x=>x.ID) %>
        <fieldset>
            <legend>Güncelle</legend>
            <div class="editor-label" style="margin:20px;">
            <span style="color:#868686; font-family:Arial; font-size:14px;">
              Başlık 
              <br />
              </span>
            </div>
            <div class="editor-field" style="margin-left:20px;">
                <%: Html.TextBoxFor(model => model.MessageName, new { style = "width: 300px;border: solid 1px #bababa;" })%>
                <%: Html.ValidationMessageFor(model => model.MessageName) %>
            </div>
           
            <div class="editor-label" style="margin-left:20px; margin-top:15px;color:#868686; font-family:Arial; font-size:14px;">
                Açıklama
            </div>
            <div class="editor-field" style="margin-left:5px; margin-top:15px;">
                <%= Html.TextAreaFor(model => model.MessageContent, new {@style="width:500px; height:100px;" })%>
                <%: Html.ValidationMessageFor(model => model.MessageContent)%>
            </div>
         <div class="editor-label" style="margin-left:20px; margin-top:15px;color:#868686; font-family:Arial; font-size:14px;">
            Mesaj Tipi
            </div>
            <div class="editor-field" style="margin-left:5px; margin-top:15px;">
            <select name="MessageType">

                <option value="1" <%:Model.MessageType==1?"selected":"" %>>Normal Mesaj</option>
                <option value="2" <%:Model.MessageType==2?"selected":"" %>>Whatsapp Mesaj</option>
            </select>
            </div>
            <p>
                <input type="submit" value="Güncelle" />
            </p>
        </fieldset>

    <% } %>
         <script type="text/javascript" defer="defer">
       
  </script>
</asp:Content>


