using Praktika.Models;

namespace Praktika.Interfaces
{
    public interface IExportService
    {
        public Task<byte[]> ExportTableData(ExportRequestModel req);
    }
}
