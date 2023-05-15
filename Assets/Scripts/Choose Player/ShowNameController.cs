using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowNameController : MonoBehaviour
{
    public bool isPlayer1;
    public PlayerTypes player;
    private string nameToShow;

    [Header("Names")]
    public string Scientist = "CHEMMI";
    public string Hacker = "BYTE";
    public string Engineer = "BILLDER";
    public string Painter = "FRUSH";
    // Start is called before the first frame update
    void Start()
    {
        changeName();
    }

    // Update is called once per frame
    void Update()
    {
        changeName();
        GetComponent<TextMeshProUGUI>().text = nameToShow;
    }

    public void onChanged(Component sender, object data)
    {
        if(sender is ChoosePlayerController && ((ChoosePlayerController) sender).isPlayer1 == isPlayer1)
        {
            player = (PlayerTypes)data;
        }
    }

    private void changeName()
    {
        switch (player)
        {
            case PlayerTypes.SCIENTIST:
                nameToShow = Scientist;
                break;
            case PlayerTypes.HACKER:
                nameToShow = Hacker;
                break;
            case PlayerTypes.ENGINEER:
                nameToShow = Engineer;
                break;
            case PlayerTypes.PAINTER:
                nameToShow = Painter;
                break;
            default:
                nameToShow = Scientist;
                break;
        }
    }
}
