﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<List<NeoSistem.MakinaTurkiye.Management.Models.StoreModel>>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 Firmalar
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div style="width: 100%; margin: 0 auto;">

    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header">
            #
          </td>
            <td  class="Header">
                Firma Adı
            </td>
            <td class="Header">
               Kayıt Tarihi
            </td>
        </tr>
      </thead>
      <tbody id="table">
          <%foreach (var item in Model.ToList())
              {%>
              <tr id="row<%:item.MainPartyId %>">
                  <td><%:item.MainPartyId %></td>

              <td class="Cell"><a href="/Store/EditStore/<%:item.MainPartyId %>"><%:item.StoreName %></a></td>
                  <td class="Cell"><%:item.StoreRecordDate.ToString("dd/MM/yyyy") %></td>
          </tr>
              <%} %>
      
          </tbody>
   <tr>
      <td class="ui-state ui-state-default" colspan="3" align="right" style="border-color: #DDD;
	 border-top: none; border-bottom: none;">
	 <div style="float: right;" class="pagination">
         <div style="float:left; margin-right:10px;">
      
             </div>
		<ul style="float:right;">
         <%int pages = ViewData["pageNumbers"].ToInt32(); %>
		  <% for (int i=1; i<=pages; i++)
            { %>
		  <li>
			 <a href="/Store/WithoutDescriptionStore?page=<%:i %>">
				<%: i%></a>&nbsp;
		
		  </li>
		  <% } %>
		</ul>
	 </div>
  </td>

</tr>
        </table>
      </div>

</asp:Content>

