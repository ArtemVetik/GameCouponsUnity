using UnityEngine;
using UnityEngine.UI;

namespace Agava.GameCoupons.Samples.Playtesting
{
    public class RefreshGroup : MonoBehaviour
    {
        [SerializeField] private Button _refreshButton;

        private void OnEnable()
        {
            _refreshButton.onClick.AddListener(OnRefreshButtonClicked);
        }

        private void OnDisable()
        {
            _refreshButton.onClick.RemoveListener(OnRefreshButtonClicked);
        }

        private async void OnRefreshButtonClicked()
        {
            var success = await GameCoupons.Refresh((error) => Debug.Log(error));

            if (success)
                Debug.Log($"Access token successfully updated");
        }
    }
}
