using System;
using System.Collections;
using DG.Tweening;
using TaskArcher.UI;
using UnityEngine;

namespace TaskArcher.Coins
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D coinRigidbody;
        [SerializeField] private HudCoinPosition endPosition;
        [SerializeField] private float waitBeforeStartCollect = 2f;
        [SerializeField] private float moveDuration = 1f;
        [SerializeField] private float rotationFactor = 5f;
        [SerializeField] private float waitBeforeDestroyFactor = 2f;

        public static Action OnCoinCollected;

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
            yield return new WaitForSeconds(waitBeforeStartCollect);

            coinRigidbody.simulated = false;

            Vector3 position = endPosition.transform.position;
            
            transform.DOMoveX(position.x, moveDuration).SetEase(Ease.OutQuad);;
            transform.DOMoveY(position.y, moveDuration).SetEase(Ease.OutQuad);;
            transform.DORotate(position, moveDuration * rotationFactor, RotateMode.FastBeyond360);
            
            yield return new WaitForSeconds(moveDuration * waitBeforeDestroyFactor);
            
            OnCoinCollected?.Invoke();
            
            Destroy(gameObject);
        }
    }
}