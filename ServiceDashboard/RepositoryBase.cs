using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Dashboard.Model;
using System.Reflection;
using System.Linq;

namespace Dashboard.DataAccess
{
    public class RepositoryBase<T>
    {        

        /// <summary>
        /// Event handler for the Added item
        /// </summary>
        public event Action<T> ItemAdded;

        /// <summary>
        /// Event handler for the removed items
        /// </summary>
        public event Action<T> ItemRemoved;

        /// <summary>
        /// Event handler for updated items
        /// </summary>
        public event Action<T> ItemUpdated;

        /// <summary>
        /// The location of the data file, full filename required
        /// </summary>
        public virtual string DataFile { get; protected set; }

        /// <summary>
        /// The items list
        /// </summary>
        public List<T> Items;

        public RepositoryBase(string dataFile)
        {
            if (string.IsNullOrEmpty(dataFile))
                throw new ArgumentNullException("dataFile is empty.");
            DataFile = dataFile;
        }

        /// <summary>
        /// overrideable on item added procedure
        /// </summary>
        /// <param name="item">The item that is to be added</param>
        protected virtual void OnItemAdded(T item)
        {
            Action<T> handler = this.ItemAdded;
            if (handler != null)
            {
                if (item != null)
                    handler(item);
            }
        }

        protected virtual void OnItemRemoved(T item)
        {
            Action<T> handler = this.ItemRemoved;
            if (handler != null)
            {
                if (item != null)
                    handler(item);
            }
        }

        protected virtual void OnItemUpdated(T item)
        {
            Action<T> handler = this.ItemUpdated;
            if (handler != null)
            {
                if (item != null)
                    handler(item);
            }
        }

        public bool ContainsItem(T item)
        {
            return this.OnContainsItem(item);
        }

        protected virtual bool OnContainsItem(T item)
        {
            return (FindMatchingPropertyValues(item) != null);
        }

        T FindMatchingPropertyValues(T item)
        {
            T result = default(T);
            if (item == null)
                throw new ArgumentNullException("The item is empty");
            string[] keyProperties = (item as ModelBase<T>).GetValidatedProperties();
            if (keyProperties.Length <= 0 )            
                throw new ArgumentException("The keyProperty is empty.");
            object[] itemValues = PropertyValues(item, keyProperties);
            if (itemValues.Length <= 0)
                throw new ArgumentException("The key property value is empty.");
            foreach (T listItem in Items)
            {
                object[] values = PropertyValues(listItem, keyProperties);                
                bool matched = itemValues.OrderBy(a => a).SequenceEqual(values.OrderBy(a => a));
                if (matched)
                {
                    result = listItem;
                    break;
                }
            }
            return result;
        }

        object[] PropertyValues(object item, string[] propertyNames)
        {
            List<object> result = new List<object>();
            foreach (System.Reflection.PropertyInfo property in item.GetType().GetProperties())
            {
                foreach (string propertyName in propertyNames)
                {
                    if (property.Name.Equals(propertyName))
                    {
                        result.Add(item.GetType().GetProperty(property.Name).GetValue(item, null));
                        break;
                    }
                }
            }
            return result.ToArray();
        }

        public void Add(T item, bool autoSave = false)
        {
            this.OnAdd(item, autoSave);
        }

        public void Update(T item, bool autoSave)
        {
            this.OnUpdate(item, autoSave);
        }

        protected virtual void OnAdd(T item, bool autoSave = false)
        {
            try
            {
                if (!ContainsItem(item))
                {
                    Items.Add(item);
                    if (autoSave) Save();
                    OnItemAdded(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem adding the item." +
                    ex.Message);
            }
        }

        protected virtual void OnUpdate(T item, bool autoSave)
        {
            try
            {
                if (ContainsItem(item))
                {
                    Remove(item, false, false);
                    Add(item, false);
                    if (autoSave) Save();
                    OnItemUpdated(item);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem updating the item." +
                    ex.Message);
            }
        }

        public bool Remove(T item, bool autoSave, bool confirm)
        {
            return this.OnRemove(item, autoSave, confirm);
        }

        protected virtual bool OnRemove(T item, bool autoSave, bool confirm)
        {
            try
            {
                bool doRemove = true;
                if (confirm)
                    doRemove = OnRemoveConfirmation();
                if (doRemove)
                {
                    T itemToRemove = FindMatchingPropertyValues(item);
                    if (itemToRemove != null)
                    {
                        Items.Remove(itemToRemove);
                        if (autoSave) Save();
                        OnItemRemoved(itemToRemove);
                    }
                    else
                        doRemove = false;
                }
                return doRemove;
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem removing the item." +
                    ex.Message);
            }
        }

        protected virtual bool OnRemoveConfirmation()
        {
            string messageBoxText = "Are you sure you want to delete the selected item?";
            string caption = "Confirm";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        protected void Save()
        {
            this.OnSave();
        }

        protected virtual void OnSave()
        {
            try
            {                
                DashboardFunctions.SerializeToFile(Items, DataFile);
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem saving the data file." + 
                    ex.Message);
            }
        }

        public void Load()
        {
            this.OnLoad();
        }

        protected virtual void OnLoad()
        {
            if (File.Exists(DataFile))
            {
                try
                {
                    Items = DashboardFunctions.DeserializeFromFile<List<T>>(DataFile);
                    foreach (T item in Items)
                    {
                        OnItemAdded(item);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("There was a problem loading the data file." +
                        ex.Message);                    
                }
            }
            else
            {
                Items = new List<T>();
            }
        }

    }
}
