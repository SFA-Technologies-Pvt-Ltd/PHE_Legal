using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

public partial class Legal_ImplementationAuthorityMst : System.Web.UI.Page
{
    APIProcedure obj = new APIProcedure();
    DataSet ds = new DataSet();
    CultureInfo cult = new CultureInfo("gu-IN");

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Emp_Id"] != null && Session["Office_Id"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                ViewState["Office_Id"] = Session["Office_Id"].ToString();
                FillDesigNation();
                FillLocation();
            }
        }
        else
        {
            Response.Redirect("../Login.aspx");
        }
    }
    #region FillDesignation
    protected void FillDesigNation()
    {
        try
        {
            ddlDesignation.Items.Clear();
            ds = obj.ByDataSet("select UserType_Id, UserType_Name from tblLegal_UserTypeMaster");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlDesignation.DataTextField = "UserType_Name";
                ddlDesignation.DataValueField = "UserType_Id";
                ddlDesignation.DataSource = ds;
                ddlDesignation.DataBind();

            }
            ddlDesignation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
    #endregion
    #region FillLocation
    protected void FillLocation()
    {
        try
        {
            ddlLocation.Items.Clear();
            ds = obj.ByProcedure("USP_Select_District", new string[]{}, new string[]{}, "dataset");
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ddlLocation.DataTextField = "District_Name";
                ddlLocation.DataValueField = "District_ID";
                ddlLocation.DataSource = ds;
                ddlLocation.DataBind();

            }
            ddlLocation.Items.Insert(0, new ListItem("Select", "0"));
        }
        catch (Exception ex)
        {
            lblMsg.Text = obj.Alert("fa-ban", "alert-warning", "Warning !", ex.Message.ToString());
        }
    }
     #endregion
}