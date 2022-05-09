using FMODUnity;
using System;
using UnityEngine;

public static class Library
{
    public static bool CompareLayer(int layer, LayerMask layerMask)
    {
        return (layerMask.value & (1 << layer)) != 0;
    }

    public static void Play3DSound(EventReference sound, Transform transform)
    {
        FMOD.Studio.EventInstance instance = RuntimeManager.CreateInstance(sound);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        instance.start();
    }
    public static void Play2DSound(EventReference sound)
    {
        FMOD.Studio.EventInstance instance = RuntimeManager.CreateInstance(sound);
        instance.start();
    }

    public static Vector2 ToAxisAndNormalize(Vector2 vector)
    {
        Vector2 normVector = vector.normalized;
        Vector2 vectorX = new Vector2(normVector.x, 0f);
        Vector2 vectorY = new Vector2(0f, normVector.y);
        if (vectorX.magnitude >= vectorY.magnitude) return vectorX.normalized;
        return vectorY.normalized;




        /*

        vector.Normalize();
        // Первый квадрант
        if (vector.x > 0 && vector.y >= 0)
        {
            if (vector.x >= vector.y) return Vector2.right;
            else return Vector2.up;
        }
        // Второй квадрант
        if (vector.x <= 0 && vector.y > 0)
        {
            if (Mathf.Abs(vector.x) >= vector.y) return Vector2.left;
            else return Vector2.up;
        }
        // Третий квадрант
        if (vector.x < 0 && vector.y <= 0)
        {
            if (Mathf.Abs(vector.x) >= Mathf.Abs(vector.y)) return Vector2.left;
            else return Vector2.down;
        }
        // Четвертый квадрант
        if (vector.x >= 0 && vector.y < 0)
        {
            if (vector.x >= Mathf.Abs(vector.y)) return Vector2.right;
            else return Vector2.down;
        }
        return Vector2.zero;

        */



    }

    public static void SetSlotPosition(Icon icon, Transform newPosition, bool forEquipment)
    {
        icon.transform.SetParent(newPosition);
        icon.transform.position = newPosition.position;
        if (forEquipment && !icon.equipped) {icon.PutOn(); return;}
        if (!forEquipment && icon.equipped) {icon.PutOff(); return;}
        if (forEquipment && icon.equipped) { icon.PutOff(); icon.PutOn(); }

    }

   
}
