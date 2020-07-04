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
                          Firma Kısa Adı :
                         </th>
                        <td>
                            <%=Model.StoreShortName%>
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <th style="width: 160px;">Firma Ünvanı :
                        </th>
                        <td>
                            <%=Model.StoreName%>
                        </td>
                    </tr>
                    <tr>
                        <th>Faaliyet Tipi :
                        </th>
                        <td>
                            <%:Model.StoreActivity %>
                        </td>
                    </tr>
                    <tr>
                        <th>Firma Kısa Bilgi :
                        </th>
                        <td>
                            <%=Model.StoreAboutShort%>
                        </td>
                    </tr>
                    <tr>
                        <th>Çalışan Sayısı :
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
                        <th width="30%">Şirket Türü :
                        </th>
                        <td>
                            <%:Model.StoreType %>
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <th>Kuruluş Yılı :
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
                        <th width="30%">Sermaye :
                        </th>
                        <td>
                            <%:Model.StoreCapital %>
                        </td>
                    </tr>
                    <%} %>
                    <tr>
                        <th width="30%">Yıllık Ciro :
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
                        <th width="30%">Ticaret Sicil No:
                        </th>
                        <td>
                            <%:Model.TradeRegistrNo %>
                        </td>
                    </tr>
                    <%} %>
                    <%if (Model.StoreInfoNumberShow.MersisNoShow)
                        { %>
                    <tr>
                        <th width="30%">Mersis No:
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


