﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PacketModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Edit
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        var isRegistered = false;
        var isUnRegistered = false;
        $(document).ready(function () {
            isRegistered = $('#Registered').attr('checked');
            isUnRegistered = $('#UnRegistered').attr('checked');
            $('#Registered').change(function () {
                if ($(this).attr('checked') == true) {
                    isRegistered = true;
                }
                else {
                    isRegistered = false;
                }
            });
            $('#UnRegistered').change(function () {
                if ($(this).attr('checked') == true) {
                    isUnRegistered = true;
                }
                else {
                    isUnRegistered = false;
                }
            });

        });

        function changeFeatureType(rdType, numberId, activeId, contentId, id, txtValue, hdnPacketFullValue, packetType, packetFeatureId) {
            if (rdType == 1) {
                $('#' + numberId).removeAttr('disabled', 'disabled');
                $('#' + activeId).attr('disabled', 'disabled');
                $('#' + contentId).attr('disabled', 'disabled');

                $('#divNumber' + id).css('background-color', '#fff');
                $('#divActive' + id).css('background-color', '#ececec');
                $('#divContent' + id).css('background-color', '#ececec');
            }
            else if (rdType == 2) {
                $('#' + activeId).removeAttr('disabled', 'disabled');
                $('#' + numberId).attr('disabled', 'disabled');
                $('#' + contentId).attr('disabled', 'disabled');

                $('#divNumber' + id).css('background-color', '#ececec');
                $('#divActive' + id).css('background-color', '#fff');
                $('#divContent' + id).css('background-color', '#ececec');
            }
            else if (rdType == 3) {
                $('#' + contentId).removeAttr('disabled', 'disabled');
                $('#' + numberId).attr('disabled', 'disabled');
                $('#' + activeId).attr('disabled', 'disabled');

                $('#divNumber' + id).css('background-color', '#ececec');
                $('#divActive' + id).css('background-color', '#ececec');
                $('#divContent' + id).css('background-color', '#fff');
            }

            if (packetType == 2) {
                $('#' + hdnPacketFullValue).val(packetFeatureId + ',' + packetType + ',' + $('#' + txtValue).attr('checked'));
            }
            else {
                $('#' + hdnPacketFullValue).val(packetFeatureId + ',' + packetType + ',' + $('#' + txtValue).val());
            }
        }

        function setValues(txtValue, hdnPacketFullValue, packetType, packetFeatureId) {
            if (packetType == 2) {
                $('#' + hdnPacketFullValue).val(packetFeatureId + ',' + packetType + ',' + $('#' + txtValue).attr('checked'));
            }
            else {
                $('#' + hdnPacketFullValue).val(packetFeatureId + ',' + packetType + ',' + $('#' + txtValue).val());
            }
        }

        function checkValidateControl() {

            var hasRecord = true;
            var alertMessage = '';
            if (isUnRegistered == true || isRegistered == true) {
                return true;
            }
            else {
                $('.hdnValues').each(function () {
                    if ($(this).val() == '') {
                        hasRecord = false;
                        alertMessage = alertMessage + '\'' + $(this).attr('featurename') + '\', ';
                        return;
                    }
                });

                if (hasRecord) {
                    return true;
                }
                else {
                    alert(alertMessage + ' Özellikleri doldurulmadı.');
                    return false;
                }
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm("Edit", "Packet", FormMethod.Post, new { enctype = "multipart/form-data" }))
        {%>
    <div style="width: 1000px; height: auto; float: left; margin-left: 20px; margin-top: 20px; padding-bottom: 30px;">
        <%------------------------------------Paket Genel Bilgileri------------------------------------%>
        <div style="float: left; width: 100%; margin-left: 10px;">
            <div style="border: dashed 2px #bababa; width: 428px; height: auto; float: left; padding-bottom: 10px; margin-top: 10px;">
                <div style="float: left; margin-left: 2%; margin-top: 5px; width: 96%; border-bottom: dashed 1px #bababa; padding-bottom: 3px;">
                    <span style="font-weight: bold;">Paket Genel Bilgileri</span>
                </div>
                <div style="float: left; width: 430px;">
                    <div style="float: left; width: 100%; margin-top: 10px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Paket Adı :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.TextBoxFor(c => c.PacketName, new { style = "width: 250px;" })%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Paket Açıklaması :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.TextBoxFor(c => c.PacketDescription, new { style = "width: 250px;" })%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Paket Fiyatı :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.TextBoxFor(c => c.PacketPrice, new { style = "width: 250px;" })%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Paket Geçerlilik Süresi (Gün) :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.TextBoxFor(c => c.PacketDay, new { style = "width: 250px;" })%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Paket Sıra No :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.TextBoxFor(c => c.PacketOrder, new { style = "width: 250px;" })%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Paket Renk Kodu :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.TextBoxFor(c => c.PacketColor, new { style = "width: 250px;" })%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Başlık Renk Kodu :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.TextBoxFor(c => c.HeaderColor, new { style = "width: 250px;" })%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Ürün Kat Sayısı:
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.TextBoxFor(c => c.ProductFactor, new { style = "width: 250px;" })%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Başlangıç Paketi :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.CheckBoxFor(c => c.IsOnset)%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Standart Paket :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.CheckBoxFor(c => c.IsStandart)%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Taahhütlü Paket :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.CheckBoxFor(c => c.Registered, new { id = "Registered" })%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Taahhütsüz Paket :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.CheckBoxFor(c => c.UnRegistered, new { id = "UnRegistered" })%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Firma Paket Yenileme Maili :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.CheckBoxFor(c => c.SendReminderMail, new { id = "sendReminderMail" })%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            İndirimli Paket :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.CheckBoxFor(c => c.IsDiscounted, new { id = "isDiscounted" })%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Doping Paket :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.CheckBoxFor(c => c.IsDopingPacket, new { id = "isDiscounted" })%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Doping Paket Gün :
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.TextBoxFor(c => c.DopingPacketDay, new { id = "isDiscounted" })%>
                        </div>
                    </div>
                    <div style="float: left; width: 100%; margin-top: 5px;">
                        <div style="float: left; width: 140px; padding-left: 10px;">
                            Admin Paket Satın Al
           
                        </div>
                        <div style="float: left; width: 280px;">
                            <%:Html.CheckBoxFor(c => c.ShowAdmin, new { id = "showAdmin" })%>
                        </div>
                    </div>
                </div>
            </div>
            <div style="float: left; margin-top: 10px; margin-left: 10px;">
                <button type="submit" onclick="return checkValidateControl();">
                    Kaydet
                </button>
                <button type="button" onclick="javascript:window.location.href='/Packet/Index/'">
                    İptal
                </button>
            </div>
        </div>
        <%------------------------------------Paket Özellikleri ------------------------------------%>

        <% foreach (var item in Model.PacketFeatureTypeItems)
            { %>
        <%var packetFeature = Model.PacketFeatureItems.SingleOrDefault(c => c.PacketFeatureTypeId == item.PacketFeatureTypeId); %>
        <% string checkedText = "checked=\"checked\""; %>
        <div style="border: solid 1px #bababa; width: 300px; height: auto; float: left; padding-bottom: 10px; margin-top: 10px; margin-left: 10px;">
            <div style="float: left; margin-left: 2%; margin-top: 5px; width: 96%; border-bottom: dashed 1px #bababa; padding-bottom: 3px;">
                <span>Paket Özellikleri - </span><span style="font-weight: bold;">
                    <%:item.PacketFeatureTypeName %></span>
            </div>
            <div style="float: left; width: 275px; border: dashed 1px #bababa; margin-left: 10px; margin-top: 10px;">
                <div style="float: left; width: 100%;">
                    <div style="width: 10%; float: left; height: 26px; border-right: dashed 1px #bababa; border-bottom: dashed 1px #bababa; text-align: center; padding-top: 4px;">
                        <input id="rdNumber<%:item.PacketFeatureTypeId %>" onclick="changeFeatureType('1', 'txtNumber<%:item.PacketFeatureTypeId %>','chkActive<%:item.PacketFeatureTypeId %>','chkContent<%:item.PacketFeatureTypeId %>','<%:item.PacketFeatureTypeId %>','txtNumber<%:item.PacketFeatureTypeId %>','hdn<%:item.PacketFeatureTypeId %>','1','<%:item.PacketFeatureTypeId %>');"
                            type="radio" name="rdActive<%:item.PacketFeatureTypeId %>" <%:packetFeature!= null && packetFeature.FeatureType.ToByte() == 1 ? checkedText :"" %> />
                    </div>
                    <div id="divNumber<%:item.PacketFeatureTypeId %>" style="width: 89%; float: left; height: 30px; border-bottom: dashed 1px #bababa;">
                        <div style="float: left; margin-top: 6px; margin-left: 5px;">
                            Sayı Kısıtlaması :
                        </div>
                        <div style="float: left; margin-top: 4px; margin-left: 5px;">
                            <input id="txtNumber<%:item.PacketFeatureTypeId %>" type="text" style="height: 15px; width: 50px;"
                                onkeyup="setValues('txtNumber<%:item.PacketFeatureTypeId %>','hdn<%:item.PacketFeatureTypeId %>','1',<%:item.PacketFeatureTypeId %>)"
                                disabled="disabled" value="<%: packetFeature!= null && packetFeature.FeatureType == 1 && packetFeature.FeatureProcessCount != 0 ? packetFeature.FeatureProcessCount.ToString() : "" %>" />
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 100%;">
                    <div style="width: 10%; float: left; height: 26px; border-right: dashed 1px #bababa; border-bottom: dashed 1px #bababa; text-align: center; padding-top: 4px;">
                        <input id="rdActive<%:item.PacketFeatureTypeId %>" type="radio" name="rdActive<%:item.PacketFeatureTypeId %>"
                            onclick="changeFeatureType('2', 'txtNumber<%:item.PacketFeatureTypeId %>','chkActive<%:item.PacketFeatureTypeId %>','chkContent<%:item.PacketFeatureTypeId %>','<%:item.PacketFeatureTypeId %>','chkActive<%:item.PacketFeatureTypeId %>','hdn<%:item.PacketFeatureTypeId %>','2','<%:item.PacketFeatureTypeId %>');"
                            <%:packetFeature!= null && packetFeature.FeatureType.ToByte() == 2 ? checkedText :"" %> />
                    </div>
                    <div id="divActive<%:item.PacketFeatureTypeId %>" style="width: 89%; float: left; height: 30px; border-bottom: dashed 1px #bababa;">
                        <div style="float: left; margin-top: 6px; margin-left: 5px;">
                            Aktif :
                        </div>
                        <div style="float: left; margin-top: 4px; margin-left: 5px;">
                            <% if (packetFeature != null && packetFeature.FeatureType == 2 && packetFeature.FeatureActive != null && packetFeature.FeatureActive.Value)
                                { %>
                            <input id="chkActive<%:item.PacketFeatureTypeId %>" type="checkbox" onclick="setValues('chkActive<%:item.PacketFeatureTypeId %>','hdn<%:item.PacketFeatureTypeId %>','2','<%:item.PacketFeatureTypeId %>');"
                                disabled="disabled" checked="checked" />
                            <% }
                                else
                                { %>
                            <input id="chkActive<%:item.PacketFeatureTypeId %>" type="checkbox" onclick="setValues('chkActive<%:item.PacketFeatureTypeId %>','hdn<%:item.PacketFeatureTypeId %>','2','<%:item.PacketFeatureTypeId %>');"
                                disabled="disabled" />
                            <% } %>
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 100%;">
                    <div style="width: 10%; float: left; height: 26px; border-right: dashed 1px #bababa; text-align: center; padding-top: 4px;">
                        <input id="rdContent<%:item.PacketFeatureTypeId %>" type="radio" name="rdActive<%:item.PacketFeatureTypeId %>"
                            onclick="changeFeatureType('3', 'txtNumber<%:item.PacketFeatureTypeId %>','chkActive<%:item.PacketFeatureTypeId %>','chkContent<%:item.PacketFeatureTypeId %>','<%:item.PacketFeatureTypeId %>','chkContent<%:item.PacketFeatureTypeId %>','hdn<%:item.PacketFeatureTypeId %>','3','<%:item.PacketFeatureTypeId %>');"
                            <%:packetFeature!=null&&packetFeature.FeatureType.ToByte() == 3 ? checkedText :"" %> />
                    </div>
                    <div id="divContent<%:item.PacketFeatureTypeId %>" style="width: 89%; float: left; height: 30px;">
                        <div style="float: left; margin-top: 6px; margin-left: 5px;">
                            İçerik Yazısı :
                        </div>
                        <div style="float: left; margin-top: 4px; margin-left: 5px;">
                            <input id="chkContent<%:item.PacketFeatureTypeId %>" type="text" style="height: 15px; width: 100px;"
                                onkeyup="setValues('chkContent<%:item.PacketFeatureTypeId %>','hdn<%:item.PacketFeatureTypeId %>','3',<%:item.PacketFeatureTypeId %>)"
                                disabled="disabled" value="<%: packetFeature!= null && packetFeature.FeatureType == 3 && !string.IsNullOrWhiteSpace(packetFeature.FeatureContent) ? packetFeature.FeatureContent.ToString() : ""%>" />
                        </div>
                    </div>
                </div>
                <input class="hdnValues" type="hidden" featurename="<%:item.PacketFeatureTypeName %>"
                    id="hdn<%:item.PacketFeatureTypeId %>" name="PacketFeature" />
                <% if (packetFeature != null && packetFeature.FeatureType == 3)
                    { %>
                <script type="text/javascript">
                    changeFeatureType('3', 'txtNumber' + <%:item.PacketFeatureTypeId %>, 'chkActive' + <%:item.PacketFeatureTypeId %>, 'chkContent' + <%:item.PacketFeatureTypeId %>, <%:item.PacketFeatureTypeId %>, 'chkContent' + <%:item.PacketFeatureTypeId %>, 'hdn' + <%:item.PacketFeatureTypeId %>, '3', <%:item.PacketFeatureTypeId %>);
                </script>
                <% }
                    else if (packetFeature != null && packetFeature.FeatureType == 1)
                    { %>
                <script type="text/javascript">
                    changeFeatureType('1', 'txtNumber' + <%:item.PacketFeatureTypeId %>, 'chkActive' + <%:item.PacketFeatureTypeId %>, 'chkContent' + <%:item.PacketFeatureTypeId %>, <%:item.PacketFeatureTypeId %>, 'txtNumber' + <%:item.PacketFeatureTypeId %>, 'hdn' + <%:item.PacketFeatureTypeId %>, '1', <%:item.PacketFeatureTypeId %>);
                </script>
                <% }
                    else if (packetFeature != null && packetFeature.FeatureType == 2)
                    { %>
                <script type="text/javascript">
                    changeFeatureType('2', 'txtNumber' + <%:item.PacketFeatureTypeId %>, 'chkActive' + <%:item.PacketFeatureTypeId %>, 'chkContent' + <%:item.PacketFeatureTypeId %>, <%:item.PacketFeatureTypeId %>, 'chkActive' + <%:item.PacketFeatureTypeId %>, 'hdn' + <%:item.PacketFeatureTypeId %>, '2', <%:item.PacketFeatureTypeId %>);
                </script>
                <% } %>
            </div>
        </div>
        <% } %>
    </div>
    <%} %>
</asp:Content>
