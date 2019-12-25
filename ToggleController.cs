using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleController : MonoBehaviour
{
    public List<Toggle> toggleChild = new List<Toggle>();
    public List<GameObject> pages = new List<GameObject>();
    public List<Text> textChild = new List<Text>();
    public List<TextMeshProUGUI> textMeshChild = new List<TextMeshProUGUI>();
    public List<Material> fontMaterials = new List<Material>();
    public List<Color> colors = new List<Color>();
    private List<Outline[]> outline = new List<Outline[]>();
    public List<int> fontsSize = new List<int>();
    public List<float> lineSpacing = new List<float>();
    public List<FontStyle> fontStyles = new List<FontStyle>();
    public List<Vector2> moveTextPos = new List<Vector2>();
    public List<GameObject[]> backgrounds = new List<GameObject[]>();

    public bool isInit = false;

    [SerializeField]
    private bool usingArrow = false;

    private void Start()
    {
        if (textChild.Count > 0)
        {
            int len = textChild.Count;
            for (int i = 0; i < len; i++)
            {
                int outLineLen = textChild[i].GetComponents<Outline>().Length;
                if (outLineLen > 0)
                {
                    Outline[] outlines = textChild[i].GetComponents<Outline>();
                    outline.Add(outlines);
                }
            }
        }

        if (toggleChild.Count > 0)
        {
            int len = toggleChild.Count;
            for (int i = 0; i < len; i++)
            {
                Image[] tmpBackgrounds = toggleChild[i].GetComponentsInChildren<Image>(true);
                int len2 = tmpBackgrounds.Length;

                List<GameObject> tmpObj = new List<GameObject>();

                for (int j = 0; j < len2; j++)
                {
                    tmpObj.Add(tmpBackgrounds[j].gameObject);
                }

                backgrounds.Add(tmpObj.ToArray());
            }
        }

        isInit = true;
    }

    public void SwitchState()
    {
        int length = toggleChild.Count;

        for (int i = 0; i < length; i++)
        {
            int v = toggleChild[i].isOn ? 1 : 0;
            SetText(i, v);
            SetTextMesh(i, v);
            SwitchPage(i, toggleChild[i].isOn);
            SwitchBackground(i, toggleChild[i].isOn);
            MovePosition(i, toggleChild[i].isOn);
            
            if(toggleChild[i].isOn)
            {
                UseArrow(i);
            }
        }
    }

    public void SwitchPage(int number, bool state)
    {
        if (pages.Count > 0)
        {
            pages[number].SetActive(state);
        }
    }

    private void UseArrow(int i)
    {
        if (usingArrow)
        {
            GetComponent<ChangeToggle>().currentIndex = i;
        }
    }

    private void SetText(int number, int value)
    {
        if (textChild.Count > 0)
        {
            textChild[number].color = colors[value];
            if (outline.Count > 0)
            {
                for (int j = 0; j < outline[number].Length; j++)
                {
                    bool state = value == 1 ? true : false;
                    outline[number][j].enabled = state;
                }
            }

            if (fontsSize.Count > 0 && lineSpacing.Count > 0)
            {
                textChild[number].fontSize = fontsSize[value];
                textChild[number].lineSpacing = lineSpacing[value];
            }

            if (fontStyles.Count > 0)
            {
                textChild[number].fontStyle = fontStyles[value];
            }
        }
    }

    private void SetTextMesh(int number, int index)
    {
        if (textMeshChild.Count > 0)
        {
            if (colors.Count > 0)
                textMeshChild[number].color = colors[index];
            if (fontMaterials.Count > 0)
                textMeshChild[number].fontMaterial = fontMaterials[index];
        }
    }

    private void SwitchBackground(int number, bool state)
    {
        if (backgrounds.Count > 0)
        {
            backgrounds[number][0].SetActive(!state);
            backgrounds[number][1].SetActive(state);
        }
    }

    private void MovePosition(int number, bool state)
    {
        if(moveTextPos.Count > 0)
        {
            int index = state ? 1 : 0;

            if(textMeshChild.Count > 0)
            {
                textMeshChild[number].rectTransform.anchoredPosition = moveTextPos[index];
            }

            if(textChild.Count > 0)
            {
                textChild[number].rectTransform.anchoredPosition = moveTextPos[index];
            }
        }
    }

    public void GetCurrentTextPosition(int index)
    {
        MaskableGraphic[] tmpGraphic = toggleChild[0].GetComponentsInChildren<MaskableGraphic>();
        bool found = false;

        for (int i = 0; i < tmpGraphic.Length; i++)
        {
            Text text = tmpGraphic[i] as Text;
            TextMeshProUGUI text2 = tmpGraphic[i] as TextMeshProUGUI;

            if(text != null)
            {
                found = true;
                AddPosition(index, text.rectTransform.anchoredPosition);
                
            }
            else if(text2 != null)
            {
                found = true;
                AddPosition(index, text2.rectTransform.anchoredPosition);
            }
   
        }

        if(found == false)
        {
            Debug.Log("Moving text type error");
        }
    }

    public void SetToTextPosition(int index)
    {
        if(moveTextPos.Count == 0)
        {
            Debug.Log("moveTextPos is empty");
            return;
        }

        MaskableGraphic[] tmpGraphic = toggleChild[0].GetComponentsInChildren<MaskableGraphic>();
        bool found = false;

        for (int i = 0; i < tmpGraphic.Length; i++)
        {
            Text text = tmpGraphic[i] as Text;
            TextMeshProUGUI text2 = tmpGraphic[i] as TextMeshProUGUI;

            if(text != null)
            {
                found = true;
                text.rectTransform.anchoredPosition = moveTextPos[index];
                
            }
            else if(text2 != null)
            {
                found = true;
                text2.rectTransform.anchoredPosition = moveTextPos[index];
            }
   
        }

        if(found == false)
        {
            Debug.Log("Moving text type error");
        }
    }

    private void AddPosition(int index, Vector3 pos)
    {
        while (true)
        {
            if (moveTextPos.Count <= index) {
                moveTextPos.Add(Vector3.zero);
            } else {
                break;
            }
        }
        moveTextPos[index] = pos;
    }
}