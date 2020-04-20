using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

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

                var cells = worksheet.Cells[worksheet.Dimension.Address];
                cells.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                cells.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                cells.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                cells.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;


                var file = new FileInfo("PersonsTable.xlsx");

                excelPackage.SaveAs(file);
            }
        }
    }
}
