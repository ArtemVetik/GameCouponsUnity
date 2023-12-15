using UnityEngine;
using UnityEngine.UI;

namespace Agava.GameCoupons.Samples.Playtesting
{
    public class LocationsGroup : MonoBehaviour
    {
        [SerializeField] private Button _locationsButton;
        [SerializeField] private InputField _nameInput;
        [SerializeField] private InputField _countryCodeInput;
        [SerializeField] private InputField _languageCodeInput;
        [SerializeField] private InputField _pageInput;
        [SerializeField] private InputField _sizeInput;

        private void OnEnable()
        {
            _locationsButton.onClick.AddListener(OnCityListButtonClicked);
        }

        private void OnDisable()
        {
            _locationsButton.onClick.RemoveListener(OnCityListButtonClicked);
        }

        private async void OnCityListButtonClicked()
        {
            var request = new LocationsRequest()
            {
                Name = _nameInput.text,
                CountryCode = _countryCodeInput.text,
                LanguageCode = _languageCodeInput.text,
                Page = int.Parse(_pageInput.text),
                Size = int.Parse(_sizeInput.text),
            };

            var locations = await GameCoupons.Locations(request, (error) => Debug.LogError(error));

            if (locations != null)
                Debug.Log($"Organizations: {JsonUtility.ToJson(locations)}");
        }
    }
}
