<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert.MTProductItem>>" %>

                <table class="table table-hover table=responsive table-condensed">
                    <thead class="thead-light">
                        <tr>

                            <th scope="col">Ürün Numarası</th>
                            <th scope="col">Ürün Adı</th>

                            <th scope="col">Kategori</th>
                            <th scope="col">Marka</th>
                            <th scope="col">Sıra</th>
                            <th scope="col">Araçlar</th>
                        </tr>
                        <tr>
                            <td>
                                <span class="clearable">
                                    <input class="searchText  form-control" id="ProductNo" type="text" value="#" />
                                    <i class="clearable__clear">&times;</i>
                                </span></td>
                            <td>
                                <span class="clearable">
                                    <input class="searchText form-control" type="text" id="ProductName" placeholder="Ürün Adı" />
                                    <i class="clearable__clear">&times;</i>
                                </span></td>

                            <td>
                                <span class="clearable">
                                    <input class="searchText  form-control" type="text" id="CategoryName" placeholder="Kategori.." />
                                    <i class="clearable__clear">&times;</i>
                                </span></td>
                            <td><span class="clearable">
                                <input class="searchText  form-control" type="text" id="BrandName" placeholder="Marka.." />
                                <i class="clearable__clear">&times;</i>
                            </span></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </thead>
                    <tbody id="">
        <%
            foreach (var count in Model.Source)
            {
        %>
        <%string productUrl = Helpers.ProductUrl(count.ProductId, count.ProductName); %>
        <tr>
            <td>
                <a href="<%=productUrl%>">
                    <%=count.ProductNo%></a>
            </td>
            <td>
                <a href="<%=productUrl%>">
                    <%=Helpers.Truncate(count.ProductName, 100)%></a>
            </td>

            <td>
                <%=count.CategoryName %>
            </td>
            <td>
                <%=count.BrandName %>
            </td>

            <td>
                <%=count.Sort%> - <a href="#" data-toggle="modal" data-target="#SortModalEdit" onclick="SortEditShow(<%=count.ProductId %>)" class="btn btn-xs btn-default"><span class="glyphicon glyphicon-pencil"></span></a>
            </td>
            <td>
                <a href="<%=productUrl%>" class="btn btn-xs btn-default"><span class="glyphicon glyphicon-eye-open"></span>&nbsp;İncele </a>
            </td>
        </tr>
        <%} %>


                          </tbody>
                </table>
                <hr>
                <div class="row">
                    <% int pageCount = Model.TotalLinkPages.Count(); %>
                    <div class="col-md-6 ">
                        Toplam
                    <%: pageCount%>
                    sayfa içerisinde
                    <%: Model.CurrentPage%>. sayfayı görmektesiniz.
                    </div>
                    <div class="col-md-6 text-right">
                        <ul class="pagination m0">
                            <li><a onclick="PageChangeOtherSettings1(1,2,1);" href="#">&laquo; </a></li>
                            <% foreach (var item in Model.TotalLinkPages)
                                { %>
                            <% if (Model.CurrentPage == item)
                                { %>
                            <li class="active"><a href="#">
                                <%: item%></a></li>
                            <% }
                                else
                                { %>
                            <li>
                                <a onclick="PageChangeOtherSettings1(<%: item %>,2,1);" href="#"><%: item%></a></li>
                            <% } %>
                            <% } %>
                            <li><a href="#" onclick="PageChangeOtherSettings1(<%: pageCount%>,2,1);">&raquo; </a></li>
                        </ul>
                    </div>
                </div>
<script type="text/javascript">
    $(document).ready(function () {
        $(".clearable").each(function () {

            var $inp = $(this).find("input:text"),
                $cle = $(this).find(".clearable__clear");

            $inp.on("input", function () {
                $cle.toggle(!!this.value);
            });

            $cle.on("touchstart click", function (e) {
                e.preventDefault();
                $inp.val("").trigger("input");
            });
        });
        $('.searchText').on('keypress', function (e) {
            if (e.which === 13) {
                var currentPage = $("#CurrentPage").val();
                var displayType = $("#DisplayType").val();
                var activeType = $("#ProductActiveType").val();
                PageChangeOtherSettings1(currentPage, displayType, activeType);


            }
        });
    });
</script>