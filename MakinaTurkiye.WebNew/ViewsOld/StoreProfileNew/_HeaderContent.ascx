<%@ Control Language="C#" %>
<%if (!string.IsNullOrEmpty(ViewBag.Canonical))
    { %>
<link rel="canonical" href="<%:ViewBag.Canonical %>" />
<%} %>
<script type="text/javascript">

    function ProductSearchResult() {
        var mainPartyId = $("#hdnStoreMainPartyId").val();
        var s = $("#productNameForSearch").val();
        $.ajax({
            url: '/StoreProfileNew/ProductSearchGet',
            type: 'POST',
            dataType: "json",

            header: {
                'X-Robots-Tag': 'noindex'
            },
            contentType: "application/json",
            dataType: 'json',
            data: JSON.stringify({ productName: s, storeMainPartyId: mainPartyId }),
            success: function (data) {
                if (data == "false") {
                    $("#dataNo").show();
                }
                else if (data) {
                    window.location.href = data.ProductUrl;
                }
            },
            error: function (x, l, e) {
                alert(e.responseText);
            }
        });
    }
    function SetStoreStatistic() {
        var mainPartyId = $("#hdnStoreMainPartyId").val();

        $.ajax({
            url: '/StoreProfileNew/StoreStatisticCreate',
            data: JSON.stringify({ storeID: mainPartyId }),
            contentType: "application/json",
            dataType: 'json',
            header: {
                'X-Robots-Tag': 'googlebot: nofollow'
            },
            type: 'post',
            success: function (data) {

            }

        }
        );
    }
    $(document).ready(function () {

        jQuery.curCSS = function (element, prop, val) {
            return jQuery(element).css(prop, val);
        };
        var mainPartyId = $("#hdnStoreMainPartyId").val();

        SetStoreStatistic();

        $("#productNameForSearch").autocomplete({

            source: function (request, response) {
                $.ajax({
                    url: "/StoreProfileNew/ProductSearch",
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json;charset=utf-8",

                    header: {
                        'X-Robots-Tag': 'noindex'
                    },
                    data: JSON.stringify({ name: request.term, storeMainPartyId: mainPartyId }),
                    success: function (data) {
                        if (data.length == 0) { $("#dataNo").show(); $("#searchButtonAdvert").attr("disabled", true); }
                        else {
                            $("#dataNo").hide();
                            $("#searchButtonAdvert").attr("disabled", false);
                            response($.map(data, function (item) {
                                return { label: item, value: item };
                            }))
                        }
                    }
                })
            },
            messages: {
                noResults: "", results: ""
            }
        });

    })

</script>
