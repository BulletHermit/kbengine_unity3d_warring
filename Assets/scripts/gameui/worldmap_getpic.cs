using UnityEngine;
using KBEngine;
using System;
using System.Collections;

public class worldmap_getpic : MonoBehaviour {

	public static UITexture obj = null;
	public static worldmap_getpic inst = null;
	static bool started = false;
	
	void Awake ()     
	{
		inst = this;
	}
	
	// Use this for initialization
	void Start () {
		obj = GetComponent<UITexture>();
		NGUITools.SetActive(obj.gameObject, false);
	}
	
	public static void show()
	{
		if(inst == null)
			return;
		
		if(started == true)
			return;
		
		started = true;
		NGUITools.SetActive(obj.gameObject, true);
		loader.inst.StartCoroutine(inst.worldmap_getpic_getFullTexture());
	}

	public static void hide()
	{
		if(inst == null)
			return;
		
		NGUITools.SetActive(obj.gameObject, false);
	}
	
	IEnumerator worldmap_getpic_getFullTexture()
	{
		Common.DEBUG_MSG("worldmap_getpic_getFullTexture::getFullTexture: curr=" + obj.mainTexture.name);
		 if(obj.mainTexture.name != loader.inst.currentSceneName)
		{
			System.Random Random1 = new System.Random();
			string path = "/ui/maps/" + loader.inst.currentSceneName + ".jpg";
			Common.DEBUG_MSG("worldmap_getpic_getFullTexture::getFullTexture: starting download(" + Common.safe_url(path) + ") backgroundpic! curr=" + obj.mainTexture.name);
			
			WWW www = new WWW(Common.safe_url(path));  
			yield return www; 
			
	        if (www.error != null)  
	            Common.ERROR_MSG(www.error);  
			else if(www.texture != null)
			{
				obj.mainTexture = www.texture;
				obj.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
			}
			
			Common.DEBUG_MSG("worldmap_getpic_getFullTexture::getFullTexture: download backgroundpic is finished!");
		}
		
		started = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
