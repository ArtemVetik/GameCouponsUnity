using UnityEngine;
using UnityEngine.UI;
using Agava.GameCoupons.Samples.LocationSelection.Canvas;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Agava.GameCoupons.Samples.LocationSelection
{
    public class OpenCitySelectionCanvas : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private Text _infoText;
        [SerializeField] private InputField _loginFuntionId;

        private LocationSelectionCanvas _locationCanvas;
        private LocationsResponse _lastResponse;
        private int _page = 1;

        private void OnEnable() => _openButton.onClick.AddListener(OnOpenButtonClicked);

        private void OnDisable() => _openButton.onClick.RemoveListener(OnOpenButtonClicked);

        private async void OnOpenButtonClicked()
        {
            if (GameCoupons.Authorized == false)
            {
                if (await GameCoupons.Login(_loginFuntionId.text) == false)
                    throw new InvalidOperationException("Failed to login!");
            }

            _locationCanvas = LocationSelectionCanvas.Create();

            _locationCanvas.Selected += OnLocationSelected;
            _locationCanvas.NextPageClicked += OnNextPageClicked;
            _locationCanvas.PreviousPageClicked += OnPreviousPageClicked;
            _locationCanvas.Closed += OnLocationCanvasClosed;

            await Render();
        }

        private async Task Render()
        {
            var request = new LocationsRequest()
            {
                CountryCode = "RU",
                LanguageCode = "ru",
                Page = _page,
                Size = 50,
            };

            _lastResponse = await GameCoupons.Locations(request, (error) => throw new InvalidOperationException(error));
            var cities = _lastResponse.items.Select(item => item.localized_name).ToArray();
            _locationCanvas.Render(_page, cities);
        }

        private void OnLocationSelected(string city)
        {
            var cityData = _lastResponse.items.First(item => item.localized_name == city);
            _infoText.text = $"{cityData.localized_name} ({cityData.longitude};{cityData.latitude})";
        }

        private async void OnNextPageClicked()
        {
            _page = Mathf.Clamp(_page + 1, 1, _lastResponse.pages);
            await Render();
        }

        private async void OnPreviousPageClicked()
        {
            _page = Mathf.Clamp(_page - 1, 1, _lastResponse.pages);
            await Render();
        }

        private void OnLocationCanvasClosed()
        {
            _infoText.text = "Closed!";
            _locationCanvas = null;
            _lastResponse = null;
            _page = 1;
        }
    }
}
