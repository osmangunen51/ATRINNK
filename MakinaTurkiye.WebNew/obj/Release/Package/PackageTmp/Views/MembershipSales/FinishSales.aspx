﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    FinishSales</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div>
            <div class="alert alert-success">
                <span class="glyphicon glyphicon-ok"></span>&nbsp;Kayıt işlemi başarıyla gerçekleşmiştir.
            </div>
            <div class="well">
                Havale/EFT işleminizi yaptıktan sonra <a href="#">destek masası</a> üzerinden <b>havale/eft
                    bildirimini</b> seçerek Havale/EFT bilgilerini tarafımıza bildirin.
                <br/>
                <br/>
                Daha sonra paket üyeliğiniz aktif edilecektir.
            </div>
        </div>
    </div>
</asp:Content>
