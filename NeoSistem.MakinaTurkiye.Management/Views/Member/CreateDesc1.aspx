<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NeoSistem.MakinaTurkiye.Management.Models.BaseMemberDescriptionModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    CreateDesc
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {

            $('#LastDate').datepicker().val();
        });

    </script>
    <style type="text/css">
        .editor-label { padding-top: 5px; }
    </style>

</asp:Content>

<asp:content id="Content2" contentplaceholderid="MainContent" runat="server">

    <div style="margin-left: 30px; margin-top: 30px; width: 70%;">
        <h2>Üye Açıklama Girişi</h2>

        <% using (Html.BeginForm())
            {%>
        <%: Html.ValidationSummary(true) %>
        <%:Html.HiddenFor(x=>x.RegistrationType) %>
        <%:Html.HiddenFor(x=>x.RegistrationStoreId) %>
        <fieldset>
            <legend>Açıklama</legend>
            <div style="color: #ff0000"><%:ViewData["Error"] %></div>

            <div style="float: left; width:40%;">

                <div class="editor-label">
                    <%: Html.Label("Açıklama :") %>
                </div>
                <div class="editor-field">
                    <%: Html.TextAreaFor(model => model.Description,new { @style = "width:100%; height:200px;"}) %>
                    <br />
                    <%:Html.ValidationMessageFor(m=>m.Description) %>
                </div>
            </div>
            <div style="float: right; width: 57%;">
                <table>
                    <tr>
                        <td colspan="2"></td>
                        <td>           <div class="editor-field">
                    <%:Html.CheckBoxFor(m=>m.IsFirst) %> Önemli 
                </div></td>
                        <td>
                                 
       
                <div class="editor-field">
                    <%:Html.CheckBoxFor(m=>m.IsImmediate) %> Çok Acil 
                </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="editor-label" style="padding-top: 5px;">
                                <%: Html.Label("Başlık :") %>
                            </div>
                            <div class="editor-field">
                                <%string descID = ""; if (ViewData["descID"] != null) { descID = ViewData["descId"].ToString(); } %>
                                <%:Html.Hidden("descId",descID) %>
                                <%:Html.Hidden("mainPartyId",ViewData["mainPartyId"])%>

                                <%:Html.DropDownList("constantId",Model.ConstantModel) %>
                                <br />
                                <%:Html.ValidationMessage("constantId")%>
                            </div>
                        </td>

                        <td>
                            <div class="editor-label">
                                Hatırlatma Tarihi
                            </div>
                            <div class="editor-field">
                                <%:Html.TextBox("LastDate",DateTime.Now.Date.ToString("dd.MM.yyyy"),new {@autocomplete="off" }) %>
                                <br />
                                <%: Html.ValidationMessageFor(model => model.LastDate) %>
                            </div>
                        </td>

                        <td>

                            <div class="editor-field">
                                Hatırlatma Saati
                            </div>
                            <div class="editor-field">
                                <select name="time" style="width: 150px;">
                                    <option value="">Seçiniz</option>
                                    <%for (int i = 9; i <= 19; i++)
                                        {
                                            for (int a = 0; a <= 3; a++)
                                            {
                                                if (a == 0)
                                                {
                                                    Response.Write("<option value='" + i + ":00'>" + i + ":00</option>");
                                                }
                                                else if (a == 1)
                                                {
                                                    Response.Write("<option value='" + i + ":15'>" + i + ":15</option>");

                                                }
                                                else if (a == 2)
                                                {
                                                    Response.Write("<option value='" + i + ":30'>" + i + ":30</option>");
                                                }
                                                else
                                                {
                                                    Response.Write("<option value='" + i + ":45'>" + i + ":45</option>");
                                                }

                                            }
                                        }%>
                                </select>
                            </div>
                        </td>

                        <td>
                            <div class="editor-label">
                                Atama Yap
                            </div>
                            <div class="editor-field">
                                <%:Html.DropDownList("userId",Model.Users) %>
                            </div>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="3"></td>
                        <td>
                <% using (Html.BeginForm())
                    { %>
                <p>
                    <button type="submit" style="width: 70px; height: 35px;">
                        Kaydet
                    </button>

                    <button type="button" style="width: 70px; height: 35px;" onclick="window.location='/Member/BrowseDesc/<%:ViewData["mid"] %>'">
                        İptal
                    </button>
                </p>
                <% } %>
            </div>
</td>
                    </tr>
                </table>
        </fieldset>

        <% } %>
    </div>

</asp:content>



