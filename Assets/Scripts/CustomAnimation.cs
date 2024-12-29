using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//--------------------------------------------\\
//-------------COPYRIGHT----------------------\\
//----OWNER:-BREAKARRAY-DEVELOPMENT-----------\\
//----WRITTEN-IN-VISUAL-STUDIO-2020-----------\\
//----COPYRIGHT-BREAKARRAY--------------------\\
//----CLASS:-CustomAnimation.cs------------------\\
//----PROJECT:-#PROJECTNAME#------------------\\
//----DEV-NAME:-BOZIDAR-BARBARIC--------------\\
//----JOB:-LEAD-DEV-CEO-----------------------\\
//--------------------------------------------\\
public enum ComponentType {
	anchoredPositionX,
	anchoredPositionY,
	sizeDeltaX,
	sizeDeltaY,
	anchorMinX,
	anchorMinY,
	anchorMaxX,
	anchorMaxY,
	pivotX,
	pivotY,
	rotationX,
	rotationY,
	rotationZ,
	scaleX,
	scaleY,
	scaleZ,
	alpha,
	vertspace
}

[System.Serializable]
public class ComponentRectAnimation {
	public ComponentType component;
	public AnimationCurve behaviour;
}

[System.Serializable]
public class ObjectRectAnimation {
	public GameObject refObject;
	[SerializeField]
	public List<ComponentRectAnimation> components;
}

public class CustomAnimation : MonoBehaviour{

	public bool forceStopAnimation = false;
	public bool shouldUpdateLayoutEveryFrame = false;
	public bool dontupdatelayout = false;
	public bool playAnimation;
	[SerializeField]
	public List<ObjectRectAnimation> objects;
	public float animationSpeed;
	[Range(0f, 1f)]
	public float animationProgressValue;
	bool updatinglayout = false;
	//bool isAnimationOver;
	float oldAnimProgVal;
	public bool playOnce;

	List<RectTransform> rts = new List<RectTransform>();
	List<CanvasGroup> cgs = new List<CanvasGroup>();
	List<VerticalLayoutGroup> vlgs = new List<VerticalLayoutGroup>();

	bool isset = false;

	private void Start() {
		LoadObjects();
	}
	void LoadObjects() {
		for(int i=0; i<objects.Count; i++ ) {
			if(objects[i] == null ) {
				rts.Add(null);
				cgs.Add(null);
				vlgs.Add(null);
				continue;
			}
			if( objects[i].refObject == null ) {
				rts.Add(null);
				cgs.Add(null);
				vlgs.Add(null);
				continue;
			}
			if ( objects[i].refObject.GetComponent<RectTransform>() != null ) {
				rts.Add(objects[i].refObject.GetComponent<RectTransform>());
			}
			else {
				rts.Add(null);
			}
			if ( objects[i].refObject.GetComponent<CanvasGroup>() != null ) {
				cgs.Add(objects[i].refObject.GetComponent<CanvasGroup>());
			}
			else {
				cgs.Add(null);
			}
			if ( objects[i].refObject.GetComponent<VerticalLayoutGroup>() != null ) {
				vlgs.Add(objects[i].refObject.GetComponent<VerticalLayoutGroup>());
			}
			else {
				vlgs.Add(null);
			}
		}
		isset = true;
	}

	private void OnValidate() {
		if ( animationProgressValue != oldAnimProgVal ) {
			RefreshAnimation();
			oldAnimProgVal = animationProgressValue;
		}
	}

	public void SetValue(float f) {
		animationProgressValue = Mathf.Clamp01(f);
		RefreshAnimation();
	}

