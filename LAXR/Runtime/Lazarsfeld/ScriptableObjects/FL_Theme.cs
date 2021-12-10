using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JFuzz.Lazarsfeld
{
    [CreateAssetMenu(fileName = "Section Theme", menuName = "ScriptableObjects/JFuzz/Lazarsfeld/Theme", order = 1)]
    public class FL_Theme : ScriptableObject
    {
        [Tooltip("The Core Theme")]
        public FLTheme ThemeInfo;
    }
}

