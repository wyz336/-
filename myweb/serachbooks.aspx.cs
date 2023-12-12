using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class serachbooks : System.Web.UI.Page
{
    string connStr = "initial catalog=xiaobai; data source=localhost; integrated security=sspi";

    protected void Page_Load(object sender, EventArgs e)
    {
        
        
            gridviewbind();
        
    }

    private void gridviewbind()
    {
        SqlConnection myConn = new SqlConnection(connStr);
        SqlDataAdapter myDa = new SqlDataAdapter("select * from t_book", myConn);
        myConn.Open();
        DataSet myDataSet = new DataSet();
        myDa.Fill(myDataSet);
        GridView1.DataSource = myDataSet.Tables[0];
        GridView1.DataBind();
        myConn.Close();
    }

    protected void procedure_Click(object sender, EventArgs e)
    {
        string s = Price.Text;
        string sql = "select * from t_book where bookname like '%" + s + "%'and  num>0 order by price ";
        if (Order.Checked) sql += "desc";
        SqlConnection myConn = new SqlConnection(connStr);
        SqlDataAdapter myDa = new SqlDataAdapter(sql, myConn);
        myConn.Open();
        DataSet myDataSet = new DataSet();
        myDa.Fill(myDataSet);
        GridView1.DataSource = myDataSet.Tables[0];
        GridView1.DataBind();
        myConn.Close();

    }
    /// <summary>
    /// 翻页功能
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        GridView1.PageIndex = e.NewPageIndex;
        gridviewbind();

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}