<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<MembershipViewModel>" %>




<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

        <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%
                NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuModel lefMenu = (NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.LeftMenuModel)ViewData["leftMenu"];
            %>
            <%= Html.RenderHtmlPartial("LeftMenu",lefMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">Kurumsal Üyelik 2. adı
            </h4>
        </div>
    </div>
     <div class="row">
          <div class="col-md-12 col-sm-12">

			 <div class="col-md-9 col-sm-12">
                <div class="well well-mt2">


                    </div>
                 </div>
              </div>
         </div>

</asp:Content>

