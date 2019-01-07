using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Interfaces
{
    public interface IDatabaseHelper
    {
        void CreateDatabase();
        void CreateTables();
    }
}
