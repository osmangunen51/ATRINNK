﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SeoDefinitionModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   Seo tanımlama
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/CKEditor/ckeditor.js"></script>
    <script type="text/javascript" src="/Scripts/CKFinder/ckfinder.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <% using (Html.BeginForm("Edit", "SeoDefinition", FormMethod.Post))
     { %>
         <%=Html.HiddenFor(sd => sd.SeoDefinitionId, new { id = "hdnSeoDefinitionId" })%>
         <%=Html.HiddenFor(sd => sd.EntityId, new { id = "hdnEntityId" })%>
         <%=Html.HiddenFor(sd => sd.EntityTypeId, new { id = "hdnEntityTypeId" })%>
         <%=Html.HiddenFor(sd => sd.Enabled, new { id = "hdnEnabled" })%>
         <div style="margin-top: 10px;">
            <table border="0" cellpadding="5" cellspacing="0" style="font-size: 13px; margin: 10px; width: 100%;">
             <tr>
                    <td>
                        <%: Html.LabelFor(sd => sd.CategoryName) %>
                    </td>
                    <td style="width: 1px">
                        :
                    </td>
                    <td>
                        <b><%:Model.CategoryName%></b>
                    </td>
                </tr>

                        <tr>
                    <td>
                        <%: Html.LabelFor(sd => sd.SeoContent) %>
                    </td>
                    <td style="width: 1px">
                        :
                    </td>
                    <td>
                        <%: Html.TextAreaFor(sd => sd.SeoContent)%>
                    </td>
                    <td>
                        <% Html.ValidateFor(sd => sd.SeoContent); %>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="right">
                        <div style="border-bottom: dashed 1px #c0c0c0; width: 100%; height: 1px; margin-bottom: 10px">
                        </div>
                        <button type="submit" style="height: 27px">
                            Kaydet
                        </button>
                        <button type="button" style="height: 27px" onclick="window.location='/Category/AllIndex'">
                            İptal
                        </button>
                    </td>
                </tr>
                <tr>
                    <td>

                    </td>
                    <td colspan="2">
                        <table width="100%">
                            <caption>
                                 <center><h3>Anahtar Kelime Analizi</h3></center>
                                
                             </caption>
                            <thead>
                                <tr>
                                     <%foreach (var Phare in Model.KeywordAnalysis.Phares.OrderBy(x=>x.PhareKey))%>
                                       <%{%>
                                       <%
                                           int Wd = 100 / Model.KeywordAnalysis.Phares.Count;
                                       %>
                                       <td <%string.Format("style=\"width:{0}%\"", Wd);%>>

                                        </td>
                                      <%}%>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <%foreach (var Phare in Model.KeywordAnalysis.Phares.Where(x=>x.PhareKey<3).OrderBy(x=>x.PhareKey))%>
                                        <%{%>
                                         <td valign="top">
                                        <table border="0" cellpadding="5" cellspacing="0" style="font-size: 13px; margin: 10px; width: 95%;">
                                         <caption>
                                             <center><h3><%=Phare.Name%></h3></center>
                                              <center><h3>Kelime Sayısı =<%=Phare.Count%></h3></center>
                                         </caption>
                                        <thead>
                                            <tr>
                                                <td style="width:30px">

                                                </td>
                                                <td style="width: 70%;">
                                                    Kelime
                                                </td>
                                                <td style="width: 10%;">
                                                    Adet
                                                </td>
                                                <td style="width: 10%;">
                                                    Yüzde % 
                                                </td>
                                            </tr>
                                        </thead>
                                         <tbody>
                                                <%int row = 0; %>
                                                <%foreach (var item in Phare.Liste.OrderByDescending(x=>x.Rate))
                                                {%>
                                                <tr id="row<%: item.Word %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
                                                      <td class="CellBegin" style="width:30px">
                                            
                                                      </td>
                                                      <td class="CellBegin">
                                                        <%: item.Word %>
                                                      </td>
                                                    <td class="Cell">
                                                        <%: item.Count %>
                                                      </td>
                                                      <td class="Cell">
                                                        <%:item.Rate.ToString("0.##")%>
                                                     </td>
                                                </tr>
                                                <%}%>
                                         </tbody>
                                    </table>
                                    </td>
                                        <%}%>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
         </div>
   <%} %>
       <script type="text/javascript" defer="defer">
           var editor = CKEDITOR.replace('SeoContent', { toolbar: 'webtool' });
           CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
           var editor = CKEDITOR.replace('ContentSide', { toolbar: 'webtool' });
           CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
           var editor = CKEDITOR.replace('ContentBottomCenter', { toolbar: 'webtool' });
           CKFinder.SetupCKEditor(editor, '/Scripts/CKFinder/');
  </script>
</asp:Content>
