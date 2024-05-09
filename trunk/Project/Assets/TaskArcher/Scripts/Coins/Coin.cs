using System;
using System.Collections;
using DG.Tweening;
using TaskArcher.Scripts.UI;
using UnityEngine;

namespace TaskArcher.Coins
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D coinRigidbody;
        [SerializeField] private HudCoinPosition endPosition;
        [SerializeField] private float moveDuration = 1f;
        
        public static Action onCoinCollected;

        private void OnEnable()
        {
            endPosition = FindObjectOfType<HudCoinPosition>();

            CollectCoin();
        }

        private void CollectCoin()
        {
            StartCoroutine(CollectCoinCoroutine());
        }
        
        private IEnumerator CollectCoinCoroutine()
        {
            yield return new WaitForSeconds(2f);

            coinRigidbody.simulated = false;

            var position = endPosition.transform.position;
            
            transform.DOMoveX(position.x, moveDuration).SetEase(Ease.OutQuad);;
            transform.DOMoveY(position.y, moveDuration).SetEase(Ease.OutQuad);;
            transform.DORotate(position, moveDuration * 5, RotateMode.FastBeyond360);
            
            yield return new WaitForSeconds(moveDuration * 2);
            
            onCoinCollected?.Invoke();
            
            Destroy(gameObject);
        }
    }
}