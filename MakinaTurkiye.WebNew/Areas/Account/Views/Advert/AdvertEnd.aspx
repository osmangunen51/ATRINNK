<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master"
    Inherits="System.Web.Mvc.ViewPage<ProductModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function nextStatu() {
            $('#loaderDiv').show();
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row">
            <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
                <div class="col-md-12">
        <h4 class="mt0 text-info">
           İlan Ekle
        </h4>
    </div>
       </div>

    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>
                <div class="well well-mt4">
                    <div>
                        <div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-5">
                                    <% if (Model.PictureList != null)
                                       { %>
                                    <% foreach (var itemPicture in Model.PictureList)
                                       {
                                           if (Model.PictureList.First() == itemPicture)
                                           {
                                    %>
                                    <img class="img-thumbnail" width="326" src="<%=string.Format("{0}{1}/{2}", AppSettings.ProductImageFolder, itemPicture.ProductId.ToString(), itemPicture.PicturePath)%>" />
                                    <% break;
                                           }
                                    %>
                                    <% } %>
                                    <% } %>
                                    <br>
                                    <br>

                                    <b>Ürün Açıklaması :
                                    </b>
                                    <br>
                                    <%=Model.ProductDescription%>
                                </div>
                                <div class="col-xs-12 col-sm-7">
                                    <h4><%= Model.ProductName%></h4>
                                    <hr>
                                    <b>Kategori :
                                    </b>
                                    <%:Model.GetCategoryName(Model.CategoryId) %> 
                 
                                    <br>
                                    <br>

                                    <b>Marka :
                                    </b>
                                    <%: Model.BrandId.HasValue ? Model.GetCategoryName(Model.BrandId.Value) : Model.OtherBrand%>
                                    <br>
                                    <br>

                                    <b>Model Tipi :
                                    </b>
                                    <%: Model.ModelId.HasValue ? Model.GetCategoryName(Model.ModelId.Value) : Model.OtherModel%>
                                    <br>
                                    <br>

                                    <b>Fiyatı :
                                    </b>
                                    <%=ViewData["dov"]%>
                                    <br>
                                    <br>

                                    <b>Model Yılı :
                                    </b>
                                    <%: Model.ModelYear%>
                                    <br>
                                    <br>

                                    <b>İlan Geçerlilik Süresi :
                                    </b>
                                    <% 
                                        TimeSpan time;
                                        time = Model.ProductAdvertEndDate.Subtract(Model.ProductAdvertBeginDate);
                                        int toplamdakika = Convert.ToInt32(time.TotalDays);
                                    %>
                                    <% string zaman = Convert.ToInt32(toplamdakika / 30).ToString();%>
                                    <% if (zaman.ToInt32() == 12)
                                       { %>
                    1 Yıl
                   
                                    <% } %>
                                    <%else
                                       {%>
                                    <%=zaman%>
                    Ay
                   
                                    <%} %>
                                    <br>
                                    <br>

                                    <%--<% if (Model.ProductFeatures != null)
                   { %>
                <% XDocument xdoc = XDocument.Parse(Model.ProductFeatures);%>
                <% var items = from x in xdoc.Element("Product").Elements()
                               select new
                               {
                                   Text = x.Attribute("Text").Value,
                                   Name = x.Attribute("Name").Value,
                                   Elements = x.Elements()
                               }; %>
                <% foreach (var item in items)
                   { %>
                    <%: item.Text %> :
                    <% foreach (var itemElements in item.Elements)
                       { %>
                        <%: itemElements.Attribute("Text").Value%>,
                    <% } %>
                <% } %>
                <% } %>--%>

                                    <b>İlan No :
                                    </b>
                                    <%:Model.ProductNo%>
                                    <br>
                                    <br>

                                    <b>İlan Tarihi :
                                    </b>
                                    <%: Model.ProductRecordDate.ToString("dd.MM.yyyy")%>
                                    <br>
                                    <br>
                                </div>
                            </div>
                            <div class="row mt20">
                                <div class="col-sm-offset-3 col-sm-9 btn-group">
                                <%--    <a href="#" class="btn btn-default">Resim Düzenle</a>
                                    <a href="#" class="btn btn-default">Video Düzenle</a>
                                    <a href="#" class="btn btn-default">Ürün Bilgileri Düzenle</a>--%>
                                    <% using (Html.BeginForm("AdverEnd", "Advert", FormMethod.Post))
                                       { %>
                                    <button type="submit" onclick="nextStatu();" class="btn btn-primary">Kaydet</button>
                                    <div id="loaderDiv" style="float: right; width: auto; height: 20px; display: none; font-size: 12px; margin-top: 8px;">
                                        <img src="../../../../Content/V2/images/loading.gif" width="30" alt="" />&nbsp; İşleminiz gerçekleştiriliyor, lütfen bekleyiniz.
                       
                                    </div>
                                    <% } %>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
