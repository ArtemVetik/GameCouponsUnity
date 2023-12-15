using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.GameCoupons.Samples.LocationSelection.Canvas
{
    public class LocationSelectionCanvas : MonoBehaviour
    {
        private const string ResourceName = nameof(LocationSelectionCanvas);

        [SerializeField] private CityButton _buttonTemplate;
        [SerializeField] private Transform _container;
        [SerializeField] private Text _pageText;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _nextPageButton;
        [SerializeField] private Button _previousPageButton;

        private List<CityButton> _buttonInstances = new();

        public event Action<string> Selected;
        public event Action NextPageClicked;
        public event Action PreviousPageClicked;
        public event Action Closed;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
            _nextPageButton.onClick.AddListener(OnNextButonClicked);
            _previousPageButton.onClick.AddListener(OnPreviousButonClicked);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
            _nextPageButton.onClick.RemoveListener(OnNextButonClicked);
            _previousPageButton.onClick.RemoveListener(OnPreviousButonClicked);
        }

        public static LocationSelectionCanvas Create()
        {
            var canvas = Resources.Load<LocationSelectionCanvas>(ResourceName);
            return Instantiate(canvas);
        }

        public void Render(int page, string[] cities)
        {
            foreach (var button in _buttonInstances)
                Destroy(button.gameObject);

            _buttonInstances.Clear();

            _pageText.text = $"Page {page}";

            foreach (var city in cities)
            {
                var button = Instantiate(_buttonTemplate, _container);
                button.Render(city, (city) =>
                {
                    Selected?.Invoke(city);
                    Destroy(gameObject);
                });

                _buttonInstances.Add(button);
            }
        }

        private void OnCloseButtonClicked()
        {
            Closed?.Invoke();
            Destroy(gameObject);
        }

        private void OnNextButonClicked()
        {
            NextPageClicked?.Invoke();
        }

        private void OnPreviousButonClicked()
        {
            PreviousPageClicked?.Invoke();
        }
    }
}
