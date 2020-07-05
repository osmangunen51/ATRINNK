﻿<%@ Page Title="" Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<InvoiceModel>" %>
<html>
  <head>
    <meta charset="utf-8">
    <title><%:DateTime.Now.ToString("dd-MM-yyyy") %>-<%:Model.StoreName %></title>
    <style type="text/css">
        .clearfix:after {
  content: "";
  display: table;
  clear: both;
}
a {
  color: #5D6975;
  text-decoration: underline;
}

body {
  position: relative;
  width: 21cm;  
  height: 29.7cm; 
  margin: 0 auto; 
  color: #001028;
  background: #FFFFFF; 
  font-family: Arial, sans-serif; 
  font-size: 12px; 
  font-family: Arial;
}

header {
  padding: 10px 0;
  margin-bottom: 30px;
}

#logo {
  text-align: center;
  margin-bottom: 10px;
}

#logo img {
  width: 90px;
}

h1 {
  border-top: 1px solid  #5D6975;
  border-bottom: 1px solid  #5D6975;
  color: #5D6975;
  font-size: 2.4em;
  line-height: 1.4em;
  font-weight: normal;
  text-align: center;
  margin: 0 0 20px 0;
  background: url(dimension.png);
}

#project {
  float: left;
  
}
#project div{
    width:225px;
    white-space:normal!important;
}

#project span {
  color: #5D6975;
  text-align: right;
  width: 52px;
  margin-right: 10px;
  display: inline-block;
  font-size: 0.8em;
}


#project span  #addressDiv{
      color: #001028;
      font-size:12px;
      margin-right:10px;
 

}

#company {
  float: right;
  text-align: right;
}

#project div,
#company div {
  white-space: nowrap;        
}

table {
  width: 100%;
  border-collapse: collapse;
  border-spacing: 0;
  margin-bottom: 20px;
}

table tr:nth-child(2n-1) td {
  background: #F5F5F5;
}

table th,
table td {
  text-align: center;
}

table th {
  padding: 5px 20px;
  color: #5D6975;
  border-bottom: 1px solid #C1CED9;
  white-space: nowrap;        
  font-weight: normal;
  font-size:15px;
}

table .service,
table .desc {
  text-align: left;
}

table td {
  padding: 20px;
  text-align: right;
}

table td.service,
table td.desc {
  vertical-align: top;
    font-size:12px;
}

table td.unit,
table td.qty,
table td.total {
  font-size:12px;
}

table td.grand {
  border-top: 1px solid #5D6975;
}

#notices .notice {
  color: #5D6975;
 font-size:14px;
}

footer {
  color: #5D6975;
  width: 100%;
  height: 30px;
  position: absolute;
  bottom: 0;
  border-top: 1px solid #C1CED9;
  padding: 8px 0;
  text-align: center;
}
    </style>
      <style media="print">
 @page {
  size: auto;
  margin: 0;
  margin-top:-20px;
       }
</style>
          <script src="/Scripts/jquery.js" type="text/javascript"></script>
      <script>
          $("#writePage").click(function(){
              window.print();
          });
          function writePage(Id)
          {
              window.print();
             
              $.ajax({
                  url: '/OrderFirm/UpdateOrderInvoiceStatus',
                  data: { orderId: Id},
                  type: 'post',
                  dataType: 'json',
                  success: function (data) {
                      var e = data;
                      if (e) {
                      }
                      else {
                          alert('Düzenleme işlemi gerçekleştirelemedi');
                      }
                  }
              });
          }
      </script>
  </head>
  <body>
      <div class="writePage" style="cursor:pointer;" onclick="writePage(<%:Model.InvoiceId %>)"><a href="javascript:void(0)">Yazdır</a></div>
    <header class="clearfix">
      <div id="company" class="clearfix">
        <div>Anexxa bilişim teknolojileri ltd şti</div>
        <div>Emniyetevleri mah. Cem Sultan Cad. <br /> No:16/2 4. Levent Kağıthane İstanbul</div>
        <div>(212)-255 7152</div>
        <div><a href="mailto:bilgi@makinaturkiye.com">bilgi@makinaturkiye.com</a></div>
      </div>
      <div id="project">
        <div><b>     
            <%:Model.InvoiceNumer %></b></div>
        <div><%:Model.PacketName %></div>
        <div> <%:Model.StoreName %></div>
        <div><%:Model.InvoiceAddress %></div>
        <div> <%:DateTime.Now.ToString("dd/MM/yyyy") %></div>
     <%--   <div><span>DUE DATE</span> September 17, 2015</div>--%>
      </div>
    </header>
    <main>
      <table>
        <thead>
          <tr>
            <th class="service">İşlem</th>
            <th class="desc">Açıklama</th>
            <th>Fiyat</th>
            <th>Adet</th>
            <th>Toplam</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td class="service"><%:Model.PacketName %></td>
            <td class="desc"><%:Model.PacketBeginDate.ToString("dd/MM/yyyy") %>-<%:Model.PacketEndDate.ToString("dd/MM/yyyy") %> tarihleri arasında  <%:Model.PacketName %> üyelik</td>
            <td class="unit"><%:Model.Price.ToString("N2") %> </td>
            <td class="qty">1</td>
            <td class="total"><%:Model.Price.ToString("N2") %> </td>
          </tr>
    
          <tr>
            <td  class="total" colspan="4">Toplam</td>
            <td class="total"><%:Model.Price.ToString("N2") %> </td>
          </tr>
          <tr>
            <td colspan="4"  class="total" >Kdv <%:Model.TaxValue %>%</td>
            <td class="total"><%:Model.TaxPrice.ToString("N2") %></td>
          </tr>
          <tr>
              <td class="grand total" style="background-color:transparent;"><%:Model.PriceWord %></td>
            <td colspan="3" class="grand total">Genel Toplam</td>
            <td class="grand total" style="font-size:14px!important;"><% decimal total = Model.TaxPrice + Model.Price; %><%:total.ToString("N2") %></td>
          </tr>
        </tbody>
      </table>
    </main>
    <footer>
    Anexxa bilişim teknolojileri ltd. şti.
    </footer>
  </body>
</html>
