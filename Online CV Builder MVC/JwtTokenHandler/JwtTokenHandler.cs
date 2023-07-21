using System.Net.Http.Headers;
using System.Security.Claims;

namespace Online_CV_Builder_MVC.JwtTokenHandler
{
	public class JwtTokenHandler : DelegatingHandler
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public JwtTokenHandler(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			// Get the JWT token from the current HttpContext
			var jwtToken = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (!string.IsNullOrEmpty(jwtToken))
			{
				// Add the JWT token to the request headers
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
			}

			// Continue sending the request
			return await base.SendAsync(request, cancellationToken);
		}
	}
}
