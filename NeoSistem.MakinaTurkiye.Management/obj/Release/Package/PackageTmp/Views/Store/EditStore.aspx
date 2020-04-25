<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<StoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Edit
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
  <script src="/Scripts/JQuery-dropdowncascading.js" type="text/javascript"></script>
  <%=Html.RenderHtmlPartial("Style") %>
  <script type="text/javascript">

      function DontAllowForAnotherTab() {
          $(".tabStore").each(function (index) {
         
              if ($(this).attr("data-a") == "BrowseDesc1")
                  $(this).removeAttr("href");
             
          });

          $(".tabLi").each(function (index) {
          if ($(this).attr("data-a") == "BrowseDesc1")
              $(this).attr("onClick", "alert('Yönetici Seçiniz.');")

        
           
          });
      }
      $(document).ready(function () {
       
          <%if (string.IsNullOrEmpty(Model.AuthName)) {
            %>
          DontAllowForAnotherTab();
          <%
      }%>
      $('#StorePacketBeginDate').datepicker();
      $('#StorePacketEndDate').datepicker();

      $('#BirthDate').datepicker();

              });

    function DeletePicture(AddressId) {
      $.ajax({
                      url: '/Store/DeletePicture',
        type: 'delete',
        data: { id: AddressId },
        success: function (data) {
          $('#divPictureList').html(data);
                      },
        error: function (x, l, e) {

                      }
                  });
              }

              function imageResize(imageName, width) {
                  if ($('#' + imageName)[0].width > width) {
        $('#' + imageName).attr('width', width);
                  }
              }
              var xTriggered = 0;
