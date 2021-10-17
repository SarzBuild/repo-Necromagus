using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : MonoBehaviour
{
    public PlayerSwapManager PlayerSwapManager;
    public int CorpseDecayValue;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = transform.GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        GetPlayerSwapManager();
        TextureUpdate();
    }

    private void GetPlayerSwapManager()
    {
        if (PlayerSwapManager == null)
        {
            PlayerSwapManager = PlayerSwapManager.Instance;
            if(PlayerSwapManager != null)
                PlayerSwapManager.SubscribeBody(this);
        }
        else
        {
            Debug.Log("Hello");
            PlayerSwapManager.SubscribeBody(this);
        }
    }

    private void OnDisable()
    {
        PlayerSwapManager.UnsubscribeBody(this);
    }

    public void IncreaseDecayValue()
    {
        CorpseDecayValue++;
    }

    public void RemoveListObject()
    {
        if (CorpseDecayValue == 4)
        {
            Destroy(gameObject);
        }
    }

    public void TextureUpdate()
    {
        switch (CorpseDecayValue)
        {
            case 0:
                _spriteRenderer.sprite = PlayerSwapManager.DecayedCorpseSprite1;
                break;
            case 1:
                _spriteRenderer.sprite = PlayerSwapManager.DecayedCorpseSprite2;
                break;
            case 2:
                _spriteRenderer.sprite = PlayerSwapManager.DecayedCorpseSprite3;
                break;
            case 3:
                _spriteRenderer.sprite = PlayerSwapManager.DecayedCorpseSprite4;
                break;
            case 4:
                transform.position = new Vector3(40, 40);
                break;
        }
    }
}
