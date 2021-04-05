using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CW
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Chart2_Load(object sender, EventArgs e)
        {
            Chart2.Series[0].ChartType=System.Web.UI.DataVisualization.Charting.SeriesChartType.Pie;
        }
    }
}