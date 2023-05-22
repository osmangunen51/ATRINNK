<%@ Control Language="C#"   Inherits="System.Web.Mvc.ViewUserControl<UserModel>" %>

    <table class="tableForm" style="padding: 10px; height: auto;">
        <%:Html.HiddenFor(x=>x.UserId) %>
        <tr>
            <td style="width: 120px;">
                <%: Html.LabelFor(model => model.UserName) %>
        :
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.UserName,new { style ="width:300px" }) %>
                <%: Html.ValidationMessageFor(model => model.UserName)%>
            </td>
        </tr>
        <tr>
            <td>
                <%: Html.LabelFor(model => model.UserPass) %>
        :
            </td>
            <td>
                <%: Html.TextBoxFor(model => model.UserPass, new { style = "width:300px" })%>
                <%: Html.ValidationMessageFor(model => model.UserPass)%>
            </td>
        </tr>



        <tr>
            <td>İzin Grubu :
            </td>
            <td>
                <div style="max-height: 300px; overflow: auto; border: solid 1px #CCC">
                    <ul style="list-style: none; padding: 0; margin: 0;">
                        <% foreach (var item in UserGroupModel.UserGroups)
                            { %>
                        <li class='hover' style="border: none; float: left; width: 170px" onclick="$get('per<%: item.UserGroupId %>').click();">
                            <% bool hasRecord = Model.Groups.Any(c => c.UserGroupId == item.UserGroupId); %>
                            <%: Html.RadioButton("Permission", item.UserGroupId, hasRecord, new { id = "per" + item.UserGroupId, value = item.UserGroupId })%><span
                                style="cursor: default; display: inline-block; height: 17px;"><%: item.GroupName %></span></li>
                        <% } %>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <%:Html.LabelFor(x=>x.Name) %>
              :
            </td>
            <td>
                <%:Html.TextBoxFor(x=>x.Name) %>
            </td>

        </tr>
                <tr>
            <td>
                <%:Html.LabelFor(x=>x.Surname) %>
              :
            </td>
            <td>
                <%:Html.TextBoxFor(x=>x.Surname) %>
            </td>

        </tr>
        <tr>
            <td>
                <%:Html.LabelFor(x=>x.UserMail) %>
              :
            </td>
            <td>
                <%:Html.TextBoxFor(x=>x.UserMail) %>
            </td>

        </tr>
        <tr>
            <td>
                <%:Html.LabelFor(x=>x.UserMail) %>
              :
            </td>
            <td>
                <%:Html.TextBoxFor(x=>x.UserMail) %>
            </td>

        </tr>
        <tr>
            <td><%:Html.LabelFor(x=>x.MailPassword) %>:</td>
            <td><%:Html.TextBoxFor(x=>x.MailPassword) %></td>
        </tr>
        <tr>
            <td><%:Html.LabelFor(x=>x.MailSmtp) %>:</td>
            <td><%:Html.TextBoxFor(x=>x.MailSmtp) %></td>
        </tr>
        <tr>
            <td><%:Html.LabelFor(x=>x.SendCode) %>:</td>
            <td><%:Html.TextBoxFor(x=>x.SendCode) %></td>
        </tr>
        <tr>
            <td><%:Html.LabelFor(x=>x.UserColor) %>:</td>
            <td><%:Html.TextBoxFor(x=>x.UserColor) %></td>
        </tr>
        <tr>

            <td colspan="2"><%:Html.CheckBoxFor(x=>x.Active) %> Bildirim Aktif
         
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <%:Html.CheckBoxFor(x=>x.ActiveForDesc) %> Açıklama Aktif
            </td>
        </tr>
        <tr>
            <td>
                <%: Html.LabelFor(model => model.Signature) %>
            :
            </td>
            <td>
                <%: Html.TextAreaFor(model => model.Signature, new {@style="width:100%; height:60px;" })%>
            </td>
        </tr>
        <tr>
            <td>
                <%: Html.LabelFor(model => model.CallCenterUrl) %>
            :
            </td>
            <td>
                <%: Html.TextAreaFor(model => model.CallCenterUrl, new {@style="width:100%; height:60px;" })%>
            </td>
        </tr>
        <tr style="height: 40px;">
            <td colspan="2" align="right" style="padding-bottom: 10px;">
                <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-top: 15px; margin-bottom: 10px;">
                </div>

            </td>
        </tr>
    </table>

