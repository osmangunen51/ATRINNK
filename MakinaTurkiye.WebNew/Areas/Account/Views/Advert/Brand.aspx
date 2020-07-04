<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master"
  Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.Adverts.MTBrandViewModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
  <style type="text/css">
    .sectorPanel
    {
      width: auto;
      height: auto;
      float: left;
      border: solid 2px #a9dce2;
      margin-top: 10px;
      margin-left: 20px;
    }
    .sectorPanel ul
    {
      list-style-type: none;
      padding: 0px;
      padding-top: 5px;
      padding-bottom: 5px;
      font-size: 12px;
      margin: 0px;
      color: #4c4c4c;
      width: 300px;
    }
    .sectorPanel ul li
    {
      height: 18px;
      padding-left: 15px;
      padding-right: 15px;
      padding-top: 2px;
      width: 270px;
    }
    .sectorPanel ul li:hover
    {
      background-color: #c4eafa;
      cursor: pointer;
    }
    .sectorPanel a
    {
      text-decoration: none;
      color: #000;
    }
    .categoryPanel
    {
      width: 440px;
      height: auto;
      float: left;
    }
    .categoryPanelTitle
    {
      margin-left: 20px;
      width: 150px;
      height: 10px;
      float: left;
      font-size: 13px;
      margin-top: 10px;
      color: #4c4c4c;
      color: #4c4c4c;
    }
    .picPanel
    {
      width: 65px;
      height: 75px;
      text-align: center;
      float: left;
      margin-left: 10px;
      margin-top: 5px;
    }
    .picPanel img
    {
      width: 65px;
      height: 65px;
    }
    .picPanelImage
    {
      width: 65px;
      height: 65px;
      background-image: #fff;
      border-left: solid 1px #afc1dc;
      border-right: solid 1px #afc1dc;
      border-top: solid 2px #d1dbeb;
      border-bottom: solid 2px #d1dbeb;
    }
    .fileUp
    {
      border: solid 1px #99D6DD;
      border-top: solid 2px #99D6DD;
      width: 280px;
      margin-top: 10px;
    }
    .postButton
    {
      width: 120px;
      height: 25px;
      text-align: center;
      background-color: #75A8CA;
      border: 3px double #366F81;
      font-family: Segoe UI, Arial;
      font-size: 15px;
      font-weight: bold;
      color: #FFFFFF;
    }
  </style>
  <script type="text/javascript">

    $(document).ready(function () {  
      RegisterList();
       
      $('#DropDownCategory').val(<%: Model.CategoryIdSession%>);

      $('#DropDownBrand').change(function () {
      var value = $(this).val();
          reset();
          $('[data-rel="series"]').html('');
          $('[data-rel="brand"]').html('');
          if (value == 0) {
            $('#divBrandText').show();
            $('#divModelText').show();
          } else {
            SeriesBind(value);
            ModelBind(value);
          } 
      });
       
//      $('#ListBrand li').each(function () {
//        $(this).click(function () {
//          reset();
//          $('#series').html('');
//          $('#model').html('');
//          var value = $(this).attr('value');
//          if (value == 0) {
//            $('#divBrandText').show();
//            $('#divModelText').show();
//          } else {
//            SeriesBind(value);
//            ModelBind(value);
//          } 
//        });
//      });

    });

    function RegisterList() {
      $('.iListBrand li').each(function () {
        $(this).click(function () {

          var control = $(this).parent().attr('control');
          var controlPanel = $(this).parent().attr('controlPanel');
          var controlButton = $(this).parent().attr('controlButton');

          var cShow = $(this).parent().attr('show');

          var selectedValue = $(this).attr('value');

          $(control).show();
          $(controlPanel).show();
          $(controlButton).show();

          $(control).val(selectedValue);

          $(cShow).show();

          $(this).parent().hide();
        });
      });
    }

    function SeriesListRegister() { 
//      $('#ListSeries li').each(function () {
//        $(this).click(function () {
//          var value = $(this).attr('value');
//          if (value === '0') {
//            ModelBind($('#DropDownBrand').val());
//          }
//          else {
//            ModelBind(value);
//          }
//        });
//      });

       $('#DropDownSeries').change(function () {
           if ($('#DropDownSeries').val() == 0) {
                $('#divModelText').show();
                $('[data-rel="brand"]').html('');
            }
            else {
             ModelBind($(this).val());
            }
        });
    }

    function ModelListRegister() { 

//      $('#ListModel li').each(function () {
//        $(this).click(function () {
//          var value = $(this).attr('value');
//          if (value == '0') {
//            $('#divModelText').show();
//          }
//        });
//      });

       $('#DropDownModel').change(function () {
          var value = $(this).val();
          if (value == '0') {
            $('#divModelText').show();
            }
              else {
            $('#divModelText').hide();
            }
      }); 

    }
    
    function DropDownBind() {
       $('#DropDownSeries').change(function () {
           if ($('#DropDownSeries').val() == 0) {
                $('#divModelText').show();
                $('[data-rel="brand"]').html('');
            }
            else {
             ModelBind($(this).val());
            }
        });
    }

    function reset(){ 
      $('#divModelText').hide();
      $('#divBrandText').hide();
      $('#divModelText').val('');
      $('#divBrandText').val('');
    }

    function SeriesBind(selectedValue) {
      $('[data-rel="series"]').html('');
      $.ajax({
        url: '/Account/ilan/Series',
        type: 'post',
        data: { value: selectedValue },
        success: function (data) {
          if (data === 'NotSerie') {
//            ModelBind(selectedValue);
//            $('#divModelText').show();
          }
          else {
            $('[data-rel="series"]').html(data);
          }
          SeriesListRegister();
          ModelListRegister();
          RegisterList();
          DropDownBind(); 
        },
        error: function (x) {
        }
      });
    }

    function ModelBind(selectedValue) {
      $.ajax({
        url: '/Account/ilan/Model',
        type: 'post',
        data: { value: selectedValue },
        success: function (data) {
          $('[data-rel="brand"]').html(data);
          ModelListRegister();
          RegisterList();
          DropDownBind();
        }
      });      
    }

  </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <div class="row">
            <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
                <div class="col-md-12">
        <h4 class="mt0 text-info">
           İlan Ekle
        </h4>
    </div>
       </div>
