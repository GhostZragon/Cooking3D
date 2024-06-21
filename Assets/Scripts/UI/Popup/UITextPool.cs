using UnityEngine;

public class UITextPool : UnityPool<UITextPopup>
{
    
    public override UITextPopup Get()
    {
        return pool.Get();
    }

    protected override void OnDestroy(UITextPopup text)
    {
        Object.Destroy(text.gameObject);
    }

    protected override void OnRelease(UITextPopup text)
    {
        text.gameObject.SetActive(false);
    }

    protected override void OnGet(UITextPopup text)
    {
        text.gameObject.SetActive(true);
    }

    protected override UITextPopup OnCreate()
    {
        var text =  Object.Instantiate(prefab,parent);

        return text;
    }


    public UITextPool(UITextPopup prefab, int size = 5, Transform parent = null) : base(prefab, size, parent)
    {
    }
}