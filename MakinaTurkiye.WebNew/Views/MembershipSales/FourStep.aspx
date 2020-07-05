<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<PacketModel>" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index</asp:Content>--%>
<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <%decimal price = Model.MaturityCalculation(Model.OrderPrice, 0);%>
        <% if (Model.OrderType == (byte)Ordertype.Havale)
           { %>
        <% Html.BeginForm("FourStep", "MembershipSales"); %>
        <% } %>
        <%else
           { %>
           <%if (Model.CreditCardId != 14)
             {%>

             <form action="/MembershipSales/FourStepNew" method="post">
           <%}
             else
             { %>
        <form action="<%: Model.VirtualPos.VirtualPosPostUrl %>" method="post">
        <%} %>
        <% var rnd = DateTime.Now.ToString(); %>
        <%string amount = "";
          string taksit = "";
          string anahtar = "69c1365cd932bffaf715009b50ef2e1d";
          string tutar="";
          string taksitpaymec=string.Empty;
    %>
        <% if (Model.CreditCardInstallmentId > 0)
           { %>
        <% amount = (Model.GetCreditCardInstallment.CreditCardCount * ((price + (price * Model.GetCreditCardInstallment.CreditCardValue / 100)) / Model.GetCreditCardInstallment.CreditCardCount).ToString("N2").ToDecimal()).ToString("N2").Replace(".", "").Replace(",", ".");
           taksit = Model.GetCreditCardInstallment.CreditCardCount.ToString();
           tutar = amount.Replace(".", ",");
           taksitpaymec = taksit.PadLeft(2, '0');
           
      %>
        <% }
           else
           { %>
        <%
           
            amount = price.ToString("N2").Replace(".", "").Replace(",", ".");
           tutar = amount.Replace(".", ",");
           taksit = "1";
           taksitpaymec = "0";
      %>
        <% } %>
        
        
        <input type="hidden" name="clientid" value="<%=Model.VirtualPos.VirtualPosClientId%>" />
        <input type="hidden" name="amount" value="<%: amount %>" />
        <input type="hidden" name="oid" value="" />
        <input type="hidden" name="okUrl" value="<%: Model.VirtualPosReturnURL %>" />
        <input type="hidden" name="failUrl" value="<%: Model.VirtualPosReturnURL %>" />
        <input type="hidden" name="rnd" value="<%: rnd %>" />
        <input type="hidden" name="hash" value="<%: Model.GetHashStr(amount,rnd) %>" />
        <input type="hidden" name="storetype" value="<%: Model.VirtualPos.VirtualPosStoreType %>" />
        <input type="hidden" name="lang" value="tr" />
        <input type="hidden" name="tutar" id="tutar" value="" />
         <input type="hidden" name="taksit" id="taksit" value="" />

        <% } %>
        <div>
            <%if (ViewData["messageError"] != null)
              { %>
                           <div class="alert alert-danger">
                            <strong><%:ViewData["messageError"] %></strong>
                        </div> 
            <%} %>
            <%if (TempData["errorPosMessage"] != null)
                { %>
                      <div class="alert alert-danger">
                            <strong><%:TempData["errorPosMessage"] %></strong>
                        </div> 
            <%} %>
            <h5>
                <span class="glyphicon glyphicon-tag"></span>&nbsp;Sipariş Detayları
            </h5>
            <table class="table table-striped">
                <tbody>
                    <tr>
                        <td>
                            Sipariş No
                        </td>
                        <td>
                            Paket Tipi
                        </td>
                        <td>
                            Tutar
                        </td>
                        <td>
                            <b>KDV Dahil Toplam Tutar </b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%:Model.OrderNo %>
                        </td>
                        <td>
                            <%:Model.PacketName %>
                            <%Session["Paketadı"] = Model.PacketName; %>
                        </td>
                        <td>
                            <%:Model.OrderPrice.ToString("C2") %>
                        </td>
                        <td>
                            <b>
                                <%:Convert.ToDecimal(Model.OrderPrice).ToString("C2")%>
                            </b>
                        </td>
                    </tr>
                </tbody>
            </table>
            <% if (Model.OrderType == (byte)Ordertype.Havale)
               { %>
            <button type="submit" class="btn btn-primary pull-right">
                Ödemeyi Tamamla
            </button>
            <% } %>
            <% if (Model.OrderType == (byte)Ordertype.KrediKarti)
               { %>
            <div class="col-sm-5 col-md-4">

             <%if (Model.CreditCardId != 14)
             {%>
                 <div class="form-group">
                    <label>
                        Tc Kimlik No
                    </label>
                    <div class="row">
                        <div class="col-xs-12">
                            <input type="text" placeholder="Tc Kimlik Numaranız" name="tcKimlikNo" class="form-control" size="100"  />
                        </div>
                    </div>
                </div>
            <div class="form-group">
                    <label>
                        Kart İsim
                    </label>
                    <div class="row">
                        <div class="col-xs-12">
                            <input type="text" name="kartisim" class="form-control" placeholder="İsim Soyisim" size="100"  />
                        </div>
                    </div>
                </div>
                <%if(Model.Gsm==null){ %>
                 <div class="form-group">
                    <label>
                        GSM No
                    </label>
                    <div class="row">
                        <div class="col-xs-12">
                            <input type="text" name="gsm" class="form-control" size="11" placeholder="XXXX-XXX-XX-XX" />
                        </div>
                    </div>
                </div>
                <%}else{ %>
                  <%:Html.HiddenFor(x=>x.Gsm) %>  
                <%} %>
            
                 <div class="form-group">
                    <label>
                        Kredi Kart Numarası
                    </label>
                    <div class="row">
                        <div class="col-xs-12">
                            <input type="text" name="kartno" class="form-control" size="20" placeholder="XXXX-XXXX-XXXX-XXXX" />
                        </div>
                    </div>
                </div>
                 <div class="form-group">
                    <label>
                        Son Kullanma Tarihi
                    </label>
                    <div class="row">
                        <div class="col-xs-4 pr0">
                            <select name="kartay" class="form-control">
                                <option value="00">Ay</option>
                                <option value="01">1</option>
                                <option value="02">2</option>
                                <option value="03">3</option>
                                <option value="04">4</option>
                                <option value="05">5</option>
                                <option value="06">6</option>
                                <option value="07">7</option>
                                <option value="08">8</option>
                                <option value="09">9</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                            </select>
                        </div>
                        <div class="col-xs-4">
                            <select name="kartyil" class="form-control">
                                <option value="14">2014</option>
                                <option value="15">2015</option>
                                <option value="16">2016</option>
                                <option value="17">2017</option>
                                <option value="18">2018</option>
                                <option value="19">2019</option>
                                <option value="20">2020</option>
                                <option value="21">2021</option>
                                <option value="22">2022</option>
                                <option value="23">2023</option>
                                <option value="24">2024</option>
                                <option value="25">2025</option>
                                <option value="26">2026</option>
                                <option value="27">2027</option>
                                <option value="28">2028</option>
                                <option value="29">2029</option>
                                <option value="30">2030</option>
                                <option value="31">2031</option>
                                <option value="32">2032</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-6">
                            <label>
                                Güvenlik Kodu
                            </label>
                            <a href="#" class="popovers" data-container="body" data-original-title="Güvenlik Kodu(cvc2)"
                                data-toggle="popover" data-placement="right" data-content="Güvenlik kodu tüm kredi kartlarının arka yüzünde bulunan 3 haneli numaradır. Bu 3 haneli numara kartınızın arka yüzünde yazılıdır. ">
                                <span class="glyphicon glyphicon-question-sign"></span></a>
                            <input type="text" class="form-control" name="kartcvv" size="4" maxlength="3" id="kartcvv"
                                placeholder="XXX">
                        </div>
                        <div class="col-xs-6">
                            <label>
                                Kart Tipi
                            </label>
                            <select name="cardType" class="form-control">
                                <option value="1">Visa</option>
                                <option value="2">MasterCard</option>
                            </select>
                        </div>
                    </div>
                </div>
           <%}
             else
             { %>

                <div class="form-group">
                    <label>
                        Kredi Kart Numarası
                    </label>
                    <div class="row">
                        <div class="col-xs-12">
                            <input type="text" name="pan" class="form-control" size="20" placeholder="XXXX-XXXX-XXXX-XXXX" />
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <label>
                        Son Kullanma Tarihi
                    </label>
                    <div class="row">
                        <div class="col-xs-4 pr0">
                            <select name="Ecom_Payment_Card_ExpDate_Month" class="form-control">
                                <option value="00">Ay</option>
                                <option value="01">1</option>
                                <option value="02">2</option>
                                <option value="03">3</option>
                                <option value="04">4</option>
                                <option value="05">5</option>
                                <option value="06">6</option>
                                <option value="07">7</option>
                                <option value="08">8</option>
                                <option value="09">9</option>
                                <option value="10">10</option>
                                <option value="11">11</option>
                                <option value="12">12</option>
                            </select>
                        </div>
                        <div class="col-xs-4">
                            <select name="Ecom_Payment_Card_ExpDate_Year" class="form-control">
                                <option value="14">2014</option>
                                <option value="15">2015</option>
                                <option value="16">2016</option>
                                <option value="17">2017</option>
                                <option value="18">2018</option>
                                <option value="19">2019</option>
                                <option value="20">2020</option>
                                <option value="21">2021</option>
                                <option value="22">2022</option>
                                <option value="23">2023</option>
                                <option value="24">2024</option>
                                <option value="25">2025</option>
                                <option value="26">2026</option>
                                <option value="27">2027</option>
                                <option value="28">2028</option>
                                <option value="29">2029</option>
                                <option value="30">2030</option>
                                <option value="31">2031</option>
                                <option value="32">2032</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="row">
                        <div class="col-xs-6">
                            <label>
                                Güvenlik Kodu
                            </label>
                            <a href="#" class="popovers" data-container="body" data-original-title="Güvenlik Kodu(cvc2)"
                                data-toggle="popover" data-placement="right" data-content="Güvenlik kodu tüm kredi kartlarının arka yüzünde bulunan 3 haneli numaradır. Bu 3 haneli numara kartınızın arka yüzünde yazılıdır. ">
                                <span class="glyphicon glyphicon-question-sign"></span></a>
                            <input type="text" class="form-control" name="cv2" size="4" maxlength="3" id="exampleInputEmail1"
                                placeholder="XXX">
                        </div>
                        <div class="col-xs-6">
                            <label>
                                Kart Tipi
                            </label>
                            <select name="cardType" class="form-control">
                                <option value="1">Visa</option>
                                <option value="2">MasterCard</option>
                            </select>
                        </div>
                    </div>
                </div>
        <%} %>
                <button type="submit" class="btn btn-primary">
                    Ödemeyi Tamamla
                </button>
            </div>
            <% } %>
        </div>
        <% if (Model.OrderType == (byte)Ordertype.Havale)
           { %>
        <% Html.EndForm(); %>
        <% } %>
        </form>
    </div>
    <% if (Model.OrderType == (byte)Ordertype.KrediKarti)
       { %>
    <div class="col-sm-7 col-md-8">
        <div class="alert alert-info">
            <span class="glyphicon glyphicon-bullhorn"></span><b>&nbsp;3D güvenli ödeme (3D Secure)
                sistemi nedir? </b>
            <br>
            3D yani şifreli ödeme, elektronik ortamda yapılan kart işlemlerinin güvenliğinin
            artırılması için Visa ve Mastercard tarafından geliştirilmiş bir ödeme sistemidir.
            3D ile ödeme sırasında banka, kart sahibinden şifresini isteyerek kimlik bilgileri
            doğrulaması yapar. Böylece sistem, şifreyi bilmeyen kişilerin yani yetkisiz kişilerin
            işlem yapmasını engellenmiş olur.
        </div>
        <div class="col-xs-6">
            <img class="img-thumbnail" alt=".." src="../../Content/V2/images/kredi-karti.png" />
        </div>
        <div class="col-xs-6">
            <% if (Model.CreditCardInstallmentId > 0)
               { %>
            <table class="table">
                <thead>
                    <tr>
                        <th colspan="2">
                            Detaylar
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            KDV Tutar
                        </td>
                        <td>
                            <%-- <%:((Model.OrderPrice * (18) / 100) / (118 / 100)).ToString("C2")%>--%>
                            <% var kdv = Convert.ToDecimal(1.18); %>
                            <%:(Model.OrderPrice - (Model.OrderPrice / kdv)).ToString("C2") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Vade Farkı
                        </td>
                        <td>
                            <%:(Model.OrderPrice * (Model.GetCreditCardInstallment.CreditCardValue) / 100).ToString("C2")%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Taksit
                        </td>
                        <td>
                            <%:Model.GetCreditCardInstallment.CreditCardCount%>
                            X
                            <%: ((price + (price * Model.GetCreditCardInstallment.CreditCardValue / 100)) / Model.GetCreditCardInstallment.CreditCardCount).ToString("C2")%>
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="success">
                        <td>
                            Toplam Tutar
                        </td>
                        <td>
                            <%: (Model.GetCreditCardInstallment.CreditCardCount * ((price + (price * Model.GetCreditCardInstallment.CreditCardValue / 100)) / Model.GetCreditCardInstallment.CreditCardCount).ToString("N2").ToDecimal()).ToString("C2")%>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <% }
               else
               { %>
            <table class="table">
                <thead>
                    <tr>
                        <th colspan="2">
                            Detaylar
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            KDV Tutar
                        </td>
                        <td>
                            <%-- <%:(Model.OrderPrice * (118) / 100).ToString("C2") %>--%>
                            <% var kdv = Convert.ToDecimal(1.18); %>
                            <%:(Model.OrderPrice - (Model.OrderPrice / kdv)).ToString("C2") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Vade Farkı
                        </td>
                        <td>
                            -
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Taksit
                        </td>
                        <td>
                            -
                        </td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr class="success">
                        <td>
                            Toplam Tutar
                        </td>
                        <td>
                           <%-- <%:Model.MaturityCalculation(Model.OrderPrice, 18).ToString("C2")%>--%>
                            <%:Model.OrderPrice.ToString("C2")%>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <% } %>
        </div>
    </div>
    <% } %>
</asp:Content>
