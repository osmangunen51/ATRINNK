<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<List<MTProductPictureModel>>" %>

<%if (Model.Count > 0)
    {%>
            <div class="thumbnail thumbnail-mt2 pr">
                <div class="btn-group mega-photo-container">
                    <a href="#" class="btn btn-md btn-mt2-1 btn-mega-photo" data-toggle="modal" data-target="#megafotoContent">
                        <span class="glyphicon glyphicon-camera"></span>
                        &nbsp;Mega Foto
                    </a>
                </div>
                <div class="tab-content">

                    <%for (int i = 0; i < Model.Count; i++)
                      { %>
                           <%if (i == 0)
                               {%>
                        <div id="urunresim-<%: i %>" class="tab-pane active">
                       
                            <img src="<%:Model[i].LargePath %>" alt="<%:Model[i].ProductName %> <%:i + 1 %>"  title="<%:Model[i].ProductName %> <%:i + 1 %>" class="img-responsive" width='500px' height='375px'/>
                            <%--<img src="../../Content/mkwm400x300.png" class="watermark400300" />--%>
            
                        </div>
                            <%}
                                else
                                { %>
                                    <div id="urunresim-<%: i %>" class="tab-pane">
                                      
                                        <img src="<%:Model[i].LargePath %>" alt="<%:Model[i].ProductName %> <%:i + 1 %>" title="<%:Model[i].ProductName %> <%:i + 1 %>"  />
                                      
                                    </div>
                               <% } %>
                    <%} %>                       
                </div>
            </div>

                <%for (int i = 0; i < Model.Count; i++)
                    {%>
                        <a href="#urunresim-<%:i %>" data-toggle="tab" class="decnone col-md-3">
                            
                            <img src="<%:Model[i].SmallPath %>" alt="<%:Model[i].ProductName %> <%:i+1 %> " title="<%: Model[i].ProductName %> <%:i+1 %>" class="img-mt2" style='height:63px !important;' />
                        
                        </a>
                <%} %>
<%} %>
