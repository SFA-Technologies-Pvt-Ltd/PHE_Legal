using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Helper
/// </summary>
public class Helper
{
    APIProcedure obj = null;
    public Helper()
    {

    }

    public DataTable GetPartyName()
    {
        obj = new APIProcedure();
        DataSet DsParty = obj.ByDataSet("select Party_ID, PartyName from tblPartyMaster");
        if (DsParty != null && DsParty.Tables[0].Rows.Count > 0)
            return DsParty.Tables[0];
        else
            return null;
    }

    public DataTable GetOIC()
    {
        obj = new APIProcedure();
        DataSet DSOIC = obj.ByDataSet("select OICMaster_ID, OICName, OICMobileNo, OICEmailID from tblOICMaster");
        if (DSOIC != null && DSOIC.Tables[0].Rows.Count > 0)
            return DSOIC.Tables[0];

        else
            return null;
    }

    public DataTable GetCasetype()
    {
        obj = new APIProcedure();
        DataSet DSOIC = obj.ByDataSet("select Casetype_ID, Casetype_Name from tbl_Legal_Casetype");
        if (DSOIC != null && DSOIC.Tables[0].Rows.Count > 0)
            return DSOIC.Tables[0];

        else
            return null;
    }

    public DataTable GetCaseNo()
    {
        obj = new APIProcedure();
        DataSet DsCaseNo = obj.ByDataSet("select Case_ID, CaseNo from tblLegalCaseRegistration where Isactive = 1 order by CaseNo asc");
        if (DsCaseNo != null && DsCaseNo.Tables[0].Rows.Count > 0)
            return DsCaseNo.Tables[0];

        else
            return null;
    }
    public DataTable GetCaseNoByCourt(string CourtType_Id)
    {
        obj = new APIProcedure();
        DataSet DsCaseNo = obj.ByDataSet("select Case_ID, CaseNo from tblLegalCaseRegistration where Isactive = 1 and CourtType_Id=" + CourtType_Id + "  order by CaseNo asc");
        if (DsCaseNo != null && DsCaseNo.Tables[0].Rows.Count > 0)
            return DsCaseNo.Tables[0];

        else
            return null;
    }
    public DataTable GetOICWiseCaseNo(string OICMaster_Id)
    {
        obj = new APIProcedure();
        DataSet DsCaseNo = obj.ByDataSet("select Case_ID, CaseNo from tblLegalCaseRegistration where Isactive = 1 and OICMaster_Id=" + OICMaster_Id + " order by CaseNo asc");
        if (DsCaseNo != null && DsCaseNo.Tables[0].Rows.Count > 0)
            return DsCaseNo.Tables[0];

        else
            return null;
    }
    public DataTable GetDistrictWiseCaseNo(string District_Id)
    {
        obj = new APIProcedure();
        DataSet DsCaseNo = obj.ByDataSet("select Case_ID, CaseNo from tblLegalCaseRegistration where Isactive = 1 and District_Id=" + District_Id + " order by CaseNo asc");
        if (DsCaseNo != null && DsCaseNo.Tables[0].Rows.Count > 0)
            return DsCaseNo.Tables[0];

        else
            return null;
    }
    public DataTable GetDvisionWiseCaseNo(string Division_Id)
    {
        obj = new APIProcedure();
        DataSet DsCaseNo = obj.ByDataSet("select Case_ID, CaseNo from tblLegalCaseRegistration CR " +
        "left join Mst_District DM on DM.District_ID=CR.District_ID " +
        "where CR.Isactive = 1 and CR.District_Id in (select District_ID from Mst_District where Division_ID=" + Division_Id + ") order by CaseNo asc");
        if (DsCaseNo != null && DsCaseNo.Tables[0].Rows.Count > 0)
            return DsCaseNo.Tables[0];

        else
            return null;
    }
    public DataTable GetCourtWiseCaseNo(string Court_Id)
    {
        obj = new APIProcedure();
        DataSet DsCaseNo = obj.ByDataSet("select Case_ID, CaseNo from tblLegalCaseRegistration CR " +
        "where CR.Isactive = 1 and CR.CourtLocation_Id in (" + Court_Id + ") order by CaseNo asc");
        if (DsCaseNo != null && DsCaseNo.Tables[0].Rows.Count > 0)
            return DsCaseNo.Tables[0];

        else
            return null;
    }
    public DataTable GetCourt()
    {
        obj = new APIProcedure();
        DataSet DsCourt = obj.ByProcedure("USP_Legal_Select_CourtType", new string[] { }, new string[] { }, "datatset");
        if (DsCourt != null && DsCourt.Tables[0].Rows.Count > 0)
            return DsCourt.Tables[0];

        else
            return null;
    }
    public DataTable GetCourtForCourt(string District_Id)
    {
        obj = new APIProcedure();
        DataSet DsCourt = obj.ByProcedure("USP_Legal_Select_CourtType", new string[] { "District_Id", "flag" }, new string[] { District_Id, "1" }, "datatset");
        if (DsCourt != null && DsCourt.Tables[1].Rows.Count > 0)
            return DsCourt.Tables[1];

        else
            return null;
    }
}