<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Management.Models.Stores.StoreSeoNotificationFormModel>" %>
   
<%:Html.HiddenFor(x=>x.StoreMainPartyId) %>
<%:Html.HiddenFor(x=>x.StoreSeoNotificationId) %>
<table>


    <tr>


        <td>
            <div class="editor-label">
                Hatırlatma Tarihi
            </div>
            <div class="editor-field">
                <%:Html.TextBox("RemindDate",DateTime.Now.Date.ToString("dd.MM.yyyy"),new {@autocomplete="off" }) %>
                <br />
                <%: Html.ValidationMessageFor(model => model.RemindDate) %>
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
                <%:Html.DropDownListFor(x=>x.ToUserId,Model.ToUsers) %>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <div class="editor-field">

                Açıklama:
        <%: Html.TextAreaFor(model => model.Text,new { @style = "width:100%; height:200px;"}) %>
                <br />
                <%:Html.ValidationMessageFor(m=>m.Text) %>
            </div>
        </td>
    </tr>
    <%if (Model.StoreSeoNotificationId != 0)
        {%>
    <tr>
        <td colspan="3">
            <div class="editor-field">
                <%:Html.HiddenFor(x=>x.StoreMainPartyId) %>
                Eski Açıklama:
        <%: Html.TextAreaFor(model => model.PreviousText,new { @style = "width:100%; height:200px;"}) %>
                <br />

            </div>
        </td>
    </tr>
    <% } %>
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
