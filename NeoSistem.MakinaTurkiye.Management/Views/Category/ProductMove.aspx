<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ProductMove
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
  <style type="text/css">
    .sectorPanel
    {
      width: auto;
      height: 170px;
      float: left;
      border: solid 2px #a9dce2;
      margin-top: 10px;
      margin-left: 20px;
      max-height: 200px;
      overflow-y: auto;
      overflow-x: hidden;
    }
    .sectorPanel ul
    {
      list-style-type: none;
      padding: 0px;
      padding-top: 5px;
      padding-bottom: 5px;
      font-size: 12px;
      margin: 0px;
      color: #4c4c4c;
      width: 300px;
      height:170px;
    }
    .sectorPanel ul .active
    {
      background-color: #c4eafa;
      cursor: pointer;
    }
    .sectorPanel ul li
    {
      height: 18px;
      padding-left: 15px;
      padding-right: 15px;
      padding-top: 2px;
      width: 270px;
    }
    .sectorPanel ul li:hover
    {
      background-color: #c4eafa;
      cursor: pointer;
    }
    .sectorPanel a
    {
      text-decoration: none;
      color: #000;
    }
    .categoryPanel
    {
      width: 600px;
      height: auto;
      float: left;
    }
    .categoryPanelTitle
    {
      width: 120px;
      height: 10px;
      float: left;
      font-size: 13px;
      margin-top: 10px;
      color: #4c4c4c;
      color: #4c4c4c;
    }
    .picPanel
    {
      width: 65px;
      height: 75px;
      text-align: center;
      float: left;
      margin-left: 10px;
      margin-top: 5px;
    }
    .picPanel img
    {
      width: 65px;
      height: 65px;
    }
    .picPanelImage
    {
      width: 65px;
      height: 65px;
      background-image: #fff;
      border-left: solid 1px #afc1dc;
      border-right: solid 1px #afc1dc;
      border-top: solid 2px #d1dbeb;
      border-bottom: solid 2px #d1dbeb;
    }
    .fileUp
    {
      border: solid 1px #99D6DD;
      border-top: solid 2px #99D6DD;
      width: 280px;
      margin-top: 10px;
    }
    .postButton
    {
      width: 120px;
      height: 25px;
      text-align: center;
      background-color: #75A8CA;
      border: 3px double #366F81;
      font-family: Segoe UI, Arial;
      font-size: 15px;
      font-weight: bold;
      color: #FFFFFF;
    }
    
    .buttonDiv
    {
      width: 250px;
      height: 60px;
      margin-top: 10px;
      text-align: right;
      float: left;
      margin-left: 20px;
      margin-top:30px;
      margin-bottom:20px;
    }
  </style>
    <script type="text/javascript">

    $(document).ready(function () {
      $('.iList').hide();
      $('.dropDown').show();
      $('#panelProduct').show();


        
     $('.iListCategory li').each(function () {
        $(this).unbind('click');
           
        $(this).click(function () {
          CategoryBind($(this).attr('value'), $(this).attr('parent'));
          
          $('.iListCategory li[parent='+ $(this).attr('parent') +']').removeClass('active');
          $(this).addClass('active');
        });
      });
      $('.iListCategory1 li').each(function () {
        $(this).unbind('click');

        $(this).click(function () {
          CategoryBind1($(this).attr('value'), $(this).attr('parent'));

          $('.iListCategory1 li[parent=' + $(this).attr('parent') + ']').removeClass('active');
          $(this).addClass('active');
        });
      }); 

    });
     
    function CategoryBind(categoryId, parentId) {
      $.ajax({
        url: '/Category/CategoryBind',
        type: 'post',
        data: {id : categoryId},
        success: function (result) {  
          result += '<div id="element' + categoryId  +'"></div>';
          AddRow(result, parentId);
          RegisterCount();
        }
      });
    }
    function CategoryBind1(categoryId, parentId) {
      $.ajax({
        url: '/Category/CategoryBind1',
        type: 'post',
        data: { id: categoryId },
        success: function (result) {
          result += '<div id="elementa' + categoryId + '"></div>';
          AddRowa(result, parentId);
          RegisterCount();
        }
      });
    }
    
    function RegisterCount() {
      $('.pnl').each(function(i) {
      if (i == 0) {
          $(this).find('.cNumber').html('Ana Kategoriyi Seçin');
        }
        else {
          $(this).find('.cNumber').html(parseInt( i ) + '. Kategoriyi Seçin');
        }
      });
      $('.pnla').each(function (i) {
        if (i == 0) {
          $(this).find('.cNumber1').html('Ana Kategoriyi Seçin');
        }
        else {
          $(this).find('.cNumber1').html(parseInt(i) + '. Kategoriyi Seçin');
        }
      });
    }
    
    function AddRow(content, parentId)  {    
      $('#element' + parentId).html(content);
       
      $('.iListCategory li').each(function () {
        $(this).unbind('click');
        $(this).click(function () {
          CategoryBind($(this).attr('value'), $(this).attr('parent'));

          $('.iListCategory li[parent='+ $(this).attr('parent') +']').removeClass('active');
          $(this).addClass('active');
          
        }); 
      }); 
    }
    function AddRowa(content, parentId) {
      $('#elementa' + parentId).html(content);

      $('.iListCategory1 li').each(function () {
        $(this).unbind('click');
        $(this).click(function () {
          CategoryBind1($(this).attr('value'), $(this).attr('parent'));

          $('.iListCategory1 li[parent=' + $(this).attr('parent') + ']').removeClass('active');
          $(this).addClass('active');

        });
      });
    }
  </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%MakinaTurkiyeEntities entities=new MakinaTurkiyeEntities(); %>
     <%using (Html.BeginForm("ProductMove", "Category", FormMethod.Post))
    { %>
    <%string s = "0"; %>
    <%if (ViewData["selecteddone"] == null)
      {  %>

  <div class="emptyContent" style="padding-bottom: 15px;float: left;width: 800px;height: auto;">
     <div style="margin-top:20px; margin-left:20px;">
         <div style="font-size:15px; font-weight:700; margin-bottom:5px;">İsime Göre Ürün Taşıma</div>
               Taşınan Kategori Adı:<input type="text" name="fromCategoryName" />
              Taşınacak Kategori Adı:<input type="text" name="toCategoryName" />
           <input type="submit" value="Taşı" />

     </div>
    <div style="background-color: #99d6dd; width: 770px; float: left; margin-top: 10px;
      margin-bottom: 10px; height: 1px;">
    </div>


    <%= Html.RenderHtmlPartial("CategoryParentView", entities.Categories.Where(c => c.CategoryParentId == null).ToList())%>
    <div id="element<%:s %>">
    </div>
  </div>
    <div class="emptyContent" style="padding-bottom: 15px;float: left;width: 800px;height: auto;">
    <div style="background-color: #99d6dd; width: 770px; float: left; margin-top: 10px;
      margin-bottom: 10px; height: 1px;">
    </div>

     

    <%= Html.RenderHtmlPartial("CategoryParentViewSecond", entities.Categories.Where(c => c.CategoryParentId == null).ToList())%>
        
    <div id="elementa<%:s %>">
    </div>
  </div>
  <input type="hidden" id="ParentCount" value="0" />
  <%}
      else
      {  %>
      <%:ViewData["selecteddone"].ToString() %>
      <%} %>
  <% } %>

</asp:Content>

