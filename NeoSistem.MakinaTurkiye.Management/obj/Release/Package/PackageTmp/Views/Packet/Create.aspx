﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<PacketModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Create
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="http: //code.jquery.com/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var isRegistered = false;
        var isUnRegistered = false;
        $(document).ready(function () {
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
    <%using (Html.BeginForm("Create", "Packet", FormMethod.Post, new { enctype = "multipart/form-data" }))
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
        <div style="border: solid 1px #bababa; width: 300px; height: auto; float: left; padding-bottom: 10px; margin-top: 10px; margin-left: 10px;">
            <div style="float: left; margin-left: 2%; margin-top: 5px; width: 96%; border-bottom: dashed 1px #bababa; padding-bottom: 3px;">
                <span>Paket Özellikleri - </span><span style="font-weight: bold;">
                    <%:item.PacketFeatureTypeName %></span>
            </div>
            <div style="float: left; width: 275px; border: dashed 1px #bababa; margin-left: 10px; margin-top: 10px;">
                <div style="float: left; width: 100%;">
                    <div style="width: 10%; float: left; height: 26px; border-right: dashed 1px #bababa; border-bottom: dashed 1px #bababa; text-align: center; padding-top: 4px;">
                        <input id="rdNumber<%:item.PacketFeatureTypeId %>" onclick="changeFeatureType('1', 'txtNumber<%:item.PacketFeatureTypeId %>','chkActive<%:item.PacketFeatureTypeId %>','chkContent<%:item.PacketFeatureTypeId %>','<%:item.PacketFeatureTypeId %>','txtNumber<%:item.PacketFeatureTypeId %>','hdn<%:item.PacketFeatureTypeId %>','1','<%:item.PacketFeatureTypeId %>');"
                            type="radio" name="rdActive<%:item.PacketFeatureTypeId %>" />
                    </div>
                    <div id="divNumber<%:item.PacketFeatureTypeId %>" style="width: 89%; float: left; height: 30px; border-bottom: dashed 1px #bababa; background-color: #ececec;">
                        <div style="float: left; margin-top: 6px; margin-left: 5px;">
                            Sayı Kısıtlaması :
           
                        </div>
                        <div style="float: left; margin-top: 4px; margin-left: 5px;">
                            <input id="txtNumber<%:item.PacketFeatureTypeId %>" type="text" style="height: 15px; width: 50px;"
                                onkeypress="setValues('txtNumber<%:item.PacketFeatureTypeId %>','hdn<%:item.PacketFeatureTypeId %>','1',<%:item.PacketFeatureTypeId %>)"
                                disabled="disabled" />
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 100%;">
                    <div style="width: 10%; float: left; height: 26px; border-right: dashed 1px #bababa; border-bottom: dashed 1px #bababa; text-align: center; padding-top: 4px;">
                        <input id="rdActive<%:item.PacketFeatureTypeId %>" type="radio" name="rdActive<%:item.PacketFeatureTypeId %>"
                            onclick="changeFeatureType('2', 'txtNumber<%:item.PacketFeatureTypeId %>','chkActive<%:item.PacketFeatureTypeId %>','chkContent<%:item.PacketFeatureTypeId %>','<%:item.PacketFeatureTypeId %>','chkActive<%:item.PacketFeatureTypeId %>','hdn<%:item.PacketFeatureTypeId %>','2','<%:item.PacketFeatureTypeId %>');" />
                    </div>
                    <div id="divActive<%:item.PacketFeatureTypeId %>" style="width: 89%; float: left; height: 30px; border-bottom: dashed 1px #bababa; background-color: #ececec;">
                        <div style="float: left; margin-top: 6px; margin-left: 5px;">
                            Aktif :
           
                        </div>
                        <div style="float: left; margin-top: 4px; margin-left: 5px;">
                            <input id="chkActive<%:item.PacketFeatureTypeId %>" type="checkbox" onclick="setValues('chkActive<%:item.PacketFeatureTypeId %>','hdn<%:item.PacketFeatureTypeId %>','2','<%:item.PacketFeatureTypeId %>');"
                                disabled="disabled" />
                        </div>
                    </div>
                </div>
                <div style="float: left; width: 100%;">
                    <div style="width: 10%; float: left; height: 26px; border-right: dashed 1px #bababa; text-align: center; padding-top: 4px;">
                        <input id="rdContent<%:item.PacketFeatureTypeId %>" type="radio" name="rdActive<%:item.PacketFeatureTypeId %>"
                            onclick="changeFeatureType('3', 'txtNumber<%:item.PacketFeatureTypeId %>','chkActive<%:item.PacketFeatureTypeId %>','chkContent<%:item.PacketFeatureTypeId %>','<%:item.PacketFeatureTypeId %>','chkContent<%:item.PacketFeatureTypeId %>','hdn<%:item.PacketFeatureTypeId %>','3','<%:item.PacketFeatureTypeId %>');" />
                    </div>
                    <div id="divContent<%:item.PacketFeatureTypeId %>" style="width: 89%; float: left; height: 30px; background-color: #ececec;">
                        <div style="float: left; margin-top: 6px; margin-left: 5px;">
                            İçerik Yazısı :
           
                        </div>
                        <div style="float: left; margin-top: 4px; margin-left: 5px;">
                            <input id="chkContent<%:item.PacketFeatureTypeId %>" type="text" style="height: 15px; width: 100px;"
                                onkeypress="setValues('chkContent<%:item.PacketFeatureTypeId %>','hdn<%:item.PacketFeatureTypeId %>','3',<%:item.PacketFeatureTypeId %>)"
                                disabled="disabled" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <input class="hdnValues" type="hidden" featurename="<%:item.PacketFeatureTypeName %>"
            id="hdn<%:item.PacketFeatureTypeId %>" name="PacketFeature" />
        <% } %>
    </div>
    <%} %>
</asp:Content>
