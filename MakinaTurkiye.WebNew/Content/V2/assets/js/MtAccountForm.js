$(document).ready(function () {

    ChangeAdress();
    PersonalUpdateValidation();
    PhoneActiveValidation();
}
);
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
function ChangeAdressValidation() {
    $('#formFastMembership1').bootstrapValidator({
        message: 'Bu değer doğru değildir',
        feedbackIcons: {
            valid: 'fa fa-sync',
            invalid: 'fa fa-exclamation',
            validating: 'fa fa-check'
        },
        fields: {
            CountryId: {
                validators: {
                    notEmpty: {
                        message: 'Ülke Boş Geçilemez'

                    }
                }
            }
        }
    });
}
function PersonalUpdateValidation() {
    $('#personal-update-form').bootstrapValidator({
        message: 'Bu değer valid değildir',
        feedbackIcons: {

        },
        fields: {
            MemberName: {
                message: 'İsim alanı zorunludur',
                validators: {
                    notEmpty: {
                        message: 'İsim alanı boş geçilemez'
                    },
                }
            },
            MemberSurname: {
                validators: {
                    notEmpty: {
                        message: 'Soyisim alanı boş geçilemez'
                    }

                }
            }
        }
    });
}
function ChangeAdress() {

    var cityId = $('[data-rel="cityId"] :selected').val();
    var areaCode = $('[data-rel="localityId"] :selected').val();
    var countryId = $('[data-rel="countryId"] :selected').val();
    var cityWrapper = $('[data-rel="city-wrapper"]');
    var addressWrapper = $('[data-rel="address-wrapper"]');
    var localityWrapper = $('[data-rel="locality-wrapper"]');
    var districtWrapper = $('[data-rel="district-wrapper"]');
    var otherElementsWrapper = $('[data-rel="other-wrapper"]');
    var phoneWrapper = $('[data-rel="phone-wrapper"]');
    var gsmWrapper = $('[data-rel="gsm-wrapper"]');
    if (countryId != 246 && countryId > 0) {
        cityWrapper.slideUp();
        localityWrapper.slideUp();
        districtWrapper.slideUp();
        otherElementsWrapper.slideUp();
        addressWrapper.slideDown();
        gsmWrapper.slideUp();
        $('[data-rel="cityId"]' + " > option").remove();
        $('#TextInstitutionalPhoneAreaCode2').keyup(function () {
            $('#InstitutionalPhoneAreaCode2').val($(this).val());
        });
        $('#TextInstitutionalPhoneAreaCode2').val($("#InstitutionalPhoneAreaCode2").val());
        $('#MembershipModel_InstitutionalPhoneCulture,#MembershipModel_InstitutionalPhoneCulture2,#MembershipModel_InstitutionalFaxCulture').val("");
        $('#DropDownInstitutionalPhoneAreaCode,#DropDownInstitutionalPhoneAreaCode2,#DropDownInstitutionalFaxAreaCode').hide();
    }
    else {
        if (cityId != "") { $("#other-wrapper").show(); }
        if (cityId == 34) {//istanbul
            $('#DropDownInstitutionalPhoneAreaCode,#DropDownInstitutionalPhoneAreaCode2,#DropDownInstitutionalFaxAreaCode').show();
            $('#TextInstitutionalPhoneAreaCode,#TextInstitutionalPhoneAreaCode2,#TextInstitutionalFaxAreaCode').val('').hide();
        } else {
            $('#TextInstitutionalPhoneAreaCode,#TextInstitutionalPhoneAreaCode2,#TextInstitutionalFaxAreaCode').show();

            $('#DropDownInstitutionalPhoneAreaCode,#DropDownInstitutionalPhoneAreaCode2,#DropDownInstitutionalFaxAreaCode').hide();
        }

        $('#DropDownInstitutionalPhoneAreaCode').change(function () {

            $("#InstitutionalPhoneAreaCode").val($(this).val());

        });

        $('#DropDownInstitutionalPhoneAreaCode2').change(function () {

            $("#InstitutionalPhoneAreaCode2").val($(this).val());

        });
        $("#MembershipModel_InstitutionalGSMAreaCode").change(function () {

            if ($(this).val() != 0) {
                $("#InstitutionalGSMAreaCode").val($(this).val());
            }
        });
        $('#DropDownInstitutionalFaxAreaCode').change(function () {

            $("#InstitutionalFaxAreaCode").val($(this).val());

        });
        $("#InstitutionalPhoneNumber").keyup(function () {
            if ($(this).val() != "")
                $('#DropDownInstitutionalPhoneAreaCode').attr("data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]");
            else
                $('#DropDownInstitutionalPhoneAreaCode').removeAttr("data-validation-engine");

        });
        $("#InstitutionalPhoneNumber2").keyup(function () {
            if ($(this).val() != "")
                $('#DropDownInstitutionalPhoneAreaCode2').attr("data-validation-engine", "validate[funcCall[ifSelectNotEmpty]]");
            else
                $('#DropDownInstitutionalPhoneAreaCode2').removeAttr("data-validation-engine");

        });

    }

    var isEmail = true;
    var cityWrapper = $('[data-rel="city-wrapper"]');
    var addressWrapper = $('[data-rel="address-wrapper"]');
    var localityWrapper = $('[data-rel="locality-wrapper"]');
    var districtWrapper = $('[data-rel="district-wrapper"]');
    var otherElementsWrapper = $('[data-rel="other-wrapper"]');
    var phoneWrapper = $('[data-rel="phone-wrapper"]');
    var gsmWrapper = $('[data-rel="gsm-wrapper"]');

    //ülke
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

}
function ChangeAdressValidation() {



    $('#change-adress-form').bootstrapValidator({
        message: 'Bu değer valid değildir',
        feedbackIcons: {

        },
        fields: {
            MembershipModel_AvenueOtherCountries: {
                validators: {
                    notEmpty: {
                        message:"Adres Giriniz"
                    }
                }
            },
            CountryId: {
                message: 'Bu alan zorunludur',
                validators: {
                    callback:
                        {
                            message: 'Ülke Seçiniz',
                            callback: function (value, validator, $field) {
                                if (value === '0') {
                                    return true;
                                }
                            }
                        }

                }
            },
            CityId: {
                message: 'Bu alan zorunludur',
                validators: {
                    callback:
                        {
                            message: 'Şehir Seçiniz',
                            callback: function (value, validator, $field) {
                                if (value === '0') {
                                    return true;
                                }
                            }
                        }

                }
            },
            LocalityId: {
                message: 'Bu alan zorunludur',
                validators: {
                    callback:
                        {
                            message: 'İlçe seçiniz',
                            callback: function (value, validator, $field) {
                                if (value === '0') {
                                    return true;
                                }
                            }
                        }

                }
            },
            TownId: {
                message: 'Bu alan zorunludur',
                validators: {
                    callback:
                        {
                            message: 'Mahalle Seçiniz',
                            callback: function (value, validator, $field) {
                                if (value === '0') {
                                    return true;
                                }
                            }
                        }

                }
            }
        }
    });


}
function PhoneActiveValidation() {
    $('#phone-active-form').bootstrapValidator({
        message: 'Bu değer valid değildir',
        feedbackIcons: {

        },
        fields: {
            activationCode: {
                message: 'Bu alan zorunludur',
                validators: {
                    notEmpty: {
                        message: 'Aktivasyon alanı boş geçilemez'
                    },
                    stringLength: {
                        min: 6,
                        message: 'Aktivasyon kodu en az 6 karakterden oluşmalıdır'
                    }
                }
            }
        }
    });
}