using TaskArcher.Coins;
using TMPro;
using UnityEngine;

namespace TaskArcher.UI
{
    public class HudPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text coinAmountText;
 
        private int _coinsAmount;

        private void OnEnable()
        {
            Coin.OnCoinCollected += IncreaseCoinsAmountText;
            
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
