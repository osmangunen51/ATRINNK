﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MessageViewModel>" %>
<div>
    <h4 class="mt0 text-info">
        <span class="text-primary glyphicon glyphicon-cog"></span> Gelen Mesajlar
    </h4>
    <div class="well well-mt2">
        <table class="table table-hover table-condensed">
            <thead>
                <tr>
                    <th style="width: 20px;">
                    </th>
                    <th style="width: 100px;">
                        Kimden
                    </th>
                    <th>
                        Konu
                    </th>
                    <th class="hidden-xs" style="width: 150px;">
                        Tarih
                    </th>
                </tr>
            </thead>
            <tbody>
                <%=Html.RenderHtmlPartial("InboxList", Model.MessageItems) %>
            </tbody>
        </table>
        <div class="row" style="display: none">
            <div class="col-md-6 ">
                Toplam 8 sayfa içerisinde 1. sayfayı görmektesiniz.
            </div>
            <div class="col-md-6 text-right">
                <ul class="pagination m0">
                    <li><a href="#">&laquo; </a></li>
                    <li><a href="#">1 </a></li>
                    <li><a href="#">2 </a></li>
                    <li><a href="#">3 </a></li>
                    <li><a href="#">4 </a></li>
                    <li><a href="#">5 </a></li>
                    <li><a href="#">&raquo; </a></li>
                </ul>
            </div>
        </div>
    </div>
</div>
