using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Agava.GameCoupons.Samples.Playtesting
{
    public class AddGameGroup : MonoBehaviour
    {
        [SerializeField] private InputField _name;
        [SerializeField] private InputField _genres;
        [SerializeField] private InputField _platforms;
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

            var addGameResponse = await GameCoupons.AddGame(_name.text, genres, platforms, (error) => Debug.Log(error));

            Debug.Log($"AddGame: {JsonUtility.ToJson(addGameResponse)}");
        }
    }
}
