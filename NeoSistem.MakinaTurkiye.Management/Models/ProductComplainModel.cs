using System;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class ProductComplainModel
        {
            public int ID { get; set; }
            public int ProductID { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Comment { get; set; }
            public string ProductName { get; set; }
            public DateTime ?ComplainDate { get; set; }
            public string ProductNo { get; set; }
            public bool IsMember { get; set; }
            public string ComplainNames { get; set; }
        }

   
}