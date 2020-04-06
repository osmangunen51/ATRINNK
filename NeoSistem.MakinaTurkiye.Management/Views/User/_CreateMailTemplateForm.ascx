﻿<%@ Control Language="C#"   Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Management.Models.MailTemplateFormModel>" %>
 <%Html.ValidationSummary();%>
            <%:Html.HiddenFor(x=>x.UserId) %>
<%:Html.HiddenFor(x=>x.MailTemplateId) %>
  <table class="tableForm" style="padding: 10px; height: auto;">

      <tr>
          <td><%:Html.LabelFor(x=>x.SpecialId) %>:</td>
          <td><%:Html.DropDownListFor(x=>x.SpecialId,Model.SpecialMails)%></td>
          <td><%:Html.ValidationMessageFor(x=>x.SpecialId) %></td>
      </tr>

        <tr>
            <td><%:Html.LabelFor(x=>x.UserGroupId) %>:</td>
                <td><%:Html.DropDownListFor(x=>x.UserGroupId,Model.UserGroups)%></td>
                    <td><%:Html.ValidationMessageFor(x=>x.UserGroupId) %></td>
        </tr>

       <tr>
      <td style="width: 120px;">
        <%: Html.LabelFor(model => model.Subject) %>
        :
      </td>
      <td>
        <%: Html.TextBoxFor(model => model.Subject,new { style ="width:300px" }) %>

      </td>
           <td> <%: Html.ValidationMessageFor(model => model.Subject)%></td>
    </tr>
    <tr>
      <td>
        <%: Html.LabelFor(model => model.MailContent) %>
        :
      </td>
      <td>
        <%: Html.TextAreaFor(model => model.MailContent, new { style = "width:300px" })%>

      </td>
        <td>
                    <%: Html.ValidationMessageFor(model => model.MailContent)%>
        </td>
    </tr>
   
    <tr style="height: 40px;">
      <td colspan="2" align="right" style="padding-bottom: 10px;">
        <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px;
          margin-bottom: 10px;">
        </div>
        <br />
        <button type="submit" style="width: 70px; height: 35px;">
          Kaydet
        </button>
        <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/User/Index'">
          İptal
        </button>
      </td>
    </tr>
  </table>