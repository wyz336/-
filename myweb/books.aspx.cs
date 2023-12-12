using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YF;
using YFModel;
using YFDAL;
using YFBLL;

public partial class books : System.Web.UI.Page

{
    string connStr = "initial catalog=xiaobai; data source=localhost; integrated security=sspi";

    protected void Page_Load(object sender, EventArgs e)
    {
        //获取id
        string id = Request["id"];
        YF.Model.Books books = YF.BLL.Books.GetBooks(int.Parse(id));
        this.isbn.Text = books.Isbn;
        this.price.Text = books.Price.ToString();
        this.num.Text = books.Num.ToString();
        this.detail.Text = books.Detail.ToString();
        this.img.ImageUrl = "admin/img/" +books.Img.ToString();
        this.img.Width = 180;
        this.bookname.Text = books.Bookname.ToString();
        gridviewbind();
    }
    private void gridviewbind()
    {
        SqlConnection myConn = new SqlConnection(connStr);
        SqlDataAdapter myDa = new SqlDataAdapter("select * from t_comment where bookid="+ Request["id"], myConn);
        myConn.Open();
        DataSet myDataSet = new DataSet();
        myDa.Fill(myDataSet);
        GridView1.DataSource = myDataSet.Tables[0];
        GridView1.DataBind();
        myConn.Close();

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //Response.Redirect(Request.Url.ToString());
        GridView1.PageIndex = e.NewPageIndex;
        gridviewbind();

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   public void submit_Click(object sender, EventArgs e)
    {
        YF.Model.User myuser = (YF.Model.User)YF.SessionHelper.GetSesstion("user");//读取当前用户信息
        if(myuser == null) YF.JsHelper.AlertAndRedirect("请先登录", "/login.aspx");
        Comment comment=new Comment();
        comment.userid = myuser.Id;
        comment.comment = TextBox1.Text;
        comment.username = myuser.Username;
        comment.bookid = int.Parse(Request["id"]);
        CommentService.insert(comment);
        YF.JsHelper.Alert("添加成功");
        Response.AddHeader("Refresh", "0");

    }

}