<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ICollection<ConstantModel>>" %>
<% int row = 0; %>
<% foreach (var item in Model)
   { %>
<% row++; %>
<tr id="row<%: item.ConstantId %>" class="<%: (row % 2 == 0 ? "Row" : "RowAlternate") %>">
  <td class="CellBegin">
    <%: item.ConstantName %>
  </td>
     <td class="Cell">
    <%: item.Order %>
  </td>
  <%if (item.ConstantType == 13 || item.ConstantType == 14 || item.ConstantType == 15 || item.ConstantType == 18 || item.ConstantType == 19 || item.ConstantType == 35 || item.ConstantType == 20 || item.ConstantType == 22 || item.ConstantType == 23  || item.ConstantType==29   || item.ConstantType==32  || item.ConstantType==33) 
    { %>
        <td class="Cell">
        <%:Html.Raw(item.ConstantTitle) %>
  </td>
    <td class="Cell">
  <a style="padding-bottom: 5px; cursor: pointer; float:left; margin-right:7px;" href="/Constant/EditConstants/<%=item.ConstantId %>" id="lightbox_click" rel="superbox[iframe]">içerik</a>
  </td>
   <%} %>
    
  <td class="CellEnd" style="width:100px;">
                  <a href="/Constant/SubContents/<%=item.ConstantId  %>" title="Alt Açıklamalar" style="float: left;" id="lightbox_click" rel="superbox[iframe]" title="İletişim Bilgileri">
         <img src="/Content/RibbonImages/BulletListDefault.png" alt="Alt Açıklamalar" style="height:16px;">
                  </a>
      
          <a style="padding-bottom: 5px; cursor: pointer" title="Alt Başlık Ekle" onclick="SubConstantInsert(<%: item.ConstantId %>);">
      <div style="float: left; margin-right: 10px">

      </div>
    </a>
    <a style="padding-bottom: 5px; cursor: pointer" onclick="EditConstant(<%: item.ConstantId %>);">
      <div style="float: left; margin-right: 10px">
        <img src="/Content/images/edit.png" />
      </div>
    </a><a style="cursor: pointer;" onclick="DeletePost(<%: item.ConstantId %>);">
      <div style="float: left;">
        <img src="/Content/images/delete.png" />
      </div>
    </a>
  </td>
</tr>
<% } %>
<tr>
  <td class="ui-state ui-state-default" colspan="7" align="right" style="border-color: #DDD;
    border-top: none; border-bottom: none;">
    <div style="float: right;" class="pagination">
      <ul>
      </ul>
    </div>
  </td>
</tr>


<link rel="stylesheet" href="/Scripts/SuperBox/jquery.superbox.css" type="text/css" media="all" />
<script type="text/javascript" src="/Scripts/SuperBox/jquery.superbox-1.js"></script>
<script type="text/javascript">
    $(function () {
        $.superbox.settings = {
            closeTxt: "Kapat",
            loadTxt: "Yükleniyor...",
            nextTxt: "Sonraki",
            prevTxt: "Önceki"
        };
        $.superbox();
    });
</script>
<style type="text/css">
    /* Custom Theme */
    #superbox-overlay {
        background: #e0e4cc;
    }

    #superbox-container .loading {
        width: 32px;
        height: 32px;
        margin: 0 auto;
        text-indent: -9999px;
        background: url(styles/loader.gif) no-repeat 0 0;
    }

    #superbox .close a {
        float: right;
        padding: 0 5px;
        line-height: 20px;
        background: #333;
        cursor: pointer;
    }

        #superbox .close a span {
            color: #fff;
        }

    #superbox .nextprev a {
        float: left;
        margin-right: 5px;
        padding: 0 5px;
        line-height: 20px;
        background: #333;
        cursor: pointer;
        color: #fff;
    }

    #superbox .nextprev .disabled {
        background: #ccc;
        cursor: default;
    }
</style>