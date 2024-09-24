using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class ToggleSound : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] private bool toggle;
        [SerializeField] private AudioMixerSnapshot defaultSnapshot;
        [SerializeField] private AudioMixerSnapshot mutedSnapshot;
        [SerializeField] private float transitionDuration = .25f;
        private Button _button;

        private void Reset() => FetchComponents();

        private void Awake() => FetchComponents();

        private void FetchComponents() => _button ??= GetComponent<Button>();

        private void OnEnable()
        {
            _button?.onClick.AddListener(HandleClick);
        }

        private void HandleClick()
        {
            var snapshot = toggle ? defaultSnapshot : mutedSnapshot;
            // "a?.Method()" is equivalent to "if(a != null) { a.Method() }"
            snapshot?.TransitionTo(transitionDuration);
        }
    }
}
