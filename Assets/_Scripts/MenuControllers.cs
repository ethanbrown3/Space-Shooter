using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControllers : MonoBehaviour {
	public Text scoreText;
	public Text messageText;
	// Use this for initialization
	void Start () {
		scoreText.text = PlayerPrefs.GetInt ("Score").ToString();
		messageText.text = PlayerPrefs.GetString ("Game Over");
	}
}
