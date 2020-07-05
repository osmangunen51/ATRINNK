<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master" Inherits="System.Web.Mvc.ViewPage<AdvertViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {


            $('#DropDownSector').change(function () {


                var sectorId = $('#DropDownSector').val();
                if (sectorId != "-1") {
                    $('#DropDownProductGroup').html('');
                    $('[data-rel="categoryList"]').html('');
                    $('[data-rel="treeCategory"]').html('');
                    $('#panelProduct').show();
                    $.ajax({
                        url: '/Account/ilan/CategoryProductGroup',
                        data: { id: sectorId },
                        type: "post",
                        success: function (msg) {
                            $('#DropDownProductGroup' + " > option").remove();
                            $.each(msg, function (i) {

                                $('#DropDownProductGroup').append("<option value=" + msg[i].Value.toString() + ">" + msg[i].Text.toString() + "</option>");
                            });
                        },
                        error: function (e) {
                            alert(e.responseText);
                        }
                    });
                }
                else if (sectorId == "-1") {
                    $("#DropDownSector").html("");
                    $("#DropDownSector").html($("#AllSector").html());
                }
            });


            $('#DropDownProductGroup').change(function () {
                if ($(this).val() == 0)
                    $('#ButtonPost').hide();
                else {
                    TreeCategoryList($(this).val());
                    $('[data-rel="treeCategory"]').html('');
                    //                  $('#ButtonPost').show();
                }
            });

        });

        function TreeCategoryList(categoryId) {
            var categoryList = $('[data-rel="categoryList"]');
            categoryList.find('select').html('');
            $.ajax({
                url: '/Account/ilan/CategoryBind',
                type: 'post',
                data: { id: categoryId },
                success: function (result) {
                    $('[data-rel="categoryList"]').html(result);
                    $('[data-rel="iListCategory"]').change(function () {
                        var selected = $("option:selected", this);
                        CategoryBind(selected.val(), selected.attr('parent'));
                        $('[data-rel="treeCategory"]').attr('id', 'element' + selected.attr('parent'));
                        selected.attr('selected');
                    });
                }
            });
        }

        function CategoryBind(categoryId, parentId) {
            $.ajax({
                url: '/Account/ilan/CategoryBind',
                type: 'post',
                data: { id: categoryId },
                success: function (result) {
                    result += '<div id="element' + categoryId + '"></div>';
                    AddRow(result, parentId);
                    RegisterCount();
                }
            });
        }

        function RegisterCount() {
            $('[data-rel="categoryPanel"]').each(function (i) {
                if (i == 0) {
                    $(this).find('[data-rel="title"]').html('Ana Kategoriyi Seçiniz*');
                }
                else {
                    $(this).find('[data-rel="title"]').html(parseInt(i) + '. Kategoriyi Seçin');
                }
            });
        }

        function AddRow(content, parentId) {
            $('#element' + parentId).html(content);
            $('[data-rel="iListCategory"]').unbind('change');
            $('[data-rel="iListCategory"]').change(function () {
                var selected = $("option:selected", this);
                CategoryBind(selected.val(), selected.attr('parent'));
                selected.attr('selected');
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-sm-12 col-md-12 store-panel-menu-header" style="">
            <%= Html.RenderHtmlPartial("LeftMenu",Model.LeftMenu)%>
        </div>
        <div class="col-md-12">
            <h4 class="mt0 text-info">
                <span class="text-primary glyphicon glyphicon-cog"></span>&nbsp;İlan Ekle
            </h4>
        </div>
    </div>

    <div class="row">
        <div class="col-sm-12 col-md-12">
            <div>

                <div class="well" style="background-color: #fff;">
                    <div>
                        <div class="row">
                            <div style="display: none;">
                            </div>
                            <%using (Html.BeginForm("Advert", "Advert", FormMethod.Post, new { @class = "form-horizontal", role = "form" }))
                                {
                            %>

                            <%= Html.RenderHtmlPartial("Sector", Model)%>

                            <div data-rel="categoryList"></div>
                            <div id="element" data-rel="treeCategory" class="col-md-12"></div>

                            <div class="row">
                                <div class="form-group" id="ButtonPost" style="display: none;">
                                    <div class="col-sm-offset-6 col-sm-9">
                                        <button type="submit" class="btn btn-primary">
                                            Devam
                                   
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <% } %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="emptyContent">
    </div>
</asp:Content>
