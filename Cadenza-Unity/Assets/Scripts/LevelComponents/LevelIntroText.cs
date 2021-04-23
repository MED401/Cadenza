using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LevelComponents
{
    public class LevelIntroText : MonoBehaviour
    {
        [SerializeField] private float waitUntilFade = 4;
        [SerializeField] private float fadeSpeed = 2f;

        private Text _childText;
        private bool _isFading;
        private Text _text;
        private string _title;

        private void Start()
        {
            _title = SceneManager.GetActiveScene().name;
            _text = GetComponent<Text>();
            _childText = _text.transform.GetChild(0).GetComponent<Text>();
            _text.text = _title;
        }

        private void Update()
        {
            if (_isFading) return;

            StartCoroutine(FadeText());
        }

        private IEnumerator FadeText()
        {
            _isFading = true;

            yield return new WaitForSeconds(waitUntilFade);

            while (_text.color.a > 0.0f)
            {
                _text.color = new Color(_text.color.r, _text.color.b, _text.color.g,
                    _text.color.a - Time.deltaTime / fadeSpeed);
                _childText.color = new Color(_childText.color.r, _childText.color.b, _childText.color.g,
                    _childText.color.a - Time.deltaTime / fadeSpeed);

                yield return null;
            }

            Destroy(gameObject);
        }
    }
}