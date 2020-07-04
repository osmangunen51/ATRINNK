﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<RelCategoryModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function createPassCheck()
        {
            if ($('#Member_NewPasswordAgain').val() != $('#Member_NewPassword').val()) {
                alert("Yeni şifreler birbiriyle uyuşmuyor.");
                return false;
            }
        }
        $(document).ready(function () {
            $('#activation').validationEngine();
            var tabContent = $('[data-rel="tabcontent"]');
            var selectedSectorIDList = [];

            $('[data-rel="categorySelect"]').click(function () {
                var id = $(this).data('id');
                $('html, body').animate({ scrollTop: 150 }, 500);
                if (selectedSectorIDList[id] == undefined) {
                    selectedSectorIDList[id] = new Array();
                    $('[data-rel="tabcontent"]').each(function () {
                        $(this).removeClass("active");
                    });
                    loadCategoryById(id, true);
                } else {
                    $('[data-rel="tabcontent"]').each(function () {
                        $(this).removeClass("active");
                    });
                    $('#baa' + id + '').addClass('active');
                }
            });
        });

        //        function loadCategoryById(selectedCategoryID, status) {
        //            $('[data-rel="loading"]').show();

        //            $.ajax({
        //                url: '/Membership/GetCategories',
        //                data: { categoryID: Number(selectedCategoryID),
        //                    isActive: status
        //                },
        //                success: function (data) {
        //                    $('[data-rel="loading"]').hide();
        //                    $('[data-rel="dataContent"]').append(data);
        //                }, error: function (x) {
        //                    $('[data-rel="loading"]').hide();
        //                    $('[data-rel="dataContent"]').append('İstenmeyen Bir Hata Oluştu');
        //                }
        //            });
        //        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-xs-12">
            <h4 class="mt0">
                <span class="glyphicon glyphicon-user"></span>
        
                    Şifre Oluştur
             
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-7 col-md-8 pr">
            <div class="row hidden-xs">
                <div class="col-xs-12">
                    <ol class="breadcrumb breadcrumb-mt">
                        <li><a href="/">Anasayfa </a></li>
                        <li class="active"> 
        
                    Şifre Oluştur
               </li>
                    </ol>
                </div>
            </div>
            <div class="well well-mt2">
                     <%using (Html.BeginForm("sifreolustur", "uyelik", FormMethod.Post, new { id = "activation", @class = "form-horizontal" }))
                  {%>
                <%--      <div class="alert alert-info alert-dismissable">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                        ×
                    </button>
                    Aktivasyon kodu ile ilgilendiğiniz sektörleri seçtikten sonra üyeliğinizi aktif
                    hale getirebilirsiniz.
                </div>--%>
                <div class="form-group">
                    <label for="inputPassword3" class="col-sm-3 control-label">
                        Şifre
                   
                    </label>
               <%:Html.Hidden("activationCode",ViewData["activationCode"], new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[required]" }})%>
                 
                    <div class="col-sm-6 pr pr0">
                        <%:Html.Password("password", "",new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[required]" },{"id","Member_NewPassword"},{"placeholder","Min. 6 haneli şifrenizi oluşturun"},{"maxlength","6"}})%>
                    </div>
                
                </div>
                   <div class="form-group">
                    <label for="inputPassword3" class="col-sm-3 control-label">
                        Şifre Tekrar
                   
                    </label>
                    <div class="col-sm-6 pr pr0">
                        <%:Html.Password("passwordAgain","", new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[required]" },{"id","Member_NewPasswordAgain"},{"placeholder","Şifrenizi tekrar giriniz"},{"maxlength","6"}})%>
                    </div>
                
                </div>
                <div class="form-group">
                    <div class="col-sm-8"></div>
                        <div class="col-sm-3">
                        <button type="submit" onclick="return createPassCheck();" class="btn btn-primary">
                          Oluştur ve Devam Et
                        </button>
                    </div>
                </div>
              
                <% } 
                 %>
                
            </div>
        </div>
        <% CompanyDemandMembership model1= new CompanyDemandMembership(); %>
        <%= Html.RenderHtmlPartial("LeftPanel",model1) %>
    </div>
</asp:Content>
