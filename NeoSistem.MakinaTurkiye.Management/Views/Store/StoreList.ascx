﻿<%@ Control Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewUserControl<FilterModel<StoreModel>>" %>
<% int row = 0; %>

<% foreach (var item in Model.Source)
    { %>
<%MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities(); %>
<% row++; %>
<tr id="row<%: item.MainPartyId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
    <td class="CellBegin" style="text-align:center">
        <%: item.StoreNo %>
    </td>
    <td class="Cell">
        <%: item.MainPartyFullName%>
    </td>
    <td class="Cell">

        <%: item.StoreName %><br />
        <font style="color: #027a14">
          <%:item.StoreShortName %>

      </font>
    </td>
    <td class="Cell" align="center">
        <img itemprop="logo" style="border: solid 1px #b6b6b6;" width="70" height="50" src="<%= ImageHelpers.GetStoreImage(item.MainPartyId,item.StoreLogo,"100")%>" />
    </td>
    <td class="Cell" title="<%: item.StoreRecordDate.ToString("dd MMMM yyyy dddd") %>">
        <%: item.StoreRecordDate.ToString("dd.MM.yyyy")%>
        <br />
        <%if (!item.PacketName.Equals("Standart Paket") && !item.PacketName.Equals("Özel Paket") && !item.PacketName.Equals("Sınırlı Paket"))
            {
        %>
        <span style="color: #9d0606;text-align:center">
            <%: (item.StorePacketEndDate.HasValue ? item.StorePacketEndDate.Value.ToString("dd.MM.yyyy") : "")%>
        </span>
        <%} %>
    </td>
    <td class="Cell">
        <%if (string.IsNullOrEmpty(item.AuthName)) { item.AuthName = "Tanımsız"; } %>
        <%:item.AuthName %>
    </td>
    <td class="Cell">
        <%if (string.IsNullOrEmpty(item.PortfoyUserName)) { item.PortfoyUserName = "Tanımsız"; } %>
        <%:item.PortfoyUserName %>
    </td>
    <td class="Cell">
        <%var packetfeature = entities.PacketFeatures.Where(c => c.PacketId == item.PacketId && c.PacketFeatureTypeId == 3).FirstOrDefault();
            string sayi = "";
            if (packetfeature != null)
            {
                if (packetfeature.FeatureContent != null)
                {
                    sayi = "(sınırsız)";
                }
                else if (packetfeature.FeatureActive != null)
                {
                    if (packetfeature.FeatureActive == true)
                    {
                        sayi = "";
                    }
                    else
                    {
                        sayi = "(0)";
                    }
                }
                else
                {
                    sayi = "(" + packetfeature.FeatureProcessCount.ToString() + ")";
                }
            }
        %>
        <%: item.PacketName %>
        <%if (sayi != "")
            {  %>
        <div style="color: Blue;"><%:sayi%></div>
        <%} %>
    </td>
    <td class="Cell">
        <%
            string packet = "";
            if (item.StoreActiveType == 1)
            {
                packet = "İnceleniyor";
            }
            else if (item.StoreActiveType == 2)
            {
                packet = "Onaylandı";
            }
            else if (item.StoreActiveType == 3)
            {
                packet = "Onaylanmadı";
            }
            else if (item.StoreActiveType == 4)
            {
                packet = "Silindi";
            }
        %>
        <%: packet %>
    </td>

    <td class="Cell">
            <%if (item.CountryName!="Türkiye")
                {%>
                    <span style="color:yellow"><%:item.CountryName %></span><br />
                <%}%>
                <span style="color:red"><%:item.CityName.ToUpper() %></span><br />
                <span style="color:yellowgreen"><%:item.LocalityName %></span>
                &nbsp
    </td>
    <td class="Cell">
        <%string webUrl = EnumModels.UrlHttpEdit(item.StoreWeb);  %>
        <%if (!string.IsNullOrEmpty(webUrl))
            {%>
        <a href="<%:webUrl %>" rel="external" onmousedown="this.target = '_blank';">
            <%: webUrl.Replace("http://","").Replace("https://","")%>  </a>
        <% } %>

    </td>
     <%var mem = (from c in new MakinaTurkiyeEntities().MemberStores
                   where c.StoreMainPartyId == item.MainPartyId
                   select c).FirstOrDefault();%>
    <%--<td class="Cell">
        <%
            int Mainpartyid = 49348;
            if (mem != null)
            {
                Mainpartyid = mem.MemberMainPartyId.ToInt32();
            }
            var product = entities.Products.Where(c => c.MainPartyId == Mainpartyid).ToList();

            int singleproduct = 0;
            int pluralproduct = 0;
            int activepro = 0;
            int pasifpro = 0;
            if (product != null)
            {
                foreach (var spro in product)
                {
                    singleproduct = singleproduct + spro.SingularViewCount.ToInt32();
                    pluralproduct = pluralproduct + spro.ViewCount.ToInt32();
                    if (spro.ProductActive == false)
                    {
                        pasifpro = pasifpro + 1;
                    }
                    else activepro = activepro + 1;
                }
            }
        %>
        <%if (item.ViewCount != 0)
            {  %>
        <span style="color: #bababa;">Ç: </span><%=item.ViewCount%><br />
        <span style="color: #bababa;">T: </span><%=item.SingularViewCount%>
        <%}
            else
            {  %>
	 -
	 <%} %>
    </td>--%>
    <td class="Cell">
        <%
            int Mainpartyid = 49348;
            if (mem != null)
            {
                Mainpartyid = mem.MemberMainPartyId.ToInt32();
            }
            var product = entities.Products.Where(c => c.MainPartyId == Mainpartyid).ToList();

            int singleproduct = 0;
            int pluralproduct = 0;
            int activepro = 0;
            int pasifpro = 0;
            if (product != null)
            {
                foreach (var spro in product)
                {
                    singleproduct = singleproduct + spro.SingularViewCount.ToInt32();
                    pluralproduct = pluralproduct + spro.ViewCount.ToInt32();
                    if (spro.ProductActive == false)
                    {
                        pasifpro = pasifpro + 1;
                    }
                    else activepro = activepro + 1;
                }
            }

            if (product.Count != 0)
            {  %>
        <table>
            <tr>
                <td>
                    <span style="color: #bababa;">Tplm</span>
                </td>
                <td width:5>
                    :
                </td>
                <td>
                    <span style="color: red;"><%=product.Count%></span>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="color: #bababa;">Aktif</span>
                </td>
                <td width:5>
                    :
                </td>
                <td>
                    <span style="color: green;"><%=activepro%></span>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="color: #bababa;">Pasif</span>
                </td>
                <td width:5>
                    :
                </td>
                <td>
                    <span style="color: blue;"><%=pasifpro%></span>
                </td>
            </tr>
        </table>
        <%}
        else
        {%>
		 -
	    <%}%>
    </td>
    <%--<td class="Cell">
        <%if (product.Count != 0)
            {  %>
        <span style="color: #bababa;">Ç: </span><%=pluralproduct%><br />
        <span style="color: #bababa;">T: </span><%=singleproduct%>

        <%}
            else
            {  %>
	 -
	 <%} %>
    </td>--%>
    <td class="CellEnd" align="center" style="width:15%">
        <a title="Giriş" style="cursor: pointer;" target="_blank" href="<%:item.LogingLink %>">Hesabım </a>
        <%string updateLink = "/Store/EditStore/" + item.MainPartyId; %>
        <%if (item.StoreActiveType == 2) { updateLink = "/Store/StoreDetailInformation/" + item.MainPartyId; } %>
        <a title="Düzenle" href="<%:updateLink %>">
            <img src="/Content/images/edit.png" hspace="2" />
        </a><a title="Sil" style="cursor: pointer;" onclick="DeletePost(<%: item.MainPartyId %>);">

            <img src="/Content/images/delete.png" hspace="2" />
        </a><a title="Ürünler" href="/Product/Index?StoreMainPartyId=<%: item.MainPartyId %>"
            hspace="2">
            <img src="/Content/images/ChartResetToMatchStyle.png" />
        </a>
        <%if (!string.IsNullOrEmpty(item.StoreUrlName))
            {%>
        <a title="Detay" href="<%= AppSettings.MakinaTurkiyeWebUrl + "/"+item.StoreUrlName %>"
            target="_blank">
            <img src="/Content/images/rightAllow.png" />
        </a>
        <%}
            else
            {%>
        <a title="Detay" href="<%= AppSettings.MakinaTurkiyeWebUrl + "/sirket/"+item.MainPartyId+"/"+Helpers.ToUrl(item.StoreName)+"/sirketprofili" %>"
            target="_blank">
            <img src="/Content/images/rightAllow.png" />
        </a>
        <%} %>

        <% if (mem != null)
            { %>
        <%
            var md = (from c in entities.MemberDescriptions
                      where c.MainPartyId == mem.MemberMainPartyId
                      orderby c.Date descending
                      select c).FirstOrDefault();
        %>
        <%if (md != null)
            { %>
        <a title="Açıklamalar" href="/Member/BrowseDesc1/<%: mem.MemberMainPartyId %>" target="_blank">
            <img src="/Content/images/productonay.png">
        </a>
        <%}
            else
            { %>
        <a title="Açıklamalar" href="/Member/BrowseDesc1/<%: mem.MemberMainPartyId %>" target="_blank">
            <img src="/Content/images/product.png" />
        </a>
        <%} %>
        <div style="float: right; width: 30px; text-align: right; cursor: pointer;">
            <a href="/Member/storemail/<%=mem.MemberMainPartyId  %>" id="lightbox_click" rel="superbox[iframe][1024x600]">
                <img src="/Content/Images/ikon_ozel_mesaj_gonder.png" /></a>
        </div>
        <% }
            else
            { %>
        <a title="Açıklamalar" href="/Member/BrowseDesc1/" target="_blank">
            <img src="/Content/images/product.png" />
        </a>
        <%} %>

        <a onclick="StoreAddExel('<%:item.MainPartyId %>');">
            <img src="/Content/images/exceladd.jpg" hspace="2" />
        </a>
        <a title="Fatura Ekle" href="/OrderFirm/OrderCreate?storeId=<%:item.MainPartyId %>">
            <img src="/Content/images/invoices.png" width="16" height="16" hspace="2" />
        </a>
        <%if (item.StoreShortName != null)
            { %>
        <a title="Firma Kısa Adı Tanımlı" href="javascript:void(0)" target="_blank">
            <img src="/Content/images/Accept-icon.png" />
        </a>
        <%string backColor = "transparent";
            if (item.SeoStoreNotificationCount > 0)
            {
                backColor = "#54b354";
            }
            %>
        <a  title="Firma Seo Açıklama" href="/StoreSeoNotification/Index?storeMainPartyId=<%:item.MainPartyId %>" target="_blank">
            <img  src="/Content/RibbonImages/store-list.png" style="width: 16px;background-color:<%:backColor%>;" />
        </a>
        <a href="/Member/SendSpecialEmailToStore/<%:item.MainPartyId.ToString() %>" title="Özel Mail Gönder">Ö.M</a>

        <%} %>
        <%if (item.StoreUpdatedDate.HasValue)
            {%>
        <span style="color: #027a14" title="Firma Bilgileri Güncelendi <%:item.StoreUpdatedDate.Value.ToString("dd.MM.yyyy HH:mm") %>">F.B.G
        </span>
        <% }
            else
            {%>
        <a onclick="MakeStoreControlled(<%:item.MainPartyId %>)" title="Firma Bilgileri Güncellendi Yap">F.B.G</a>

        <%} %>
                    <%if (NeoSistem.MakinaTurkiye.Management.Models.Authentication.CurrentUserModel.CurrentManagement.UserId == 1) {
                    %>
            <a  style="cursor:pointer; color:Blue;" onclick="DeleteStore(<%:item.MainPartyId %>)">(Sil)</a>
            <%
                } %>
    </td>
</tr>
<% } %>
<% if (Model.TotalRecord <= 0)
    { %>
<tr class="Row">
    <td colspan="15" class="CellBegin Cell" style="color: #FF0000; padding: 5px; font-weight: 700; font-size: 14px;">Mağaza bulunamadı.
    </td>
</tr>
<% } %>
<tr>
    <td class="ui-state ui-state-default" colspan="15" align="right" style="border-color: #DDD; border-top: none; border-bottom: none;">
        <div style="float: right;" class="pagination">
            <ul>
                <%foreach (int page in Model.TotalLinkPages)
                    { %>
                <li>
                    <% if (page == Model.CurrentPage)
                        { %>
                    <span class="currentpage">
                        <%: page %></span>&nbsp;
			 <% } %>
                    <% else
                        { %>
                    <a onclick="PagePost(<%: page %>)">
                        <%: page %></a>&nbsp;
			 <% } %>
                </li>
                <% } %>
                <li>Gösterim: </li>
                <li>
                    <select id="PageDimension" name="PageDimension" onchange="SearchPost();">
                        <option value="20" <%: Session["store_PAGEDIMENSION"].ToString() == "20" ? "selected=selected" : "" %>>20</option>
                        <option value="50" <%: Session["store_PAGEDIMENSION"].ToString() == "50" ? "selected=selected" : "" %>>50</option>
                        <option value="100" <%: Session["store_PAGEDIMENSION"].ToString() == "100" ? "selected=selected" : "" %>>100</option>
                        <option value="250" <%: Session["store_PAGEDIMENSION"].ToString() == "250" ? "selected=selected" : "" %>>250</option>
                        <option value="500" <%: Session["store_PAGEDIMENSION"].ToString() == "500" ? "selected=selected" : "" %>>500</option>
                        <option value="750" <%: Session["store_PAGEDIMENSION"].ToString() == "750" ? "selected=selected" : "" %>>750</option>
                    </select>
                </li>
            </ul>
        </div>
    </td>