	void RefreshAnimation() {//Next step.. make these vars global

		if ( !isset ){
			LoadObjects(); 
			return; 
		}
		float anchoredPositionX = 0;
		float anchoredPositionY = 0;
		float sizeDeltaX = 0;
		float sizeDeltaY = 0;
		float anchorMinX = 0;
		float anchorMinY = 0;
		float anchorMaxX = 0;
		float anchorMaxY = 0;
		float pivotX = 0;
		float pivotY = 0;
		float rotationX = 0;
		float rotationY = 0;
		float rotationZ = 0;
		float scaleX = 0;
		float scaleY = 0;
		float scaleZ = 0;
		float alphav = 0;
		float spacebetween = 0;

		bool isAP = false;
		bool isSD = false;
		bool isAMn = false;
		bool isAMx = false;
		bool isPV = false;
		bool isRT = false;
		bool isSC = false;
		bool isA = false;
		bool isS = false;
		for ( int i = 0; i < objects.Count; i++ ) {

			if (rts[i] != null)
			{
				anchoredPositionX = rts[i].anchoredPosition.x;
				anchoredPositionY = rts[i].anchoredPosition.y;
				sizeDeltaX = rts[i].sizeDelta.x;
				sizeDeltaY = rts[i].sizeDelta.y;
				anchorMinX = rts[i].anchorMin.x;
				anchorMinY = rts[i].anchorMin.y;
				anchorMaxX = rts[i].anchorMax.x;
				anchorMaxY = rts[i].anchorMax.y;
				pivotX = rts[i].pivot.x;
				pivotY = rts[i].pivot.y;
				rotationX = rts[i].localEulerAngles.x;
				rotationY = rts[i].localEulerAngles.y;
				rotationZ = rts[i].localEulerAngles.z;
				scaleX = rts[i].localScale.x;
				scaleY = rts[i].localScale.y;
				scaleZ = rts[i].localScale.z;
			}
			if ( cgs[i] != null ) {
				alphav = cgs[i].alpha;
			}
			if ( vlgs[i] != null ) {
				spacebetween = vlgs[i].spacing;
			}
			isAP = false;
			isSD = false;
			isAMn = false;
			isAMx = false;
			isPV = false;
			isRT = false;
			isSC = false;
			isA = false;
			isS = false;

			foreach ( ComponentRectAnimation cra in objects[i].components ) {
				float tempVal = cra.behaviour.Evaluate(animationProgressValue);
				switch ( cra.component ) {
					case ComponentType.anchoredPositionX:
						anchoredPositionX = tempVal;
						isAP = true;
						break;
					case ComponentType.anchoredPositionY:
						anchoredPositionY = tempVal;
						isAP = true;
						break;
					case ComponentType.sizeDeltaX:
						sizeDeltaX = tempVal;
						isSD = true;
						break;
					case ComponentType.sizeDeltaY:
						sizeDeltaY = tempVal;
						isSD = true;
						break;
					case ComponentType.anchorMinX:
						anchorMinX = tempVal;
						isAMn = true;
						break;
					case ComponentType.anchorMinY:
						anchorMinY = tempVal;
						isAMn = true;
						break;
					case ComponentType.anchorMaxX:
						anchorMaxX = tempVal;
						isAMx = true;
						break;
					case ComponentType.anchorMaxY:
						anchorMaxY = tempVal;
						isAMx = true;
						break;
					case ComponentType.pivotX:
						pivotX = tempVal;
						isPV = true;
						break;
					case ComponentType.pivotY:
						pivotY = tempVal;
						isPV = true;
						break;
					case ComponentType.rotationX:
						rotationX = tempVal;
						isRT = true;
						break;
					case ComponentType.rotationY:
						rotationY = tempVal;
						isRT = true;
						break;
					case ComponentType.rotationZ:
						rotationZ = tempVal;
						isRT = true;
						break;
					case ComponentType.scaleX:
						scaleX = tempVal;
						isSC = true;
						break;
					case ComponentType.scaleY:
						scaleY = tempVal;
						isSC = true;
						break;
					case ComponentType.scaleZ:
						scaleZ = tempVal;
						isSC = true;
						break;
					case ComponentType.alpha:
						alphav = tempVal;
						isA = true;
						break;
					case ComponentType.vertspace:
						spacebetween = tempVal;
						isS = true;
						break;
					default:
						break;
				}
			}
			if ( rts[i] != null ) {
				if ( isAP ) rts[i].anchoredPosition = new Vector2(anchoredPositionX, anchoredPositionY);
				if ( isSD ) rts[i].sizeDelta = new Vector2(sizeDeltaX, sizeDeltaY);
				if ( isAMn ) rts[i].anchorMin = new Vector2(anchorMinX, anchorMinY);
				if ( isAMx ) rts[i].anchorMax = new Vector2(anchorMaxX, anchorMaxY);
				if ( isPV ) rts[i].pivot = new Vector2(pivotX, pivotY);
				if ( isRT ) rts[i].localEulerAngles = new Vector3(rotationX, rotationY, rotationZ);
				if ( isSC ) rts[i].localScale = new Vector3(scaleX, scaleY, scaleZ);
			}
			if ( cgs[i] != null ) {
				if ( isA ) cgs[i].alpha = alphav;
			}
			if ( vlgs[i] != null ) {
				if ( isS ) vlgs[i].spacing = spacebetween;
			}
		}
		if( shouldUpdateLayoutEveryFrame ){
			if ( !updatinglayout ) {
				StartCoroutine(UpdateCanvas());
			}
		}
	}
	/*
	void RefreshAnimationOld() {
		foreach ( ObjectRectAnimation ora in objects ) {
			RectTransform rc = null;
			CanvasGroup cg = null;
			VerticalLayoutGroup vlg = null;
			if(ora.refObject.GetComponent<RectTransform>() != null ) {
				rc = ora.refObject.GetComponent<RectTransform>();
			}
			if(ora.refObject.GetComponent<CanvasGroup>() != null ) {
				cg = ora.refObject.GetComponent<CanvasGroup>();
			}
			if(ora.refObject.GetComponent<VerticalLayoutGroup>() != null ) {
				vlg = ora.refObject.GetComponent<VerticalLayoutGroup>();
			}
			if(rc != null){
				float anchoredPositionX = rc.anchoredPosition.x;
				float anchoredPositionY = rc.anchoredPosition.y;
				float sizeDeltaX = rc.sizeDelta.x;
				float sizeDeltaY = rc.sizeDelta.y;
				float anchorMinX = rc.anchorMin.x;
				float anchorMinY = rc.anchorMin.y;
				float anchorMaxX = rc.anchorMax.x;
				float anchorMaxY = rc.anchorMax.y;
				float pivotX = rc.pivot.x;
				float pivotY = rc.pivot.y;
				float rotationX = rc.localEulerAngles.x;
				float rotationY = rc.localEulerAngles.y;
				float rotationZ = rc.localEulerAngles.z;
				float scaleX = rc.localScale.x;
				float scaleY = rc.localScale.y;
				float scaleZ = rc.localScale.z;

				bool isAP = false;
				bool isSD = false;
				bool isAMn = false;
				bool isAMx = false;
				bool isPV = false;
				bool isRT = false;
				bool isSC = false;

				foreach ( ComponentRectAnimation cra in ora.components ) {
					float tempVal = cra.behaviour.Evaluate(animationProgressValue);
					switch ( cra.component ) {
						case ComponentType.anchoredPositionX:
							anchoredPositionX = tempVal;
							isAP = true;
							break;
						case ComponentType.anchoredPositionY:
							anchoredPositionY = tempVal;
							isAP = true;
							break;
						case ComponentType.sizeDeltaX:
							sizeDeltaX = tempVal;
							isSD = true;
							break;
						case ComponentType.sizeDeltaY:
							sizeDeltaY = tempVal;
							isSD = true;
							break;
						case ComponentType.anchorMinX:
							anchorMinX = tempVal;
							isAMn = true;
							break;
						case ComponentType.anchorMinY:
							anchorMinY = tempVal;
							isAMn = true;
							break;
						case ComponentType.anchorMaxX:
							anchorMaxX = tempVal;
							isAMx = true;
							break;
						case ComponentType.anchorMaxY:
							anchorMaxY = tempVal;
							isAMx = true;
							break;
						case ComponentType.pivotX:
							pivotX = tempVal;
							isPV = true;
							break;
						case ComponentType.pivotY:
							pivotY = tempVal;
							isPV = true;
							break;
						case ComponentType.rotationX:
							rotationX = tempVal;
							isRT = true;
							break;
						case ComponentType.rotationY:
							rotationY = tempVal;
							isRT = true;
							break;
						case ComponentType.rotationZ:
							rotationZ = tempVal;
							isRT = true;
							break;
						case ComponentType.scaleX:
							scaleX = tempVal;
							isSC = true;
							break;
						case ComponentType.scaleY:
							scaleY = tempVal;
							isSC = true;
							break;
						case ComponentType.scaleZ:
							scaleZ = tempVal;
							isSC = true;
							break;
						default:
							break;
					}
				}
				if(isAP) rc.anchoredPosition = new Vector2(anchoredPositionX, anchoredPositionY);
				if(isSD) rc.sizeDelta = new Vector2(sizeDeltaX, sizeDeltaY);
				if(isAMn) rc.anchorMin = new Vector2(anchorMinX, anchorMinY);
				if(isAMx) rc.anchorMax = new Vector2(anchorMaxX, anchorMaxY);
				if(isPV) rc.pivot = new Vector2(pivotX, pivotY);
				if(isRT) rc.localEulerAngles = new Vector3(rotationX, rotationY, rotationZ);
				if(isSC) rc.localScale = new Vector3(scaleX, scaleY, scaleZ);
			}
			if ( cg != null ) {
				float alphav = cg.alpha;

				bool isA = false;

				foreach ( ComponentRectAnimation cra in ora.components ) {
					float tempVal = cra.behaviour.Evaluate(animationProgressValue);
					switch ( cra.component ) {
						case ComponentType.alpha:
							alphav = tempVal;
							isA = true;
							break;
						default:
							break;
					}
				}
				if ( isA ) cg.alpha = alphav;
			}
			if ( vlg != null ) {
				float spacebetween = vlg.spacing;

				bool isA = false;

				foreach ( ComponentRectAnimation cra in ora.components ) {
					float tempVal = cra.behaviour.Evaluate(animationProgressValue);
					switch ( cra.component ) {
						case ComponentType.vertspace:
							spacebetween = tempVal;
							isA = true;
							break;
						default:
							break;
					}
				}
				if ( isA ) vlg.spacing = spacebetween;
			}
		}
	}
	*/


