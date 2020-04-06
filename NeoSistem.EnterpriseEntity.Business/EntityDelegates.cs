namespace NeoSistem.EnterpriseEntity.Business
{
    public delegate void EntitySaveHandler(object sender, SaveCommandArgs e);
  public delegate void EntityDeleteHandler(object sender, DeleteCommandArgs e);
}
