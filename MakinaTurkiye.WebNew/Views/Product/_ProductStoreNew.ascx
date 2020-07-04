<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTProductStoreModel>" %>

<%if (!string.IsNullOrEmpty(Model.StoreName))
  {%>
<input id="hiddenProductNo" value="<%:Model.ProductNo %>" type="hidden" />
<input id="hiddenMemberNo" value="<%:Model.MemberNo %>" type="hidden" />

<% } %>

<div class="panel-body" style="padding: 0px 15px">
     <a href='<%=Model.StoreUrl %>'>
    <table>
        <tr style="vertical-align: middle;">
            <td>
                <div class="firma-logo">
                      <% if (Model.MainPartyId > 0)
                   { %>
                                            <img src="<%=Model.StoreLogoPath%>" class="img-responsive" alt="<%= Model.StoreName %>" />

                      <% } %>
                </div>
            </td>
            <td style="vertical-align: middle">
                <h4> <% if (Model.MainPartyId > 0)
                   { %>
                    <%= Model.StoreShortName %> 
                    <% } %></h4>
            </td>
        </tr>
    </table>
         </a>
    <%--<div class="flex-row flex-nowrap">
        <div class="flex-xs-4">
            <div class="firma-logo">
                <% if (Model.MainPartyId > 0)
                   { %>
                <a href='<%=Model.StoreUrl %>'>
                    <img src="<%=Model.StoreLogoPath%>" class="img-responsive" alt="<%= Model.StoreName %>" />
                </a>
                <%} %>
            </div>

        </div>
        <div class="flex-xs-8">
            <h4 style="line-height: 20px;">
                <% if (Model.MainPartyId > 0)
                                              { %>

                <%= Model.StoreShortName %>
                <% } %></h4>


        </div>

    </div>--%>
    <div class="flex-row">
        <div class="flex-xs-4">
            <a style="font-size: 10px;" class="magaza-git" href="<%:Model.StoreUrl %>">Mağazaya Git</a>

        </div>
        <div class="flex-xs-8">
            <div class="yetkili-firma">
                <div class="text-right  ">
                    <%--                                        <span class="baslik">Yetkili:</span>--%>
                    <span class="yetkili-adi" style=""><%= Model.MemberName + " " + Model.MemberSurname%></span>
                </div>
            </div>
        </div>
    </div>

</div>
<div class="panel-footer" style="border-bottom: 1px solid #cccccc; padding: 5px 15px;">
    <div class="btn-firma">
        <div class="row">
            <div class="col-md-7 col-xs-8">
                <div style="line-height: 26px; font-size: 10px">Satıcının Diğer Ürünleri</div>
            </div>
            <div class="col-md-5 xol-xs-4">
                <div class="text-right">
                    <a href="<%:Model.StoreAllProductUrl %>" style="font-size: 10px">» Tümü</a>
                </div>
            </div>
        </div>
    </div>
</div>


