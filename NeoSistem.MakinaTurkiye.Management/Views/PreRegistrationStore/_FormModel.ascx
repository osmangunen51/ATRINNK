<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Management.Models.PreRegistrations.PreRegistrainFormModel>" %>

<div style="float: left; width: 800px; margin-top: 10px;">
    <%:Html.HiddenFor(x=>x.Id) %>
    <table border="0" class="tableForm" cellpadding="5" cellspacing="0">
        <tr style="height: 40px;">
            <td colspan="3" align="right">
                <button type="submit" style="width: 70px; height: 35px;" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" aria-disabled="false">
                    <span class="ui-button-text">Kaydet
                    </span>
                </button>
                <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/PreRegistrationStore/'" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" aria-disabled="false">
                    <span class="ui-button-text">İptal
                    </span>
                </button>
                <br>
                <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px; margin-bottom: 10px;">
                </div>
            </td>
        </tr>

        <tr>
            <td valign="top">Firma Adı
            </td>
            <td valign="top">:
            </td>
            <td valign="top">
                <%= Html.TextBoxFor(model => model.StoreName, new { style = "height: 20px; width:250px;" })%>
                <%:Html.ValidationMessageFor(x=>x.StoreName) %>
            </td>
        </tr>

        <tr>
            <td valign="top">Email
            </td>
            <td valign="top">:
            </td>
            <td valign="top">
                <%= Html.TextBoxFor(model => model.Email)%>
                <%:Html.ValidationMessageFor(x=>x.Email) %>
            </td>
        </tr>
        <tr>
            <td>Yetkili Adı/Soyadı</td>
            <td>:</td>
            <td><%:Html.TextBoxFor(x=>x.MemberName) %>/

                <%:Html.TextBoxFor(x=>x.MemberSurname) %>
            </td>

        </tr>
        <tr>
            <td>Telefon Numarası</td>
            <td>:</td>
            <td><%:Html.TextBoxFor(x=>x.PhoneNumber) %></td>
        </tr>
               <tr>
            <td>Telefon Numarası 2</td>
            <td>:</td>
            <td><%:Html.TextBoxFor(x=>x.PhoneNumber2) %></td>
        </tr>
               <tr>
            <td>Telefon Numarası 3</td>
            <td>:</td>
            <td><%:Html.TextBoxFor(x=>x.PhoneNumber3) %></td>
        </tr>
                <tr>
            <td>Web Adresi:</td>
            <td>:</td>
            <td><%:Html.TextBoxFor(x=>x.WebUrl) %></td>
        </tr>
    </table>
</div>
