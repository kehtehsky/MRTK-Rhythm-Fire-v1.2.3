using UnityEngine;
using System.Collections;

namespace EpicToonFX
{

    public class ETFXTarget : MonoBehaviour
    {
        

        [Header("Effect shown on target hit")]
	    public GameObject hitParticle;
        [Header("Effect shown on target respawn")]
	    public GameObject respawnParticle;
	    public Renderer targetRenderer;
	    public Collider targetCollider;

        private GameManagerScript gameManager;

      
        void Start()
        {
		    targetRenderer = GetComponent<Renderer>();
		    targetCollider = GetComponent<Collider>();

            gameManager = GameObject.Find("GameManager").GetComponent<GameManagerScript>();
        
            //transform.position = RandomSpawnPos();

            StartCoroutine("MissedNote");

            Destroy(gameObject, 2f);
        }

        private void Update()
        {
            targetRenderer = GetComponent<Renderer>();
            Color c = targetRenderer.material.color;

            
        }

        public void SpawnTarget()
        {
            targetRenderer = GetComponent<Renderer>();
            targetCollider = GetComponent<Collider>();

            transform.position = RandomSpawnPos();

            

            
            targetRenderer.enabled = true; //Shows the target
		    targetCollider.enabled = true; //Enables the collider
		    GameObject respawnEffect = Instantiate(respawnParticle, transform.position, transform.rotation) as GameObject; //Spawns attached respawn effect
		    Destroy(respawnEffect, 3.5f); //Removes attached respawn effect after x seconds

            

        }

        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Missile") // If collider is tagged as missile
            {
                if (hitParticle)
                {
				    //Debug.Log("Target hit!");
				    GameObject destructibleEffect = Instantiate(hitParticle, transform.position, transform.rotation) as GameObject; // Spawns attached hit effect
				    Destroy(destructibleEffect, 2f); // Removes hit effect after x seconds
				    targetRenderer.enabled = false; // Hides the target
				    targetCollider.enabled = false; // Disables target collider
				    StartCoroutine(Respawn()); // Sets timer for respawning the target
                    gameManager.UpdateScore(1);
                }
            }
        }

        IEnumerator MissedNote()
        {
            for (float f = 1f; f >= -0.05f; f -= 0.05f)
            {
                Color c = targetRenderer.material.color;
                c.a = f;
                targetRenderer.material.color = c;
                yield return new WaitForSeconds(0.1f);

            }
        }

        IEnumerator Respawn()
        {
            yield return new WaitForSeconds(2);
		    SpawnTarget();
        }

        Vector3 RandomSpawnPos()
        {
            return new Vector3(Random.Range(-2f, 2f), Random.Range(-0.5f, 1.5f), 5);

        }
    }
}