namespace Praktika.Interfaces
{
    public interface IExportService
    {
        public Task<Stream> ExportTableData();
    }
}
