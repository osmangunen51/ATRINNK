﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ICollection<NeoSistem.Trinnk.Management.Models.Entities.UserLog>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Kullanıcı Hareketleri
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
        function DeleteLog(ID)
        {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Log/DeleteLog',
                    data: { id: ID },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        var e = data;
                        if (e) {
                            $('#row' + ID).hide();
                        }
                        else {
                            alert('Bu sabit kullanılıyor.Silme işlemi başarısız.');
                        }
                    }
                });
            }
        }
        $(function() {
            $("#LogType").change(function () {
                var status = $("#LogStatus").val();
                if (status != "16")
                    GetDeger($(this).val(), status);
                else GetDeger($(this).val());
            });
            $("#LogStatus").change(function () {
                var type = $("#LogType").val();
                GetDeger(type,$(this).val());
                    
            });
        })
        function GetDeger(type1,status1,page1)
        {
            $.ajax({
                url: '/Log/GetLogType',
                data: { type:type1,status:status1,page:page1},
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    var e = data;
                    $("#table").html("");
                    var row = 0;
                    if (data.length != 0) {
                        $.each(data, function (index, item) {
                            row++;
                            var string = '<tr class="' + (row % 2 == 0 ? "Row" : "RowAlternate") + '" id="row' + item.logID + '"><td>' + item.logID + '</td><td>' + item.logName + '</td><td>' + (item.logShorDesc == null ? "" : item.logShorDesc) + '</td><td>' + item.logDesc + '</td>';
                            if (item.logType == "5")
                                string = string + '<td>Üyelik</td>';
                            else
                                string = string + "<td>Mesaj</td>";
                            if (item.logStatus == "1")
                                string = string + '<td style="color:#047a47">Başarılı</td>';
                            else
                                string = string + "<td style='color:#c60505;'>Hatalı</td>";

                            string = string + '<td>' + item.logDate + '</td><td><a style="cursor: pointer;" onclick="DeleteLog(' + item.logID + ');">  <div style="float: left;"><img src="/Content/images/delete.png" /></div></a></td></tr>';
                            $("#table").append(string);
                        });
                    }
                    else {
                        $("#table").append('<tr><td colspan="8" style="color:red; font-size:15px;">Kayıtlı log bulunamadı</td></tr>');
                        $("#pagination").hide();
                    }
                }, error: function (request, status, error) { alert(request.responseText); }
            });
        }
        function Paging(deger)
        {
            var status = $("#LogStatus").val();
            var type = $("#LogType").val();
            if (status == "16") status = null;
            GetDeger(type, status, deger);
            $("#page-"+deger).css("background-color", "#fff");

        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <h2>Kullanıcı Hareketleri</h2>
  <div style="width: 100%; margin: 0 auto;">
    <table cellpadding="5"  cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td style="width:5%;">
          ID
          </td>
            <td style="width:15%;">
           Log Adı 
                            </td>
            <td style="width:10%;">Kısa Açıklama</td>
            <td style="width:30%;">Açıklama</td>
            <td>
                Log Tip           
            </td>
            <td style="width:15%;">Durum</td>
            <td style="with:15%;">Tarih</td>
          <td style="width:10;">
          </td>
        </tr>
      </thead>
        <tr>
            <td>
            </td>
            <td>             
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
                <select id="LogType">
                    <option value="0" selected>Tümü--</option>
                    <option value="5 ">Üyelik</option>
                    <option value="10">Mesaj</option>
                </select>
            </td>
            <td>
                  <select id="LogStatus">
                    <option value="16" selected>Tümü--</option>
                    <option value="1">Başarılı</option>
                    <option value="0">Hatalı</option>
                </select>
            </td>
            <td>

            </td>
            <td>

            </td>
        </tr>
      <tbody id="table">
       <% int row = 0; %>
<% foreach (var item in Model)
   { %>

<% row++; %>
<tr id="row<%: item.LogıD %>"  class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
     <td><%:item.LogıD %></td>
  <td>
    <%: item.LogName %>
  </td>
    <td>
        <%:item.LogShortDescription %>
    </td>
    <td>
        <%:item.LogDescription %>
    </td>

    <% int page = Convert.ToInt32(ViewData["page"]); 
            if (item.LogType == 5)
            {
                %> 
    <td >
         Üyelik<br />     
               </td>
         <%} else{%><td>
         Mesaj<br />
         </td>
             <%} %>
    <%if (item.LogStatus == 1)
            {
                %> 
    <td style="color:#047a47" >
         Başarılı<br />     
               </td>
         <%} else{%><td style="color:#c60505;">
         Hatalı<br />
         </td>
             <%} %>
     
    <td><%:item.LogDate %></td>

  <td >
    <a style="cursor: pointer;" onclick="DeleteLog(<%:item.LogıD %>)">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
      </div>
    </a>
  </td>
</tr>
<% } %>


      </tbody>
        <tfoot>
            <tr>
  <td class="ui-state ui-state-default" colspan="8" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
          <%int pageNumbers = Convert.ToInt32(ViewData["pageNumbers"]);
            for (int i = 1; i <=pageNumbers ; i++)
            {

                Response.Write("<li id='page-" + i + "'><a  onclick='Paging(" + i + ")'>" + i + "</a></li>");
                
                
            } %>
      </ul>
    </div>
  </td>
</tr>
        </tfoot>
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