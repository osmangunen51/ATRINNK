namespace NeoSistem.EnterpriseEntity.Business
{
    using System;

    public class SaveCommandArgs : EventArgs
  {
    public SaveCommandArgs(SaveCommandArgs e)
    {
      Action = e.Action;
      Value = e.Value;
      Transaction = e.Transaction;
    }

    public SaveCommandArgs(TransactionUI trans, EntityAction action, object value)
    {
      Action = action;
      Value = value;
      Transaction = trans;
    }

    public EntityAction Action { get; private set; }
    public object Value { get; private set; }
    public TransactionUI Transaction { get; private set; }
  }

  public class DeleteCommandArgs : EventArgs
  {
    public DeleteCommandArgs(DeleteCommandArgs e)
    {
      Value = e.Value;
      Transaction = e.Transaction;
    }

    public DeleteCommandArgs(TransactionUI trans, object value)
    {
      Value = value;
      Transaction = trans;
    }

    public object Value { get; private set; }
    public TransactionUI Transaction { get; private set; }
  }
}
