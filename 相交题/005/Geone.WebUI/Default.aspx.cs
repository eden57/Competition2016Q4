using System;
using System.Configuration;
using System.Data;
using Geone.Data;
using Geone.Helper;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using QHH.Web.UI.WebControls;
using System.Drawing;

public partial class Default : System.Web.UI.Page
{
    private int _PageSize
    {
        set { ViewState["PageSize"] = value; }
        get
        {
            return Convert.ToInt32(ViewState["PageSize"]);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _PageSize = 20;
            ShowData(0, _PageSize);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DBAccess.ExecuteNonQuery("delete from ResultC");
        string sql = @"declare @OBJECTID			int
	                   declare @Shape				geometry

	                    DECLARE myCursor CURSOR
	                       FOR select OBJECTID, Shape from dbo.AA01交通水利用地图斑2014

	                    OPEN myCursor
	                    FETCH NEXT FROM myCursor INTO @OBJECTID, @Shape

	                    WHILE @@FETCH_STATUS = 0
	                    BEGIN
		                    insert ResultC
		                    select @OBJECTID, OBJECTID, SHAPE.STIntersection(@Shape).STArea() 
		                    from AB02非建设用地图斑2009 a
		                    where a.SHAPE.STIntersects(@Shape) =1 and a.SHAPE.STIntersection(@Shape).STArea() > 0			
			
		                    FETCH NEXT FROM myCursor INTO @OBJECTID, @Shape
	                    END

	                    CLOSE myCursor
	                    DEALLOCATE myCursor";
        ExecuteDataTable(sql);
        ShowData(0, _PageSize);
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        ShowData(0, _PageSize);
    }

    private void ShowData(long PageIndex, long PageSize)
    {
        string sql = "select ROW_NUMBER() OVER (ORDER BY id asc) AS RowNumber, * from ResultC";
        DataTable dt = DBAccess.ExecuteDataTable(sql);
        BindGrid(gvData, Navigator, PageIndex, PageSize, dt);
    }

    protected void Navigator_PageChanged(object sender, QHH.Web.UI.EventArg.NavigatorEventArgs e)
    {
        _PageSize = (int)e.PageSize;
        ShowData(e.NewPageIndex, _PageSize);
    }

    public DataTable ExecuteDataTable(string sql)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection cn = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand(sql, cn);
        cmd.CommandTimeout = 3600;
        cn.Open();

        SqlDataAdapter sdap = new SqlDataAdapter();
        sdap.SelectCommand = cmd;
        DataTable dt = new DataTable();
        sdap.Fill(dt);
        cn.Close();
        return dt;
    }       

    protected void BindGrid(GridView gvData, Navigator navigator, long PageIndex, long PageSize, DataTable dt)
    {
        navigator.NextPreviousSetting.CustomCellColor = Color.Transparent;
        navigator.NextPreviousSetting.FirstPageCellColor = Color.Transparent;
        navigator.NextPreviousSetting.PreviousPageCellColor = Color.Transparent;
        navigator.NextPreviousSetting.NextPageCellColor = Color.Transparent;
        navigator.NextPreviousSetting.LastPageCellColor = Color.Transparent;
        navigator.NextPreviousSetting.PageListCellColor = Color.Transparent;
        navigator.NextPreviousSetting.PageListCellWidth = new Unit(100);
        navigator.NextPreviousSetting.PageSizeCellColor = Color.Transparent;
        navigator.NextPreviousSetting.PageSizeCellWidth = new Unit(100);
        navigator.NumericSetting.CustomCellColor = Color.Transparent;
        navigator.NumericSetting.NumericCellColor = Color.Transparent;
        navigator.NumericSetting.PageListCellColor = Color.Transparent;
        navigator.NumericSetting.PageSizeCellColor = Color.Transparent;

        if (dt.Rows.Count == 0)
        {
            dt.Rows.Add(dt.NewRow());
            gvData.DataSource = dt;
            gvData.DataBind();
            gvData.Rows[0].Cells.Clear();

            navigator.Visible = false;
        }
        else
        {
            gvData.DataSource = dt;
            gvData.AllowPaging = true;
            gvData.PagerSettings.Visible = false;
            gvData.AllowSorting = true;
            gvData.PageIndex = (int)PageIndex;
            gvData.PageSize = (int)(PageSize);
            gvData.DataBind();

            navigator.Visible = true;
            navigator.RecordCount = dt.Rows.Count;
            navigator.PageIndex = PageIndex;
            navigator.PageSize = PageSize;
        }
    }
}
