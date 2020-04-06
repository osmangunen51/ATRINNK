<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.MTProductCatologItem>>" %>

<%if (Model.Count > 0)
    {%>



<%foreach (var item in Model.ToList())
    {%>
<div class="col-md-2" id="catolog<%:item.CatologId %>">
    <div class="thumbnail thumbnail-mt">
        <a target="_blank" href="<%:item.FilePath%>" class="pdf-icon-link">
            <img class="pdf-icon" src="/Content/V2/images/pdf-icon.png" /></a>
        <a onclick="DeleteCatolog(<%:item.CatologId %>)" style="cursor:pointer;">Sil</a>
    </div>
</div>
<%} %>
<% }
    else { %>
    <p>Eklenen E-Katolog Bulunmamaktadır*</p>
<%}%>