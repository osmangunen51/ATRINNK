<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<NeoSistem.MakinaTurkiye.Management.Models.Entities.MemberDescription>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	BrowseDesc
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <button type="button" style="width: 100px;  float:right; height: 35px; margin-right:300px;margin-top:20px;" onclick="window.location='/Member/CreateDesc/<%:ViewData["mi"] %>'">
          Yeni Açıklama Girişi
        </button>

     <% foreach (var item in Model)
        { %>
        <div style="margin-top:20px;margin-left:25px;width:1000px;height:inherit;border:1px solid black;position:relative;">
        
        <%--<hr style="margin-top:12px; border:1px double black" />--%>
        <div style="  width:100%;height:20%; border-bottom:2px solid black; "><h2>  &nbsp;Açıklama Başlığı : <%=item.Title %></h2>
        <div style=" margin-top:-30px; margin-right:15px; float: right; width: 50px; height: 25px;">
        <a href='<%: Url.Action("DeleteDesc","Member",new { id=item.descId }) %>'>
    <img src='<%: Url.Content("~/Content/Images/delete.png") %>' />
        </a>&nbsp;
         <a href='<%: Url.Action("EditDesc","Member",new { id=item.descId }) %>'>
    <img src='<%: Url.Content("~/Content/Images/edit.png") %>' />
        </a>
      </div>
        </div>
    
        <div style="  width:100%;min-height:200px;height:auto; border-bottom:1px solid black;">
        
        <div style="float:left;width:15%; border-right:1px solid black;min-height:200px;">
        <br />
        
         <% string dtt = item.Date.Value.ToString();
            string dte;%>
        &nbsp;Oluşturulma Tarihi:<br />&nbsp;<%=dtt %>
        <br />
        <br />
        <%if (item.UpdateDate == null)
          {
              dte = "Değiştirilmedi...";%>
              
          <%}
          else
              dte = item.UpdateDate.Value.ToString();%>
         &nbsp;Değiştirilme Tarihi:<br />&nbsp;<%=dte %>
         <br />
         <br />
      

         <%--&nbsp;Üye Adı :<br />&nbsp;<%=ViewData["mem"] %>--%>
        </div>
        
        <div style="float:right; width:84%;  min-width:200px; margin-left:-100px;">
        <%=item.Description %>

     
      </div>
      <div style="clear:both;"></div>
         </div>
        
        </div>

     <%} %>
    <%-- <p>
        <%: Html.ActionLink("Yeni Açıklama Girişi", "Create") %>
    </p>--%>
   <%-- <table>
        <tr>
            <th></th>
            
            <th>
                Başlık
            </th>
           
            <th>
                Açıklama
            </th>
            <th>
                Tarih
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: Html.ActionLink("Edit", "Edit", new { id=item.descID }) %> |
                <%: Html.ActionLink("Details", "Details", new { id=item.descID })%> |
                <%: Html.ActionLink("Delete", "Delete", new { id=item.descID })%>
            </td>
            <td>
                <%: item.Title%>
            </td>
            <td>
                <%: item.Description %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.Date) %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Yeni Açıklama Girişi", "CreateDesc") %>
    </p>
    --%></div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
