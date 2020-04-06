<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<PictureModel>>" %>
<style type="text/css">#gallery{ width:100%; float:left; margin-top:100px;}

#gallery ul{ margin: 0;
padding: 0;
list-style-type: none;
text-align: center;}

#gallery ul li{ padding:.2em 1em; border:2px solid #ccc; float:left; margin:5px 3.5px; background:none; width:auto; height:auto;}

    .reorder_ul, .reorder-photos-list,.reorder-photos-list li,ul.reorder-photos-list li {
     list-style-type:none !important; float:left !important; }

 </style>

<script type="text/javascript">
    $(document).ready(function () {
        $("ul.reorder-photos-list").sortable({ tolerance: 'pointer' });
        $('.reorder_link').html('Sıralamayı kaydet');
        $('.reorder_link').attr("id", "save_reorder");
        $('#reorder-helper').slideDown('slow');
        $('.image_link').attr("href", "javascript:void(0);");
        $('.image_link').css("cursor", "move");
        $("#save_reorder").click(function (e) {
            if (!$("#save_reorder i").length) {
                $(this).html('').prepend('<img src="/Content/Images/ajax-loader.gif"/>');
                $("ul.reorder-photos-list").sortable('destroy');
                $("#reorder-helper").html("Fotoğraf sıraları kaydediliyor...Lütfen bekleyiniz").removeClass('light_box').addClass('notice notice_error');

                var h = [];
                $("ul.reorder-photos-list li").each(function () { h.push($(this).attr('id').substr(9)); });
                $.ajax({
                    type: "POST",
                    url: "/Product/UpdatePictureOrder",
                    data: { idArray: " " + h + "" },
                    success: function (html) {
                        console.log(h);
                        window.location.reload();
                    }

                });
                return false;
            }
            e.preventDefault();
        });
        

    });
 
    </script>
   
        

   <% if (Model.Count > 0)
   {
       
       %>
           
             <a href="javascript:void(0);" class="btn outlined mleft_no reorder_link" id="save_reorder">Sıralamayı Kaydet</a>
          <div id="reorder-helper" class="light_box" style="display:none;">1. Önce fotoğrafları sıralayın.<br>2. Sıraladıktan sonra  sıralamyı kaydet linkine tıklayın.</div>
 
  <div id="gallery">
      <ul class="reorder_ul reorder-photos-list"> 
             <%
  for (int i = 0; i < Model.Count; i++)
   {
     %>
        <li id="image_li_<%=Model[i].PictureId %>"  class="ui-sortable-handle">
   <div style="width: 100px; height: 100px; margin-left: 10px; margin-top: 10px; float: left;  cursor:move; 
    text-align: center;">

                <img class="img-thumbnail" id="Img2" src="<%=string.Format("{0}{1}/{2}", AppSettings.ProductImageFolder, Model[i].ProductId.ToString(), Model[i].PicturePath)%>"
  alt=".." />
<a style="cursor: pointer; text-decoration: none;" onclick="DeletePictureFromList('<%=Model[i].ProductId %>','<%=Model[i].PictureId %>','<%=Model[i].PictureName %>');">
    Resmi Sil</a>
         </div>
             </li>
<% 

}
  
%> 
   
  </ul>
    </div> <%
}
   else {
           %>
<div style="float: left; margin-top: 20px; margin-left: 20px;">
  Herhangi bir ürün resmi bulunamadı.
</div>
<%
           
       }  
        
%> 
          