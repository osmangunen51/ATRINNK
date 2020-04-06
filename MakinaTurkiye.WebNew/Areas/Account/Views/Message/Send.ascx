﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MessageViewModel>" %>
<script type="text/javascript">
    $(document).ready(function () {
        $('#formSend').validationEngine();
    });
</script>
<div>
    <h4 class="mt0 text-info">
        <span class="text-primary glyphicon glyphicon-cog"></span>&nbsp;Mesaj Gönder
    </h4>
    <div class="well well-mt2">
        <div class="row">
            <div class="col-sm-12 col-md-8">
                <%if (ViewData["receiverMember"] != null)
                  {%>
                   <%  var member = (MakinaTurkiye.Entities.Tables.Members.Member)ViewData["receiverMember"]; %>
                <%using (Html.BeginForm("Index", "Message", FormMethod.Post, new { id = "formSend", @class = "form-horizontal" }))
                  {%>
                <%if (Model.MemberMessageDetail.Member != null)
                  {  %>
             
                <input type="hidden" name="Message.MainPartyId" id="Hidden1" class="MainPartyId"
                    value="<%:member.MainPartyId %>" />
                <input type="hidden" name="Message.ProductId" id="Hidden2" value="<%:Model.Message.ProductId  %>" />
                <%} %>
                <%else
                  {  %>
                <input type="hidden" name="Message.MainPartyId" class="MainPartyId" id="hidden3" />
                <%} %>
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        Kime
                    </label>
                    <div class="col-sm-10">
                        <%if( ViewData["receiverMember"]!=null){
                            
                              
                              %>
                                <%= Html.TextBoxFor(m => m.Message.MainPartyFullName,   new Dictionary<string, object> { { "class", "form-control" }, 
                                  { "data-validation-engine", "validate[required]" },{"Value",member.MemberName+" "+member.MemberSurname}})%>
                           
                        <%}else { %>
                        
                                                <%= Html.TextBoxFor(m => m.Message.MainPartyFullName,   new Dictionary<string, object> { { "class", "form-control" }, 
                                  { "data-validation-engine", "validate[required]" } })%>
                        <%} %>
                    </div>
                </div>
                <% var advertNoExist = Model.Message.Subject.IndexOf("İlan");
                   if(advertNoExist==-1)
                   { 
                     %>
               <div class="form-group">
                         <label class="col-sm-2 control-label">
                            Ürün Adı
                        </label>
                        <div class="col-sm-10" style="font-size:14px;">
                          <a href="<%:Model.ProductUrl %>"><%:Model.Product.ProductName %></a>
                        </div>
                </div>
                <%} %>
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        Konu
                    </label>
                    <div class="col-sm-10">
                 
                        <%= Html.TextBoxFor(m => m.Message.Subject,  new Dictionary<string, object> { { "class", "form-control" }, 
                                  { "data-validation-engine", "validate[required]" },
                                  { "placeholder", "Lütfen konu giriniz." } })%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        Mesajınız
                    </label>
                    <div class="col-sm-10">
                        <%= Html.TextAreaFor(m => m.Message.Content, new Dictionary<string, object> { {"rows","5"}, {"class", "form-control" }, 
                                  { "data-validation-engine", "validate[required]" },
                                  { "placeholder", "Lütfen Mesajınızı Yazınız." } })%>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-3 col-sm-9">
                        <div class="btn-group">
                            <button type="submit" class="btn btn-primary">
                                Gönder
                            </button>
                        </div>
                    </div>
                </div>
                <%} %>
                <% } %>
                <%else
                  { %>
                <%using (Html.BeginForm("SaveMessage", "Message", FormMethod.Post, new { id = "formSend", @class = "form-horizontal" }))
                  {%>
                <%if (Model.MemberMessageDetail.Member != null)
                  {  %>
                <input type="hidden" name="Message.MainPartyId" id="MainPartyId" class="MainPartyId"
                    value="<%:Model.MemberMessageDetail.Member.MainPartyId  %>" />
                <input type="hidden" name="Message.ProductId" id="ProductId" value="<%:Model.Message.ProductId  %>" />
                <%} %>
                <%else
                  {  %>
                <input type="hidden" name="Message.MainPartyId" />
                <%} %>
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        Kime
                    </label>
                    <div class="col-sm-10">
                        <%= Html.TextBoxFor(m => m.Message.MainPartyFullName,   new Dictionary<string, object> { { "class", "form-control" }, 
                                  { "data-validation-engine", "validate[required]" } })%>
                    </div>
                </div>
                <div class="form-group">
                         <label class="col-sm-2 control-label">
                        Ürün Adı
                    </label>
                    <div class="col-sm-10"  style="font-size:14px;">
                     <a href="<%:Model.ProductUrl %>" > <%:Model.ProductName %></a>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        Konu
                    </label>
                    <div class="col-sm-10" >
                  
                        <%= Html.TextBoxFor(m => m.Message.Subject,  new Dictionary<string, object> { { "class", "form-control" }, 
                                  { "data-validation-engine", "validate[required]" },
                                  { "placeholder", "Lütfen konu giriniz." } })%>
                    </div>
                </div>
                <div class="form-group">
                    <label class="col-sm-2 control-label">
                        Mesajınız
                    </label>
                    <div class="col-sm-10">
                        <%= Html.TextAreaFor(m => m.Message.Content, new Dictionary<string, object> { {"rows","5"}, {"class", "form-control" }, 
                                  { "data-validation-engine", "validate[required]" },
                                  { "placeholder", "Lütfen Mesajınızı Yazınız." } })%>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-9">
                        <div class="btn-group">
                            <button type="submit" class="btn btn-primary">
                                Gönder
                            </button>
                        </div>
                    </div>
                </div>
                <%} %>
                <%} %>
            </div>
            <div class="col-sm-12 col-md-4">
                <div class="alert alert-info">
                    <span class="glyphicon glyphicon-info-sign"></span><strong>&nbsp;Güvenlik İpuçları: </strong>
                    <br />
                    <br />
                    <i class="fa fa-caret-right fa-fw"></i>İlgilendiğiniz ilanda bulunan ürünü görmeden
                    kaparo olarak bir ödeme yapmayın, para göndermeyin.
                    <br />
                    <br />
                    <i class="fa fa-caret-right fa-fw"></i>Satın almayı düşündüğünüz ürünü test etmeniz
                    tavsiye edilir.
                    <br />
                    <br />
                    <i class="fa fa-caret-right fa-fw"></i>İlanda belirtilen herhangi bir bilgi ya da
                    görselin gerçeği yansıtmadığını düşünüyorsanız, lütfen bize haber verin.
                    <br />
                    <br />
                    Detaylı bilgi için <a href="#">tıklayın. </a>
                </div>
            </div>
        </div>
    </div>
</div>
