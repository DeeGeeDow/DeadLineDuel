using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ChoosePlayerController : MonoBehaviour
{
    public bool isPlayer1;
    private Vector2 chooseDirection;
    [SerializeField] private Vector2 playerPosition;
    public float chooseDirectionCD = 0.1f;
    public bool chooseDirectionAvailable = true;
    public PlayerTypes player;
    public bool chosen = false;
    public GameEvent ChangedEvent;
    public GameEvent ChosenEvent;

    public bool chooseAvailable = true;
    public float chooseCD = 0.1f;

    [Header("Sprites")]
    public Sprite Scientist;
    public Sprite Hacker;
    public Sprite Engineer;
    public Sprite Painter;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!chosen)
        {
            bool changed = false;
            if(chooseDirection.magnitude > 0 && chooseDirectionAvailable)
            {
                chooseDirectionAvailable = false;
                if(Mathf.Abs(chooseDirection.x) > Mathf.Abs(chooseDirection.y))
                {
                    chooseDirection = new Vector2(Mathf.Sign(chooseDirection.x), 0);
                }
                else
                {
                    chooseDirection = new Vector2(0, Mathf.Sign(chooseDirection.y));
                }
                changed = true;
                StartCoroutine(ChooseDirectionCD());
            }else if (!chooseDirectionAvailable)
            {
                chooseDirection = new Vector2(0, 0);
            }

            playerPosition += chooseDirection;
            if(playerPosition.x < 0)
            {
                playerPosition = new Vector2(0, playerPosition.y);
                changed = false;
            }
            else if(playerPosition.x > 1)
            {
                playerPosition = new Vector2(1, playerPosition.y);
                changed = false;
            }

            if(playerPosition.y < 0)
            {
                playerPosition = new Vector2(playerPosition.x, 0);
                changed = false;
            }else if(playerPosition.y > 1)
            {
                playerPosition = new Vector2(playerPosition.x, 1);
                changed = false;
            }

            changePlayer(changed);
        }
    }

    public void changePlayer(bool changed)
    {
        if (changed)
        {
            Debug.Log("Masuk");
            int positionFlattened = (int)playerPosition.x + (int)playerPosition.y * 2;
            Image image = GetComponentInChildren<Image>();
            switch (positionFlattened)
            {
                case 0:
                    player = PlayerTypes.ENGINEER;
                    image.sprite = Engineer;
                    break;
                case 1:
                    player = PlayerTypes.PAINTER;
                    image.sprite = Painter;
                    break;
                case 2:
                    player = PlayerTypes.SCIENTIST;
                    image.sprite = Scientist;
                    break;
                case 3:
                    player = PlayerTypes.HACKER;
                    image.sprite = Hacker;
                    Debug.Log("woi");
                    break;
                default:
                    break;
            }

            ChangedEvent.Raise(this, player);
        }
    }
    public void ChangePlayer(InputAction.CallbackContext context)
    {
        chooseDirection = context.ReadValue<Vector2>();
    }

    private IEnumerator ChooseDirectionCD()
    {
        yield return new WaitForSeconds(chooseDirectionCD);
        chooseDirectionAvailable = true;
    }

    public void ChoosePlayer(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            choose();
        }
    }

    public void UnchoosePlayer(InputAction.CallbackContext context)
    {
        if (context.action.triggered)
        {
            unchoose();
        }
    }

    private void choose()
    {
        if(!chosen && chooseAvailable)
        {
            chooseAvailable = false;
            chosen = true;
            ChosenEvent.Raise(this, true);
            StartCoroutine(chooseCoroutine());
        }
    }

    private void unchoose()
    {
        if(chosen && chooseAvailable)
        {
            chooseAvailable = false;
            chosen = false;
            ChosenEvent.Raise(this, false);
            StartCoroutine(chooseCoroutine());
        }
    }

    private IEnumerator chooseCoroutine()
    {
        yield return new WaitForSeconds(chooseCD);
        chooseAvailable = true;
    }
}
