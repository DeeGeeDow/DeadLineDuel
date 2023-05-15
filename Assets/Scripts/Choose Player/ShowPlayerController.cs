using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerController : MonoBehaviour
{
    public PlayerTypes playerToShow = PlayerTypes.SCIENTIST;
    public bool isPlayer1;

    [Header("Sprites")]
    public Sprite ScientistSprite;
    public Sprite HackerSprite;
    public Sprite EngineerSprite;
    public Sprite PainterSprite;

    // Update is called once per frame
    void Update()
    {
        ShowChara();
    }

    private void ShowChara()
    {
        Image charaImage = GetComponent<Image>();
        switch (playerToShow)
        {
            case (PlayerTypes.SCIENTIST):
                charaImage.sprite = ScientistSprite;
                break;
            case (PlayerTypes.HACKER):
                charaImage.sprite = HackerSprite;
                break;
            case (PlayerTypes.PAINTER):
                charaImage.sprite = PainterSprite;
                break;
            case (PlayerTypes.ENGINEER):
                charaImage.sprite = EngineerSprite;
                break;
            default:
                charaImage.sprite = null;
                break;   
        }

        Vector3 sc = charaImage.transform.localScale;
        if (!isPlayer1 && sc.x > 0) charaImage.transform.localScale = new Vector3(-sc.x, sc.y, sc.z);
    }

    public void onChangedChara(Component sender, object data)
    {
        if(sender is ChoosePlayerController && ((ChoosePlayerController) sender).isPlayer1 == isPlayer1)
        {
            if(data is PlayerTypes)
            {
                playerToShow = (PlayerTypes) data;
            }
        }
    }
}
