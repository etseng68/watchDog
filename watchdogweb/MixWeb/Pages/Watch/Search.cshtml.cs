using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MixWeb.Models;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;

namespace MixWeb.Pages.Watch
{
   [Authorize]
	public class SearchModel : PageModel
    {
		private readonly MixWebContext _context;

		public SearchModel(MixWebContext context)
		{
			_context = context;
		}
		public async Task<IActionResult> OnGetAsync()
		{
			if (_context.Mwatches != null)
			{
				string rangeQry = Request.Query["range"].ToString();
				string domainQry = Request.Query["domain"].ToString();
				string startDayQry = Request.Query["start"].ToString();
				string endDayQry = Request.Query["end"].ToString();
				//Console.WriteLine(CultureInfo.CurrentCulture.ToString());
				if (!(String.IsNullOrWhiteSpace(rangeQry)))
				{
					DateTime sr = new DateTime();
					DateTime er = new DateTime();
					if (rangeQry == "h") //時
					{
						er = DateTime.Now;
						sr = er.AddHours(-1);
					}
					else if (rangeQry == "d") //日
					{
						er = DateTime.Now;
						sr = er.AddDays(-1);
					}
					else if (rangeQry == "m")//月
					{
						er = DateTime.Now;
						sr = er.AddMonths(-1);
					}
					else if (rangeQry == "c") //自訂
					{
						sr = DateTime.Parse(startDayQry);
						er = DateTime.Parse(endDayQry);
					}
					object? watchs = null;
					if (er != DateTime.MinValue && sr != DateTime.MinValue)
					{
						if (String.IsNullOrEmpty(domainQry))
						{
							watchs = await _context.Mwatches.AsNoTracking()
									.Where(w => w.WdateTime >= sr && w.WdateTime <= er)
									.Select(w => new { w.Url, w.Status, w.WdateTime, w.Org })
									.OrderByDescending(w => w.WdateTime)
									.ToArrayAsync();
						}
						else
						{
							watchs = await _context.Mwatches.AsNoTracking()
									.Where(w => w.WdateTime >= sr && w.WdateTime <= er)
									.Where(w => w.Url.Contains(domainQry))
									.Select(w => new { w.Url, w.Status, w.WdateTime, w.Org })
									.OrderByDescending(w => w.WdateTime)
									.ToArrayAsync();
						}
						return Content(JsonConvert.SerializeObject(watchs), "application/json");
					}
				}
			}
			return Content("[{\"Status\": \"未指定域名資料\",\"WdateTime\": \""+DateTime.Now.ToString()+"\"}]");	
			//	if (!(String.IsNullOrWhiteSpace(startDayQry)) && !(String.IsNullOrWhiteSpace(endDayQry)))
			//	{
			//		object? watchs = null;
					
			//		DateTime sDay = DateTime.Parse(startDayQry);
			//		DateTime eDay = DateTime.Parse(endDayQry);
			//		//DateTime tDay = convertDayTime("2023/4/13 上午 12:57:28");
			//		//Console.WriteLine(tDay.ToString());

			//		if (String.IsNullOrEmpty(domainQry))
			//		{
			//			watchs = await _context.Mwatches.AsNoTracking()
			//					.Where(w => w.WdateTime >= sDay && w.WdateTime <= eDay)
			//					.Select(w => new { w.Status, w.Wtime, w.Url })
			//					.OrderByDescending(w => w.Wtime)
			//					.ToArrayAsync();
			//		}
			//		else
			//		{
			//			watchs = await _context.Mwatches.AsNoTracking()
			//					.Where(w => w.WdateTime >= sDay && w.WdateTime <= eDay)
			//					.Where(w => w.Url.Contains(domainQry))
			//					.Select(w => new { w.Status, w.Wtime, w.Url })
			//					.OrderByDescending(w => w.Wtime)
			//					.ToArrayAsync();
			//		}
			//		return Content(JsonConvert.SerializeObject(watchs), "application/json");
			//	}
			//}


			//if (!(String.IsNullOrEmpty(domainQry)
			//		&& String.IsNullOrEmpty(startDayQry)
			//		&& String.IsNullOrEmpty(startDayQry)))
			//	{
			//		DateTime startDate = DateTime.Parse(startDayQry);
			//		DateTime endDate = DateTime.Parse(endDayQry);
			//		if (domainQry == "*")
			//		{
			//			watchs = await _context.Mwatches.AsNoTracking()
			//					.Where(w => DateTime.Parse(w.Wtime) >= startDate && DateTime.Parse(w.Wtime) <= endDate)
			//					.Select(w => new { w.Status, w.Wtime, w.Url })
			//					.OrderByDescending(w => w.Wtime)
			//					.ToArrayAsync();
			//		}
			//		else 
			//		{ 
			//			watchs = await _context.Mwatches.AsNoTracking()
			//					.Where(w => DateTime.Parse(w.Wtime) >= startDate && DateTime.Parse(w.Wtime) <= endDate)
			//					.Where(w => w.Url.Contains(domainQry))
			//					.Select(w => new { w.Status, w.Wtime, w.Url })
			//					.OrderByDescending(w => w.Wtime)
			//					.ToArrayAsync();
			//		}
			//		return Content(JsonConvert.SerializeObject(watchs), "application/json");
			//	}	
			//}
			//return Content("[{\"Status\": \"查詢資料不完整\",\"Wtime\": \"\"}]");
		}
		public DateTime convertDayTime(string dayTimeStr)
		{
			DateTime cDayTime;
			//CultureInfo c = CultureInfo.CurrentCulture;
			if(DateTime.TryParse(dayTimeStr, out cDayTime))
			{
				return cDayTime;
			}
			else
			{
				if(DateTime.TryParseExact(dayTimeStr,"yyyy-MM-dd",
					CultureInfo.InvariantCulture, DateTimeStyles.None, out cDayTime)){
					return cDayTime;
				}
				else  if (DateTime.TryParseExact(dayTimeStr, "yyyy/MM/dd tt hh:mm:ss",
					CultureInfo.CurrentCulture, DateTimeStyles.None,out cDayTime))
				{
					return cDayTime;
				}
			}
			return DateTime.Now;
		}
		//public void OnGet()
		//      {
		//      }
	}
}
