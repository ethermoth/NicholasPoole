using System.Collections;
using System.Collections.Generic;

public class Order {

    private List<BaseMenuItem> items = new List<BaseMenuItem>();

    public bool AddItem (BaseMenuItem m_Item) {
        items.Add(m_Item);
        return items.Contains(m_Item);
    }

    public bool RemoveItem (BaseMenuItem m_Item) {
        return items.Remove(m_Item);
    }

    public BaseMenuItem GetItem (int m_ItemIndex) {
        return items[m_ItemIndex];
    }

    public int GetItemIndex (BaseMenuItem m_Item) {
        return items.IndexOf(m_Item);
    }

    public List<BaseMenuItem> GetItems () {
        return items;
    }
}
