<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<MessageViewModel>"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Mesajlar-Makina Türkiye
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        $(function () {

            jQuery.curCSS = function (element, prop, val) {
                return jQuery(element).css(prop, val);
            };
            $("#Message_MainPartyFullName").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: "/Account/Message/FindMainPartyFullName",
                        type: "POST",
                        dataType: "json",
                        data: { searchText: request.term },
                        success: function (data) {
                            response($.map(data, function (item) {
                                return { label: item.MainPartyFullName, value: item.MainPartyFullName, id: item.MainPartyId }
                            }))
                        },
                        error: function (x, y, z) {
                            alert(x.responseText);
                        }
                    })
                },
                select: function (event, ui) {
                    $('#MainPartyId').val(ui.item.id);
                    $('.MainPartyId').val(ui.item.id);
                }
            });

        });

        function DeletePicture(messageId) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Message/DeletePicture',
                    type: 'POST',
                    data:
                    {
                        MessageId: messageId
                    },
                    success: function (data) {
                        $('#inboxList').html(data);
                    },
                    error: function (x, l, e) {
                        alert(e.responseText);
                    }
                });
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
    </div>

    <div class="row">

        <div class="col-sm-12 col-md-12">

            <%
                var type = (MessagePageType)Request.QueryString["MessagePageType"].ToByte();
                switch (type)
                {
                    case MessagePageType.Send: %>
            <%= Html.RenderHtmlPartial("Send")%>
            <%break;
                case MessagePageType.Inbox: %>
            <%= Html.RenderHtmlPartial("Inbox",Model)%>
            <%break;
                case MessagePageType.Outbox: %>
            <%= Html.RenderHtmlPartial("Outbox")%>
            <%break;
                case MessagePageType.RecyleBin: %>
            <%= Html.RenderHtmlPartial("RecyleBin")%>
            <%break;
                    default:
                        break;
                }%>
        </div>
    </div>
</asp:Content>
