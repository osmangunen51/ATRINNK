<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<SearchModel<NeoSistem.MakinaTurkiye.Web.Models.Products.MTProductCommentItem>>" %>
<%foreach (var item in Model.Source)
    {%>
<div class="col-md-12  commentWrapper">
    <div class="col-md-2 hidden-xs">
        <div class="commentProfil">
            <%:item.MemberProfilPhotoString %>
        </div>
    </div>
    <div class="col-md-10">
        <div class="pull-left">
            <span class="memberName "><%:item.MemberNameSurname %></span>
            <%int rateRemind = 5 - item.Rate; %>
            <%for (int i = 0; i < item.Rate; i++)
                {%>
            <span class="glyphicon .glyphicon-star-empty glyphicon-star" style="color: #F28B00"></span>
            <%}%>

            <%for (int i = 0; i < rateRemind; i++)
                {%>
            <span class="glyphicon .glyphicon-star-empty glyphicon-star-empty" style="color: #F28B00;"></span>
            <%} %>
        </div>
        <div class="pull-right commentDate">
            <%:item.RecordDate %>
        </div>
        <div style="clear: both"></div>

        <div class="commentText">
            <%:item.CommentText %>
        </div>
        <div class="pull-left commentAdress hidden-xs">
            <%:item.Location %>
        </div>
    </div>
</div>

<%} %>

<div class="commentPagination row">    

    <div class="col-md-12" style="text-align:center;">
        <ul class="pagination">
<%foreach (var item in Model.TotalLinkPages)
    {
    
        if (item == Model.CurrentPage)
        {%>
            <li class="active"><span><%:item %></span> </li>
        <%}
            else { %>
            <li><a onclick="ProductCommentPage(<%:item %>)" style="cursor:pointer;"><%:item %></a></li>
            <%}

        %>
       
 <%   } %>
            </ul>
    
</div>

    </div>