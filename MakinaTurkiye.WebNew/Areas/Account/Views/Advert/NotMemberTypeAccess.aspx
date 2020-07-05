﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<AdvertViewModel>" %>



<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
    </div>
    <div class="row">

        <div class="col-sm-12 col-md-12 store-panel-container">
            <div class="col-xs-12">
                <h3>
                    <span class="text-warning glyphicon glyphicon-warning-sign"></span>
                </h3>
                <div class="well">
                    Bireysel üyesiniz. 
			 <br />
                    Sitemizde bireysel üyelerimiz ilan ekleyememektedir. 
			 <br />
                    İlan ekleyebilmeniz için kurumsal üye (firma üyeliği) olmalısınız.
			 <br />

                    <br />
                    <a href="/Account/MemberType/InstitutionalStep">Firma üyeliğine geçiş yapmak için tıklayınız.</a>
                    <br>
                </div>

            </div>
        </div>

    </div>
</asp:Content>

