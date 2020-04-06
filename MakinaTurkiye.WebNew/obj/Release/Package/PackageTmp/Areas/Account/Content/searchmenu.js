$(document).ready(function () {

  $('#btnSearchTop').click(function () {
    if ($('#hdnTopSearchType').val() == 3) {
      window.location = '/sirketler?SearchType=3&SearchText=' + $('#topSearchText').val();
    }
    else if ($('#hdnTopSearchType').val() == 2) {
      window.location = '/Urunler/AramaSonuclari?SearchText=' + $('#topSearchText').val() + "&CategoryId=" + $('#searchCategoryId').val();
    }
    else if ($('#hdnTopSearchType').val() == 1) {
      alert('Alım talebi araması şuan etkin değildir.');
    }
    else if ($('#hdnTopSearchType').val() == 4) {
      alert('Video araması şuan etkin değildir.');
    }
  });

  $('#topSearchText').keyup(function () {
    $('#divSearch').hide();
  });

  $('#searchVideo').click(function () {
    $(this).attr('class', 'searchMenuActive');
    $('#searchPurchaseAdvert').attr('class', 'searchMenu');
    $('#searchFirm').attr('class', 'searchMenu');
    $('#searchPurchaseRequest').attr('class', 'searchMenu');
    $('#searchSpan').html('Video Arama :');
    $('#hdnTopSearchType').val('4');
    $('#topSearchText').val('');
  });

  $('#searchFirm').click(function () {
    $(this).attr('class', 'searchMenuActive');
    $('#searchPurchaseAdvert').attr('class', 'searchMenu');
    $('#searchPurchaseRequest').attr('class', 'searchMenu');
    $('#searchVideo').attr('class', 'searchMenu');
    $('#searchSpan').html('Firma Arama :');
    $('#hdnTopSearchType').val('3');
    $('#topSearchText').val('');
  });

  $('#searchPurchaseAdvert').click(function () {
    $(this).attr('class', 'searchMenuActive');
    $('#searchFirm').attr('class', 'searchMenu');
    $('#searchPurchaseRequest').attr('class', 'searchMenu');
    $('#searchVideo').attr('class', 'searchMenu');
    $('#searchSpan').html('Ürün Arama :');
    $('#hdnTopSearchType').val('2');
    $('#topSearchText').val('');
  });

  $('#searchPurchaseRequest').click(function () {
    $(this).attr('class', 'searchMenuActive');
    $('#searchPurchaseAdvert').attr('class', 'searchMenu');
    $('#searchVideo').attr('class', 'searchMenu');
    $('#searchFirm').attr('class', 'searchMenu');
    $('#searchSpan').html('Alım Talebi Arama :');
    $('#hdnTopSearchType').val('1');
    $('#topSearchText').val('');
  });


  $('#topSearchText').hover(function () {
    $("#divSearch").show();
  }, function () {

    var hasOpen = false;
    $('#divSearch').mousemove(function () {
      $("#divSearch").show();
      hasOpen = true;
    });

    if (hasOpen) {
      $("#divSearch").show();
    }
    else {
      $("#divSearch").hide();
    }
  });

  $('#divSearch').mouseout(function () {
    $(this).hide();
  });


  $('#btnDetailedSearch').hover(function () {
    $("#detailedSearch").show();
    $("#imgDetailedSearch").attr('src', '/Content/Images/headerDetailedSearchActive.png');
  }, function () {

    var hasOpen = false;
    $('#detailedSearch').mousemove(function () {
      $("#detailedSearch").show();
      $("#imgDetailedSearch").attr('src', '/Content/Images/headerDetailedSearchActive.png');
      hasOpen = true;
    });

    if (hasOpen) {
      $("#detailedSearch").show();
      $("#imgDetailedSearch").attr('src', '/Content/Images/headerDetailedSearchActive.png');
    }
    else {
      $("#detailedSearch").hide();
      $("#imgDetailedSearch").attr('src', '/Content/Images/headerDetailedSearch.png');
    }
  });

  $('#detailedSearch').mouseout(function () {
    $(this).hide();
    $("#imgDetailedSearch").attr('src', '/Content/Images/headerDetailedSearch.png');
  });


  $("#topSearchText").autocomplete({
    source: function (request, response) {
      $.ajax({
        url: "/Home/FindStoreForName",
        type: "POST",
        dataType: "json",
        data: { searchText: request.term, SearchType: $('#hdnTopSearchType').val() },
        success: function (data) {
          response($.map(data, function (item) {
            return { label: item.StoreName, value: item.MainPartyFullName, id: item.MainPartyId }
          }))
        }
      })
    }
  });


});