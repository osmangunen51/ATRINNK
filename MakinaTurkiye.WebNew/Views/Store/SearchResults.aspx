﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<SearchModel<StoreModel>>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
  <link href="<%: ResolveCssUrl("/Content/category.css") %>" rel="stylesheet" type="text/css" />
  <style type="text/css">
    .contentWrapper, .topBand, .listHeader, .tableHeader, .tableContent, .tableFooter, .bigBanner
    {
      width: 540px;
      float: left;
    }
    
    .listHeader .listContent
    {
      width: 525px;
    }
    .tableFooter div.text
    {
      display: inline-block;
      margin: 1px;
      margin-left: 3px;
      height: 19px;
    }
    .bigBanner
    {
      background-color: #f26700;
      height: 65px;
      margin: 2px;
      margin-top: 5px;
      text-align: center;
      color: #fff;
      font-weight: bold;
      font-size: 13px;
      padding-top: 50px;
    }
    .rightContentWrapper
    {
      width: 170px;
      margin-left: 5px;
      float: left;
    }
    .banner
    {
      text-align: center;
      width: 170px;
      height: 110px;
      background-color: #99d6dd;
      margin-left: 2px;
      margin-top: 5px;
      float: left;
      padding-top: 90px;
    }
    .productContent
    {
      width: 125px;
      height: auto;
      float: left;
      margin-left: 5px;
      margin-right: 5px;
      margin-top: 5px;
    }
    .productImage
    {
      width: 119px;
      height: 119px;
      border: 1px solid #d9d9d9;
    }
    .productText
    {
      font-size: 12px;
      float: left;
    }
    .productText span
    {
      display: block;
      margin: 0px;
    }
  </style>
  <script type="text/javascript">
    onload = function () {

      $('#searchFirm').attr('class', 'searchMenuActive');
      $('#hdnTopSearchType').val('3');
      $('#searchSpan').html('Firma Arama :');

    }
  </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
  <div id="Store">
    <%=Html.RenderHtmlPartial("SearchStore")%>
  </div>
  <input id="hiddenCategoryId" type="hidden" value="<%=ViewData["StoreCategoryId"]%>" />
  <input id="hiddenSearchText" type="hidden" value="<%=ViewData["StoreSearchText"]%>" />
  <script type="text/javascript">
    function PageChange(p, pd) {
      $('#imgLoading').show();
      $.ajax({
        url: '/Store/SearchResultsPaging',
        data: { page: p, pageDimension: pd, SearchText: $('#hiddenSearchText').val(), CategoryId: $('#hiddenCategoryId').val() },
        success: function (data) {
          $('#Store').html(data);
          $('#imgLoading').hide();
        }, error: function (x) {
          $('#imgLoading').hide();
          alert(x.responseText);
        }
      });
    }
  </script>
</asp:Content>
