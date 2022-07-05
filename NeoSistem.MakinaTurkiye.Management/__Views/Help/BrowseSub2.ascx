<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<NeoSistem.MakinaTurkiye.Management.Models.HelpModel>" %>

    <link href="/Content/jquery.treeTable.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.treeTable.js" type="text/javascript"></script>
 


  <script type="text/javascript">

      $(document).ready(function () {
          $("#tree").treeTable();
      });

</script>

<table id="tree">
  <tr id="node-1">
    <td>Parent</td>
  </tr>
  <tr id="node-2" class="child-of-node-1">
    <td>Child</td>
  </tr>
  <tr id="node-3" class="child-of-node-2">
    <td>Child</td>
  </tr>
</table>