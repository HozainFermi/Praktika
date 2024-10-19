using Praktika.Interfaces;

namespace Praktika.Services
{
    public class CsvExportService:IExportService
    {
        private List<string> strings;
        public CsvExportService(List<string> data) { strings = data; }

        public async Task<Stream> ExportTableData()
        {

            using (var stream = new MemoryStream()) 
            {
                
            
            }
            throw new NotImplementedException();
        }
    }
}
