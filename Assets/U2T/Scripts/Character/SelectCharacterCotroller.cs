using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SelectCharacterCotroller : MonoBehaviour
{
    SelectCharactorScreen selectCharactorScreen;
    Save save;

    public Charactor[] charactor;
    private int numberPlayer = 0;
    private GameObject _gameObjectCharacter;


    public void Awake()
    {
        save = new Save();
        _gameObjectCharacter = Instantiate(Resources.Load<GameObject>(charactor[numberPlayer].nameCharactor)) as GameObject;
        selectCharactorScreen = GetComponent<SelectCharactorScreen>();
        selectCharactorScreen.OnLeftBtn += () =>
        {
            if (numberPlayer > 0)
            {
                Destroy(_gameObjectCharacter);
                numberPlayer--;
                _gameObjectCharacter = Instantiate(Resources.Load<GameObject>(charactor[numberPlayer].nameCharactor)) as GameObject;
            }
        };

        selectCharactorScreen.OnRightBtn += () =>
        {
            if (numberPlayer < 4)
            {
                Destroy(_gameObjectCharacter);
                numberPlayer++;
                _gameObjectCharacter = Instantiate(Resources.Load<GameObject>(charactor[numberPlayer].nameCharactor)) as GameObject;
            }
        };

        selectCharactorScreen.OnEnter += () =>
        {
            save.SaveCharacterID(charactor[numberPlayer].nameCharactor);
        };
    }


    [Serializable]
    public class Charactor
    {
        public string nameCharactor;
    }
}
