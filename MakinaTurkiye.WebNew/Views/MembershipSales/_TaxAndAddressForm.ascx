<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<TaxAndAddressViewModel>" %>
<div class="row">
 <div class="col-sm-6">
                <h4>
                    Fatura Bilgileri
                </h4>
                <hr>
     
                <div class="form-group row">
                    <label class="col-sm-3 control-label">
                        Firma Adı
                    </label>
                    <div class="col-sm-6">
                        <p class="form-control-static">
                            <%:Model.StoreName %>
                        </p>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-3 control-label">
                        Vergi Dairesi
                    </label>
                    <div class="col-sm-6">
                        <%if(Model.TaxOffice==null){ %>
                            <%:Html.TextBoxFor(x => x.TaxOffice, new {@class="form-control",@id="TaxOfficeText",@placeholder="Vergi Dairesi" })%>
                        <%}else{%>
                        
                              <div id="TaxOfficeTextWrapper" style="display:none;"> <%:Html.TextBoxFor(x => x.TaxOffice, new {@class="form-control col-md-12",@id="TaxOfficeText" })%><br />
                                 <button type="button" class="btn btn-info" id="TaxOfficeSave">Kaydet</button>
                                  <button type="button" class="btn btn-info" id="TaxOfficeCancel"  >İptal</button>
                                   
                              </div>
                        <div id="TaxOfficeDisplayWrapper" class="col-md-12" style="border:1px solid #e1e1e1; font-size:12px; padding:5px; width:auto;"><span id="TaxOfficeDisplay"><%:Model.TaxOffice %></span><a style="cursor:pointer;"><i style="margin-left:10px;color:#333" class="glyphicon glyphicon-pencil"  id="TaxOfficeUpdateClick"></i></a></div> 
                        
                        <%} %>
                    </div>
                </div>
                <div class="form-group row">
                    <label class="col-sm-3 control-label">
                        Vergi No
                    </label>
                    <div class="col-sm-6">
                                     <%if(Model.TaxNumber==null){ %>
                            <%:Html.TextBoxFor(x => x.TaxNumber, new {@class="form-control",@id="TaxNumberText",@placeholder="Vergi No" })%>
                        <%}else{%>
                        
                              <div id="TaxNumberTextWrapper" style="display:none;"> <%:Html.TextBoxFor(x => x.TaxNumber, new {@class="form-control col-md-12",@id="TaxNumberText" })%><br />
                                 <button type="button" class="btn btn-info" id="TaxNumberSave">Kaydet</button>
                                  <button type="button" class="btn btn-info" id="TaxNumberCancel"  >İptal</button>
                                   
                              </div>
                        <div id="TaxNumberDisplayWrapper" class="col-md-12" style="border:1px solid #e1e1e1; font-size:12px; padding:5px; width:auto;"><span id="TaxNumberDisplay"><%:Model.TaxNumber %></span><a style="cursor:pointer;"><i style="margin-left:10px;color:#333" class="glyphicon glyphicon-pencil"  id="TaxNumberUpdateClick"></i></a></div> 
                        
                        <%} %>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <h4>
                    Fatura Adresi
                </h4>
                <hr>
                <div class="form-group row">
                    <label class="col-sm-3 control-label">
                        Adres
                    </label>
                    <div class="col-sm-6">
                         <%if(Model.Address==null){ %>
                            <%:Html.TextBoxFor(x => x.Address, new {@class="form-control", @id="AddressText" })%>
                        <%}else{%>
                        
                              <div id="AddressTextWrapper" style="display:none;"> <%:Html.TextAreaFor(x => x.Address, new {@class="form-control col-md-12",@id="AddressText",@rows="6" })%><br />
                                 <button type="button" class="btn btn-info" id="AddressSave">Kaydet</button>
                                  <button type="button" class="btn btn-info" id="AddressCancel"  >İptal</button>
                                   
                              </div>
                        <div id="AddressDisplayWrapper" class="col-md-12" style="border:1px solid #e1e1e1; font-size:12px; padding:5px; width:auto;"><span id="AddressDisplay"><%:Model.Address %></span><a style="cursor:pointer;"><i style="margin-left:10px;color:#333" class="glyphicon glyphicon-pencil"  id="AddressUpdateClick"></i></a></div> 
                        
                        <%} %>
                    </div>
                </div>
                  </div>
</div>