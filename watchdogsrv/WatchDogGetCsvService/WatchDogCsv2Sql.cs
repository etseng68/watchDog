using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WatchDogGetCsvService
{
	public partial class WatchDogCsv2Sql : ServiceBase
	{
		public string _serviceName = "WatchDogCsv2Sql Service";
		public string dbConn = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
		private Timer timer;
		public WatchDogCsv2Sql()
		{
			InitializeComponent();

		}

		protected override void OnStart(string[] args)
		{
			// 讀取設定檔中的參數
			//var dbConn = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
			var csvUrl = ConfigurationManager.AppSettings["CsvUrl"];
			var localFilePath = ConfigurationManager.AppSettings["LocalFilePath"];
			var watchTable = ConfigurationManager.AppSettings["WatchTableName"];
			var domainTable = ConfigurationManager.AppSettings["DomainTableName"];
			var logTable = ConfigurationManager.AppSettings["GetLogTableName"];
			var orgTable = ConfigurationManager.AppSettings["OrgTableName"];
			var intervalMinutes = int.Parse(ConfigurationManager.AppSettings["IntervalMinutes"]);
			
			// 讀取外部參數
			if (args.Length > 0)
			{
				csvUrl = args[0];
			}
			if (args.Length > 1)
			{
				intervalMinutes = int.Parse(args[1]);
			}

			// 建立 Timer 並設定觸發事件
			timer = new Timer((state) =>
			{
				var log = new StringBuilder();
				log.AppendLine($"讀取{csvUrl}");
				Int64 logId = InserGetLog(logTable, log.ToString());
				try
				{
					if (File.Exists(localFilePath))
					{
						File.Delete(localFilePath);
						EventLog.WriteEntry(_serviceName, "Found csv file is being deleted.", EventLogEntryType.Information);
						log.AppendLine($"找到CSV暫存檔:{localFilePath},已刪除!");
					}
					// 下載 CSV 檔案
					using (var client = new WebClient())
					{
						client.DownloadFile(csvUrl, localFilePath);
					}

					// 讀取 CSV 檔案
					using (var reader = new StreamReader(localFilePath))
					{
						// 跳過標題列
						reader.ReadLine();
						string pOrg = "WORG";
						int orgId = 0;
						int i= 0;
						// 讀取每一行並寫入資料庫
						while (!reader.EndOfStream)
						{
							var line = reader.ReadLine();
							var values = line.Split(',');
							//var name = values[0];
							//var age = int.Parse(values[1]);
							var url = values[0];
							var status = values[1];
							var wtime = values[2];
							var org = values[3];
							var domainId = Convert.ToInt32(values[4]);

							if (pOrg != org)
							{
								pOrg = org;
								//orgId = GetOrgId(pOrg, orgTable);
								orgId = GetTableId($"SELECT TOP 1 * FROM {orgTable} WHERE org = '{pOrg}'");
							}
							// 檢查名字是否已存在Doamin資料表，存在則不寫入資料庫
							//var domainId = GetTableId($"SELECT TOP 1 * FROM {domainTable} WHERE WUrl = '{url}'");
							//if(domainId == 0)
							//{
							//	domainId = Convert.ToInt32(InsertDomain(url, intervalMinutes, org, domainTable));
							//	log.AppendLine($"域名:{url},資料不存在,寫入Domain完成,ID={domainId}");
							//}
							// 檢查名字是否已存在watch，存在則不寫入資料庫
							if (!IsWatchExist(domainId, wtime, watchTable))
							{
								i++;
								InsertWatch(url, status, wtime, org, watchTable,domainId, orgId, logId);
							}
						}
						EventLog.WriteEntry(_serviceName, "CSV file imported successfully", EventLogEntryType.Information);
						log.AppendLine($"CSV檔案轉資料庫完成!共寫入{i}筆");

					}
					if (File.Exists(localFilePath))
					{
						File.Delete(localFilePath);
						EventLog.WriteEntry(_serviceName, "The csv file is deleted after the data is updated.", EventLogEntryType.Information);
						log.AppendLine($"刪除CSV暫存檔:{localFilePath}.");
					}
				}
				catch (WebException ex)
				{
					EventLog.WriteEntry(_serviceName, $"The CSV file {csvUrl} does not exist: {ex.Message}", EventLogEntryType.Error);
					log.AppendLine($"讀取CSV檔案:{csvUrl} ,錯誤:{ex.Message}");

				}
				catch (Exception ex)
				{
					EventLog.WriteEntry(_serviceName, $"Error importing data : {ex.Message}", EventLogEntryType.Error);
					log.AppendLine($"CSV檔案轉資料庫錯誤:{ex.Message}");
				}
				UpdateGetLog(logId, log.ToString(), logTable);
			}, null, 0, intervalMinutes * 60 * 1000);
		}

		protected override void OnStop()
		{

			// 停止 Timer
			timer.Dispose();
		}
		public int GetTableId(String sql)
		{
            //string sql = $"SELECT TOP 1 * FROM {tableName} WHERE WUrl = '{url}'";
            try
            {
                using (SqlConnection connection = new SqlConnection(dbConn))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(_serviceName, $"Error DB GetOrgId : {ex.Message}", EventLogEntryType.Error);
                //Debug.Print(ex.ToString());
            }
            return 0;
        }
		//public int GetOrgId(string org, string tableName)
		//{
		//	string sql = $"SELECT TOP 1 * FROM {tableName} WHERE org = '{org}'";
		//	try
		//	{
		//		using (SqlConnection connection = new SqlConnection(dbConn))
		//		{
		//			connection.Open();
		//			using (SqlCommand command = new SqlCommand(sql, connection))
		//			{
		//				using (SqlDataReader reader = command.ExecuteReader())
		//				{
		//					if (reader.Read())
		//					{
		//						return reader.GetInt32(0);
		//					}
		//				}
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		EventLog.WriteEntry(_serviceName, $"Error DB GetOrgId : {ex.Message}", EventLogEntryType.Error);
		//		//Debug.Print(ex.ToString());
		//	}
		//	return 0;
		//}
		public bool IsUrlInDomain(string url, string tableName)
		{
			string sql = $"SELECT COUNT(*) FROM {tableName} WHERE WUrl = '{url}'";
			var count = sqlExecuteScalar(sql);
			return count > 0;
			//using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString))
			//{
			//    connection.Open();
			//    var command = new SqlCommand($"SELECT COUNT(*) FROM {tableName} WHERE WUrl = @Url", connection);
			//    command.Parameters.AddWithValue("@Url", url);
			//    var count = (int)command.ExecuteScalar();
			//    return count > 0;
			//}
		}
		public Int64 InsertDomain(string url, int time, string org, string tableName)
		{
			string sql = $"INSERT INTO {tableName} (WUrl,RTime,WAct,Org) VALUES ('{url}',{time},'GET','{org}') SELECT IDENT_CURRENT('{tableName}')";
			return sqlExecuteScalar(sql);

			//using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString))
			//{
			//    connection.Open();
			//    var command = new SqlCommand($"INSERT INTO {tableName} (WUrl,RTime,WAct,Org) VALUES (@WUrl,@RTime,@WAct,@Org)", connection);
			//    command.Parameters.AddWithValue("@WUrl", url);
			//    command.Parameters.AddWithValue("@RTime", 2);
			//    command.Parameters.AddWithValue("@WAct", "GET");
			//    command.Parameters.AddWithValue("@Org", org);
			//    command.ExecuteNonQuery();
			//}
		}

		public bool IsWatchExist(int domainId, string wtime, string tableName)
		{
			//string sql = $"SELECT COUNT(*) FROM {tableName} WHERE Url = '{url}' AND WTime = '{wtime}'";
			string sql = $"SELECT COUNT(*) FROM {tableName} WHERE DomainId = {domainId} AND WTime = '{wtime}'";
			var count = sqlExecuteScalar(sql);
			return count > 0;
			//using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString))
			//         {
			//             connection.Open();
			//             var command = new SqlCommand($"SELECT COUNT(*) FROM {tableName} WHERE Url = @Url AND WTime = @WTime", connection);
			//             command.Parameters.AddWithValue("@Url", url);
			//	command.Parameters.AddWithValue("@WTime", wtime);
			//             var count = (int)command.ExecuteScalar();
			//             return count > 0;
			//         }
		}
		public Int64 InserGetLog(string tableName, string note)
		{
			string sql = $"INSERT INTO {tableName} (note) VALUES ('{note}') SELECT IDENT_CURRENT('{tableName}')";
			return sqlExecuteScalar(sql);
		}
		public void UpdateGetLog(Int64 id, string note, string tableName)
		{
			string sql = $"UPDATE {tableName} SET note='{note}' WHERE id={id}";
			sqlExecuteNonQuery(sql);
		}

		public Int64 InsertWatch(string url, string status, string wtime, string org, string tableName,int domainId, int orgId, Int64 getId)
		{
            var cultureInfo = new CultureInfo("zh-Hant");
            string wDatetime = (DateTime.Parse(wtime,cultureInfo)).ToString("yyyy-MM-dd HH:mm:ss");
			string sql = $"INSERT INTO {tableName} (Url,Status,WTime,Org,WDateTime,DomainId,OrgId,GetId) VALUES ('{url}','{status}','{wtime}','{org}','{wDatetime}',{domainId},{orgId},{getId}) SELECT IDENT_CURRENT('{tableName}')";
			return sqlExecuteScalar(sql);
			//        using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString))
			//        {
			//            connection.Open();
			//            var command = new SqlCommand($"INSERT INTO {tableName} (Url,Status,WTime,Org,WDateTime,OrgId,GetId) VALUES (@Url,@Status,@WTime,@Org,@WDateTime,@OrgId,@GetId)", connection);

			//            //command.Parameters.AddWithValue("@Name", name);
			//            //command.Parameters.AddWithValue("@Age", age);
			//            command.Parameters.AddWithValue("@Url", url);
			//            command.Parameters.AddWithValue("@Status", status);
			//            command.Parameters.AddWithValue("@WTime", wtime);
			//            command.Parameters.AddWithValue("@Org", org);
			//command.Parameters.AddWithValue("@WDateTime", DateTime.Parse(wtime));
			//command.Parameters.AddWithValue("@OrgId", orgId);
			//command.Parameters.AddWithValue("@GetId", getId);
			//command.ExecuteNonQuery();
			//        }

		}

		public void sqlExecuteNonQuery(string sql)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(dbConn))
				{
					connection.Open();
					SqlCommand command = new SqlCommand(sql, connection);
					command.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				EventLog.WriteEntry(_serviceName, $"sql:{sql},Error DB ExecuteNonQuery : {ex.Message}", EventLogEntryType.Error);
				//Debug.Print(ex.ToString());
			}
		}

		public Int64 sqlExecuteScalar(string sql)
		{
			Int64 identity = -1;
			try
			{
				using (SqlConnection connection = new SqlConnection(dbConn))
				{
					connection.Open();
					SqlCommand command = new SqlCommand(sql, connection);
					command.CommandType = CommandType.Text;
					object value = command.ExecuteScalar();
					identity = Convert.ToInt64(value);
				}
			}
			catch (Exception ex)
			{
				EventLog.WriteEntry(_serviceName, $"sql:{sql},Error DB ExecuteScalar : {ex.Message}", EventLogEntryType.Error);
				//Debug.Print(ex.ToString());
			}
			return identity;
		}
		//static bool DownloadCsv(string url, string filePath, int maxRetry = 3, int retryIntervalSeconds = 10)
		//{
		//    bool success = false;
		//    int retryCount = 0;


		//    while (!success && retryCount < maxRetry)
		//    {
		//        try
		//        {
		//            using (var client = new WebClient())
		//            {
		//                client.DownloadFile(url, filePath);
		//            }
		//            success = true;
		//        }
		//        catch (Exception ex)
		//        {
		//            //Console.WriteLine($"An error occurred while downloading the CSV file: {ex.Message}");
		//            EventLog.WriteEntry("Csv2Sql Service", $"Error importing data :" + ex.Message, EventLogEntryType.Error);
		//            retryCount++;
		//            if (retryCount < maxRetry)
		//            {
		//                //Console.WriteLine($"Retry after {retryIntervalSeconds} seconds.");
		//                EventLog.WriteEntry("Csv2Sql Service", $"Retry after {retryIntervalSeconds} seconds." + ex.Message, EventLogEntryType.Warning);
		//                Thread.Sleep(retryIntervalSeconds * 1000);
		//            }
		//        }
		//    }

		//    return success;
		//}
	}
}
