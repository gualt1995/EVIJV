using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Chatbot core class is intended to advance from Program # to a new Level of Artificial
/// Intelligence. Whereas Program # only gives the basic functionallity of parsing
/// AIML 1.0.1 files, Chatbot fills the gap between simple chat application
///	and advanced Gameprogramming, especially the use of artificial intelligent NPC's. 
/// </summary>
namespace Chatbot {
	/// <summary>
	/// Class to process Inner and Outer Triggers
	/// Inner triggers are things like feelings,
	/// needs e.g.
	/// Outer triggers are things bots see, hear,
	/// e.g.
	/// </summary>
	internal class NestedPlanner {
		// Reference to Chatbot.Core Instance
		private Chatbot.Core bot;
		// Holds Planner Gameobject
		private GameObject planner;
		// Motive Instance
		private Chatbot.Motives motives;
		// Current selected Motive
		public GameObject CurrentMotive;
		// Should next motive be selected?
		private bool SelectNextMotive;
		// Should only childs be selected
		// before continue
		private bool OnlySelectChildren;
		// The parent motive
		private Transform Parent;

		/// <summary>
		/// Initializes a new instance of the <see cref="Chatbot.Trigger"/> class.
		/// </summary>
		/// <param name="Chatbot">Chatbot.</param>
		public void Initialize(GameObject tmpbot,Chatbot.Core tmpbotcore,Chatbot.Motives tmpmotives) {
			// Get Chatbot.Core Instance
			this.bot = tmpbotcore;
			if(this.bot==null)
				Debug.LogWarning("Chatbot.Core instance not passed");
			// Throw error if no chatbot gameobject attached
			if(tmpbot==null)
				Debug.LogWarning("Chatbot Gameobject is not passed.");
			// Throw error if no Chatbot.Motives instance attached
			if(tmpmotives==null)
				Debug.LogWarning("Chatbot.Motives instance is not passed.");
			// Retrieve Chatbot.Motives instance
			motives = tmpmotives;
			// Get planner GameObject
			planner = tmpbot.GetComponentInChildren<Planner>().gameObject;
			// Throw error if no planner attached
			if(planner==null)
				Debug.LogWarning("To use chatbot you need to attatch Planner.cs script to the Planner GameObject and pass Planner GameObject to Chatbot. GameObject is empty.");
			// No parent selected
			Parent = null;
			// Next motive should be selected
			SelectNextMotive = true;
			// Look for all motives without children
			OnlySelectChildren = false;
		}


		/// <summary>
		/// Update per frame
		/// </summary>
		public void Update() {
			if (SelectNextMotive) {
				SelectNextMotive=false;
				SelectCurrentMotive();
			}
		}

		/// <summary>
		/// Gets the child transforms recursive.
		/// </summary>
		/// <param name="tmpTransform">Tmp transform.</param>
		/// <param name="tmpChildTransformList">Tmp child transform list.</param>
		void GetChildTransformsRecursive(Transform tmpTransform, List<Transform> tmpChildTransformList){
			// Check if tmpTransform and tmpChildTransformList passed
			if (tmpTransform!=null&&tmpChildTransformList!=null) {
				// Loop through transform instances
				foreach (Transform trans in tmpTransform) {
					// Case one: only select child transforms
					// of parent motive
					if(OnlySelectChildren&&trans.parent==Parent){
						tmpChildTransformList.Add(trans);
						// Recursive find all attached Subtransforms
						if(trans.childCount>0)
							GetChildTransformsRecursive(trans,tmpChildTransformList);
					// Case two: select all transforms
					} else {
						// If current trans is not root transform
						// this recursive function started of
						// (at the beginning it starts with planner)
						if (trans != tmpTransform) {
							// Add transform to list
							tmpChildTransformList.Add(trans);
							// Recursive find all attached Subtransforms
							// when children attatched
							if(trans.childCount>0)
								GetChildTransformsRecursive(trans,tmpChildTransformList);
						}
					}
				}
			}
		}

