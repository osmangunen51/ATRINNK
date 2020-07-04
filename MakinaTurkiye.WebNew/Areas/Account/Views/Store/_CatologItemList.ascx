<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores.MTCatologItem>>" %>



<%foreach (var item in Model.ToList())
    {%>
<tr id="row<%:item.CatologId %>">
    <td><%:item.Name %></td>
    <td><a href="<%:item.FilePath %>" target="_blank">Görüntüle</a></td>
    <td><%:item.FileOrder %></td>
    <td><a href="#" data-toggle="modal" data-target="#CatologEdit" onclick="CatologEditShow(<%=item.CatologId %>)" ><span style="font-size:16px;" class="glyphicon glyphicon-pencil" style="color:#1051e9"></span></a>|<a style="cursor:pointer" onclick="DeleteCatolog(<%:item.CatologId %>)"><i style="color:#1051e9; font-size:16px;" class="fa fa-trash"></i></a></td>
</tr>
<%} %>