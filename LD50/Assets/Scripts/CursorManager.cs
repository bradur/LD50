using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CursorManager : MonoBehaviour
{
    public static CursorManager main;


    private void Awake()
    {
        main = this;
    }

    [SerializeField]
    private List<GameCursor> cursors = new List<GameCursor>();

    private GameCursor currentCursor;

    [SerializeField]
    private CursorType defaultCursorType;


    void Start()
    {
        Show(defaultCursorType);
    }
    void Update()
    {
        Cursor.visible = false;
        if (PlayerFishingControl.main.IsFishing)
        {
            if (HookShooter.main.CanShoot)
            {
                Show(CursorType.Target);
            }
            else
            {
                Show(CursorType.CantTarget);
            }
        }
        else if (PlayerFishingControl.main.MousePositionIsFishable())
        {
            if (PlayerFishingControl.main.CanFish())
            {
                Show(CursorType.Fishing);
            }
            else
            {
                Show(CursorType.CantFishWhileWalking);
            }
        }
        else
        {
            Show(CursorType.Default);
        }
    }

    void LateUpdate()
    {
        transform.position = mousePos;
    }
    private Vector2 mousePos
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    public void Show(CursorType cursorType)
    {
        GameCursor newCursor = cursors.Find(cursor => cursor.CursorType == cursorType);
        ShowCursor(newCursor);
    }

    private void ShowCursor(GameCursor newCursor)
    {
        if (newCursor == null)
        {
            return;
        }
        if (currentCursor != null)
        {
            if (currentCursor.CursorType == newCursor.CursorType)
            {
                return;
            }
            currentCursor.Hide();
        }
        newCursor.Show(transform);
        currentCursor = newCursor;
    }

}

[System.Serializable]
public class GameCursor
{
    public CursorType CursorType;
    public GameObject Prefab;
    private GameObject instance;

    private void Create(Transform parent)
    {
        if (instance == null)
        {
            instance = GameObject.Instantiate(Prefab, Vector2.zero, Quaternion.identity, parent);
            instance.transform.localPosition = Vector2.zero;
        }
    }
    public void Hide()
    {
        instance.SetActive(false);
    }

    public void Show(Transform parent)
    {
        Create(parent);
        instance.SetActive(true);
    }
}

public enum CursorType
{
    Default,
    Fishing,
    CantFishWhileWalking,
    Target,
    CantTarget,
}
