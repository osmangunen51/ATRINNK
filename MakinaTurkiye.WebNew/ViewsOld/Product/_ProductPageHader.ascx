<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTProductPageHeaderModel>" %>
<input type="hidden" id="hdnProductId" value="<%:Model.ProductId %>" />
<input type="hidden" id="hdnMainPartyId" value="<%:Model.MainPartyId %>" />
<input type="hidden" id="hdnMemberEmail" value="<%:Model.MemberEmail %>" />

<div class="row">
    <div class="col-xs-12">
        <%=Model.Navigation %>
    </div>
</div>
<div class="row">
    <div class="col-xs-12" >
        
        <%if(!string.IsNullOrEmpty(Model.preProductUrl)){ %>
        <div style="float:left;"><a href="<%:Model.preProductUrl %>" title="Önceki Ürün"  data-toggle="tooltip"><i class="fa fa-long-arrow-left fa-3x" style="color:#808080" aria-hidden="true" ></i></a></div>
        <%} if(!string.IsNullOrEmpty(Model.nextProductUrl)){%>
        <div style="float:right;"><a href="<%:Model.nextProductUrl %>" title="Sonraki Ürün"  data-toggle="tooltip"><i class="fa fa-long-arrow-right fa-3x" style="color:#808080" aria-hidden="true"></i></a></div>
        <%} %>
        <div style="clear:both"></div>
        <div class="page-header mt0">
            <h1 class="mt0 di2" itemprop="name">
                <%:Model.ProductName%>
            </h1>

             <%if (Model.IsActive)
                {%>
                <div class="pull-right">
                    <%string removeFavoriProductCss = "";  %>
                    <%string addFavoriProductCss = "";  %>
                    <% if (Model.IsFavoriteProduct)
                        { %>
                    <% addFavoriProductCss = addFavoriProductCss + "display:none;"; %>
                    <% }
                        else
                        { %>
                    <% removeFavoriProductCss = removeFavoriProductCss + "display:none;"; %>
                    <% } %>
                    <a href="#" id="aRemoveFavoriteProduct" onclick="RemoveFavoriteProduct('<%=Model.ProductId %>','<%=Model.ProductName %>');"
                        style="<%=removeFavoriProductCss %>"><span class="glyphicon glyphicon-ok"></span>&nbsp;Favorilerimden Kaldır</a>


                    <a href="#" id="aAddFavoriteProduct" style="<%=addFavoriProductCss %>" onclick="AddFavoriteProduct('<%=Model.ProductId %>','<%=Model.ProductName %>');"><span class="glyphicon glyphicon-star text-warning"></span>&nbsp;Favorilerime Ekle</a>
                   
                    <a href="#" data-toggle="modal" data-target="#PostCommentsModal" id="PostCommentMessage"></a>
                    <a href="#" data-toggle="modal" data-target="#PostCommentsModalOtherCountry" id="PostCommentMessageCountry"></a>
                    <a href="#" data-toggle="modal" data-target="#LoginModal"></a>
                    &nbsp;&nbsp; <a data-rel="print"><span class="glyphicon glyphicon-print text-info"></span>&nbsp;Yazdır</a>&nbsp;&nbsp;<a data-toggle="modal" data-target="#ComplaintModal" id="ComplaintProduct" href="#"><span class="glyphicon glyphicon-flag text-danger"></span>&nbsp;Şikayet Et </a>&nbsp;&nbsp;
                </div>
            <%}%>
        </div>
    </div>
</div>
