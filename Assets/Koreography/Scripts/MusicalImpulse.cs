//----------------------------------------------
//            	   Koreographer                 
//    Copyright © 2014-2020 Sonic Bloom, LLC    
//----------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SonicBloom.Koreo.Demos
{
	[RequireComponent(typeof(Rigidbody))]
	[AddComponentMenu("Koreographer/Demos/Musical Impulse")]
	public class MusicalImpulse : MonoBehaviour
	{

		
		[EventID]
		public string eventID;
		public float jumpSpeed = 3f;

		Renderer visuals;

		//Range on the X value the notes will randomly spawn.
		float xRange = 5;
		float yRange = 5;

		Vector3 spawnLocation;

		//Scaling values
		public float minScale = 0.5f;
		public float maxScale = 1f;

		//Starting value for the Lerp
		//static float counter = 0.0f;


		Rigidbody rigidbodyCom;

        private void Awake()
        {
			
			
        }

        void Start()
		{
			// Register for Koreography Events.  This sets up the callback.
			//Koreographer.Instance.RegisterForEvents(eventID, AddImpulse);

			visuals = GetComponent<Renderer>();
			Color c = visuals.material.color;
			c.a = 1f;
			visuals.material.color = c;


			rigidbodyCom = GetComponent<Rigidbody>();

			transform.position = RandomSpawnPos();

			
			StartCoroutine("MissedNote");

			

		}

        private void Update()
        {
			visuals = GetComponent<Renderer>();
			Color c = visuals.material.color;

			if (c.a == 0.5f)
			{
				Destroy(gameObject);
			}
		}


        IEnumerator MissedNote()
		{
			for (float f = 1f; f >= -0.05f; f -= 0.05f)
			{
				Color c = visuals.material.color;
				c.a = f;
				visuals.material.color = c;
				yield return new WaitForSeconds(0.1f);
				
			}
		}

		IEnumerator FadeOut()
		{
			for (float f = 1f; f >= -0.05f; f -= 0.05f)
			{
				Color c = visuals.material.color;
				c.a = f;
				visuals.material.color = c;
				yield return new WaitForSeconds(0.05f);

			}
		}

		public void OnMouseDown()
        {
			Destroy(gameObject);

		}

		

		void OnDestroy()
		{
			// Sometimes the Koreographer Instance gets cleaned up before hand.
			//  No need to worry in that case.
			if (Koreographer.Instance != null)
			{
				Koreographer.Instance.UnregisterForAllEvents(this);
			}
		}

		//void AddImpulse(KoreographyEvent evt)
		//{
		//RandomSpawnPos();

		// Add impulse by overriding the Vertical component of the Velocity.
		//Vector3 vel = rigidbodyCom.velocity;
		//vel.y = jumpSpeed;

		//rigidbodyCom.velocity = vel;
		//}

		Vector3 RandomSpawnPos()
		{
			return new Vector3(Random.Range(-xRange, xRange), Random.Range(-yRange, yRange));

		}


	}
}
