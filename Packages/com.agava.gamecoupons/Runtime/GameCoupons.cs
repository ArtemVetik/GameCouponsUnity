using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Agava.GameCoupons
{
    public static class GameCoupons
    {
        private const string BaseAddress = "https://stage.game-coupon.com";

        private static string _refreshToken;

        public static async Task Login(int id, string password, Action<string> onErrorCallback = null)
        {
            var postData = JsonUtility.ToJson(new LoginRequest() { id = id, password = password });
            using var request = UnityWebRequest.Post($"{BaseAddress}/api/v1/login", postData, "application/json");

            await request.SendWebRequest();

            var login = Parse<LoginResponse>(request, onErrorCallback);
            _refreshToken = login.refresh;
        }

        public static async Task<GenresResponse> Genres(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/genres?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {_refreshToken}");

            await request.SendWebRequest();

            return Parse<GenresResponse>(request, onErrorCallback);
        }

        public static async Task<OrganizationsResponse> Organizations(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/organizations?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {_refreshToken}");

            await request.SendWebRequest();

            return Parse<OrganizationsResponse>(request, onErrorCallback);
        }

        public static async Task<PlatformsResponse> Platforms(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/platforms?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {_refreshToken}");

            await request.SendWebRequest();

            return Parse<PlatformsResponse>(request, onErrorCallback);
        }

        public static async Task<RefreshResponse> Refresh(Action<string> onErrorCallback = null)
        {
            var postData = $"{{\"refresh\": {_refreshToken}}}";
            using var request = UnityWebRequest.Post($"{BaseAddress}/api/v1/refresh", postData, "application/json");

            await request.SendWebRequest();

            return Parse<RefreshResponse>(request, onErrorCallback);
        }

        private static T Parse<T>(UnityWebRequest request, Action<string> onErrorCallback) where T : struct
        {
            if (request.result != UnityWebRequest.Result.Success)
            {
                onErrorCallback?.Invoke(request.error);
                return default;
            }

            if (request.responseCode == 200)
                return JsonUtility.FromJson<T>(request.downloadHandler.text);

            onErrorCallback?.Invoke($"Response code: {request.responseCode}");
            return default;
        }
    }
}
