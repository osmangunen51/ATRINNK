<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

  <style type="text/css">
    .style1
    {
    	 border: solid 1px #bababa;
      width: 86%;
    }
    .style7
    {
      border: solid 1px #bababa;
      width: 959px;
    }
    .style8
    {
      border: solid 1px #bababa;
      width: 153px;
    }
    .style10
    {
      border: solid 1px #bababa;
      width: 176px;
    }
    .style11
    {
      border: solid 1px #bababa;
      width: 172px;
    }
  </style>

  <div style=" height: auto;min-height: 294px;width: 784px;float: left;border: 1px solid #B1CEE1;margin-bottom: 20px; min-height:294px;">
  <div class="orta_baslik">Genel</div>
  <%if (AuthenticationUser.Membership.MemberType == 20)
    {  %>
       <%MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
      var storeview = entities.MemberStores.Where(c => c.MemberMainPartyId == AuthenticationUser.Membership.MainPartyId).SingleOrDefault().StoreMainPartyId;
      var store = entities.Stores.Where(c => c.MainPartyId == storeview).SingleOrDefault();
      var product = entities.Products.Where(c => c.MainPartyId == AuthenticationUser.Membership.MainPartyId).ToList();
      int singular = 0;
      int plural = 0;
      foreach (var count in product)
      {
        singular = count.SingularViewCount.ToInt32() + singular;
        plural = count.ViewCount.ToInt32() + plural;
      }
       %>
    <table class="style1" style="margin-left:10px;">
      <tr style="height:21px;">
        <td class="style7">
         </td>
        <td class="style8" align="center">
         <span style="color:#4C4C4C; font-family:Arial; font-size:11px;">İlan Sayısı </span></td>
         <td class="style11" align="center">
         <span style="color:#4C4C4C; font-family:Arial; font-size:11px;"> Tekil </span></td>
        <td class="style10" align="center">
         <span style="color:#4C4C4C; font-family:Arial; font-size:11px;"> Çoğul </span></td>
      </tr>
      <tr style="height:21px;">
        <td class="style7" >
          <span style="color:#4C4C4C; font-family:Arial; font-size:11px;">
 Firma Sayfa Görüntülenme</span></td>
 <td class="style8" align="center">
         <span style="color:#4C4C4C; font-family:Arial; font-size:11px;"> - </span></td>
        <td class="style11" style="color:#4C4C4C; font-family:Arial; font-size:11px;" align="center">
          <%:store.SingularViewCount%></td>
        <td class="style10" style="color:#4C4C4C; font-family:Arial; font-size:11px;" align="center">
              <%:store.ViewCount%></td>
      </tr>
      <tr style="height:21px;">
        <td class="style7" >
          <span style="color:#4C4C4C; font-family:Arial; font-size:11px;">
  Toplam Ürün Tıklama Sayısı</span></td>
  <td class="style8" style="color:#4C4C4C; font-family:Arial; font-size:11px;" align="center">
         <%=product.Count %></td>
        <td class="style11" style="color:#4C4C4C; font-family:Arial; font-size:11px;" align="center">
         <%=singular %></td>
        <td class="style10" style="color:#4C4C4C; font-family:Arial; font-size:11px;" align="center">
          <%=plural %></td>
      </tr>
    </table>
    <br />
    <%} %>
    
    <br />
    </div>
    
