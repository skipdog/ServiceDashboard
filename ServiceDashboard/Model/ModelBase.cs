using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Windows.Media;

namespace Dashboard.Model
{
    [Serializable()]
    public abstract class ModelBase<T> : IDataErrorInfo
    {

        [XmlIgnore]
        public bool AutoUpdate { get; set; }

        public static T Add(params object[] args)
        {
            var instance = Activator.CreateInstance(typeof(T), args);
            return (T)instance;
        }        

        public string[] GetValidatedProperties()
        {
            return OnGetValidatedProperties();
        }

        protected virtual string[] OnGetValidatedProperties()
        {
            return null;
        }

        public string Error 
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get { return this.GetValidationError(columnName); }
        }

        public bool IsValid
        {
            get
            {
                foreach (string property in GetValidatedProperties())
                    if (GetValidationError(property) != null)
                        return false;

                return true;
            }
        }

        public string GetValidationError(string propertyName)
        {
            return OnGetValidationError(propertyName);
        }

        protected virtual string OnGetValidationError(string propertyName)
        {
            return null;
        }

    }
}
