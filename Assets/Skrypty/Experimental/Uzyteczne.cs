using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Uzyteczne 
{
    private static Camera mainCamera;

    public static Vector3 GetMouseWorldPosition() // pozycja myszki
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        //Debug.Log(mouseWorldPosition);
        return mouseWorldPosition;
    }

    public static Vector3 GetRandomDir() // randomowy kierunek 
    {
        return new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector) // K¹t z vektora 
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }
}
