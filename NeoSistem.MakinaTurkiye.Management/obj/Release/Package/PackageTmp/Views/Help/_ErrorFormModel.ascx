﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Management.Models.WebSiteErrorCreateModel>" %>
<div style="float: left; width: 800px; margin-top: 10px;">
    <div style="padding: 5px; font-size: 16px;">
        <b>Not:</b> Lütfen karşılaştığınız sorunu detaylı bir şekilde gerekirse resim koyarak paylaşınız.
    </div>
    <%:Html.HiddenFor(x=>x.UserId) %>
    <%:Html.HiddenFor(x=>x.WebSiteErrorId) %>
    <%:Html.HiddenFor(x=>x.IsAdvice)%>
    <table border="0" class="tableForm" cellpadding="5" cellspacing="0">
        <tr style="height: 40px;">
            <td colspan="3" align="right">
                <button type="submit" style="width: 70px; height: 35px;" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" aria-disabled="false">
                    <span class="ui-button-text">Kaydet
                    </span>
                </button>
                <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/Help/Index'" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" aria-disabled="false">
                    <span class="ui-button-text">İptal
                    </span>
                </button>
                <br>
                <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px; margin-bottom: 10px;">
                </div>
            </td>
        </tr>
        <tr>
            <td>Bölümünüz</td>
            <td>:</td>
            <td><%:Html.DropDownListFor(x=>x.ProblemType, Model.ErrorTypes) %>
                <%:Html.ValidationMessageFor(x=>x.ProblemType) %>    
            </td>
        </tr>
        <tr>
            <td valign="top">Dosya
            </td>
            <td valign="top">:
            </td>
            <td valign="top">
                <input type="file" name="file" />
            </td>
        </tr>
        <tr>
            <td valign="top">Başlık*
            </td>
            <td valign="top">:
            </td>
            <td valign="top">
                <%= Html.TextBoxFor(model => model.Title, new { style = "height: 20px; width:250px;" })%>
                <%:Html.ValidationMessageFor(x=>x.Title) %>
            </td>
        </tr>

        <tr>
            <td valign="top">İçerik*
            </td>
            <td valign="top">:
            </td>
            <td valign="top">
                <%= Html.TextAreaFor(model => model.Content, new { style = "height: 150px; width:400px;" })%>
                <%:Html.ValidationMessageFor(x=>x.Content) %>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td><%:Html.CheckBoxFor(x=>x.IsSolved) %> Sorun Çözüldü</td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td><%:Html.CheckBoxFor(x=>x.IsFirst) %> Öncelikli Yap</td>
        </tr>
                <tr>
            <td></td>
            <td></td>
            <td><%:Html.CheckBoxFor(x=>x.IsWaiting) %> İncelendi Beklemede </td>
        </tr>
    </table>
