<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Catologs.MTProductDopingModel>" %>


<div class="modal-content">
    <div class="modal-header" style="height: 60px; border: 0px!important;">
        <h3 class="modal-title" style="float: left;" id="exampleModalLabel">Doping Satın Al!</h3>
        <button type="button" style="float: right;" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
    </div>
    <div class="modal-body">
        <% float dayPrice = (float)(Model.ProductDopingPricePerMonth / 30); %>
        <div class="alert alert-info">
            <i class="fa fa-3x fa-bullhorn pull-left"></i>
            <b><span style="font-size: 17px"><%:dayPrice.ToString("C2") %></span> günlük ücret ile, </b>
            makinaturkiye.com'da ilanınız kategorilerde ilk sıralara çıkarılır ve ilanlarınız daha fazla kişi tarafından görüntülenerek alıcısına çok daha hızlı bir şekilde ulaşır.
        </div>
        <form action="/MemberShipSales/PayWithCreditCard" method="get">
     
            <%:Html.HiddenFor(x=>x.ProductId) %>
            <%:Html.Hidden("DayPrice",dayPrice.ToString()) %>
            <div class="form-horizontal">
                <div class="form-group">
                    <label class="col-md-3">Ürün Adı</label>
                    <div class="col-md-9"><%:Model.ProductName %></div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 contorl-label">Kategori Adı:</label>
                    <div class="col-md-9">
                        <b><%:Html.Raw(Model.CategoryName) %></b>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 contorl-label">Süre:</label>
                    <div class="col-md-9">
                        <select class="form-control" name="PacketId" onchange="PriceChange();" id="PacketId">
                            <%foreach (var item in Model.Packets)
                            {%>
                            <option value="<%:item.Value %>"><%:item.Text %> Gün</option>
                             <%} %>
                        </select>
                    </div>
                    <div class="row">
                        <div class="col-md-3"></div>
                        <div id="errorMessageDay" class="col-md-9" style="color: #b40505; font-size: 14px; display: none;">
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-md-3 contorl-label">Fiyat</label>
                    <div class="col-md-9">
                        <b id="Price" style="font-size: 16px;"><%:Html.Raw(Model.ProductDopingPricePerMonth.ToString("C2")) %></b>
                    </div>
                </div>
                <div class="form-group">
                    <div style="float: right; margin-right: 15px;">
                        <button type="submit" id="buttonPay" onclick="if(!TakeProductDoping())return false;" class="btn background-mt-btn btn-block btn-bg">Satın Al <span class="glyphicon glyphicon-chevron-right"></span></button>
                    </div>
                </div>

            </div>
        </form>
    </div>
</div>
<script type="text/javascript">
    function PriceChange() {
        var packetId = $("#PacketId").val();
       
        $.ajax({
            url: '/Account/ilan/GetDopingPrice',
            type: 'get',
            data:
                {
                    PacketId: packetId,
        

                },
            success: function (data) {
                $("#Price").html(data);
            },
            error: function (x, l, e) {
                alert(e.responseText);
            }
        });
    }
    function TakeProductDoping() {

        var dopingDay = $("#DopingDay").val();
        var dayPrice = $("#ProductDopingPricePerMonth").val();

        if (dopingDay != "0") {

            return true;
        }
        else {
            $("#errorMessageDay").html("Lütfen Doping Süresini Seçiniz*");
            $("#errorMessageDay").show();
            return false;
        }

    }
</script>
