<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTStoreAboutModel>" %>
<div class="row mb20">
    <div class="col-sm-12">

        <div id="carousel-example-generic" class="carousel slide hover carousel-mt2 pull-left img-left col-md-6 m0"
            data-ride="carousel">
            <div class="carousel-inner">
                <div id="test-slider-1" class="item active">
                    <img style="width: auto; height: 100%; max-width: 100%;" src="<%:Model.AboutImagePath %>"
                        alt="<%:Model.StoreName%>" title="<%:Model.StoreName %>" />
                </div>
            </div>
        </div>
        <div class="col-md-6">

            <table class="table table-striped">
                <tbody>
                    <%if (!string.IsNullOrEmpty(Model.StoreShortName))
                        { %>
                    <tr>
                        <th style="width: 160px;">
                            <h3>Firma Kısa Adı :
                            </h3>
                        </th>
                        <td>
                            <%=Model.StoreShortName%>
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <th style="width: 160px;"><h3>Firma Ünvanı :</h3>
                        </th>
                        <td>
                            <%=Model.StoreName%>
                        </td>
                    </tr>
                    <tr>
                        <th><h3>Faaliyet Tipi :</h3>
                        </th>
                        <td>
                            <%:Model.StoreActivity %>
                        </td>
                    </tr>
                    <tr>
                        <th><h3>Firma Kısa Bilgi :</h3>
                        </th>
                        <td>
                            <%=Model.StoreAboutShort%>
                        </td>
                    </tr>
                    <tr>
                        <th><h3>Çalışan Sayısı :</h3>
                        </th>
                        <td>
                            <%if (!string.IsNullOrEmpty(Model.StoreEmployeeCount))
                                {%>
                            <%:Model.StoreEmployeeCount %>
                            <% }
                                else
                                {%>
                                    Belirtilmedi   
                                    <% } %>
                        </td>
                    </tr>
                    <%if (Model.StoreType != null)
                        {  %>
                    <tr>
                        <th width="30%"><h3>Şirket Türü :</h3>
                        </th>
                        <td>
                            <%:Model.StoreType %>
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <th><h3>Kuruluş Yılı :</h3>
                        </th>
                        <td>
                            <%if (!string.IsNullOrEmpty(Model.StoreEstablishmentDate))
                                {
                            %>
                            <%= Model.StoreEstablishmentDate%>
                            <%}
                                else
                                {
                            %>
                                        Belirtilmedi
                                    <%} %>
                              
                        </td>
                    </tr>
                    <%if (Model.StoreCapital != null)
                        { %>
                    <tr>
                        <th width="30%"><h3>Sermaye :</h3>
                        </th>
                        <td>
                            <%:Model.StoreCapital %>
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <th width="30%"><h3>Yıllık Ciro :</h3>
                        </th>
                        <td>
                            <%if (!string.IsNullOrEmpty(Model.StoreEndorsement))
                                {%>
                            <%:Model.StoreEndorsement %>
                            <%}
                                else
                                {%>
                                    Belirtilmedi
                                        <%} %>
                        </td>
                    </tr>
                    <%if (Model.StoreInfoNumberShow.TradeRegistryNoShow)
                        { %>
                    <tr>
                        <th width="30%"><h3>Ticaret Sicil No:</h3>
                        </th>
                        <td>
                            <%:Model.TradeRegistrNo %>
                        </td>
                    </tr>
                    <%} %>
                    <%if (Model.StoreInfoNumberShow.MersisNoShow)
                        { %>
                    <tr>
                        <th width="30%"><h3>Mersis No:</h3>
                        </th>
                        <td>
                            <%:Model.MersisNo %>
                        </td>
                    </tr>
                    <%} %>
                </tbody>
            </table>
        </div>
    </div>
</div>
<%if (!string.IsNullOrEmpty(Model.AboutText)) {%>
<div class="row">
    <div class="col-sm-12 pt20 aboutusText">
        <div class="col-md-12" id="aboutTextDetail">
            <%= Html.Raw(Model.AboutText)%>
        </div>
    </div>

</div>
<% } %>


