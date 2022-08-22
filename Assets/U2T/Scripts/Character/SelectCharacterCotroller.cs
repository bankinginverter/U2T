using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectCharacterCotroller : MonoBehaviour
{
    public Charactor[] charactor;
    private int numberPlayer = 0;

    SelectCharactorScreen selectCharactorScreen;

    public void Awake()
    {
        selectCharactorScreen = GetComponent<SelectCharactorScreen>();
        selectCharactorScreen.OnLeftBtn += () =>
        {
            if (numberPlayer > 0)
            {
                charactor[numberPlayer].charactorObject.SetActive(false);
                numberPlayer--;
                charactor[numberPlayer].charactorObject.SetActive(true);
            }
        };

        selectCharactorScreen.OnRightBtn += () =>
        {
            if (numberPlayer < 4)
            {
                charactor[numberPlayer].charactorObject.SetActive(false);
                numberPlayer++;
                charactor[numberPlayer].charactorObject.SetActive(true);
            }
        };

        selectCharactorScreen.OnEnter += () =>
        {
            PlayerPrefs.SetString("character", charactor[numberPlayer].nameCharactor);
        };

        selectCharactorScreen.OnSelectBtn += () =>
        {
            //PlayerPrefs.SetString("character", charactor[numberPlayer].nameCharactor);
            //SelectCharacter.instance.playerAvatarName = charactor[numberPlayer].nameCharactor;
        };
    }


    [Serializable]
    public class Charactor
    {
        public GameObject charactorObject;
        public string nameCharactor;
    }
}
