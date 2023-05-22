var isEmail = true;
$(document).ready(function () {
    $("#formFastMembership").validationEngine();
    $("#formFastMembershipIndividual").validationEngine();
    $('#-xcaptcha-refresh').attr("title", "Güvenlik Kodunu Yenile");
    var isEmail = true;
    var cityWrapper = $('[data-rel="city-wrapper"]');
    var addressWrapper = $('[data-rel="address-wrapper"]');
    var localityWrapper = $('[data-rel="locality-wrapper"]');
    var districtWrapper = $('[data-rel="district-wrapper"]');
    var otherElementsWrapper = $('[data-rel="other-wrapper"]');
    var phoneWrapper = $('[data-rel="phone-wrapper"]');
    var gsmWrapper = $('[data-rel="gsm-wrapper"]');

    $('[data-rel="countryId"]').change(function () {
        var countryId = $(this).val();
   
        getCultureCode(countryId, function (cultureCode) {
        
            $('#MembershipModel_InstitutionalPhoneCulture,#MembershipModel_InstitutionalGSMCulture,#MembershipModel_InstitutionalPhoneCulture2,#MembershipModel_InstitutionalFaxCulture').val(cultureCode);
        });
        phoneWrapper.slideDown();

        if (countryId == 246) {//türkiye ise
            cityWrapper.slideDown();
            addressWrapper.slideUp();
            gsmWrapper.slideDown();
            $.ajax({
                url: '/Membership/Cities',
                data: { id: countryId },
                success: function (msg) {
                    $('[data-rel="cityId"]' + " > option").remove();
                    $.each(msg, function (i) {
                        $('[data-rel="cityId"]').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                    });
                },
                error: function (e) {
                    alert(e.responseText);
                }
            });
        }
        else {
            cityWrapper.slideUp();
            localityWrapper.slideUp();
            districtWrapper.slideUp();
            otherElementsWrapper.slideUp();
            addressWrapper.slideDown();
            gsmWrapper.slideUp();
            $('[data-rel="cityId"]' + " > option").remove();
            $('#MembershipModel_InstitutionalPhoneCulture,#MembershipModel_InstitutionalPhoneCulture2,#MembershipModel_InstitutionalFaxCulture').val("");
            $('#TextInstitutionalPhoneAreaCode,#TextInstitutionalPhoneAreaCode2,#TextInstitutionalFaxAreaCode').val("").show();
            $('#DropDownInstitutionalPhoneAreaCode,#DropDownInstitutionalPhoneAreaCode2,#DropDownInstitutionalFaxAreaCode').hide();
        }
    });

    //il
    $('[data-rel="cityId"]').change(function () {
        var cityId = $(this).val();
        localityWrapper.slideDown();
        districtWrapper.slideUp();
        otherElementsWrapper.slideUp();
        $.ajax({
            url: '/Membership/Localities',
            data: { id: cityId },
            success: function (msg) {
                $('[data-rel="localityId"]' + " > option").remove();
                $.each(msg, function (i) {
                    $('[data-rel="localityId"]').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                });

                getAreaCode(cityId, function (areaCode) {
                    if (areaCode == 0) {//istanbul
                        $('#DropDownInstitutionalPhoneAreaCode,#DropDownInstitutionalPhoneAreaCode2,#DropDownInstitutionalFaxAreaCode').show();
                        $('#TextInstitutionalPhoneAreaCode,#TextInstitutionalPhoneAreaCode2,#TextInstitutionalFaxAreaCode').val('').hide();
                    } else {
                       
                        $('#TextInstitutionalPhoneAreaCode,#InstitutionalPhoneAreaCode,#TextInstitutionalPhoneAreaCode2,#TextInstitutionalFaxAreaCode').val(areaCode).show();
                        $('#MembershipModel_InstitutionalPhoneAreaCode,#MembershipModel_InstitutionalPhoneAreaCode2,#MembershipModel_InstitutionalFaxAreaCode').val(areaCode);
                        $('#DropDownInstitutionalPhoneAreaCode,#DropDownInstitutionalPhoneAreaCode2,#DropDownInstitutionalFaxAreaCode').hide();
                    }
                });
            },
            error: function (e) {
                alert(e.responseText);
            }
        });


        $('#TextInstitutionalPhoneAreaCode').keyup(function () {
            $('#MembershipModel_InstitutionalPhoneAreaCode').val($(this).val());
        
        });
        $('#TextInstitutionalPhoneAreaCode2').keyup(function () {
            $('#MembershipModel_InstitutionalPhoneAreaCode2').val($(this).val());
        });
        $('#TextInstitutionalFaxAreaCode').keyup(function () {
            $('#MembershipModel_InstitutionalFaxAreaCode').val($(this).val());
        });
        $('#DropDownInstitutionalPhoneAreaCode').change(function () {
            $('#MembershipModel_InstitutionalPhoneAreaCode').val($(this).val());
       
        });
        $('#DropDownInstitutionalPhoneAreaCode2').change(function () {
            $('#MembershipModel_InstitutionalPhoneAreaCode2').val($(this).val());
        });
        $('#DropDownInstitutionalFaxAreaCode').change(function () {
            $('#MembershipModel_InstitutionalFaxAreaCode').val($(this).val());
        });
    });

    function getAreaCode(cityId, callback) {
        $.ajax({
            url: '/Membership/AreaCode',
            data: { CityId: cityId },
            success: function (msg) {
                callback(msg);
            },
            error: function (e) {
                callback('');
            }
        });
    }

    function getCultureCode(countryid, callback) {
        $.ajax({
            url: '/Membership/CultureCode',
            data: { CountryId: countryid },
            success: function (msg) {
                callback(msg);
            },
            error: function (e) {
                callback('');
            }
        });
    }


    //ilçe
    $('[data-rel="localityId"]').change(function () {
        districtWrapper.slideDown();
        $.ajax({
            url: '/Membership/Towns',
            data: { id: $(this).val() },
            success: function (msg) {
                $('[data-rel="mahalle"]' + " > option").remove();
                $.each(msg, function (i) {
                    $('[data-rel="mahalle"]').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                });
            },
            error: function (e) {
                alert(e.responseText);
            }
        });
    });

    //mahalle - köy
    $('[data-rel="mahalle"]').change(function () {
        otherElementsWrapper.slideDown();
        var townId = $(this).val();
        $('[name="MembershipModel.TownId"]').val(townId);
        if ($(this).val() != "0") {
            $.ajax({
                url: '/Membership/ZipCode',
                data: { TownId: townId },
                success: function (data) {
                    $('[name="MembershipModel.ZipCode"]').val(data);
                }
            });
        }

    });

    //bireysel, kurumsal 
    $('[data-toggle="tab"]').click(function () {
        var selectedType = ($(this).attr('href') == "#bireysel" ? 10 : 20);
        $('[data-rel="MembershipType"]').val(selectedType);
        $('[data-rel="title"]').html(selectedType == 10 ? "Ücretsiz Kayıt Olun (Bireysel)" : "Ücretsiz Kayıt Olun (Kurumsal)");
    });

    //günvelik kodu refresh
    $('[data-rel="security-code-refresh"]').click(function () {
        randomSecuirtyCode();
        $('[data-rel="security-code"]').val('');
    });
    //    $('#btnSend').click(function () {
    //        var isValidate = $(this).validationEngine('validate');
    //        if (!isValidate) {
    //            var comparePassword = $(this).val();
    //            var newPassword = $('#newPassword').val();

    //            if (comparePassword != newPassword) {
    //                setTimeout(function () {
    //                    $(this).validationEngine('showPrompt', 'Girmiş olduğunuz şifre ile doğrulama şifresi aynı değil. Tekrar deneyiniz.', 'red')
    //                }, 1000);
    //                return false;
    //            }
    //            
    //        }
    //    });

    $('[data-rel="DestinationEmail"]').keyup(function () {
        var isValidate = $(this).validationEngine('validate');
        if (!isValidate) {
            $.ajax({
                url: '/Membership/CheckMail',
                data: { email: $(this).val() },
                success: function (data) {
                    isEmail = (data == "true" ? true : false);
                    //  alert(isEmail.toString());

                    if (isEmail) {
                        setTimeout(function () {
                            $('[data-rel="DestinationEmail').validationEngine('showPrompt', 'Bu E-Posta adresi kullanılmamaktadır. Lütfen Tekrar Deneyiniz.', 'red')
                        }, 1000);

                        $('[data-rel="form-submit"]').attr('disabled', 'disabled');

                    }
                    else {
                        $('[data-rel="form-submit"]').removeAttr('disabled');
                    }

                }
            });
        }
    });
    $('[data-rel="memberEmail"]').keyup(function () {
        var isValidate = $(this).validationEngine('validate');
        if (!isValidate) {
            $.ajax({
                url: '/Membership/CheckEmailForNewMember',
                data: { email: $(this).val() },
                success: function (data) {
                    if (data) {
                        $('[data-rel="memberEmail').validationEngine('showPrompt', 'Bu E-Posta adresi kullanılmaktadır. Lütfen Tekrar Deneyiniz.', 'red');
                        $('[data-rel="form-submit"]').addClass('disabled');
                    }
                    else {
                        $('[data-rel="form-submit"]').removeClass('disabled');
                    }
                }
            });
        }
    });

    $("[data-rel='form-submit']").click(function () {
        var sehirId = $("#MembershipModel_CityId").val();
        var telNo = $("#MembershipModel_InstitutionalPhoneNumber").val();
        var gsm = $("#MembershipModel_InstitutionalGSMAreaCode").val();
        var gsmAna = $("#MembershipModel_InstitutionalGSMNumber").val();
        
        if (isEmail) {
          //  $('#formFastMembership').submit();
            $('[data-rel="email-wrapper').hide();

        }
        else {
            $('[data-rel="memberEmail').focus();
            $('[data-rel="email-wrapper').show();
        }
    });

});

//dropdown selected value control
function ifSelectNotEmpty(field, rules, i, options) {
    if (field.val() == "0") {
        return "* Bu alan zorunludur";
    }
}

$(document).on('invalid-form.validate', 'form', function () {
    $('[data-rel="form-submit"]').removeClass('disabled');
});
$(document).on('submit', 'form', function () {
    $('[data-rel="form-submit"]').addClass('disabled');
});