<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="System.Web.Mvc.ViewPage<AllSectorProductsModel>" %>


<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <%foreach (var item in Model.Sectors)
          {%>
            <%:item.CategoryName %> 
            <%:item.CategoryId %>
    <%foreach (var product in Model.Products.Where(x=>x.SectorId==item.CategoryId))
      {%>
          <%:product.ProductName %>
            <%:product.PicturePaths[0]%><!-- px100   -->
            <%:product.PicturePaths[1]  %><!-- //px100x75   -->
            <%:product.PicturePaths[2] %><!--  //px180x135   -->
            <%:product.PicturePaths[3]  %><!-- //x160x120   -->
            <%:product.PicturePaths[4]  %><!-- //px400x300   test asda asd a-->
     <% } %>
          <%} %>
</asp:Content>
