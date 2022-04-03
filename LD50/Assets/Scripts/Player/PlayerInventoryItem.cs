using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryItem : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer fgRend;
    [SerializeField]
    SpriteRenderer bgRend;


    [SerializeField]
    Rigidbody2D rb2d;
    [SerializeField]
    DistanceJoint2D joint;
    [SerializeField]
    Transform container;
    private bool started = false;
    private int number = 0;
    private int numberOff = 0;
    private int factor { get { return number % 2 == 0 ? 1 : -1; } }
    private bool wood = false;
    [SerializeField]
    private DriftWood driftPrefab;
    private Vector3 woodOffset = new Vector3(0f, -1f, 0f);
    private List<float> angles = new List<float>(){
        0,
        45,
        90,
        135,
        180,
        225,
        270,
        315
    };
    private void Start()
    {
        //joint.connectedBody = PlayerMovement.main.RB;
    }

    void LateUpdate()
    {
        if (started)
        {
            // move towards angle
            float angle = angles[number];
            float angleRad = angle * Mathf.Deg2Rad;
            float x = Mathf.Cos(angleRad);
            float y = Mathf.Sin(angleRad);
            Vector3 movement = new Vector3(x, y, 0f).normalized * 0.2f;
            //Vector3 offset = new Vector3(number * (factor * 0.1f), number * (factor * 0.01f), 0f);
            //Vector3 angle = new Vector3(0, 0, angles[number]);
            //Vector3 offset = angle * (0.1f + (numberOff * 0.1f));
            joint.connectedAnchor = PlayerMovement.main.transform.position + movement;
        }
        if (wood)
        {
            joint.connectedAnchor = PlayerMovement.main.transform.position;
        }
    }
    public void Init(Color fgColor, Sprite fgSprite, Sprite bgSprite, float scale, Transform parent, int number)
    {
        if (number > angles.Count - 1)
        {
            this.number = 0;
            numberOff = 1;
        }
        else
        {
            this.number = number;
        }
        rb2d.angularDrag = (number + 1f) * 0.2f;
        rb2d.drag = (number + 1f) * 0.2f;
        rb2d.mass = scale;
        transform.SetParent(parent);
        transform.localPosition = Vector2.zero;
        fgRend.sprite = fgSprite;
        fgRend.color = fgColor;
        bgRend.sprite = bgSprite;
        container.localScale = new Vector3(scale, scale, 1);
        started = true;
    }

    public void InitWood(Transform parent, Vector3 pos)
    {
        wood = true;
        transform.position = Vector3.zero;
        transform.SetParent(parent, true);
        transform.localScale = Vector3.one;
        container.position = PlayerMovement.main.transform.position + woodOffset;
        joint.distance = 1f;
        joint.connectedAnchor = PlayerMovement.main.transform.position;
        fgRend.maskInteraction = SpriteMaskInteraction.None;
        bgRend.maskInteraction = SpriteMaskInteraction.None;
        fgRend.sortingOrder = 50;
        bgRend.sortingOrder = 49;
        fgRend.sprite = driftPrefab.FG;
        fgRend.color = driftPrefab.Color;
        bgRend.sprite = driftPrefab.BG;
        container.localScale = new Vector3(1f, 1f, 1);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
