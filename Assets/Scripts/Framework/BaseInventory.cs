using System.Collections.Generic;

namespace frameworks.inventory
{
    public class BaseInventory<T> where T : struct
    {
        protected Dictionary<string, T> items = new Dictionary<string, T>();

        public int Count => items.Count;

        public void SetItem(string item_id, T value)
        {
            if (!HasItem(item_id))
            {
                items.Add(item_id, value);
            }
            else
            {
                items[item_id] = value;
            }
        }

        public T GetItem(string item_id)
        {
            if (HasItem(item_id))
            {
                return items[item_id];
            }
            return default;
        }

        public void DeleteItem(string item_id)
        {
            if (HasItem(item_id))
            {
                items.Remove(item_id);
            }
        }

        public bool HasItem(string item_id)
        {
            return items.ContainsKey(item_id);
        }

        public string GetItemByIndex(int index)
        {
            int count = 0;
            foreach (var item in items)
            {
                if (count++ == index)
                {
                    return item.Key;
                }
            }

            return string.Empty;
        }
    }
}