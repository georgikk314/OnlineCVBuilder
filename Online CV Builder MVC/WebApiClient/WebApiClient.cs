using Online_CV_Builder_MVC.JwtTokenHandler;
using System.Net.Http.Headers;

namespace Online_CV_Builder_MVC.WebApiClient
{
	public class WebApiClient
	{
		private readonly HttpClient _httpClient;

		public WebApiClient(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<T> GetAsync<T>(string requestUri)
		{
			// Send the GET request to the API and include the JWT token in the Authorization header
			var response = await _httpClient.GetAsync(requestUri);
			response.EnsureSuccessStatusCode();

			// Deserialize the JSON response to the specified type T
			var responseBody = await response.Content.ReadAsAsync<T>();
			return responseBody;
		}
	}
}
