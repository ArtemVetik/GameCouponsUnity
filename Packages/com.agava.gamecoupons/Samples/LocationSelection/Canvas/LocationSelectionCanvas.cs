using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.GameCoupons.Samples.LocationSelection.Canvas
{
    public class LocationSelectionCanvas : MonoBehaviour
    {
        private const string ResourceName = nameof(LocationSelectionCanvas);

        [SerializeField] private CityButton _buttonTemplate;
        [SerializeField] private Transform _container;
        [SerializeField] private Button _closeButton;

        private Action _onClosed;

        private void OnEnable() => _closeButton.onClick.AddListener(OnCloseButtonClicked);

        private void OnDisable() => _closeButton.onClick.RemoveListener(OnCloseButtonClicked);

        public static void Create(string[] cities, Action<string> onSelected, Action onClosed)
        {
            var canvas = Resources.Load<LocationSelectionCanvas>(ResourceName);
            var canvasInstance = Instantiate(canvas);
            canvasInstance.Render(cities, onSelected, onClosed);
        }

        public static IEnumerator CreateAsync(string[] cities, Action<string> onSelected, Action onClosed)
        {
            var request = Resources.LoadAsync<LocationSelectionCanvas>(ResourceName);

            yield return request;
            
            var canvasInstance = Instantiate(request.asset as LocationSelectionCanvas);
            canvasInstance.Render(cities, onSelected, onClosed);
        }

        internal void Render(string[] cities, Action<string> onSelected, Action onClosed)
        {
            foreach (var city in cities)
            {
                var button = Instantiate(_buttonTemplate, _container);
                button.Render(city, (city) =>
                {
                    onSelected?.Invoke(city);
                    Destroy(gameObject);
                });
            }

            _onClosed = onClosed;
        }

        private void OnCloseButtonClicked()
        {
            _onClosed?.Invoke();
            Destroy(gameObject);
        }
    }
}
