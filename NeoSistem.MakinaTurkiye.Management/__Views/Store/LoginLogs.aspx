<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.FilterModel<MakinaTurkiye.Entities.Tables.Logs.LoginLog>>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
        Telefon Bilgileri Değişikliği
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
  <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
    <script type="text/javascript">
        function DeletePost(changeId) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $("#preLoading").show();
                $.ajax({
                    url: '/Store/loginLogDelete',
                    data: { id: changeId },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                     
                        if (data) {
                               $("#preLoading").hide();
                            $('#row' + changeId).hide();

                        }
                    }
                });
            }
        }
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
    <script type="text/javascript"> 

    </script>
</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
      <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px;
    border-width: 5px;" id="preLoading">
    <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
    <strong>Siliniyor.</strong> Lütfen bekleyiniz...
  </div>
        <table cellpadding="8" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
            <td style="width:3%;" class="Header HeaderBegin">Sıra No</td>
        
          <td class="Header HeaderBegin" style="width: 12%;" >
            Mağaza Adı
          </td>
          <td class="Header" style="width: 4%;" >
            İp Adresi
          </td>
          <td class="Header" style="width: 5%;">
         Tarih
          </td>
          <td class="Header HeaderEnd" style="width: 8%;">
            
          </td>
        </tr>
        
       </thead>
            <tbody>
                <% int row=1;
                    foreach(var item in Model.Source) {%>
       <tr id="row<%: item.LoginLogId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
           <td class="CellBegin"><%:row %></td>
          
    <td class="Cell"><%if(item.Store!=null) {%><a target="_blank"  href="/Store/EditStore/<%:item.Store.MainPartyId %>"><%:item.Store.StoreName %><%} %></a></td>
           <td class="Cell"><%:item.IpAddress %></td>
           <td class="Cell"><%:item.LoginDate.ToString("dd/MM/yyyy HH:mm") %></td>
           <td class="Cell"><a href="javascript:void(0)" onclick="DeletePost(<%:item.LoginLogId %>)"><img src="/Content/images/delete.png" /></a></td>
                <% row++;
                    } %>
          
        </tr>
  <td class="ui-state ui-state-default" colspan="6" align="right" style="border-color: #DDD;
	 border-top: none; border-bottom: none;">
	 <div style="float: right;" class="pagination">
		<ul>

		  <% foreach (var i in Model.TotalPages)
       { %>
		  <li>
			 <% if (i == Model.CurrentPage)
       { %>
			 <span class="currentpage">
				<%: i%></span>&nbsp;
			 <% } %>
			 <% else
       { %>
			 <a href="/store/loginlogs?page=<%:i %>">
				<%: i%></a>&nbsp;
			 <% } %>
		  </li>
		  <% } %>

		</ul>
	 </div>
  </td>
</tr>
   </tbody>
      </table>
</asp:Content>

