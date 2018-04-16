Imports DevExpress.Web.Data
Imports System.Collections.Specialized
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Newtonsoft.Json
Partial Public Class _Default
	Inherits System.Web.UI.Page

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
	End Sub

	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
		ASPxGridView1.DataSource = GridDataItem.GetData()
		ASPxGridView1.DataBind()
	End Sub

	Protected Sub ASPxCallback_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgs)
		Try
			GridDataItem.UpdateData(JsonConvert.DeserializeObject(Of GridDataItem)(e.Parameter))
			e.Result = "OK"
		Catch ex As Exception
			e.Result = ex.Message
		End Try
	End Sub
End Class