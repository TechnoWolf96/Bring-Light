using UnityEngine;

public class MaterialSoundArea : MonoBehaviour
{
    public MaterialSound areaMaterial;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.TryGetComponent(out Footsteps footsteps);
        if (footsteps != null) footsteps.currentMaterial = areaMaterial;
    }




}
