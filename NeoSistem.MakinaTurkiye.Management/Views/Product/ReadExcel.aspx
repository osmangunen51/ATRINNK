﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ReadExcel
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <%using (Html.BeginForm("ReadExcel", "Product", FormMethod.Post, new { enctype = "multipart/form-data" }))
    {%>
        <div style="float: left; margin-left: 30px; margin-top: 40px; width: 500px">
        <div style="float: left; margin-top: 4px;">
          <span style="font-size: 12px;">Ürün exceli :</span></div>
        <div style="float: left; margin-left: 5px;">
          <input type="file" name="ExcelProduct" style="border: solid 1px #bababa; width: 220px;
            height: 20px;" /></div>
        <div style="float: left; margin-left: 5px;">
          <button style="border: solid 1px #bababa; height: 20px;">
            Yükle</button>
        </div>
      </div>
      <%} %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
