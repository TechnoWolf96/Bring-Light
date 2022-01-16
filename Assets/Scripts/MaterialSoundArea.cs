using UnityEngine;

public class MaterialSoundArea : MonoBehaviour
{
    public MaterialSound areaMaterial;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Work");
        Footsteps footsteps;
        collision.TryGetComponent(out footsteps);
        if (footsteps != null) footsteps.currentMaterial = areaMaterial;
    }




}
