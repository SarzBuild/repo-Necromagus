using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwapManager : MonoBehaviour
{
    private static PlayerSwapManager _instance;
    public static PlayerSwapManager Instance { get 
    {
        if (_instance == null)
        {
            PlayerSwapManager singleton = GameObject.FindObjectOfType<PlayerSwapManager>();
            if (singleton == null)
            {
                GameObject go = new GameObject();
                _instance = go.AddComponent<PlayerSwapManager>();
            }
        }
        return _instance;
    }  }
    
    [SerializeField]private List<DeadBody> _bodyList = new List<DeadBody>();

    public Sprite DecayedCorpseSprite1;
    public Sprite DecayedCorpseSprite2;
    public Sprite DecayedCorpseSprite3;
    public Sprite DecayedCorpseSprite4;

    public PlayerController PlayerController;
    public GameObject CorpsePrefab;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void PlayerSwap(GameObject interactedBody)
    {
        GameObject NewDeadBody = Instantiate(CorpsePrefab, PlayerController.transform);
        NewDeadBody.transform.parent = null;
        DeadBody InstantiatedDeadBodyComponent = NewDeadBody.GetComponent<DeadBody>();
        InstantiatedDeadBodyComponent.PlayerSwapManager = this;
        InstantiatedDeadBodyComponent.CorpseDecayValue = interactedBody.GetComponent<DeadBody>().CorpseDecayValue;
        InstantiatedDeadBodyComponent.TextureUpdate();
        PlayerController.transform.position = interactedBody.transform.position;
        Destroy(interactedBody);
    }

    public void CreateNewBody()
    {
        GameObject NewDeadBody = Instantiate(CorpsePrefab, PlayerController.transform);
        NewDeadBody.transform.parent = null;
        DeadBody InstantiatedDeadBodyComponent = NewDeadBody.GetComponent<DeadBody>();
        InstantiatedDeadBodyComponent.PlayerSwapManager = this;
    }

    public void SubscribeBody(DeadBody body)
    {
        if(!_bodyList.Contains(body))
            _bodyList.Add(body);
    }

    public void UnsubscribeBody(DeadBody body)
    {
        if (_bodyList.Contains(body))
            _bodyList.Remove(body);
    }

    public void IncreaseBodyTimeLoopCount()
    {
        for (int i = 0; i < _bodyList.Count; i++)
        {
            _bodyList[i].IncreaseDecayValue();
            _bodyList[i].TextureUpdate();
        }
    }

    public void RemoveLeftOverObjects()
    {
        for (int i = 0; i < _bodyList.Count; i++)
        {
            _bodyList[i].RemoveListObject();
        }
    }
}
