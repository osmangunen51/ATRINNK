<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<PictureModel>>" %>
<% if (Model.Count > 0)
   { %>
<% for (int i = 0; i < Model.Count; i++)
   { %>
<% if (i == 0)
   { %>
<div class="col-xs-12 col-md-6">
    <div class="picture-list-item picture-list-item__big">
        <img id="Img2" src="<%=string.Format("{0}{1}/{2}", AppSettings.ProductImageFolder, Model[i].ProductId.ToString(), Model[i].PicturePath)%>" alt=".." />
        <div class="picture-list-item__button">
            <a data-rel="deleteImage" onclick="DeleteImage('<%:Model[i].ProductId%>', '<%:Model[i].ProductId%>', '<%:Model[i].PicturePath%>')" data-index="<%: i %>" class="picture-list-item__delete"><i class="glyphicon glyphicon-remove-circle"></i></a>
        </div>
    </div>
</div>
<div class="col-xs-12 col-md-6">
    <div class="row no-gutters clearfix">
        <% }
               else
               { %>
                    <div class="col-xs-4">
                        <div class="picture-list-item picture-list-item__small">
                            <img id="<%= Model[i].PicturePath %>" src="<%=string.Format("{0}{1}/{2}", AppSettings.ProductImageFolder, Model[i].ProductId.ToString(), Model[i].PicturePath)%>"
                                alt="..">
                            <div class="picture-list-item__button">
                                <a  onclick="DeleteImage('<%:Model[i].ProductId%>', '<%:Model[i].ProductId%>', '<%:Model[i].PicturePath%>')" data-index="<%: i %>" class="picture-list-item__delete"><i class="glyphicon glyphicon-remove-circle"></i></a>
                                <a  onclick="mainImage('<%:Model[i].ProductId%>', '<%:Model[i].PicturePath%>')" data-index="<%: i %>" style="cursor: pointer" class="picture-list-item__main-image">Ana Resim Seç</a>
                            </div>
                        </div>
                    </div>

               <% } %>

        <% } %>

               <% if (Model.Count > 0 && Model.Count < 10)
               { %>
                    <%for (int a = Model.Count; a <= 9; a++)
                      {%>
                        <div class="col-xs-4">
                             <div class="picture-list-item picture-list-item__small">
                                <img src="https://dummyimage.com/100x100/efefef/000000.jpg&text=resim<%:a+1 %>"alt="..">
                             </div>
                        </div>
                    <%} %>
            <% } %>

        <%: MvcHtmlString.Create("</div></div>") %>

        <% }
   else
   { %>
        <% for (int i = 1; i <= 10; i++)
           { %>
        <% if (i == 1)
           { %>
        <div class="col-xs-12 col-md-6">
            <div class="picture-list-item picture-list-item__big">
                <img src="https://dummyimage.com/200x161/efefef/000000.jpg&text=resim<%:i %>" alt=".." />
            </div>
        </div>
        <div class="col-xs-12 col-md-6">
            <div class="row no-gutters clearfix">
                <% }
           else
           { %>
                <div class="col-xs-4">
                    <div class="picture-list-item picture-list-item__small">
                        <img src="https://dummyimage.com/100x100/efefef/000000.jpg&text=resim<%:i %>" alt="..">
                    </div>
                </div>
                <% } %>

                <% } %>
                <%: MvcHtmlString.Create("</div></div>") %>
                <% } %>
