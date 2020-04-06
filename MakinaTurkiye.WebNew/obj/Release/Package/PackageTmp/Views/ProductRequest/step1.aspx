﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.ProductRequests.MTProductRequestModel>" %>

<asp:Content ID="Content5" ContentPlaceHolderID="HeaderContent" runat="server">

        <script type="text/javascript" src="/Content/v2/assets/js/phonemask.js"></script>

    <script type="text/javascript">
        var currentTab = 0; // Current tab is set to be the first tab (0)
        $(document).ready(function () {

            showTab(currentTab); // Display the current tab
            $('#sector').change(function () {
                var CategoryId = this.value;
                FillCategory(CategoryId, "productGroup", "productGorupLoading");
  
                $("#category").html("");
                $("#category").hide();


            })
            $("#productGroup").change(function () {
                var CategoryId = this.value;
                FillCategory(CategoryId, "category", "categoryLoading");

            })

        });

        function FillCategory(CategoryId, divName, loadingId) {
            $("#" + loadingId).show();
            $.ajax({
                url: '/Category/GetSubCategory',
                data: { categoryId: CategoryId },
                type: 'post',
                success: function (data) {
                    $("#" + divName).html("");
                    if (data.length > 0) {
                        $("#" + divName).append("<option value='0' selected >Tümü</option>");
                        $.each(data, function (key, value) {

                            $("#" + divName).show();
                            $("#" + divName).append("<option value='" + value.Value + "'>" + value.Name + "</option>");
                        });
                    }
                    $("#" + loadingId).hide();
                },
                error: function (x, l, e) {
                }
            });
        }



        function showTab(n) {
            // This function will display the specified tab of the form ...
            var x = document.getElementsByClassName("tab");
            x[n].style.display = "block";
            // ... and fix the Previous/Next buttons:
            if (n == 0) {
                document.getElementById("prevBtn").style.display = "none";
            } else {
                document.getElementById("prevBtn").style.display = "inline";
            }
            if (n == (x.length - 1)) {
                document.getElementById("nextBtn").innerHTML = "Kaydet";
            } else {
                document.getElementById("nextBtn").innerHTML = "İleri";
            }
            // ... and run a function that displays the correct step indicator:
            fixStepIndicator(n)
        }

        function nextPrev(n) {
            // This function will figure out which tab to display
            var x = document.getElementsByClassName("tab");
            // Exit the function if any field in the current tab is invalid:
            if (n == 1 && !validateForm()) return false;
            // Hide the current tab:
            x[currentTab].style.display = "none";
            // Increase or decrease the current tab by 1:
            currentTab = currentTab + n;
            // if you have reached the end of the form... :
            if (currentTab >= x.length) {
                //...the form gets submitted:

                document.getElementById("regForm").submit();
                return false;
            }
            // Otherwise, display the correct tab:
            showTab(currentTab);
        }

        function validateForm() {
            // This function deals with validation of the form fields
            var x, y, i, valid = true;
            x = document.getElementsByClassName("tab");
            y = x[currentTab].getElementsByTagName("input");
            var sector = $("#sector").val();
            
            if (sector == null) {
                valid = false;
                alert("Lütfen kategori seçiniz");
                return false;
            }
            // A loop that checks every input field in the current tab:
            for (i = 0; i < y.length; i++) {
                // If a field is empty...
                if (y[i].value == "") {
                    // add an "invalid" class to the field:
                    y[i].className += " invalid";
                    // and set the current valid status to false:
                    valid = false;
                }
            }
            // If the valid status is true, mark the step as finished and valid:
            if (valid) {
                document.getElementsByClassName("step")[currentTab].className += " finish";
            }
            return valid; // return the valid status
        }

        function fixStepIndicator(n) {
            // This function removes the "active" class of all steps...
            var i, x = document.getElementsByClassName("step");
            for (i = 0; i < x.length; i++) {
                x[i].className = x[i].className.replace(" active", "");
            }
            //... and adds the "active" class to the current step:
            x[n].className += " active";
        }

        $(document).ready(function () {
            // GetSimilarProductAjax();

            $("#PhoneNumber").mask("(9999) 999-9999");



        });

    </script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <div class="fast-access-bar hidden-xs">
        <div class="fast-access-bar__inner">
            <div class="row clearfix">
                <div class="col-xs-12 col-md-6">
                    <ol class="breadcrumb breadcrumb-mt">
                        <li class="active">Ürün Talebi</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <div class="row clearfix">
        <div class="col-md-6">

            <div class="request-container ">
                <div class="row">
                <h3>Nasıl Çalışır?</h3>
                <ul class="request-info-list">
                    <li>
                        <div class="request-info-t">1</div>
                        <div class="request-info-i"><i class="fa fa-plus"></i></div>
                        <div class="request-info-c">Satın almak istediğiniz ürün talebinde bulunun</div>
                    </li>
                    <li>

                        <div class="request-info-t">2</div>
                        <div class="request-info-i"><i class="fa fa-share"></i></div>
                        <div class="request-info-c">Talebiniz ilgili makina üreticilerine ulaşsın</div>
                    </li>
                    <li>

                        <div class="request-info-t">3</div>
                        <div class="request-info-i"><i class="fa fa-undo"></i></div>
                        <div class="request-info-c">Gelen tekliflerden size uygun ürüne karar verin</div>
                    </li>
                </ul>
                    </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="request-container">
                <%using (Html.BeginForm("Step1", "ProductRequest", FormMethod.Post, new { @id = "regForm" }))
                    {
                %>
                <h3>Alım Talebi Oluştur</h3>
                <%if (TempData["success"] != null)
                    {%>
                <div id="CommentedAlert" class="alert alert-success">
                    Talebiniz makina üreticilerine ulaştırılacakatır.

                </div>
                <% } %>
                <div class="form-horizontal row">
                    <div class="tab">
                        <div class="col-xs-12 scrollCategory">
                            <div class="form-group">
                                <label class="col-md-3">Kategori</label>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">

                                    <select class="form-control mt-form-control" name="MTProductRequestForm.SectorId" id="sector" size="10">
                                        <%foreach (var item in Model.SectorList)
                                            {%>
                                        <option value="<%:item.CategoryId %>"><%:item.CategoryContentTitle%></option>
                                        <%} %>
                                    </select>
                                    <img src="/Content/V2/images/loading.gif" style="width: 30px; display: none;" id="productGorupLoading" />
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <select class="form-control mt-form-control" id="productGroup" name="MTProductRequestForm.ProductGroupId" size="10" style="display: none;">
                                    </select>
                                    <img src="/Content/V2/images/loading.gif" style="width: 30px; display: none;" id="categoryLoading" />
                                </div>
                            </div>
                            <div class="col-md-4 col-sm-4">
                                <div class="form-group">
                                    <select class="form-control" name="MTProductRequestForm.CategoryId" id="category" style="display: none;" size="10">
                                    </select>
                                    <img src="/Content/V2/images/loading.gif" style="width: 30px; display: none;" id="brandLoading" />
                                </div>
                            </div>
                        </div>
         
                    </div>
                    <div class="tab">
                        <div class="col-md-12">
                            
                            <div class="form-group">
                                <label class="col-md-3">
                                    Telefon
                                </label>
                                <div class="col-md-9">
                                    <%:Html.TextBoxFor(x=>x.MTProductRequestForm.PhoneNumber,new {@class="form-control mt-form-control",@id="PhoneNumber" }) %>
                               <small>Örn:0537 222 2222</small>
                                    </div>

                            </div>

                            <div class="form-group">
                                <label class="col-md-3">Açıklama</label>
                                <div class="col-md-9">
                                    <%:Html.TextAreaFor(x=>x.MTProductRequestForm.Message,new {@class="form-control mt-form-control", @style="height:100px;", @id="Message",@placeholder="Eklemek istediğiniz mesaj.."}) %>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-4 pull-right">
                        <div class="pull-right">
                            <button type="button" id="prevBtn" class="btn background-mt-btn" style="background-color: rgba(236, 128, 55, 0.63)" onclick="nextPrev(-1)"><< Geri</button>
                            <button type="button" id="nextBtn" class="btn background-mt-btn" onclick="nextPrev(1)">Devam et>></button>
                        </div>
                    </div>
                    <div class="clearfix"></div>
                    <div style="text-align: center; margin-top: 40px;">
                        <span class="step"></span>
                        <span class="step"></span>
                    </div>
                </div>
                <%} %>
            </div>
        </div>
    </div>

</asp:Content>
