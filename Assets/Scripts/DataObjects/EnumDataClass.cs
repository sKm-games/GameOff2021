using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnumDataClass
{
    public enum BugTypeEnum
    {
        Random, Ants, Termite
    }

    public BugTypeEnum BugType;

    public enum GamePhasesEnum
    {
        Hub, Investigation, Extermination
    }

    public GamePhasesEnum GamePhase;
}
