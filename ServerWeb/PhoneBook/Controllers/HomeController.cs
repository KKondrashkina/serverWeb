using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace PhoneBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly PhoneBookContext db;

        public HomeController(PhoneBookContext context)
        {
            db = context;
        }

        public IActionResult Index(SortState sortOrder, string searchString)
        {
            var contactsDto = db.Contacts
                .Select(c => c.ToDto())
                .ToList()
                .AsQueryable();

            ViewData["SearchString"] = searchString;
            ViewData["NameSort"] = sortOrder == SortState.NameAscending ? SortState.NameDescending : SortState.NameAscending;
            ViewData["LastNameSort"] = sortOrder == SortState.LastNameAscending ? SortState.LastNameDescending : SortState.LastNameAscending;
            ViewData["PhoneNumberSort"] = sortOrder == SortState.PhoneNumberAscending ? SortState.PhoneNumberDescending : SortState.PhoneNumberAscending;

            contactsDto = sortOrder switch
            {
                SortState.NameDescending => contactsDto.OrderByDescending(c => c.Name),
                SortState.LastNameAscending => contactsDto.OrderBy(c => c.LastName),
                SortState.LastNameDescending => contactsDto.OrderByDescending(c => c.LastName),
                SortState.PhoneNumberAscending => contactsDto.OrderBy(c => c.PhoneNumber),
                SortState.PhoneNumberDescending => contactsDto.OrderByDescending(c => c.PhoneNumber),
                _ => contactsDto.OrderBy(c => c.Name),
            };

            if (searchString != null)
            {
                contactsDto = contactsDto
                    .Select(n => n)
                    .Where(c => (c.Name.Contains(searchString) || c.LastName.Contains(searchString) || c.PhoneNumber.Contains(searchString)));
            }

            return View(contactsDto);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ContactDto contactDto)
        {
            var contactsWithSamePhoneCount = db.Contacts
                .Select(n => n)
                .Count(c => c.PhoneNumber.Contains(contactDto.PhoneNumber));

            ViewData["PhonesCount"] = contactsWithSamePhoneCount;

            if (contactsWithSamePhoneCount > 0)
            {
                return View(contactDto);
            }

            if (ModelState.IsValid)
            {
                await db.Contacts.AddAsync(contactDto.ToModel());
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(contactDto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await db.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            db.Contacts.Remove(contact);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult DeleteChecked([FromBody]DeletedContacts contacts)
        {
            foreach (var id in contacts.Ids)
            {
                var contact = db.Contacts.Find(Convert.ToInt32(id));

                db.Contacts.Remove(contact);
            }

            db.SaveChanges();

            return Json(true);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contactDto = (await db.Contacts.FirstOrDefaultAsync(p => p.Id == id)).ToDto();

            if (contactDto != null)
            {
                return View(contactDto);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ContactDto contactDto)
        {
            var contactsWithSamePhoneCount = db.Contacts
                .Select(n => n)
                .Where(n => n.Id != contactDto.Id)
                .Count(c => c.PhoneNumber.Contains(contactDto.PhoneNumber));

            ViewData["IsExist"] = contactsWithSamePhoneCount;

            if (contactsWithSamePhoneCount > 0 || !ModelState.IsValid)
            {
                return View(contactDto);
            }

            db.Contacts.Update(contactDto.ToModel());
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UploadToExcel(string fileName)
        {
            var contacts = db.Contacts;

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].LoadFromCollection(contacts, true);
                worksheet.Row(1).Style.Font.Bold = true;

                var cells = worksheet.Cells[worksheet.Dimension.Address];
                cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                cells.AutoFitColumns();

                var file = new FileInfo($"{fileName}.xlsx");
                await excelPackage.SaveAsAsync(file);
            }

            return RedirectToAction("Index");
        }
    }
}
