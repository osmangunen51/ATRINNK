﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/StoreProfile.Master" Inherits="NeoSistem.MakinaTurkiye.Core.Web.ViewPage<NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles.MTStoreProfileBranchModel>" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeaderContent" runat="server">
    <%=Html.RenderHtmlPartial("_HeaderContent") %>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="StoreProfileHeaderContent" runat="server">
    <%if (Model.StoreActiveType == 2)
        {  %>
    <%} %>
    <%=Html.RenderHtmlPartial("_HeaderTop",Model.MTStoreProfileHeaderModel) %>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="StoprofileMenu" runat="server">
    <%if (Model.StoreActiveType == 2)
        {  %>
    <%=Html.RenderHtmlPartial("_LeftMenu", Model.MTStoreProfileHeaderModel)%>

    <%} %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="StoreProfileContent" runat="server">
    <%if (Model.StoreActiveType == 2)
        { %>
    <%:Html.Hidden("hdnStoreMainPartyId",Model.MainPartyId) %>

    <div class="col-sm-7 col-md-8 col-lg-9">
        <div class="StoreProfileContent">

            <% foreach (var item in Model.BranchAddresses)
                { %>
            <div class="col-xs-12">
                <div class="col-xs-4 col-md-2">
                    <div class="thumbnail thumbnail-mt">
                        <div class="caption text-left">
                            <b><%:Model.StoreBranchs.FirstOrDefault(c=> c.StoreDealerId == item.StoreDealerId).DealerName %> </b>
                            <br>
                            <p>
                                <%= Model.AdressEdits.FirstOrDefault(x=>x.AddressId==item.AddressId).Address %>
                                <br />
                                <% foreach (var itemPhones in Model.Phones.Where(c => c.AddressId == item.AddressId))
                                    { %>
                                <% var pType = (PhoneType)itemPhones.PhoneType;
                                    string phone = "";
                                    switch (pType)
                                    {
                                        case PhoneType.Phone:
                                            phone = "Telefon:";
                                            break;
                                        case PhoneType.Fax:
                                            phone = "Fax:";
                                            break;
                                        case PhoneType.Gsm:
                                            phone = "Gsm:";
                                            break;
                                        default:
                                            break;
                                    }
                                %>
                                <%:phone%>
                                <%:itemPhones.PhoneCulture + " " + itemPhones.PhoneAreaCode + " " + itemPhones.PhoneNumber%>
                                <%} %>
                            </p>
                        </div>
                    </div>
                </div>
            </div>
            <% }%>
            <div class="clearfix"></div>
        </div>
        <!-- /.col-md-5 main content -->
    </div>


    <%}
    else
    { %>
    <%=Html.Action("NoAccessStore",new{id=Model.MainPartyId}) %>
    <%} %>
</asp:Content>

