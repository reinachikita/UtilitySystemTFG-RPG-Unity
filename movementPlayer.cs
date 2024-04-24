using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class movementPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public static movementPlayer instance;
    Rigidbody2D CH;
    [SerializeField] private Vector2 dir;
    [SerializeField] public float velocity;
    [SerializeField] public SpriteRenderer spritePlayer;
    public HUDSystemNotBattle hudSystem;
    public GameObject door,player,npc;
    public Animator layerBlack;
    

    private void Awake()
    {
       
        if (instance == null)
        {
            instance = this;
            spritePlayer = GetComponent<SpriteRenderer>();
            CH = GetComponent<Rigidbody2D>();
            velocity = 6;
            hudSystem = GetComponent<HUDSystemNotBattle>();
            hudSystem.startUI();
            DontDestroyOnLoad(gameObject);
           

        }
        else
        {
            
            Debug.Log("Player ya hay xd");
        }
    }
  

    // Update is called once per frame
    void Update()
    {

        //Ajuste  de dirección entre 1 y -1 tanto para horizontal como vertical
        dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (dir.x == -1)
        {
            spritePlayer.flipX = true;

        }else if(dir.x == 1) 
        {
            spritePlayer.flipX = false;
        }

        if (dir.y == -1)
        {
            spritePlayer.flipY = true;

        }
        else if (dir.y == 1)
        {
            spritePlayer.flipY = false;
        }
        if (Input.GetKey(KeyCode.P))
        {
            hudSystem.gameObject.SetActive(false);

            SceneManager.LoadScene("BattleTestScene");
        }

        if (door != null)
        {
            
            StartCoroutine(openDoors(door));
        }
            


    }

    private void FixedUpdate()
    {
        //se mueve en base a la dirección y velocidad asignada en el tiempo, esto se hace para que haya movimiento y tambien para ajustar la velocidad del mismo
        //si no se moveria siempre a velocidad 1 con el axis
        if (Input.GetAxisRaw("Fire3")==0) //si no presionas shift no corres 
        {
            CH.MovePosition(CH.position + dir * velocity * Time.fixedDeltaTime);
        }
        else //si presionas shift corres
        {   
            CH.MovePosition(CH.position + dir * velocity * Time.fixedDeltaTime*2);
            
        }


        
    }

    IEnumerator openDoors(GameObject door)
    {
        if (Input.GetKey(KeyCode.Z) && SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("RoomBrother")))
        {
            layerBlack.SetBool("layerbool", true);
            yield return new WaitForSeconds(2f);
            layerBlack.SetBool("layerbool", false);
            SceneManager.LoadScene("Hall");
            this.transform.position = new Vector3(16.6251831f, -0.0988605246f, 0);
           
        }


        if (Input.GetKey(KeyCode.Z) && SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("LivingRoom")))
        {
            layerBlack.SetBool("layerbool", true);
            yield return new WaitForSeconds(2f);
            layerBlack.SetBool("layerbool", false);
            SceneManager.LoadScene("Hall");
            this.transform.position = new Vector3(-33.61f, -16.775f, 0);
        }

        if (Input.GetKey(KeyCode.Z) && SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Kitchen")))
        {
            layerBlack.SetBool("layerbool", true);
            yield return new WaitForSeconds(2f);
            layerBlack.SetBool("layerbool", false);
            SceneManager.LoadScene("Hall");
            this.transform.position = new Vector3(-29.0599976f, -23.5199966f, 0);
        }

        if (Input.GetKey(KeyCode.Z) && SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("BathRoom")))
        {
            layerBlack.SetBool("layerbool", true);
            yield return new WaitForSeconds(2f);
            layerBlack.SetBool("layerbool", false);
            SceneManager.LoadScene("Hall");
            this.transform.position = new Vector3(-24.8545856f, -0.725640059f, 0);
        }

        if (Input.GetKey(KeyCode.Z) && SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Hall")) && door.tag == "doorBathRoom")
        {
            layerBlack.SetBool("layerbool", true);
            yield return new WaitForSeconds(2f);
            layerBlack.SetBool("layerbool", false);
            SceneManager.LoadScene("BathRoom");
            this.transform.position = new Vector3(0, -2, 0);
        }

        if (Input.GetKey(KeyCode.Z) && SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Hall")) && door.tag == "doorKitchen")
        {
            layerBlack.SetBool("layerbool", true);
            yield return new WaitForSeconds(2f);
            layerBlack.SetBool("layerbool", false);
            SceneManager.LoadScene("Kitchen");
            this.transform.position = new Vector3(-6.90f, -3.96f, 0);
        }

        if (Input.GetKey(KeyCode.Z) && SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Hall")) && door.tag=="doorBro")
        {
            layerBlack.SetBool("layerbool", true);
            yield return new WaitForSeconds(2f);
            layerBlack.SetBool("layerbool", false);
            SceneManager.LoadScene("RoomBrother");
            this.transform.position = new Vector3(5.17000008f, -4.57999992f, 0);
        }

        if (Input.GetKey(KeyCode.Z) && SceneManager.GetActiveScene().Equals(SceneManager.GetSceneByName("Hall")) && door.tag == "doorLiving")
        {
            layerBlack.SetBool("layerbool", true);
            yield return new WaitForSeconds(2f);
            layerBlack.SetBool("layerbool", false);
            SceneManager.LoadScene("LivingRoom");
            this.transform.position = new Vector3(7.98269749f, 0.849116802f, 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider) //intercepta los gameObjects con los que el player interactua
    {
        
        if (collider.gameObject.CompareTag("npc"))
        {
            Debug.Log("trigger npc!");
            Debug.Log(collider.gameObject.name);
            Debug.Log(collider.GetComponent<Conversation>());
            Conversation conversation = collider.GetComponent<Conversation>();
            npc = collider.gameObject;
            DialogueManager.instance.InteractuateNPCOk(conversation,npc);
        }
       
        if (collider.gameObject.CompareTag("door") || collider.gameObject.CompareTag("doorBro") || collider.gameObject.CompareTag("doorLiving") || collider.gameObject.CompareTag("doorKitchen") || collider.gameObject.CompareTag("doorBathRoom"))
        {
            Debug.Log("trigger door!");
            door = collider.gameObject;

        }
       
    }

    private void OnTriggerExit2D(Collider2D collider) //intercepta los gameObjects con los que el player interactua
    {

        if (collider.gameObject.CompareTag("npc"))
        {
            npc = null;
        }
        if (collider.gameObject.CompareTag("door") || collider.gameObject.CompareTag("doorBro") || collider.gameObject.CompareTag("doorLiving") || collider.gameObject.CompareTag("doorKitchen") || collider.gameObject.CompareTag("doorBathRoom"))
        {

            door = null;
        }
    }
}


