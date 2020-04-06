<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.OrderModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
 <%:Model.StoreName %> Fatura Kesim İşlemleri
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


  <div style="float: left; width: 800px; margin-top: 10px; margin-left:10px;">
      <%using(Html.BeginForm()) {%>
      <%:Html.HiddenFor(o=>o.MainPartyId) %>
   <table border="0" cellpadding="0" cellspacing="0" width="1000px" style="float: left;">
            <tr>
                <td>
                    Firma Adı
                </td>
                <td>:</td>
                <td>
                   <%:Html.TextBoxFor(x => x.StoreName, new {@disabled="true" })%>
                   
                </td>
            </tr>
       <tr>
           <td>
               Hizmet Açıklama
           </td>
           <td>:</td>
           <td>
              <%:Html.TextAreaFor(o=>o.OrderDescription) %>
           </td>

       </tr>
       <tr>
           <td>Vergi Dairesi</td>
           <td>:</td>
           <td><%:Html.TextBoxFor(o=>o.TaxOffice) %></td>
       </tr>
      <tr>
           <td>Vergi No</td>
           <td>:</td>
           <td><%:Html.TextBoxFor(o=>o.TaxNo) %></td>
       </tr>
       <tr>
           <td>Kdv Dahil Fiyat
           </td>
            <td>:</td>
           <td><%:Html.TextBoxFor(o=>o.OrderPrice) %></td>
       </tr>
       <tr>
           <td>Adres</td>
           <td>:</td>
           <td><%:Html.TextAreaFor(o=>o.Address) %></td>
       </tr>
       <tr>
          <td></td>
           <td></td>
           <td><input type="submit" value="Ekle" /></td>
       </tr>
    </table>
      <%} %>
  </div>
</asp:Content>

