<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores.StoresViewModel.MTContactSettingsModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#StartTime').mask('00:00');
            $('#EndTime').mask('00:00');
            $("#AvaliableAlways").change(function () {
                if (this.checked) {
                    //Do stuff
                    $("#SaturdayWorking").prop("checked", true);
                    $("#SundayWorking").prop("checked", true);
                    $("#SundayWorking").prop("disabled", true);
                    $("#SaturdayWorking").prop("disabled", true);

                    $("#StartTime").prop("disabled", true);
                    $("#EndTime").prop("disabled", true);
                }
                else {
                    $("#StartTime").prop("disabled", false);
                    $("#EndTime").prop("disabled", false);
                    $("#SundayWorking").prop("disabled", false);
                    $("#SaturdayWorking").prop("disabled", false);
                }
            });
        });
        function GetContactSettingInfo(id) {
            $.ajax({
                url: '/Account/Store/GetPhoneSetting',
                type: 'GET',
                data: { "phoneId": id },
                dataType: 'json',
                success: function (data) {
                    if (data.IsSuccess) {
                        $("#PhoneId").val(data.Result.PhoneId);

                        $("#PhoneTypeDisplay").html(data.Result.PhoneTypeText);
                        $("#PhoneNumberDisplay").html(data.Result.PhoneNumber);
                        if (data.Result.StartTime) {
                            $("#StartTime").val(data.Result.StartTime);
                        }
                        if (data.Result.EndTime) {
                            $("#EndTime").val(data.Result.EndTime);
                        }
                        $("#SaturdayWorking").prop("checked", data.Result.SaturdayWorking);
                        $("#SundayWorking").prop("checked", data.Result.SundayWorking);
                        $("#StoreMainPartyId").val(data.Result.StoreMainPartyId);
                    }
                    else {
                        alert(data.Message);
                    }
                },
                error: function (request, error) {
                    alert("Error: " + JSON.stringify(request));
                }
            });
        }
        function ChangePhoneSetting() {
            $("#loaderDiv").show();
            var startTime = $("#StartTime").val();
            var endTime = $("#EndTime").val();

            var eTimes = endTime.split(":");
            var sTimes = startTime.split(":");

            if (sTimes[0] < 24 && sTimes[0] >= 0 && sTimes[1] >= 0 && sTimes[1] < 61) {

                if (eTimes[0] < 24 && eTimes[0] >= 0 && eTimes[1] >= 0 && eTimes[1] < 61) {
                    $.ajax({
                        url: '/Account/Store/ChangePhoneSettings',
                        type: 'POST',
                        data: {
                            "StoreMainPartyId": $("#StoreMainPartyId").val(),
                            "EndTime": $("#EndTime").val(),
                            "StartTime": $("#StartTime").val(),
                            "PhoneId": $("#PhoneId").val(),
                            "AvaliableAlways": $("#AvaliableAlways").prop("checked"),
                            "SaturdayWorking": $("#SaturdayWorking").prop("checked"),
                            "SundayWorking": $("#SundayWorking").prop("checked")

                        },
                        dataType: 'json',
                        success: function (data) {
                            if (data.IsSuccess) {
                                window.location = "/Account/Store/ContactSettings";

                            }
                            else {
                                $("#loaderDiv").hide();
                                alert(data.Message);
                            }
                        },
                        error: function (request, error) {
                            alert("Error: " + JSON.stringify(request));
                        }
                    });

                }
                else {
                    $("#errorMessageEndTime").show();
                    $("#errorMessageEndTime").html("Lütfen saati doğru formatta giriniz");
                    $("#loaderDiv").hide();

                }
            }
            else {
                $("#errorMessageStartTime").show();
                $("#errorMessageStartTime").html("Lütfen saati doğru formatta giriniz");
                $("#loaderDiv").hide();

            }

        }
    </script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>İletişim Ayarları
            </h4>
        </div>
    </div>
    <div class="row">
    <div class="col-sm-12 col-md-12">
        <div class="well store-panel-container col-xs-12" style="background: #fff;">
            <div class="col-md-12">
                <table class="table table-hover">
                    <tbody>
                        <%foreach (var item in Model.MTSettingItems)
                            {%>
                        <tr>
                            <td><%:item.PhoneNumber %>
           
                            </td>
                            <td><%:item.PhoneTypeText %></td>
                            <td>

                                <%if (string.IsNullOrEmpty(item.StartTime))
                                    { %>
                                <b style="color: #13960e">Her saat ulaşılabilir</b>
                                <%}
                                    else
                                    { %>
                                <b>
                                    <%:item.StartTime %> - <%:item.EndTime %>
                                    <%if (item.SaturdayWorking)
                                        {%>
                                        Cumartesi
                                    <%} %>
                                    <%if (item.SundayWorking)
                                        { %>
                                        - Pazar
                                    <%}%>
                                     dahil
                                </b>
                                <%} %>
                        
                            </td>
                            <td><a data-toggle="modal" style="cursor: pointer;" onclick="GetContactSettingInfo(<%:item.PhoneId %>)" data-target="#UpdateSetting">Düzenle<i class="fa fa-pencil"></i></a></td>
                        </tr>
                        <%} %>
                    </tbody>
                </table>

                <div class="alert alert-info" role="alert">
                    Firmanızın iletişim ayarlarını güncelleyebilir istediğiniz saatlerde rahatsız etme moduna alabilirsiniz.
                      <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                </div>
            </div>
        </div>
    </div>
        </div>
    <div class="modal fade" id="UpdateSetting" tabindex="-1" role="dialog" aria-labelledby="UpdateSetting" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered" role="document" id="modelContent">
            <div class="modal-content">
                <div class="modal-header" style="height: 60px; border: 0px!important;">
                    <h3 class="modal-title" style="float: left;" id="exampleModalLabel">Çalışma Saatleri</h3>
                    <button type="button" style="float: right;" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="alert alert-info">
                        <i class="fa fa-3x fa-bullhorn pull-left"></i>
                        Çalışma saatleri size ulaşılacak saatleri belirtir. 
                        Eğer siz 09:00-19:00 arasında belirtirseniz o saatler dışında telefon numaralarınız portalımızda gözükmez.
                    </div>
                    <input type="hidden" id="PhoneId" />
                    <input type="hidden" id="StoreMainPartyId" />
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-md-3">Telefon Numarası</label>
                            <div class="col-md-9" id="PhoneNumberDisplay"></div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 contorl-label">Telefon Tipi:</label>
                            <div class="col-md-9">
                                <b id="PhoneTypeDisplay"></b>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 contorl-label">Başlangıç Saati:</label>
                            <div class="col-md-9">
                                <input class="mt-form-control" value="09:00" id="StartTime" />
                            </div>

                            <div class="col-md-3"></div>
                            <div id="errorMessageStartTime" class="col-md-9" style="color: #b40505; font-size: 12px; display: none;">
                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-md-3 contorl-label">Bitiş Saati:</label>
                            <div class="col-md-9">
                                <input class="mt-form-control" value="19:00" id="EndTime" />
                            </div>

                            <div class="col-md-3"></div>
                            <div id="errorMessageEndTime" class="col-md-9" style="color: #b40505; font-size: 12px; display: none;">
                            </div>

                        </div>

                        <div class="form-group">
                            <label class="col-md-3 contorl-label"></label>
                            <div class="col-md-4">
                                <input type="checkbox" name="AvaliableAlways" id="SaturdayWorking" />
                                Cumartesi dahil et
                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-md-3"></label>
                            <div class="col-md-4">
                                <input type="checkbox" name="AvaliableAlways" id="SundayWorking" />
                                Pazar dahil et
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 contorl-label"></label>
                            <div class="col-md-9">
                                <input type="checkbox" name="AvaliableAlways" id="AvaliableAlways" />
                                Her zaman ulaşılabilir
                            </div>
                        </div>
                        <div class="form-group">

                            <div style="float: right;">
                                <div id="loaderDiv" style="display: none; width: auto; height: 20px; font-size: 12px; margin-top: 8px; float: left;">
                                    <img src="/Content/images/load.gif" alt="">&nbsp; Kaydediliyor, lütfen bekleyiniz.
                                </div>
                                <button type="submit" id="buttonPay" onclick="ChangePhoneSetting()" style="float: right;" class="btn background-mt-btn ">Kaydet <span class="glyphicon glyphicon-chevron-right"></span></button>

                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
