<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores.MTStoreActivityModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="margin-top: -20px; height: 60px;">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">Firma Faaliyet Alanları
            </h4>
        </div>
    </div>
    <div class="row">
        <div class="well well-mt4 col-xs-12" style="background: #fff;">
            <%if (TempData["ErrorMessage"] != null)
                { %>
            <div class="alert mt-alert-danger" style="background-color: #f1f1f1;" role="alert">
                <i class="fa fa-exclamation-circle" aria-hidden="true"></i><%:TempData["ErrorMessage"].ToString() %>
            </div>
            <%}%>
            <%if (TempData["SuccessMessage"] != null)
                { %>
            <div class="alert alert-success" role="alert">
                <%:TempData["SuccessMessage"].ToString() %>
            </div>
            <%}%>

            <%using (Html.BeginForm("index", "StoreActivity", FormMethod.Post, new { id = "store-activity-form" }))
                {%>


            <div class="form-top">
                <div class="pull-left">
                    <div class="form-header-text">Firma Faaliyet Alanları</div>
                </div>
            </div>
            <div class="form-horizontal" style="margin-top: 20px;">
                <div class="row">
                    <div class="col-md-9">
                        <div class="form-group">
                            <label class="col-md-3">Sektör</label>
                            <div class="col-md-9">
                                <% 
                                    foreach (var item in Model.Categories)
                                    {%>
                                <div class="col-md-4 col-sm-12">
                                    <input type="checkbox" name="subCategory" value="<%:item.Value %>" <%:item.Selected==true ? "checked":"" %> /><label style="font-weight: 500"><%:item.Text %></label>
                                </div>
                                <%} %>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
            <div class="col-md-9">

                <div class="pull-right">
                    <input type="submit" value="Kaydet" class="btn btn-success" />
                </div>
            </div>
            <% } %>
            <div class="col-md-9" style="margin-top: 20px">
                <%if (Model.StoreActivityCategories.Count > 0)
                    {%>
                <table class="table">
                    <%foreach (var item in Model.StoreActivityCategories)
                        {%>
                    <tr id="storeActivity<%:item.Key %>">
                        <td style="font-size: 14px;">
                            <%:item.Value %></td>
                        <td><a style="cursor: pointer" onclick="DeleteStoreActivityCategory(<%:item.Key %>)" title="Kaldır"><i class="fa fa-trash"></i></a></td>
                    </tr>

                    <%} %>
                </table>
                <% }
                    else
                    {%>
                <div class="alert alert-warning" role="alert">
                    Tanımlanmış firma faaliyet alanınız bulunmamaktadır.
                </div>
                <% }%>
            </div>
        </div>
    </div>

</asp:Content>
