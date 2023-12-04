using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.GameCoupons.Samples.Playtesting
{
    public class CouponIssuanceGroup : MonoBehaviour
    {
        [SerializeField] private InputField _longitude;
        [SerializeField] private InputField _latitude;
        [SerializeField] private InputField _gameId;
        [SerializeField] private InputField _platforms;
        [SerializeField] private InputField _genres;
        [SerializeField] private InputField _organizations;
        [SerializeField] private Button _requestButton;

        private void OnEnable()
        {
            _requestButton.onClick.AddListener(OnRequestButtonClicked);
        }

        private void OnDisable()
        {
            _requestButton.onClick.RemoveListener(OnRequestButtonClicked);
        }

        private async void OnRequestButtonClicked()
        {
            var genres = string.IsNullOrEmpty(_genres.text) ? new int[0] : _genres.text.Split(',').Select(genre => int.Parse(genre)).ToArray();
            var platforms = string.IsNullOrEmpty(_platforms.text) ? new int[0] : _platforms.text.Split(',').Select(platform => int.Parse(platform)).ToArray();
            var exludeOrganizations = string.IsNullOrEmpty(_organizations.text) ? new int[0] : _organizations.text.Split(',').Select(organization => int.Parse(organization)).ToArray();

            var request = new CouponIssuanceRequest()
            {
                longitude = int.Parse(_longitude.text),
                latitude = int.Parse(_latitude.text),
                game_id = int.Parse(_gameId.text),
                platform_ids = platforms,
                genre_ids = genres,
                exclude_organization_ids = exludeOrganizations
            };

            var coupon = await GameCoupons.CouponIssuance(request, (error) => Debug.LogError(error));

            if (coupon != null)
                Debug.Log($"Coupon: {JsonUtility.ToJson(coupon)}");
        }
    }
}
