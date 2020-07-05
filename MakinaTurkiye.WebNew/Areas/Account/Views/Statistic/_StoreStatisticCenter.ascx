<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.Statistics.MTStoreStatisticViewModel>" %>

<%--  <script src="https://www.chartjs.org/samples/latest/utils.js"></script>--%>
<%--    <script src="https://www.chartjs.org/dist/2.8.0/Chart.min.js"></script>--%>

<div class="row">
    <%=Html.RenderHtmlPartial("_StoreStatistic",Model.MTStoreStatisticModel) %>
    <div class="col-md-5" style="margin-top: 10px; margin-bottom: 10px;">

        <div class="col-md-12">
            <h3>Metric Değerler</h3>
            <div class="col-md-6 number-container" style="">

                <span>Kullanıcılar</span>

                <div class="col-md-12">
                    <b><%=Model.TotalUserCount %></b>
                </div>
            </div>
            <div class="col-md-6  number-container">
                <span>Görütülenme Sayısı</span>
                <div class="col-md-12">
                    <b><%:Model.TotalViewCount %></b>
                </div>

            </div>
        </div>
        <div class="col-md-12">
            <h3>Bölgeler</h3>
            <table class="table table-striped">
                <tr>
                    <th>Ülke</th>
                    <th>Şehir</th>
                    <th>Adet</th>
                </tr>
                <%foreach (var item in Model.MTStatisticLocationModels)
                    {%>
                <tr>
                    <td><%:item.Country %></td>
                    <td><%:item.City %></td>
                    <td><%:item.ViewCount %></td>
                </tr>
                <%} %>
            </table>
        </div>


    </div>
</div>
