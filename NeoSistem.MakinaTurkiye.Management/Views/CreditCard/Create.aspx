<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<CreditCardViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript">
    function SetVirtualPostInstallment(value, count) {
      $.ajax({
        url: '/CreditCard/SetVirtualPostInstallment',
        data: { value: value, count: count },
        cache: false,
        success: function (data) { $('#divVirtualPostInstallment').html(data) },
        error: function (x) { alert(x.responseText) }
      });
    }

    function deleteInstallment(value) {
      if (confirm("Taksit işlemini silmek istediğinizden eminmisiniz ?")) {
        $.ajax({
          url: '/CreditCard/DeleteVirtualPostInstallment',
          data: { value: value },
          cache: false,
          success: function (data) { $('#divVirtualPostInstallment').html(data) },
          error: function (x) { alert(x.responseText) }
        });
      }
    }
    
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginForm("Create", "CreditCard", FormMethod.Post))
    {%>
  <div style="float: left; width: 64%; padding-top: 10px; padding-left: 10px;">
    <button type="submit" style="width: 70px; height: 35px;">
      Kaydet
    </button>
    <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/CreditCard/Index'">
      İptal
    </button>
    <br />
    <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
      margin-bottom: 10px;">
    </div>
  </div>
  <div style="float: left;">
    <div style="float: left;">
      <table class="tableForm" style="padding-top: 10px; height: auto;">
        <tr>
          <td style="width: 120px;">
            Kredi Kartı Tanımı :
          </td>
          <td>
          
            <%: Html.TextBoxFor(model => model.CreditCard.CreditCardName, new { style = "width:300px" })%>
          </td>
        </tr>
               <tr>
          <td>
              Durum:
          </td>
          <td><%:Html.CheckBox("Active") %></td>
              </tr>
        <tr>
          <td style="width: 120px;">
            Sanal Pos :
          </td>
          <td>
            <%: Html.DropDownListFor(c => c.CreditCard.VirtualPosId, Model.VirtualPosItems)%>
          </td>
        </tr>
      </table>
    </div>
    <div style="float: left;">
      <div style="float: left; margin-top: 13px; float: left; width: 500px; margin-left: 20px;
        border-left: dashed 1px #bababa; padding-left: 20px; min-height: 70px;">
        <div style="float: left; width: 100%; padding-bottom: 10px; border-bottom: dashed 1px #bababa">
          Taksit ve Vade Farkları
        </div>
        <div style="float: left; margin-top: 10px; width: 100%;">
          <div style="float: left;">
            Taksit Sayısı:&nbsp;&nbsp;
            <input type="text" style="width: 40px;" id="txtInstallmentCount" />
          </div>
          <div style="float: left; margin-left: 5px;">
            Vade:&nbsp;&nbsp;
            <input type="text" style="width: 70px;" id="txtInstallment" />
            <button type="button" onclick="SetVirtualPostInstallment($('#txtInstallment').val(), $('#txtInstallmentCount').val());">
              Taksit Ekle
            </button>
          </div>
        </div>
        <div style="float: left; width: 349px;" id="divVirtualPostInstallment">
          <%=Html.RenderHtmlPartial("CreditCardInstallment", Model.CreditCardInstallmentItems)%>
        </div>
      </div>
    </div>
  </div>
  <div style="float: left; width: 100%; padding-left: 10px;">
    <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
      margin-bottom: 10px;">
    </div>
    <br />
    <button type="submit" style="width: 70px; height: 35px;">
      Kaydet
    </button>
    <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/CreditCard/Index'">
      İptal
    </button>
  </div>
  <%} %>
</asp:Content>