		/// <summary>
		/// Function to determinate next Motive to Trigger
		/// </summary>
		void SelectCurrentMotive() {
			float TotalExpectedTimeSpan=0.0f;
			List<Transform> ChildTransformList = new List<Transform>();
			// Retrieve all Subtransforms recursively
			GetChildTransformsRecursive (planner.transform,ChildTransformList);
			// Reset this setting to false for next prozessing
			OnlySelectChildren=false;
			// Loop all selected transforms to calculate
			// Whole absolute time needed by Motives
			foreach(Transform trans in ChildTransformList){
				// If transform exists
				if(trans) {
					// And has no childs attatched
					if(trans.childCount==0){
						Motive tmpmotive;
						// Get Motive instance
						tmpmotive=trans.gameObject.GetComponent<Motive>();
						// If existing
						if(tmpmotive){
							// Add TimeSpan in milliseconds
							TotalExpectedTimeSpan += tmpmotive.ExpectedTimeSpan;
						}
					}
				}
			}
			// Calculate relative time span (sum of all==1)
			// of all selected motives
			foreach(Transform trans in ChildTransformList){
				// If no attatched child motive
				if(trans.childCount==0){
					Motive tmpmotive;
					// Retrieve motive
					tmpmotive=trans.gameObject.GetComponent<Motive>();
					// If it exists and expected prozessing time of motive
					// is not zero 
					if(tmpmotive&&TotalExpectedTimeSpan!=0.0f){
						tmpmotive.RelativeExpectedTimeSpan = tmpmotive.ExpectedTimeSpan / TotalExpectedTimeSpan ;
					// Else set it to zero.
					// In later calculation it will be considered 
				    // as (1-reltimespan)=1.
					} else {
						tmpmotive.RelativeExpectedTimeSpan = 0.0f;
					}
				}
			}
			// No motive selected
			Motive tmpMotive=null;
			// Reset Score
			float tmpMotiveScore=0.0f;
			// Loop through selected motives
			foreach(Transform trans in ChildTransformList){
				// If transiton has no children and is thus
				// ready for prozessing
				if(trans.childCount==0){
					Motive tmpmotive2;
					// Get motive of trans
					tmpmotive2=trans.gameObject.GetComponent<Motive>();
					// If motive exists
					if(tmpmotive2){
						// If tmp motive is still zero
						if(!tmpMotive)
							// Assign the current motive to it, so it's not the
							// motive, that will be calculated next, if not replaced by
							// other motive with bigger score
							tmpMotive= tmpmotive2;
						// If this motive has better score than current best scoring motive
						if(tmpmotive2.Importance*tmpmotive2.Need*(1.0f-tmpmotive2.RelativeExpectedTimeSpan)>tmpMotiveScore){
							// Set this motive to be the next motive to calculate
							// Set Score
							tmpMotiveScore=tmpmotive2.Importance*tmpmotive2.Need*(1.0f-tmpmotive2.RelativeExpectedTimeSpan);
							// And the motive
							tmpMotive=tmpmotive2;
						}
					}
				}
			}
			// If a motive to proess was selected
			if (tmpMotive) {
				// Assign tmpMotive gameobject to CurrentMotive
				CurrentMotive = tmpMotive.gameObject;
				// And trigger the current motive
				TriggerMotive ();
			} else {
				// If no motive has been selected
				// Set current motive to zero
				CurrentMotive = null;
				// And try to grab next motive in following
				// prozessing
				SelectNextMotive=true;
			}
		}

		/// <summary>
		/// Triggers the CurrentMotive instance if exists.
		/// </summary>
		void TriggerMotive(){
			// Is CurrentMotive GameObject assigned?
			if(CurrentMotive)
				// Call motives helperfunction with CurrentMotive
				// Gameobject's name, thus it's important that 
				// Motive Gameobject remains same name when instanced.
				motives.Trigger (CurrentMotive.name);
		}

		/// <summary>
		/// Called when MotiveHelperfunction finishes prozessing
		/// </summary>
		public void Finished() {
			// If current motive attatched
			if (CurrentMotive) {
				// And motive exists
				if(CurrentMotive.GetComponent<Motive> ()){
					// If parent transform exists
					if(CurrentMotive.transform.parent!=null){
						// And has Motive instance attatched
						if(CurrentMotive.transform.parent.gameObject.GetComponent<Motive> ()!=null){
							// If children should be triggered before others
							if(CurrentMotive.transform.parent.gameObject.GetComponent<Motive> ().TriggerChildrenBeforeContinue){
								// Set appropriate value to true
								OnlySelectChildren = true;
								// And set Parent transform
								Parent = CurrentMotive.transform.parent;
							}
						}
					}
				}
				// Destroy current motive instance, as it is 
				// already processed
				UnityEngine.Object.Destroy(CurrentMotive);
				// Reset CurrentMotive to null
				CurrentMotive=null;
			}
			// Now we need to select next motive again
			SelectNextMotive = true;
		}
	}
}
