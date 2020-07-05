<%@ Control Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewUserControl<FilterModel<NeoSistem.MakinaTurkiye.Management.Models.Catolog.HomeSectorProductItem>>" %>
<% int row = 0; %>

<% foreach (var item in Model.Source)
    { %>
<% row++; %>
<tr id="row<%: item.Id %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
    <td class="CellBegin">
        <%: item.SectorName%>
    </td>
    <td class="Cell">
        <%:item.StoreName %>
    </td>
    <td class="Cell">
        <%string productUrl = Helpers.ProductUrl(item.ProductId, item.ProductName); %>
        <a href="<%:productUrl %>"><%: item.ProductNo%></a>
    </td>

    <td class="Cell">
        <%: item.ProductName%>
    </td>


    <td class="Cell">
        <%string beginDate = item.BeginDate != null ? item.BeginDate.ToDateTime().ToString("dd.MM.yyyy") : ""; %>
        <%:beginDate %>
    </td>
    <td class="Cell">
        <%string endDate = item.EndDate != null ? item.EndDate.ToDateTime().ToString("dd.MM.yyyy") : ""; %>
        <%:endDate %>
    </td>
    <td class="Cell">
        <%if (item.Type == (byte)HomePageProductType.Free)
            {%>
            Ücretsiz
           <% }
               else
               {
           %>
            Ücretli
               <%}%>
    </td>
    <td class="Cell">
        <div id="displayOrder<%:item.Id %>">
            <span>
            <%item.ProductHomeOrder = item.ProductHomeOrder.HasValue ? item.ProductHomeOrder.Value : 0; %>
            <%:item.ProductHomeOrder %>
            </span>
                <a onclick="GetOrder(<%:item.Id %>)">
                <img src="/Content/images/edit.png">
            </a>
        </div>
        <div id="inputOrderContainer<%:item.Id %>" style="display: none">
            <input value="<%:item.ProductHomeOrder %>" type="text" id="inputOrder<%:item.Id %>" />
            <button onclick="UpdateOrder(<%:item.ProductId %>, <%:item.Id %>)">
                Kaydet
            </button>
        </div>



    </td>
    <td class="Cell">
        <a title="Düzenle" style="cursor: pointer;" href="/Product/Edit/<%:item.ProductId %>">
            <img src="/Content/images/edit.png" hspace="2" />
        </a>
        <a title="Sil" style="cursor: pointer;" onclick="DeletePost(<%: item.ProductId %>);">
            <img src="/Content/images/delete.png" hspace="2" />
        </a>
    </td>
    <td class="CellEnd">
    <%string text = "";

        if (DateTime.Now.Date > item.EndDate.Date)
        {
            text = "<span style='color:red;'>Süresi Doldu</span>";
        }
        else {
            text ="<span style='color:green;'>"+ (item.EndDate - item.BeginDate).TotalDays.ToString()+" kalan gün</span>";
        }%>
    <%=text %>
</td>
<% } %>
</tr>

<tr>
    <td class="ui-state ui-state-default" colspan="10" align="right" style="border-color: #DDD; border-top: none; border-bottom: none;">
        <div style="float: right;" class="pagination">
            <div style="float: left; margin-right: 10px;">
                <b><%:Model.CurrentPage %> . sayfadasınız
            </div>
            <ul style="float: right;">

                <% foreach (int i in Model.TotalLinkPages)
                    { %>
                <li>
                    <% if (i == Model.CurrentPage)
                        { %>
                    <span class="currentpage">
                        <%: i%></span>&nbsp;
			 <% } %>
                    <% else
                        { %>
                    <a href="javascript:void(0)" onclick="PagePost(<%:i %>)">
                        <%: i%></a>&nbsp;
			 <% } %>
                </li>
                <% } %>
            </ul>
        </div>
    </td>

</tr>
<tr>
    <td class="ui-state ui-state-default" colspan="10" align="right" style="border-color: #DDD; border-top: none; border-bottom: none;">
        <b>Toplam Adet:</b><%:Model.TotalRecord %>
    </td>
</tr>


<script type="text/javascript">
    function DeletePost(productId) {
        if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
            $.ajax({
                url: '/Product/HomeSectorDelete',
                data: { id: productId },
                type: 'post',
                dataType: 'json',
                success: function (data) {

                    if (data) {
                        $('#row' + productId).hide();
                    }
                }
            });
        }
    }
    function GetOrder(id) {
        $("#displayOrder" + id).hide();
        $("#inputOrderContainer" + id).show();

    }
    function UpdateOrder(pId, id) {
        $("#preLoading").show();
        var newValue = $("#inputOrder"+id).val();
        $.ajax({
            url: '/Product/HomeSectorProductOrder',
            data: {
                productId: pId,
                order: newValue
            },
            type: 'post',
            dataType: 'json',
            success: function (data) {
                if (data) {
                    $("#displayOrder" + id+" span").html(newValue);
                    $("#displayOrder" + id).show();
                    $("#inputOrderContainer" + id).hide();
                        $("#preLoading").hide();

                }
                else {
                    alert("İşlem Başarısız. Lütfen 0 dan büyük değer giriniz!");
                               $("#preLoading").hide();
                }
            }
        });

    }
</script>
