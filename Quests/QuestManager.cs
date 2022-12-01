using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public onPlayerScript player;

    public int sideQuestCount;

    [Header("Quest start/finish")]
    public bool questStarted;
    public bool questFinished;
    public bool canStartQuest;
    public bool canFinishQuest;
    
    //unused
    //[Header("Reward")]
    //public bool getReward;

    public Quest quest;

    void Start()
    {
        questStarted = false;
        questFinished = false;
        canStartQuest = true;
    }

    void Update()
    {
        //reward the player
        /*
        if(getReward == true)
        {
            ReceiveReward();
        }*/
    }

    //quest functions
    public void StartQuest()
    {
        if(canFinishQuest == true)
        {
            FinishQuest();
            FindObjectOfType<DialogueManager>().EndDialogue();
            return;
        }
        CheckWhichQuest();
        questStarted = true;
        canStartQuest = false;
        FindObjectOfType<DialogueManager>().EndDialogue();
    }

    public void FinishQuest()
    {
        questStarted = false;
        //getReward = true;
        sideQuestCount++;
        canFinishQuest = false;
        canStartQuest = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //ReceiveReward();
    }

    //planned for something that did not make it to the full game
    //maybe you find it helpful
    /*
    public void ReceiveReward()
    {
        //check the questReward for the right reward
        if(quest.questReward == "dash")
        {
            FindObjectOfType<Movement>().ActivateDash();
        }
        if(quest.questReward == "double jump")
        {
            FindObjectOfType<Movement>().ActivateDoubleJump();
        }
        getReward = false;
    }*/

    public void ItemPickup()
    {
        canFinishQuest = true;
    }

    //checking the quest name to see which one to start
    public void CheckWhichQuest()
    {
        quest = player.currentQuest;
        //unused
        /*
        if(quest.questName == "dash")
        {
            
        }
        if(quest.questName == "hp+")
        {
            
        }
        */
    }
}
