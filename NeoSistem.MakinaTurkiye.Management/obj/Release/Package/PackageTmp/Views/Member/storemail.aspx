<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<ICollection<ConstantModel>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>storemail</title>
    <script type="text/javascript">
   
      function gonder() {
        $('#gonderr').hide();
        $('#divLoading').show();
       }

    </script>
</head>
<body>
  
   <%using (Html.BeginForm())
     {%>
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
          <div id="button3" style="float: left; margin-left: 10px;">
          <button type="submit" class="btnOnayla" onclick="gonder();">
          Mail Gönder
          </button>
        </div>
        </div>
        <div style="float: left; margin-left: 270px; margin-top:200px; width:580px; height:400px; display: none;" id="divLoading">
        <div style="float: left; font-size: 12px;">
         Mail Gönderiliyor..</div>
        <div style="float: left; margin-left: 10px; margin-top: 2px;">
          <img src="/Content/Images/load.gif"  alt=""/></div>
      </div>
        <%} %>
  
</body>
</html>
