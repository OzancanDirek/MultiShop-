using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MultiShop.WebUI.Services.Interfaces;
using System.Net;
using System.Net.Http.Headers;

namespace MultiShop.WebUI.Handlers
{
    public class ResourceOwnerPasswordTokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IIdentityService _identityService;

        public ResourceOwnerPasswordTokenHandler(IIdentityService identityService, IHttpContextAccessor httpContextAccessor)
        {
            _identityService = identityService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // 1. Mevcut token'ı al ve başlığa ekle
            var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await base.SendAsync(request, cancellationToken);

            // 2. Eğer 401 Unauthorized dönerse (token süresi dolmuş olabilir)
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Refresh token kullanarak yeni access token al
                var isRefreshed = await _identityService.GetRefreshToken();

                if (isRefreshed)
                {
                    // KRİTİK DÜZELTME: Yeni alınan güncel token'ı çerezden (HttpContext) tekrar oku!
                    var newAccessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

                    // İsteğin başlığını yeni token ile güncelle
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newAccessToken);

                    // İsteği yeni token ile tekrar gönder
                    response = await base.SendAsync(request, cancellationToken);
                }
            }

            return response;
        }
    }
}