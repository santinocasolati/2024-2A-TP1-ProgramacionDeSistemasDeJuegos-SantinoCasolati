using Enemies;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InstancesCounter<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private string textFormat = "Enemies: {0}";

        private void Awake()
        {
            text ??= GetComponent<TMP_Text>();
            if (text == null) enabled = false;
        }

        private void Update()
        {
            //Is this the most efficient way to search for these references??
            var count = FindObjectsOfType<T>().Length;
            text.SetText(string.Format(textFormat, count));
        }
    }
}