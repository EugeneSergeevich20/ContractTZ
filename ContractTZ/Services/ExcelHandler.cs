using ContractTZ1.Models;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace ContractTZ.Services
{
    public class ExcelHandler
    {

        IWorkbook workbook;

        public ExcelHandler(Stream stream, string fileEx)
        {
            if (fileEx == ".xlsx")
            {
                workbook = new XSSFWorkbook(stream);
            } 
            else if (fileEx == ".xls")
            {
                workbook = new HSSFWorkbook(stream);
            }
            else
            {
                return;
            }
        }

        ~ExcelHandler()
        {
            workbook.Close();
        }

        public List<Contract> GetContractsFromExcel()
        {
            try
            {
                List<Contract> contracts = new List<Contract>();
                for (int s = 0; s < workbook.NumberOfSheets; s++)
                {
                    ISheet sheet = workbook.GetSheetAt(s);

                    if (sheet != null)
                    {
                        if (sheet.SheetName == "ДОГОВОРЫ")
                        {
                            List<string> headersString = new List<string>();

                            foreach (var HeadCell in sheet.GetRow(0))
                            {
                                headersString.Add(GetCellValue(HeadCell).ToString());
                            }

                            for (int i = 1; i <= sheet.LastRowNum; i++)
                            {
                                Contract contract = new Contract();
                                foreach (var cell in sheet.GetRow(i))
                                {
                                    if (cell.ColumnIndex < headersString.Count)
                                    {
                                        string curHead = headersString.ElementAt(cell.ColumnIndex);
                                        switch (curHead)
                                        {
                                            case "ИДЕНТИФИКАТОР":
                                                contract.id = Convert.ToInt32(GetCellValue(cell));
                                                break;
                                            case "ШИФР ДОГОВОРА":
                                                contract.contractCode = GetCellValue(cell).ToString();
                                                break;
                                            case "НАИМЕНОВАНИЕ ДОГОВОРА":
                                                contract.contractName = GetCellValue(cell).ToString();
                                                break;
                                            case "ЗАКАЗЧИК":
                                                contract.customer = GetCellValue(cell).ToString();
                                                break;
                                        }
                                    }
                                }
                                contracts.Add(contract);
                            }

                        }
                        else if (sheet.SheetName == "ЭТАПЫ ДОГОВОРА")
                        {
                            List<string> headersString = new List<string>();

                            foreach (var HeadCell in sheet.GetRow(0))
                            {
                                headersString.Add(GetCellValue(HeadCell).ToString());
                            }

                            for (int i = 1; i <= sheet.LastRowNum; i++)
                            {
                                ContractStage stage = new ContractStage();

                                int contractId = 0;

                                foreach (var cell in sheet.GetRow(i))
                                {
                                    if (cell.ColumnIndex < headersString.Count)
                                    {
                                        string curHead = headersString.ElementAt(cell.ColumnIndex);
                                        switch (curHead)
                                        {
                                            case "ИДЕНТИФИКАТОР ДОГОВОРА":
                                                contractId = Convert.ToInt32(GetCellValue(cell));
                                                break;
                                            case "НАИМЕНОВАНИЕ ЭТАПА":
                                                stage.nameStage = GetCellValue(cell).ToString();
                                                break;
                                            case "ДАТА НАЧАЛА":
                                                stage.startDate = FromExcelSerialDate(Convert.ToInt32(GetCellValue(cell)));
                                                break;
                                            case "ДАТА ОКОНЧАНИЯ":
                                                stage.stopDate = FromExcelSerialDate(Convert.ToInt32(GetCellValue(cell)));
                                                break;
                                        }
                                    }
                                }
                                try
                                {
                                    contracts.Where(c => c.id == contractId).First().contractStages.Add(stage);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("IMPORT ERROR: " + e.Message);
                                }

                            }

                        }

                    }

                }
                contracts.ForEach(c => c.id = 0);
                return contracts;
            }
            catch (Exception e)
            {
                Console.WriteLine("IMPORT ERROR: " + e.Message);
                return null;
            }
        }

        private object GetCellValue(ICell cell)
        {
            object cellValue;

            switch (cell.CellType)
            {
                case CellType.Numeric:
                    cellValue = cell.NumericCellValue;
                    break;
                case CellType.String:
                    cellValue = cell.StringCellValue;
                    break;
                case CellType.Blank:
                    cellValue = null;
                    break;
                case CellType.Boolean:
                    cellValue = cell.BooleanCellValue;
                    break;
                case CellType.Error:
                    cellValue = cell.ErrorCellValue;
                    break;
                case CellType.Formula:
                    cellValue = cell.CellFormula;
                    break;
                case CellType.Unknown:
                    cellValue = null;
                    break;
                default:
                    cellValue = null;
                    break;
            }
            return cellValue;
        }

        private DateTime FromExcelSerialDate(int SerialDate)
        {
            if (SerialDate > 59)
            {
                SerialDate -= 1;
            }
            return new DateTime(1899, 12, 31).AddDays(SerialDate);
        }

    }
}
