﻿<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<UpdateDateDescriptionModel>" %>

<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui.datepicker-tr.js" type="text/javascript"></script>
<link href="/Content/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>


<script type="text/javascript">

    $(document).ready(function () {

        $('#LastDate').datepicker().val();

            CKEDITOR.replace('Content', {
        toolbar: [
            { name: 'document', items: ['Source', '-', 'NewPage', 'Preview', '-', 'Templates'] },	// Defines toolbar group with name (used to create voice label) and items in 3 subgroups.
            ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'],			// Defines toolbar group without name.
            '/',																					// Line break - next group will be placed in new line.
            { name: 'basicstyles', items: ['Bold', 'Italic'] }
        ]
    });

    });


    
</script>


<div style="float: left; margin: 20px 0px 0px 20px">
    <%if (ViewData["basarili"] != null)
        {%>
    <p style="color: #047217"><%:ViewData["basarili"] %></p>
    <% } %>
    <%using (Html.BeginForm())
        { %>
    <%:Html.HiddenFor(x=>x.ID) %>

    <div style="float: left;">

        <div style="float: left">
            <div class="editor-label">
                Hatırlatma Tarihi
            </div>
            <div class="editor-field">
                <%:Html.TextBox("LastDate",DateTime.Now.Date.ToString("dd.MM.yyyy"),new {@autocomplete="off" }) %><br />

                <br />
                <%: Html.ValidationMessageFor(model => model.LastDate) %>
            </div>
        </div>
        <div style="float: left; margin-left: 20px;">
            <div class="editor-field">
                Hatırlatma Saati
            </div>
            <div class="editor-field">
                <select name="Hour" style="width: 150px;">
                    <option value="">Seçiniz</option>
                    <%for (int i = 9; i <= 19; i++)
                        {
                            for (int a = 0; a < 2; a++)
                            {
                                if (a == 0)
                                {
                                    Response.Write("<option value='" + i + ":00'>" + i + ":00</option>");
                                }
                                else
                                {
                                    Response.Write("<option value='" + i + ":30'>" + i + ":30</option>");

                                }

                            }
                        }%>
                </select>
            </div>
        </div>
        <div style="clear: both"></div>
        <div style="background-color: #f9f9f9; min-height: 40px;">
            <%if (Model.Content != null)
                {%>
            <%if (Model.Content.Length > 300) { Model.Content = Model.Content.Substring(0, 300) + ".."; } %>
            <div style="width:100%; min-height:150px;">
            <%:Html.Raw(Model.Content) %>
                </div>

            <%} %>
        </div>
        <div style="float: right; margin-top: 10px;">
            <button type="submit" style="width: 70px; height: 35px;" id="saveDate">
                Kaydet
            </button>
        </div>

        <%}%>
    </div>
