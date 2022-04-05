namespace NeoSistem.EnterpriseEntity.Business
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;

    public abstract class AsynBusinessDataEntity : BusinessDataEntity
    {

        protected virtual IAsyncResult BeginExecuteReader(string procName)
        {
            IAsyncResult result = this.BeginExecuteReader(procName, null, null, null);
            return result;
        }

        protected virtual IAsyncResult BeginExecuteReader(string procName, ICollection<IDataParameter> prms)
        {
            IAsyncResult result = this.BeginExecuteReader(procName, prms, null, null);
            return result;
        }

        protected virtual IAsyncResult BeginExecuteReader(string procName, ICollection<IDataParameter> prms, TransactionUI transaction)
        {
            IAsyncResult result = this.BeginExecuteReader(procName, prms, transaction, null, null);
            return result;
        }

        protected virtual IAsyncResult BeginExecuteReader(string procName, AsyncCallback callback, object state)
        {
            IAsyncResult result = this.BeginExecuteReader(procName, null, callback, state);
            return result;
        }

        protected virtual IAsyncResult BeginExecuteReader(string procName, ICollection<IDataParameter> prms, AsyncCallback callback, object state)
        {
            DbCommand command = null;

            this.CreateDB();

            command = this.CommandBuilder(procName, prms);

            IAsyncResult result = this.DatabaseInstance.BeginExecuteReader(command, callback, state);

            return result;
        }



        protected virtual IAsyncResult BeginExecuteReader(string procName, ICollection<IDataParameter> prms, TransactionUI transaction, AsyncCallback callback, object state)
        {
            DbCommand command = null;

            this.TranDB(transaction);

            command = this.CommandBuilder(procName, prms);

            IAsyncResult result = this.DatabaseInstance.BeginExecuteReader(command, transaction.DbTransactionInstance, callback, state);

            return result;
        }

        protected virtual IDataReader EndExecuteReader(IAsyncResult state)
        {
            return this.DatabaseInstance.EndExecuteReader(state);
        }
    }
}
