namespace NeoSistem.MakinaTurkiye.Management.Models
{
    /// <summary>
    /// jQuery TreeView plugin node
    /// </summary>
    public class TreeViewNode
  {
    #region Properties
    /// <summary>
    /// Node identifier
    /// </summary>
    public string id
    {
      get;
      set;
    }

    /// <summary>
    /// Node text
    /// </summary>
    public string text
    {
      get;
      set;
    }

    /// <summary>
    /// Whether node is expanded
    /// </summary>
    public bool expanded
    {
      get;
      set;
    }

    /// <summary>
    /// Whether node has children
    /// </summary>
    public bool hasChildren
    {
      get;
      set;
    }

    /// <summary>
    /// CSS classes for node
    /// </summary>
    public string classes
    {
      get;
      set;
    }

    /// <summary>
    /// CSS classes for node
    /// </summary>
    public string rowclasses
    {
      get;
      set;
    }



    /// <summary>
    /// Node children's
    /// </summary>
    public TreeViewNode[] children
    {
      get;
      set;
    }

    public int groupId { get; set; }
    public string tool { get; set; }

    #endregion
  }
}