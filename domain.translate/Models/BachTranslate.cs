namespace domain.translate.Models
{
	using System.Collections.Generic;

	public class BachTranslate
	{
		public string Culture { get; set; }
		public List<Translate> Translates { get; set; }
	}
}
