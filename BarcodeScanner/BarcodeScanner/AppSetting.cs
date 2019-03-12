using System.Data;

public class AppSetting {
    
    private DataSet oDS = new DataSet();
    
    private DataTable oTable = new DataTable();
    
    private string sFilePath = "";
    
    public AppSetting() {
        string sAssPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
        string sPath = System.IO.Path.GetDirectoryName(sAssPath);
        sFilePath = System.IO.Path.Combine(sPath, "Settings.xml");
    }
    
    public void LoadData() {
        oDS = new DataSet();
        if (System.IO.File.Exists(sFilePath)) {
            oDS.ReadXml(sFilePath);
            if ((oDS.Tables.Count > 0)) {
                oTable = oDS.Tables[0];
                return;
            }
            
        }
        
        // setup New
        oTable = new DataTable();
        oTable.Columns.Add(new DataColumn("key"));
        oTable.Columns.Add(new DataColumn("value"));
        oDS.Tables.Add(oTable);
    }
    
    public void SaveData() {
        // If System.IO.File.Exists(sFilePath) Then
        //     System.IO.File.Delete(sFilePath)
        // End If
        oTable.DataSet.WriteXml(sFilePath, XmlWriteMode.WriteSchema);
    }
    
    public void SetValue(string sKey, string sValue) {
        DataRow oDataRow;
        DataRow[] oDataRows = oTable.Select(("key = \'" 
                        + (sKey.Replace("\'", "\'\'") + "\'")));
        if ((oDataRows.Length > 0)) {
            oDataRows[0]["value"] = sValue;
        }
        else {
            oDataRow = oTable.NewRow();
            oDataRow["key"] = sKey;
            oDataRow["value"] = sValue;
            oTable.Rows.Add(oDataRow);
        }
        
    }
    
    public string GetValue(string sKey) {
        DataRow[] oDataRows = oTable.Select(("key = \'" 
                        + (sKey.Replace("\'", "\'\'") + "\'")));
        if ((oDataRows.Length > 0)) {
            return (oDataRows[0]["value"] + "");
        }
        
        return "";
    }
    
    public string GetValueDef(string sKey, string sDefVal) {
        string sValue = this.GetValue(sKey);
        if ((sValue != "")) {
            return sValue;
        }
        
        return sDefVal;
    }
}