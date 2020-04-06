﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<PacketModel>" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <style type="text/css">
        #AddressDisplayWrapper:hover{
            background-color:#e1e1e1;
        }
      #TaxOfficeDisplayWrapper:hover{
            background-color:#e1e1e1;
        }
        #TaxNumberDisplayWrapper:hover{
            background-color:#e1e1e1;
        }
    </style>
    
    <script type="text/javascript">

        function ThreeStepCheck() {

            var AddressText=$.trim($("#AddressText").val());
            var TaxOfficeText=$.trim($("#TaxOfficeText").val());
            var TaxNumberText=$.trim($("#TaxNumberText").val());
            
            if (AddressText == "" || TaxOfficeText == "" || TaxNumberText == "")
            {
                if(AddressText=="")
                {
                    alert("Adres  boş geçilemez.");
                }
               else if (TaxNumberText == "") {
                    alert("Vergi Numarası boş geçilemez.");
                }
               else if (TaxOfficeText == "") {
                    alert("Vergi Dairesi  boş geçilemez.");
                }
                return false;
            }
            else{
            if ($('#rdBankTransfer').attr('checked') == 'checked') {
                if ($('#AccountId').val() == '') {
                    alert('Bir sonraki adıma geçmek için banka hesabı seçmelisiniz.');
                    return false;
                }
                else {
                    return true;
                }
            }
            else {

                if ($('#rdTekCekim').attr('checked') == 'checked') {
                    if ($('#CreditCardInstallmentId').val() == '') {
                        alert('Bir sonraki adıma geçmek için kredi kartı tipi seçmelisiniz.');
                        return false;
                    }
                    return true;
                }
                else if ($('#rdTaksit').attr('checked') == 'checked') {
                    if ($('#CreditCardInstallmentId').val() == '') {
                        alert('Bir sonraki adıma geçmek için kredi kartlarına ait taksitlerden birini seçmeniz gerekmektedir.');
                        return false;
                    }
                    return true;
                }
                else {
                    alert('Bir sonraki adıma geçmek ödeme seçeneği seçmelisiniz.');
                    return false;
                }
            }
            }
        }

        $(document).ready(function () {
        
            $("a[href='#divBankTransfer']").click(function () {
                $('#rdBankTransfer').attr('checked', true);
                $('#OrderType').val($('#hdnHavale').val());
            });
            $("a[href='#kredikarti']").click(function () {
                $('#rdBankTransfer').attr('checked', false)
                $('#rdTekCekim').attr('checked', true);
                $('#OrderType').val($('#hdnKrediKarti').val());
                $('#CreditCardInstallmentId').val(0);
                $('#CreditCardId').val(14);
            });
            $("a[href='#tekcekim']").on('click', function () {
                $('#rdTekCekim').attr('checked', true);
                $('#OrderType').val($('#hdnKrediKarti').val());
                $('#CreditCardInstallmentId').val(0);
                $('#CreditCardId').val(14);
            });
            $(".taksit").on('click', function () {
                $('#rdTaksit').attr('checked', true);
                $('#OrderType').val($('#hdnKrediKarti').val());
            });

            $('#rdBankTransfer').change(function () {
                if ($(this).attr('checked') == true) {
                    $('#divBankTransfer').show();
                    $('#divCreditCard').hide();

                    $('#OrderType').val($('#hdnHavale').val());
                }
            });

            $('#rdCreditCard').change(function () {
                if ($(this).attr('checked') == true) {
                    $('#divCreditCard').show();
                    $('#divBankTransfer').hide();

                    $('#OrderType').val($('#hdnKrediKarti').val());
                }
            });

            $('#rdTekCekim').change(function () {
                if ($(this).attr('checked') == true) {
                    $('#CreditCardInstallmentId').val(0);
                    $('#CreditCardId').val(14);
                    //          window.location.href = '/UyelikSatis/4.Adim';
                    //$('#divTekCekim').show();
                    $('#divTaksit').hide();
                }
            });

            $('#rdTaksit').change(function () {
                if ($(this).attr('checked') == true) {
                    $('#divTaksit').show();
                    $('#divTekCekim').hide();
                }
            });
            $("#TaxOfficeUpdateClick").click(function () {
             
                if ($.trim($("#TaxOfficeText").val()) != "")
                {
                    $("#TaxOfficeDisplayWrapper").hide();
                    $("#TaxOfficeTextWrapper").show();

                }
            });
            $("#TaxOfficeSave").click(function () {
                    if ($.trim($("#TaxOfficeText").val()) != "") {
                        $("#TaxOfficeDisplay").html($.trim($("#TaxOfficeText").val()));
                        $("#TaxOfficeTextWrapper").hide();
                        $("#TaxOfficeDisplayWrapper").show();
                      
                    }
                    
                
             
            });
            $("#TaxOfficeCancel").click(function () {
                if ($.trim($("#TaxOfficeText").val()) != "") {
                    $("#TaxOfficeTextWrapper").hide();
                    $("#TaxOfficeDisplayWrapper").show();

                }
            });
            $("#TaxNumberUpdateClick").click(function () {
                if ($.trim($("#TaxNumberText").val()) != "") {
                    $("#TaxNumberDisplayWrapper").hide();
                    $("#TaxNumberTextWrapper").show();

                }
            });
            $("#TaxNumberSave").click(function () {
                if ($.trim($("#TaxNumberText").val()) != "") {
                    $("#TaxNumberDisplay").html($.trim($("#TaxNumberText").val()));
                    $("#TaxNumberTextWrapper").hide();
                    $("#TaxNumberDisplayWrapper").show();

                }



            });
            $("#TaxNumberCancel").click(function () {
                if ($.trim($("#TaxNumberText").val()) != "") {
                    $("#TaxNumberTextWrapper").hide();
                    $("#TaxNumberDisplayWrapper").show();

                }
            });
            $("#AddressUpdateClick").click(function () {
                if ($.trim($("#AddressText").val()) != "") {
                    $("#AddressDisplayWrapper").hide();
                    $("#AddressTextWrapper").show();

                }
            });
            $("#AddressSave").click(function () {
                if ($.trim($("#AddressText").val()) != "") {
                    $("#AddressDisplay").html($.trim($("#AddressText").val()));
                    $("#AddressTextWrapper").hide();
                    $("#AddressDisplayWrapper").show();

                }



            });
            $("#AddressCancel").click(function () {
                if ($.trim($("#AddressText").val()) != "") {
                    $("#AddressTextWrapper").hide();
                    $("#AddressDisplayWrapper").show();

                }
            });

        });
        

  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input type="hidden" id="hdnKrediKarti" value="<%:(byte)Ordertype.KrediKarti %>" />
    <input type="hidden" id="hdnHavale" value="<%:(byte)Ordertype.Havale %>" />
    <%using (Html.BeginForm("ThreeStep", "MembershipSales",FormMethod.Post))
      {%>
    <div>
        <div>
            <%=Html.RenderHtmlPartial("_TaxAndAddressForm",Model.TaxAndAddressViewModel)%> 
            <hr />
            <div class="row">
                <div class="col-sm-12">
                    <div class="btn-group">
                        <span class="btn btn-md btn-mt2 disabled">Ödeme Tipi </span><a href="#divBankTransfer"
                            data-toggle="tab" class="btn btn-md btn-mt2 active">Havale / EFT </a><a href="#kredikarti"
                                data-toggle="tab" class="btn btn-md btn-mt2">Kredi Kartı </a>
                    </div>
                    <hr>
                    <h5>
                        <span class="glyphicon glyphicon-tag"></span>&nbsp; Sipariş Bilgileri
                    </h5>
                    <table class="table table-striped">
                        <tbody>
                            <tr>
                                <td>
                                    Sipariş No
                                </td>
                                <td>
                                    Paket Tipi
                                </td>
                                <td>
                                    Tutar
                                </td>
                                <td>
                                    <b>KDV Dahil Toplam Tutar </b>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%:Model.OrderNo %>
                                </td>
                                <td>
                                    <%:Model.PacketName %>
                                </td>
                                <td>
                                    <%:Model.OrderPrice.ToString("C2") %>
                                </td>
                                <td>
                                    <b>
                                        <%:Model.MaturityCalculation(Model.OrderPrice, 0).ToString("C2")%>
                                    </b>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <hr>
                    <div class="tab-content">
                        <div class="tab-pane active" id="divBankTransfer">
                            <%=Html.RenderHtmlPartial("BankTransfer")%>
                                  <button type="submit" onclick="return ThreeStepCheck();" class="pull-right btn btn-primary">
                        Tamamla >>
                    </button>
                        </div>
                        <div class="tab-pane" id="kredikarti">
                           
                               <button type="submit" onclick="return ThreeStepCheck();" class="pull-right btn btn-primary">
                        Sonraki Adım >>
                    </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" name="hasRecord" value="true" />
    <input type="hidden" id="OrderType" name="OrderType" value="<%:(byte)Ordertype.Havale %>" />
    <input type="hidden" id="CreditCardInstallmentId" name="CreditCardInstallmentId" />
    <input type="hidden" id="CreditCardId" name="CreditCardId" value="" />
    <input type="hidden" name="AccountId" id="AccountId" />
    <input type="hidden" name="rdPaymentOption" id="rdTekCekim" />
    <input type="hidden" name="rdPaymentOption" id="rdTaksit" />
    <input type="hidden" checked="checked" name="rdOrderType" id="rdBankTransfer" />
    <% } %>
</asp:Content>
