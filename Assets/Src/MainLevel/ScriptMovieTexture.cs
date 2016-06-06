using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ScriptMovieTexture : MonoBehaviour {

	private MovieTexture video = new MovieTexture();
	private AudioSource audio = new AudioSource();

	// Use this for initialization
	void Start () {
		GetComponent<Image> ().enabled = false;

	}

	void Update()
	{
		if (video != null) {
			if (video.isPlaying)
				GetComponent<Image> ().enabled = true;
			else 
				GetComponent<Image> ().enabled = false;
		}
			
	}
	
	public void playVideo( string name )
	{
		if (video != null)
			video.Stop ();
		video = (MovieTexture)Resources.Load("textures/movieTextures/"+name);
		GetComponent<Image> ().material.mainTexture = video as MovieTexture;
		audio = GetComponent<AudioSource> ();
		video.Play ();
	}
}
