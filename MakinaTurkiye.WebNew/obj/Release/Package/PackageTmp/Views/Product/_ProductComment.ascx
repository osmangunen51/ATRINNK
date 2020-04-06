﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.Products.MTProductTabModel>" %>
<div class="pull-left">
<span style="font-size:18px; font-weight:600;"><%:Model.ProductName %> Yorumları</span>
    </div>

<div style="clear:both"></div>
<hr />
<div class="row">
    <div class="col-md-8">
        <div class="form-horizontal" id="CommentForm">
            <%if (AuthenticationUser.Membership.MainPartyId != 0)
                {%>
            <div class="form-group">
                <label class="col-md-3">Puan</label>
                <div class="col-md-9">
                    <div id="stars-existing" class="starrr" data-rating='4'></div>
                    <span id="displayRateError" style="color: #b10606; display: none;" class="small">Lütfen puan seçiniz.</span>
                    <input type="hidden" id="rateProduct" value="0" />

                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3">Yorum</label>
                <div class="col-md-9">
                    <textarea style="height: 100px; font-size: 14px; font-family: Roboto; border-radius: 0!important;"
                        name="CommentText" id="CommentText" class="form-control" placeholder="Yorumunuz.."></textarea>
                </div>
            </div>
            <div class="form-group">
                <label class="col-md-3">
                </label>
                <div class="col-md-9">
                    <a class="btn background-mt-btn" style="cursor: pointer" id="btnProductComment"><i class="fa fa-comments" style="color: #fff"></i>Yorum Yap</a>
                </div>
            </div>
            <% }
                else
                { %>
            <div class="form-group">
                <label class="col-md-3">
                </label>
                <div class="col-md-9">
                    <a class="btn background-mt-btn" style="cursor: pointer" id="btnProductCommentInValid"><i class="fa fa-comments" style="color: #fff"></i>Yorum Yap</a>
                </div>
            </div>
            <%} %>
        </div>
        <div class="col-md-3">
        </div>
        <div class="col-md-9">
            <div id="CommentedAlert" class="alert alert-success" style="display: none;">
                Yorumunuz iletilmiştir. Bir kaç gün içinde incelenerek onaylanacaktır. 
                                          Yorumumunuzun onaylanması ürün hakkında olup olmadığına göre değişmektedir.

            </div>
        </div>

    </div>
</div>
<div class="row">
 
    <%if (Model.MTProductComment.MTProductCommentItems.Source.Count > 0)
        {%>
       <div id="productCommentContainer">
        <%=Html.RenderHtmlPartial("_ProductCommentList",Model.MTProductComment.MTProductCommentItems) %>
        </div>
        <% } %>

    <%else {%>
    <div class="col-md-12">
        <div class="alert" style="background-color:#f3f3f3; color:#333; font-size:13px;">
          Bu ürüne yapılmış herhangi bir yorum bulunmamaktadır.İlk yorum yapan sen ol!
        </div>
        </div>
    <% } %>
</div>
