using System.Web;
using System.Web.Mvc;

namespace MiSOI_Passport_Reader
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}
	}
}
