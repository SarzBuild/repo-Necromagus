using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSwapManager : MonoBehaviour
{
    public enum CorpsePosition
    {
        OUTSIDE,
        CORPSE1,
        CORPSE2,
        CORPSE3,
        CORPSE4
    }

    public CorpsePosition corpsePosition;
    
}
