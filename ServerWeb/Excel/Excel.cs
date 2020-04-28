using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml.Style;

namespace Excel
{
    class Excel
    {
        static void Main(string[] args)
        {
            var persons = new List<Person>();

            for (var i = 0; i < 10; i++)
            {
                var person = new Person
                {
                    Name = "Name " + i,
                    LastName =  "Last Name " + i,
                    Age = i * 2,
                    PhoneNumber =  "8-800-555-35-3" + i
                };

                persons.Add(person);
            }

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].LoadFromCollection(persons, true);
                worksheet.Row(1).Style.Font.Bold = true;

                var cells = worksheet.Cells[worksheet.Dimension.Address];
                cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cells.AutoFitColumns();

                var file = new FileInfo("PersonsTable.xlsx");
                excelPackage.SaveAs(file);
            }
        }
    }
}
