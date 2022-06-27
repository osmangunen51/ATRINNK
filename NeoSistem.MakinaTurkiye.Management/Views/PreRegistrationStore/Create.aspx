<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.PreRegistrations.PreRegistrainFormModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Firma Ön Kayıt
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
    $('#StoreName').keypress(function (e) {
     if ($(this).val().length > 2) {
         SearchByName($(this).val(),$('#Email').val());
        }
        $('#Email').change(function (e) {
     if ($(this).val().length > 2) {
         SearchByName($('#StoreName').val(),$(this).val());
     }
});
       });      
        });   

        function SearchClick() {
            SearchByName($('#StoreName').val(), $('#Email').val());

        }

        function SearchByName(name,email) {
                        $.ajax({
                url: '/PreRegistrationStore/SerachByName',
                data: {
                    storename: name,
                    email:email

                },
                type: 'post',
                            success: function (data) {
                    $("#findedStore").html(data);
                },
                error: function (x, a, r) {
                    alert("Error");

                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div style="width: 30%; float: left; margin-left: 10px;">
            <%if (TempData["error"] != null)
        {%>
        <p style="font-size:15px;"><%=TempData["error"].ToString()%></p>
        <%} %>
        <%if (TempData["success"] != null)
            {%>
        <p style="font-size: 15px;">Firma Eklenmiştir</p>
        <%} %>
        <%using (Html.BeginForm("Create", "PreRegistrationStore", FormMethod.Post))
            { %>
        <%Html.RenderPartial("_FormModel", Model); %>
        <%} %>
    </div>
    <div style="float: right; width: 68%;" id="findedStore">
    </div>
    <div style="clear: both"></div>
</asp:Content>

