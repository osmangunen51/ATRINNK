<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master"
    Inherits="System.Web.Mvc.ViewPage<AddressModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">




</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>
                <%if (ViewData["error"] == "true")
                    { %>
                <div class="alert alert-danger alert-dismissable" data-rel="email-wrapper">
                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                        &times;
                    </button>
                    <strong>Hata! </strong>
                    Böyle bir aktivasyon kodu bulunamadı
                </div>

                <%} %>
                <div class="well store-panel-container col-xs-12">
                    <%using (Html.BeginForm("PhoneActive", "Personal", FormMethod.Post, new { @class = "form-horizontal", role = "form" }))
                        { %>
                    <input type="hidden" name="mtypePage" value="<%:ViewData["mtypePage"] %>" />
                    <input type="hidden" name="uyeNo" value="<%:ViewData["uyeNo"] %>" />
                    <input type="hidden" name="urunNo" value="<%:ViewData["urunNo"] %>" />
                    <input type="hidden" name="gelSayfa" value="<%:ViewData["gelenSayfa"] %>" />
                    <div class="form-group ">

                        <label class="col-sm-3 control-label">
                            <b><%:ViewData["phoneNumber"] %></b> numaralı telefona gelen
                            <br />
                            Aktivasyon Kodu (6 Haneli)
                        </label>
                        <div class="col-sm-7 col-md-5">
                            <%:Html.TextBox("activationCode", "", new Dictionary<string, object> { { "type", "text" },{"class","form-control popovers"},{"size ","30" },{"required","true"}})%>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-9">
                            <button type="submit" class="btn btn-primary">
                                Aktif Et
                            </button>
                        </div>
                    </div>
                    <%} %>
                </div>

                <div class="panel panel-mt col-xs-12 p0">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon-question-sign"></span>
                        Sayfa
            Yardımı
                    </div>
                    <div class="panel-body">
                        <b>E-posta
                        </b>
                        <br>

                        <i class="fa fa-angle-right"></i>
                        &nbsp;&nbsp;
            <a
                href="#">E-posta adresimin kullanım dışı olduğu uyarısını
              alıyorum. Ne yapabilirim?
            </a>
                        <br>

                        <i class="fa fa-angle-right"></i>
                        &nbsp;&nbsp;
            <a
                href="#">Üye olduktan sonra onay e-postası ulaşmadı. Ne
              yapmalıyım?
            </a>
                        <br>

                        <i class="fa fa-angle-right"></i>
                        &nbsp;&nbsp;
            <a
                href="#">Üyelik bilgilerimdeki e-posta adresimi değiştirebilir
              miyim?
            </a>
                        <br>

                        <i class="fa fa-angle-right"></i>
                        &nbsp;&nbsp;
            <a
                href="#">EK-6 makinaturkiye.com Kullanıcı Sözleşmesi
            </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
