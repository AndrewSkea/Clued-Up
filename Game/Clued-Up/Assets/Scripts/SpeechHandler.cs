﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpeechHandler : MonoBehaviour {

	public GameObject TargetChar;
	public GameObject TextObject;
	public GameObject ParentCanvas;
	public HUDController HUDCanvas;
	public Vector3 DetectivePos;
	public Vector3 SuspectPos;
	public Button[] TraitButtonArray;
	public Detective ActiveDet;
	public Story ActiveStory;
	public Button LeaveButton;
	public GameObject[] OtherUIElements;

	private Button ParentButton;
	private Text ActualText;
	private ImportSpeech SpeechRef;

	void Start (){
		Waiting ();
		ActiveDet = GetComponent<Detective> ();
		ActiveStory = GetComponent<Story> ();
		//string[] StrTraits = new string[ActiveDet.traits.Length];
		string [] StrTraits = new string[3] {"UNO", "DOS", "TRES"};
		for (int i = 0; i < StrTraits.Length; i++) {
			//StrTraits [i] = ActiveStory.getTraitString (ActiveDet.traits [i]);
			Debug.Log(TraitButtonArray[i].name);
			Text ButtonText = TraitButtonArray [i].GetComponentInChildren<Text> ();
			ButtonText.text = StrTraits [i];
			TraitButtonArray [i].name = StrTraits [i];
		}
		SpeechRef = TargetChar.GetComponentInChildren<ImportSpeech> ();
		ActualText = TextObject.GetComponentInChildren<Text> ();
		ParentCanvas.SetActive (false);
		gameObject.SetActive(false);
	}

	IEnumerator Waiting(){
		yield return new WaitForFixedUpdate ();
	}

	public void TurnOnSpeechUI(){
		//TODO: Maybe some intro speech here?
		if (ParentCanvas.gameObject.activeSelf == false) {
			foreach (GameObject ForObj in OtherUIElements) {
				ForObj.gameObject.SetActive (false);
			}
			HUDCanvas.loadPanelAndPause (ParentCanvas);
		} else {
			Debug.Log ("Tried to turn on speech UI, but was already on");
		}
	}

	public void TurnOffSpeechUI(){
		if (ParentCanvas.gameObject.activeSelf == true) {
			foreach (GameObject ForObj in OtherUIElements) {
				ForObj.gameObject.SetActive (true);
			}
			HUDCanvas.hidePanelAndResume (ParentCanvas);
		} else {
			Debug.Log ("Tried to turn off speech UI but it was already off.");
		}
	}

	public void StartBranch(Button SendingButton){
		SpeechRef.SetBranch (SendingButton.name);
		gameObject.SetActive(true);
		TextObject.SetActive(true);
		foreach (Button ForButton in TraitButtonArray) {
			ForButton.gameObject.SetActive (false);
		}
		LeaveButton.gameObject.SetActive (false);
		OnClick ();
	}

	public void OnClick (){
		string NextLine = SpeechRef.NextLine ();
		if (NextLine != null) {
			ActualText.text = NextLine.Substring(1, NextLine.Length - 1);
			if (NextLine [0].ToString () == "(") {
				TextObject.transform.position = DetectivePos;
			} else {
				TextObject.transform.position = SuspectPos;
			}
		} else {
			gameObject.SetActive(false);
			TextObject.SetActive(false);
			foreach (Button ForButton in TraitButtonArray) {
				ForButton.gameObject.SetActive (true);
			}
			LeaveButton.gameObject.SetActive (true);
		}
	}
}
