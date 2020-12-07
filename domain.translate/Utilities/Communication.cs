namespace domain.translate.Utilities
{
    using domain.translate.Enums;
    using Newtonsoft.Json.Linq;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Net.Http;
	using System.Text;
	using System.Threading.Tasks;

	public static class Communication
	{
		/// <summary>
		///		Connect to service without credentials
		/// </summary>
		/// <param name="url">string url</param>
		/// <param name="requestType">type of request</param>
		/// <param name="objectResult">expected success object result</param>
		/// <param name="objectErrorResult">expected error object result</param>
		/// <param name="objectErrorResultDescription">expected error object description</param>
		/// <param name="body">request body</param>
		/// <param name="bodyType">type of body</param>
		/// <param name="parameters">parameters of request</param>
		/// <returns></returns>
		public static object ConnectToService(string url, string model, RequestType requestType, string objectResult, string objectErrorResult,
														  string objectErrorResultDescription, string body = null, string bodyType = null,
														  string parameters = null)
		{
			try
			{
				HttpClient client = new HttpClient();
				string tokenEndpoint = url;
				StringContent stringContent;
				string result = string.Empty;

				switch (requestType)
				{
					case RequestType.Get:
						{
							var returnRequest = client.GetAsync(tokenEndpoint).Result;
							result = returnRequest.Content.ReadAsStringAsync().Result;

							break;
						}
					case RequestType.Post:
						{
							stringContent = new StringContent(body, Encoding.UTF8, bodyType);

							var returnRequest = client.PostAsync(tokenEndpoint, stringContent).Result;
							result = returnRequest.Content.ReadAsStringAsync().Result;

							break;
						}
					case RequestType.Put:
						{
							stringContent = new StringContent(body, Encoding.UTF8, bodyType);

							var returnRequest = client.PutAsync(tokenEndpoint, stringContent).Result;
							result = returnRequest.Content.ReadAsStringAsync().Result;

							break;
						}
					case RequestType.Delete:
						{
							var returnRequest = client.DeleteAsync(tokenEndpoint).Result;
							result = returnRequest.Content.ReadAsStringAsync().Result;

							break;
						}
					default:
						break;
				}

				JObject jobject = !string.IsNullOrEmpty(result) ? JObject.Parse(result) : null;
				var obj = jobject != null ? (jobject[objectResult]?.ToList()?.Count > 0 ? jobject[objectResult]?.ToList() : null) : null;

				return (obj == null && jobject?.ToString() == null && jobject[objectResult]?.ToString() == null) ? throw new Exception(($"{jobject[objectErrorResult]?.ToString()} - {jobject[objectErrorResultDescription]?.ToString()}") ?? (new { Error = new { Code = 400, Message = $"{model} - Requisição inválida." } }).ToString()) : (obj ?? (object)jobject[objectResult]?.ToString()) == null ? jobject : (obj ?? (object)jobject[objectResult]?.ToString());
			}
			catch (NullReferenceException)
			{
				return null;
			}
			catch (Exception e)
			{
				throw new Exception($"{model} - Requisição inválida. Detalhes: {e.Message ?? e.InnerException.Message}");
			}
		}

		/// <summary>
		///		Connect to service with credentials
		/// </summary>
		/// <param name="url">string url</param>
		/// <param name="requestType">type of request</param>
		/// <param name="handler">credentials</param>
		/// <param name="objectResult">expected success object result</param>
		/// <param name="objectErrorResult">expected error object result</param>
		/// <param name="objectErrorResultDescription">expected error object description</param>
		/// <param name="body">request body</param>
		/// <param name="bodyType">type of body</param>
		/// <param name="parameters">parameters of request</param>
		/// <returns></returns>
		public static object ConnectToService(string url, string model, RequestType requestType, HttpClientHandler handler, string objectResult, string objectErrorResult,
														  string objectErrorResultDescription, string body = null, string bodyType = null,
														  string parameters = null)
		{
			try
			{
				HttpClient client = new HttpClient(handler);
				string tokenEndpoint = url;
				StringContent stringContent;
				string result = string.Empty;

				switch (requestType)
				{
					case RequestType.Get:
						{
							var returnRequest = client.GetAsync(tokenEndpoint).Result;
							result = returnRequest.Content.ReadAsStringAsync().Result;

							break;
						}
					case RequestType.Post:
						{
							stringContent = new StringContent(body, Encoding.UTF8, bodyType);

							var returnRequest = client.PostAsync(tokenEndpoint, stringContent).Result;
							result = returnRequest.Content.ReadAsStringAsync().Result;

							break;
						}
					case RequestType.Put:
						{
							stringContent = new StringContent(body, Encoding.UTF8, bodyType);

							var returnRequest = client.PutAsync(tokenEndpoint, stringContent).Result;
							result = returnRequest.Content.ReadAsStringAsync().Result;

							break;
						}
					case RequestType.Delete:
						{
							var returnRequest = client.DeleteAsync(tokenEndpoint).Result;
							result = returnRequest.Content.ReadAsStringAsync().Result;

							break;
						}
					default:
						break;
				}

				JObject jobject = !string.IsNullOrEmpty(result) ? JObject.Parse(result) : null;
				var obj = jobject != null ? (jobject[objectResult]?.ToList()?.Count > 0 ? jobject[objectResult]?.ToList() : null) : null;

				return (obj == null && jobject?.ToString() == null && jobject[objectResult]?.ToString() == null) ? throw new Exception(($"{jobject[objectErrorResult]?.ToString()} - {jobject[objectErrorResultDescription]?.ToString()}") ?? (new { Error = new { Code = 400, Message = $"{model} - Requisição inválida." } }).ToString()) : (obj ?? (object)jobject[objectResult]?.ToString()) == null ? jobject : (obj ?? (object)jobject[objectResult]?.ToString());
			}
			catch (NullReferenceException)
			{
				return null;
			}
			catch (Exception e)
			{
				throw new Exception($"{model} - Requisição inválida. {e.Message.Split(',')[0] ?? e.InnerException.Message.Split(',')[0]}");
			}
		}
	}
}
