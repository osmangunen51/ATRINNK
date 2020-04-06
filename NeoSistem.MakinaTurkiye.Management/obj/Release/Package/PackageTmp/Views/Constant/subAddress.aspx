﻿<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master"  Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.ViewModel.SubAddressViewModel>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
  Sabit Alanlar Güncelleme
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        #tableForm tr td{
            padding-top:10px;
            width:150px;
        }
    </style>
  <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
  <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>

    <script type="text/javascript">
        function DeleteCity(cityId) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Constant/DeleteCity',
                    data: { id: cityId },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        var e = data;
                        if (e) {
                            $('#row' + cityId).hide();
                        }
                        else {
                            alert('Bu şehir kullanılıyor.Silme işlemi başarısız.');
                        }
                    }
                });
            }
        }
        function DeleteLocality(localityid)
        {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Constant/DeleteLocality',
                    data: { id: localityid },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        var e = data;
                        if (e) {
                            $('#row' + localityid).hide();
                        }
                        else {
                            alert('Bu ilçe kullanılıyor.Silme işlemi başarısız.');
                        }
                    }
                });
            }

        }
        function DeleteTown(townid) {
            if (confirm('Kaydı Silmek istediğinizden eminmisiniz ?')) {
                $.ajax({
                    url: '/Constant/DeleteTown',
                    data: { id: townid },
                    type: 'post',
                    dataType: 'json',
                    success: function (data) {
                        var e = data;
                        if (e) {
                            $('#row' + townid).hide();
                        }
                        else {
                            alert('Bu mahalle&köy kullanılıyor.Silme işlemi başarısız.');
                        }
                    }
                });
            }

        }
        function UpdateCity(cityId)
        {
            $("#updateType").val("il");
            $("#constantValue").focus();
            $("#btnOparation").html("Kaydet");
            $("#updateHdn").val("1");
            $.ajax({
                url: '/Constant/GetCity',
                data: { id: cityId },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                  
                    if (data) {
                        $("#constantValue").val(data.Name);
                        $("#areacode").val(data.AreaCode);
                        $("#updateCityId").val(data.ID);
                    }
                    else {
                        alert('Bu mahalle&köy kullanılıyor.Silme işlemi başarısız.');
                    }
                }
            });
        }
        function UpdateLocality(localityid)
        {
            $("#updateType").val("ilce");
            $("#constantValue").focus();
            $("#btnOparation").html("Kaydet");
            $("#updateHdn").val("1");
            $.ajax({
                url: '/Constant/GetLocality',
                data: { id: localityid },
                type: 'post',
                dataType: 'json',
                success: function (data) {

                    if (data) {
                        $("#constantValue").val(data.Name);
                        $("#updateLocalityId").val(data.ID);
                    }
                    else {
                        alert('Bu mahalle&köy kullanılıyor.Silme işlemi başarısız.');
                    }
                }
            });

        }
        function UpdateTown(townid) {
            $("#updateType").val("mahalle");
            $("#constantValue").focus();
            $("#btnOparation").html("Kaydet");
            $("#updateHdn").val("1");
            $.ajax({
                url: '/Constant/GetTown',
                data: { id: townid },
                type: 'post',
                dataType: 'json',
                success: function (data) {

                    if (data) {
                        $("#constantValue").val(data.Name);
                        $("#updateTownId").val(data.ID);
                    }
                    else {
                        alert('Bu mahalle&köy kullanılıyor.Silme işlemi başarısız.');
                    }
                }
            });

        }

        $(document).ready(function () {
            $("#countryId").change(function () {
                $("#constantValue").attr("placeholder", "Şehir Adını Girinz");
          
                if($(this).val()!=0)
                {
                    $("#type").val("il");
                    var countryId = $(this).val();
                  
                    CityListGet(countryId);
                    $("#rowCity").show();
                    $("#constantDisplay").html("Şehir Adı");
                    $("#areaCode").show();
                    $.ajax({
                        url: '/Constant/CityList',
                        data: { id: countryId },
                        type: 'post',
                        dataType: 'json',
                        success: function (msg) {
                            $('[data-rel="cityID"]' + " > option").remove();
                            $.each(msg, function (i) {
                                $('[data-rel="cityID"]').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                                
                            });
                        },
                        error: function (e) {
                            alert(e.responseText);
                        }
                    });
                }
                else {
                    $("[data-rel='locatiyList']").hide();
                    $("[data-rel='cityList']").hide();
                    $("[data-rel='townList']").hide();
                    $('[data-rel="cityId"]' + " > option").remove();
                    $('[data-rel="cityId"]' + " > option").append("<option value='0'></option>");
                    $("#areaCode").hide();
                    $("#rowConstant").hide();
                    $("#type").val("");

                }
            });
            $("[data-rel='cityID']").change(function () {
                var cityId = $(this).val();
                var countryId = $("[data-rel='countryId']").val();
                if($(this).val()!=0)
                {
                    $("#rowLocality").show();
                    $("[data-rel='townList']").hide();
                    $("#constantDisplay").html("İlçe Adı");
                    $("#constantValue").attr("placeholder", "İlçe Adını Girinz");
                    $("#type").val("ilce");
                    $("#areaCode").hide();
                   

                    $.ajax({
                        url: '/Constant/LocalityList',
                        data: { countryid: countryId,cityid:cityId },
                        type: 'post',
                        dataType: 'json',
                        success: function (msg) {
                            $('[data-rel="LocalityId"]' + " > option").remove();
                            $.each(msg, function (i) {
                                $('[data-rel="LocalityId"]').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");

                            });
                        },
                        error: function (e) {
                            alert(e.responseText);
                        }
                    });
                   
                    LocationListGet(countryId, cityId);

                }
                else {
                    $("#rowLocality").hide();
                    $("[data-rel='locatiyList']").hide();
                    $("[data-rel='cityList']").show();
                    $('[data-rel="LocalityId"]' + " > option").remove();
                    $('[data-rel="LocalityId"]' + " > option").append("<option value='0'></option>")
                    $("#constantDisplay").html("Şehir Adı");
                    $("#constantValue").attr("placeholder", "Şehir Adını Girinz");
                    $("#areaCode").show();
                    $("#type").val("il");

                }
               
                

            });
            $("[data-rel='LocalityId']").change(function () {
                var localityId = $(this).val();
                var cityID = $("[data-rel='cityID']").val();
                var countryId = $("[data-rel='countryId']").val();
                $("#areaCode").hide();
                if($(this).val()!="0")
                {
                    $("#constantDisplay").html("Köy/Mahalle");
                    $("#constantValue").attr("placeholder", "Köy/Mahalle");
                    $("#type").val("mahalle");
                   
                    TownListGet(cityID, localityId);
                }
                else {
                    $("[data-rel='townList']").hide();
                    $("[data-rel='locatiyList']").show();
                    $("#constantDisplay").html("İlçe Adı");
                    $("#constantValue").attr("placeholder", "İlçe Adını Giriniz");
                    $("#type").val("ilce");
                    $("#areaCode").hide();

                }

                    
            });
        })
        function CityListGet(countryId)
        {
            $('[data-rel="cityList"]').show();
           
            $.ajax({
                url: '/Constant/CityListGet',
                data: { id: countryId },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    $("[data-rel='cityList']'>tbody").html("");
                    var row = 0;
                    $.each(data, function (index, item) {
                        row++;
                        $("[data-rel='cityList']'>tbody").append('<tr class="' + (row % 2 == 0 ? "Row" : "RowAlternate") + '" id="row' + item.cityID + '"><td>' + item.cityID + '</td><td>' + item.CityName + '</td><td>' + item.AreaCode + '</td><td> <a style="padding-bottom: 5px; cursor: pointer" onclick="UpdateCity('+item.cityID+')"> <div style="float: left; margin-right: 10px">  <img src="/Content/images/edit.png" />  </div>  </a><a style="cursor: pointer;" onclick="DeleteCity(' + item.cityID + ');">  <div style="float: left;"><img src="/Content/images/delete.png" /></div></a></td></tr>');
                    });
                },
                error: function (request, status, error) { alert(request.responseText); }
            });
            
        }
        function LocationListGet(countryId,cityId)
        {
            $('[data-rel="cityList"]').hide();
            $("[data-rel='locatiyList']").show();
            $.ajax({
                url: '/Constant/LocationListGet',
                data: { countryid: countryId,cityid:cityId },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    $("[data-rel='locatiyList']'>tbody").html("");
                    var row = 0;
                    $.each(data, function (index, item) {
                        row++;
                        $("[data-rel='locatiyList']'>tbody").append('<tr class="' + (row % 2 == 0 ? "Row" : "RowAlternate") + '" id="row' + item.id + '"><td>' + item.id + '</td><td>' + item.name + '</td><td> <a style="padding-bottom: 5px; cursor: pointer" onclick="UpdateLocality('+item.id+')"> <div style="float: left; margin-right: 10px">  <img src="/Content/images/edit.png" />  </div>  </a><a style="cursor: pointer;" onclick="DeleteLocality(' + item.id + ');">  <div style="float: left;"><img src="/Content/images/delete.png" /></div></a></td></tr>');
                    });
                },
                error: function (request, status, error) { alert(request.responseText); }
            });

        }
        function TownListGet(cityId,localityId)
        {
            $('[data-rel="cityList"]').hide();
            $("[data-rel='locatiyList']").hide();
            $("[data-rel='townList']").show();

            $.ajax({
                url: '/Constant/TownListGet',
                data: { localityid:localityId, cityid: cityId },
                type: 'post',
                dataType: 'json',
                success: function (data) {
                    $("[data-rel='townList']'>tbody").html("");
                    var row = 0;
                    $.each(data, function (index, item) {
                        row++;
                        $("[data-rel='townList']'>tbody").append('<tr class="' + (row % 2 == 0 ? "Row" : "RowAlternate") + '" id="row' + item.id + '"><td>' + item.id + '</td><td>' + item.name + '</td><td> <a style="padding-bottom: 5px; cursor: pointer" onclick="UpdateTown('+item.id+')"> <div style="float: left; margin-right: 10px">  <img src="/Content/images/edit.png" />  </div>  </a><a style="cursor: pointer;" onclick="DeleteTown(' + item.id + ');">  <div style="float: left;"><img src="/Content/images/delete.png" /></div></a></td></tr>');
                    });
                },
                error: function (request, status, error) { alert(request.responseText); }
            });
        }
        function onBeginSend() {
            $("#loading").show();
        }
        function onSuccess() {

            $("#ajaxSuccess").show();
            $("#constantValue").val("");
            var localityId = $("[data-rel='LocalityId']").val();
            var cityID = $("[data-rel='cityID']").val();
            var countryId = $("[data-rel='countryId']").val();
            $("#updateHdn").val("0");
            $("#updateCityId").val("");
            $("#updateLocalityId").val("");
            $("#updateType").val("0");
            $("#updateTownId").val("");
            $("#btnOparation").html("Ekle");
            $("#areaCode").val("");
            if (cityID != 0 && localityId != 0)
            {
                TownListGet(cityID, localityId);
            }
            else if (cityID != 0 && localityId == 0)
            {
                LocationListGet(countryId, cityID);
            }
            else if (countryId != 0 && cityID == 0 && localityId == 0)
            {
                CityListGet(countryId);
            }
           
           

        }
        function ajaxError() {
            $("#ajaxError").show();
            $("#ajaxSuccess").hide();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


  <div style="float: left; width: 800px; margin-top: 10px;">
      <%if (ViewData["success"] == "true")
        { %>
            <b  style="color:#038f20; display:none;">İşleminiz gerçekleşmiştir.</b>
     
          if (ViewData["error"] == "true") { %>
      <b style="color:#f56767">Lütfen girdiğiz değerleri kontrol ediniz, işleminiz gerçekleştirilemedi.</b>
      <%}
         %>

    <table id="tableForm" border="0" cellpadding="0" cellspacing="0" width="%100" style="float: left">
        <tr>
            <td></td><td><b id="ajaxSuccess" style="color:#038f20;display:none;">
            İşleminiz Gerçekleşmiştir.
             </b>
             <b id="ajaxError" style="color:#ff0000; display:none;">
                 Değerleri Kontrol Ediniz, işleminiz Gerçekleşmedi.
                </b>
               </td>
        </tr>
        <%using (Ajax.BeginForm("SubAddress", "Constant", new AjaxOptions { UpdateTargetId = "satutusTalep", LoadingElementId = "loading", OnSuccess = "onSuccess", OnFailure = "ajaxError", OnBegin = "onBeginSend" }))
          { %>
        <tr>
            <td>Ülke</td>
            <td><%:Html.DropDownListFor(x => x.CountryID, Model.CountryItems, new Dictionary<string, object> { { "required", "true" }, { "id", "countryId" }, { "data-rel", "countryId" } })%></td>
        </tr>
        <tr id="rowCity" style="display:none;">
            <td>Şehir</td>
            <td><select name="cityId" data-rel="cityID">
             
                </select></td>
        </tr>
        <tr id="rowLocality" style="display:none;">
            <td>İlçe</td>
            <td><select name="localityId" data-rel="LocalityId">

                </select>
       <input type="hidden" name="type" id="type" />
                <input type="hidden" id="updateHdn" name="update" value="0" />
                <input type="hidden" id="updateCityId" name="updateCityId" />
                <input type="hidden" id="updateLocalityId" name="updateLocalityId" />
                <input type="hidden" id="updateType" name="updateType" value="0" />
                <input type="hidden" id="updateTownId" name="updateTownId"/>

            </td>
        </tr>
        <tr id="rowConstant">
            <td id="constantDisplay">


            </td>
            <td id="cityName"><%:Html.TextBox("constantValue", "", new { @required = true, @id = "constantValue" })%></td>
        </tr>
        <tr id="areaCode" style="display:none;">
            <td>Telefon Alan Kodu</td>
            <td><%:Html.TextBox("areacode", "", new { @placeholder = "Alan Kodu",@id="areaCode" })%></td>
        </tr>
        <tr>
            <td><img src="/Content/Images/ajax-loader.gif" alt="loading" id="loading" style="display:none;"/></td>
            <td><button style="padding:5px;" type="submit" id="btnOparation" >Ekle</button></td>
        </tr>
        

        <%} %>
    </table> 

      <div style="width: 100%; margin: 0 auto;">
         <table cellpadding="4" cellspacing="0" class="TableList" data-rel="locatiyList" style="width: 100%; display:none; margin-top: 5px">
             <thead>
                 <tr>
                     <td>İlçe ID</td>
                     <td>İlçe Adı</td>
                     <td></td>
                     
                 </tr>
             </thead>
             <tbody></tbody>
             </table>  
          <table cellpadding="4" cellspacing="0" class="TableList" data-rel="townList" style="width: 100%; display:none; margin-top: 5px">
             <thead>
                 <tr>
                     <td>Mahalle&Köy ID</td>
                     <td>Mahalle&Köy Adı</td>
                     <td></td>
                 </tr>
             </thead>
             <tbody></tbody>
         </table>   
    <table cellpadding="4" cellspacing="0" class="TableList" data-rel="cityList" style="width: 100%; margin-left:30px; display:none; margin-top: 5px">
   
            <thead>
                 <tr>
                     <td>Şehir ID</td>
                     <td>Şehir Adı</td>
                     <td>Alan Kodu</td>
                     <td></td>
                     
                 </tr>
             </thead>
       
        <tbody>
            <tr>
                <td>1</td>
                <td>Deneme</td>
                <td>Sada</td>
            </tr>
        </tbody>
        </table>
          </div>
  </div>
</asp:Content>

