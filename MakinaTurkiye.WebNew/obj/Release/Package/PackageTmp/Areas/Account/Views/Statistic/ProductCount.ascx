<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>


   <div class="hesabim_orta_icerik">
  <div class="orta_baslik">Toplam İlan Görüntülenme</div>
   
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
   <span style="color:#4C4C4C; font-family:Arial; font-size:11px;">Toplam İlan : </span> <span style="color:#4C4C4C; font-family:Arial; font-size:11px;"> <%=product.Count %> </span><br />
  <span style="color:#4C4C4C; font-family:Arial; font-size:11px;"> Toplam Tekil Tıklanma : </span> <span style="color:#4C4C4C; font-family:Arial; font-size:11px;"> <%=singular %> </span><br />
  <span style="color:#4C4C4C; font-family:Arial; font-size:11px;"> Toplam Çoğul Tıklama  :  </span><span style="color:#4C4C4C; font-family:Arial; font-size:11px;"> <%=plural %> </span>
    </div>
