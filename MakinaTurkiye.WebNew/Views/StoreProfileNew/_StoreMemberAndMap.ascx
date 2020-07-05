﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTStoreMemberAndMapModel>" %>
<%string wGsm = "";
 %>
  <div class="row">
            <div class="col-sm-6">
                <table class="table table-striped">
                    <tbody>
                        <tr>
                            <th style="width: 160px;">
                                Yetkili :
                            </th>
                            <td>
                                <%:Model.AuthorizedNameSurname%>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Firma Ünvanı :
                            </th>
                            <td>
                                <%:Model.StoreName %>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                Adres :
                            </th>
                            <td>
                              
                                <%= Model.AddressMap %>
                                
                            </td>
                        </tr>
                        <% foreach (var item in Model.Phones)
                           { %>
                        <% if (item.PhoneType == (byte)PhoneType.Fax)
                           { %>
                        <tr>
                            <th>
                                Fax :
                            </th>
                            <td>
                                <%= item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber %>
                            </td>
                        </tr>
                        <% }
                           else if (item.PhoneType == (byte)PhoneType.Gsm)
                           { %>
                        <tr>
                            <th>
                                GSM :
                            </th>
                            <td>
                                <div class="hidden-xs">
                                     <%= item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber %>
                                </div>
             
                                </div>
                                <div class="visible-xs">
                                    <a style="color:#0000FF; font-weight:normal; font-size:12px; text-decoration:underline"  href="tel:<%= item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber %>">        <%= item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber %></a>
                                </div>
                              
                            </td>
                        </tr>
                        <% }
                          
                           else if(item.PhoneType==(byte)PhoneType.Phone)
                           { %>
                        <tr>
                            <th>
                                Telefon :
                            </th>
                            <td>
                                <div class="hidden-xs">
                                <%= item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber %>
                            </div>
                                <div class="visible-xs">
                                    <a style="color:#0000FF; font-weight:normal; font-size:12px; text-decoration:underline"  href="tel:<%= item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber %>"><%= item.PhoneCulture + " " + item.PhoneAreaCode + " " + item.PhoneNumber %></a>
                                </div>
                                    </td>
                        </tr>
                        <% } %>
                     <% else if(item.PhoneType==(byte)PhoneType.Whatsapp)
                           {
                               wGsm = item.PhoneCulture + " "+item.PhoneAreaCode +" "+ item.PhoneNumber;
                               
                            
                          }%>
                        <% } %>
                        <tr>
                            <th>
                                Web :
                            </th>
                            <td>
                                <%if (!string.IsNullOrEmpty(Model.StoreWebUrl))
                                  { 
                                      %>
                                <a href="<%:Model.StoreWebUrl %>"><%:Model.StoreWebUrl %></a>
                                <%} %>
                            </td>
                        </tr>
                        <%if(wGsm!=""){ %>
                               <tr>
                            <th>
                                Whatsapp:
                            </th>
                            <td>
                        
                                    <img src="/Content/SocialIcon/wp-32.png" alt="whatsapp logo"/>  <a href="https://api.whatsapp.com/send?phone=<%:wGsm.Replace("+","").Replace(" ","")%>&text=makinaturkiye.com firma sayfanız aracılığıyla" rel="external" target="_blank">Whatsapp Hattı</a>
                      
                       
                              
                            </td>
                        </tr>    
                        <%} %>
                    </tbody>
                </table>
            </div>
            <div class="col-sm-6">
                <div style="width: 100%; overflow: hidden; height: 300px;">
                    <iframe id="mainMap" height="462" width="100%" frameborder="0" scrolling="no" marginheight="0"
                        marginwidth="0" src="  https://maps.google.com/maps?q= 
			                        <%if (Model.AddressMap != "") 
											  { %>
												<%:Model.AddressMap %>
										  <% } %> &amp;num=1&amp;ie=UTF8&amp;t=m&amp;z=14&amp;iwloc=A&amp;output=embed"
                        style="border: 0; margin-top: -150px;"></iframe>
                </div>
            </div>
        </div>
    </div>