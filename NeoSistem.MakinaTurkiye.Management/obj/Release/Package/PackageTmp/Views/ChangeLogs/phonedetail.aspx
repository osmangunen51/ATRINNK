﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MakinaTurkiye.Entities.Tables.Common.PhoneChangeHistory>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Telefon Bilgileri Değişikliği
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

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
              <div>
                  <% var entities = new MakinaTurkiyeEntities();
                     var store=entities.Stores.FirstOrDefault(x=>x.MainPartyId==Model.MainPartyId);
                     var member=entities.Members.FirstOrDefault(x=>x.MainPartyId==Model.MainPartyId);
                     var phone = entities.Phones.FirstOrDefault(x=>x.PhoneId==Model.PhoneId); %>
                  <h2></h2>
            <table border="0" cellpadding="5" cellspacing="0" style="margin-left: 20px;">
                <thead>
                    <tr>
                        <th></th>
                        <th></th>
                        <th>Önce</th>
                        <th>Sonra</th>
                    </tr>
                </thead>
        <tr>
          <td style="width: 150px;">
            Firma İsmi
          </td>
          <td>
            :
          </td>
          <td>
            <%if(store!=null){ %>
                <%:store.StoreName %>
              <%} else{%>
                <%:member.MemberName+" "+member.MemberSurname %>
              <%} %>
          </td>
            <td>
             
            </td>
        </tr>
         <tr>
          <td>
       Telefon
          </td>
          <td>
            :
          </td>
          <td>
          <%:Model.PhoneCulture+" "+Model.PhoneAreaCode+" "+Model.PhoneNumber %>
          </td>
                <td>
           <%:phone.PhoneCulture+" "+phone.PhoneAreaCode+" "+phone.PhoneNumber %>
          </td>
        </tr>
            </table>
        </div>

</asp:Content>