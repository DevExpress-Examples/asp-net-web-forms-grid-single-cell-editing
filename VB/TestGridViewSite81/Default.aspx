<%@ Page Language="vb" AutoEventWireup="true"  CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
	Namespace="System.Web.UI" TagPrefix="cc1" %>


<%@ Register Assembly="DevExpress.Web.ASPxGridView.v8.2"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v8.2"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>Untitled Page</title>
	<script type="text/javascript">
		function onCellClick(rowIndex, fieldName) {
			ASPxGridView1.PerformCallback(rowIndex + "|" + fieldName);
		}
		function OnEditorKeyPress(editor, e) {
			if(e.htmlEvent.keyCode == 13 || e.htmlEvent.keyCode == 9) {
				ASPxGridView1.UpdateEdit();
			}
			else
				if(e.htmlEvent.keyCode == 27)
					ASPxGridView1.CancelEdit();
		}
	</script>
</head>
<body>

	<form id="form1" runat="server">
		&nbsp;<asp:AccessDataSource ID="AccessDataSource1" runat="server" DataFile="~/App_Data/pubs.mdb"
			SelectCommand="SELECT * FROM [jobs]" DeleteCommand="DELETE FROM [jobs] WHERE [job_id] = ?" InsertCommand="INSERT INTO [jobs] ([job_id], [job_desc], [min_lvl], [max_lvl]) VALUES (?, ?, ?, ?)" UpdateCommand="UPDATE [jobs] SET [job_desc] = ?, [min_lvl] = ?, [max_lvl] = ? WHERE [job_id] = ?">
			<DeleteParameters>
				<asp:Parameter Name="job_id" Type="Int16" />
			</DeleteParameters>
			<UpdateParameters>
				<asp:Parameter Name="job_desc" Type="String" />
				<asp:Parameter Name="min_lvl" Type="Byte" />
				<asp:Parameter Name="max_lvl" Type="Byte" />
				<asp:Parameter Name="job_id" Type="Int16" />
			</UpdateParameters>
			<InsertParameters>
				<asp:Parameter Name="job_id" Type="Int16" />
				<asp:Parameter Name="job_desc" Type="String" />
				<asp:Parameter Name="min_lvl" Type="Byte" />
				<asp:Parameter Name="max_lvl" Type="Byte" />
			</InsertParameters>
		</asp:AccessDataSource>
		<br />
		<dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False"
			DataSourceID="AccessDataSource1" KeyFieldName="job_id" OnHtmlDataCellPrepared="ASPxGridView1_HtmlDataCellPrepared" Width="742px" OnCustomCallback="ASPxGridView1_CustomCallback" OnHtmlRowCreated="ASPxGridView1_HtmlRowCreated" OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize" OnRowUpdated="ASPxGridView1_RowUpdated">
			<Columns>
				<dxwgv:GridViewDataTextColumn FieldName="job_id" ReadOnly="True" VisibleIndex="0">
				</dxwgv:GridViewDataTextColumn>
				<dxwgv:GridViewDataTextColumn FieldName="job_desc" VisibleIndex="1">
				</dxwgv:GridViewDataTextColumn>
				<dxwgv:GridViewDataTextColumn FieldName="min_lvl" VisibleIndex="2">
				</dxwgv:GridViewDataTextColumn>
				<dxwgv:GridViewDataTextColumn FieldName="max_lvl" VisibleIndex="3">
				</dxwgv:GridViewDataTextColumn>
			</Columns>
			<SettingsEditing Mode="Inline" />
			<SettingsBehavior AllowFocusedRow="True" />
		</dxwgv:ASPxGridView>
		<div style="visibility:hidden">
			<input id="inputIE" type="text"/>
		</div>
	</form>
</body>
</html>
