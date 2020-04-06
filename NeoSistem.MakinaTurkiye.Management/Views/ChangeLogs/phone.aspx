
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ICollection<MakinaTurkiye.Entities.Tables.Common.PhoneChangeHistory>>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
        Telefon Bilgileri Değişikliği
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
  <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
    <script type="text/javascript">
        function DeletePost(changeId) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/ChangeLogs/phoneChangeHistoryDelete',
                    data: { id: changeId },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                     
                        if (data) {
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
        <table cellpadding="8" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
            <td style="width:3%;" class="Header HeaderBegin">Sıra No</td>

          <td class="Header"  style="width: 5%;">
            MainPartyId
          </td>
        <td class="Header" style="width:5%">
            Üye Adı
        </td>
          <td class="Header" style="width: 12%;" >
            Mağaza Adı
          </td>
          <td class="Header" style="width: 4%;" >
            Ülke Kodu
          </td>
          <td class="Header" style="width: 5%;">
            Alan kodu
          </td>
          <td class="Header" style="width: 10%;" >
         Telefon

          </td>
            <td class="Header" style="width:7%;">
                Tarih
            </td>
          <td class="Header" style="width: 8%;">
            Araçlar
          </td>
        </tr>
        
       </thead>
            <tbody>
                <%=Html.RenderHtmlPartial("PhoneList",Model) %>
          <tr>
  <td class="ui-state ui-state-default" colspan="9" align="right" style="border-color: #DDD;
	 border-top: none; border-bottom: none;">
	 <div style="float: right;" class="pagination">
		<ul>
            <%int totalPages = Convert.ToInt32(ViewData["pageNumbers"]); %>
		  <% for (int i = 1; i <= totalPages; i++ )
       { %>
		  <li>
			 <% if (i == Convert.ToInt32(ViewData["page"]))
       { %>
			 <span class="currentpage">
				<%: i%></span>&nbsp;
			 <% } %>
			 <% else
       { %>
			 <a href="/ChangeLogs/phone?page=<%:i %>">
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

