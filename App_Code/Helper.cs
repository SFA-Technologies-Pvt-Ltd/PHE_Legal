using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Helper
/// </summary>
public  class Helper
{
    APIProcedure obj = null;
	public Helper()
	{
		
	}

    public  DataTable GetPartyName()
    {
        obj = new APIProcedure();
        DataSet DsParty = obj.ByDataSet("select Party_ID, PartyName from tblPartyMaster");
        if (DsParty != null && DsParty.Tables[0].Rows.Count > 0)
            return DsParty.Tables[0];
        else
           return null;
    }
}