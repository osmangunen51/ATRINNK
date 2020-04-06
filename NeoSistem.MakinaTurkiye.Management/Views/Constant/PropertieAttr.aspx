<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.ViewModel.PropertieAttrViewModel>" %>

<div style="margin: auto">
    <div style="margin-top: 20px;" class="newValue">
        <h3>Alt Seçim ekle</h3>
        <%using (Html.BeginForm())
            {%>
        <%:Html.HiddenFor(x=>x.PropertieId) %>
        <table>
            <tr>
                <td>Özellik Adı</td>
                <td>:</td>
                <td><%:Html.Raw(Model.PropertieName) %></td>
                <td></td>

            </tr>
            <tr>
                <td>Seçenek Adı</td>
                <td>:</td>
                <td><%:Html.TextBoxFor(x=>x.PropertieAttrValue) %></td>
                <td>
                    <%=Html.ValidationMessageFor(x=>x.PropertieAttrValue) %>
                </td>

            </tr>
            <tr>
                <td>Sıra</td>
                <td>:</td>
                <td><%:Html.TextBoxFor(x=>x.Order) %></td>
            </tr>
            <tr>
                <td></td>
                <td colspan="3">
                    <button type="submit" >Ekle</button>
                </td>
            </tr>
        </table>
        <% } %>
    </div>
    <div class="data" style="margin-top: 10px;">
        <%if (Model.PropertieAttrs.Count > 0)
            {%>
        <table cellpadding="2" cellspacing="0" class="TableList" style="width: 100%;">
            <thead>
                <td class="Header HeaderBegin">Seçenek Adı</td>
                <td class="Header">Sıra </td>
                <td class="Header HeaderEnd">Araçlar</td>
            </thead>
            <%foreach (var item in Model.PropertieAttrs)
                {%>
            <tr>
                <td><%:item.PropertieAttrName %></td>
                <td>
                    <%if (item.Order != 0) {%>
                                 <%:item.Order %></td> 
                    <% } %>
          
                <td class="Cell CellEnd"><a href="/Constant/DeletePropertieAttr/<%:item.PropertieAttrId %>?propertieId=<%:item.PropertieId %>" style="cursor: pointer;">Sil</a></td>
            </tr>
            <% } %>
        </table>
        <% } %>
    </div>
</div>



