<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Account.Master"
    Inherits="System.Web.Mvc.ViewPage<AdvertViewModel>" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {
            //      $('.iList').hide();
            //      $('.dropDown').show();
            //      $('#panelProduct').show();

            //      $('#DropDownSector').val(<%: Model.CategorizationSessionModel.SectorId %>);
        //      $('#DropDownProductGroup').val(<%: Model.CategorizationSessionModel.ProductGroupId %>);

        $('[data-rel="iListCategory"] option').each(function () {
            $(this).unbind('click');

            $(this).click(function () {
                CategoryBind($(this).attr('value'), $(this).attr('parent'));

                $('[data-rel="iListCategory"] option[parent=' + $(this).attr('parent') + ']').removeClass('active');

                $(this).addClass('active');
            });
        });
    });

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
                $(this).find('[data-rel="title"]').html('Ana Kategoriyi Seçin');
            }
            else {
                $(this).find('[data-rel="title"]').html(parseInt(i) + '. Kategoriyi Seçin');
            }
        });
    }

    function AddRow(content, parentId) {
        $('#element' + parentId).html(content);

        $('[data-rel="iListCategory"] option').each(function () {
            $(this).unbind('click');
            $(this).click(function () {
                CategoryBind($(this).attr('value'), $(this).attr('parent'));

                $('[data-rel="iListCategory"] option[parent=' + $(this).attr('parent') + ']').removeClass('active');
                $(this).addClass('active');

            });
        });
    }

    function DeletePicture(id) {
        $.ajax({
            url: '/Advert/DeletePicture',
            type: 'delete',
            data: { index: id, horizontal: true },
            success: function (data) {
                $('#divPictureList').html(data);
            },
            error: function (x, l, e) {
            }
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
           İlan Ekle
        </h4>
    </div>
       </div>

    <div class="row">

        <div class="col-sm-12 col-md-12">
            <div>
                <div class="well store-panel-container">
                    <div>
                        <div>
                            <%using (Html.BeginForm("Category", "Advert", FormMethod.Post, new { @class = "form", role = "form" }))
                              { %>
                            <div class="form-group">
                                <div class="row">
                                    <%= Html.RenderHtmlPartial("CategoryParent", Model.CategoryList.OrderBy(c => c.CategoryName).OrderBy(c => c.CategoryOrder))%>
                                    <div id="element" data-rel="treeElement"></div>
                                </div>


                            </div>
                            <% } %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>





    <input type="hidden" id="ParentCount" value="0" />

</asp:Content>
