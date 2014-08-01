using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Dashboard.Model
{
    [Serializable(), XmlRoot(ElementName = "Task")]
    public class ModelTask : ModelBase<ModelTask>
    {
        public string MachineName { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }

        public string TaskName { get; set; }

        public string Description { get; set; }

        public string AlertStatus { get; set; }

        [XmlIgnore]
        public bool IsOnAlert 
        { 
            get { return (TaskStatus!=null && 
                AlertStatus!=null && AlertStatus.ToLower().Equals(TaskStatus.ToLower())); } 
        }

        [XmlIgnore]
        public string LastRan { get; set; }

        [XmlIgnore]
        public string NextRun { get; set; }

        [XmlIgnore]
        public string TaskStatus { get; set; }

        public ModelTask()
        {
            AutoUpdate = true;
        }

        public ModelTask(bool autoUpdate)
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
                    case "TaskName":
                        error = this.ValidateTaskName();
                        break;
                    case "Description":
                        error = this.ValidateDescription();
                        break;
                    case "Password":
                        error = this.ValidatePassword();
                        break;
                    default:
                        Debug.Fail("Unexpected property being validated on Task: " + propertyName);
                        break;
                }
            }
            return error;
        }

        protected override string[] OnGetValidatedProperties()
        {
            return new string[] { "MachineName", "TaskName", "Description", "Password", };
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

        string ValidateTaskName()
        {
            if (String.IsNullOrEmpty(this.TaskName))
            {
                return "The TaskName is empty";
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

        string ValidatePassword()
        {
            if ((!string.IsNullOrEmpty(this.UserName)) && String.IsNullOrEmpty(this.Password))
            {
                return "The Password is empty";
            }
            else
                return null;
        }

    }
}
