using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HorseHides : MonoBehaviour
{
    [SerializeField] private List<HorseColor> m_colors = new();
    [SerializeField] private List<HorseMarking> m_markings = new();
    public Sprite GetHorseSprite(string color)
    {
        Sprite sprite = null;
        HorseColor horseColor = m_colors.FirstOrDefault(c => c.color == color);

        if (horseColor != null)
            sprite = horseColor.horseSprite;

        return sprite;
    }
    
    public Sprite GetMarkingSprite(string marking)
    {
        Sprite sprite = null;
        HorseMarking horseMarking = m_markings.FirstOrDefault(c => c.marking == marking);

        if (horseMarking != null)
            sprite = horseMarking.markingSprite;

        return sprite;
    }

    [System.Serializable]
    public class HorseColor
    {
        public string color;
        public Sprite horseSprite;
    }

    [System.Serializable]
    public class HorseMarking
    {
        public string marking;
        public Sprite markingSprite;
    }
}
