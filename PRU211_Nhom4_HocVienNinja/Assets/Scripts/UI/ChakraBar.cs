using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChakraBar : MonoBehaviour
{

    public Slider Slider;
    public Color HighChakra;
    public Color LowChakra;
    public Vector3 Offset;

    public void SetChakra(int Chakra, int maxChakra)
    {
        Slider.gameObject.SetActive(Chakra > 0);
        Slider.value = Chakra;
        Slider.maxValue = maxChakra;
        Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(LowChakra, HighChakra, Slider.normalizedValue);
    }
    private void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
