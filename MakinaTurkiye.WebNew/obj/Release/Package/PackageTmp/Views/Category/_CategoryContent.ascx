﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTCategorySeoModel>" %>

<%if (!string.IsNullOrEmpty(Model.SeoContent) || !string.IsNullOrEmpty(Model.Description))
    { %>

<div class="alert alert-info">
    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
        ×
    </button>
    <%if (!string.IsNullOrEmpty(Model.SeoContent))
        { %>
    <%=Model.SeoContent %>

    <%}%>
    <%=Model.Description %>
</div>
<%} %>

