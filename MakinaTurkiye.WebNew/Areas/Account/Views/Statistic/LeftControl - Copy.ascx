<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<%using (Html.BeginLeft("İstatistik"))
  {%>
<ul>
  <li><a style="color:Gray;" href="/Account/Statistic/Index">
    Genel</a> </li>
    <%if (AuthenticationUser.Membership.MemberType == 20)
      {  %>
  <li><a style="color:Gray;" href="/Account/Statistic/Index?pagetype=1">
    Firma Görüntülenme</a> </li>
    <%} %>
   <li><a style="color:Gray;" href="/Account/Statistic/Index?pagetype=3">
    İlan Görüntülenme</a> </li>

</ul>
<%}%>