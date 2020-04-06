<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTCompanyProfileModel>" %>
<%if (Model.Certificates.Count > 0)
    {%>
<div class="row">
    <div class="col-md-12 certificate-popup-gallery">
        <h2 class="section-title">
            <span style="background-color: #fff"><%:Model.MTStoreProfileHeaderModel.StoreName %> Sertifikaları</span>
        </h2>
        <%foreach (var item in Model.Certificates.ToList())
            {%>
        <div class="col-md-3">
            <a class="img-cerficate-link" href="<%:item.ImageFullPath%>" title="<%:item.Name %>">
                <img class="img-responsive" style="border: 1px solid #ccc; -webkit-box-shadow: -2px 4px 7px -3px #000000; box-shadow: -2px 4px 7px -3px #000000;" src="<%:item.ImagePath.Replace("png","jpg") %>" alt="<%:item.Name %>" />
            </a>
            <p style="font-size: 16px; margin-top: 5px;"><%:item.Name %></p>
        </div>
        <%} %>
    </div>
</div>
<%} %>