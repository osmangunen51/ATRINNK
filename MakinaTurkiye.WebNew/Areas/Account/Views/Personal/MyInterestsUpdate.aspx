<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.Account.MyInterestsUpdateModel>" %>


<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        var selectedSectorIDList = [];
        $(document).ready(function () {
            var tabContent = $('[data-rel="tabcontent"]');

            $('[data-rel="categorySelect"]').click(function () {
                var id = $(this).data('id');
                $('html, body').animate({ scrollTop: 150 }, 500);
                if (selectedSectorIDList[id] == undefined) {
                    selectedSectorIDList[id] = new Array();
                    isActiveTab(id);
                    loadCategoryById(id, true);
                } else {
                    isActiveTab(id);
                }
            });
        });

        function isActiveTab(id) {
            $('[data-rel="tabcontent"]').each(function () {
                $(this).removeClass("active");
            });
            $('#baa' + id + '').addClass('active');
        }

        function loadCategoryById(selectedCategoryID, status) {
            $('[data-rel="loading"]').show();
            $.ajax({
                url: 'GetCategories',
                data: {
                    categoryID: Number(selectedCategoryID),
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

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>&nbsp;İlgi Alanlarımı Güncelle
            </h4>
        </div>
    </div>
    <div class="row">

        <div class="col-sm-12 col-md-12">
            <div>

                <div class="well store-panel-container">
                    <%using (Html.BeginForm("MyInterestsUpdate", "Personal", FormMethod.Post, new { @class = "form-horizontal", role = "form" }))
                        {%>
                    <div class="row">
                        <div class="col-xs-6 col-md-5 col-lg-4">
                            <ul role="menubar" class="list-group list-group-mt2">
                                <% foreach (var item in Model.CategoryList)
                                    { %>
                                <% 
                                    if (item == Model.CategoryList.First())
                                    { %>
                                <script>
                                    loadCategoryById(<%:item.CategoryId %>, true)
                                </script>
                                <%}
                                    bool isSelected = Model.SelectedMainCategoryList.Any(c => c.CategoryId == item.CategoryId);
                                    if (isSelected)
                                    { %>
                                <script>
                                    loadCategoryById(<%:item.CategoryId %>, false)
                                </script>
                                <% }
                                %>
                                <li class="<%:item == Model.CategoryList.First() ? "active" : string.Empty %> list-group-item">
                                    <i class="fa fa-angle-right"></i>&nbsp;&nbsp;&nbsp; <a data-rel="categorySelect"
                                        class="<%= (isSelected) ? "text-bold" : string.Empty %>" data-id="<%:item.CategoryId %>"
                                        data-toggle="tab">
                                        <%=item.CategoryName %>
                                    </a></li>
                                <% } %>
                            </ul>
                        </div>
                        <div style="width: 50px" data-rel="loading">
                            <img src="../../../../Content/V2/images/loading.gif" />
                            Yükleniyor. Lütfen Bekleyiniz
                        </div>
                        <div class="tab-content col-xs-6 col-md-7 col-lg-8" data-rel="dataContent">
                        </div>
                    </div>
                    <div class="form-group">
                        <div class=" col-sm-12">
                            <label>
                                <%=Html.CheckBoxFor(m => m.ReceiveEmail)%>
                                Seçtiğim sekktörlerle ilgili gelişmeleri mail olarak almak istiyorum.
                            </label>
                            <br />
                            <button type="submit" class="btn btn-primary">
                                Değişiklikleri Kaydet
                            </button>
                        </div>
                    </div>
                    <%} %>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
