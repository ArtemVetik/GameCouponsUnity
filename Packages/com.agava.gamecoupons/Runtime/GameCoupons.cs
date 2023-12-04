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

        private static LoginResponse _loginData;

        public static async Task<bool> Login(int id, string password, Action<string> onErrorCallback = null)
        {
            var postData = JsonUtility.ToJson(new LoginRequest() { id = id, password = password });
            using var request = UnityWebRequest.Post($"{BaseAddress}/api/v1/login", postData, "application/json");

            await request.SendWebRequest();

            _loginData = Parse<LoginResponse>(request, onErrorCallback);

            return _loginData.access != null;
        }

        public static async Task<CouponIssuanceResponse> CouponIssuance(CouponIssuanceRequest requestData, Action<string> onErrorCallback = null)
        {
            Debug.Log(JsonConvert.SerializeObject(requestData));
            using var request = UnityWebRequest.Post($"{BaseAddress}/api/v1/coupon_issuance", JsonConvert.SerializeObject(requestData), "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {_loginData.access}");

            await request.SendWebRequest();

            return Parse<CouponIssuanceResponse>(request, onErrorCallback);
        }

        public static async Task<GamesResponse> GetGames(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/games?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {_loginData.access}");

            await request.SendWebRequest();

            return Parse<GamesResponse>(request, onErrorCallback);
        }

        public static async Task<GamesResponse.Item> AddGame(string name, int[] genreIds, int[] platformIds, Action<string> onErrorCallback = null)
        {
            var postData = new AddGameResponse()
            {
                name = name,
                genre_ids = genreIds,
                platform_ids = platformIds,
            };

            using var request = UnityWebRequest.Post($"{BaseAddress}/api/v1/games", JsonConvert.SerializeObject(postData), "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {_loginData.access}");

            await request.SendWebRequest();

            return Parse<GamesResponse.Item>(request, onErrorCallback);
        }

        public static async Task<GenresResponse> Genres(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/genres?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {_loginData.access}");

            await request.SendWebRequest();

            return Parse<GenresResponse>(request, onErrorCallback);
        }

        public static async Task<OrganizationsResponse> Organizations(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/organizations?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {_loginData.access}");

            await request.SendWebRequest();

            return Parse<OrganizationsResponse>(request, onErrorCallback);
        }

        public static async Task<PlatformsResponse> Platforms(int page = 1, int size = 50, Action<string> onErrorCallback = null)
        {
            using var request = UnityWebRequest.Get($"{BaseAddress}/api/v1/platforms?page={page}&size={size}");
            request.SetRequestHeader("Authorization", $"Bearer {_loginData.access}");

            await request.SendWebRequest();

            return Parse<PlatformsResponse>(request, onErrorCallback);
        }

        public static async Task<bool> Refresh(Action<string> onErrorCallback = null)
        {
            var postData = $"{{\"refresh\": \"{_loginData.refresh}\"}}";
            using var request = UnityWebRequest.Post($"{BaseAddress}/api/v1/refresh", postData, "application/json");

            await request.SendWebRequest();

            var response = Parse<RefreshResponse>(request, onErrorCallback);

            if (response.access == null)
                return false;

            _loginData.access = response.access;
            return true;
        }

        private static T Parse<T>(UnityWebRequest request, Action<string> onErrorCallback) where T : struct
        {
            if (request.result != UnityWebRequest.Result.Success)
            {
                onErrorCallback?.Invoke($"{request.error}\n{request.downloadHandler.text}");
                return default;
            }

            if (request.responseCode == 200)
                return JsonUtility.FromJson<T>(request.downloadHandler.text);

            onErrorCallback?.Invoke($"Response code: {request.responseCode}");
            return default;
        }
    }
}
