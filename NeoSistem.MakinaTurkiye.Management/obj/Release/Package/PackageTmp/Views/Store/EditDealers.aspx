﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StoreModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  EditDealers
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <%=Html.RenderHtmlPartial("Style") %>
  <script src="/Scripts/MakinaTurkiye.js" type="text/javascript" defer="defer"></script>
  <script type="text/javascript">

    function DeleteAddress(Id, typeId) {
      if (confirm('Adresi ve adrese ait telefonları silmek istediğinizden eminmisiniz ?')) {
        $.ajax({
          url: '/Store/DeleteAddress',
          type: 'post',
          data: { AddressId: Id, type: typeId, storeId: $('#storeId').val() },
          success: function (data) {
            alert('Silmek istediğiniz adres başarıyla silinmiştir.');
            $('#divAddressItems').html(data);
          },
          error: function (x, l, e) {
            alert(x.responseText);
          }
        });
      }
    }

    $(document).ready(function () {


      $('#TextInstitutionalPhoneAreaCode').change(function () {
        $('#InstitutionalPhoneAreaCode').val($(this).val());
      });
      $('#DropDownInstitutionalPhoneAreaCode').change(function () {
        $('#InstitutionalPhoneAreaCode').val($(this).val());
      });

      $('#TextInstitutionalPhoneAreaCode2').change(function () {
        $('#InstitutionalPhoneAreaCode2').val($(this).val());
      });
      $('#DropDownInstitutionalPhoneAreaCode2').change(function () {
        $('#InstitutionalPhoneAreaCode2').val($(this).val());
      });

      $('#TextInstitutionalFaxAreaCode').change(function () {
        $('#InstitutionalFaxAreaCode').val($(this).val());
      });
      $('#DropDownInstitutionalFaxAreaCode').change(function () {
        $('#InstitutionalFaxAreaCode').val($(this).val());
      });

      $('#CountryId').change(function () {

        $.ajax({
          url: '/Store/Cities',
          data: { id: $(this).val() },
          success: function (msg) {
            $('#CityId' + " > option").remove();
            $.each(msg, function (i) {
              $('#CityId').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
            });
            $('#CityId').attr("disabled", "");
            $('#imgLoader').css("visibility", "hidden");
          },
          error: function (e) {
            alert(e.responseText);
          }
        });
      });

      $('#LocalityId').change(function () {

        $.ajax({
          url: '/Store/Towns',
          data: { id: $(this).val() },
          success: function (msg) {
            $('#TownId' + " > option").remove();
            $.each(msg, function (i) {
              $('#TownId').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
            });
            $('#TownId').attr("disabled", "");
            $('#imgLoader').css("visibility", "hidden");
          },
          error: function (e) {
            alert(e.responseText);
          }
        });
        $('#TownId').html('<option value="0">< Lütfen Seçiniz ></option>');

      });

      $('#CityId').change(function () {

        $.ajax({
          url: '/Store/Localities',
          data: { id: $(this).val() },
          success: function (msg) {
            $('#LocalityId' + " > option").remove();
            $.each(msg, function (i) {
              $('#LocalityId').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
            });
            $('#LocalityId').attr("disabled", "");
            $('#imgLoader').css("visibility", "hidden");
          },
          error: function (e) {
            alert(e.responseText);
          }
        });

        $.ajax({
          url: '/Membership/AreaCode',
          data: { CityId: $('#CityId').val() },
          success: function (data) {
            $('#InstitutionalPhoneAreaCode').val(data);
            $('#TextInstitutionalPhoneAreaCode').val(data);

            $('#InstitutionalPhoneAreaCode2').val(data);
            $('#TextInstitutionalPhoneAreaCode2').val(data);

            $('#InstitutionalFaxAreaCode').val(data);
            $('#TextInstitutionalFaxAreaCode').val(data);

            if ($('#CityId').val() == "34") {
              $('#InstitutionalPhoneAreaCode').val('212');
              $('#InstitutionalPhoneAreaCode2').val('212');
              $('#InstitutionalFaxAreaCode').val('212');
            }

          }
        });

        $('#TownId').html('<option value="0">< Lütfen Seçiniz ></option>');

        if ($('#CityId').val() != "0") {
          if ($('#CityId').val() == "34") {
            $('#AreaCodeTextBox').hide();
            $('#AreaCodeSelect').show();
            $('#AreaCodeTextBox2').hide();
            $('#AreaCodeSelect2').show();
            $('#AreaCodeTextBox3').hide();
            $('#AreaCodeSelect3').show();

            $('#InstitutionalPhoneAreaCode').val('212');
            $('#InstitutionalPhoneAreaCode2').val('212');
            $('#InstitutionalFaxAreaCode').val('212');
          }
          else {
            $('#AreaCodeTextBox').show();
            $('#AreaCodeSelect').hide();
            $('#AreaCodeTextBox2').show();
            $('#AreaCodeSelect2').hide();
            $('#AreaCodeTextBox3').show();
            $('#AreaCodeSelect3').hide();
          }
        }
        else {
          $('#AreaCodeTextBox').show();
          $('#AreaCodeSelect').hide();
          $('#AreaCodeTextBox2').show();
          $('#AreaCodeSelect2').hide();
          $('#AreaCodeTextBox3').show();
          $('#AreaCodeSelect3').hide();
        }

      });

      $('#InstitutionalGSMNumber').keyup(function () {
        if ($(this).val() != "") {
          $('#trGsmType').show()
        }
        else {
          $('#trGsmType').hide()
        }
      });

      $('#CountryId').change(function () {

        if ($(this).val() > 0) {
          if ($(this).val() == 246) {
            $('#divGsm').show();
            $('#trOperator').show();
          }
          else {
            $('#divGsm').hide();
            $('#trOperator').hide();
          }
        }

        if ($(this).val() > 0 && $(this).val() != 246) {
          $('#InstitutionalPhoneAreaCode').val('');
          $('#InstitutionalPhoneAreaCode2').val('');
          $('#InstitutionalFaxAreaCode').val('');

          $('#TextInstitutionalPhoneAreaCode').val('');
          $('#TextInstitutionalPhoneAreaCode2').val('');
          $('#TextInstitutionalFaxAreaCode').val('');
        }

        $.ajax({
          url: '/Store/CultureCode',
          type: 'post',
          data: { CountryId: $('#CountryId').val() },
          success: function (data) {
            $('#InstitutionalPhoneCulture').val(data);
            $('#InstitutionalPhoneCulture2').val(data);

            $('#InstitutionalGSMCulture').val(data);
            $('#InstitutionalGSMCulture2').val(data);

            $('#InstitutionalFaxCulture').val(data);
          }
        });

        $('#TownId').html('<option value="0">< Lütfen Seçiniz ></option>');
        $('#LocalityId').html('<option value="0">< Lütfen Seçiniz ></option>');
      });

      $('#TownId').change(function () {

        $.ajax({
          url: '/Store/ZipCode',
          data: { TownId: $(this).val() },
          success: function (data) {
            $('#ZipCode').val(data);
          }

        });

      });

    });

    onload = function () {
      $.ajax({
        url: '/Store/CultureCode',
        data: { CountryId: $('#CountryId').val() },
        success: function (data) {
          $('#InstitutionalPhoneCulture').val(data);
          $('#InstitutionalPhoneCulture2').val(data);

          $('#InstitutionalGSMCulture').val(data);
          $('#InstitutionalGSMCulture2').val(data);

          $('#InstitutionalFaxCulture').val(data);
        }
      });

      if ($('#CityId').val() != "0") {
        if ($('#CityId').val() == "34") {
          $('#AreaCodeTextBox').hide();
          $('#AreaCodeSelect').show();
          $('#AreaCodeTextBox2').hide();
          $('#AreaCodeSelect2').show();
          $('#AreaCodeTextBox3').hide();
          $('#AreaCodeSelect3').show();

          $('#InstitutionalPhoneAreaCode').val('212');
          $('#InstitutionalPhoneAreaCode2').val('212');
          $('#InstitutionalFaxAreaCode').val('212');

        }
        else {
          $('#AreaCodeTextBox').show();
          $('#AreaCodeSelect').hide();
          $('#AreaCodeTextBox2').show();
          $('#AreaCodeSelect2').hide();
          $('#AreaCodeTextBox3').show();
          $('#AreaCodeSelect3').hide();
        }
      }
      else {
        $('#AreaCodeTextBox').show();
        $('#AreaCodeSelect').hide();
        $('#AreaCodeTextBox2').show();
        $('#AreaCodeSelect2').hide();
        $('#AreaCodeTextBox3').show();
        $('#AreaCodeSelect3').hide();
      }

    }

  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <input type="hidden" id="storeId" name="storeId" value="<%:this.RouteData.Values["id"] %>" />
  <%
    DealerType pageType = (DealerType)Request.QueryString["DealerType"].ToByte();
    string uControlName = string.Empty;
  %>
  <%switch (pageType)
    {
      case DealerType.Bayii:
        uControlName = "EditDealer";
        break;
      case DealerType.YetkiliServis:
        uControlName = "EditService";
        break;
      case DealerType.Sube:
        uControlName = "EditBranch";
        break;
      default:
        break;
    } %>
  <%using (Html.BeginPanel())
    { %>
  <%=Html.RenderHtmlPartial("TabMenu") %>
  <%using (Html.BeginForm("EditDealers", "Store", FormMethod.Post))
    {%>
  <div style="width: 900px; float: left; margin: 20px 0px 0px 20px">
    <input id="DealerTypeId" name="DealerTypeId" value="<%:Request.QueryString["DealerType"].ToByte() %>"
      type="hidden" />
    <%=Html.RenderHtmlPartial(uControlName) %></div>
  <%} %>
  <% } %>
</asp:Content>
