using NPOI.SS.UserModel;

namespace PersonasMS.Infraestructure.API.Validations
{
    public static class ExcelValidation
    {
        public static T GetCellValue<T>(ICell cell, T defaultValue = default)
        {
            if (cell == null)
            {
                return defaultValue;
            }

            var cellType = cell.CellType;

            if (cellType == CellType.Blank)
            {
                return defaultValue;
            }

            if (typeof(T) == typeof(string))
            {
                if (cellType == CellType.String)
                {
                    return (T)(object)cell.StringCellValue;
                }

                if (cellType == CellType.Numeric)
                {
                    return (T)(object)cell.NumericCellValue.ToString();
                }

                return defaultValue;
            }

            if (typeof(T) == typeof(double))
            {
                if (cellType == CellType.Numeric)
                {
                    return (T)(object)cell.NumericCellValue;
                }

                if (cellType == CellType.String && double.TryParse(cell.StringCellValue, out var doubleValue))
                {
                    return (T)(object)doubleValue;
                }

                return defaultValue;
            }

            if (typeof(T) == typeof(int))
            {
                if (cellType == CellType.Numeric)
                {
                    return (T)(object)(int)cell.NumericCellValue;
                }

                if (cellType == CellType.String && int.TryParse(cell.StringCellValue, out var intValue))
                {
                    return (T)(object)intValue;
                }

                return defaultValue;
            }

            if (typeof(T) == typeof(DateTime))
            {
                if (cellType == CellType.Numeric)
                {
                    return (T)(object)cell.DateCellValue;
                }

                if (cellType == CellType.String && DateTime.TryParse(cell.StringCellValue, out var dateTimeValue))
                {
                    return (T)(object)dateTimeValue;
                }

                return defaultValue;
            }

            return defaultValue;
        }

    }
}
