﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.HelpListModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Satış Yardım Listesi
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style>

.fa {
  display: inline-block;
  font-style: normal;
  font-weight: normal;
  line-height: 1;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

* {
  box-sizing: border-box;
}

body {
  background: #f0f0f0;
}

.content {
  /*width: 260px;*/
  /*margin: 20px auto;*/
}

#demo-list a {
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
  width: 100%;
}

.jquery-accordion-menu, .jquery-accordion-menu * {
  box-sizing: border-box;
  outline: 0;
}

.jquery-accordion-menu {
  min-width: 260px;
  float: left;
  position: relative;
 border:1px solid #e2e4e7
}

.jquery-accordion-menu .jquery-accordion-menu-footer, .jquery-accordion-menu .jquery-accordion-menu-header {
  width: 100%;
  height: 50px;
  padding-left: 22px;
  float: left;
  line-height: 50px;
  font-weight: 600;
  color: #222222;
  background: #414956;
  font-size:18px;
}

.jquery-accordion-menu ul {
  margin: 0;
  padding: 0;
  list-style: none;
}

.jquery-accordion-menu ul li {
  width: 100%;
  display: block;
  float: left;
  position: relative;
}

.jquery-accordion-menu ul li a {
  width: 100%;
  padding: 14px 22px;
  float: left;
  text-decoration: none;
  color: #222222;
  font-size: 13px;
  background: #f5f5f5;
  white-space: nowrap;
  position: relative;
  overflow: hidden;
  transition: color .2s linear, background .2s linear;
}

.jquery-accordion-menu>ul>li.active, .jquery-accordion-menu>ul>li:hover>a {
  color: #fff;
  background: #4177dc;
}

.jquery-accordion-menu>ul>li>a {
  border-bottom: solid 1px #3b424d;
}

.jquery-accordion-menu ul li a i {
  width: 34px;
  float: left;
  line-height: 18px;
  font-size: 16px;
  text-align: left;
}

.jquery-accordion-menu .submenu-indicator {
  float: right;
  right: 22px;
  position: absolute;
  line-height: 19px;
  font-size: 20px;
  transition: transform .3s linear;
}

.jquery-accordion-menu ul ul.submenu .submenu-indicator {
  line-height: 16px;
}

.jquery-accordion-menu .submenu-indicator-minus>.submenu-indicator {
  transform: rotate(45deg);
}

.jquery-accordion-menu ul ul.submenu, .jquery-accordion-menu ul ul.submenu li ul.submenu {
  width: 100%;
  display: none;
  position: static;
}

.jquery-accordion-menu ul ul.submenu li {
  clear:both;
  width: 100%;
}

.jquery-accordion-menu ul ul.submenu li a {
  width: 100%;
  float: left;
  font-size: 13px;
  background: #f5f5f5;
  border-top: none;
  position: relative;
  border-left: solid 6px transparent;
  transition: border .2s linear;
}

.jquery-accordion-menu ul ul.submenu li:hover>a {
  border-left-color: #414956;
}

.jquery-accordion-menu ul ul.submenu>li>a {
  padding-left: 30px;
}

.jquery-accordion-menu ul ul.submenu>li>ul.submenu>li>a {
  padding-left: 45px;
}

.jquery-accordion-menu ul ul.submenu>li>ul.submenu>li>ul.submenu>li>a {
  padding-left: 60px;
}

.jquery-accordion-menu ul li .jquery-accordion-menu-label, .jquery-accordion-menu ul ul.submenu li .jquery-accordion-menu-label {
  min-width: 20px;
  padding: 1px 2px 1px 1px;
  position: absolute;
  right: 18px;
  top: 14px;
  font-size: 11px;
  font-weight: 800;
  color: #555;
  text-align: center;
  line-height: 18px;
  background: #f0f0f0;
  border-radius: 100%;
}

.jquery-accordion-menu ul ul.submenu li .jquery-accordion-menu-label {
  top: 12px;
}

.ink {
  display: block;
  position: absolute;
  background: rgba(255,255,255,.3);
  border-radius: 100%;
  transform: scale(0);
}

.animate-ink {
  animation: ripple .5s linear;
}

@keyframes ripple {
  100% {
    opacity: 0;
    transform: scale(2.5);
  }
}

.red.jquery-accordion-menu .jquery-accordion-menu-footer,.red.jquery-accordion-menu .jquery-accordion-menu-header,.red.jquery-accordion-menu ul li a {
	background: #ebf1f6
}

.red.jquery-accordion-menu>ul>li.active>a,.red.jquery-accordion-menu>ul>li:hover>a {
	background: #3d6bcd
}

.red.jquery-accordion-menu>ul>li>a {
	border-bottom-color: #ebf1f6
}

.red.jquery-accordion-menu ul ul.submenu li:hover>a {
	border-left-color: #ebf1f6
}
    </style>
      <script type="text/javascript">

          function HelpSearch() {
              var value = $("#HelpSearchText").val();
              if (value.length > 2) {
           
                  $.ajax({
                      url: '/Help/SearchByText',
                      data: {
                          SearchText: value
                      },
                      type: 'post',
                      success: function (data) {
                          $("#table").html("");
                          $('#table').html(data);
                       

                      },
                      error: function (x, a, r) {
                          alert("Error");

                      }
                  });
              }
              else {
                  PageHelp(1);
              }
              if (value == "") {
               
                  PageHelp(1);
              }
          }

          function DeleteHelp(id) {
   
                         $.ajax({
          url: '/Help/Delete',
          data: {
         ID: id 
          },
          type: 'post',
         success: function (data) {
             $("#row" + id).hide();

          },
               error: function (x, a, r) {
                   alert("Error");
         
          }
        });
          }
    function PageHelp(page) {
           $.ajax({
          url: '/Help/GetForPaging',
          data: {
         newPage: page 
          },
          type: 'post',
          success: function (data) {
              $("#table").html("");
                          $('#table').html(data);

        
          },
               error: function (x, a, r) {
                   alert("Error");
         
          }
        });
  
     
    }
  </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div style="width: 100%; margin: 0 auto;">

<button style="margin-top:10px;" onclick="window.location='/Help/Add'" >Yeni Ekle</button>
        <div style="margin-top:20px">Ara:  <input id="HelpSearchText" onfocus="PageHelp(1)"  onkeyup="HelpSearch()" class="" placeholder="Anahtar Kelime.." style="width: 30%; " /></div>

    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
       
          <td class="Header">
          Konu
          </td>
            <td  class="Header">
               Content
            </td>
            <td  class="Header">
               Tarih
            </td>
              <td class="Header">
          Kategori
          </td>
            <td class="Header">
            
            </td>
        </tr>
      </thead>
      <tbody id="table">
          <%Html.RenderPartial("_HelpDataItem",Model); %>
          </tbody>
        </table>
      </div>
</asp:Content>

