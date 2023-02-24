using Assets.Scripts.UniversalComponents;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class HealthBarWidget : MonoBehaviour
    {
        [SerializeField] private Image _bar;
        [SerializeField] private HealthComponent _hp;

        private int _maxHp;

        private void Start()
        {
            _maxHp = _hp.Health;
            OnHpChanged(_maxHp);

            _hp.OnHpChanged += OnHpChanged;
        }

        private void OnHpChanged(int hp)
        {
            var progress = (float) hp / _maxHp;
            _bar.fillAmount = progress;
        }

        private void OnDestroy()
        {
            _hp.OnHpChanged -= OnHpChanged;
        }
    }
}