using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public TileState state { get; private set; }
    public TileCell cell { get; private set; }
    public int number { get; private set; }
    private Image background;
    private TextMeshProUGUI text;
    public  bool locked { get;  set; }

    private void Awake()
    {
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetState(TileState state, int number)
    {
        this.state = state;
        this.number = number;
        background.color = state.backgroundColor;
        text.color = state.textColor;
        text.text = number.ToString();

    }
    public void Spawn(TileCell cell)
    {
        if(this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;
        transform.position = cell.transform.position;
    }
    public void  Moveto(TileCell cell)
    {
        if(this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;
       // transform.position = cell.transform.position;
        StartCoroutine(Animate(cell.transform.position, false));

    }
    public void Merge(TileCell  Cell )
    {
        if(this.cell != null)
        {
            this.cell.tile = null;
        }
        this.cell = null;
        Cell.tile.locked = true;

        StartCoroutine(Animate(Cell.transform.position, true));

    }
    private IEnumerator Animate(Vector3 to, bool mering)
    {
        float elapsed = 0f;
        float duration = 0.1f;
        Vector3 form = transform.position;
        while(elapsed < duration)
        {
            transform.position = Vector3.Lerp(form, to, elapsed/ duration);
            elapsed += Time.deltaTime;
            yield return null;

        }
        transform.position = to;
        if(mering)
        {
            Destroy(gameObject); 
        }
    }
}
