<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.Statistics.MTStatisticModel>" %>


<script src="../../../../Content/V2/assets/js/chart-util.js"></script>
<script src="../../../../Content/V2/assets/js/Chart.min.js"></script>
<script>

    var config = {
        type: 'line',
        data: {
            labels: <%=Model.JsoonLabels%>,
                datasets: [{
                    label: 'Görüntülenme Sayısı',
                    backgroundColor: window.chartColors.blue,
                    borderColor: window.chartColors.blue,
                    data: <%=Model.JsonDatas%>,
                    fill: false,
                }]
            },
            options: {
                responsive: true,
                title: {
                    display: true,
                    text: 'Genel Ürün Görüntülenme Oranları <%=Model.DateString%>'
                },
                tooltips: {
                    mode: 'index',
                    intersect: false,
                },
                hover: {
                    mode: 'nearest',
                    intersect: true
                },
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: '<%=Model.LabelString%>'
                    }
                }],
                yAxes: [{
                    display: true,
                    scaleLabel: {
                        display: true,
                        labelString: 'Görüntülenme Değeri'
                    }
                }]
            }
        }
    };
    $(document).ready(function () {
        var ctx = document.getElementById('canvas').getContext('2d');
        window.myLine = new Chart(ctx, config);
    });



</script>
   <canvas id="canvas"></canvas>