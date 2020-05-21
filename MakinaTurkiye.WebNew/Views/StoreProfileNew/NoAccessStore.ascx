<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTNoAccessStoreModel>"%>

          <div class="row">
        <div class="col-xs-12">
            <h3>
                <span class="text-warning glyphicon glyphicon-warning-sign"></span>&nbsp; Firma Yayında Değil
            </h3>
            <div class="col-md-3">
                <img style="border:1px solid #ccc;" src="<%:Model.StoreLogoPath %>" class="img-responsive" />
                <hr />
                <h4><%:Model.StoreName%></h4>
            </div>
            <div class="col-md-9">
            <div class="well" style="background-color:#fff; text-align:center;">
                Sayfa kaldırılmış veya adres eksik girilmiş olabilir.
                <br>
                Lütfen adresi doğru yazdığınızdan emin olun.
                <br>
                Eğer Sık Kullanılanlar listesinden bu sayfaya yönlendirildiyseniz, <a href="<%:AppSettings.SiteUrl %>">Makina
                    Türkiye Anasayfa </a>veya <a href="/sirketler" style="font-size:14px">Mağazalar</a> linklerini kullanarak
                ulaşmaya çalışın.
                <br>
                Eğer bunun teknik bir sorun olduğunu düşünüyorsanız, sayfayı <a href="mailto:destek@makinaturkiye.com">Destek Masası
                </a>'na bildirin.
                <br>
            </div>
                </div>
        </div>
      </div>