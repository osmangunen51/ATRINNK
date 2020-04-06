﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
 <script src="/Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
<script src="/Scripts/jquery.unobtrusive-ajax.min.js" type="text/javascript"></script>
  <link href="/Content/Site.css" rel="stylesheet" type="text/css" />
  <link href="/Content/Ribbon.css" rel="stylesheet" type="text/css" />
  <script src="/Scripts/MicrosoftAjax.js" type="text/javascript"></script>
  <script src="/Scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
  <script src="/Scripts/MicrosoftMvcValidation.js" type="text/javascript"></script>
  <script src="/Scripts/NeoSistem.js" type="text/javascript"></script>
  <script src="/Scripts/jquery.js" type="text/javascript"></script>
  <script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
  <script src="/Scripts/jquery-ui.datepicker-tr.js" type="text/javascript"></script>
  <script src="/Scripts/jquery.cookie.js" type="text/javascript"></script>
  <script src="/Scripts/Ribbon.js" type="text/javascript"></script>
  <script src="/Scripts/jquery-ui-1.8.8.custom.min.js" type="text/javascript"></script>
    <title>MailAllMemberType</title>
    <script type="text/javascript">
      $(document).ready(function () {

        $("input[name='RelatedCategory']", $('#cons')).change(function (e) {
          if ($(this).val() === '1') {
            $("#bireyselyadakurumsal").hide();
            $("#storecategorypacket").hide();
            $("#Store").hide();
          }
          else if ($(this).val() === '2') {
            $("#bireyselyadakurumsal").hide();
            $("#storecategorypacket").hide();
            $("#Store").hide();
          }
          else if ($(this).val() === '3') {
            $("#bireyselyadakurumsal").show();
            $("#storecategorypacket").hide();
            $("#Store").hide();
          }
          else if ($(this).val() === '4') {
            $("#bireyselyadakurumsal").hide();
            $("#storecategorypacket").show();

          }
        });

        $("input[name='chose']", $('#storecategorypacket')).change(function (e) {
          if ($(this).val() === '1') {
            $("#bireyselyadakurumsal").show();
            $("#Store").hide();
          }
          else if ($(this).val() === '2') {
            $("#bireyselyadakurumsal").hide();
            $("#Store").show();
          }
        });
      });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--<%: Html.RadioButton("StoreActiveType", "1", inceleniyor)%>--%>
