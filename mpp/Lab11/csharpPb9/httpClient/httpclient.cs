using System.Text;
using System.Text.Json;


namespace restApiTest
{
	class Program
	{
		private static readonly HttpClient client = new HttpClient();
		private static readonly string baseUrl = "https://localhost:7063/api/proba";

		public static async Task Main(string[] args)
		{
			var options = new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase
			};

            HttpResponseMessage getAllResponse = await client.GetAsync($"{baseUrl}");

            try
			{
				Console.WriteLine("Teste:");

				Console.WriteLine("\n1. Teste add:");
				string id1 = await AddProba("100m Sprint", "alergat");
				string id2 = await AddProba("Marathon", "ciclism");
				string id3 = await AddProba("Swimming", "inot");

				Console.WriteLine("\n2. Teste get:");
				await GetAllProbas();

				Console.WriteLine($"\n3.Test get by id {id1}:");
				await GetProbaById(id1);

				Console.WriteLine($"\n4. Test put (uptadte) {id2}:");
				await UpdateProba(id2, "Half Marathon", "ciclism");

				Console.WriteLine($"\n5. test delete {id3}:");
				await DeleteProba(id3);

				Console.WriteLine($"\n6. test daca s-a sters {id3}:");
				await GetProbaById(id3);

				Console.WriteLine("\n7. get all final:");
				await GetAllProbas();
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}

			Console.ReadKey();
		}

		static async Task<string> AddProba(string nume, string categorie)
		{
			var probaDto = new ProbaDTO { Nume = nume, Categorie = categorie };
			var content = new StringContent(
				JsonSerializer.Serialize(probaDto),
				Encoding.UTF8,
				"application/json");

			HttpResponseMessage response = await client.PostAsync(baseUrl, content);

			Console.WriteLine($"POST Status: {response.StatusCode}");

			if (response.IsSuccessStatusCode)
			{
				// Extract the ID from the response
				string responseUri = response.Headers.Location.ToString();
				string id = responseUri.Substring(responseUri.LastIndexOf('/') + 1);
				Console.WriteLine($"Added Proba with ID: {id}");
				return id;
			}
			else
			{
				string errorResponse = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error: {errorResponse}");
				return null;
			}
		}

		static async Task GetAllProbas()
		{
			HttpResponseMessage response = await client.GetAsync(baseUrl);

			Console.WriteLine($"GET All Status: {response.StatusCode}");

			if (response.IsSuccessStatusCode)
			{
				string jsonResponse = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Response: {jsonResponse}");
			}
			else
			{
				string errorResponse = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error: {errorResponse}");
			}
		}

		static async Task GetProbaById(string id)
		{
			HttpResponseMessage response = await client.GetAsync($"{baseUrl}/{id}");

			Console.WriteLine($"GET By ID Status: {response.StatusCode}");

			if (response.IsSuccessStatusCode)
			{
				string jsonResponse = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Response: {jsonResponse}");
			}
			else
			{
				string errorResponse = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error: {errorResponse}");
			}
		}

		static async Task UpdateProba(string id, string nume, string categorie)
		{
			var probaDto = new ProbaDTO { Nume = nume, Categorie = categorie };
			var content = new StringContent(
				JsonSerializer.Serialize(probaDto),
				Encoding.UTF8,
				"application/json");

			HttpResponseMessage response = await client.PutAsync($"{baseUrl}/{id}", content);

			Console.WriteLine($"PUT Status: {response.StatusCode}");

			if (response.IsSuccessStatusCode)
			{
				string jsonResponse = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Updated Proba: {jsonResponse}");
			}
			else
			{
				string errorResponse = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error: {errorResponse}");
			}
		}

		static async Task DeleteProba(string id)
		{
			HttpResponseMessage response = await client.DeleteAsync($"{baseUrl}/{id}");

			Console.WriteLine($"DELETE Status: {response.StatusCode}");

			if (response.IsSuccessStatusCode)
			{
				string jsonResponse = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Deleted Proba: {jsonResponse}");
			}
			else
			{
				string errorResponse = await response.Content.ReadAsStringAsync();
				Console.WriteLine($"Error: {errorResponse}");
			}
		}
	}

	public class ProbaDTO
	{
		public string Nume { get; set; }
		public string Categorie { get; set; }
		public ProbaDTO() { }
		public ProbaDTO(string nume, string categorie)
		{
			Nume = nume;
			Categorie = categorie;
		}
	}

}