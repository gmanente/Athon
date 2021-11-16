using Stimulsoft.Report;
using Stimulsoft.Report.Export;
using Stimulsoft.Report.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Sistema.Web.UI.Relatorio.Util
{
    public class StiReportResponseExtend 
    {
        public static void ResponseAsPdf(Page page, StiReport report)
        {
            StiReportResponseExtend.ResponseAsPdf(report);
        }

        public static void ResponseAsPdf(Page page, StiReport report, bool openDialog)
        {
            StiReportResponseExtend.ResponseAsPdf(report, openDialog);
        }

        public static void ResponseAsPdf(Page page, StiReport report, bool openDialog, StiPdfExportSettings settings)
        {
            StiReportResponseExtend.ResponseAsPdf(report, settings);
        }

        public static void ResponseAsXls(Page page, StiReport report)
        {
            StiReportResponseExtend.ResponseAsXls(report);
        }

        public static void ResponseAsXls(Page page, StiReport report, StiExcelExportSettings settings)
        {
            StiReportResponseExtend.ResponseAsXls(report, settings);
        }

        public static void ResponseAsXls(Page page, StiReport report, StiExcelExportSettings settings, bool saveFileDialog)
        {
            StiReportResponseExtend.ResponseAsXls(report, settings, saveFileDialog);
        }

        public static void ResponseAsExcel2007(Page page, StiReport report)
        {
            StiReportResponseExtend.ResponseAsExcel2007(report);
        }

        public static void ResponseAsWord2007(Page page, StiReport report)
        {
            StiReportResponseExtend.ResponseAsWord2007(report);
        }



    }
}