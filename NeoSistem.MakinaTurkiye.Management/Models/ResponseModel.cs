﻿namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public string Message { get; set; }
       
    }
}