<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTProductStoreModel>" %>
       <div class="seller-box">
<%if (!string.IsNullOrEmpty(Model.StoreName))
  {%>

<input id="hiddenProductNo" value="<%:Model.ProductNo %>" type="hidden" />
<input id="hiddenMemberNo" value="<%:Model.MemberNo %>" type="hidden" />

<% } %>

<div class="seller-body">

    <table>
        <tr style="vertical-align: middle;">
            <td>
                <div class="firma-logo">
                         <a href='<%=Model.StoreUrl %>' title="<%:Model.StoreName %>">
                      <% if (Model.MainPartyId > 0)
                   { %>
                                            <img src="<%=Model.StoreLogoPath%>" class="img-responsive" alt="<%= Model.StoreName %>" />

                      <% } %>
                                   </a>
                </div>
            </td>
            <td style="vertical-align: middle">
                            <a href='<%=Model.StoreUrl %>' title="<%:Model.StoreName %>">
                <h3 class="seller-name"> <% if (Model.MainPartyId > 0)
                   { %>
                    <%= Model.StoreShortName %> 
                    <% } %></h3>
                                </a>
                <p class="seller-info"> MK / <span>Kurumsal Üyelik</span></p>
            </td>
        </tr>
    </table>
   
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


</div>
<div class="seller-links">
    <a href="<%:Model.StoreUrl %>">Mağazaya Git</a>
    <a href="<%:Model.StoreAllProductUrl %>">Tüm İlanlar</a>
</div>


        </div>