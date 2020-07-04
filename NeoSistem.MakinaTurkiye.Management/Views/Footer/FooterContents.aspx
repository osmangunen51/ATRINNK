﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ICollection<NeoSistem.MakinaTurkiye.Management.Models.FooterContentModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Footer Ana Başlıklar
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
  <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
  	<style type="text/css">
		/* Custom Theme */
		#superbox-overlay{background:#e0e4cc;}
		#superbox-container .loading{width:32px;height:32px;margin:0 auto;text-indent:-9999px;background:url(styles/loader.gif) no-repeat 0 0;}
		#superbox .close a{float:right;padding:0 5px;line-height:20px;background:#333;cursor:pointer;}
		#superbox .close a span{color:#fff;}
		#superbox .nextprev a{float:left;margin-right:5px;padding:0 5px;line-height:20px;background:#333;cursor:pointer;color:#fff;}
		#superbox .nextprev .disabled{background:#ccc;cursor:default;}
	</style>
    <script type="text/javascript"> 
        function DeleteFooterContent(ID)
        {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Footer/FooterContentDelete',
                    data: { id: ID },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        var e = data;
                        if (e) {
                            $('#row' + ID).hide();
                        }
                        else {
                            alert('Bu sabit kullanılıyor.Silme işlemi başarısız.');
                        }
                    }
                });
            }
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div style="width: 100%; margin: 0 auto;">
<button style="margin-top:10px;" onclick="window.location='/Footer/FooterContentAdd'" >Yeni Ekle</button>
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin">
          Başlık
          </td>
            <td>
                Üst Başlık
            </td>
            <td>
                Link
            </td>
            <td>
               Sıra
            </td>
          <td>
          </td>
        </tr>
      </thead>
      <tbody id="table">
       <% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++; %>
<tr id="row<%: item.FooterContentId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%: item.FooterContentName %>
  </td>
    <td>
        <%:item.FooterParentName %>
    </td>
    <td><a target="_blank" href="<%:item.FooterContentUrl %>"><%:item.FooterContentUrl %></a></td>
    <td>
        <%:item.DisplayOrder %>
    </td>
  <td >
      <div style="float: left;">
            <a style="cursor: pointer;" onclick="DeleteFooterContent(<%:item.FooterContentId %>)">
        <img src="/Content/images/delete.png" />
    </a>
     <a href="/Footer/FooterContentUpdate/<%: item.FooterContentId %>">
      <img src="/Content/images/edit.png"/>
    </a>
 </div>

 
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="9" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
          <%int pageNumbers = Convert.ToInt32(ViewData["pageNumbers"]);
            for (int i = 1; i <=pageNumbers ; i++)
            {
                if(i==(int)ViewData["page"])
                {
                    Response.Write("<li>"+i+"</li>");
                }
                else
                {
                    Response.Write("<li><a href='/CompanyDemand?page="+i+"'>" + i + "</a></li>");
                }
                
            } %>
      </ul>
    </div>
  </td>
</tr>

      </tbody>
<%--      <tfoot>
        <tr>
          <td class="ui-state ui-state-hover" style="border-color: #DDD;
            border-top: none; padding-right: 10px" colspan="1" valign="top" style="width: 150px;">
          <%:Ajax.ActionLink("Hesapla", "ProductRateCalculate", new AjaxOptions() { UpdateTargetId = "statisticproduct", HttpMethod = "Post" })%>
          </td>
          <td class="ui-state ui-state-hover" style="border-color: #DDD;
            border-top: none; padding-right: 10px" colspan="1">
           : <div id="statisticproduct" style="margin-top: 5px">

            </div>
          </td>
          <td class="ui-state ui-state-hover" colspan="1" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%= Model.Count %></strong>
          </td>
        </tr>
      </tfoot>--%>
    </table>
  </div>
</asp:Content>