﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.Trinnk.Management.Models.Entities.MobileMessage>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<script src="http: //code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <script type="text/javascript">
  </script>
  <input id="activeText" type="hidden" value="" />
   <%using (Html.BeginForm("Create", "MobileMessage", FormMethod.Post, new { enctype = "multipart/form-data" }))
    {%>
        <%: Html.ValidationSummary(true) %>
        
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
              <%= Html.TextAreaFor(model => model.MessageContent, new {@row="6",@cols="6" })%>
                <%: Html.ValidationMessageFor(model => model.MessageContent)%>
            </div>
            
                    <div class="editor-label" style="margin-left:20px; margin-top:15px;color:#868686; font-family:Arial; font-size:14px;">
            Mesaj Tipi
            </div>
            <div class="editor-field" style="margin-left:5px; margin-top:15px;">
            <select name="MessageType">
                <option value="1">Normal Mesaj</option>
                <option value="2">Whatsapp Mesaj</option>
            </select>
            </div>
            <p>
                <input type="submit" value="Ekle" />
            </p>
        </fieldset>

    <% } %>
    
</asp:Content>

