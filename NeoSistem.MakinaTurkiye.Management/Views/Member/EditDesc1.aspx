<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.BaseMemberDescriptionModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Düzenle-Üye Açıklamaları
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
    <script type="text/javascript">
        function subContentCheck(cb) {
            console.log(cb.checked);


            if (cb.checked == true) {
                var text = cb.value + " ";

                CKEDITOR.instances["DescriptionNew"].insertHtml(text);
            }
            else {
                var value = CKEDITOR.instances['DescriptionNew'].getData()
                console.log(value);
                console.log(cb.value, 'val');
                var newValue = value.replace(cb.value, " ");
                console.log(newValue);
                CKEDITOR.instances["DescriptionNew"].setData(newValue);

            }
        }
        var data = [];
        $(document).ready(function () {


            $("#constantId").change(function () {
                $("#subContent").html("");

                var val = $("#constantId").val();
                $.ajax({

                    url: '/Constant/GetSubConstant/' + val,
                    type: 'GET',

                    success: function (data) {
                        $.each(data.data, function (key, value) {
                            $("#subContent").append("<input type='checkbox' onClick='subContentCheck(this)'  value='" + value + "'/>" + value + "<br>");
                        });

                    },
                    error: function (request, error) {

                    }
                });
            });
            $('#LastDateNew').datepicker().val();
            $('#form').submit(function (event) {
                var last = $("#LastDateNew").val();
                var time = $("#timeNew").val();

                var isLarger = fn_DateCompare(new Date(), last + " " + time);

                if (isLarger < 0) {
                    alert("Atama tarihi şuan ki tarihten küçük olamaz");
                    return false;
                }

            });

        });
        function UpdateDescriptionText() {

            if ($("#constantId").val() == "<%:Model.ConstantId%>") {

                $("#DescriptionLastC").show();
                //CKEDITOR.instances["DescriptionNew"].setData(text);
            }
            else {
                $("#DescriptionLastC").hide();
            }
        }

        String.prototype.toDate = function (format) {
            var normalized = this.replace(/[^a-zA-Z0-9]/g, '-');
            var normalizedFormat = format.toLowerCase().replace(/[^a-zA-Z0-9]/g, '-');
            var formatItems = normalizedFormat.split('-');
            var dateItems = normalized.split('-');

            var monthIndex = formatItems.indexOf("mm");
            var dayIndex = formatItems.indexOf("dd");
            var yearIndex = formatItems.indexOf("yyyy");
            var hourIndex = formatItems.indexOf("hh");
            var minutesIndex = formatItems.indexOf("ii");
            var secondsIndex = formatItems.indexOf("ss");

            var today = new Date();

            var year = yearIndex > -1 ? dateItems[yearIndex] : today.getFullYear();
            var month = monthIndex > -1 ? dateItems[monthIndex] - 1 : today.getMonth() - 1;
            var day = dayIndex > -1 ? dateItems[dayIndex] : today.getDate();

            var hour = hourIndex > -1 ? dateItems[hourIndex] : today.getHours();
            var minute = minutesIndex > -1 ? dateItems[minutesIndex] : today.getMinutes();
            var second = secondsIndex > -1 ? dateItems[secondsIndex] : today.getSeconds();

            return new Date(year, month, day, hour, minute, second);
        };
        function fn_DateCompare(DateA, DateB) {     // this function is good for dates > 01/01/1970

            var a = new Date(DateA);
            var b = new Date(DateB.toDate("dd/mm/yyyy hh:ii"));

            var msDateA = Date.UTC(a.getFullYear(), a.getMonth() + 1, a.getDate(), a.getHours(), a.getMinutes());
            var msDateB = Date.UTC(b.getFullYear(), b.getMonth() + 1, b.getDate(), b.getHours(), b.getMinutes());
            console.log(msDateA, msDateB);
            if (parseFloat(msDateA) < parseFloat(msDateB))
                return 1;  // lt
            else if (parseFloat(msDateA) == parseFloat(msDateB))
                return 0;  // eq
            else if (parseFloat(msDateA) > parseFloat(msDateB))
                return -1;  // gt
            else
                return null;  // error
        }
    </script>


    <style type="text/css">
        .editor-label {
            padding-bottom: 5px;
        }

        .editore-field {
            padding-bottom: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width:100%;">
        <div style="margin-left: 20px">
            <%
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
            %>
            <div style="color: #b70606; border: 3px solid #b70606; width: 500px; padding: 10px; font-size: 16px;">* <%:error.ErrorMessage %></div>
            <%}
                }
            %>
        </div>
         <% using (Html.BeginForm("EditDesc1", "Member", FormMethod.Post, new { @id = "form" }))
            {%>
        <br />
       <div style="width:100%">
           <div style="width: 40%; float: left;">
            <a style="font-size: 18px;margin-left:10px" target="_blank" href="/Store/EditStore/<%:Model.StoreID %>"><%=Model.StoreName%></a>
            <br />
            <fieldset style="height:382px;margin-top:10px;">
            <legend>
            </legend>
              <br />
             <label style="font-size: 16px;color:#458754;"><%=Model.StoreShortName %> </label><span>&nbsp;&nbsp;&nbsp;&nbsp;</span><label style="font-size:16px;text-transform: uppercase;color:#b70606;font-weight:bold"><%=Model.City %></label>
             <br />
              <br />
              <label style="color:#000000;font-weight:bold"><%:ViewData["MemberName"] %> </label><br /><br /><label style="color:#617889;font-weight:bold"><%=Model.Contact.Replace("<br>","<br><br>") %> </label> 
              <br />
              <br />
             <br />
             <a style="font-size: 13px;color:#489be3" target="_blank"  href="//<%=Model.StoreWebUrl%>">
                <label><%=Model.StoreWebUrl %></label>
             </a>
             <br />
             <%if (!string.IsNullOrEmpty(Model.StoreUrl))
                {%>
                  <br />
                    <a style="font-size: 13px;color:#489be3" target="_blank" href="<%=Model.StoreUrl%>">
                          <label><%=Model.StoreUrl%></label>
                     </a>
                  <br />
                <%}%>

        </fieldset>
           </div>
           <div style="width: 60%; float: left;">
               <div style="margin-left:80px;">
                     <label>Aranacak Kişi : </label>
                     <input id="txtContactName" type="text"  placeholder="Adı Soyadı" value="<%=Model.ContactNameSurname %>"  />
                     <input id="txtPhoneNumber" type="text" placeholder="Telefon" value="<%=Model.ContactPhoneNumber%>" />
                      <%if (!string.IsNullOrEmpty(Model.ContactNameSurname) | !string.IsNullOrEmpty(Model.ContactPhoneNumber))
                          {%>
                             <button type="button" id="btnContactChange" data-storemainPartyId="<%=Model.StoreID%>">Güncelle</button>
                          <%}
                          else
                          {%> 
                            <button type="button" id="btnContactChange" data-storemainPartyId="<%=Model.StoreID%>">Kaydet</button>
                          <%}
                       %>
                     <br />
             </div>
            <fieldset>
            <legend>
              <%: Html.Label("Yeni Açıklama :") %>
            </legend>
                <div class="editor-field">
                    <%: Html.TextArea("DescriptionNew", new {@style="width:100%; height:60px;" })%>
                </div>
                <%: Html.ValidationSummary(true) %>
                <%:Html.HiddenFor(x=>x.ID)%>
                <%:Html.HiddenFor(x=>x.InputDate) %>
                <%:Html.HiddenFor(x=>x.MainPartyId) %>
                <%:Html.HiddenFor(x=>x.RegistrationStoreId) %>
               <div  style="margin-top:2px;">
                    <table>
                        <tr>
                            <td colspan="4">
                                <table style="margin-top:20px">
                            <tbody>
                                <tr>
                                    <td style="width:40px;text-align:right">
                                      TS
                                    </td>
                                    <td style="width:5px">
                                        :
                                    </td>
                                    <td style="color:#2776e5;font-weight:800;width:150px">
                                        <%:Model.AuthorizeName %>
                                    </td>
                                   <td style="width:10px">

                                    </td>
                                    <td style="width:40px;text-align:right">
                                      PY
                                    </td>
                                    <td style="width:5px">
                                        :
                                    </td>
                                    <td style="color:#31c854;font-weight:800;width:150px">
                                        <%:Model.PortfoyName %>
                                    </td>
                                    <td style="width:150px">
                                        <div class="editor-field"><%:Html.CheckBoxFor(m=>m.IsFirst) %> Önemli 
                                         </div>
                                    </td>
                                    <td style="width:150px">
                                    <div class="editor-field">
                                        <%:Html.CheckBoxFor(m=>m.IsImmediate) %> Çok Acil 
                                    </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                        </td>
                    </tr>
               
                    <tr>
                        <td>
                                               <div class="editor-label">
                    <%: Html.Label("Yeni Başlık :") %>
                </div>
                <div class="editor-field">
                    <%:Html.DropDownList("constantId",Model.ConstantModel,new {@onchange="UpdateDescriptionText()" }) %>
                </div>
                        </td>
                        <td>
                    <div class="editor-label">
                        Yeni Hatırlatma Tarihi
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBox("LastDateNew",DateTime.Now.Date.ToString("dd.MM.yyyy"),new {@placeholder="Tarih Giriniz",@autocomplete="off"}) %>
                        <%: Html.ValidationMessageFor(model => model.Description) %>
                    </div>
                        </td>
                        <td>
                                                  <div class="editor-field">
                        Yeni Hatırlatma Saati
                    </div>
                    <div class="editor-field">

                        <select name="timeNew" style="width: 150px;" id="timeNew">
                            <option value="">Seçiniz</option>
                            <%for (int i = 9; i <= 17; i++)
                                {
                                    for (int a = 0; a <= 2; a++)
                                    {
                                        if (a == 0)
                                        {
                                            Response.Write("<option value='" + i + ":00'>" + i + ":00</option>");
                                        }
                                        else if (a == 1)
                                        {
                                            Response.Write("<option value='" + i + ":15'>" + i + ":20</option>");

                                        }
                                        else if (a == 2)
                                        {
                                            Response.Write("<option value='" + i + ":30'>" + i + ":40</option>");
                                        }
                                        //else
                                        //{
                                        //    Response.Write("<option value='" + i + ":45'>" + i + ":45</option>");
                                        //}

                                    }
                                }%>
                        </select>
                    </div>
                        </td>
                        <td>
                                                           <div class="editor-label">
                        Atama Yap
                    </div>
                    <div class="editor-field">
                        <%:Html.DropDownList("userId",Model.Users) %>
                    </div>
                        </td>
   
                    </tr>
                    <tr>
                    <td colspan="3">
                    <div id="subContent" style="width:500px;">
                            <%foreach (var item in Model.SubConstants)
                                {
                                     %>
                                 <input type='checkbox' onclick='subContentCheck(this)' value='<%:item %>' /><%:item %> <br>
                                <%} %>
                    </div>
                    </td>
                    <td>
                    <% using (Html.BeginForm())
                            { %>
                        <div style="float: left; margin-left: 15px; margin-top:10px">
                            <button type="submit"  style="width: 90px; height: 35px; border: 1px solid #404241; background: #dfefcc;">
                                <b>Atama Yap</b>
                            </button>
                        </div>
                        <%if (Model.LastDate == null && Model.BaseMemberDescriptionByUser.Where(x => x.LastDate != null).Count() == 0)
                            {%>
                        <div style="float: right; margin-left: 15px;margin-top:10px"">
                            <button type="button" style="width: 120px; height: 35px; background-color: #617cca;" onclick="window.location='/Member/CreateDesc1/<%:Model.MainPartyId %>?descId=<%:Model.ID %>'">
                                Yeni Açıklama Girişi
                            </button>
                        </div>
                        <% } %>
                        <% } %>
                    <br />
                    <br />
                    <br />
                    
                </td>
            </tr>
            </table>
            </div>
            </fieldset>
           </div>
       </div>
       <br />
    <% } %>
    <div>
    <br />
    </div>
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
        <thead>
            <tr>
                <td class="Header HeaderBegin">Üye Adı
                </td>
                <td class="Header">Firma Adı</td>
                <td class="Header">Başlık</td>
                <td class="Header">İçerik</td>
                <td class="Header">İşlem Tarihi</td>
                <td class="Header">Hatırlatma Tarihi</td>
                <td class="Header">Atayan</td>
                <td class="Header">Atanan</td>
                <td class="Header">Kayıt Türü</td>
                <td class="Header HeaderEnd">Araçlar</td>
            </tr>
        </thead>
        <tbody>
            <%int row = 0; %>
            <%if (Model.BaseMemberDescriptionByUser.Count > 0)
                { %>
            <%foreach (var itemMemberDesc in Model.BaseMemberDescriptionByUser)
                {
                    row++;
            %>
            <tr id="row<%:itemMemberDesc.ID %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
                <td class="Cell CellBegin">
                    <%:Html.Truncate(ViewData["MemberName"].ToString(),15)%>..</td>
                <td class="Cell"><%if (Model.StoreName != "")
                                     {
                %>
                    <%if (!itemMemberDesc.StoreID.HasValue)
                        {%>
                    <%:itemMemberDesc.StoreName %>
                    <% }
                        else
                        { %>
                    <a target="_blank" href="/Store/EditStore/<%:itemMemberDesc.StoreID %>"><%:Html.Truncate(Model.StoreName,20)%></a>

                    <%} %>
                    <%}%></td>
                <td class="Cell"><%:itemMemberDesc.Title %></td>
                <td class="Cell" style="font-size: 15px;"><%:Html.Raw(itemMemberDesc.Description)
                %>
                </td>
                <td class="Cell"><% if (itemMemberDesc.InputDate != null)
{
                                         string[] Inputdate = itemMemberDesc.InputDate.ToString().Split(' ');
                                         Response.Write(Inputdate[0] + " ");
                                         Response.Write("<font style='color:#C5D5DD'>" + Inputdate[1] + "</font>");
                                     }%></td>
                <td class="Cell"><%if (itemMemberDesc.LastDate.ToDateTime().Date >= DateTime.Now.Date)
                                     {
                                         string[] lastDate = itemMemberDesc.LastDate.ToString().Split(' ');
                                         Response.Write(lastDate[0] + " ");
                                         Response.Write("<font style='color:#C5D5DD'>" + lastDate[1] + "</font>");
                                     } %></td>
                <td class="Cell" style="background-color:#8ed9fa;color:#3f2828;">
                    <%:itemMemberDesc.UserName %>
                </td>
                <td class="Cell" style="background-color:#bff5cb;color:#3f2828;">
                    <%:itemMemberDesc.ToUserName %>
                </td>
                <td class="Cell">
                    <%string type = "Normal";
                        if (!itemMemberDesc.StoreID.HasValue)
                        { type = "Ön Kayıt"; }%>
                    <%:type %>
                </td>
                <td class="CellEnd">
                    <%if (itemMemberDesc.Description != "Mail" && !string.IsNullOrEmpty(itemMemberDesc.Description))
                        {%>
                    <a href="/Member/EditDesc1/<%:itemMemberDesc.ID %>">
                        <img src="/Content/images/ac.png" />
                    </a>
                    <% } %>

                    <a style="cursor: pointer;" onclick="DeletePost(<%:itemMemberDesc.ID %>);">
                        <img src="/Content/images/delete.png" hspace="5" />
                    </a>
                </td>
            </tr>
            <%} %>

            <%} %>
        </tbody>
    </table>
   <script type="text/javascript" defer="defer">
      

       CKEDITOR.replace('DescriptionNew', {
           toolbar: [
               //{ name: 'document', items: ['Source', '-', 'NewPage', 'Preview', '-', 'Templates'] },	// Defines toolbar group with name (used to create voice label) and items in 3 subgroups.
               //['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'],			// Defines toolbar group without name.
               '/',																					// Line break - next group will be placed in new line.
               { name: 'basicstyles', items: ['Bold', 'NumberedList'] },
               
           ],
           height: '135px'
       });

       function InitInformationUpdate()
       {
           var txtContactName = $('#txtContactName').val();
           var txtPhoneNumber = $('#txtPhoneNumber').val();
           var content = '<p>Aranan Kişi : <strong style="color:red;font-weight:bold">' + txtContactName + ' ' + txtPhoneNumber +'</strong></p><br>';
           CKEDITOR.instances["DescriptionNew"].setData(content);
       }

       // InitInformationUpdate();
      

       $('#btnContactChange').click(
           function () {
               var storemainPartyId = $(this).attr("data-storemainPartyId");
               var txtContactName = $('#txtContactName').val();
               var txtPhoneNumber = $('#txtPhoneNumber').val();
               var dt = { storemainPartyId: storemainPartyId, name: txtContactName, number: txtPhoneNumber };
               $.ajax({
                   url: '/Member/ChangeContactInformation',
                   data: dt,
                   type: 'post',
                   dataType: 'json',
                   success: function (data) {
                       if (data.IsSuccess) {
                           InitInformationUpdate();
                       }
                       else {

                       }
                   }
               });
           });

       //CKEDITOR.replace('DescriptionLast', {
       //    toolbar: [
       //        //{ name: 'document', items: ['Source', '-', 'NewPage', 'Preview', '-', 'Templates'] },	// Defines toolbar group with name (used to create voice label) and items in 3 subgroups.
       //        //  ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'],			// Defines toolbar group without name.
       //        '/',																					// Line break - next group will be placed in new line.
       //        { name: 'basicstyles', items: ['Bold', 'NumberedList'] }
       //    ]
       //});
       //<![CDATA[

       //]]>
       function DeletePost(descId) {
           if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
               $.ajax({
                   url: '/Member/DeleteDescription',
                   data: { id: descId },
                   type: 'post',
                   dataType: 'json',
                   success: function (data) {

                       if (data) {
                           $('#row' + descId).hide();
                       }
                   }
               });
           }
       }

       //CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
   </script>
</asp:Content>


