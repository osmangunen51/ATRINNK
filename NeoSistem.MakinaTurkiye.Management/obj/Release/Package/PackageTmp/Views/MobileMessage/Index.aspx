<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ICollection<NeoSistem.MakinaTurkiye.Management.Models.Entities.MobileMessage>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
    <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
    <style type="text/css">
        /* Custom Theme */
        #superbox-overlay { background: #e0e4cc; }
        #superbox-container .loading { width: 32px; height: 32px; margin: 0 auto; text-indent: -9999px; background: url(styles/loader.gif) no-repeat 0 0; }
        #superbox .close a { float: right; padding: 0 5px; line-height: 20px; background: #333; cursor: pointer; }
            #superbox .close a span { color: #fff; }
        #superbox .nextprev a { float: left; margin-right: 5px; padding: 0 5px; line-height: 20px; background: #333; cursor: pointer; color: #fff; }
        #superbox .nextprev .disabled { background: #ccc; cursor: default; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <a class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" href="/MobileMessage/Create">Yeni Ekle</a>
    <div style="width: 100%; margin: 0 auto;">
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header HeaderBegin" style="width: 200px;" unselectable="on">Mesaj Adı
          </td>
                    <td id="tabloadd" class="Header" style="width: 70px; height: 19px;">Mesaj İçerik
          </td>
                    <td class="Header" style="width: 70px; height: 19px;">Tip</td>
                    <td class="Header" style="width: 70px; height: 19px"></td>
                </tr>
            </thead>
            <tbody id="table">
                <% int row = 0; %>
                <% foreach (var item in Model)
                    { %>
                <% row++; %>
                <tr id="row<%: item.ID %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
                    <td class="CellBegin">
                        <%: item.MessageName %>
  </td>
                    <td class="Cell"><%Response.Write(item.MessageContent); %></td>
                    <td class="Cell">
                        <%if (item.MessageType == (byte)MobileMessageType.Normal)
                            { %>
                        <p>Normal</p>
                        <%}
                        else
                        {%>
                        <p>Whatsapp</p>
                        <% } %>
                    </td>
                    <td class="CellEnd" style="width: 100px;">
                        <a style="padding-bottom: 5px; cursor: pointer" href="/MobileMessage/EditMessage/<%:item.ID %>">

                            <div style="float: left; margin-right: 10px">
                                <img src="/Content/images/edit.png" />
                            </div>
                        </a><%--<a style="cursor: pointer;" href="MessagesMT/Delete?id=<%:item.MessagesMTId %>">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
      </div>
    </a>--%>
  </td>
                </tr>
                <% } %>
                <tr>
                    <td class="ui-state ui-state-default" colspan="7" align="right" style="border-color: #DDD; border-top: none; border-bottom: none;">
                        <div style="float: right;" class="pagination">
                            <ul>
                            </ul>
                        </div>
                    </td>
                </tr>

            </tbody>
            <%--      <tfoot>
        <tr>
          <td class="ui-state ui-state-hover" style="border-color: #DDD;
            border-top: none; padding-right: 10px" colspan="1" valign="top" style="width: 150px;">
          <%:Ajax.ActionLink("Hesapla", "ProductRateCalculate", new AjaxOptions() { UpdateTargetId = "statisticproduct", HttpMethod = "Post" })%>
          </td>
          <td class="ui-state ui-state-hover" style="border-color: #DDD;
            border-top: none; padding-right: 10px" colspan="1">
           : <div id="statisticproduct" style="margin-top: 5px">

            </div>
          </td>
          <td class="ui-state ui-state-hover" colspan="1" align="right" style="border-color: #DDD;
            border-top: none; padding-right: 10px">
            Toplam Kayıt : &nbsp;&nbsp;<strong>
              <%= Model.Count %></strong>
          </td>
        </tr>
      </tfoot>--%>
        </table>
    </div>
</asp:Content>

