using System;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private float readPeriod = 1f;
        private TMP_Text _fpsText;
        private float _time;
        private int _frameCount;

        private void Reset() => FetchComponents();

        private void Awake() => FetchComponents();

        private void FetchComponents() => _fpsText ??= GetComponent<TMP_Text>();

        private void Update()
        {
            _time += Time.deltaTime;
            _frameCount++;

            if (_time >= readPeriod)
            {
                int frameRate = Mathf.RoundToInt(_frameCount / _time);
                _fpsText?.SetText($"{frameRate} FPS");

                _time -= readPeriod;
                _frameCount = 0;
            }
        }
    }
}

