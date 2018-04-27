using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseMenuItem", menuName = "Menu Item/Base Item")]
public class BaseMenuItem : ScriptableObject {

    [SerializeField] string itemName;
    [SerializeField] int reward;
    [SerializeField] List<BaseItemComponent> itemComponents;

    // Debugging
    public bool debug;

    // Use this for initialization
    public virtual void Start () {
        if (debug)
            Debug.Log(string.Format("{0} ({1}) Start() called", itemName, GetType()));
    }

    // Update is called once per frame
    public virtual void Update () {
        if (debug)
            Debug.Log(string.Format("{0} ({1}) Update() called", itemName, GetType()));
    }

    public virtual void FixedUpdate () {
        if (debug)
            Debug.Log(string.Format("{0} ({1}) FixedUpdate() called", itemName, GetType()));
    }

    public string GetName () { return itemName; }
    public void SetName (string aName) { itemName = aName; }
    public int GetReward () { return reward; }
    public void SetReward (int aReward) { reward = aReward; }
    public List<BaseItemComponent> GetItemComponents () { return itemComponents; }
    public void SetItemComponents (List<BaseItemComponent> aComponents) { itemComponents = aComponents; }

    public virtual string GetItemDetails () {
        string retStr = "";
        retStr += itemName;

        if (itemComponents.Count > 0) {
            retStr += "\nComponents:";
            foreach (BaseItemComponent comp in itemComponents) {
                retStr += "\n\t- " + comp.componentName;
            }
        }

        retStr += "\n\nReward: $" + reward;

        return retStr;
    }
}
