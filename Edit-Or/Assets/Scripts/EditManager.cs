using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditManager : MonoBehaviour
{
    #region Singleton

    public static EditManager instance;
    private void Awake()
    {
        instance = this;
    }

    #endregion
    [Header("Editable Objects")]
    public EditableObject[] editableObjects;

    public void SelectNewWall()
    {
        foreach(EditableObject item in editableObjects)
        {
            item.Deselect();
        }
    }

    public void DeselectAllWalls()
    {
        foreach(EditableObject item in editableObjects)
        {
            item.isDeselectingAll = true;
            item.Deselect();
        }
    }

}
