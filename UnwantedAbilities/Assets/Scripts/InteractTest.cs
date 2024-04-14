using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class InteractTest : MonoBehaviour
{
    private Input @input;
    public GameObject interact;
    public GameObject replacement;
    public GameObject textObject;
    public TMP_Text popUp;
    public string textStr;
    private Vector2 pos;
    private Vector2 textPos;
    private bool inRange;
    public Player player;
    public bool fireCheck = false;
    public bool waterCheck = false;
    public bool jumpCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        input = new Input();
        input.Enable();
        inRange = false;
        pos = transform.position;
        textPos = textObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.Instance.input.Default.Interact.triggered && inRange){
            Destroy(interact);
            replacement.transform.position = new Vector2(pos.x, pos.y); 
            if (gameObject.tag == "FireStatue") {
                player.noFireImmunity();
                fireCheck = true;
                checkIfWin();
                player.RelocatePlayer();
               

            }
            if (gameObject.tag == "WaterStatue") {
                player.noWaterBreathing();
                waterCheck = true;
                checkIfWin();
                player.RelocatePlayer();
                
}
            if (gameObject.tag == "AirStatue") {
                player.noDoubleJump();
                jumpCheck = true;
                checkIfWin();
                player.RelocatePlayer();
            }
        }
    }
    public void checkIfWin()
    {
        if(waterCheck == true && fireCheck == true && jumpCheck == true)
        {
            SceneManager.LoadScene(0);
        }

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")){
            inRange = true;
            popUp.text = textStr;
            popUp.transform.position = new Vector2(pos.x, pos.y + 2f);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")){
            inRange = false; 
            popUp.transform.position = new Vector2(textPos.x, textPos.y);
        }
    }
}
