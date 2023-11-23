using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Szevasz
{
    internal class peopleservice
    {
		private HttpClient client = new HttpClient();
		private string url = "https://retoolapi.dev/yxp08e/data";

		public List<person> GetAll()
		{
			string json = client.GetStringAsync(url).Result;
			return JsonConvert.DeserializeObject<List<person>>(json);
		}

		public person Add(person person)
		{
			StringContent content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
			HttpResponseMessage responsemessage = client.PostAsync(url, content).Result;
			string responsecontent = responsemessage.Content.ReadAsStringAsync().Result;
			return JsonConvert.DeserializeObject<person>(responsecontent);

		}
		public bool Delete(person person)
		{
			int id = person.Id;
			HttpResponseMessage response = client.DeleteAsync($"{url}/{id}").Result;
			return response.IsSuccessStatusCode;
		}

		public person Update(int id, person person)
		{
			StringContent content = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
			HttpResponseMessage responseMessage = client.PatchAsync($"{url}/{id}", content).Result;

			string responseContent = responseMessage.Content.ReadAsStringAsync().Result;
			return JsonConvert.DeserializeObject<person>(responseContent);
		}
	}
}
