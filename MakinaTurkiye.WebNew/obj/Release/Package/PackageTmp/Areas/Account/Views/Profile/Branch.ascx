﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<StoreModel>" %>

<input type="hidden" id="hdnDealerType" value="<%:Request.QueryString["DealerType"] %>" />
<div class="col-md-7">
    <div class="form-horizontal" role="form">
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Şube Adı
            </label>
            <div class="col-sm-9">
                <div class="input-group">
                    <%= Html.TextBox("DealerNameForBranch",null, new { @class="form-control" })%>
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button" onclick="DealerAddForBranch();">
                            Ekle
                        </button>
                    </span>
                </div>
            </div>
        </div>
    </div>
    <hr />
    <div class="form-horizontal" role="form">
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Şube Seç
            </label>
            <div class="col-sm-7">
                <%:Html.DropDownList("StoreDealerId", Model.DealerItemsForBranch, 
                new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]" } })%>
            </div>
        </div>
        <div class="form-group">
            <%=Html.RenderHtmlPartial("Address") %>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-3 col-sm-9">
                <button type="submit" class="btn btn-primary" onclick="return checkForm();">
                    Değişiklikleri Kaydet
                </button>
            </div>
        </div>
    </div>
</div>
<div class="col-md-offset-1 col-md-4">
    <%=Html.RenderHtmlPartial("DealerAddressItems", Model.DealerAddressItems)%>
</div>


<script type="text/javascript">
    function DealerAddForBranch() {
        var dealerName = $('#DealerNameForBranch').val();
        if (dealerName != "") {
            $.ajax({
                url: '/Profile/DealerAddForBranch',
                type: 'post',
                dataType: 'json',
                data: { DealerNameForBranch: $('#DealerNameForBranch').val() },
                success: function (data) {
                    $('#DealerNameForBranch').val('');

                    var dealerItems = eval(data);

                    $("#StoreDealerId").empty();

                    $("#StoreDealerId").append("<option value=\"0\">" + "< Lütfen Seçiniz >" + "</option>");
                    for (var i = 0; i < dealerItems.length; i++) {
                        $("#StoreDealerId").append("<option value=\"" + dealerItems[i].StoreDealerId + "\">" + dealerItems[i].DealerName + "</option>");
                    }
                    alert('Şubeniz başarıyla eklenmiştir.');
                },
                error: function (x, l, e) {
                    alert(x.responseText);
                }
            });
        }
        else {
            alert('Lütfen Şube Adı Giriniz');
        }
    }

    function checkForm() {
        if ($('#StoreDealerId').val() == '0') {
            alert('Ekleyeceğiniz adresin şubesini seçin.');
            return false;
        }
        else {
            return true;
        }
    }
</script>