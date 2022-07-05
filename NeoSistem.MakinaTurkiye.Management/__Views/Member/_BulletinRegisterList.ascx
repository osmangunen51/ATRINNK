<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.BulletinMemberModel>>" %>

<%:Html.HiddenFor(x=>x.Order) %>
<%:Html.HiddenFor(x=>x.CurrentPage) %>

<%foreach (var item in Model.Source.ToList())
 {%>
              <tr id="row<%:item.BulletinMemberId %>">
                  <td onclick="PageOrder('BulletinMemberId')" class="Cell CellBegin">##<%:item.BulletinMemberId %></td>
                  <td class="Cell"><%:item.Email %></td>
                  <td class="Cell"><%:item.MemberName+" "+item.MemberSurname %> </td>
                  <td class="Cell"><%:string.Join(",",item.Categories.Select(x=>x.CategoryName).ToList())%></td>
                  <td class="Cell"><%:item.RecordDate.ToString("dd-MM-yyyy HH:mm") %></td>
              <td class="Cell CellEnd"><a  onclick="DeleteRecord(<%:item.BulletinMemberId %>)" >Sil</a></td>
          
             </tr>

 <%} %>
<tr>
      <td class="ui-state ui-state-default" colspan="6" align="right" style="border-color: #DDD;
	 border-top: none; border-bottom: none;">
	 <div style="float: right;" class="pagination">
         <div style="float:left; margin-right:10px;">
      <b> <%:Model.CurrentPage %> . sayfadasınız
             </div>
		<ul style="float:right;">
         
		  <% foreach (int i in Model.TotalLinkPages )
       { %>
		  <li>
			 <% if (i == Model.CurrentPage)
       { %>
			 <span class="currentpage">
				<%: i%></span>&nbsp;
			 <% } %>
			 <% else
       { %>
			 <a href="javascript:void(0)" onclick="PagingBulletin(<%:i %>)">
				<%: i%></a>&nbsp;
			 <% } %>
		  </li>
		  <% } %>

      
		</ul>
	 </div>
  </td>

</tr>
<tr>
      <td class="ui-state ui-state-default" colspan="6" align="right" style="border-color: #DDD;
	 border-top: none; border-bottom: none;">
        <div style="float:right">
            Toplam Kayıt:<%:Model.TotalRecord %>
        </div>
  </td>

</tr>