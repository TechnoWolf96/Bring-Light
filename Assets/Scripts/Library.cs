using FMODUnity;
using UnityEngine;

public static class Library
{
    public static void Play3DSound(EventReference sound, Transform transform)
    {
        FMOD.Studio.EventInstance instance = RuntimeManager.CreateInstance(sound);
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        instance.start();
    }
    public static Vector2 ToAxisAndNormalize(Vector2 vector)
    {
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
    }

}