</tr>
<tr>
    <td class="ui-state ui-state-default" colspan="15" align="left" style="border-color: #DDD; border-top: none; border-bottom: none;">
        <div style="float: right;">
            <b>Toplam Firma: <%:Model.TotalRecord %></b>
        </div>

    </td>
</tr>
<tr>
    <td class="ui-state ui-state-default" colspan="15" align="left" style="border-color: #DDD; border-top: none; border-bottom: none;">
        <div style="float: right;">
            <button type="button" onclick="ExportStores()">
                <img src="../../Content/RibbonImages/excel.png" />
            </button>
        </div>

    </td>
</tr>

<link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
<script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
<script type="text/javascript">
    $(function () {
        $.superbox.settings = {
            closeTxt: "Kapat",
            loadTxt: "Yükleniyor...",
            nextTxt: "Sonraki",
            prevTxt: "Önceki"
        };
        $.superbox();
    });

    function MakeStoreControlled(mainPartyId) {
        if (confirm('Firma bilgilerinin kontrollü güncellendiğini onaylıyor musunuz?')) {
            location.href = "/Store/StoreInfoChecked/" + mainPartyId;
        }
    }
    function ExportStores() {
        var data = {
            StoreNo: $('#StoreNo').val(),
            StoreName: $('#StoreName').val(),
            MainPartyFullName: $('#MainPartyFullName').val(),
            StorePacketId: $('#StorePacketId').val(),
            StoreRecordDate: $('#StoreRecordDate').val(),
            StorePacketEndDate: $('#StorePacketEndDate').val(),
            StoreActiveType: $('#StoreActiveType').val(),
            StoreWeb: $('#StoreWeb').val(),
            OrderName: $('#OrderName').val(),
            PacketId: $('#PacketId').val(),
            Order: $('#Order').val(),
            Page: $('#Page').val(),
            PageDimension: $('#PageDimension').val(),
            PacketStatus: $('#PacketStatus').val(),
            PortfoyUserId: $("#PortfoyUserId").val(),
            AuthorizedId: $("#SalesUserId").val()
        };

        var s = "?" + $.param(data) + "";


        var url = '/Store/ExportStores' + s;
        window.open(url);
    }


</script>
<style type="text/css">
    /* Custom Theme */
    #superbox-overlay { background: #e0e4cc; }

    #superbox-container .loading { width: 32px; height: 32px; margin: 0 auto; text-indent: -9999px; background: url(styles/loader.gif) no-repeat 0 0; }

    #superbox .close a { float: right; padding: 0 5px; line-height: 20px; background: #333; cursor: pointer; }

        #superbox .close a span { color: #fff; }

    #superbox .nextprev a { float: left; margin-right: 5px; padding: 0 5px; line-height: 20px; background: #333; cursor: pointer; color: #fff; }

    #superbox .nextprev .disabled { background: #ccc; cursor: default; }
</style>

