﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.MemberShip.MTLoginViewModel>" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Giriş Yap - makinaturkiye.com
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <link rel="canonical" href="https://www.makinaturkiye.com/uyelik/kullanicigirisi" />
    <script src="https://www.google.com/recaptcha/api.js" async defer></script>
     <script src="https://apis.google.com/js/api:client.js"></script>
    <style>
        .abcRioButton {
            width:100%!important;
    
        }
    </style>
      <script>
  var googleUser = {};
  var startApp = function() {
    gapi.load('auth2', function(){
      // Retrieve the singleton for the GoogleAuth library and set up the client.
      auth2 = gapi.auth2.init({
        client_id: '638267544487-nqpu1s475ju88si2rpper76f0rs5e03o.apps.googleusercontent.com',
        cookiepolicy: 'single_host_origin',
        // Request scopes in addition to 'profile' and 'email'
        scope: 'profile email'
      });
      attachSignin(document.getElementById('customBtn'));
    });
  };

  function attachSignin(element) {
    console.log(element.id);
    auth2.attachClickHandler(element, {},
        function(googleUser) {
          document.getElementById('name').innerText = "Signed in: " +
              googleUser.getBasicProfile().getName();
        }, function(error) {
          alert(JSON.stringify(error, undefined, 2));
        });
  }
  </script>
    <style type="text/css">
    #customBtn {
      display: inline-block;
      background: white;
      color: #444;
      width: 190px;
      border-radius: 5px;
      border: thin solid #888;
      box-shadow: 1px 1px 1px grey;
      white-space: nowrap;
    }
    #customBtn:hover {
      cursor: pointer;
    }
    span.label {
      font-family: serif;
      font-weight: normal;
    }
    span.icon {
      background: url('/identity/sign-in/g-normal.png') transparent 5px 50% no-repeat;
      display: inline-block;
      vertical-align: middle;
      width: 42px;
      height: 42px;
    }
    span.buttonText {
      display: inline-block;
      vertical-align: middle;
      padding-left: 42px;
      padding-right: 42px;
      font-size: 14px;
      font-weight: bold;
      /* Use the Roboto font that is loaded in the <head> */
      font-family: 'Roboto', sans-serif;
    }
  </style>
    <script type="text/javascript">
function onSignIn(googleUser) {
    var profile = googleUser.getBasicProfile();
     console.log('ID: ' + profile.getId()); // Do not send to your backend! Use an ID token instead.
  console.log('Name: ' + profile.getName());
  console.log('Image URL: ' + profile.getImageUrl());
  console.log('Email: ' + profile.getEmail()); // This is null if the 'email' scope is not present.
   /* $.ajax({

    url : '/MemeberShip/SocialMember',
    type : 'GET',
    data : {
 JSON.stringify({
            "model": {
                "MemberName": profile.getName(),
                "MemberSurname": profile.last_name,
                "MemberEmail": response.email,
                "MemberEmailAgain": response.email,
                "LocalityId": "0",
                "TownId": "0",
                "CityId": "0",
                "CountryId": "0",
                "AddressTypeId": "0"
            },
            "MembershipType": "5",
            "profileId": response.id
    },
    dataType:'json',
    success : function(data) {              
        alert('Data: '+data);
    },
    error : function(request,error)
    {
        alert("Request: "+JSON.stringify(request));
    }
});*/

}
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row sign-in-container">



        <div class="col-xs-12 col-sm-5 col-md-4">
            <%if (TempData["MessageError"] != null)
                { %>
            <div class="alert alert-info">
                <i class="glyphicon glyphicon-exclamation-sign"></i>&nbsp;
        <span style="font-size: 18px;">
            <%=TempData["MessageError"].ToString() %></span>
            </div>
            <%}
                else
                {
                    if (Request.QueryString["ReturnUrl"] != null)
                    {%>
            <div class="alert alert-info">
                <i class="glyphicon glyphicon-exclamation-sign"></i>&nbsp;
        <span style="font-size: 18px;">Bu sayfayı görmek için üye girişi yapmalısınız.</span>
            </div>
            <% 
                    }
                } %>

            <div class="alert alert-danger mt-alert-danger" role="alert" id="MembershipError" style="display: none;">
            </div>
            <%if (!string.IsNullOrEmpty(Model.MembershipViewModel.ErrorMessage))
                {%>
            <div class="alert alert-danger mt-alert-danger" role="alert">
                <%:Model.MembershipViewModel.ErrorMessage %>
            </div>
            <% } %>
            <div class="loading-membership">
                <img src="../../Content/V2/images/menu-loading.gif" style="width: 64px" />
            </div>
            <ul class="nav nav-tabs login-tabs">

                <li class="<%:Model.LoginTabType==(byte)LoginTabType.Login?"active":""  %>"><a data-toggle="tab" href="#login">Giriş Yap</a></li>
                <li class="<%:Model.LoginTabType==(byte)LoginTabType.Register?"active":""  %>" id="map"><a data-toggle="tab" href="#register">Üye Ol</a></li>

            </ul>

            <div class="tab-content login-container" id="myTabContent">

                <div class="tab-pane fade <%:Model.LoginTabType==(byte)LoginTabType.Login?"active in":""  %>" id="login" role="tabpanel" aria-labelledby="login">
                    <%=Html.RenderHtmlPartial("_LoginForm", Model.LoginModel) %>
                </div>
                <div class="tab-pane fade <%:Model.LoginTabType==(byte)LoginTabType.Register?"active in":""  %>" id="register" role="tabpanel" aria-labelledby="register">
                    <% using (Html.BeginForm("RegisterOnLogin", "membership", FormMethod.Post, new { @id = "register-form" }))
                        {%>
                    <input type="hidden" name="PageType" value="LogOn" />
                    <%=Html.RenderHtmlPartial("_RegisterForm", Model.MembershipViewModel) %>
                    <% } %>
                </div>

            </div>


        </div>
    </div>
</asp:Content>
