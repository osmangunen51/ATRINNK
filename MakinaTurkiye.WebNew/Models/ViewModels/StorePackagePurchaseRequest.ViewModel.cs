using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.ViewModels
{
	public class StorePackagePurchaseRequestViewModel
	{
		public int MainPartyId { get; set; } = 0;
		[Required]
		public string FirstName { get; set; } = "";
		[Required]
		public string LastName { get; set; } = "";
		[Required]
		public string Phone { get; set; } = "";
		[Required]
		public int ProductQuantity { get; set; } = 0;
		public string Desciption { get; set; } = "";
		public string StoreName { get; set; } = "";
	}
}