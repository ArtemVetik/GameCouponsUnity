using UnityEngine;
using UnityEngine.UI;
using Agava.GameCoupons.Samples.LocationSelection.Canvas;
using System;
using System.Linq;

namespace Agava.GameCoupons.Samples.LocationSelection
{
    public class OpenCitySelectionCanvas : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private Text _infoText;
        [SerializeField] private InputField _loginFuntionId;

        private void OnEnable() => _openButton.onClick.AddListener(OnOpenButtonClicked);

        private void OnDisable() => _openButton.onClick.RemoveListener(OnOpenButtonClicked);

        private async void OnOpenButtonClicked()
        {
            if (await GameCoupons.Login(_loginFuntionId.text) == false)
                throw new InvalidOperationException("Failed to login!");

            var cities = await GameCoupons.CityList("ru", (error) => throw new InvalidOperationException(error));
            var names = cities.Data.Select(x => x.Name).ToArray();
            Array.Sort(names);

            LocationSelectionCanvas.Create(names,
                onSelected: (city) =>
                {
                    var cityData = cities.Data.First(x => x.Name == city);
                    _infoText.text = $"{cityData.Name} ({cityData.Longitude};{cityData.Latitude})";
                },
                onClosed: () =>
                {
                    _infoText.text = "Closed!";
                }
            );
        }
    }
}
