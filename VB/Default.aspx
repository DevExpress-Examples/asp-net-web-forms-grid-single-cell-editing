<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<script>
		function onBatchEditEndEditing(s, e) {
			var cellInfo = s.batchEditApi.GetEditCellInfo();
			setTimeout(function() {
				if (s.batchEditApi.HasChanges(cellInfo.rowVisibleIndex, cellInfo.column.index))
					UpdateEdit(createObject(s, s.GetRowKey(e.visibleIndex), e.rowValues), cellInfo);
			}, 0);
		}

		function UpdateEdit(object, cellInfo) {
			callback.cpCellInfo = cellInfo;
			callback.PerformCallback(JSON.stringify(object));
		}
		function onEndUpdateCallback(s, e) {
			if (e.result != "OK") {
				alert(e.result);
				grid.batchEditApi.StartEdit(s.cpCellInfo.rowVisibleIndex, s.cpCellInfo.column.index);
			}
		}
		function createObject(grid, key, values) {
			var object = {};
			object["ID"] = key;
			Object.keys(values).map(function(k) {
				object[grid.GetColumn(k).fieldName] = values[k].value;
			});

			return object;
		}
	</script>
</head>
<body>
	<form id="form1" runat="server">
		<div>
			<p>To modify a cell, click on it and enter a value. </p>
			 <p>   After you leave the cell, a callback will be sent to the server and the data source will be updated with the new value.</p>
			<dx:ASPxGridView ID="ASPxGridView1" runat="server" ClientInstanceName="grid" Width="100%"
				KeyFieldName="ID">
				<Styles>
					<BatchEditModifiedCell BackColor="White"></BatchEditModifiedCell>
				</Styles>
				<ClientSideEvents BatchEditEndEditing="onBatchEditEndEditing" />
				<SettingsEditing Mode="Batch">
					<BatchEditSettings StartEditAction="Click" ShowConfirmOnLosingChanges="false" />
				</SettingsEditing>
				<Columns>
					<dx:GridViewDataColumn FieldName="C1" Width="20%" />
					<dx:GridViewDataSpinEditColumn FieldName="C2" Width="20%" />
					<dx:GridViewDataTextColumn FieldName="C3" Width="20%" />
					<dx:GridViewDataCheckColumn FieldName="C4" Width="20%" />
					<dx:GridViewDataDateColumn FieldName="C5" Width="20%" />
				</Columns>
			</dx:ASPxGridView>
			<dx:ASPxCallback ID="ASPxCallback" runat="server" ClientInstanceName="callback" OnCallback="ASPxCallback_Callback">
				<ClientSideEvents CallbackComplete="onEndUpdateCallback" />
			</dx:ASPxCallback>
		</div>
	</form>
</body>
</html>