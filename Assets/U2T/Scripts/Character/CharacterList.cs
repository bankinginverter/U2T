using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterList : MonoBehaviour
{
    private List<GameObject> _character = new List<GameObject>();
    public static CharacterList Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCharacter(GameObject character)
    {
        _character.Add(character);
    }

    public void ClearList()
    {
        //Destroy(GameObject.Find(SelectCharacterCotroller._gameObjectCharacter.name));
        _character.Clear();
    }
}
