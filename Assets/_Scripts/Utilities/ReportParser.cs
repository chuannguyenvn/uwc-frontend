using System;
using System.IO;
using Commons.Communications.Reports;
using NPOI.XSSF.UserModel;
using UnityEngine;

namespace Utilities
{
    public static class ReportParser
    {
        public static void SaveReport(GetReportFileResponse response)
        {
            using var workbook = new XSSFWorkbook();

            CreateMcpFillLevelsSheet(response, workbook);
            CreateEmptyingRecordsSheet(response, workbook);

            Directory.GetParent(Application.dataPath).CreateSubdirectory("Reports");
            string xlsxPath = Path.Combine(Directory.GetParent(Application.dataPath) + "/Reports/",
                DateTime.Now.AddDays(-7).ToString("yyyy-MM-ddTHHmmss") + " " + DateTime.Now.ToString("yyyy-MM-ddTHHmmss") + ".xlsx");

            using var fileStream = new FileStream(xlsxPath, FileMode.Create, FileAccess.Write);
            workbook.Write(fileStream);

            Debug.Log("Done");
        }

        private static void CreateMcpFillLevelsSheet(GetReportFileResponse response, XSSFWorkbook workbook)
        {
            var mcpFillLevelSheet = workbook.CreateSheet("MCPs fill level");

            var font = workbook.CreateFont();
            font.FontHeightInPoints = 11;
            font.FontName = "Calibri";
            font.IsBold = true;
            var boldStyle = workbook.CreateCellStyle();
            boldStyle.SetFont(font);

            boldStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            boldStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            boldStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            boldStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            boldStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            boldStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;

            var headerRow = mcpFillLevelSheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("ID");
            headerRow.CreateCell(1).SetCellValue("Timestamp");
            headerRow.CreateCell(2).SetCellValue("MCP ID");
            headerRow.CreateCell(3).SetCellValue("MCP Address");
            headerRow.CreateCell(4).SetCellValue("Fill level");

            for (int i = 0; i < 5; i++)
            {
                headerRow.GetCell(i).CellStyle = boldStyle;
            }

            var columnStyle = workbook.CreateCellStyle();
            columnStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            columnStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            var lastRowStyle = workbook.CreateCellStyle();
            lastRowStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            lastRowStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            lastRowStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

            for (int i = 0; i < response.McpFillLevelTimestamps.Count; i++)
            {
                var row = mcpFillLevelSheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(i + 1);
                row.CreateCell(1).SetCellValue(response.McpFillLevelTimestamps[i].ToLocalTime().ToString());
                row.CreateCell(2).SetCellValue(response.FillLevelMcpIds[i]);
                row.CreateCell(3).SetCellValue(response.FillLevelMcpAddresses[i]);
                row.CreateCell(4).SetCellValue((response.McpFillLevelValues[i] * 100).ToString("F2") + "%");

                for (int j = 0; j < 5; j++)
                {
                    row.GetCell(j).CellStyle = columnStyle;
                    if (i == response.McpFillLevelTimestamps.Count - 1)
                        row.GetCell(j).CellStyle = lastRowStyle;
                }
            }

            mcpFillLevelSheet.SetColumnWidth(0, 255 * 8);
            mcpFillLevelSheet.SetColumnWidth(1, 255 * 25);
            mcpFillLevelSheet.SetColumnWidth(2, 255 * 12);
            mcpFillLevelSheet.SetColumnWidth(3, 255 * 36);
            mcpFillLevelSheet.SetColumnWidth(4, 255 * 12);
        }

        private static void CreateEmptyingRecordsSheet(GetReportFileResponse response, XSSFWorkbook workbook)
        {
            var emptyingLogsSheet = workbook.CreateSheet("Emptying logs");

            var font = workbook.CreateFont();
            font.FontHeightInPoints = 11;
            font.FontName = "Calibri";
            font.IsBold = true;
            var boldStyle = workbook.CreateCellStyle();
            boldStyle.SetFont(font);

            boldStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            boldStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            boldStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            boldStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            boldStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
            boldStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;

            var headerRow = emptyingLogsSheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("ID");
            headerRow.CreateCell(1).SetCellValue("Timestamp");
            headerRow.CreateCell(2).SetCellValue("MCP ID");
            headerRow.CreateCell(3).SetCellValue("MCP Address");
            headerRow.CreateCell(4).SetCellValue("Worker ID");
            headerRow.CreateCell(5).SetCellValue("Worker Name");

            for (int i = 0; i < 6; i++)
            {
                headerRow.GetCell(i).CellStyle = boldStyle;
            }

            var columnStyle = workbook.CreateCellStyle();
            columnStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            columnStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;

            var lastRowStyle = workbook.CreateCellStyle();
            lastRowStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            lastRowStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            lastRowStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;

            for (int i = 0; i < response.McpEmptiedTimestamps.Count; i++)
            {
                var row = emptyingLogsSheet.CreateRow(i + 1);
                row.CreateCell(0).SetCellValue(i + 1);
                row.CreateCell(1).SetCellValue(response.McpEmptiedTimestamps[i].ToLocalTime().ToString());
                row.CreateCell(2).SetCellValue(response.EmptyingMcpIds[i]);
                row.CreateCell(3).SetCellValue(response.EmptyingMcpAddresses[i]);
                row.CreateCell(4).SetCellValue(response.WorkerIds[i]);
                row.CreateCell(5).SetCellValue(response.WorkerNames[i]);

                for (int j = 0; j < 6; j++)
                {
                    row.GetCell(j).CellStyle = columnStyle;
                    if (i == response.McpEmptiedTimestamps.Count - 1)
                        row.GetCell(j).CellStyle = lastRowStyle;
                }
            }

            emptyingLogsSheet.SetColumnWidth(0, 255 * 8);
            emptyingLogsSheet.SetColumnWidth(1, 255 * 25);
            emptyingLogsSheet.SetColumnWidth(2, 255 * 12);
            emptyingLogsSheet.SetColumnWidth(3, 255 * 36);
            emptyingLogsSheet.SetColumnWidth(4, 255 * 12);
            emptyingLogsSheet.SetColumnWidth(5, 255 * 25);
        }
    }
}