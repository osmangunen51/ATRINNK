using System;

namespace MakinaTurkiye.Pinterest
{
    public class ProcessStatus
    {
        public enum ProcessType
        {
            Success = 1,
            Error = 2,
            Warning = 3,
            Info = 4
        }

        #region -Alanlar-

        /// <summary>
        /// İşlem Durumu
        /// </summary>
        private bool _Status = true;

        /// <summary>
        /// İşlem Durumu
        /// </summary>
        public bool Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }

        /// <summary>
        /// İşlem Mesajı
        /// </summary>
        private string _Message = "";

        /// <summary>
        /// İşlem Mesajı
        /// </summary>
        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
            }
        }

        /// <summary>
        /// İşlem Degerı
        /// </summary>
        private object _Value;

        /// <summary>
        /// İşlem Degerı
        /// </summary>
        public object Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        private Exception _Error = new Exception();

        public Exception Error
        {
            get { return _Error; }
            set { _Error = value; }
        }

        public ProcessType Tip { get; set; }

        #endregion -Alanlar-

        public ProcessStatus()
        {
            Tip = ProcessType.Success;
        }
    }
}