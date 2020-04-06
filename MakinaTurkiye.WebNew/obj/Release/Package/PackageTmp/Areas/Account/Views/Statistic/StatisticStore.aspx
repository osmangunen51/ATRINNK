<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.Statistics.MTStoreStatisticViewModel>" %>

<%@ Import Namespace="NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Firma Görüntülenme İstatistikleri

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>Firma İstatistikleri
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>
                <div class="well" style="background-color: #fff!important;">
                    <div class="alert alert-info">
                        <span class="glyphicon glyphicon-info-sign"></span>Makina Türkiye özel reklam seçenekleri
                ile firmanızın görüntülenme sayısını arttırabilirsiniz. Detaylı bilgi için <a href="/reklam-y-141615">buraya </a>tıklayın.
                    </div>

                    <div class="row">


                        <div class="col-md-12">
                            <div style="float: right">
                                            <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Başlangıç Tarihi</label>
                                            <%:Html.TextBox("startDate","", new {@class="mt-form-control date", @autocomplete="off" }) %>
                        
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Bitiş Tarihi</label>
                                                     <%:Html.TextBox("endDate","", new {@class="mt-form-control date", @autocomplete="off" }) %>
                           
                                        </div>
                                    </div>
                       
                            </div>
                        </div>
                    </div>
                    <div class="loading">Loading&#8230;</div>
                    <div id="data">

                        <%=Html.RenderHtmlPartial("_StoreStatisticCenter",Model) %>
                    </div>


                    <%--                    <div class="alert alert-warning">
                        <span class="glyphicon glyphicon-question-sign"></span><b>Çoğul Hit ve Tekil Hit arasındaki
                    fark nedir? </b>
                        <br>
                        Firma profilinizi ziyaret eden kullanıcı aynı gün içerisinde profilinizi tekrar
                ziyaret ettiğinde çoğul hitiniz artmakta ancak tekil hitiniz değişmemektedir.
                    </div>--%>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
     <script type="text/javascript" src="../../../../Content/V2/assets/js/bootstrap-datepicker.min.js"></script>
       <script type="text/javascript" src="../../../../Content/V2/assets/js/bootstrap-datepicker.tr.min.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $('.date').datepicker({
                format: 'dd/mm/yyyy',
                language: 'tr',
                todayHighlight: true
            });

            $('#endDate').change(function () {

                $(".loading").show();

                $.ajax({
                    url: '/Account/Statistic/GetStoreStatistic',
                    type: 'get',
                    data: { startDate: $("#startDate").val(), endDate: $("#endDate").val() },
                    success: function (data) {
                        $("#data").html(data);
                        $(".loading").hide();
                    }
                });
            });
        });
    </script>
</asp:Content>
