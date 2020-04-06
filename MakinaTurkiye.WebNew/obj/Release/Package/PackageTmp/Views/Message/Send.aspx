﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<MessageModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
  <link href="/Content/home.css" rel="stylesheet" type="text/css" />
  <style type="text/css">
    .btnGonder
    {
      background-image: url('/Content/Images/sendMessage.png');
      width: 205px;
      height: 45px;
      border: none;
      cursor: pointer;
    }
    .textBig
    {
      width: 304px;
      height: 18px;
      border: solid 2px #aadbdf;
      padding-left: 12px;
      background-color: #fff;
      padding-top: 2px;
    }
    .textBig input
    {
      width: 290px;
      background-color: transparent;
      border: none;
      font-family: Segoe UI,Arial;
      font-size: 11px;
    }
    .fileBig
    {
      width: 291px;
      height: 23px;
      border: solid 2px #aadbdf;
      padding-left: 6px;
      background-color: #fff;
    }
    .fileBig input
    {
      width: 290px;
      height: 23px;
      background-color: transparent;
      border: none;
      font-family: Segoe UI,Arial;
      font-size: 11px;
    }
    
    .textBigArea
    {
      width: 494px;
      height: 104px;
      border: solid 2px #aadbdf;
      padding-left: 6px;
      background-color: #fff;
      padding-top: 2px;
    }
    .textBigArea textarea
    {
      height: 100px;
      width: 490px;
      background-color: transparent;
      border: none;
      font-family: Segoe UI,Arial;
      font-size: 11px;
    }
  </style>
  <link href="/Content/Redmond/jquery-ui-1.8.1.css" rel="stylesheet" type="text/css" />
  <script type="text/javascript">

      $(document).ready(function () {

          $('#Loading').dialog
      ({
          autoOpen: false,
          width: 400,
          height: 200,
          modal: true
      });

      });

      onload = function () {

          if ($('#FileName').val() != '') {
              $('#fileStatu').html('Dosya eklediniz.');
              $('#divRemoveFile').show();
          }
          else {
              $('#fileStatu').html('Henüz dosya eklenmedi.');
              $('#divRemoveFile').hide();
          }

      }

      function DeletePicture() {
          if (confirm('Dosyayı silmek istediğinizden eminmisiniz ?')) {
              $.ajax({
                  url: '/Message/DeleteFile',
                  type: 'delete',
                  dataType: 'json',
                  data:
        {
            fileName: $('#FileName').val()
        },
                  success: function (data) {
                      alert('Dosya başarıyla silinmiştir.');
                      $('#fileStatu').html('Henüz dosya eklenmedi.');
                      $('#divRemoveFile').hide();
                  },
                  error: function (x, l, e) {
                      alert(e.responseText);
                  }
              });
          }
      }

      function saveMessage() {
          $.ajax({
              url: '/Message/SaveMessage',
              type: 'post',
              dataType: 'json',
              data:
        {
            Content: $('#Content').val(),
            Subject: $('#Subject').val(),
            FileName: $('#FileName').val(),
            ProductNo: $('#ProductNo').val(),
            MemberNo: $('#MemberNo').val()
        },
              success: function (data) {
                  alert('Mesajınız başarıyla gönderilmiştir. Ana sayfaya yölendiriliyorsunuz.');
                  window.location.href = '/';
              },
              error: function (x, l, e) {
                  alert(e.responseText);
              }
          });
      }
  
  </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <input type="hidden" id="ProductNo" name="ProductNo" value="<%: this.RouteData.Values["ProductNo"].ToString() %>" />
  <input type="hidden" id="MemberNo" name="MemberNo" value="<%:  this.RouteData.Values["MemberNo"].ToString() %>" />
  <input type="hidden" id="FileName" name="FileName" value="<%: Model.FileName %>" />
  <% using (Html.BeginForm("Send", "Message", FormMethod.Post, new { enctype = "multipart/form-data" }))
     {%>
  <div id="Loading">
    <div style="width: 100%; height: 40px; text-align: center; margin-top: 60px;">
      <div style="width: auto; height: 20px;">
        <span id="modalSpan">Sorgulanıyor..</span>
      </div>
    </div>
  </div>
  <div style="width: 100%; height: 30px; margin-top: 5px; float: left; margin-left: 10px;">
    <div style="width: 250px; height: 24px; background-color: #74a1d0; color: #fff; font-size: 13px;
      padding-left: 20px; padding-top: 4px;">
      <span style="font-family: Segoe UI,Arial; font-weight: bold">MESAJ GÖNDER</span>
    </div>
  </div>
  <div style="float: left; width: 800px; height: auto; margin-left: 10px; margin-top: 5px;">
    <div style="width: 800px; height: 50px; margin-left: 10px; margin-top: 10px;">
      <div style="float: left; width: 100%;">
        <div style="float: left; width: 100px;">
          <span style="font-size: 12px;">İlgili Ürün No :</span>
        </div>
        <div style="float: left;">
          <span style="font-size: 12px; font-weight: bold;">
            <%:Model.ProductItem.ProductNo%></span>
        </div>
      </div>
      <div style="float: left; width: 100%; margin-top: 10px;">
        <div style="float: left; width: 100px;">
          <span style="font-size: 12px;">İlgili Ürün Adi :</span>
        </div>
        <div style="float: left;">
          <span style="font-size: 12px;">
            <%:Model.ProductItem.ProductName%></span>
        </div>
      </div>
      <div style="background-color: #aadbdf; width: 605px; height: 1px; float: left; margin-top: 20px;
        margin-bottom: 10px;">
      </div>
      <div style="float: left; width: 100%; margin-top: 10px; display:none;">
        <div style="float: left; width: 100px;">
          <span style="font-size: 12px;">Konu :</span>
        </div>
        <div style="float: left;">
          <div class="textBig">
            <%=Html.TextBoxFor(c => c.Subject, new { maxlength = "50", id = "Subject" })%>
          </div>
        </div>
      </div>
      <div style="float: left; width: 100%; margin-top: 5px;">
        <div style="float: left; width: 100px;">
          <span style="font-size: 12px;">Mesaj :</span>
        </div>
        <div style="float: left;">
          <div class="textBigArea">
            <%=Html.TextAreaFor(c => c.Content, new { maxlength = "500", id = "Content" })%>
          </div>
        </div>
      </div>
      <div style="background-color: #aadbdf; width: 605px; height: 1px; float: left; margin-top: 20px;
        margin-bottom: 10px;">
      </div>
      <div style="float: left; width: 100%; margin-top: 5px;">
        <div style="float: left; width: 100px;">
          <span style="font-size: 12px;">Dosya :</span>
        </div>
        <div style="float: left;">
          <div class="fileBig">
            <input id="File" name="File" type="file" style="margin: 0px;" />
          </div>
        </div>
        <div style="float: left; margin-left: 5px; margin-top: 3px;">
          <button style="background-image: url('/Content/Images/addFile.gif'); width: 51px;
            height: 24px; border: none;">
          </button>
        </div>
      </div>
      <div style="float: left; width: 100%; margin-top: 5px;">
        <div style="float: left; width: 100px; margin-top: 5px; height: 20px;">
          <span style="font-size: 12px;">Dosya Durumu :</span>
        </div>
        <div style="float: left; height: 20px; margin-top: 5px;">
          <span id="fileStatu" style="font-size: 12px;"></span>
        </div>
      </div>
      <div id="divRemoveFile" style="float: left; width: 100%; margin-top: 5px;">
        <div style="float: left; width: 100px; margin-top: 5px;">
        </div>
        <div style="float: left; height: 20px;">
          <a style="color: Red; font-size: 12px; cursor: pointer;" onclick="DeletePicture();">
            Dosyayı Sil</a>
        </div>
      </div>
      <div style="background-color: #aadbdf; width: 605px; height: 1px; float: left; margin-top: 20px;
        margin-bottom: 10px;">
      </div>
      <div style="width: 605px; float: left;">
        <div style="float: left; margin-top: 10px; padding-left: 400px;">
          <input type="button" class="btnGonder" onclick="saveMessage();" />
        </div>
      </div>
    </div>
  </div>
  <% } %>
</asp:Content>
