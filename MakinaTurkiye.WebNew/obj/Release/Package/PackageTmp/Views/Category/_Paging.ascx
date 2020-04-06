﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTCategoryProductViewModel>" %>
<div class="row marginTop clearfix">
	<div class="col-md-12 text-right">
	    <ul class="pagination m0">
	         <li><span  id="divLoading"  style="display: none">Yükleniyor.. Lütfen Bekleyiniz&nbsp;&nbsp;</span></li>
	    </ul>
	</div>
	<div class="col-md-7 text-right">
			  <ul class="pagination m0">
				
				<% foreach (var item in Model.PagingModel.PageUrls)
					{ %>
				<% if (Model.PagingModel.CurrentPageIndex == item.Key)
					{ %>
				<li class="active"><a  style="cursor: pointer;" onclick="ProductPaging(<%: item.Key %>, <%=Model.PagingModel.PageSize %>);">
					 <%: item.Key%></a></li>
				<% }
					else
					{ %>
				<li><a style="cursor: pointer;" onclick="ProductPaging(<%: item.Key %>, <%=Model.PagingModel.PageSize %>);">
					 <%: item.Key%></a></li>
				<% } %>
				<% } %>
		  </ul>
	</div>
	<div class="col-xs-5 col-sm-4 col-md-4 col-lg-3 text-right">
		<div class="input-group input-group-sm col-md-6">
			<input id="paget" type="text" class="form-control" placeholder="Sayfa No" value="<%: Request.QueryString["page"].ToInt32() == 0 ? 1 : Request.QueryString["page"].ToInt32() %>" />
			<span class="input-group-btn">
			<button class="btn btn-default" type="button" onclick="ProductPaging($('#paget').val(),<%=Model.PagingModel.PageSize %>);" >Git!</button>
			</span>
		</div>
	</div> 
         	<br />
           
    	<div class="col-xs-12 text-center" style="margin-top:10px">
		  Toplam <%:Model.TotalItemCount%> ürün, <%:Model.PagingModel.TotalPageCount %> sayfa bulunmaktadır.
	</div>
</div>

