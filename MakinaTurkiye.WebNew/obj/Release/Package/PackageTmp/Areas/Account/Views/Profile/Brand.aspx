<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Markalarımız-Makina Türkiye
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <%=Html.RenderHtmlPartial("ProfileStyle")%>
    <script type="text/javascript">
        function DeleteStoreBrand(Id) {
            if (confirm('Markayı silmek istediğinizden emin misiniz ?')) {
                $.ajax({
                    url: '/Profile/DeleteStoreBrand',
                    type: 'post',
                    data: { StoreBrandId: Id },
                    success: function (data) {
                        if (data) {
                            alert('Silmek istediğiniz marka başarıyla silinmiştir.');
                            location.reload();
                        }
                        else {
                            alert('Silme işlemi sırasında bir hata oluşmuştur. Tekrar deneyiniz.');
                        }
                    },
                    error: function (x, l, e) {
                        alert(x.responseText);
                    }
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">

                <span class="text-primary glyphicon glyphicon-cog"></span>&nbsp;Firma Markaları
            </h4>
        </div>
    </div>
    <div class="profileBg">
        <%using (Html.BeginForm("Brand", "Profile", FormMethod.Post, new { enctype = "multipart/form-data" }))
            {%>
        <div class="col-sm-12 col-md-12">
            <div>
                <div class="well well-mt2">
                    <div class="row">
                        <div class="col-xs-12 col-md-8">
                            <div class="alert alert-info">
                                <span class="glyphicon glyphicon-info-sign"></span><strong>&nbsp;Bayisi olduğunuz marka
                                    logosunu ekleyiniz. </strong>
                                <br>
                                Logonuzu yüklemeniz aynı zamanda sonuçların listelendiği sayfalarada tanınmanızı
                                kolaylaştıracaktır
                            </div>
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Marka Adı:
                                    </label>
                                    <div class="col-sm-10">
                                        <%= Html.TextBox("StoreBrandName", "", new {@class="form-control", placeholder="Marka adını girin." })%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Marka Açıklama:
                                    </label>
                                    <div class="col-sm-10">
                                        <%= Html.TextArea("BrandDescription", new { @class = "form-control", placeholder = "Marka açıklamasını girin." })%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-sm-2 control-label">
                                        Logo:
                                    </label>
                                    <div class="col-sm-10">
                                        <input type="file" name="upFileBrandImage" />
                                    </div>
                                    <div class="col-sm-offet-2 col-sm-10">
                                        <p class="help-block">
                                            Eklemek istediğiniz logonun yerini 'Dosya Seç' butonunu tıklayarak bulun ve seçin.
                                            Eklenen logonuzu görümünü pencereden kontrol edin. Şuanda logonuza ulaşamıyorsanız
                                            devam edebilirsiniz.
                                        </p>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-xs-offset-2 col-sm-10">
                                        <%-- <a href="#" class="btn btn-sm btn-default"><span class="glyphicon glyphicon-trash"></span>
                                            &nbsp;Seçili Resmi Sil </a>--%>
                                        <button type="submit" class="btn btn-sm btn-default">
                                            Yükle
                                        </button>
                                        &nbsp; Maksimum dosya boyutu: 10 Kb
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-md-4">
                            <% foreach (var item in Model.StoreBrandItems)
                                { %>

                            <div class="media media-mt">
                                <a class="pull-left" href="#">
                                    <% var img = AppSettings.StoreBrandImageFolder + item.BrandPicture; %>
                                    <img class="media-object" src="<%: img %>" alt="<%: item.BrandName %>" />
                                </a>
                                <div class="media-body">
                                    <h5 class="media-heading">
                                        <%=item.BrandName%>
                                        <a href="#" class="btn btn-xs btn-default" onclick="DeleteStoreBrand('<%:item.StoreBrandId %>');">
                                            <span class="glyphicon glyphicon-trash"></span>&nbsp;Sil </a>
                                    </h5>
                                    <%= Html.Truncate( item.BrandDescription, 50)%>
                                </div>
                            </div>

                            <% } %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%}%>
    </div>
</asp:Content>
