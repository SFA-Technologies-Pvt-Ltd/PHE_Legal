using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data;
public partial class Legal_HoMaster : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    APIProcedure obj = new APIProcedure();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
            ViewState["Office_Id"] = Session["Office_Id"].ToString();
            FillGrid();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                lblMsg.Text = "";
                if (btnSave.Text == "Save")
                {
                    ds = obj.ByProcedure("Usp_InsertHoMaster", new string[] { "HoName", "Office_Id", "CreatedBy", "CreatedByIP" }
                    , new string[] { txtHoName.Text.Trim(), ViewState["Office_Id"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress() }, "dataset");
                }
                else if (btnSave.Text == "Edit" && ViewState["Ho_Id"].ToString() != "" && ViewState["Ho_Id"].ToString() != null)
                {
                    ds = obj.ByProcedure("Usp_UpdateHoMaster", new string[] { "HoName", "Office_Id", "LastUpdatedBy", "LastUpdatedByIP", "Ho_Id" }
                                                        , new string[] { txtHoName.Text.Trim(), ViewState["Office_Id"].ToString(), ViewState["Emp_Id"].ToString(), obj.GetLocalIPAddress(), ViewState["Ho_Id"].ToString() }, "dataset");
                }
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    string ErrorMsg = ds.Tables[0].Rows[0]["ErrorMsg"].ToString();
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "OK")
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-success", "Thanks !", ErrorMsg);
                        FillGrid();
                        btnSave.Text = "Save";
                        ClearData();
                    }
                    else
                    {
                        lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ErrorMsg);
                    }
                }
                else
                {
                    lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Warning !", ds.Tables[0].Rows[0]["Msg"].ToString());
                }
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "Alert-danger", "Sorry !", ex.Message.ToString());
        }
    }
    protected void ClearData()
    {
        txtHoName.Text = "";
        btnSave.Text = "Save";
    }
    protected void FillGrid()
    {
        try
        {
            ds = obj.ByProcedure("Usp_SelectHomaster", new string[] { }, new string[] { }, "Dataset");
            if (ds != null && ds.Tables.Count > 0)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        {
            try
            {
                lblMsg.Text = "";
                ViewState["Ho_Id"] = "";
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                Label lblHoId = (Label)row.FindControl("lblHoId");
                Label lblHoName = (Label)row.FindControl("lblHoName");
                ViewState["Ho_Id"] = lblHoId.Text;
                if (e.CommandName == "EditDetails")
                {
                    txtHoName.Text = lblHoName.Text;
                    btnSave.Text = "Edit";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = obj.Alert("fa-ban", "alert-danger", "Sorry!", ex.Message.ToString());
            }
            finally { if (ds != null) { ds.Dispose(); } }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblMsg.Text = "";
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }
}

