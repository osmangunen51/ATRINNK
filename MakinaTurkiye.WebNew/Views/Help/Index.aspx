﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Main.Master"  %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-5 col-md-4 col-lg-3">
            <div class="well well-mt2 form-horizontal">
                <div class="input-group">
                    <input type="text" class="form-control" placeholder="Aranacak kelimeyi giriniz">
                    <span class="input-group-btn">
                        <button class="btn btn-default" type="button">
                            Ara</button>
                    </span>
                </div>
            </div>
            <div class="panel-group" id="accordion">
                <%:Html.Action("Menu","Help") %>
            </div>
        </div>
        <div class="col-sm-8 col-sm-7 col-md-8 col-lg-9">
            <div class="row hidden-xs">
                <div class="col-xs-12">
                    <ol class="breadcrumb breadcrumb-mt">
                        <li><a href="/">Anasayfa</a> </li>
                        <li><a href="/yardim">Yardım</a> </li>
                    </ol>
                </div>
            </div>
            <div class="alert alert-info">
                <i class="fa fa-3x fa-bullhorn pull-left"></i><h1 style="font-size:15px!important;">Yardım Menüsü</h1>
           
            </div>
            <div class="well well-mt">
                <div class="row">
                    Yardım almak istediğiniz konuları yandaki panelden seçebilirsiniz.Ayrıca her türlü
                    soru ve öneriniz için bilgi@makinaturkiye.com adresinden bize ulaşabilirsiniz.
               
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
     <link rel="canonical" href="<%= ViewBag.Canonical%>"/>
</asp:Content>
