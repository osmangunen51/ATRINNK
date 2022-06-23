<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ICollection<ConstantModel>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>storemail</title>
</head>
<body>  
   <%using (Html.BeginForm(null, null, FormMethod.Post, new { id = "frmmailsend" }))
     {%>
     <input id="tip" name="tip" type="hidden" value="1" />
                  <div id="cons" class="plusContent">
                  <%if (TempData["StoreEmailError"] != null) {%>
                    <div style="font-size:16px; color:#780000">
                        <%:TempData["StoreEmailError"].ToString() %>*
                    </div>
                    
                  <% } %>
                  
            
          </div>
         <br />
     <div id="gonderr">
         <div style="column-count:3;
       -moz-column-count:3;
       -webkit-column-count:3;
       background-color: #f8fbff;
       border:
       solid 1px #808080;
       padding-top: 10px;
       padding-bottom: 10px;
       ">
                      <ul style="list-style:none;background-color: #f8fbff;margin-top:0px;">
              <% foreach (var item2 in Model.OrderBy(x=>x.Order))
                 { %>
                    <li style="float:left;width:90%">
                          <div style="width:15px;float: left;">
                            <%: Html.CheckBox("RelatedCategory", false, new { value = item2.ConstantId, @class = "ActivityName", style = "border:solid 1px #c9e6e2;padding:0px; height:15px;" })%>
                          </div>
                          <div style="float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                            color: #000">
                            <%: item2.ConstantName%>
                          </div>
                    </li>
                    <%--  <li style="width:25%;">
                        <div style="width: auto; float: left; height: auto;">
                          <div style="width: auto; float: left; height: auto;">
                            <%: Html.CheckBox("RelatedCategory", false, new { value = item2.ConstantId, @class = "ActivityName", style = "border:solid 1px #c9e6e2;padding:0px; height:15px;" })%>
                          </div>
                          <div style="width: auto; float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                            color: #000">
                            <%: item2.ConstantName%>
                          </div>
                        </div>
                      </li>--%>
              <% } %>
            </ul>
                  </div>
         <table style="float:left;">
             <tbody>
                 <tr>
                     <td>
                         <div id="button3" style="float: left; margin-left: 10px;">
                          <button type="button" class="btnOnayla" onclick="gonder(1);">
                                  Sistem Maili İle Gönder
                          </button>
                        </div>
                     </td>
                      <td>
                         <div id="button4" style="float: left; margin-left: 10px;">
                          <button type="button" class="btnOnayla" onclick="gonder(2);">
                                  Düzenle | Benim Mailim İle Gönder 
                          </button>
                        </div>
                     </td>
                 </tr>
             </tbody>
         </table>
        </div>
        <div style="float: left; margin-left: 270px; margin-top:200px; width:580px; height:400px; display: none;" id="divLoading">
        <div style="float: left; font-size: 12px;">
         Mail Gönderiliyor..</div>
        <div style="float: left; margin-left: 10px; margin-top: 2px;">
          <img src="/Content/Images/load.gif"  alt=""/></div>
      </div>
    <br />
    <div id="pnlmail" style="width:100%;height:100%;min-height:480px">
        <h2>Mail İçeriği</h2>
        <%: Html.TextArea("mailcontent", new {id="mailcontent",@style="width:100%; height:60px;" })%>
                    <div style="width100%;text-align:right;margin-top:10px">
                 <button type="button" onclick="mailpenceresikapat();">
                        Kapat
                  </button>
                 <button type="button" onclick="gonder(3);">
                        Gönder
                  </button>
            </div>


    </div>
        <%} %>

    <script type="text/javascript" src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
    <script type="text/javascript" defer="defer">
        CKEDITOR.replace('mailcontent',
            {
                toolbar: 'webtool',
                extraAllowedContent: 'table{*}',
                height:'300px',
                width:'100%',
            });
    </script>
    <script type="text/javascript">
        function mailpenceresikapat(tip)
        {
            $('#pnlmail').hide();
            $("#gonderr").show();
        }
        function gonder(tip)
        {
            if (tip == 1) {
                $('#tip').val(tip);
                $('#frmmailsend').submit();
                $('#gonderr').hide();
                $('#divLoading').show();
            }
            else if (tip == 2) {

                var RelatedCategory = [];
                $("input:checkbox:checked").each(function () {
                    RelatedCategory.push($(this).val());
                });
                console.log(RelatedCategory);
                var data = {
                    id: "<%=ViewData["storemailid"]%>",
                    RelatedCategory: RelatedCategory,
                };
                $.ajax({
                    type: 'POST',
                    url: '/Member/GetCreateMailForm',
                    data: data,
                    dataType: 'json',
                    success: function (result) {
                        if (result.IsSuccess) {
                            CKEDITOR.instances["mailcontent"].setData(result.Result);
                            $('#pnlmail').show();
                            $("#gonderr").hide();
                        }
                        else {

                        }
                    }
                });
                event.preventDefault();
            }
            else if(tip==3)
            {
                    $('#tip').val(tip);
                    $('#frmmailsend').submit();
                    $('#pnlmail').hide();
                    $('#gonderr').hide();
                    $('#divLoading').show();
            }
        }
        $('#pnlmail').hide();
    </script>
</body>
</html>
