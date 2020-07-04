﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MTProductComplainModel>" %>

     <div class="modal fade" id="ComplaintModal" tabindex="-1" 
role="dialog" aria-labelledby="helpModalLabel" aria-hidden="true">
            <div class="modal-dialog">   
             
         <div class="modal-content">
         <%using (Ajax.BeginForm("ProductComplainAdd", "CatologAjax",FormMethod.Post ,  new AjaxOptions { UpdateTargetId = "satutusTalep", LoadingElementId = "loading", OnSuccess = "onSuccess", OnFailure = "ajaxError", OnBegin = "onBeginSend" }))
                  { %>
                <%:Html.ValidationSummary(true) %>
                    <div class="modal-header">
                        <button type="button" class="close" 
                        data-dismiss="modal">
                            <span aria-hidden="true">&times;
                        </span><span class="sr-only">Kapat</span></button>              
                        <h4 class="modal-title" id="H1">
                            <%:Model.ProductName %></h4>
                        <div style="font-size:14px; color:#0069ea"><%:Model.ProductCityName %></div>
                  
                  </div>
   <div class="modal-body">
       <div class="row">
                     <div class="col-md-12">
                            <div id="ajaxSuccess" style="display: none;">
                                <div class="alert alert-success alert-dismissable" style="font-size: 14px;">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                        &times;
                                    </button>
                                    <strong>Teşekkürler! </strong>
                                    En yakın zamanda inceleyip gerekli düzenlemeyi yapacağız.
                                </div>
                                <div id="ajaxError" style="display: none;">
                                    <div class="alert alert-danger alert-dismissable" style="font-size: 14px;">
                                        <button type="button" class="close" data-dismiss="alert" aria-hidden="true">
                                            &times;
                                        </button>
                                        <strong>Hata! </strong>
                                        Talebiniz şikayetiniz şu anda iletilemedi.Site yöneticisiyle irtibata geçebilirsiniz.
                                    </div>
                                </div>
                            </div>
                        </div>
       </div>
           <div class="row">
                <div class="col-md-4"><label class="control-label">Şikayet Tipi:</label></div>
                          <div class="col-md-8"> 
                                  <%:Html.HiddenFor(x => x.ProductId)%>
                                <%foreach (var complainType in Model.ComplainTypeItemModels)
                                  {%>
                                    <div class="col-md-6" style="font-size:13px;">
                                    <%:Html.CheckBox("complainTypeItems", false, new {@value=complainType.ItemId })%><%:complainType.ItemName %>  
                                     </div>
                                  <%} %>
                          </div>
          </div>
       <%if (Model.IsMember == 0)
         {  %>
              <div class="row" style="margin-top:10px;">
           <div class="col-md-4">
               <label class="control-label">
               İsim:
                   </label>
           </div>
           <div class="col-md-8">
            
               <%:Html.TextBoxFor(x => x.UserName, new { @class = "form-control", @required = "true" })%>
                  <%:Html.ValidationMessageFor(x => x.UserName)%>
           </div>
       </div>
              <div class="row" style="margin-top:10px;">
           <div class="col-md-4">
               <label class="control-label">
               Soyisim:
                   </label>
           </div>
           <div class="col-md-8">
               <%:Html.TextBoxFor(x => x.UserSurname, new { @class = "form-control", @required = "true" })%>
               <%:Html.ValidationMessageFor(x => x.UserSurname)%>
           </div>
       </div>
               <div class="row" style="margin-top:10px;">
           <div class="col-md-4">
               <label class="control-label">
               Email:
                   </label>
           </div>
           <div class="col-md-8">
               <%:Html.TextBoxFor(x => x.UserEmail, new { @class = "form-control", @required = "true", @type = "email" })%>
           </div>
       </div>
                <div class="row" style="margin-top:10px;">
           <div class="col-md-4">
               <label class="control-label">
               Telefon:
                   </label>
           </div>
           <div class="col-md-8">
               <%:Html.TextBoxFor(x => x.UserPhone, new { @class = "form-control", @required = "true", @type = "phone",@placeholder="Örn:5142456788",@maxlength="11" })%>
           </div>
       </div>
       <%}
          %>
               <div class="row" style="margin-top:10px;">
           <div class="col-md-4">
               <label class="control-label">
               Ek Mesajınız:
                   </label>
           </div>

           <div class="col-md-8">
               <%:Html.TextAreaFor(x => x.UserComment, new {@class="form-control",@cols="6",@rows="6"})%>
           </div>
       </div>
       <div class="row">
           <div class="col-md-4"><label class="control-label"></label></div>
           <div class="col-md-8">
           <div class="g-recaptcha" data-sitekey="6LemzhUUAAAAAMgP3NVU5i2ymC5iC_3bVvB466Xh"></div></div>
       </div>
        </div>
                      <div class="modal-footer">
                    <div id="loading" style="text-align: center; display: none;">
                        <img alt="Yükleniyor.." style="width: 30px; height: 30px;" src="<%:Url.Content("~/Content/v2/images/loading.gif") %>">
                    </div>
                    <button type="button" name="submitButton"  class="btn btn-default" data-dismiss="modal">İptal</button>
                    <button type="submit" class="btn btn-primary">Gönder</button>
                </div>
                  <%} %>
             </div>
          
            </div>
         </div>