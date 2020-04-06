<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<ProductModel>>" %>

<%MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
  var product = entities.Products.Where(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId).ToList();
  int singular = 0;
  int plural = 0;
  foreach (var count in product)
  {
      singular = count.SingularViewCount.ToInt32() + singular;
      plural = count.ViewCount.ToInt32() + plural;
  }
      %>
            <table class="table table-hover table-condensed">
                <thead>
                    <tr>
                        <th style="width: 100px">
                            Toplam İlan Sayısı
                        </th>
                        <th style="width: 100px">
                            Toplam Tekil Hit
                        </th>
                        <th style="width: 150px">
                            Toplam Çoğul Hit
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <%:String.Format("{0:#,#}", product.Count)%>
                            ilan
                        </td>
                        <td>
                            <%:String.Format("{0:#,#}", singular)%>
                            kez görüntülendi
                        </td>
                        <td>
                            <%:String.Format("{0:#,#}", plural)%>
                            kez görüntülendi
                        </td>
                    </tr>
                </tbody>
            </table>
            <%if (product.Count != 0)
              {%>
            <table class="table table-hover table-condensed">
                <thead>
                    <tr>
                        <th style="width: 100px">
                            Ürün No
                        </th>
                        <th>
                            Ürün Adı
                        </th>
                        <th>
                            Tekil
                        </th>
                        <th>
                            Çoğul
                        </th>
                        <th>
                        </th>
                    </tr>
                </thead>
                <tbody>
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
                            <%=count.SingularViewCount%>
                        </td>
                        <td>
                            <%=count.ViewCount%>
                        </td>
                        <td>

                            <a href="/Account/Statistic/ProductStatistics?ProductId=<%:count.ProductId %>" class="btn btn-xs btn-default"><span class="glyphicon glyphicon-eye-open">
                            </span>&nbsp;İncele </a>
                        </td>
                    </tr>
                    <%
                  }
      %>
                </tbody>
            </table>
            <%
              }
      %>
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
                        <li onclick="PageChangeStatic(1,2,1);"><a href="#">&laquo; </a></li>
                        <% foreach (var item in Model.TotalLinkPages)
                           { %>
                        <% if (Model.CurrentPage == item)
                           { %>
                        <li class="active"><a href=""><%: item%></a></li>
                        <% }
                           else
                           { %>
                        <li onclick="PageChangeStatic(<%: item %>,2,1);"><span style="cursor: pointer;"><%: item%></span></li>
                        <% } %>
                        <% } %>
                        <li onclick="PageChangeStatic(<%: pageCount%>,2,1);"><a href="#">&raquo; </a></li>
                    </ul>
                </div>
            </div>