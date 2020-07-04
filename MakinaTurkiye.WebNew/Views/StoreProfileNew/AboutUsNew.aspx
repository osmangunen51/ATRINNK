<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StoreProfile.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTStoreProfileAboutUsModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <%=Html.RenderHtmlPartial("_HeaderContent") %>
</asp:Content>
    <asp:Content ID="Content6" ContentPlaceHolderID="StoreProfileHeaderContent" runat="server">
                  <%if (Model.StoreActiveType == 2)
        {  %>
            <%} %>
        <%=Html.RenderHtmlPartial("_HeaderTop",Model.MTStoreProfileHeaderModel) %>
    </asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="StoprofileMenu" runat="server">
           <%if (Model.StoreActiveType == 2)
        {  %>
    <%=Html.RenderHtmlPartial("_LeftMenu", Model.MTStoreProfileHeaderModel)%>

    <%} %>
    </asp:Content>


 <asp:Content  runat="server" ContentPlaceHolderID="StoreProfileContent" ID="Content7">
     <%if(Model.StoreActiveType==2) {%>
     <div class="col-sm-7 col-md-8 col-lg-9">
            <div class="StoreProfileContent aboutusText">
                  <%=Model.GeneralText %>
              </div>

     <div class="clearfix"></div>       
     </div>   
        
    <%} else{%>
       <%=Html.Action("NoAccessStore",new{id=Model.MainPartyId}) %>
    <%} %>

</asp:Content>