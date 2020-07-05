<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.Statistics.ProductStatisticModel>" %>

<script type="text/javascript">
    $(document).ready(function () {
        $("#loading").show();
        $.ajax({
            url: '/Account/Statistic/GetProductStatisticDay',
            type: 'get',
            data: {},
            success: function (data) {
                $("#productStatistic").html(data);
                $("#loading").hide();
            }
        });
    });
</script>
<div class="col-sm-12 col-md-12">
    <div>
        <h4 class="mt0 text-info">
            <span class="text-primary glyphicon glyphicon-cog"></span>&nbsp;İlan İstatistikleri
        </h4>
        <div class="well well-mt2">
            <div class="alert alert-info">
                <span class="glyphicon glyphicon-info-sign"></span>&nbsp;Makina Türkiye özel reklam seçenekleri
                ile ilanlarınızın görüntülenme sayısını arttırabilirsiniz. Detaylı bilgi için <a
                    href="#">buraya </a>tıklayın.
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div id="productStatistic" style="text-align: center;">
                        <div id="loading" style="margin: 0;">
                            <div>
                                <b style="color: #0031b7">Saatlik İstatistik Yükleniyor..</b><br />
                                <img src="/Content/v2/images/loading1.gif" />
                            </div>
                        </div>
                    </div>
                    <div style="float: right;"><a href="/Account/Statistic/ProductStatistics" class="btn btn-info">Detaylı İstatistiğe Git</a></div>

                </div>
                <div class="col-md-6">
                    <div id="AdvertData">
                        <%=Html.RenderHtmlPartial("_ProductLists",Model.ProductItems) %>
                    </div>
                               <div class="alert alert-warning">
                    <span class="glyphicon glyphicon-question-sign"></span><b>&nbsp;Çoğul Hit ve Tekil Hit arasındaki
                    fark nedir? </b>
                    <br>
                    Firma profilinizi ziyaret eden kullanıcı aynı gün içerisinde profilinizi tekrar
                ziyaret ettiğinde çoğul hitiniz artmakta ancak tekil hitiniz değişmemektedir.
                </div>
                </div>
     
            </div>


        </div>
    </div>
</div>
