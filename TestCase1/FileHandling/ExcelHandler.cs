using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestCase1.FileHandling;
using TestCase1.Models;

namespace TestCase1.Models
{
    class ExcelHandler : IFileHandler
    {
        private static IFileHandler instance;
        private static string PrimaryPath { get; set; }

        private ExcelHandler()
        {
            PrimaryPath = FileConfig.PrimaryPath;
        }

        public static IFileHandler GetInstance()
        {
            if (instance == null)
                instance = new ExcelHandler();
            return instance;
        }
        public List<List<string>> ReadFile(string fileName)
        {
            
            List<List<string>> data = new List<List<string>>();

            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Open(string.Format("{0}//{1}", PrimaryPath, fileName), false))
                {
                    SharedStringTable sharedStringTable = document.WorkbookPart.SharedStringTablePart.SharedStringTable;
                    foreach (WorksheetPart worksheetPart in document.WorkbookPart.WorksheetParts)
                    {
                        foreach (SheetData sheetData in worksheetPart.Worksheet.Elements<SheetData>())
                        {

                            data = ReadSheetData(sheetData, sharedStringTable);
                        }
                    }
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine("Ошибка считывания файла");

            }
            return data;
        }

        public List<List<string>> ReadSheetData(SheetData sheetData, SharedStringTable sharedStringTable)
        {
            List<List<string>> data = new List<List<string>>();
            int count = 0;

            if (sheetData.HasChildren)
            {
                foreach (Row row in sheetData.Elements<Row>())
                {
                    List<string> temp = new List<string>();
                    if (count < 1)
                    {
                        count++;
                        continue;
                    }
                    foreach (Cell cell in row.Elements<Cell>())
                    {
                        
                        if (cell.DataType == null)
                        {
                            temp.Add(cell.InnerText);
                        }
                        else if (cell.DataType == CellValues.SharedString)
                        {
                            temp.Add(sharedStringTable.ElementAt(Int32.Parse(cell.InnerText)).InnerText);
                        }
                        else
                        {
                            temp.Add(cell.InnerText);
                            
                        }
                    }
                    data.Add(temp);
                }
               
            }

            return data;
        }

        public void WriteToFile<T>(List<T> entities, string[] headers, string fileName) where T : IConvertable
        {
            try
            {
                using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName,
                    DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
                {

                    WorkbookPart workbookpart = document.AddWorkbookPart();
                    workbookpart.Workbook = new Workbook();
                    WorksheetPart worksheetPart = workbookpart.AddNewPart<WorksheetPart>();
                    worksheetPart.Worksheet = new Worksheet(new SheetData());
                    Sheets sheets = document.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                    Sheet sheet = new Sheet() { Id = document.WorkbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet" };
                    sheets.Append(sheet);
                    SheetData sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                    SetHeaders(headers, sheetData);
                    SetData<T>(entities, sheetData);

                    worksheetPart.Worksheet.Save();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка записи в файл");

            }
           


        }

        public SheetData SetHeaders(string[] headers, SheetData sheetData)
        {
            Row headerRow = new Row();

            for (int j = 0; j < headers.Length; j++)
            {
                Cell cell = new Cell() { DataType = CellValues.String, CellValue = new CellValue(headers[j]) };
                headerRow.Append(cell);
            }
            sheetData.Append(headerRow);

            return sheetData;
        }
        public SheetData SetData<T>(List<T> entities, SheetData sheetData) where T : IConvertable
        {
            foreach (IConvertable convertable in entities)
            {
                Row row = new Row();

                string[] convertedArray = convertable.ToArray();

                for (int j = 0; j < convertedArray.Length; j++)
                {
                    Cell cell = new Cell() { DataType = CellValues.String, CellValue = new CellValue(convertedArray[j]) };
                    row.Append(cell);
                }
                sheetData.Append(row);
            }

            return sheetData;
        }
    }
}




