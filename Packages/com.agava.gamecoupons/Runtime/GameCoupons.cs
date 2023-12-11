using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Agava.GameCoupons
{
    public static class GameCoupons
    {
        private const string BaseAddress = "https://stage.game-coupon.com";

        private static LoginResponse _loginData;
        private static string _functionId;

        public static bool Authorized => _loginData != null && _functionId != null;

        public static async Task<bool> Login(string functionId, Action<string> onErrorCallback = null)
        {
            var postData = "{\"method\": \"LOGIN\"}";
            using var request = UnityWebRequest.Post($"https://functions.yandexcloud.net/{functionId}?integration=raw", postData, "application/json");

            await request.SendWebRequest();
            _loginData = Parse<LoginResponse>(request, onErrorCallback);
            _functionId = functionId;

            return _loginData != null;
        }

        public static async Task<CouponIssuanceResponse> CouponIssuance(float longitude, float latitude, int gameId, Action<string> onErrorCallback = null)
        {
            ThrowIfNotAuthorized();

            var requestData = new CouponIssuanceRequest()
            {
                longitude = longitude,
                latitude = latitude,
                game_id = gameId,
                genre_ids = new int[0],
                platform_ids = new int[0],
                exclude_organization_ids = new int[0],
            };
            using var request = UnityWebRequest.Post($"{BaseAddress}/api/v1/coupon_issuance", JsonUtility.ToJson(requestData), "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {_loginData.access}");

            await request.SendWebRequest();

            return Parse<CouponIssuanceResponse>(request, onErrorCallback);
        }

        public static async Task<GamesResponse> GetGames(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            ThrowIfNotAuthorized();

            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/games?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {_loginData.access}");

            await request.SendWebRequest();

            return Parse<GamesResponse>(request, onErrorCallback);
        }

        public static async Task<GenresResponse> Genres(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            ThrowIfNotAuthorized();

            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/genres?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {_loginData.access}");

            await request.SendWebRequest();

            return Parse<GenresResponse>(request, onErrorCallback);
        }

        public static async Task<OrganizationsResponse> Organizations(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            ThrowIfNotAuthorized();

            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/organizations?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {_loginData.access}");

            await request.SendWebRequest();

            return Parse<OrganizationsResponse>(request, onErrorCallback);
        }

        public static async Task<PlatformsResponse> Platforms(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            ThrowIfNotAuthorized();

            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/platforms?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {_loginData.access}");

            await request.SendWebRequest();

            return Parse<PlatformsResponse>(request, onErrorCallback);
        }

        public static async Task<bool> Refresh(Action<string> onErrorCallback = null)
        {
            ThrowIfNotAuthorized();

            var postData = $"{{\"refresh\": \"{_loginData.refresh}\"}}";
            using var request = UnityWebRequest.Post($"{BaseAddress}/api/v1/refresh", postData, "application/json");

            await request.SendWebRequest();

            var response = Parse<RefreshResponse>(request, onErrorCallback);

            if (response == null)
                return false;

            _loginData = new LoginResponse()
            {
                access = response.access,
                refresh = _loginData.refresh
            };

            return true;
        }

        public static async Task<CityListResponse> CityList(string language, Action<string> onErrorCallback = null)
        {
            var postData = $"{{\"method\": \"CITY_LIST\", \"body\": \"{language}\"}}";
            using var request = UnityWebRequest.Post($"https://functions.yandexcloud.net/{_functionId}?integration=raw", postData, "application/json");

            await request.SendWebRequest();

            return Parse<CityListResponse>(request, onErrorCallback);
        }

        private static void ThrowIfNotAuthorized()
        {
            if (Authorized == false)
                throw new InvalidOperationException("You have to login first.");
        }

        private static T Parse<T>(UnityWebRequest request, Action<string> onErrorCallback) where T : class
        {
            if (request.result != UnityWebRequest.Result.Success)
            {
                onErrorCallback?.Invoke($"{request.error}\n{request.downloadHandler.text}");
                return null;
            }

            if (request.responseCode == 200)
                return JsonUtility.FromJson<T>(request.downloadHandler.text);

            onErrorCallback?.Invoke($"Response code: {request.responseCode}");
            return null;
        }
    }
}
