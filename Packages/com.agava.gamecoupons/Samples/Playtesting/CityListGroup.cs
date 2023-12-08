using UnityEngine;
using UnityEngine.UI;

namespace Agava.GameCoupons.Samples.Playtesting
{
    public class CityListGroup : MonoBehaviour
    {
        [SerializeField] private Button _cilyListButton;

        private void OnEnable()
        {
            _cilyListButton.onClick.AddListener(OnCityListButtonClicked);
        }

        private void OnDisable()
        {
            _cilyListButton.onClick.RemoveListener(OnCityListButtonClicked);
        }

        private async void OnCityListButtonClicked()
        {
            var cityList = await GameCoupons.CityList((error) => Debug.LogError(error));

            if (cityList == null)
                return;

            foreach (var city in cityList.Data)
                Debug.Log($"City: {city.Name} ({city.Longitude}; {city.Latidute})");
        }
    }
}
