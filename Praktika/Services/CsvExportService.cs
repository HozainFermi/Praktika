using Praktika.Interfaces;
using Praktika.Models;
using CsvHelper;
using System.Globalization;
using Microsoft.OpenApi.Validations;
using System.IO;
using System.Text;

namespace Praktika.Services
{
    public class CsvExportService:IExportService
    {
        private List<string> data;
        
        public async Task<byte[]> ExportTableData(ExportRequestModel req)
        {
            data = req.Data;
           // byte[] filesContent;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
                {
                    // Write CSV data to the stream
                    for(int i = 0; i < data.Count; i++)
                    {
                        await streamWriter.WriteLineAsync(data[i]);
                    }
                }
                // Convert MemoryStream to byte array
                byte[] csvData = memoryStream.ToArray();

                return csvData;
               
            }
        }
    }
}
