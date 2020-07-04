<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<MTVideoModel>>" %>

<%if(Model.Count>0){ %>

     <div class="alert alert-info tumSektorBack">
           Benzer Videolar
            </div>
<div class="row" id="anaDiv">

    <%foreach (var item in Model)
        { %>
    <div class="col-sm-12 col-md-6 col-lg-4">
        <div class="thumbnail thumbnail-mt2 mb20 hidden-item-container">
            <div class="videos-minute">
                <span class="text-mutedx"><%=item.VideoMinute%>:<%=item.VideoSecond%></span>
            </div>
            <a href="<%:item.VideoUrl %>" class="pa-center btn btn-sm btn-info hidden-item"><span
                class="glyphicon glyphicon-facetime-video"></span>&nbsp;Oynat </a><a href="<%=item.VideoUrl %>">
                    <img src="<%=item.PicturePath %>" alt="<%=item.StoreName %>" />
                </a>
            <div class="caption">
                <%if (item.TruncateProductName.Length > 45)
                    {%>
                <p class="margin0 productName productNameLineHeight">
                    <%}
                        else
                        { %>
                <p class="margin0 productName">

                    <%} %>
                    <a href="<%=item.ProductUrl %>"><%=item.TruncateProductName%> </a>
                    <%--<span class="text-muted pull-right"><%=item.SingularViewCount %> Görüntülenme </span>--%>
                </p>
                <p class="mb0 margin0">
                    <span class="text-muted text-size">
                        <%=item.VideoRecordDate%>
                    </span>
                    <span class="text-muted pull-right text-size">
                        <%=item.SingularViewCount %> Görüntülenme
                    </span>
                    <%--  <a href="<%:item.StoreUrl %>" class="pull-right" style="display: none;"><span class="glyphicon glyphicon-shopping-cart"></span>
                        <%:item.ShortDescription %>
                    </a>--%>
                </p>
            </div>
            <a href="<%=item.StoreUrl %>" class="btn btn-sm btn-mt2 btn-block companyName">
                <%=item.TruncateStoreName %>
            </a>
        </div>
    </div>
    <% } %>

    <!--
    <%if (Model.Count < 21)
        { %>
    <%foreach (var item in Model)
        { %>
    <div class="col-sm-12 col-md-6 col-lg-4">
        <div class="thumbnail thumbnail-mt2 mb20 hidden-item-container">
            <div class="videos-minute">
                <span class="text-mutedx"><%=item.VideoMinute%>:<%=item.VideoSecond%></span>
            </div>
            <a href="<%:item.VideoUrl %>" class="pa-center btn btn-sm btn-info hidden-item"><span
                class="glyphicon glyphicon-facetime-video"></span>&nbsp;Oynat </a><a href="<%=item.VideoUrl %>">
                    <img src="<%=item.PicturePath %>" alt="<%=item.StoreName %>" />
                </a>
            <div class="caption">
                <p>
                    <a href="<%=item.ProductUrl %>"><%=item.TruncateProductName%> </a>
                    <%--<span class="text-muted pull-right"><%=item.SingularViewCount %> Görüntülenme </span>--%>
                </p>
                <p class="mb0">
                    <span class="text-muted">
                        <%=item.VideoRecordDate%>
                    </span>
                    <span class="text-muted pull-right">
                        <%=item.SingularViewCount %> Görüntülenme
                    </span>
                    <%--  <a href="<%:item.StoreUrl %>" class="pull-right" style="display: none;"><span class="glyphicon glyphicon-shopping-cart"></span>
                        <%:item.ShortDescription %>
                    </a>--%>
                </p>
            </div>
            <a href="<%=item.StoreUrl %>" class="btn btn-sm btn-mt2 btn-block">
                <%=item.TruncateStoreName %>
            </a>
        </div>
    </div>
    <% } %>

    <%} %> -->
</div>
<style type="text/css">
    /*.text-size {
        font-size: 11px;
    }

    .caption {
        padding: 9px 9px 5px 9px;
    }

        .caption p.margin0 {
            margin: 0px 0px 0px 0px;
        }

    .productName {
        height: 30px;
        line-height: 30px;
        font-size: 11px;
    }

    .productNameLineHeight {
        line-height: 15px;
    }

    .companyName {
        padding: 0px;
        height: 30px;
        line-height: 30px;
        color: green;
        font-size: 12px;
    }
    .companyNameLineHeight{
        line-height: 15px;
    }*/ 
</style>
<%} %>