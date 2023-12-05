using System;
using System.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Agava.GameCoupons
{
    public static class GameCoupons
    {
        private const string BaseAddress = "https://stage.game-coupon.com";

        private static LoginResponse? _loginData;

        public static bool Authorized => _loginData != null;

        public static async Task<bool> Login(string functionId, Action<string> onErrorCallback = null)
        {
            using var request = UnityWebRequest.Post($"https://functions.yandexcloud.net/{functionId}?integration=raw", "{}", "application/json");

            await request.SendWebRequest();
            _loginData = Parse<LoginResponse>(request, onErrorCallback);
            
            return _loginData != null;
        }

        public static async Task<CouponIssuanceResponse?> CouponIssuance(CouponIssuanceRequest requestData, Action<string> onErrorCallback = null)
        {
            Debug.Log(JsonConvert.SerializeObject(requestData));
            using var request = UnityWebRequest.Post($"{BaseAddress}/api/v1/coupon_issuance", JsonConvert.SerializeObject(requestData), "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {(_loginData ?? default).access}");

            await request.SendWebRequest();

            return Parse<CouponIssuanceResponse>(request, onErrorCallback);
        }

        public static async Task<GamesResponse?> GetGames(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/games?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {(_loginData ?? default).access}");

            await request.SendWebRequest();

            return Parse<GamesResponse>(request, onErrorCallback);
        }

        public static async Task<GenresResponse?> Genres(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/genres?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {(_loginData ?? default).access}");

            await request.SendWebRequest();

            return Parse<GenresResponse>(request, onErrorCallback);
        }

        public static async Task<OrganizationsResponse?> Organizations(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/organizations?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {(_loginData ?? default).access}");

            await request.SendWebRequest();

            return Parse<OrganizationsResponse>(request, onErrorCallback);
        }

        public static async Task<PlatformsResponse?> Platforms(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/platforms?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {(_loginData ?? default).access}");

            await request.SendWebRequest();

            return Parse<PlatformsResponse>(request, onErrorCallback);
        }

        public static async Task<bool> Refresh(Action<string> onErrorCallback = null)
        {
            var postData = $"{{\"refresh\": \"{(_loginData ?? default).refresh}\"}}";
            using var request = UnityWebRequest.Post($"{BaseAddress}/api/v1/refresh", postData, "application/json");

            await request.SendWebRequest();

            var response = Parse<RefreshResponse>(request, onErrorCallback);

            if (response == null)
                return false;

            _loginData = new LoginResponse()
            {
                access = response.Value.access,
                refresh = _loginData.Value.refresh
            };

            return true;
        }

        private static T? Parse<T>(UnityWebRequest request, Action<string> onErrorCallback) where T : struct
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
