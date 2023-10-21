using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MasterData
{
    [Serializable]
    public struct ReadFile
    {
        [SerializeField] public string Command;
        [SerializeField] public string Text;
    }

    [Serializable]
    public class MasterDataClass<T>
    {
        public T[] Data;
    }
}