﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
/// <summary>
/// Character class
/// </summary>
public class Character : MonoBehaviour {
	/// <summary>
	/// Name to be used when displaying a name (i.e. contains spaces)
	/// </summary>
	public string longName;
	/// <summary>
	/// If character is the murderer. Set by Story.
	/// </summary>
	public bool isMurderer;
	/// <summary>
	/// If character is the victim. Set by Story.
	/// </summary>
	public bool isVictim;
	/// <summary>
	/// List of Clues attatched to this character instance
	/// </summary>
	public List<Clue> characterClues;
	/// <summary>
	/// Makes the object persistant throughout scenes
	/// </summary>
	public SpeechHandler SpeechUI;
	public string CritBranch;
	public TextAsset SpeechFile;

	void Awake ()
	{
		SpeechUI = FindObjectOfType<SpeechHandler> ();
		DontDestroyOnLoad(gameObject);
	}

	void OnMouseDown(){
		SpeechUI.TurnOnSpeechUI ();
	}

	public void PreSpeech (string BranchName){
		//TODO: Figure out what clues and items go into speech
	}

	public void PostSpeech (string BranchName){
		if (BranchName == CritBranch) {
			//TODO: Give item
		} else if (BranchName == "Accuse-NoItems" || BranchName == "Accuse-WrongChar"
		           || BranchName == "Accuse-Motive" || BranchName == "Accuse-Weapon") {
			Collider CharCollider = gameObject.GetComponent<Collider> ();
			CharCollider.gameObject.SetActive (false);
		} else if (BranchName == "Accuse-Right") {
			Story ActiveStory = FindObjectOfType<Story> ();
			ActiveStory.EndGame ();
		}
	}

	/// <summary>
	/// Initialise the specified Character with properties and CharacterClues
	/// </summary>
	///<param name="characterIndex">Index of character to be initialised</param>
	public void initialise(int characterIndex){
		StreamReader stream = new StreamReader("Assets/TextFiles/character" + characterIndex.ToString() + ".txt");
		List<string> lines = new List<string> ();
		while(!stream.EndOfStream){
			lines.Add(stream.ReadLine());
		}
		stream.Close( );

		this.gameObject.name = lines[1]; //file contains comment in line 0
		this.longName = lines [2];
		this.isMurderer = false;
		this.isVictim = false;

		//gameObject.GetComponent<Renderer> ().enabled = false; //dont draw any characters yet

		//TODO set up character clues
}
}
