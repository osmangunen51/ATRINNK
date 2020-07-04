$(document).ready(function() {
    ChooseMembershipForm();
});

function ChooseMembershipForm() {
    $('input:radio[name=membership]').change(function () {
        if (this.value == 'login') {
            $("#LogInContainer").show();
            $("#RegisterContainer").hide();
            $("#MemberShipHeader").html("Giriş Yap");
        }
        else if (this.value == 'register') {
            $("#LogInContainer").hide();
            $("#RegisterContainer").show();
            $("#MemberShipHeader").html("Üye Ol");
        }
    });
}