/// <reference path="../../jQuery/jquery_1.9.1_vsdoc.js" />



$(function () {
 

 $("#btnSave").on("click", save);
 $("#btnCancel").on("click", cancel);

 if (typeof dialogArguments != "undefined" && typeof dialogArguments != null) {


   showRecords();
 
  
 }

});


function selectRow() {
 $(".sdk-sample-List-SelectedRow").removeClass("sdk-sample-List-SelectedRow").addClass("sdk-sample-List-Row");
 $(this).removeClass("sdk-sample-List-Row");
 $(this).addClass("sdk-sample-List-SelectedRow");
}

function showRecords() {
 var summaryMessage = "";
 var numberOfRecords = dialogArguments.length;
 var resultsTable = $("<table class='sdk-sample-List-Data' id='gridBodyTable' />");
 resultsTable.attr("border", 1);
 resultsTable.attr("cellspacing", 0);
 resultsTable.attr("cellpadding", 1);
 

 if (numberOfRecords > 1)
 { summaryMessage = "This list contains " + numberOfRecords + " Test records." }
 else
 {
  summaryMessage = "This list contains 1 Test record."
  var DlgHdTitleText = "Potential Duplicate Test Detected";
  $("#DlgHdTitle").text(DlgHdTitleText);
  $("#DlgHdTitle").attr("title", DlgHdTitleText);
  var DlgHdDescText = "This test may be a duplicate of the test below."
  $("#DlgHdDesc").text(DlgHdDescText);
  $("#DlgHdDesc").attr("title", DlgHdDescText);

 }
 resultsTable.attr("summary", summaryMessage);

 var colGroup = $("<colgroup />");
 colGroup.append($("<col width='50' class='sdk-sample-List-DataColumn' name='sample_name' />"));
 colGroup.append($("<col width='100' class='sdk-sample-List-DataColumn' name='sample_account' />"));
 colGroup.append($("<col width='50' class='sdk-sample-List-DataColumn' name='sample_bool' />"));
 colGroup.append($("<col width='50' class='sdk-sample-List-DataColumn' name='sample_date' />"));
 colGroup.append($("<col width='50' class='sdk-sample-List-DataColumn' name='sample_decimal' />"));
 colGroup.append($("<col width='50' class='sdk-sample-List-DataColumn' name='sample_double' />"));
 colGroup.append($("<col width='50' class='sdk-sample-List-DataColumn' name='sample_integer' />"));
 colGroup.append($("<col width='50' class='sdk-sample-List-DataColumn' name='sample_memo' />"));
 colGroup.append($("<col width='50' class='sdk-sample-List-DataColumn' name='sample_money' />"));
 colGroup.append($("<col width='50' class='sdk-sample-List-DataColumn' name='sample_optionset' />"));
 colGroup.append($("<col />"));
 resultsTable.append(colGroup);

 for (var i = 0; i < dialogArguments.length; i++) {
  var record = dialogArguments[i];
  var row = $("<tr class='sdk-sample-List-Row' otypename='sample_duplicatedetecttest' oid=" + record.id + " />");


  //sample_name column
  var sample_nameCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var sample_nameGridcellpadding = $("<nobr class='gridcellpadding' />");
  var sample_nameLink = $("<a class='sdk-sample-List-Link' />");
  sample_nameLink.attr("title", record.sample_name)
  sample_nameLink.attr("tabindex", i)
  sample_nameLink.attr("href", "#")
  sample_nameLink.attr("target", "_blank")
  sample_nameLink.attr("oid", record.id)
  sample_nameLink.attr("otypename", 'sample_duplicatedetecttest')
  sample_nameLink.text(record.sample_name);
  sample_nameGridcellpadding.append(sample_nameLink);
  sample_nameCell.append(sample_nameGridcellpadding);
  sample_nameCell.append(sample_nameGridcellpadding);
  row.append(sample_nameCell);
  sample_nameLink.on("click", openRecord);

  ////primary_attribute_name column
  //var primary_attribute_nameCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  //var primary_attribute_nameGridcellpadding = $("<nobr class='gridcellpadding' />");
  //var primary_attribute_nameLink = $("<a class='sdk-sample-List-Link' />");
  //primary_attribute_nameLink.attr("title", record.primary_attribute_name)
  //primary_attribute_nameLink.attr("tabindex", i)
  //primary_attribute_nameLink.attr("href", "#")
  //primary_attribute_nameLink.attr("target", "_blank")
  //primary_attribute_nameLink.attr("oid", record.id)
  //primary_attribute_nameLink.attr("otypename", 'sample_duplicatedetecttest')
  //primary_attribute_nameLink.text(record.primary_attribute_name);
  //primary_attribute_nameGridcellpadding.append(primary_attribute_nameLink);
  //primary_attribute_nameCell.append(primary_attribute_nameGridcellpadding);
  //primary_attribute_nameCell.append(primary_attribute_nameGridcellpadding);
  //row.append(primary_attribute_nameCell);
  //primary_attribute_nameLink.on("click", openRecord);


  //sample_account column
  var sample_accountCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var sample_accountGridcellpadding = $("<nobr class='gridcellpadding' />");
  sample_accountGridcellpadding.attr("title", record.sample_account)
  sample_accountGridcellpadding.text(record.sample_account);
  sample_accountCell.append(sample_accountGridcellpadding);
  row.append(sample_accountCell);


  //sample_bool column
  var sample_boolCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var sample_boolGridcellpadding = $("<nobr class='gridcellpadding' />");
  sample_boolGridcellpadding.attr("title", record.sample_bool)
  sample_boolGridcellpadding.text(record.sample_bool);
  sample_boolCell.append(sample_boolGridcellpadding);
  row.append(sample_boolCell);

  //sample_date column
  var sample_dateCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var sample_dateGridcellpadding = $("<nobr class='gridcellpadding' />");
  sample_dateGridcellpadding.attr("title", record.sample_date)
  sample_dateGridcellpadding.text(record.sample_date);
  sample_dateCell.append(sample_dateGridcellpadding);
  row.append(sample_dateCell);

  //sample_decimal column
  var sample_decimalCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var sample_decimalGridcellpadding = $("<nobr class='gridcellpadding' />");
  sample_decimalGridcellpadding.attr("title", record.sample_decimal)
  sample_decimalGridcellpadding.text(record.sample_decimal);
  sample_decimalCell.append(sample_decimalGridcellpadding);
  row.append(sample_decimalCell);

  //sample_double column
  var sample_doubleCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var sample_doubleGridcellpadding = $("<nobr class='gridcellpadding' />");
  sample_doubleGridcellpadding.attr("title", record.sample_double)
  sample_doubleGridcellpadding.text(record.sample_double);
  sample_doubleCell.append(sample_doubleGridcellpadding);
  row.append(sample_doubleCell);

  //sample_integer column
  var sample_integerCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var sample_integerGridcellpadding = $("<nobr class='gridcellpadding' />");
  sample_integerGridcellpadding.attr("title", record.sample_integer)
  sample_integerGridcellpadding.text(record.sample_integer);
  sample_integerCell.append(sample_integerGridcellpadding);
  row.append(sample_integerCell);

  //sample_memo column
  var sample_memoCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var sample_memoGridcellpadding = $("<nobr class='gridcellpadding' />");
  sample_memoGridcellpadding.attr("title", record.sample_memo)
  sample_memoGridcellpadding.text(record.sample_memo);
  sample_memoCell.append(sample_memoGridcellpadding);
  row.append(sample_memoCell);

  //sample_money column
  var sample_moneyCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var sample_moneyGridcellpadding = $("<nobr class='gridcellpadding' />");
  sample_moneyGridcellpadding.attr("title", record.sample_money)
  sample_moneyGridcellpadding.text(record.sample_money);
  sample_moneyCell.append(sample_moneyGridcellpadding);
  row.append(sample_moneyCell);

  //sample_optionset column
  var sample_optionsetCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  var sample_optionsetGridcellpadding = $("<nobr class='gridcellpadding' />");
  sample_optionsetGridcellpadding.attr("title", record.sample_optionset)
  sample_optionsetGridcellpadding.text(record.sample_optionset);
  sample_optionsetCell.append(sample_optionsetGridcellpadding);
  row.append(sample_optionsetCell);

  // Find/Replace the attribute name
  ////attributeName column
  //var attributeNameCell = $("<td class='sdk-sample-List-DataCell inner-grid-cellPadding' />");
  //var attributeNameGridcellpadding = $("<nobr class='gridcellpadding' />");
  //attributeNameGridcellpadding.attr("title", record.attributeName)
  //attributeNameGridcellpadding.text(record.attributeName);
  //attributeNameCell.append(attributeNameGridcellpadding);
  //row.append(attributeNameCell);






  //spacer Column
  row.append($("<td class='sdk-sample-List-DataCell'><nobr class='gridcellpadding'></nobr></td>"));

  row.on("click", selectRow);
  row.on("dblclick", openRecord);

  resultsTable.append(row);

 }
 $("#DlgHdBodyContainer").append(resultsTable);

}

function openRecord() {
 var type = $(this).attr("otypename");
 var id = $(this).attr("oid");
 window.returnValue = { "type": type, "id": id };
 window.close();
 return false;
}

 function save() {
  window.returnValue = "save";
  window.close();
 }
 function cancel() {
  window.returnValue = "cancel";
  window.close();
 }