using System;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Management.Models
{
    public class SubConstantModel
    {
        public SubConstantModel()
        {
            this.Contents = new Dictionary<int, string>();
        }
        public string Content { get; set; }
        public string ConstantId { get; set; }
        public String Message { get; set; }

        public Dictionary<int, string> Contents { get; set; }
    }
}

