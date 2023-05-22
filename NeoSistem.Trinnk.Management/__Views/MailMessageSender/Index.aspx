<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.Trinnk.Management.Models.ViewModel.MailSenderViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
  <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox.js"></script>
  	<style type="text/css">
		/* Custom Theme */
		#superbox-overlay{background:#e0e4cc;}
		#superbox-container .loading{width:32px;height:32px;margin:0 auto;text-indent:-9999px;background:url(styles/loader.gif) no-repeat 0 0;}
		#superbox .close a{float:right;padding:0 5px;line-height:20px;background:#333;cursor:pointer;}
		#superbox .close a span{color:#fff;}
		#superbox .nextprev a{float:left;margin-right:5px;padding:0 5px;line-height:20px;background:#333;cursor:pointer;color:#fff;}
		#superbox .nextprev .disabled{background:#ccc;cursor:default;}
	</style>
    <script type="text/javascript"> 
        function GetMemberType()
        {
            var memberType = $("input:radio[name='memberType']:checked").val();
            if(memberType=="5")
            {
                $("#FastMemberTypeWrapper").show();
                $("#StorePacketWrapper").hide();
                $("#PhoneConfirmWrapper").show();
                $("#MailActiveWrapper").show();
                

            }
            else if(memberType=="20")
            {
                $("#StorePacketWrapper").show();
                $("#PhoneConfirmWrapper").show();
                $("#FastMemberTypeWrapper").hide();
                $("#MailActiveWrapper").hide();
           

            }
            else if(memberType=="10")
            {
                $("#PhoneConfirmWrapper").show();
                $("#FastMemberTypeWrapper").hide();
                $("#StorePacketWrapper").hide();
                $("#MailActiveWrapper").show();
                //bireysel ise
            }
            else {
                $("#PhoneConfirmWrapper").show();
                $("#FastMemberTypeWrapper").hide();
                $("#StorePacketWrapper").hide();
                $("#MailActiveWrapper").show();

            }

        }
        function FastMemberShipClick() {
            var fastmemberType = $("input:radio[name='FastMembershipType']:checked").val();
            if(fastmemberType=="10")
            {
                $("#MailActiveWrapper").hide();
                $("#PhoneConfirmWrapper").hide();

            }
            else if(fastmemberType=="30")
            {
                $("#MailActiveWrapper").show();
                $("#PhoneConfirmWrapper").hide();
            }
            else if(fastmemberType=="20")
            {
                $("#MailActiveWrapper").show();
                $("#PhoneConfirmWrapper").show();
            }
            else if(fastmemberType=="35")
            {
                $("#MailActiveWrapper").hide();
                $("#PhoneConfirmWrapper").hide();
            }
            else if(fastmemberType=="5")
            {
                $("#PhoneConfirmWrapper").hide();
            }
        }
    </script>
    <style type="text/css">
        .panelLeft{
            background-color:#e0e4cc;
            min-height:100px;
            height:auto!important;
            width:47%;
            height:auto;
            margin-left:15px;
            margin-top:10px;
            margin-right:15px;
        }
        
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%if(ViewData["noMember"]=="1"){ %>
        Aradığınız kriterlerde üye bulunamadı.
    <%} %>
    <%using(Html.BeginForm()){ %>
    <div style="margin:10px;">
    Dosya İsmi: <%:Html.TextBox("fileName")%>
        </div>
    <div class="panelLeft">
        <h4 style="padding:10px; text-decoration:underline;">Üyelik Tipi</h4>
            <%int row = 0;
              foreach (var item in Model.MemberTypes.ToList())
              {%>
                 
            <div style="float:left; width:50%;">
                <%:Html.RadioButton("memberType", item.Value, item.IsChecked, new {@onclick="GetMemberType()" })%><%:item.DisplayName %>
            </div> 
             <% }
               %>   
    </div>
    <div class="panelLeft" style="display:none;" id="FastMemberTypeWrapper">
         <h4 style="padding:10px; text-decoration:underline;">Hızlı Üye Tipleri</h4>
             <table>
            <tr>
                    <td><%:Html.RadioButton("FastMembershipType","5", new {@onclick="FastMemberShipClick()",@id="All" }) %>Normal Hızlı Üyelik</td>
                <td><%:Html.RadioButton("FastMembershipType", "20", new {@onclick="FastMemberShipClick()",@id="Message" })%>Mesajla Hızlı Üyeler</td>
                <td><%:Html.RadioButton("FastMembershipType", "10", new {@onclick="FastMemberShipClick()",@id="Facebook" })%>Facebook İle Hızlı Üyeler</td>
               </tr>
            <tr>
                 <td><%:Html.RadioButton("FastMembershipType", "30", new {@onclick="FastMemberShipClick()",@id="Complain" })%>Ürün Şikayetiyle Hızlı Üyeler</td>
                  <td><%:Html.RadioButton("FastMembershipType","35", new {@onclick="FastMemberShipClick()",@id="All" }) %>Hepsi</td>
            </tr>
        </table>
    </div>
          <div class="panelLeft" id="MailActiveWrapper">
                 <h4 style="padding:10px; text-decoration:underline;">Hesap Durumu</h4>
                <table>
            <tr>
                <td><%:Html.RadioButton("mailActive", "1")%>Aktif Üyeler</td>
                <td><%:Html.RadioButton("mailActive", "0",true)%>Aktif Olmayan Üyeler</td>
                   <td><%:Html.RadioButton("mailActive", "2",true)%>Hepsi</td>
               </tr>
        </table>
    </div>
    <div class="panelLeft" id="PhoneConfirmWrapper">
                 <h4 style="padding:10px; text-decoration:underline;">Teleofon Onay Durumu</h4>
                <table>
            <tr>
                <td><%:Html.RadioButton("memberPhoneConfirm", "0")%>Telefonu Onaylanmamış Üyeler</td>
                <td><%:Html.RadioButton("memberPhoneConfirm", "1")%>Telefon Onaylanmış Üyeler</td>
                <td><%:Html.RadioButton("memberPhoneConfirm","2")%>Hepsi</td>
               </tr>
        </table>
    </div>
  
    <div class="panelLeft" id="StorePacketWrapper" style="display:none; height:250px!important;">
             <h4 style="padding:10px; text-decoration:underline;">Paket Tipi</h4>
            <%foreach (var item in Model.Packets.ToList())
              {%>
                 
            <div style="float:left; width:50%;">
                <%:Html.RadioButton("packetId", false, new {@Value=item.PacketId })%><%:item.PacketName %>
            </div> 
             <% }
               %>
         <div style="float:left; width:50%;">
                <%:Html.RadioButton("packetId", false, new {@Value="0"})%>Hepsi
            </div>   
    </div>

    <button style="margin-left:10px;margin-top:10px;" type="submit">Gönder</button>
    <%} %>
</asp:Content>