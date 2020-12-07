namespace domain.translate.Models
{
	using System;

	public class BaseModel
	{
		public DateTime? InsertDate { get; set; }
		public int? UserInsert { get; set; }
		public DateTime? UpdateDate { get; set; }
		public int? UserUpdate { get; set; }

	}
}
