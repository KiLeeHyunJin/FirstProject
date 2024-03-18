using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboUI : MonoBehaviour
{
    [SerializeField] Image[] countImg;
    [SerializeField] Sprite[] countSprite;
    GridLayoutGroup grid;
    Vector2 normalSize;
    private void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        normalSize = grid.cellSize;
    }
    // Start is called before the first frame update
    public void SetCount(int count)
    {
        int i = 0;
        grid.cellSize = normalSize;
        if (count  == 0)
        {
            for (; i < countImg.Length ; i++)
            {
                countImg[i].enabled = false;
                countImg[i].color = new Color(1, 1, 1, 1);
            }
            countImg[0].sprite = countSprite[0];
            if (offCo != null)
                StopCoroutine(offCo);
            offCo = StartCoroutine(CloseOut());
            return;
        }

        int forCount = 0;
        int temp = count;

        while (true)
        {
            if (temp == 0)
                break;
            forCount++;
            temp /= 10;
        }
        i = 0;
        for (; i < forCount; i++)
        {
            int imgCount = count % 10;
            if (countImg[i].enabled == false)
                countImg[i].enabled = true;
            countImg[i].sprite = countSprite[imgCount];
            countImg[i].color = new Color(1, 1, 1, 1);
            count /= 10;
        }
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(PopSize());
    }
    Coroutine coroutine = null;
    IEnumerator PopSize()
    {
        if (offCo != null)
            StopCoroutine(offCo);
        offCo = StartCoroutine(CloseOut());
        Vector2 size = normalSize;
        while(true)
        {
            size += new Vector2(660, 900) * Time.deltaTime;
            grid.cellSize = size;
            yield return new WaitForFixedUpdate();
            if (grid.cellSize.x >= 110)
                break;
        }
        while(true)
        {
            size -= new Vector2(440, 600) * Time.deltaTime;
            grid.cellSize = size;
            yield return new WaitForFixedUpdate();
            if (grid.cellSize.x <= 66)
                break;
        }
        grid.cellSize = normalSize;

    }
    Coroutine offCo = null;
    IEnumerator CloseOut()
    {
        Color color = countImg[0].color;
        while(true)
        {
            color.a -= Time.deltaTime;
            for (int i = 0; i < countImg.Length; i++)
                countImg[i].color = color;
            if(color.a <= 0)
                break;
            yield return new WaitForFixedUpdate();
        }
    }
}
