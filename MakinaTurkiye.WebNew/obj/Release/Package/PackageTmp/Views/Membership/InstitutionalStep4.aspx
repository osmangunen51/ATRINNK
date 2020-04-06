<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<MembershipViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
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

        function loadCategoryById(selectedCategoryID, status) {
            $('[data-rel="loading"]').show();

            $.ajax({
                url: '/Membership/GetCategories',
                data: { categoryID: Number(selectedCategoryID),
                    isActive: status
                },
                success: function (data) {
                    $('[data-rel="loading"]').hide();
                    $('[data-rel="dataContent"]').append(data);
                }, error: function (x) {
                    $('[data-rel="loading"]').hide();
                    $('[data-rel="dataContent"]').append('İstenmeyen Bir Hata Oluştu');
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-xs-12">
            <h4 class="mt0">
                <span class="glyphicon glyphicon-user"></span>Faaliyet Alanı Seçiniz
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-7 col-md-8 pr">
            <div class="row hidden-xs">
                <div class="col-xs-12">
                    <ol class="breadcrumb breadcrumb-mt">
                        <li><a href="/">Anasayfa </a></li>
                        <li class="active">Kurumsal Üyelik 4. Adım </li>
                    </ol>
                </div>
            </div>
            <div class="well well-mt4">
                <%using (Html.BeginForm("KurumsalUyelik/Adim-4", "Uyelik", FormMethod.Post, new { id = "formInstitutionalStep4", @class = "form-horizontal" }))
                  {%>
                <div class="row">
                    <div class="col-xs-6 col-md-5 col-lg-4">
                        <ul role="menubar" class="list-group list-group-mt2">
                            <% foreach (var item in Model.CategoryItems)
                               {
                                   if (item == Model.CategoryItems.First())
                                   { %>
                            <script type="text/javascript">
                                loadCategoryById(<%:item.CategoryId %>,true)
                            </script>
                            <%} %>

                            <li class="<%:item == Model.CategoryItems.First() ? "active" : string.Empty %> list-group-item">
                                <i class="fa fa-angle-right"></i>&nbsp;&nbsp;&nbsp; <a style="cursor: pointer" data-rel="categorySelect" data-id="<%:item.CategoryId %>"
                                    data-toggle="tab">
                                    <%=item.CategoryName %>
                                </a></li>
                            <% } %>
                        </ul>
                    </div>
                    <div data-rel="loading">
                        <img style="width: 50px" src="../../../../Content/V2/images/loading.gif" />
                        Yükleniyor.   Lütfen Bekleyiniz
                    </div>
                    <div class="tab-content col-xs-6 col-md-7 col-lg-8" data-rel="dataContent">
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-9">
                        <a href="Account/MemberType/InstitutionalStep3">
                            <button class="btn btn-primary" type="button">
                                Önceki Adım
                           
                            </button>
                        </a>
                        <button class="btn btn-success" data-rel="form-submit" type="submit">
                            Sonraki Adım
                       
                        </button>
                    </div>
                </div>
                <% } %>
            </div>
        </div>
        <%CompanyDemandMembership model1 = new CompanyDemandMembership(); %>
        <%= Html.RenderHtmlPartial("LeftPanel",model1) %>
    </div>
</asp:Content>
