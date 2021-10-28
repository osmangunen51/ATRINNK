namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }

    }
}