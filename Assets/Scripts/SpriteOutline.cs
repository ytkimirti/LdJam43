using UnityEngine;

[ExecuteInEditMode]
public class SpriteOutline : MonoBehaviour {
    public Color color = Color.white;

    public bool isOutlineActive = false;
    private bool memActive;
    
    public Material defaultMaterial;
    public Material outlineMaterial;
    
    [Range(0, 16)]
    public int outlineSize = 1;

    public SpriteRenderer spriteRenderer;

    public void EnableOutline(bool enable)
    {
        if (enable == memActive)
            return;
        
        memActive = enable;
        
        if (enable)
        {
            spriteRenderer.material = outlineMaterial;
            UpdateOutline(true);
        }
        else
        {
            spriteRenderer.material = defaultMaterial;
            UpdateOutline(false);
        }
    }

    private void Update()
    {
        EnableOutline(isOutlineActive);
        isOutlineActive = false;
    }

    void UpdateOutline(bool outline) {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        spriteRenderer.GetPropertyBlock(mpb);
        
        mpb.SetFloat("_Outline", outline ? 1f : 0);
        mpb.SetColor("_OutlineColor", color);
        mpb.SetFloat("_OutlineSize", outlineSize);
        spriteRenderer.SetPropertyBlock(mpb);
    }
}
