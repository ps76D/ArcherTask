using TaskArcher.Coins;
using TMPro;
using UnityEngine;

namespace TaskArcher.Scripts.UI
{
    public class HudPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text coinAmountText;
 
        private int _coinsAmount;

        private void OnEnable()
        {
            Coin.onCoinCollected += IncreaseCoinsAmountText;
            
            UpdateCoinsAmountText();
        }

        private void IncreaseCoinsAmountText()
        {
            _coinsAmount++;

            UpdateCoinsAmountText();
        }

        private void UpdateCoinsAmountText()
        {
            coinAmountText.text = _coinsAmount.ToString();
        }
    }
}