	public void PlayAnimation() {
		if ( forceStopAnimation ) return;
		animationProgressValue = 0f;
		playAnimation = true; 
		StartCoroutine(UpdateCanvas());
	}

	void Update() {
		if ( playAnimation ) {
			animationProgressValue += Time.deltaTime * animationSpeed;
			if ( animationProgressValue > 1f ) {
				//isAnimationOver = true;
				playAnimation = false;
				animationProgressValue = 1f;
				if ( dontupdatelayout ) return;
				StartCoroutine(UpdateCanvas());
				/*if (playOnce != false)
				{
					gameObject.SetActive(false);
				}*/
			}
			RefreshAnimation();
		}
	}
	IEnumerator UpdateCanvas() {
		updatinglayout = true;
		yield return new WaitForEndOfFrame();
		foreach(RectTransform rt in rts ) {
			LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
		}
		Debug.Log("Updating Canvas On Object: " + this.gameObject.name);
		updatinglayout = false;
	}
	/*
	public void PlayEditor()
	{
		animationProgressValue += Time.deltaTime * animationSpeed;
		if (animationProgressValue > 1f)
		{
			isAnimationOver = true;
			playAnimation = false;
			animationProgressValue = 1f;
		}

	}*/
}

//--------------------------------------------\\
//-------------COPYRIGHT----------------------\\
//----OWNER:-BREAKARRAY-DEVELOPMENT-----------\\
//----WRITTEN-IN-VISUAL-STUDIO-2020-----------\\
//----COPYRIGHT-BREAKARRAY--------------------\\
//----CLASS:-CustomAnimation.cs------------------\\
//----PROJECT:-#PROJECTNAME#------------------\\
//----DEV-NAME:-BOZIDAR-BARBARIC--------------\\
//----JOB:-LEAD-DEV-CEO-----------------------\\
//--------------------------------------------\\