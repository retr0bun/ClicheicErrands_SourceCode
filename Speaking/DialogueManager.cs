using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour 
{
	public Text nameText;
	public Text dialogueText;
	public GameObject button;

	public Animator animator;

	private Queue<string> sentences;

	void Start () 
    {
		button.SetActive(false);
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
	{
		animator.SetBool("isOpen", true);

		nameText.text = dialogue.name;

		sentences.Clear();

		//get all the sentences and enqueue them
		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
	}

	public void DisplayNextSentence()
	{
		if(sentences.Count == 1)
		{
			button.SetActive(true);
		}
		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		//remove the sentence that has been shown to from the queue
		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
		FindObjectOfType<AudioController>().Play("npc voice");
	}

	//make sentences have a nice animated effect
	IEnumerator TypeSentence (string sentence)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			FindObjectOfType<AudioController>().Pause("pick up");
			yield return null;
		}
	}

	public void EndDialogue()
	{
		FindObjectOfType<AudioController>().Pause("npc voice");
		animator.SetBool("isOpen", false);
		button.SetActive(false);
	}

	public void StartAQuest()
	{
		FindObjectOfType<QuestManager>().StartQuest();
	}
}