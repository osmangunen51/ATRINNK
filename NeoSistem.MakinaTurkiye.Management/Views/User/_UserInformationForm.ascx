﻿<%@ Control Language="C#"   Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Management.Models.Users.UserInformationModel>" %>
    <table class="tableForm" style="padding: 10px; height: auto;">

        <tr>
            <td style="width: 120px;">
              İsim Soyisim
        :
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.NameSurname,new { style ="width:300px" }) %>
                <%: Html.ValidationMessageFor(model => model.NameSurname)%>
            </td>
        </tr>
        <tr>
            <td>
              Tc Kimlik No
        :
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.IdentityNumber, new { style = "width:300px" })%>
             
            </td>
        </tr>
        <tr>
            <td>Telefon Numarası
            </td>
            <td>
              <%: Html.TextBoxFor(model => model.PhoneNumber, new { style = "width:300px" })%>
            </td>
        </tr>
                <tr>
            <td>2. Telefon Numarası
            </td>
            <td>
              <%: Html.TextBoxFor(model => model.SecondPhoneNumber, new { style = "width:300px" })%>
            </td>
        </tr>
                        <tr>
            <td>Banka Hesap Numarası
            </td>
            <td>
              <%: Html.TextBoxFor(model => model.BankAccountNumber, new { style = "width:300px" })%>
            </td>
        </tr>
        <tr>
            <td>
                İşe Giriş  Tarihi
              :
            </td>
            <td>
                <%:Html.TextBoxFor(x=>x.StartWorkDate, new {@class="date-pick" }) %>
            </td>

        </tr>
                <tr>
            <td>
                İşten Çıkış Tarihi
              :
            </td>
            <td>
                <%:Html.TextBoxFor(x=>x.EndDate, new {@class="date-pick" }) %>
            </td>

        </tr>
        <tr>
            <td>Açık Adres:</td>
            <td><%:Html.TextAreaFor(x=>x.Adress) %></td>
        </tr>
        <tr>
            <td>Doğum Tarihi:</td>
            <td><%:Html.TextBoxFor(x=>x.BirthDate, new {@class="date-pick" }) %></td>
        </tr>
        <tr>
            <td>Cinsiyet:</td>
            <td><%:Html.RadioButtonFor(x=>x.Gender,"1")%>Erkek&nbsp; <%:Html.RadioButtonFor(x=>x.Gender,"0")%>Kadın </td>
        </tr>
             <tr>
            <td>Evli mi ?</td>
            <td><%:Html.CheckBoxFor(x=>x.MarialStatus)%></td>
        </tr>
        <tr>
            <td>Evli ise kaç çocuğu var?</td>
            <td><%:Html.TextBoxFor(x=>x.NumberOfChildren) %></td>
        </tr>
        <tr>
            <td>Ehliyet:</td>
            <td><%:Html.CheckBoxFor(x=>x.DriverLicense) %></td>
        </tr>
               <tr>
            <td>Eğitim:</td>
            <td><%:Html.TextAreaFor(x=>x.Education) %></td>
        </tr>
    </table>