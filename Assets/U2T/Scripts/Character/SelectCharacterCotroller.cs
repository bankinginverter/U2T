using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectCharacterCotroller : MonoBehaviour
{
    SelectCharactorScreen selectCharactorScreen;
    Save save;

    public Charactor[] charactor;
    private int numberPlayer = 0;
    public static GameObject _gameObjectCharacter;


    public void Awake()
    {
        save = new Save();
        selectCharactorScreen = GetComponent<SelectCharactorScreen>();
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            _gameObjectCharacter = Instantiate(Resources.Load<GameObject>(charactor[numberPlayer].nameCharactor)) as GameObject;
        }
        selectCharactorScreen.OnLeftBtn += () =>
        {
            if (numberPlayer > 0)
            {
                Destroy(_gameObjectCharacter);
                numberPlayer--;
                _gameObjectCharacter = Instantiate(Resources.Load<GameObject>(charactor[numberPlayer].nameCharactor)) as GameObject;
            }
            //if (SceneManager.GetActiveScene().name == "Lobby")
            //{
            //    if (numberPlayer > 0)
            //    {
            //        Destroy(_gameObjectCharacter);
            //        numberPlayer--;
            //        _gameObjectCharacter = Instantiate(Resources.Load<GameObject>(charactor[numberPlayer].nameCharactor)) as GameObject;
            //    }
            //}
            //else
            //{
            //    if (numberPlayer > 0)
            //    {
            //        Destroy(_gameObjectCharacter);
            //        numberPlayer--;
            //        _gameObjectCharacter = Instantiate(Resources.Load<GameObject>(charactor[numberPlayer].nameCharactor),new Vector3(-1.54f, 1.900001f, 118.9f),Quaternion.EulerRotation(0f,-180f,0f)) as GameObject;
            //        CharacterList.Instance.AddCharacter(_gameObjectCharacter);
            //    }
            //}
        };

        selectCharactorScreen.OnRightBtn += () =>
        {
            if (numberPlayer < 4)
            {
                Destroy(_gameObjectCharacter);
                numberPlayer++;
                _gameObjectCharacter = Instantiate(Resources.Load<GameObject>(charactor[numberPlayer].nameCharactor)) as GameObject;
            }
            //if (SceneManager.GetActiveScene().name == "Lobby")
            //{
            //    if (numberPlayer < 4)
            //    {
            //        Destroy(_gameObjectCharacter);
            //        numberPlayer++;
            //        _gameObjectCharacter = Instantiate(Resources.Load<GameObject>(charactor[numberPlayer].nameCharactor)) as GameObject;
            //    }
            //}
            //else
            //{
            //    if (numberPlayer < 4)
            //    {
            //        Destroy(_gameObjectCharacter);
            //        numberPlayer++;
            //        _gameObjectCharacter = Instantiate(Resources.Load<GameObject>(charactor[numberPlayer].nameCharactor),new Vector3(-1.54f, 1.900001f, 118.9f), Quaternion.EulerRotation(0f, -180f, 0f)) as GameObject;
            //        CharacterList.Instance.AddCharacter(_gameObjectCharacter);
            //    }
            //}
        };

        selectCharactorScreen.OnEnter += () =>
        {
            string _phares = charactor[numberPlayer].nameCharactor;
            string[] _words = _phares.Split('P');
            string _nameRealCharacter = "";
            foreach (var word in _words)
            {
                _nameRealCharacter = word;
                break;
            }
            save.SaveCharacterID(_nameRealCharacter);
        };
    }

    [Serializable]
    public class Charactor
    {
        public string nameCharactor;
    }
}
