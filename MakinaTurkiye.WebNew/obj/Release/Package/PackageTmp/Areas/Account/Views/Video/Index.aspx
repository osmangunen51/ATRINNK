﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage< NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Videos.MTStoreVideoModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Tanıtım Videolarım - makinaturkiye.com
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <script type="text/javascript">
        function DeleteVideo(newid) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $(".loading").show();
                $.ajax({
                    url: '/Account/Video/Delete',
                    data: { id: newid },
                    type: 'get',
                    dataType: 'json',
                    success: function (data) {
                        if (data) {
                            $('#row' + newid).hide();
                        }
                        $(".loading").hide();
                    }
                });
            }

        }
        function UpdateVideoOrder() {
            $.ajax({
                url: '/Account/Video/UpdateOrder',
                data: { id: $("#VideoId").val(), order: $("#NewOrder").val(), title: $("#NewTitle").val() },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    if (data) {
                        alert("Sıra başarıyla güncellenmiştir.");
                        window.location = "/account/video";
                    }

                }
            });

        }

        function VideOrderGet(newid) {
            $.ajax({
                url: '/Account/Video/GetOrder',
                data: { id: newid },
                type: 'get',
                dataType: 'json',
                success: function (data) {

                    if (data.IsSuccess) {
                        $("#OrderUpdateHeaderText").html(data.Result.Title + " Sıra Düzenle");
                        $("#NewOrder").val(data.Result.Order);
                        $("#VideoId").val(data.Result.VideoId);
                        $("#NewTitle").val(data.Result.Title);
                    }

                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
    </div>
    <div class="row">

        <div class="col-sm-12 col-md-12">
            <div class="well well-mt4 col-xs-12" style="background: #fff;">
                <div class="pull-left">
                    <h4>Tanıtım Videolarım</h4>
                </div>
                <div class="pull-right">
                    <a class="btn btn-success" href="/Account/Video/Create">Yeni Ekle <i class="fa fa-add"></i></a>
                </div>
                <div class="col-md-12">
                    <%if (Model.StoreVideoItemModels.Count() > 0)
                        {%>

                    <div class="loading">Loading…</div>
                    <table class="table table-hover">
                        <thead>
                            <tr>
                                <th>Video</th>
                                <th>Başlık</th>
                                <th>Sıra</th>
                                <th>Tarih</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <%foreach (var item in Model.StoreVideoItemModels.OrderByDescending(x => x.RecordDate))
                                {%>
                            <tr id="row<%:item.VideoId %>">
                                <td>
                                    <div class="video-container" style="">
                                        <a style="cursor: pointer" data-toggle="modal" data-target="#video<%:item.VideoId %>">
                                            <img style="width: 200px; height: 120px" src="<%:item.ImagePath %>" class="img-responsive" />
                                        </a>
                                        <div class="minute-container" style="position: absolute;">
                                            <%:item.Minute %>:<%:item.Second %>
                                        </div>
                                        <div class="modal fade" id="video<%:item.VideoId %>" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                            <div class="modal-dialog" role="document">
                                                <div class="modal-content">
                                                    <div class="modal-header" style="height: 50px">
                                                        <h5 class="modal-title" style="float: left;" id="exampleModalLabel"><%:item.Title %></h5>
                                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                            <span style="font-size: 30px;" aria-hidden="true">&times;</span>
                                                        </button>
                                                    </div>
                                                    <div class="modal-body">
                                                        <div class="videocontent">
                                                            <video id="vd" class="video-js vjs-default-skin" controls preload="auto" width="100%"
                                                                height="100%" style="width: 100%; height: 100%; padding: 4px; border: 1px solid #ddd; border-radius: 4px; transition: all .2s ease-in-out; display: inline-block;"
                                                                poster="" data-setup='{"techOrder": ["html5"]}'>
                                                                <source src="https://s.makinaturkiye.com/NewVideos/<%= item.VideoPath %>.mp4" type='video/mp4' />
                                                            </video>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td><%:item.Title %></td>
                                <td>

                                    <%:item.Order %>
                              

                                </td>
                                <td><%:item.RecordDate %></td>
                                <td><a onclick="DeleteVideo(<%:item.VideoId %>)" style="cursor: pointer; color: #333;"><i class="fa fa-trash"></i></a>| <a data-toggle="modal" style="cursor: pointer" data-target="#orderUpdate" onclick="VideOrderGet(<%:item.VideoId %>)"><i class="fa fa-pencil" style="color: #333"></i>
                                </a></td>
                            </tr>

                            <%} %>
                        </tbody>

                    </table>

                    <% }
                        else
                        {%>
                    <div class="alert alert-info" role="alert" style="margin-top: 10px;">
                        Eklediğiniz herhangi bir firma tanıtım videosu bulunmamaktadır. 
                      <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                      </button>
                    </div>
                    <% } %>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="orderUpdate" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header" style="height: 50px">
                    <h5 class="modal-title" style="float: left;" id="OrderUpdateHeaderText"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span style="font-size: 30px;" aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-md-2">Başlık</label>
                            <div class="col-md-6">

                                <input type="text" id="NewTitle" class="form-control mt-form-control" />
                            </div>

                        </div>
                        <div class="form-group">
                            <label class="col-md-2">Sıra</label>
                            <div class="col-md-6">
                                <input type="hidden" id="VideoId" />
                                <input type="text" id="NewOrder" class="form-control mt-form-control" />
                            </div>

                        </div>

                        <div class="form-group">
                            <label class="col-md-2"></label>
                            <div class="col-md-6">
                                <input type="button" class="btn background-mt-btn" onclick="UpdateVideoOrder()" value="Kaydet" />
                            </div>

                        </div>

                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
