using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Dashboard.Model
{
    [Serializable(), XmlRoot(ElementName = "Service")]
    public class ModelService : ModelBase<ModelService>
    {

        public string MachineName { get; set; }

        public string ServiceName { get; set; }

        public string Description { get; set; }

        public string AlertStatus { get; set; }

        [XmlIgnore]
        public bool IsOnAlert { get { return ( ServiceStatus != null && AlertStatus!= null && 
            AlertStatus.ToLower().Equals(ServiceStatus.ToLower())); } }

        [XmlIgnore]
        public string ServiceStatus { get; set; }

        public ModelService()
        {
            AutoUpdate = true;
        }

        public ModelService(bool autoUpdate)
        {
            AutoUpdate = autoUpdate;
        }

        protected override string OnGetValidationError(string propertyName)
        {
            string error = null;

            if (Array.IndexOf(GetValidatedProperties(), propertyName) >= 0)
            {
                switch (propertyName)
                {
                    case "MachineName":
                        error = this.ValidateMachineName();
                        break;
                    case "ServiceName":
                        error = this.ValidateServiceName();
                        break;
                    case "Description":
                        error = this.ValidateDescription();
                        break;
                    default:
                        Debug.Fail("Unexpected property being validated on Service: " + propertyName);
                        break;
                }
            }
            return error;
        }

        protected override string[] OnGetValidatedProperties()
        {
            return new string[] { "MachineName", "ServiceName", "Description", };
        }

        string ValidateMachineName()
        {
            if (String.IsNullOrEmpty(this.MachineName))
            {
                return "The MachineName is empty";
            }
            else
                return null;
        }

        string ValidateServiceName()
        {
            if (String.IsNullOrEmpty(this.ServiceName))
            {
                return "The ServiceName is empty";
            }
            else
                return null;
        }

        string ValidateDescription()
        {
            if (String.IsNullOrEmpty(this.Description))
            {
                return "The Description is empty";
            }
            else
                return null;
        }

    }
}
