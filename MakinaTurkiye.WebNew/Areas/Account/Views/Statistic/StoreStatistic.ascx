<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.Statistics.MTStatisticModel>" %>

<% 
    var store = new Store();
    try
    {
        MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
        var storeview = entities.MemberStores.Where(c => c.MemberMainPartyId == AuthenticationUser.Membership.MainPartyId).SingleOrDefault().StoreMainPartyId;
        store = entities.Stores.Where(c => c.MainPartyId == storeview).SingleOrDefault();
    }
    catch (Exception)
    {
        Response.Redirect("/Uyelik/kullanicigirisi");
    }

%>
<%--<div class="hesabim_orta_icerik">
  <div class="orta_baslik"><span style="color:#4C4C4C; font-family:Arial; font-size:11px;">Firma Görüntülenme</span></div>
<%if (store == null)
  {  %>
  Firma Üyeliğiniz mevcut değil.
<%}
  else
  {%>
  <span style="color:#4C4C4C; font-family:Arial; font-size:11px;">
  &nbsp;&nbsp; Firma Tekil Tıklanma &nbsp;&nbsp;:</span> <span style="color:#4C4C4C; font-family:Arial; font-size:11px;"><%:store.SingularViewCount%></span><br />
   <span style="color:#4C4C4C; font-family:Arial; font-size:11px;">&nbsp;&nbsp; Firma Çoğul Tıklanma :</span> <span style="color:#4C4C4C; font-family:Arial; font-size:11px;"><%:store.ViewCount%></span>
    <%} %>
    </div>--%>
<div class="col-sm-12 col-md-12">
    <div>
        <h4 class="mt0 text-info">
            <span class="text-primary glyphicon glyphicon-cog"></span>Firma İstatistikleri
        </h4>
        <div class="well well-mt2">
            <div class="alert alert-info">
                <span class="glyphicon glyphicon-info-sign"></span>Makina Türkiye özel reklam seçenekleri
                ile firmanızın görüntülenme sayısını arttırabilirsiniz. Detaylı bilgi için <a href="#">buraya </a>tıklayın.
            </div>
            <div class="row">
                <div class="col-md-12">

                    <%if (!string.IsNullOrEmpty(Model.JsonDatas))
                        {%>
                    <div class="col-md-8">
                        <div class="col-md-12" style="margin-bottom: 15px;">
                            <%=Html.RenderHtmlPartial("_StoreStatistic",Model) %>
                            <div style="float: right;"><a href="/Account/Statistic/StatisticStore" class="btn background-mt-btn">Detayları Gör</a></div>

                        </div>
                    </div>
                    <% } %>

                    <div class="col-md-4">

                        <table class="table table-hover table-condensed">
                            <thead>
                                <tr>
                                    <th style="width: 100px;">Firma Tekil Hit
                                    </th>
                                    <th style="width: 150px;">Firma Çoğul Hit
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <%:String.Format("{0:#,#}", store.SingularViewCount)%>
                            kez görüntülendi
                                    </td>
                                    <td>
                                        <%:String.Format("{0:#,#}", store.ViewCount)%>
                            kez görüntülendi
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        <div class="col-md-12">
                            <div class="alert alert-warning">
                                <span class="glyphicon glyphicon-question-sign"></span><b>Çoğul Hit ve Tekil Hit arasındaki
                    fark nedir? </b>
                                <br>
                                Firma profilinizi ziyaret eden kullanıcı aynı gün içerisinde profilinizi tekrar
                ziyaret ettiğinde çoğul hitiniz artmakta ancak tekil hitiniz değişmemektedir.
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
</div>
</div>