$("#StoreUniqueShortName").keyup(function (event) {
    $("#StoreUniqueShortName").length() < 3
    {
    }
              }).keydown(function (event) {
                  if (event.which == 13) {
        event.preventDefault();
    }
      });

 
  </script>
  <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginPanel())
    { %>
  <%=Html.RenderHtmlPartial("TabMenu") %>

    <%if (Request.QueryString["page"] != null) {%>
        <p style="color:#005b31; font-size:15px;">Bilgiler Güncellendi</p>
    <% } %>

  <% using (Html.BeginForm("EditStore", "Store", FormMethod.Post, new { enctype = "multipart/form-data" }))
     { %>
     <%MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
       var mainparty = entities.MemberStores.Where(c => c.StoreMainPartyId == Model.MainPartyId).FirstOrDefault().MemberMainPartyId;
       int mainpartyid = mainparty.ToInt32();
        %>
  <div style="float: left; margin: 20px 0px 0px 10px">
              <%if (string.IsNullOrEmpty(Model.AuthName)) {%>
            <p style="width:100%; padding:10px; background-color:#cc9d04; color:#fff; font-size:14px;">Bu firmanın <b>Portföy Yöneticisi</b> tanımlanmamıştır. Lütfen Portföy yöneticisi seçiniz. Yönetici seçmeden yaptığınız işlemler kayıt altına alınmayacaktır. </p>
        <% } %>
      <%if (TempData["ErrorAuth"] != null) {
         %>
      <div style="padding:10px; width:100%; border:1px solid #be0303; font-size:15px;"><%:TempData["ErrorAuth"] %></div>
          <%} %>
      <div style="float:right;">
          <button type="submit" style="width: 60px; height: 25px; margin-left:15">
          Kaydet
        </button>
             <table  border="0" cellpadding="5" cellspacing="0" style="margin-left: 20px;">
                <tr>
             <td>Tele Satış Sorumlusu</td>
             <td>:</td>
             <td>
            <%if (Model.GroupName == "Administrator" || Model.Users.Count>0 || string.IsNullOrEmpty(Model.AuthName)) {
                                  %>
                  <%:Html.DropDownListFor(m=>m.AuthorizedId,Model.Users) %>
                 <%  
                     }
                     else
                     {%>
           <%:Model.AuthName %>
                 <%:Html.HiddenFor(x=>x.AuthorizedId) %>
                     <%}%>
             </td>
         </tr>
            <tr>
             <td>Portföy Yöneticisi</td>
             <td>:</td>
             <td>
              <%if (Model.GroupName == "Administrator" || Model.Users.Count>0  || string.IsNullOrEmpty(Model.PortfoyUserName)) {
                   %>
                  <%:Html.DropDownListFor(m=>m.PortfoyUserId,Model.PortfoyUsers) %>
                 <%  
                }
                     else
                     {%>
           <%:Model.PortfoyUserName %>
                 <%:Html.HiddenFor(x=>x.PortfoyUserId) %>
                     <%}%>
             </td>
            </tr>
          </table>
      </div>
    <div style="width: 520px; float: left;">
        
      <table border="0" cellpadding="5" cellspacing="0" style="margin-left: 20px;">
                  <tr>
          <td style="width: 150px;">
            <%: Html.LabelFor(m => m.StoreNo)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%: Model.StoreNo%>
          </td>
          <td>
          </td>
        </tr>

                  <tr>
          <td style="width: 150px;">
            <%: Html.LabelFor(m => m.StoreName)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.TextBoxFor(m => m.StoreName, new { style = "width: 250px;" })%>
          </td>
          <td>
            <%: Html.ValidationMessageFor(m => m.StoreName)%>
          </td>
        </tr>
                            <tr>
          <td>
           Firma Kısa Adı
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.TextBoxFor(m => m.StoreShortName, new { style = "width: 250px;" })%>
          </td>
          <td>
          </td>
        </tr>
                <tr>
          <td>
            Üyelik Paketi :
          </td>
          <td>
            :
          </td>
          <td>
            <%:Html.DropDownListFor(m => m.PacketId, Model.PacketItems)%>
          </td>
          <td>
          </td>
        </tr>
        <tr>
          <td>
            <%: Html.LabelFor(m => m.StorePacketEndDate)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.TextBox("StorePacketEndDate", Model.StorePacketEndDate.HasValue ? Model.StorePacketEndDate.Value.ToString("dd.MM.yyyy") : "", new { style = "width: 94px;" })%>
          </td>
          <td>
          </td>
        </tr>

        <tr>
          <td style="width: 150px;">
          Tekil Görüntülenme
          </td>
          <td>
            :
          </td>
          <td>
            <%: Model.ViewCount%>
          </td>
          <td>
          </td>
        </tr>
          <%if (Model.WhatsappClickCount != 0) {%>
                      <tr>
              <td>Whatsapp Tıklanma</td>
              <td>:</td>
              <td>
              <%:Model.WhatsappClickCount %>
                  </td>
          </tr>
          <% } %>

        <tr>
          <td style="width: 150px;">
           Görüntülenme
          </td>
          <td>
            :
          </td>
          <td>
            <%: Model.SingularViewCount%>
          </td>
          <td>
          </td>
        </tr>

   <%--      <tr>
          <td>
            <%: Html.LabelFor(m => m.StoreUniqueShortName)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.TextBoxFor(m => m.StoreUniqueShortName, new { style = "width: 250px;" })%>
          </td>
          <td>
          </td>
        </tr>--%>

                            <tr>
          <td>
            Firma Url
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.TextBoxFor(m => m.StoreUrlName, new { style = "width: 250px;" })%>
          </td>
          <td>
          </td>
        </tr>
          <tr>
              <td><%:Html.LabelFor(x=>x.StoreWeb) %></td>
              <td>:</td>
              <td>
              <%:Html.TextBoxFor(x=>x.StoreWeb) %></td>
          </tr>
 
          <%if (!string.IsNullOrEmpty(Model.StoreWeb)) {%>      
          <tr>
          <td>
          </td>
          <td>
            :
          </td>
          <td>

            <% 
                if (!Model.StoreWeb.Contains("http")) { Model.StoreWeb = "http://" + Model.StoreWeb; } %>
            <a style="color: Blue;" href="<%:Model.StoreWeb %>" target="_blank">
              <%:Model.StoreWeb %></a> 
          </td>
          <td>
          </td>
        </tr>
          <% } %>

<%--        <tr>
          <td>
            <%: Html.LabelFor(m => m.StoreEMail)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.TextBoxFor(m => m.StoreEMail, new { style = "width: 250px;" })%>
          </td>
          <td>
            <%: Html.ValidationMessageFor(m => m.StoreEMail)%>
          </td>
        </tr>--%>
        <tr>
          <td>
            <%: Html.LabelFor(m => m.StoreCapital)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%:Html.DropDownListFor(m => m.StoreCapital, Model.StoreCapitalItems)%>
          </td>
          <td>
          </td>
        </tr>
        <tr>
          <td>
            <%: Html.LabelFor(m => m.StoreEstablishmentDate)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.TextBoxFor(m => m.StoreEstablishmentDate, new { style = "width: 94px;" })%>
          </td>
          <td>
          </td>
        </tr>
        <tr>
          <td>
            <%: Html.LabelFor(m => m.StoreType)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%:Html.DropDownListFor(m => m.StoreType, Model.StoreTypeItems, new { style = "width: 170px; "})%>
          </td>
          <td>
          </td>
        </tr>
        <tr>
          <td>
            <%: Html.LabelFor(m => m.StoreEmployeesCount)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.DropDownListFor(c => c.StoreEmployeesCount, Model.EmployeesCountItems, new { style = "width: 170px; " })%>
          </td>
          <td>
          </td>
        </tr>
        <tr>
          <td>
            <%: Html.LabelFor(m => m.StoreEndorsement)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%:Html.DropDownListFor(m => m.StoreEndorsement, Model.StoreEndorsementItems, new { style = "width: 170px; " })%>
          </td>
          <td align="left">
          </td>
        </tr>
        <tr>
          <td>
            <%: Html.LabelFor(m => m.StorePacketBeginDate)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.TextBox("StorePacketBeginDate", Model.StorePacketBeginDate.HasValue ? Convert.ToDateTime(Model.StorePacketBeginDate).ToString("dd.MM.yyyy") : "", new { style = "width: 94px;" })%>
          </td>
          <td>
          </td>
        </tr>

        <tr>
          <td>
            <%: Html.LabelFor(m => m.StoreRecordDate)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%:Model.StoreRecordDate.ToString("dd.MM.yyyy")%>
          </td>
          <td>
            <%: Html.ValidationMessageFor(m => m.StoreActiveType)%>
          </td>
        </tr>
        <tr>
          <td>
            <%: Html.LabelFor(m => m.PurchasingDepartmentName)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.TextBoxFor(m => m.PurchasingDepartmentName)%>
          </td>
          <td>
          </td>
        </tr>
        <tr>
          <td>
            <%: Html.LabelFor(m => m.PurchasingDepartmentEmail)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%: Html.TextBoxFor(m => m.PurchasingDepartmentEmail)%>
          </td>
          <td>
          </td>
        </tr>
          <tr>
              <td><%:Html.LabelFor(x=>x.TaxOffice) %>/<%:Html.LabelFor(x=>x.TaxNumber) %></td>
              <td>:</td>
              <td><%:Html.TextBoxFor(m=>m.TaxOffice) %>/<%:Html.TextBoxFor(m=>m.TaxNumber) %></td>
          </tr>
        <tr>
     
                <td><%:Html.LabelFor(x=>x.MersisNo) %>/<%:Html.LabelFor(x=>x.TradeRegistrNo) %></td>
                <td>:</td>
                <td><%:Html.TextBoxFor(m=>m.MersisNo) %>/<%:Html.TextBoxFor(m=>m.TradeRegistrNo) %></td>
     
          <td valign="top">
            <%: Html.LabelFor(m => m.StoreAbout)%>
          </td>
          <td valign="top">
            :
          </td>
          <td>
            <%: Html.TextAreaFor(m => m.StoreAbout, new { style = "width:350px; height : 60px;" })%>
          </td>
          <td>
          </td>
        </tr>
  
        <tr>
          <td>
            <%: Html.LabelFor(m => m.ReadyForSale)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%:Html.CheckBoxFor(m => m.ReadyForSale, Model.ReadyForSale)%>
          </td>
          <td>
          </td>
        </tr>
        <tr>
          <td>
            <%: Html.LabelFor(m => m.StoreShowcase)%>
          </td>
          <td>
            :
          </td>
          <td>
            <%:Html.CheckBoxFor(m => m.StoreShowcase, Model.StoreShowcase)%>
          </td>
          <td>
          </td>
        </tr>
          <tr>
              <td>Seo Title</td>
              <td>:</td>
              <td><%:Html.TextBoxFor(x=>x.SeoTitle) %></td>
          </tr>
                <tr>
              <td>Seo Keyword</td>
              <td>:</td>
              <td><%:Html.TextBoxFor(x=>x.SeoKeyword) %></td>
          </tr>
          <tr>
              <td>Seo Description</td>
              <td>:</td>
              <td><%:Html.TextAreaFor(x=>x.SeoDescription) %></td>
          </tr>
        <tr>

          <td>
            <%: Html.LabelFor(m => m.PacketStatu)%>
          </td>
          <td>
            :
          </td>
          <td>
            <% 
       bool inceleniyor = false;
       bool onaylandi = false;
       bool onaylanmadi = false;
       bool silindi = false;
       byte StoreActiveType = Model.StoreActiveType.Value;
       if (StoreActiveType == 1)
       {
         inceleniyor = true;
       }
       else if (StoreActiveType == 2)
       {
         onaylandi = true;
       }
       else if (StoreActiveType == 3)
       {
         onaylanmadi = true;
       }
       else if (StoreActiveType == 4)
       {
         silindi = true;
       }
            %>
            İnceleniyor&nbsp;<%: Html.RadioButton("StoreActiveType", "1", inceleniyor)%>&nbsp;&nbsp;
            Onaylandı&nbsp;<%: Html.RadioButton("StoreActiveType", "2", onaylandi)%>&nbsp;&nbsp;
            <span style="width:30px;" onmouseup="tıkla()">
            Onaylanmadı&nbsp;<%: Html.RadioButton("StoreActiveType", "3", onaylanmadi)%>&nbsp;&nbsp;
            </span>
            Silindi&nbsp;<%: Html.RadioButton("StoreActiveType", "4", silindi)%>&nbsp;&nbsp;
          </td>
          <td>
            <%: Html.ValidationMessageFor(m => m.PacketStatu)%>
          </td>
        </tr>
          <tr>
              <td>Ödeme Sayfası Ekleyebilsin </td>
              <td>:</td>
              <td><%:Html.CheckBox("IsAllowProductSellUrl", Model.IsAllowProductSellUrl.HasValue?Model.IsAllowProductSellUrl.Value : false) %></td>
          </tr>
        <tr>
          <td colspan="3">
            <div style="margin: 20px auto; padding-top: 10px; border-top: solid 1px #DDD; width: 99%;
              float: left">
              <button type="submit">
                Kaydet</button>
              <button type="reset">
                İptal</button>
            </div>
          </td>
        </tr>
      </table>
    </div>
    <div style="float: left; width: 400px; height: auto; margin-top: 40px;">
      <table>
        <tr>
          <td valign="top" style="width: 150px;">
            <%: Html.LabelFor(m => m.StoreLogo)%>
          </td>
          <td valign="top">
            :
          </td>
          <td colspan="2">
            <%: Html.FileUploadFor(m => m.StoreLogo, new { @class = "FileUpload", style = "width: 256px" })%><br />
            <br />
            <% if (!String.IsNullOrEmpty(Model.StoreLogo))
               {
                 string logoThumb = "//s.makinaturkiye.com/Store/" + Model.MainPartyId + "/" + Model.StoreLogo;  %>
	               <img src="<%:logoThumb %>" style="border: solid 1px #b6b6b6;width:250px;"  />
            <% }
               else
               {

               } %>

            <div id="deleteImage" style="margin-top: 5px">
              <% Html.RenderPartial("ImageDelete",
               new ControlModel { ImageDeleted = false, Text = "", IsImage = Model.IsImage }); %>
            </div>
          </td>
        </tr>
         <tr>
          <td valign="top" style="width: 150px;">
            Hızlı Giriş Link
          </td>
          <td valign="top">
            :
          </td>
          <td colspan="2">
            
            <% string loginLink = ViewData["LoginId"].ToString();
                %>
              <%:Html.TextBox("loginLink", loginLink, new {@style="width:400px;" })%>

            
          </td>
   
        </tr>
         <tr>
          <td valign="top" style="width: 150px;">
            Ödeme Sayfası Linki
          </td>
          <td valign="top">
            :
          </td>
   
       <td colspan="1">
              <%:Html.TextBox("loginLink", loginLink+"&returnUrl=/MemberShipSales/PayWithCreditCard", new {@style="width:400px;" })%>
          </td>
        </tr>
        <tr>
          <td valign="top" style="width: 150px;">
          <%:Ajax.ActionLink("Ürün İstatistiklerini gör.", "Statistic", new { id = mainpartyid }, new AjaxOptions() { UpdateTargetId = "statisticproduct", HttpMethod = "Post" })%>
           
          </td>
          <td valign="top">
            :
          </td>
          <td colspan="2">
            <div id="statisticproduct" style="margin-top: 5px">

            </div>
          </td>
        </tr>

      </table>
    </div>
   
  </div>
  <% } %>
  <%} %>
</asp:Content>
