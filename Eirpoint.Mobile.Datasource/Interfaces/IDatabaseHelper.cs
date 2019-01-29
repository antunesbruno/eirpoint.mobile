namespace Eirpoint.Mobile.Datasource.Interfaces
{
    public interface IDatabaseHelper
    {
        void CreateDatabase(string absolutePath);
        void CreateTables();        
    }
}
