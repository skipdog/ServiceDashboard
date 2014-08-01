using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.ComponentModel;

namespace Dashboard.ViewModel
{
    class SQLDialogViewModel : WorkspaceViewModel<object>, IDataErrorInfo
    {
        string IDataErrorInfo.Error { get { return /*(service as IDataErrorInfo).Error*/null; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;
                //error = (service as IDataErrorInfo)[propertyName];
                //CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }
    }
}
