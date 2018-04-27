using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseItemComponent", menuName = "Item Component/Base Item Component")]
public class BaseItemComponent : ScriptableObject {
    public string componentName;
    public GameObject model;
    public List<Property> properties;
    
    [System.Serializable]
    public class Property {
        public string propertyName;
        public enum PropertyType {
            Int,
            Float,
            String,
            Bool
        }
        public PropertyType propertyType;

        // property type values
        private int propInt;
        private float propFloat;
        private string propString;
        private bool propBool;

        public object GetValue () {
            object returnData = null;
            switch (propertyType) {
                case PropertyType.Int: returnData = propInt; break;
                case PropertyType.Float: returnData = propFloat; break;
                case PropertyType.String: returnData = propString; break;
                case PropertyType.Bool: returnData = propBool; break;
            }
            return returnData;
        }
        public void SetValue (object aObjectValue) {
            switch (propertyType) {
                case PropertyType.Int: propInt = (int)aObjectValue; break;
                case PropertyType.Float: propFloat = (float)aObjectValue; break;
                case PropertyType.String: propString = (string)aObjectValue; break;
                case PropertyType.Bool: propBool = (bool)aObjectValue; break;
            }
        }
    }
}
