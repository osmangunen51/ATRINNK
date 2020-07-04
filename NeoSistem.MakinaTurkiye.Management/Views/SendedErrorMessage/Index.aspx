﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.SendErrorMessageModel>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
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
        function DeleteErrorMessage(ID)
        {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/SendedErrorMessage/DeleteErrorMessage',
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
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin">
          Gönderen No
          </td>
            <td class="Header">Gönderen Adı</td>
            <td class="Header">
                Alıcı No
            </td>
            <td class="Header">Alıcı Adı</td>
            <td class="Header">Product No</td> 
            <td class="Header">Konu</td>
            <td class="Header">Mesaj</td>
            <td class="Header">Tarih</td>
          <td class="Header HeaderEnd">
          </td>
        </tr>
      </thead>
      <tbody id="table">
       <% int row = 0; %>
<% foreach (var item in Model.Source)
   { %>
<% row++; %>
<tr id="row<%: item.ID %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="Cell CellBegin">
    <%: item.SenderMemberNo %>
  </td>
    <td class="Cell">
        <%:item.SenderName %>
    </td>
    <td class="Cell">
        <%:item.ReceiverMemberNo %>
    </td>
    <td class="Cell"><%:item.ReceiverName %></td>
    <td class="Cell"><%:item.ProductNo %></td>
    <td class="Cell">
        <%:item.MessageSubject %>
    </td>
    <td class="Cell">
    <%:item.MessageContent %>
    </td>  
    <td class="Cell CellEnd"><%:item.ErrorDate %></td>
  <td >
    <a style="cursor: pointer;" onclick="DeleteErrorMessage(<%:item.ID %>)">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
      </div>
    </a>
  </td>
 
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="9" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
          <%
       
            foreach (int i in Model.TotalLinkPages)
            {
                if(i==Model.CurrentPage)
                {%>
                  <li><span class="currentpage"><%:i %></span></li>    
               
                <%}
                    else { 
                %>
                <li><a href="/SendedErrorMessage?page=<%:i %>"><%:i %></a></li>
                 
                <%}
                
            }
            %>
      </ul>
    </div>
  </td>
</tr>
          <tr>
              <td class="ui-state ui-state-default" colspan="9" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
                  <b>Toplam Kayıt:</b><%:Model.TotalRecord %>
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