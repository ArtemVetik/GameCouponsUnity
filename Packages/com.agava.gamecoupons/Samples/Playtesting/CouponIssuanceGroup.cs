using UnityEngine;
using UnityEngine.UI;

namespace Agava.GameCoupons.Samples.Playtesting
{
    public class CouponIssuanceGroup : MonoBehaviour
    {
        [SerializeField] private InputField _longitude;
        [SerializeField] private InputField _latitude;
        [SerializeField] private InputField _gameId;
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
            var coupon = await GameCoupons.CouponIssuance(int.Parse(_longitude.text), int.Parse(_latitude.text), int.Parse(_gameId.text), (error) => Debug.LogError(error));

            if (coupon != null)
                Debug.Log($"Coupon: {JsonUtility.ToJson(coupon)}");
        }
    }
}
