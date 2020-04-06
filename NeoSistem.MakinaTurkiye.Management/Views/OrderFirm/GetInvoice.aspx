﻿<%@ Page Title="" Language="C#" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<InvoiceModel>" %>

<html>
<head>
    <meta charset="utf-8">
    <title><%:DateTime.Now.ToString("dd-MM-yyyy") %>-<%:Model.StoreName %></title>
    <style type="text/css">
        body { font-family: Arial, sans-serif; font-size: 12px; }
        table tr td { font-size: 12px; padding-top: 20px; }

        #border_bottom td.grand { border-top: thin solid #5D6975; padding: 0px; }
    </style>
    <style media="print">
        @page { size: auto; margin: 0; margin-top: -20px; }
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
                  url: '/OrderFirm/OrderWriteLogAdd',
                  data: { orderId: Id },
                  type: 'post',
                  dataType: 'json',
                  success: function (data) {
                      var e = data;
                      if (e) {
                     
                      }
                  }
              });
          }
    </script>
</head>
<body>
    <div class="writePage" style="cursor: pointer;" onclick="writePage(<%:Model.InvoiceId %>)"><a href="javascript:void(0)">Yazdır</a></div>

    <div style="top: 230px; position: absolute; left: 110px; width: 200px;">
        <div style="width: 225px;"><%:Model.StoreName %></div>
        <div style="width: 225px;"><%:Model.InvoiceAddress %></div>
    </div>
    <div style="left: 145px; position: absolute; top: 340px;">
        <%:Model.TaxOffice %> &nbsp;/ &nbsp;<%:Model.TaxNo %>
    </div>
    <div style="top: 309px; left: 618px; position: absolute;">
        
        <div><%:Model.InvoiceDate.ToString("dd/MM/yyyy") %></div>
    </div>
    <div style="top: 345px; left: 618px; position: absolute;">
        <div><%:DateTime.Now.Hour %>:<%:DateTime.Now.Minute %></div>
    </div>
    <div style="top: 400px; left: 55px; position: absolute">
        <table>

            <tr>
                <td style="width: 115px; height: auto">
                    <%if (Model.OrderNo == null)
                        { %>
                            ------
                
                            <%}
                                else
                                {%>
                    <%:Model.PacketName %>
                    <%} %>
                     
                </td>
                <td style="width: 285px; height: auto;">
                    <%if (Model.OrderNo != null)
                        { %>


                    <%:Model.PacketBeginDate.ToString("dd/MM/yyyy") %>-<%:Model.PacketEndDate.ToString("dd/MM/yyyy") %> tarihleri arasında  <%:Model.PacketName %> üyelik
                            <%}
            else
            {%>  <%:Model.OrderDescription %>

                    <%} %>
                </td>
                <td style="width: 80px; height: auto">1
                </td>
                <td style="width: 95px; height: auto;">
                    <%:Model.NormalPrice!=0 ? Model.NormalPrice.ToString("N2") : Model.Price.ToString("N2") %> 
                </td>
                <td style="width: 60px; text-align: right; height: auto;">
                    <%:Model.NormalPrice!=0 ? Model.NormalPrice.ToString("N2") : Model.Price.ToString("N2") %> 
                </td>
            </tr>
            <%if (Model.NormalPrice != 0)
                {%>
            <tr>
                <td colspan="3"></td>
                <td>İndirim
                      <%:Model.DiscountAmount.HasValue?" Miktarı":" Oranı" %>
                </td>
                <td style="text-align: right; width: 30px;">
                    <%:Model.DiscountPercentage.HasValue?"%"+Model.DiscountPercentage:Model.DiscountAmount.Value.ToString("N2") %>
                </td>
            </tr>
            <% } %>

            <tr style="padding-top: 20px;">
                <td colspan="3"></td>
                <td>Toplam</td>
                <td style="text-align: right; width: 30px;"><%:Model.Price.ToString("N2") %> </td>
            </tr>

            <tr style="border-bottom: 1px; solid: #333;">
                <td colspan="3"></td>
                <td>KDV<%:Model.TaxValue %>%</td>
                <td style="text-align: right; width: 30px;"><%:Model.TaxPrice.ToString("N2") %>  </td>
            </tr>
            <tr style="height: 5px!important;" id="border_bottom">
                <td class="grand" colspan="5"></td>
            </tr>
            <tr>
                <td class="grand" style="background-color: transparent;"><%:Model.PriceWord %></td>
                <td colspan="2"></td>
                <td class="grand">Genel Toplam</td>
                <td class="grand" style="text-align: right; width: 40px;"><% decimal total = Model.TaxPrice + Model.Price; %><%:total.ToString("N2") %> </td>
                <td>TL
                </td>
            </tr>

        </table>
    </div>
</body>
</html>
