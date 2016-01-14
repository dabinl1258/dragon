using UnityEngine;
using System.Collections;
using System.Data;
using System.Data.Odbc;
using System.Text;

public class MExcelDataChunk
{
    public string[,] arrData = null;
    public int nX = 0;
    public int nY = 0;

    public MExcelDataChunk()
    {

    }

    public MExcelDataChunk(string[,] data, int x, int y)
    {
        arrData = data;
        nX = x;
        nY = y;
    }
}

public class MExcelParser
{
    public static MExcelDataChunk ReadXLS(string sPathData, string sSheetName)
    {
        string con = "Driver={Microsoft Excel Driver (*.xls)}; DriverId=790; Dbq=" + sPathData + ";";
        string yourQuery = "SELECT * FROM [" + sSheetName + "$]";

        OdbcConnection oCon = new OdbcConnection(con);
        OdbcCommand oCmd = new OdbcCommand(yourQuery, oCon);
        DataTable dtYourData = new DataTable("YourData");
        
        oCon.Open();
        
        OdbcDataReader rData = oCmd.ExecuteReader();
        
        dtYourData.Load(rData);
        rData.Close();
        oCon.Close();
        
        string[,] str = new string[dtYourData.Rows.Count, dtYourData.Columns.Count];

        if (dtYourData.Rows.Count <= 0)
            return null;

        for (int i = 0; i < dtYourData.Rows.Count; i++)
        {
            for (int j = 0; j < dtYourData.Columns.Count; j++)
                str[i,j] = dtYourData.Rows[i][dtYourData.Columns[j].ColumnName].ToString();
        }

        MExcelDataChunk chunk = new MExcelDataChunk(str, dtYourData.Columns.Count, dtYourData.Rows.Count);

        return chunk;
    }
}