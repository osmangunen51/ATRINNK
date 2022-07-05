<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<MailTemplateFormModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Kulanıcı Özel Mail Ekle
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
  <% Html.EnableClientValidation();%>
  <%using (Html.BeginForm())
               {%>
 <%=Html.RenderHtmlPartial("_CreateMailTemplateForm", Model) %>
  <%} %>
      <div style="width: 60%; margin: 0 auto;">
    <table cellpadding="5" cellspacing="0" class="TableList" style="width: 100%; margin-top: 5px">
      <thead>
        <tr>
          <td class="Header HeaderBegin" unselectable="on" >
            #
          </td>
          <td class="Header" width="200px" unselectable="on">
            Mail Template Adı
          </td>
            <td class="Header">
                Konu
            </td>
            <td class="Header">
                Kulanıcı Grubu
            </td>
           <td class="Header HeaderEnd">
            Araçlar
            </td>
 
        </tr>
      </thead>
      <tbody id="table">
          <%int counter = 1; %>
          <%foreach (var mailITem in Model.UserMailTemplateListItemModels)
              {%>

                  <tr id="row-<%:mailITem.Id %>">
        <td class="Cell CellBegin"><%:counter %></td>
            <td class="Cell"><%:mailITem.Name %></td>
            <td class="Cell"><%:mailITem.Subject %></td>
            <td class="Cell"><%:mailITem.UserGroupName %></td>
            <td class="Cell CellEnd">
               <a href="javascript:void(0)" onclick="DeleteMailTemplate(<%:mailITem.Id %>)"> <img src="/Content/images/delete.png" hspace="5" /></a>
                <a href="/User/CreateMailTemplate/<%:Model.UserId%>?tempId=<%:mailITem.Id %>">  <img src="/Content/images/edit.png" hspace="5"/></a>

            </td>
        </tr>
              <% counter = counter + 1;
                  } %>

      </tbody>

    </table>
  </div>

        <script type="text/javascript" defer="defer">
        var editor = CKEDITOR.replace('MailContent', { toolbar: 'webtool' });
            CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');


            
            function DeleteMailTemplate(id) {
                if (confirm('Mail Template Silmek İstiyormusunuz?')) {
                                             $.ajax({
          url: '/User/DeleteMailTemplate',
          data: {
         ID: id 
          },
          type: 'post',
         success: function (data) {
             $("#row-" + id).hide();

          },
               error: function (x, a, r) {
                   alert("Error");
         
          }
        });

              }

          }
    </script>
</asp:Content>
