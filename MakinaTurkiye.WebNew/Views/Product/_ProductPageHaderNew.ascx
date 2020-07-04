<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTProductPageHeaderModel>" %>
<input type="hidden" id="hdnProductId" value="<%:Model.ProductId %>" />
<input type="hidden" id="hdnMainPartyId" value="<%:Model.MainPartyId %>" />
<input type="hidden" id="hdnMemberEmail" value="<%:Model.MemberEmail %>" />

<div class="fast-access-bar">
    <div class="fast-access-bar__inner">
        <div class="row clearfix">
            <div class="col-xs-12 col-md-12">
                <%=Model.Navigation %>
            </div>
        </div>
    </div>
</div>

