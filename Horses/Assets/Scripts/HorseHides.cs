using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HorseHides : MonoBehaviour
{
    [SerializeField] private List<Hide> m_hides = new();
    public AnimationClip GetHorseClip(string id)
    {
        AnimationClip clip = null;
        Hide horseColor = m_hides.FirstOrDefault(c => c.id == id);

        if (horseColor != null)
            clip = horseColor.clip;

        return clip;
    }

    [System.Serializable]
    public class Hide
    {
        public string id;
        public AnimationClip clip;
    }
}
