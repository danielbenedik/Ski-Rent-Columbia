using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace formSkiRentColumbia
{
    class MyExel
    {
        string path;
        _Excel.Application excelFile = new _Excel.Application();
        _Excel.Workbook workBook;
        _Excel.Worksheet workSheet_orders;
        _Excel.Worksheet workSheet_storage;

        public MyExel(string _path)
        {
            path = _path;
        }
        public void OpenFile()
        {
            workBook = excelFile.Workbooks.Open(path);
            workSheet_orders = workBook.Worksheets[1];
            workSheet_storage = workBook.Worksheets[2];
        }
        public void CloseFile(bool toSaveData = false)
        {
            workBook.Close(toSaveData);
        }
      
        public Workbook WorkBook { get => workBook; set => workBook = value; }
        public Worksheet WorkSheet_orders { get => workSheet_orders; set => workSheet_orders = value; }
        public Worksheet WorkSheet_storage { get => workSheet_storage; set => workSheet_storage = value; }
    }
}
