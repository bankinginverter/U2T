using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManagerInScene : MonoBehaviour
{
    public static CharacterManagerInScene Instance;

    [SerializeField] GameObject[] _character;

    private void Awake()
    {
        Instance = this;
    }

    public void OnOffCharacter(string name)
    {
        foreach (var item in _character)
        {
            if (item.name == name)
            {
                item.SetActive(true);
            }
            else
            {
                item.SetActive(false);
            }
        }
    }
}
