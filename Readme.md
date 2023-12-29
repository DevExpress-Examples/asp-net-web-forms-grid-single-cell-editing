<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128540907/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E430)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Template.cs](./CS/TestGridViewSite81/App_Code/Template.cs) (VB: [Template.vb](./VB/TestGridViewSite81/App_Code/Template.vb))
* [Default.aspx](./CS/TestGridViewSite81/Default.aspx) (VB: [Default.aspx](./VB/TestGridViewSite81/Default.aspx))
* [Default.aspx.cs](./CS/TestGridViewSite81/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/TestGridViewSite81/Default.aspx.vb))
<!-- default file list end -->
# How to implement a single cell editing feature in the ASPxGridView


<p><strong>UPDATED<br></strong>The new version of these examples uses a completely different approach to implement the required feature. When a cell is modified, a custom callback is sent via component ASPxCallback and the grid data source is updated manually. The drawback of such an approach is that the ASPxGridView editing API is not used here and you have to implement custom functions to update the data source directly. On the other hand, this approach works much more smooth and fast and provides more pleasant user experience.<br>The steps to implement are:<br>1. Place ASPxGridView with the enabled Batch Edit mode and ASPxCallback on the page;<br>2. Handle the <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebScriptsASPxClientGridView_BatchEditEndEditingtopic">ASPxClientGridView.BatchEditEndEditing</a>, <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxCallback_Callbacktopic">ASPxCallback.Callback</a> and <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebScriptsASPxClientCallback_CallbackCompletetopic">ASPxClientCallback.CallbackComplete</a> events;<br>3. In the BatchEditEndEditing event handler, collect the information about the edited row and send the information to the server side. Use the <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebScriptsASPxClientCallback_PerformCallbacktopic(zXTuZg)">ASPxClientCallback.PerformCallback</a> method for this.<br>4. In the Callback event handler, update the data source with the new data and return the result string (OK or the error message);<br>5. Use the CallbackComplete event handler to return the focus to the last edited cell if something went wrong. For example, a server-side validation error.<br><br><strong>Description for versions prior 16.2</strong><br>By default, the ASPxGridView allows an end-user to edit a whole row in the ASPxGridView. The sample project attached to this example shows how to edit a clicked cell only. <br>Here are the necessary steps:</p>
<p> 1) Create a new class supporting the ITemplate interface. Its instance will be used inside the EditItemTemplate Container of all the columns except for the clicked one;<br> 2) Handle the ASPxGridView's server side HtmlDataCellPrepared event to subscribe to the cell's client side onclick event;<br> 3) Determine the clicked cell and send a callback to the server in this event handler;<br> 4) Handle the ASPxGridView's CustomCallback event handler to set the columns' EditItemTemplate property;<br> 5) Handle the Page_Load method and restore templates set within the CustomCallback event handler;<br> 6) A hidden input is used on this page to eliminate a known IE issue when a postback is automatically sent to the server when the Enter key is pressed and a single visible INPUT element is on the page.</p>
<p><strong>See also:</strong><br><a href="https://www.devexpress.com/Support/Center/p/E4600">How to implement a single cell editing feature in ASPxGridView for iOS devices</a><br><a href="https://www.devexpress.com/Support/Center/p/T498424">How to implement a single cell editing feature in GridView</a></p>

<br/>


