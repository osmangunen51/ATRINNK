<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<RelCategoryModel>" %>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function check()
        {
            if ($('#Member_NewPasswordAgain').val() != $('#Member_NewPassword').val()) {
                alert("Yeni şifreler birbiriyle uyuşmuyor.");
                return false;
            }
        }
        $(document).ready(function () {

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
    Aktivasyon İçin İlgilendiğiniz Sektörden
                Kategori Seçiniz
         
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
                        Aktivasyon
                            </li>
                    </ol>
                </div>
            </div>
            <div class="well well-mt2">
              
                <%using (Html.BeginForm("Aktivasyon", "Uyelik", FormMethod.Post, new { id = "activation", @class = "form-horizontal" }))
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
                        Aktivasyon Kodu
                   
                    </label>
                    <div class="col-sm-6 pr pr0">
                        <%:Html.TextBoxFor(c => c.ActivationCode, new Dictionary<string, object> { { "class", "form-control" }, { "data-validation-engine", "validate[required]" }})%>
                        <%:Html.CheckBox("ReceiveEmail", true)%><span style="font-size: 12px;">&nbsp;&nbsp;Seçtiğim
                            sektörlerle ilgli gelişmeleri mail olarak almak istiyorum</span>
                    </div>
                    <div class="col-sm-3">
                        <button type="submit" class="btn btn-primary">
                            Onayla
                       
                        </button>
                    </div>
                </div>
                <div class="row">
                    <%--<div class="col-xs-6 col-md-5 col-lg-4">
                        <ul role="menubar" class="list-group list-group-mt2">
                            <% foreach (var item in Model.SectorItems)
                               {
                                   if (item == Model.SectorItems.First())
                                   { %>
                            <script type="text/javascript">
                                    loadCategoryById(<%:item.CategoryId %>,true)
                            </script>
                            <%} %>
                            <li class="<%:item == Model.SectorItems.First() ? "active" : string.Empty %> list-group-item">
                                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;&nbsp; <a style="cursor: pointer" data-rel="categorySelect"
                                    data-id="<%:item.CategoryId %>" data-toggle="tab">
                                    <%=item.CategoryName %>
                                </a></li>
                            <% } %>
                        </ul>
                    </div>--%>
                    <%--  <div data-rel="loading">
                        <img style="width: 50px" src="../../../../Content/V2/images/loading.gif" />
                        Yükleniyor. Lütfen Bekleyiniz
                    </div>--%>
                    <div class="tab-content col-xs-6 col-md-7 col-lg-8" data-rel="dataContent">
                    </div>
                </div>
                <% } %>
                  
            </div>
        </div>
        <% CompanyDemandMembership model1= new CompanyDemandMembership(); %>
        <%= Html.RenderHtmlPartial("LeftPanel",model1) %>
    </div>
</asp:Content>
