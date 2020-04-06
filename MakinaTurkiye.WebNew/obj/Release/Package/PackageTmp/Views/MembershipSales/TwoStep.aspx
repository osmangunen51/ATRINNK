﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<PacketModel>" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function twoStepCheck() {


            if ($('#chkNewAddress').attr('checked') == false) {
                if ($('#TaxOffice').val() == '' || $('#TaxNo').val() == '') {
                    alert('Bir sonraki adıma geçmek vergi dairesi ve vergi numarası alanlarını eksiksiz girmelisiniz.');
                    return false;
                }
                else if ($('#Address').val() == '') {
                    alert('Yeni fatura adresini doldurmalısınız.');
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                if ($('#TaxOffice').val() == '' || $('#TaxNo').val() == '') {
                    alert('Bir sonraki adıma geçmek için vergi dairesi ve vergi numarası alanlarını eksiksiz girmelisiniz.');
                    return false;
                }
                else {
                    return true;
                }
            }
        }

        $(document).ready(function () {
            $('#chkNewAddress').change(function () {
                if ($(this).is(":checked")) {
                    $('#Address').attr('disabled', 'disabled');
                    $('#Address').css('background-color', '#ccc');
                }
                else {
                    $('#Address').removeAttr('disabled');
                    $('#Address').css('background-color', '#fff');
                }
//                if ($(this).attr('checked') == true) {
//                    $('#Address').attr('disabled', 'disabled');
//                    $('#Address').css('background-color', '#ccc');
//                }
//                else {
//                    $('#Address').removeAttr('disabled');
//                    $('#Address').css('background-color', '#fff');
//                }
            });
        });
    


  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div class="row">
            <%using (Html.BeginForm("TwoStep", "MembershipSales", FormMethod.Post, new { @class = "form-horizontal" }))
              {%>
            <div class="col-sm-6">
                <h4>
                    Fatura Bilgileri
                </h4>
                <hr>
                <div class="form-group">
                    <label class="col-sm-3 control-label">
                        Firma Adı
                    </label>
                    <div class="col-sm-6">
                        <p class="form-control-static">
                            <%:Model.StoreName %>
                        </p>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label">
                        Vergi Dairesi
                    </label>
                    <div class="col-sm-6">
                        <input type="text" class="form-control" name="TaxOffice" id="TaxOffice" placeholder="Vergi Dairesi">
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label">
                        Vergi No
                    </label>
                    <div class="col-sm-6">
                        <input type="text" class="form-control" name="TaxNo" id="TaxNo" placeholder="Vergi No">
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-3 control-label">
                        Adres
                    </label>
                    <div class="col-sm-6">
                        <p class="form-control-static">
                            <%:Model.Address %>
                        </p>
                        <div class="checkbox">
                            <label>
                                <%:Html.CheckBoxFor(c => c.NewAddress, new { id = "chkNewAddress" })%>
                                Fatura adresi olarak kullan
                            </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <h4>
                    Fatura Adresi
                </h4>
                <hr>
                <div class="form-group">
                    <label class="col-sm-3 control-label">
                        Adres
                    </label>
                    <div class="col-sm-6">
                        <p class="help-block">
                            Fatura adresinizde değişiklik yapmak istiyorsanız lütfen yeni fatura adresi yazınız.
                        </p>
                        <textarea class="form-control" name="Address" id="Address" rows="3"></textarea>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-6">
                        <button type="submit" onclick="return twoStepCheck();" class="pull-right btn btn-primary">
                            Sonraki Adım >>
                        </button>
                    </div>
                </div>
            </div>
            <% } %>
        </div>
    </div>
</asp:Content>--%>
