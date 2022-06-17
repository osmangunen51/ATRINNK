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
     <div id="gonderr">
              <div id="cons" class="plusContent">
                  <%if (TempData["StoreEmailError"] != null) {%>
                    <div style="font-size:16px; color:#780000">
                        <%:TempData["StoreEmailError"].ToString() %>*
                    </div>
                    
                  <% } %>
            <ul style="list-style: none; float: left; padding: 0px; width: 580px; border: solid 1px #8fc0d1;
              padding-top: 10px; padding-left: 10px; background-color: #f8fbff">
              <% foreach (var item2 in Model)
                 { %>
              <li style="float: left; width: 240px;">
                <div style="width: auto; float: left; height: auto;">
                  <div style="width: auto; float: left; height: auto;">
                    <%: Html.CheckBox("RelatedCategory", false, new { value = item2.ConstantId, @class = "ActivityName", style = "border:solid 1px #c9e6e2;padding:0px; height:15px;" })%>
                  </div>
                  <div style="width: auto; float: left; margin-left: 5px; margin-top: 5px; font-size: 12px;
                    color: #000">
                    <%: item2.ConstantName%>
                  </div>
                </div>
              </li>
              <% } %>
            </ul>
          </div>
         <table>
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
                                  Benim Mailim İle Gönder 
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
    <div id="pnlmail">
        <fieldset>
            <legend> Mail İçeriği </legend>
            <%: Html.TextArea("mailcontent", new {id="mailcontent",@style="width:100%; height:60px;" })%>
            <hr />
            <div style="width100%;text-align:right">
                 <button type="button" class="btnOnayla">
                        Gönder
                  </button>
            </div>
        </fieldset>
    </div>
        <%} %>

    <script type="text/javascript" src="https://code.jquery.com/jquery-3.6.0.js"></script>
    <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
    <script type="text/javascript" defer="defer">
        CKEDITOR.replace('mailcontent',
            {
                toolbar: [
                    //{ name: 'document', items: ['Source', '-', 'NewPage', 'Preview', '-', 'Templates'] },	// Defines toolbar group with name (used to create voice label) and items in 3 subgroups.
                    //['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'],			// Defines toolbar group without name.
                    '/',																					// Line break - next group will be placed in new line.
                    { name: 'basicstyles', items: ['Bold', 'NumberedList'] },
                ],
                height: '135px'
            });
    </script>
    <script type="text/javascript">
        function gonder(tip) {
            if (tip == 1) {
                $('#tip').val(tip);
                $('#frmmailsend').submit();
                $('#gonderr').hide();
                $('#divLoading').show();
            }
            else {
                var data = {
                    id: "107166",
                    RelatedCategory:
                        $("#RelatedCategory").val(),
                };
                $.ajax({
                    type: 'POST',
                    url: '/Member/GetCreateMailForm',
                    data: data,
                    dataType: 'json',
                    success: function (result) {
                        if (result.IsSuccess) {
                            $('#mailcontent').val(result.Result);
                            $('#pnlmail').show();
                        }
                        else {
                            alert(result.Message);
                        }
                    }
                });
                event.preventDefault();
            }
        }
        $('#pnlmail').hide();
    </script>
</body>
</html>
