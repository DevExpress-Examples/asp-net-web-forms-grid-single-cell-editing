using DevExpress.Web.Data;
using System.Collections.Specialized;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
public partial class _Default : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
    }

    protected void Page_Init(object sender, EventArgs e) {
        ASPxGridView1.DataSource = GridDataItem.GetData();
        ASPxGridView1.DataBind();
    }

    protected void ASPxCallback_Callback(object source, DevExpress.Web.CallbackEventArgs e) {
        try {
            GridDataItem.UpdateData(JsonConvert.DeserializeObject<GridDataItem>(e.Parameter));
            e.Result = "OK";
        }
        catch(Exception ex) {
            e.Result = ex.Message;
        }
    }
}