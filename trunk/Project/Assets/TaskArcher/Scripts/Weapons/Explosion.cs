using UnityEngine;

namespace TaskArcher.Weapons
{
	public class Explosion : MonoBehaviour
	{
		[SerializeField] private Collider2D[] results = new Collider2D[]{};
		
		public void Explode(RaycastHit2D raycastHit2D, float radius, float power)
		{
			Vector3 pos = raycastHit2D.point;

			results = Physics2D.OverlapCircleAll(pos, radius);

			foreach (Collider2D col in results) {
				
				Rigidbody2D rigidBody = col.GetComponent<Rigidbody2D>();
				if (rigidBody) 
				{
					rigidBody.AddExplosionForce2D(raycastHit2D.point,power, radius);
				}
			}
		}
	}
}
