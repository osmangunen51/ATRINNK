<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage< NeoSistem.MakinaTurkiye.Management.Models.Orders.StoreContactOrderModel>" %>



<div style="width: 700px; float: left; margin: 20px 0px 0px 20px">
    <div style="float: left; width: 50%;">
        <b>Firma Adı:</b><%:Model.StoreName %><br />
        <b>Yetkili Adı:</b><%:Model.MemberNameSurname %><br />
        <%:EnumModels.AddressEdit(Model.Address) %>
        <br />
        <% foreach (var item in Model.Phones)
            { %>
        <% if (!string.IsNullOrWhiteSpace(item.PhoneNumber))
            { %>
        <% string phoneType = string.Empty;
            if (item.PhoneType == (byte)PhoneType.Fax)
            {
                phoneType = "Fax :"; %>
        <%:phoneType %>
        <%:item.PhoneCulture%>&nbsp;<%:item.PhoneAreaCode%>&nbsp;<%:item.PhoneNumber%><%

            }
            else if (item.PhoneType == (byte)PhoneType.Gsm)
            {
                phoneType = "Gsm :";%>
        <%:phoneType %>
        <%:item.PhoneCulture%>&nbsp;<%:item.PhoneAreaCode%>&nbsp;<%:item.PhoneNumber%><%
            }
            else if (item.PhoneType == (byte)PhoneType.Phone)
            {
                phoneType = "Phone :";%>
        <%:phoneType %>
        <%:item.PhoneCulture%>&nbsp;<%:item.PhoneAreaCode%>&nbsp;<%:item.PhoneNumber%><%
            }
            else if (item.PhoneType == (byte)PhoneType.Whatsapp)
            {
                phoneType = "Whatsapp";%>
        <%:phoneType %> 

        <a style="font-size: 15px; font-weight: 600" href="https://api.whatsapp.com/send?phone=<%:item.PhoneCulture%>&nbsp;<%:item.PhoneAreaCode%>&nbsp;<%:item.PhoneNumber%>" target="_blank" ><%:item.PhoneCulture%>&nbsp;<%:item.PhoneAreaCode%>&nbsp;<%:item.PhoneNumber%></a> 
        
        <%
            }
        %>        
        <br />

        <% }
            }%>
    Email:
        <%:Model.Email %>
    </div>
    <div style="float: left; width: 50%;">

        <table>
            <tr>
                <td colspan="3"><a style="font-size: 15px; font-weight: 600" href="/Member/MemberDescription/<%:Model.MemberMainPartyId %>">Son Üye Açıklama</a></td>
            </tr>

            <%foreach (var item in Model.StoreMemberDescriptions)
                {%>
            <tr>
                <td colspan="3">
                    <b><%:item.Title %></b>(<%:item.UserName %>)<br />
                    <span style="padding-left: 5px; font-size: 12px;"><%:Html.Raw(item.Description) %></span> <b style="font-size: 12px; color: #0fe54e">
                        <br />
                        <%:item.RecordDate.HasValue==true?item.RecordDate.ToDateTime().ToString("dd-MM-yyyy hh:mm"):"" %></b>
                </td>
            </tr>
            <%} %>
        </table>
    </div>
</div>

