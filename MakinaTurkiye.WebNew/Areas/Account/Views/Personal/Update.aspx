<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<MemberModel>" %>


<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
        
               <span class="text-primary glyphicon glyphicon-cog"></span>&nbsp;Kişisel bilgilerimi güncelle
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>
          
                <div class="col-xs-12 well store-panel-container">
                    <%using (Html.BeginForm("Update", "Personal", FormMethod.Post, new { role = "form", @class = "form-horizontal" }))
                      {%>
                    <div class="form-group ">
                        <label class="col-sm-3 control-label">
                            E-Posta Adresiniz
                        </label>
                        <div class="col-sm-8 col-md-7">
                            <%:Html.TextBoxFor(model => model.MemberEmail, new { type = "email", @class = "form-control popovers", disabled = "disabled" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Adınız
                        </label>
                        <div class="col-sm-4">
                            <%:Html.TextBoxFor(model => model.MemberName, new { @class = "form-control" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Soyadınız
                        </label>
                        <div class="col-sm-4">
                            <%:Html.TextBoxFor(model => model.MemberSurname, new { @class = "form-control" })%>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Doğum Tarihiniz
                        </label>
                        <div class="col-sm-3 col-md-2">
                            <select name="Day" class="form-control">
                                <option value="0" selected="selected">Gün</option>
                                <%for (int i = 1; i <= 31; i++)
                                  { %>
                                <% if (i == Model.Day)
                                   { %>
                                <option selected="selected" value="<%=i %>">
                                    <%=i%></option>
                                <% }
                                   else
                                   { %>
                                <option value="<%=i %>">
                                    <%=i%></option>
                                <% } %>
                                <%} %>
                            </select>
                        </div>
                        <div class="col-sm-3 col-md-2">
                            <select name="Month" class="form-control">
                                <option value="0" selected="selected">Ay</option>
                                <% foreach (KeyValuePair<string, int> item in EnumModel.GetMonth())
                                   { %>
                                <% if (item.Value == Model.Month)
                                   { %>
                                <option selected="selected" value="<%=item.Value %>">
                                    <%=item.Key %></option>
                                <% }
                                   else
                                   { %>
                                <option value="<%=item.Value %>">
                                    <%=item.Key %></option>
                                <% } %>
                                <% } %>
                            </select>
                        </div>
                        <div class="col-sm-3 col-md-2">
                            <select name="Year" class="form-control">
                                <option value="0" selected="selected">Yıl</option>
                                <%for (int y = 1945; y <= 2002; y++)
                                  {%>
                                <% if (y == Model.Year)
                                   { %>
                                <option selected="selected" value="<%=y.ToString() %>">
                                    <%=y.ToString() %></option>
                                <% }
                                   else
                                   { %>
                                <option value="<%=y.ToString() %>">
                                    <%=y.ToString() %></option>
                                <% } %>
                                <%} %>
                            </select>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Cinsiyetiniz
                        </label>
                        <div class="col-sm-9">
                            <div class="radio-inline">
                                <label>
                                    <%=Html.RadioButton("Gender", 1, Model.Gender == false, new { style = "height: 11px" })%>
                                    Bay
                                </label>
                            </div>
                            <div class="radio-inline">
                                <label>
                                    <%=Html.RadioButton("Gender", 2, Model.Gender, new { style = "height: 11px" })%>
                                    Bayan
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-9">
                            <button type="submit" class="btn btn-primary">
                                Değişiklikleri Kaydet
                            </button>
                        </div>
                    </div>
                    <% } %>
                </div>
  <%--              <div class="panel panel-mt col-xs-12 p0">
                    <div class="panel-heading">
                        <span class="glyphicon glyphicon-question-sign"></span>Sayfa Yardımı
                    </div>
                    <div class="panel-body">
                        <b>Bilgilerim </b>
                        <br>
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="#">Üyelik bilgilerimi nasıl değiştiririm?
                        </a>
                        <br>
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="#">Fatura bilgilerimi nasıl sisteme
                            tanımlarım? </a>
                        <br>
                        <i class="fa fa-angle-right"></i>&nbsp;&nbsp; <a href="#">Makina Türkiye duyurularını
                            almak istemiyorum. </a>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
