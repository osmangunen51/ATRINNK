<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.Statistics.MTProductStatisticViewModel>" %>

<div class="col-md-7" >
    <%=Html.RenderHtmlPartial("_ProductStatistic",Model.MTStatisticModel) %>
</div>
<div class="col-md-5" style="margin-top: 10px; margin-bottom: 10px;">
    <div class="">
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
        <div class="col-md-12" style="margin-top: 20px;">
            <table class="table table-striped">
                <tr>
                    <th>Ürün Adı</th>
                    <th title="Görüntülenme Sayısı">Görüntülenme S.</th>
                </tr>
                <%foreach (var item in Model.ProductItems.OrderByDescending(x => x.ViewCount))
                    {
                        string url = "/Account/Statistic/ProductStatistics";
                        if (Request.QueryString["day"] != null)
                        {
                            url = url + "?day=" + Request.QueryString["day"].ToString() + "&ProductId=" + item.ProductId;
                        }
                        else
                        {
                            url = url + "?ProductId=" + item.ProductId;
                        }
                %>
                <tr>
                    <td><a href="<%=url %>"><%:item.ProductName %></a></td>
                    <td><%:item.ViewCount %></td>
                </tr>
                <%} %>
            </table>
        </div>
    </div>
    <div class="col-md-6" >
        <h3>Bölgeler</h3>
        <div style="height: 400px;
  overflow: scroll;">

  
        <table class="table table-striped">
            <tr>
                <th>Ülke</th>
                <th>Şehir</th>
                <th title="Görüntülenme Sayısı">Görüntülenme S.</th>
            </tr>
            <%foreach (var item in Model.MTStatisticLocationModels.OrderByDescending(x => x.ViewCount))
                {%>
            <tr>
                <td><%:item.Country %></td>
                <td><%:item.City %></td>
                <td><%:item.ViewCount %></td>
            </tr>
            <%} %>
            <%if (Model.ProductItems.Count > 20)
                {%>
            <tr>
                <td colspan="3"><a href="">Tümünü Gör</a></td>
            </tr>
            <% } %>
        </table>
                  </div>
    </div>

</div>
