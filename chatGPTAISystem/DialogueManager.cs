using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    //instance
    public static DialogueManager instance;

    // UI Conversation
    public Conversation currentConversation;
    public TextMeshProUGUI responsesUI, sentencesUI;
    public TextMeshProUGUI npcNameUI, textFeed;
    public GameObject textbox,textNPC, buttonResponse,textFeedGM;
    public ChatGPTManager chatGPTManager;
    public GameObject inputField;
    public TMP_InputField inputFieldTMP;
    private bool textToInteractuateOrNot = true;
    //objects to interaction: npc, and player
    [SerializeField] public GameObject player,npcGM;

    //time to print textUI
    private float letterDelay = 0.1f;
    private int currentIndex = 0;
    [SerializeField]
    int i;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);


        }
        else
        {

            Debug.Log("ya hay DialogueM bonita");
        }
    }

    // Update is called once per frame
    void Update() //llama a la interaccion con el npc en bucle cuando hay un npc en collision
    {

        if(currentConversation!=null && npcGM!=null)
        {
            InteractuateNPCOk(currentConversation , npcGM);
        }else 
        {
            textToInteractuateOrNot = true;
        }
        
    }
   
    IEnumerator textDelayPrint() //imprime el texto letra a letra.
    {
        while(currentIndex < currentConversation.sentences[i].Length)
        {
            sentencesUI.text+= currentConversation.sentences[i][currentIndex];
            currentIndex++;
            yield return new WaitForSeconds(letterDelay);
        }
        i++;
        currentIndex = 0;
        
    }

    private void endConversation()  //termina conversacion
    {
    
        i = 0; //contador de convers
        sentencesUI.text = currentConversation.sentences[i];
        buttonResponse.SetActive(false);
        textbox.SetActive(false);
        textNPC.SetActive(false);
    }

    /* public void showNextSentence() //Cambia de frase en un array de conversaciones.
     {
         if (i >= currentConversation.sentences.Length)
         {
             //terminó la conver

             movementPlayer.instance.velocity = 6;
             endConversation();

             return;
         }
         else
         {
             StartCoroutine(textDelayPrint());
         }
         /* for(int i=0;i<buttonsResponse.Length;i++)
          {
         n
          }*/
    //}

    [System.Obsolete]
    
    public void InteractuateNPCOk(Conversation npc, GameObject npcG) //llama a la conversacion, y se imprime por texto de UI, y cancela tambien la conversacion si se aleja o acaba
    {
        //intercepta a que npc estamos accediendo a traves de colliders.
        currentConversation = npc;
        npcGM=npcG;
        textbox.SetActive(true);
        if (textToInteractuateOrNot)
        {
            textFeed.alignment = TextAlignmentOptions.Center;
            textFeed.text = "PRESS THE Z TO SPEAK WITH THE NPC";
        }
        if (Input.GetKeyDown(KeyCode.Z) )
        {
            textToInteractuateOrNot = false;
            textFeedGM.SetActive(false);
            textbox.SetActive(false);
            movementPlayer.instance.velocity = 0;
            inputField.SetActive(true);
            textFeed.text = "";
            textFeed.alignment = TextAlignmentOptions.Left;


            //cuanto más pulses mas se acelera la velocidad del texto
            /*if (letterDelay > 0.02)
            {
                letterDelay -= 0.01f;
            }
            //imprimir siguiente frase:
           /* if (currentIndex == 0)
            {
                letterDelay = 0.1f; //reset de velocidad del texto cada frase.
                Debug.Log("sentence print");
                sentencesUI.text = "";*/


            //}
        }

        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && inputField.activeSelf && inputFieldTMP.text != null)
        {
            textFeed.text = "";
            inputField.SetActive(false);
            textbox.SetActive(true);
            //buttonResponse.SetActive(true);
            textNPC.SetActive(true);
            string sendText = inputFieldTMP.text;
            inputFieldTMP.text = "";
            chatGPTManager.SendRequestToChatGPT(sendText);
        }
        else if(Mathf.Abs(player.transform.position.x - npcGM.transform.position.x) > 2 && Mathf.Abs(player.transform.position.y - npcGM.transform.position.y) > 2)
        {
            movementPlayer.instance.velocity = 6;
            endConversation();
        }
       
    }
}

