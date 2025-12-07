using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomizer : MonoBehaviour
{
    public Renderer characterRenderer; // the mesh renderer that has material with _MainTex or specific texture slot
    public Texture2D[] faceTextures; // assign user's face images or face textures
    public Button[] faceButtons;

    void Start()
    {
        // bind buttons
        for (int i = 0; i < faceButtons.Length; i++)
        {
            int idx = i;
            faceButtons[i].onClick.AddListener(() => ApplyFace(idx));
        }
    }

    void ApplyFace(int index)
    {
        if (index < 0 || index >= faceTextures.Length) return;
        Material mat = characterRenderer.material;
        mat.mainTexture = faceTextures[index];
    }
}
