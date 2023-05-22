<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<NeoSistem.Trinnk.Management.Models.BaseMenuModels.BaseMenuImageModel>" %>

<link href="/Content/Site.css" rel="stylesheet" type="text/css" />
<link href="/Content/Ribbon.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jquery-1.8.3.js" type="text/javascript"></script>
<div style="margin: auto">
    <div style="margin-top: 20px;" class="newValue">
        <%if (TempData["result"] != null) {%>
        <p style="font-size:15px; color:#03a015"><%:TempData["result"].ToString() %></p>
        <% }%>
        <h3>Fotoğraf Ekle</h3>
        <%using (Html.BeginForm("BaseMenuImage","Category",FormMethod.Post,new {@enctype="multipart/form-data" }))
            {%>
        <%:Html.HiddenFor(x=>x.BaseMenuId) %>
        <%:Html.HiddenFor(x=>x.BaseMenuImageId) %>
        <table>
            <%if (!string.IsNullOrEmpty(Model.ImagePath)) {%>
            <tr>
                <td colspan="3">
                    <img src="<%:Model.ImagePath %>" style="width:150px" />
                </td>
            </tr>
            <% } %>
            <tr>
                <td>Fotoğraf</td>
                <td>:</td>
                <td>
                    <input name="image" type="file" style="height: 30px;" /></td>

            </tr>
            <tr>
                <td>Link</td>
                <td>:</td>
                <td>
                    <%:Html.TextBoxFor(x=>x.Url) %>
                 
            </tr>
            <tr>
                <td>Aktif</td>
                <td>:</td>
                <td><%:Html.CheckBoxFor(x=>x.Active) %></td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td><input type="submit" value="Ekle" /></td>
            </tr>
        </table>
        <% } %>
    </div>

</div>


