﻿<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Web.Models.MTHeaderTopMenuModel>" %>
                        <ul class="topmenu clearfix">
                            <li class="topmenu__item js-topmenu-item">
                                <span class="topmenu__label">Alıcılar için</span>
                                <div style="display:none;">
                                    <ul>
                                        <%foreach (var item in Model.HeaderTopMenuForHelp.ToList().Where(x=>x.HelpCategoryType==(byte)HelpCategoryPlace.ForMember))
                                          {%>
                                         <li>
                                            <a class="link" href="<%:item.Url %>"><%:item.HelpCategoryName %></a>
                                        </li>      
                                      <% } %>
                    
                                    </ul>
                                </div>
                            </li>
                            <li class="topmenu__item js-topmenu-item">
                                <span class="topmenu__label">Satıcılar için</span>
                                <div style="display:none;">
                                    <ul>
                                        <%foreach (var item in Model.HeaderTopMenuForHelp.Where(x=>x.HelpCategoryType==(byte)HelpCategoryPlace.ForStore))
                                          { %>
                                         <li>
                                            <a class="link" href="<%:item.Url %>"><%:item.HelpCategoryName %></a>
                                        </li>
                                          <%} %>
                                    </ul>
                                </div>
                            </li>
                        </ul>

