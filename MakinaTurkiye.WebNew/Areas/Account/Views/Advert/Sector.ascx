﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AdvertViewModel>" %>
<div class="col-md-4">
    <div class="form-group">
    <label class="col-sm-4">
        Sektör Seçin
    </label>
    <div class="col-sm-12">
        <%if (Model.PrivateSectorCategories.Count > 0)
            { %>
        <div style="display: none">
          
            <%:Html.DropDownList("AllSector", new SelectList(Model.SectorItems.Skip(1).Take(Model.SectorItems.ToList().Count), "CategoryId", "CategoryName"), new { @class = "form-control", @size="10" })%>
        </div>
        <%:Html.DropDownList("DropDownSector", Model.PrivateSectorCategories, new { @class = "form-control",@size="10"  })%>

        <% }
            else
            {%>
        <%:Html.DropDownList("DropDownSector", new SelectList(Model.SectorItems, "CategoryId", "CategoryName"), new { @class = "form-control" ,@size="10" })%>

        <% } %>
    </div>
</div>
</div>
 <div class="col-md-4">
     <div class="form-group" id="panelProduct" style="display: none">
    <label class="col-sm-12">
        Ürün Grubu Seçin
   
    </label>
    <div class="col-sm-12">
        <select id="DropDownProductGroup" name="DropDownProductGroup" class="form-control" size="10">
        </select>
    </div>
</div>
 </div>