<div class="row">

    <div class="col-sm-12 col-md-12">
          <div>
            <div class="well store-panel-container">
              <div>
                <div>
                  <% using (Html.BeginForm("Brand", "Advert", FormMethod.Post, new { @class="form-horizontal" ,role="form" }))
                     { 
                         %>
                      <%: Html.HiddenFor(m => m.CategoryId)%>
                        <div class="form-group">
                          <label class="col-sm-3 control-label">
                            Marka Seçiniz
                          </label>
                          <div class="col-sm-6">
                            <%:Html.DropDownList("DropDownBrand", new SelectList(Model.BrandItems, "CategoryId", "CategoryName"), "< Lütfen Seçiniz >", new { @class = "form-control" })%>
                          </div>
                        </div>
                        <div class="form-group" data-rel="series"></div>
                        <div class="form-group" data-rel="brand"></div>
                            <div id="divBrandText" style="display: none" class="form-group">
                          <label class="col-sm-3 control-label">
                            Marka Giriniz :</label>
                          <div class="col-sm-6">
                            <input type="text" class="form-control" id="OtherBrand" name="OtherBrand" value="" /></div>
                        </div>
                            <div id="divModelText" style="display: none" class="form-group">
                          <label class="col-sm-3 control-label">
                            Model Tipi Giriniz :</label>
                          <div class="col-sm-6">
                            <input type="text" class="form-control" id="OtherModel" name="OtherModel" value="" /></div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-3 col-sm-9 btn-group">
                            <button type="submit" class="btn btn-primary">
                              Devam
                            </button>
                            </div>
                        </div>
                    <% } %>
                </div>
              </div>
            </div>
          </div>
    </div>
</div>
</asp:Content>
