﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<FilterModel<global::MakinaTurkiye.Entities.StoredProcedures.Members.MemberDescriptionTaskItem>>" %>

<asp:Content ID="Content4" ContentPlaceHolderID="TitleContent" runat="server">
    Görev Atamaları
    
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui-state-highlight ui-corner-all loadingContent ui-helper-hidden" style="margin-top: 200px; border-width: 5px;"
        id="preLoading">
        <span style="float: left; margin-right: 0.3em" class="ui-icon ui-icon-info"></span>
        <strong>Yükleniyor.</strong> Lütfen bekleyiniz...
    </div>
    <%string userId = ViewData["UserId"].ToString(); %>
    <%:Html.Hidden("UserId",userId) %>

    <div style="width: 100%; margin: 0 auto;">
        <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
            <thead>
                <tr>
                    <td class="Header HeaderBegin" onclick="OrderPost('descId');">#</td>
                    <td class="Header">Üye Adı
                    </td>

                    <td class="Header">Firma Adı</td>
                    <td class="Header">Başlık</td>
                    <td class="Header">Açıklama</td>
                    <td class="Header">Oluşuturulma Tarihi</td>
                    <td class="Header" onclick="OrderPost('UpdateDate');">Hatırlatma Tarihi</td>
                    <td class="Header">Atayan</td>
                    <td class="Header">Atanan</td>
                    <td class="Header">Kayıt T</td>
                    <td class="Header HeaderEnd ">Araçlar</td>
                </tr>
                <tr style="background-color: #F1F1F1">
                    <td class="CellBegin" align="center"></td>
                    <td class="Cell" align="center"></td>
                    <td class="Cell" align="center"></td>
                    <td class="Cell" align="center">
                        <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td style="">
                                        <%var constantList = (List<SelectListItem>)ViewBag.Constants; %>
                                        <%:Html.DropDownList("ConstantId",constantList,new {id="ConstantId",@onchange="SearchPost();"  }) %>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell"></td>
                    <td class="Cell">
                                                <table style="width: 100%;" border="0" cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>

                                    <td style="border: solid 1px #CCC; background-color: #FFF">
                                        <input id="CreatedDate" class="Search date" autocomplete="off" style="width: 70%; border: none" />
                                        <span class="ui-icon ui-icon-close searchClear" onclick="ClearSearch('UpdateDate');"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </td>
                    <td class="Cell" style="width: 7%;">
                        <table style="width: 100%;" border="0" cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>

                                    <td style="border: solid 1px #CCC; background-color: #FFF">
                                        <input id="UpdateDate" class="Search date" autocomplete="off" style="width: 70%; border: none" />
                                        <span class="ui-icon ui-icon-close searchClear" onclick="ClearSearch('UpdateDate');"></span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                    <td class="Cell"></td>

                    <td class="Cell">
                        <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                            <tbody>
                                <tr>
                                    <td style="">
                                        <%var userList = (List<SelectListItem>)ViewBag.Users; %>
                                        <%:Html.DropDownList("userId",userList,new {id="UserIdDropdown",@onchange="SearchPost();"  }) %>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                         <td class="Cell"></td>
                    <td class="Cell"></td>

                </tr>
            </thead>
            <tbody id="data">
                <%=Html.RenderHtmlPartial("_AllTaskListItem",Model) %>
            </tbody>
        </table>
            
    </div>


</asp:Content>

<asp:Content ID="Content6" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">
        $(document).ready(function () {
            $('#UpdateDate').datepicker().val();
           $('#CreatedDate').datepicker().val();
            $('.Search').keyup(function (e) {
                if (e.keyCode == 13) {
                    $('#OrderType').val('1');
                    $('#OrderName').val($(this).attr("id"));
                    SearchPost();
                }
            });

            toggleDescription();
        });
        function OrderPost(ordercolumn) {
            $('#OrderType').val(($('#OrderType').val() == '1' ? '0' : '1'));
            $('#OrderName').val(ordercolumn);
            SearchPost();
        }

        function SearchPost() {
                      if ($("#UserIdDropdown").val()) {
        
                        $("#UserId").val($("#UserIdDropdown").val());
            }

            $("#preLoading").show();
            $.ajax({
                url: '/MemberDescription/AllTask',
                data: {
                    UserId: $('#UserId').val(),
                    Page: $("#CurrentPage").val(),
                    OrderColumn: $('#OrderName').val(),
                    OrderType: $("#OrderType").val(),
                    Date: $("#UpdateDate").val(),
                    ConstantId: $("#ConstantId").val(),
                    CreatedDate: $("#CreatedDate").val()
                },
                type: 'post',
                success: function (data) {
                    $('#data').html(data);


                    $('#preLoading').hide();
                },
                error: function (x, a, r) {
                    $('#preLoading').hide();
                    //$('#table').html(x.responseText);
                }
            });
        }
        function ClearSearch(name) {
            $("#" + name).val('');

        }
        function PagePost(page) {
            $("#CurrentPage").val(page);
            SearchPost();
        }

        function toggleDescription() {
            $( ".SeeAll").click(function () {
                $(this).closest("#description-container").css("height", "auto").css("owerflow", "");
            });
        }


    </script>

 <link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
  <script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox-1.js"></script>
    <script type="text/javascript">
	 $(function () {
		$.superbox.settings = {
		  closeTxt: "Kapat",
		  loadTxt: "Yükleniyor...",
		  nextTxt: "Sonraki",
		  prevTxt: "Önceki"
		};
		$.superbox();
	 });
	 </script>
		<style type="text/css">
		/* Custom Theme */
		#superbox-overlay{background:#e0e4cc;}
		#superbox-container .loading{width:32px;height:32px;margin:0 auto;text-indent:-9999px;background:url(styles/loader.gif) no-repeat 0 0;}
		#superbox .close a{float:right;padding:0 5px;line-height:20px;background:#333;cursor:pointer;}
		#superbox .close a span{color:#fff;}
		#superbox .nextprev a{float:left;margin-right:5px;padding:0 5px;line-height:20px;background:#333;cursor:pointer;color:#fff;}
		#superbox .nextprev .disabled{background:#ccc;cursor:default;}
	</style>
</asp:Content>