<%--var var_name = $("input[@name='radio_name']:checked").val();--%>
   <%using (Html.BeginForm())
     {%>
     <div style="width: 595px; float:left;">
      <div id="cons" class="plusContent" style="float:left;">
            <ul style="list-style: none; float: left; padding: 0px; width: 580px; border: solid 1px #8fc0d1;
              padding-top: 10px; padding-left: 10px; background-color: #f8fbff">
              
              <li style="float: left; width: 240px;">
                <div style="width: auto; float: left; height: auto;">
                  <div style="width: auto; float: left; height: auto;">
                  <%: Html.RadioButton("RelatedCategory", "1", false)%>
                  </div>
                  <div style="width: auto; float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                    color: #000">
                    Tüm Üyeler
                  </div>
                </div>
              </li>
                <li style="float: left; width: 240px;">
                <div style="width: auto; float: left; height: auto;">
                  <div style="width: auto; float: left; height: auto;">
                    <%: Html.RadioButton("RelatedCategory", "2", false)%>
                  </div>
                  <div style="width: auto; float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                    color: #000">
                   Hızlı
                  </div>
                </div>
              </li>
               <li style="float: left; width: 240px;">
                <div style="width: auto; float: left; height: auto;">
                  <div style="width: auto; float: left; height: auto;">
                    <%: Html.RadioButton("RelatedCategory", "3", false)%>
                  </div>
                  <div style="width: auto; float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                    color: #000">
                   Bireysel
                  </div>
                </div>
               </li>
               <li style="float: left; width: 240px;">
                <div style="width: auto; float: left; height: auto;">
                  <div style="width: auto; float: left; height: auto;">
                     <%: Html.RadioButton("RelatedCategory", "4", false)%>
                  </div>
                  <div style="width: auto; float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                    color: #000">
                   Kurumsal
                  </div>
                </div>
               </li>
            </ul>
          </div>
                     <div id="storecategorypacket" style="display:none;float:left;">
          <br />
          <br />
          <hr />
          <ul style="list-style: none; float: left; padding: 0px; width: 580px; border: solid 1px #8fc0d1;
              padding-top: 10px; padding-left: 10px; background-color: #f8fbff">
              <li style="float: left; width: 240px;">
                <div style="width: auto; float: left; height: auto;">
                  <div style="width: auto; float: left; height: auto;">
                  <%: Html.RadioButton("chose", "1", false)%> 
                  </div>
                  <div style="width: auto; float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                    color: #000">
                    Kategoriye göre
                  </div>
                </div>
                </li>
               <li style="float: left; width: 240px;">
                <div style="width: auto; float: left; height: auto;">
                  <div style="width: auto; float: left; height: auto;">
                 <%: Html.RadioButton("chose", "2", false)%> 
                  </div>
                  <div style="width: auto; float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                    color: #000">
                    Paket Tipine göre
                  </div>
                </div>
                </li>
              </ul>
          </div>
          <div id="bireyselyadakurumsal" style="display:none;float:left;">
          <br /><br />
          <hr />
           kategoriye göre toplu mail.

            <div id="Div1" class="plusContent">
            <ul style="list-style: none; float: left; padding: 0px; width: 580px; border: solid 1px #8fc0d1;
              padding-top: 10px; padding-left: 10px; background-color: #f8fbff">
              <%MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
                var categorylist = entities.Categories.Where(c => c.CategoryParentId == null && c.MainCategoryType==1).ToList(); %>
                <li style="float: left; width: 240px;">
                <div style="width: auto; float: left; height: auto;">
                  <div style="width: auto; float: left; height: auto;">
               <%: Html.RadioButton("SectoreId", "1", false)%> 
                  </div>
                  <div style="width: auto; float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                    color: #000">
                    Tüm Kategoriler
                  </div>
                </div>
              </li>
              <%foreach (var item in categorylist)
                {  %>
              <li style="float: left; width: 240px;">
                <div style="width: auto; float: left; height: auto;">
                  <div style="width: auto; float: left; height: auto;">
                   <%: Html.RadioButton("SectoreId", item.CategoryId, false)%> 
                  </div>
                  <div style="width: auto; float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                    color: #000">
                    <%:item.CategoryName%>
                  </div>
                </div>
              </li>

              <%} %>
              </ul>
              </div>
          </div>
          <div id="Store" style="display:none;float:left;">
          <br />
          <br />
          <hr />
          <ul style="list-style: none; float: left; padding: 0px; width: 580px; border: solid 1px #8fc0d1;
              padding-top: 10px; padding-left: 10px; background-color: #f8fbff">
              
               <% var storepacket = entities.Packets.ToList();
                %>
               <li style="float: left; width: 240px;">
                <div style="width: auto; float: left; height: auto;">
                  <div style="width: auto; float: left; height: auto;">
                   <%: Html.RadioButton("StoreId", "0", false)%> 
                  </div>
                  <div style="width: auto; float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                    color: #000">
                    Tüm Firmalar
                  </div>
                </div>
                </li>
               <%foreach (var item in storepacket)
                {  %>
                <li style="float: left; width: 240px;">
                <div style="width: auto; float: left; height: auto;">
                  <div style="width: auto; float: left; height: auto;">
                  <%: Html.RadioButton("StoreId", item.PacketId, false)%> 
                  </div>
                  <div style="width: auto; float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                    color: #000">
                    <%:item.PacketName %>
                  </div>
                </div>
              </li>

              <%} %>
              </ul>
          </div>
          <%var dataConstant = entities.Constants.Where(c => c.ConstantType == 18).ToList();%>
           <div id="taslak" style="float:left;">
          <br />
          <br />
          <hr />
          <ul style="list-style: none; float: left; padding: 0px; width: 580px; border: solid 1px #8fc0d1;
              padding-top: 10px; padding-left: 10px; background-color: #f8fbff">
               <%foreach (var item in dataConstant)
                {  %>
                <li style="float: left; width: 240px;">
                <div style="width: auto; float: left; height: auto;">
                  <div style="width: auto; float: left; height: auto;">
                  <%: Html.RadioButton("TaslakId", item.ConstantId, false)%> 
                  </div>
                  <div style="width: auto; float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                    color: #000">
                    <%:item.ConstantName %>
                  </div>
                </div>
              </li>

              <%} %>
              </ul>
          </div>

          <div id="button3" style="float: left; margin-left: 10px;">
          <button type="submit" class="btnOnayla">
          Gönder
          </button>
        </div>
        <a href="/Member/MailAllMemberSender">Loglara bak</a>
        </div>
        <%} %>
</asp:Content>
