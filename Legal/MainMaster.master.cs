using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Legal_MainMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Emp_Id"] != null)
        {
            if (!IsPostBack)
            {
                ViewState["Emp_Id"] = Session["Emp_Id"].ToString();
                //1. spnUsername.InnerHtml = Session["UserName"].ToString() + "<br/><small>" + Session["Designation_Name"].ToString() + "</small>";
                spnUsername.InnerHtml = "<small>" + Session["UserName"].ToString() + "</small>";
                //spnOfficenaame.InnerHtml += "<li class='header' style='font-weight: 600;text-transform: uppercase;letter-spacing: 2px;'>" + Session["Office_Name"].ToString() + "</li>";
                //lblOfficeName.InnerHtml = Session["Office_Name"].ToString();


                //Navigation.InnerHtml = "<ul class='nav nav-pills nav-sidebar flex-column' data-widget='treeview' role='menu' data-accordion='false'>"
                //                + "<li class='nav-item'>"
                //                    + "<a href='../Legal/LegalDashboard.aspx' class='nav-link'>"
                //                        + "<i class='nav-icon fas fa-list-ul text-light'></i>"
                //                        + "<p class='text-light'>"
                //                            + "<span style='color: #e5e5e5; font-size: 16px;'>Main Page</span>"
                //    //  + "<i class='fas fa-angle-left right'></i>" 
                //                            + "</p></a></li>'";
                ////  DataTable dtAccess = Session["AccessModule"] as DataTable;
                //DataTable dtAccessForm = Session["AccessForm"] as DataTable;
                //string menuname = string.Empty;
                //DataView objdbview = new DataView(dtAccessForm);
                //DataTable objdistinctitem = objdbview.ToTable(true, "Menu_Name");
                //for (int i = 0; i < objdistinctitem.Rows.Count; i++)
                //{
                //    menuname = objdistinctitem.Rows[i]["Menu_Name"].ToString();
                //    string str = string.Format("Menu_Name = '{0}'", menuname);
                //    Navigation.InnerHtml = Navigation.InnerHtml.TrimEnd(Convert.ToChar("'"));
                //    DataRow[] rslt = dtAccessForm.Select(str);
                //    menuname = "";
                //    foreach (DataRow row in rslt)
                //    {
                //        Navigation.InnerHtml += "<li class='nav-item'>";
                //        if (menuname != row[2].ToString()) //dtAccessForm.Rows[i]["Menu_Name"].ToString())
                //        {
                //            menuname = row[2].ToString();

                //            Navigation.InnerHtml += "<a href='#' class='nav-link'>"
                //                + "<i class='nav-icon fas fa-list-ul text-light'></i>"
                //                + "<p class='text-light'>"
                //                    + "<span style='color: #e5e5e5; font-size: 16px;'>" + menuname + "</span>"
                //                    + "<i class='fas fa-angle-left right'></i>"
                //                + "</p></a>";
                //            Navigation.InnerHtml += "<ul class='nav nav-treeview'>";
                //            Navigation.InnerHtml += "<li class='nav-item'>"
                //                                + "<a href='" + row[4].ToString() + "' class='nav-link custom_bg_color'>"
                //                                    + "<i class='far fa-circle nav-icon text-light'></i>"
                //                                    + "<p class=''>" + row[3].ToString() + "</p>"
                //                                + "</a></li>";
                //        }
                //        else
                //        {
                //            Navigation.InnerHtml += "<li class='nav-item'>"
                //                                + "<a href='" + row[4].ToString() + "' class='nav-link custom_bg_color'>"
                //                                    + "<i class='far fa-circle nav-icon text-light'></i>"
                //                                    + "<p class=''>" + row[3].ToString() + "</p>"
                //                                + "</a></li>";
                //        }

                //    }

                //    Navigation.InnerHtml += "</ul>";
                //    //if (i == dtAccessForm.Rows.Count)
                //    Navigation.InnerHtml += "</li>";
                //}
                //Navigation.InnerHtml += "</ul>";
            }
        }
        else
        {
            //Response.Redirect("~/Login.aspx");
            Response.Redirect("../Login.aspx");
        }

    }
    protected void btnlogout_Click(object sender, EventArgs e)
    {

        // Response.Redirect("~/Login.aspx");
        Response.Redirect("../Login.aspx");
    }
}
