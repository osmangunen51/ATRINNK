﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<MessageViewModel>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
    Gelen Kutusu-Makina Türkiye
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript" language="javascript">
        function deleteMessage() {
            if (confirm('Mesajı silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Account/Message/DeleteMessage',
                    type: 'post',
                    dataType: 'json',
                    data: {
                        MessageId: $('#MessageId').val(),
                        messagetype: $('#messagetype').val()
                    },
                    success: function (data) {
                        alert('Mesajınız başarıyla silinmiştir.');
                        window.location.href = '/Account/Message/Index?MessagePageType=0';
                    },
                    error: function (x, l, e) {
                        alert("hata");
                    }
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MainContent" runat="server">
    <%MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities(); %>
    <% var product = entities.Products.Where(x => x.ProductId == Model.EntitiesMessage.ProductId).FirstOrDefault();

    %>
    <input type="hidden" id="messagetype" name="messagetype" value="<%=Request.QueryString["RedirectMessageType"].ToInt32() %>" />
    <input type="hidden" id="MessageId" name="MessageId" value="<%:Model.EntitiesMessage.MessageId %>" />

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">Favori İlanlarım
            </h4>
        </div>
    </div>

    <div class="row">

        <div class="col-sm-12 col-md-12">
            <div class="row hidden-xs">
                <div class="col-xs-12">
                    <ol class="breadcrumb breadcrumb-mt">
                        <li><a href="#">Profilim </a></li>
                        <li class="active">Mesajlarım </li>
                    </ol>
                </div>
            </div>
            <div class="well store-panel-container">
                <div class="page-header mt0">
                    <div class="btn-group pull-right">
                        <a href="/Account/Message/Index?MessagePageType=1&Mainparty=<%:Model.MemberMessageDetail.Member.MainPartyId%>&messageid=<%:Model.EntitiesMessage.MessageId %>&UrunNo=<%:product.ProductId %>"
                            class="btn btn-sm btn-default"><span class="glyphicon glyphicon-pencil"></span>&nbsp;Yanıtla
                        </a><a onclick="deleteMessage();" class="btn btn-sm btn-default"><span class="glyphicon glyphicon-trash"></span>&nbsp;Sil </a>
                    </div>
                    <div class="visible-sm">
                        <br />
                        <br />
                        <br />
                    </div>
                    <h5 class="mt0 di">
                        <%
                        %>
                        <b style="font-size: 12px;">İlan No:</b><span class="text-muted"><a href="<%:Model.ProductUrl%>"><%:product.ProductNo %></a></span><br />
                        <b style="font-size: 12px;">Ürün Adı:</b><span class="text-muted"><a href="<%:Model.ProductUrl %>"><%:product.ProductName %></a></span><br />
                        <br />
                        <b>Konu: </b><span class="text-muted">
                            <%=Model.EntitiesMessage.MessageSubject%>
                            <%-- <% if (FileHelpers.HasFile(AppSettings.MessageFileFolder + Model.EntitiesMessage.MessageFile))
                               { %>
                            <br />
                            <b>Dosya :</b> <a href="/UserFiles/MessageFiles/<%: Model.EntitiesMessage.MessageFile %>">
                                Dosyayı Yükle </a>
                            <% } %>--%>
                        </span>
                    </h5>
                    <br />
                </div>
                <p>
                    <%=Model.EntitiesMessage.MessageContent%>
                </p>
                <hr />
                <div class="row">
                    <%=Html.RenderHtmlPartial("MemberInfo", Model.MemberMessageDetail) %>
                    <%--  <%if (Model.Product != null)
                      {  %>
                    <% 
                          var groupname = entities.Categories.Where(c => c.CategoryId == Model.Product.ProductGroupId).SingleOrDefault().CategoryName;
                          var categoryname = entities.Categories.Where(c => c.CategoryId == Model.Product.CategoryId).SingleOrDefault().CategoryName;
                          string productUrl = "/" + Helpers.ToUrl(groupname) + "/" + Model.Product.ProductId + "/" + Helpers.ToUrl(categoryname) + "/" + Helpers.ToUrl(Model.Product.ProductName);%>
                    <%var categorybrand = entities.Categories.Where(c => c.CategoryId == Model.Product.BrandId).SingleOrDefault(); %>
                    <%var categorymodel = entities.Categories.Where(c => c.CategoryId == Model.Product.ModelId).SingleOrDefault(); %>
                    <div class="col-sm-6">
                        <b>İlan Bilgileri </b>
                        <br />
                        İlan No :
                        <%:Model.Product.ProductNo%>
                        <a target="_blank" href="<%:productUrl %>">(İlanı görmek için buraya tıklayınız.) </a>
                        <br />
                        İlgili Ürün Adı :
                        <%: Model.Product.ProductName %>.
                        <br />
                        Marka :
                        <%:categorybrand.CategoryName%>
                        <br />
                        Model Tipi:
                        <%:categorymodel.CategoryName%>
                    </div>
                    <%} %>--%>
                </div>
                <hr />
                <div class="alert alert-info">
                    <span class="glyphicon glyphicon-info-sign"></span><strong>MakinaTürkiye </strong>
                    e-posta yoluyla hiç bir şekilde kullanıcılarından kullanıcı adı ve şifre bilgilerini
                    talep etmemektedir. Size bu yönde gelebilecek olan e-postaları kesinlikle dikkate
                    almayınız.
                </div>


            </div>
        </div>
    </div>
</asp:Content>

