using UnityEngine;

namespace TaskArcher.Scripts.Weapons
{
    public class Trajectory : MonoBehaviour
    {
        [SerializeField] private RangeWeapon weapon;
        [SerializeField] private SpriteRenderer pointPrefab;
        [SerializeField] private Transform pointsParent;
        [SerializeField] private float minDistanceBetweenPoints;
        [SerializeField] private float maxDistanceBetweenPoints;
        [SerializeField] private int numberOfPoints;

        [SerializeField] private float pointMaxScale;
        [SerializeField] private float pointMinScale;

        [SerializeField] private Animator nestedFadeGroupAnimator;

        public Transform PointsParent => pointsParent;
        
        private float _distanceBetweenPoints;
        private SpriteRenderer[] _trajectoryPoints;
        
        private void OnEnable()
        {
            CreateTrajectoryPoints();
        }

        private void Update()
        {
            CalculateTrajectory();
        }

        private void CreateTrajectoryPoints()
        {
            _trajectoryPoints = new SpriteRenderer[numberOfPoints];

            var scaleStep = (pointMaxScale - pointMinScale) / numberOfPoints;
            var nextScale = pointMaxScale;
            
            for (int i = 0; i < numberOfPoints; i++)
            {
                nextScale -= scaleStep;
                var scale = Vector3.one * nextScale;
                
                _trajectoryPoints[i] = Instantiate(pointPrefab, pointsParent);
                
                _trajectoryPoints[i].transform.localScale = scale;
            } 
        }

        private void CalculateTrajectory()
        {
            CalculateDistanceBetweenPoints();
                
            for (int i = 0; i < _trajectoryPoints.Length; i++)
            {
                _trajectoryPoints[i].transform.position = 
                    CalculatePointPosition(_distanceBetweenPoints / 4 + i * _distanceBetweenPoints);
            }
        }

        private Vector2 CalculatePointPosition(float distance)
        {
            Vector2 position = (Vector2)weapon.BulletStartTransform.position + (Vector2)weapon.BulletStartTransform.right.normalized
                * (weapon.Force * distance) + Physics2D.gravity * (distance * distance * 0.5f);
            
            return position;
        }

        private void CalculateDistanceBetweenPoints()
        {
            weapon.CalculateForce();
            
            var calcDistance = minDistanceBetweenPoints * Mathf.Sqrt(weapon.Force);

            if (calcDistance > maxDistanceBetweenPoints)
            {
                calcDistance = maxDistanceBetweenPoints;
            }
            
            if (calcDistance < minDistanceBetweenPoints)
            {
                calcDistance = minDistanceBetweenPoints;
            }

            _distanceBetweenPoints = calcDistance;
        }

        public void ShowTrajectoryInTime()
        {
            /*Debug.Log("ShowTrajectoryInTime");*/
            PointsParent.gameObject.SetActive(true);
            nestedFadeGroupAnimator.CrossFade("Show", 0);
        }

        private void HideParent()
        {
            PointsParent.gameObject.SetActive(false);
        }
        
        public void HideTrajectory()
        {
            /*Debug.Log("HideTrajectory");*/
            nestedFadeGroupAnimator.CrossFade("Hide", 0);
            Invoke(nameof(HideParent), 0.07f );
        } 
    }
}
