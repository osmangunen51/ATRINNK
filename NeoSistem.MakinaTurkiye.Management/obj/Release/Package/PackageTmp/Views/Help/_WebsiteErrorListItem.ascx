<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<NeoSistem.MakinaTurkiye.Management.Models.WebSiteErrorListItem>>" %>
  <%foreach (var item in Model.ToList())
                    {%>
                <tr id="row<%:item.WebSiteErrorId %>">
                    <td class="Cell"><%:item.ProblemTypeText %></td>
                    <td class="Cell"><%:item.Title %></td>
                    <%if (item.Content.Length > 50)
                        {
                            item.Content = item.Content.Substring(0, 50) + "...";
                        }
                    %>
                    <td class="Cell"><%:item.Content %></td>
                    <td class="Cell"><%:item.UserName %></td>
                    <td class="Cell">
                        <%if (!string.IsNullOrEmpty(item.FilePath))
                            {
                        %>
                        <a target="_blank" href="<%:Url.Content(item.FilePath) %>">Dosya</a>
                        <%} %>
                    
                    </td>
                    <td class="Cell">
                        <%:item.RecordDate.HasValue == true ? item.RecordDate.Value.ToString("dd/MM/yyyy") : "" %>
                    </td>

                    <%if (item.IsSolved)
                        {%>
                    <td class="Cell" style="font-weight: 600; color: #09a825">Yapıldı
                    </td>
                    <% }
                        else
                        {%>
                    <td class="Cell" style="font-weight: 600;">Bekliyor
                    </td>
                    <%}%>

                    <td class="Cell">
                        <div style="float: left;">
                            <a style="cursor: pointer;" onclick="DeleteError(<%:item.WebSiteErrorId %>)">
                                <img src="/Content/images/delete.png" />
                            </a>
                            <a href="/Help/ErrorEdit/<%: item.WebSiteErrorId %>">
                                <img src="/Content/images/edit.png" />
                            </a>
                            <%if (!item.IsFirst)
                                { %>
                            <a href="/Help/MakeFirst/<%:item.WebSiteErrorId %>">Öncelik Ver
                            </a>
                            <%}
                                else
                                {%>
                            <b>Öncelikli</b>
                            <% } %>
                        </div>
                    </td>
                </tr>
                <%} %>