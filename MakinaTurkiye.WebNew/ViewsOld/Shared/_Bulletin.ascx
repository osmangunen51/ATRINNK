<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<div class="subscribe">
    <div class="container">
        <div class="row">
            <div class="col-md-5 col-sm-5 col-md-offset-1">
                <span>Yeniliklerimizden Haberdar Olun</span>
            </div>
            <div class="col-md-5 col-sm-8">
                <form action="/uyelik/bulten-uyeligi" method="get" class="clearfix inputItem">
                    <input type="text" autocomplete="off" name="email" placeholder="E-Posta Adresinizi Giriniz">
                    <button>ABONE OL</button>
                </form>
            </div>
        </div>
    </div>
</div>
