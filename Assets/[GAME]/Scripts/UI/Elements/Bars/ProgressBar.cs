using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _fill;
    [SerializeField] private float _maxSpeedFill = 0.5f;
    [SerializeField] private float _minSpeedFill = 0.03f;

    protected float _currentValue;
    protected float _initialValue;

    protected virtual void Update()
    {
        if (_fill.fillAmount != _currentValue)
        {
            var t = Mathf.InverseLerp(_initialValue, _currentValue, _fill.fillAmount);
            var step = Time.smoothDeltaTime * Mathf.Lerp(_maxSpeedFill, _minSpeedFill, t);

            _fill.fillAmount =
                Mathf.MoveTowards(_fill.fillAmount, _currentValue, step);
        }
    }
}