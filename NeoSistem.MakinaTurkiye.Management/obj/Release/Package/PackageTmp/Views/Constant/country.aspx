<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.ViewModel.CountryViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Sabit Alanlar Güncelleme
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
    <script type="text/javascript">
        function DeletePost(countryId) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Constant/DeleteCountry',
                    data: { id: countryId },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        var e = data;
                        if (e) {
                            $('#row' + countryId).hide();
                        }
                        else {
                            alert('Bu sabit kullanılıyor.Silme işlemi başarısız.');
                        }
                    }
                });
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


  <div style="float: left; width: 800px; margin-top: 10px;">
    <table border="0" cellpadding="0" cellspacing="0" width="900px" style="float: left">
        <tr>
            <td></td><td><b style="color:#038f20"><%if(ViewData["exist"]=="true"){ %>
    Böyle bir ülke zaten kayıtlı.
    <%} %>
    <%if(ViewData["success"]=="true"){ %>
            İşleminiz Gerçekleşmiştir.
    <%} %></b></td>
        </tr>
        <%using(Html.BeginForm("Country","Constant",FormMethod.Post)){ %>
        <tr>
            <td>Ülke ID</td>
            <td><%:Html.TextBoxFor(x => x.Id, new {@required=true })%></td>
        </tr>
        <tr>
            <td>Ülke Adı</td>
            <td><%:Html.TextBoxFor(x=>x.CountryName,new {@required=true }) %></td>
        </tr> 
        <%:Model.CountryName %>
        <tr>
            <td>Telefon Kodu</td>
            <td><%:Html.TextBoxFor(x=>x.CultureCode,new {@required=true })%></td>
        </tr>
        <tr>
            <td>Aktif</td>
            <td><%:Html.CheckBoxFor(x=>x.Active) %>
                <input type="hidden" value="<%:ViewData["update"] %>" name="update" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td> <button type="submit" title="Ekle">
                <%if( ViewData["update"]!=null){%>
                Kaydet
                <%}else{ %>
                    Ekle<%} %>

                 </button></td>
        </tr>

        <%} %>
    </table> 

      <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
          <tr><td>Ülke ID</td>
              <td>Ülke Adı</td>
              <td>Telefon Kodu</td>
              <td>Durum</td>
              <td></td>
          </tr>
          </thead>
        <tbody>
              <%= Html.RenderHtmlPartial("CountryList",Model.CountryList)%>
        </tbody>
        </table>
          </div>
  </div>
</asp:Content>

